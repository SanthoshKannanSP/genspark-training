-- Cursors 
-- Write a cursor to list all customers and how many rentals each made. Insert these into a summary table.
CREATE TABLE summary (
	customer_id INT PRIMARY KEY,
	customer_name VARCHAR(1000),
	rental_count INT
);

DO $$
DECLARE
	customer_cursor cursor for
	select customer_id, concat(first_name,' ',last_name) name from customer order by 2;
	customer_record record;
BEGIN
	OPEN customer_cursor;

	LOOP
		FETCH customer_cursor INTO customer_record;
		EXIT WHEN NOT FOUND;

		INSERT INTO summary
		SELECT customer_record.customer_id, customer_record.name, COUNT(*) rental_count FROM rental
		WHERE rental.customer_id = customer_record.customer_id;
	END LOOP;
	CLOSE customer_cursor;
END;
$$;

select * from summary;

-- Using a cursor, print the titles of films in the 'Comedy' category rented more than 10 times.
DO $$
DECLARE
	title_cursor CURSOR FOR
	SELECT film.film_id,title FROM film
		JOIN film_category ON film.film_id = film_category.film_id
		where film_category.category_id = (
			select category_id from category where name like 'Comedy'
		);
	title_record record;
	rental_count INT;
BEGIN
	OPEN title_cursor;
	LOOP
		FETCH title_cursor INTO title_record;
		EXIT WHEN NOT FOUND;

		SELECT COUNT(*) INTO rental_count FROM rental 
		join inventory ON rental.inventory_id = rental.inventory_id
		WHERE film_id = title_record.film_id;
		
		IF (rental_count > 10)
		THEN RAISE NOTICE 'Title: %, Count: %', title_record.title, rental_count;
		END IF;
	END LOOP;
	CLOSE title_cursor;
END;
$$;

-- Create a cursor to go through each store and count the number of distinct films available, and insert results into a report table.
CREATE TABLE report (
	store_id INT,
	film_count INT
);

DO $$
DECLARE
	store_cursor CURSOR FOR
	SELECT store_id FROM store;
	store_record record;
	film_count INT;
BEGIN
	OPEN store_cursor;
	LOOP
		FETCH store_cursor INTO store_record;
		EXIT WHEN NOT FOUND;

		SELECT COUNT(DISTINCT film_id) INTO film_count FROM inventory
		WHERE inventory.store_id = store_record.store_id;

		INSERT INTO report VALUES (store_record.store_id, film_count);
	END LOOP;
	CLOSE store_cursor;
END;
$$;

select * from report;

-- Loop through all customers who haven't rented in the last 6 months and insert their details into an inactive_customers table.
CREATE TABLE inactive_customers (
	customer_id INT,
	first_name VARCHAR(45),
	last_name VARCHAR(45),
	email VARCHAR(50),
	address_id INT
);

DO $$
DECLARE
	customer_cursor CURSOR FOR
	SELECT customer_id, first_name, last_name, email, address_id
	FROM customer;

	customer_record RECORD;
	last_rental_date DATE;
BEGIN
	OPEN customer_cursor;
	LOOP
		FETCH customer_cursor INTO customer_record;
		EXIT WHEN NOT FOUND;

		SELECT MAX(rental_date) INTO last_rental_date
		FROM rental where customer_id = customer_record.customer_id;

		IF (NOW() - last_rental_date > '6 months'::INTERVAL)
		THEN INSERT INTO inactive_customers VALUES 
		(customer_record.customer_id, customer_record.first_name, customer_record.last_name, customer_record.email, customer_record.address_id);
		END IF;
	END LOOP;
	CLOSE customer_cursor;
END;
$$;

select * from inactive_customers;

--------------------------------------------------------------------------

-- Transactions 
-- Write a transaction that inserts a new customer, adds their rental, and logs the payment – all atomically.
BEGIN TRANSACTION;
	INSERT INTO customer values
	(600,1,'John','Doe','johndoe@gmail.com',5,true,NOW(),NOW(),1);
	select * from payment;
	
	INSERT INTO rental values
	(60000,NOW(),1525,600,null,2,NOW());

	INSERT INTO payment values
	(34000, 600, 2, 60000, 8.99, NOW());

COMMIT;

-- Simulate a transaction where one update fails (e.g., invalid rental ID), and ensure the entire transaction rolls back.
BEGIN TRANSACTION;
	INSERT INTO customer values
	(611,1,'Jane','Doe','janedoe@gmail.com',5,true,NOW(),NOW(),1);
	select * from payment;
	
	INSERT INTO rental values
	(121,NOW(),1525,611,null,2,NOW());

	INSERT INTO payment values
	(34001, 611, 2, 121, 8.99, NOW());

ROLLBACK;


-- Use SAVEPOINT to update multiple payment amounts. Roll back only one payment update using ROLLBACK TO SAVEPOINT.
select * from payment order by payment_id desc

BEGIN TRANSACTION;
UPDATE payment SET amount = 10.99 WHERE payment_id = 34000;
UPDATE payment SET amount = 10.99 WHERE payment_id = 32099;
SAVEPOINT my_savepoint;
UPDATE payment SET amount = 10.99 WHERE payment_id = 32098;
ROLLBACK TO my_savepoint;
COMMIT;

select * from payment order by payment_id desc

-- Perform a transaction that transfers inventory from one store to another (delete + insert) safely.
BEGIN TRANSACTION;
	CREATE TEMP TABLE temp_inventory (
		inventory_id INT,
		film_id INT,
		store_id INT,
		last_update TIMESTAMP
	) ON COMMIT DROP;
	INSERT INTO temp_inventory SELECT * FROM inventory;
	update inventory
	SET store_id=1
	where inventory_id in (select inventory_id from temp_inventory where store_id=2);
	update inventory
	SET store_id=2
	where inventory_id in (select inventory_id from temp_inventory where store_id=1);
COMMIT;

select * from inventory order by inventory_id desc;

-- Create a transaction that deletes a customer and all associated records (rental, payment), ensuring referential integrity.
BEGIN TRANSACTION;
	DELETE FROM payment
	WHERE customer_id = 49;

	DELETE FROM rental
	WHERE customer_id = 49;
	
	DELETE FROM customer
	WHERE customer_id = 49;
COMMIT;

select * from customer;
----------------------------------------------------------------------------

-- Triggers
-- Create a trigger to prevent inserting payments of zero or negative amount.
create or replace function trigger_check_payment()
returns trigger
language plpgsql
as $$
begin
	IF (new.amount<=0)
	then raise exception 'Invalid payment amount';
	end if;
	return new;
end;
$$;

create trigger insert_payment
before insert
on payment
for each row
execute procedure trigger_check_payment()

insert into payment (customer_id,staff_id,rental_id,amount,payment_date) values (341,2,1520,0,'2007-02-15 22:25:46.996577')

-- Set up a trigger that automatically updates last_update on the film table when the title or rental rate is changed.
create or replace function trigger_set_last_update()
returns trigger
language plpgsql
as $$
begin
	new.last_update := NOW();
	return new;
end;
$$;

create trigger update_film
before update
on film
for each row
execute procedure trigger_set_last_update()

-- Write a trigger that inserts a log into rental_log whenever a film is rented more than 3 times in a week
CREATE TABLE rental_log (
	log_id SERIAL PRIMARY KEY,
	film_id INT,
	rental_count INT
);

create or replace function trigger_rental_log()
returns trigger
language plpgsql
as $$
DECLARE
	v_film_id INT;
	rental_count INT;
begin
	select film_id INTO v_film_id from rental
	join inventory on inventory.inventory_id = rental.inventory_id
	where rental_id = new.rental_id;
	SELECT COUNT(*) INTO rental_count FROM rental
	join inventory on inventory.inventory_id = rental.inventory_id
	where NOW() - rental_date < '7 days'::INTERVAL
	AND film_id = v_film_id;

	IF (rental_count > 3)
	THEN INSERT INTO rental_log (film_id, rental_count) VALUES (v_film_id,rental_count);
	END IF;
	RETURN NEW;
end;
$$;

create trigger insert_rental_log
after insert
on rental
for each row
execute procedure trigger_rental_log()

insert into rental (rental_date,inventory_id,customer_id,staff_id)
values (NOW(),2080,459,2);
insert into rental (rental_date,inventory_id,customer_id,staff_id)
values (NOW(),2080,500,2);
insert into rental (rental_date,inventory_id,customer_id,staff_id)
values (NOW(),2080,501,2);

select * from rental_log;

insert into rental (rental_date,inventory_id,customer_id,staff_id)
values (NOW(),2080,502,2);

select * from rental_log;