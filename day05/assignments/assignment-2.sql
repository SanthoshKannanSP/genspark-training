-- Cursor-Based Questions (5)

-- Write a cursor that loops through all films and prints titles longer than 120 minutes.
DO $$
DECLARE
	film_cursor cursor for
	select title,length from film
	where length > 120;
	film_record record;
BEGIN
	open film_cursor;
	LOOP
		FETCH NEXT FROM film_cursor INTO film_record;
		EXIT WHEN NOT FOUND;
		
		raise notice 'Film Name: %, Duration: %',film_record.title,film_record.length;
	END LOOP;
	close film_cursor;
END $$;

-- Create a cursor that iterates through all customers and counts how many rentals each made.
DO $$
declare
	customer_cursor cursor for
	select
		customer_id,
		concat(first_name,' ',last_name) as customer_name
	from customer
	order by 2;

	customer_record record;
	rental_count int;
begin
	open customer_cursor;
	loop
		fetch next from customer_cursor into customer_record;
		exit when not found;

		select count(*) into rental_count from rental
		where customer_id=customer_record.customer_id;

		raise notice 'Customer Name: %, Rental Count: %',customer_record.customer_name,rental_count;
	end loop;
	close customer_cursor;
end $$;
	
-- Using a cursor, update rental rates: Increase rental rate by $1 for films with less than 5 rentals.
do $$
declare
	film_cursor cursor for
	select film_id as rental_count from rental
	join inventory on inventory.inventory_id = rental.inventory_id
	group by film_id
	having count(*)<5;

	film_record int;
begin
	open film_cursor;
	loop
		fetch next from film_cursor into film_record;
		exit when not found;

		update film
		set rental_rate=rental_rate+1
		where film_id=film_record;

		raise notice 'Rental rate of Film with id % increased',film_record;
	end loop;
	close film_cursor;
end $$;

-- Create a function using a cursor that collects titles of all films from a particular category.
create or replace function fn_list_films_in_category(p_category_id int)
returns void
language plpgsql
as $$
declare
	film_cursor cursor for
	select title from film
	join film_category on film_category.film_id=film.film_id
	where category_id=p_category_id;

	v_title varchar(255);
begin
	open film_cursor;
	loop
		fetch next from film_cursor into v_title;
		exit when not found;
		raise notice 'Film Name: %',v_title;
	end loop;
	close film_cursor;
end $$;

select fn_list_films_in_category(6)
-- Loop through all stores and count how many distinct films are available in each store using a cursor.
do
$$
declare
	store_cursor cursor for
	select store.store_id, count(distinct film_id) as distinct_film_count from store
	join staff on staff.store_id=store.store_id
	join rental on rental.staff_id=staff.staff_id
	join inventory on inventory.inventory_id=rental.inventory_id
	group by store.store_id;

	store_record record;
begin
	open store_cursor;
	loop
		fetch next from store_cursor into store_record;
		exit when not found;

		raise notice 'Store Id: %, Distinct Films: %',store_record.store_id,store_record.distinct_film_count;
	end loop;
	close store_cursor;
end
$$;

-- Trigger-Based Questions (5)

-- Write a trigger that logs whenever a new customer is inserted.
create or replace function trigger_log_new_customer()
returns trigger
language plpgsql
as $$
begin
	raise notice 'New Customer % Inserted',concat(new.first_name,' ',new.last_name);
	return new;
end;
$$;

create trigger insert_new_customer
after insert
on customer
for each row
execute procedure trigger_log_new_customer()

insert into customer (store_id,first_name,last_name,email,address_id,activebool,create_date,last_update,active) values (1,'Jared','Ely','jared.ely@sakilacustomer.org',530,true,'2006-02-14','2013-05-26 14:49:45.738',1)

-- Create a trigger that prevents inserting a payment of amount 0.
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

insert into payment (customer_id,staff_id,rental_id,amount,payment_date) values (341,2,1520,7.99,'2007-02-15 22:25:46.996577')
insert into payment (customer_id,staff_id,rental_id,amount,payment_date) values (341,2,1520,0,'2007-02-15 22:25:46.996577')
select * from payment order by payment_id desc;

-- Set up a trigger to automatically set last_update on the film table before update.
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

select * from film order by film_id;
update film
set release_year=2024
where film_id=1;

-- Create a trigger to log changes in the inventory table (insert/delete).
create or replace function trigger_log_inventory_deletion()
returns trigger
language plpgsql
as $$
begin
	raise notice 'Record with Inventory ID % deleted',old.inventory_id;
	return new;
end;
$$;

create trigger delete_inventory
after delete
on inventory
for each row
execute procedure trigger_log_inventory_deletion()

insert into inventory (inventory_id,film_id,store_id,last_update) values (4582,1000,2,'2006-02-15 10:09:17');
select * from inventory order by inventory_id desc;
delete from inventory where inventory_id=4582;

-- Write a trigger that ensures a rental can’t be made for a customer who owes more than $5.
create or replace function trigger_check_due_before_rental()
returns trigger
language plpgsql
as $$
declare
	due_amount numeric;
begin
	select SUM(amount) into due_amount from payment
	join rental on rental.rental_id=payment.rental_id
	where rental.return_date is null
	and rental.customer_id=new.customer_id;

	if (due_amount > 5)
	then raise exception 'Customer already owes more than $5';
	end if;
	return new;
end;
$$;

create trigger new_rental
before insert
on rental
for each row
execute procedure trigger_check_due_before_rental()

insert into rental (rental_date,inventory_id,customer_id,return_date,staff_id,last_update)
values (NOW(),1525,267,NULL,2,NOW())

-- Transaction-Based Questions (5)
-- Write a transaction that inserts a customer and an initial rental in one atomic operation.
begin transaction;
insert into customer (customer_id,store_id,first_name,last_name,email,address_id,activebool,create_date,last_update,active)
values (2000,1,'Tom','Richard','tom.richard@sakilacustomer.org',530,true,NOW(),NOW(),1);
insert into rental (rental_date,inventory_id,customer_id,return_date,staff_id,last_update)
values (now(),1525,2000,NULL,2,NOW());
commit transaction;
select * from rental order by rental_id desc;

-- Simulate a failure in a multi-step transaction (update film + insert into inventory) and roll back.
begin transaction;
update film
set release_year=2021
where film_id=1;
insert into inventory (film_id,store_id,last_update)
values (1,2,now());
rollback transaction;

-- Create a transaction that transfers an inventory item from one store to another.
begin transaction;
update inventory
set store_id=1
where inventory_id=4581;
commit transaction;
select * from inventory order by inventory_id desc;

-- Demonstrate SAVEPOINT and ROLLBACK TO SAVEPOINT by updating payment amounts, then undoing one.
begin transaction;
update inventory
set store_id=2
where inventory_id=4581;
savepoint new_savepoint;
update film
set release_year=2021
where film_id=1;
rollback to new_savepoint;
commit transaction;

select * from inventory order by inventory_id desc;
select * from film order by film_id;

-- Write a transaction that deletes a customer and all associated rentals and payments, ensuring atomicity.
begin transaction;
delete from payment
where customer_id=599;
delete from rental
where customer_id=599;
delete from customer
where customer_id=599;
commit transaction;

select * from customer order by customer_id desc;