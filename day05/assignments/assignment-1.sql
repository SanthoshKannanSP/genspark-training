-- SELECT Queries
-- List all films with their length and rental rate, sorted by length descending.
-- Columns: title, length, rental_rate
select title,length,rental_rate from film
order by length desc;

-- Find the top 5 customers who have rented the most films.
-- Hint: Use the rental and customer tables.
select concat(first_name,' ',last_name), COUNT(*) from rental
join customer on customer.customer_id=rental.customer_id
group by 1
order by 2 desc
limit 5;

-- Display all films that have never been rented.
-- Hint: Use LEFT JOIN between film and inventory → rental.
select title from film
where film_id not in (
select distinct film_id from inventory
join rental on rental.inventory_id=inventory.inventory_id
)

-- JOIN Queries
-- List all actors who appeared in the film ‘Academy Dinosaur’.
-- Tables: film, film_actor, actor
select concat(first_name,' ',last_name) as actor_name from film_actor
join film on film.film_id=film_actor.film_id
join actor on actor.actor_id=film_actor.actor_id
where title = 'Academy Dinosaur';

-- List each customer along with the total number of rentals they made and the total amount paid.
-- Tables: customer, rental, payment
select concat(first_name,' ',last_name),Count(*), SUM(amount) from customer
join rental on rental.customer_id=customer.customer_id
join payment on payment.rental_id=rental.rental_id
group by 1
order by 2 desc

-- CTE-Based Queries
-- Using a CTE, show the top 3 rented movies by number of rentals.
-- Columns: title, rental_count
with cte_number_of_rental
as
(
select title, COUNT(*) as total_rental from film
join inventory on inventory.film_id=film.film_id
join rental on rental.inventory_id=inventory.inventory_id
group by title
)
select title,total_rental from cte_number_of_rental
order by total_rental desc
limit 3;

-- Find customers who have rented more than the average number of films.
-- Use a CTE to compute the average rentals per customer, then filter.
with cte_total_rental_of_customer as
(
select concat(first_name,' ',last_name),COUNT(*) as total_rental from customer
join rental on rental.customer_id=customer.customer_id
group by 1
order by 2 desc
)
select * from cte_total_rental_of_customer
where total_rental>(select AVG(total_rental) from cte_total_rental_of_customer)

--  Function Questions
-- Write a function that returns the total number of rentals for a given customer ID.
-- Function: get_total_rentals(customer_id INT)
create or replace function fn_get_total_rentals(p_customer_id INT)
returns int
language plpgsql
as
$$
declare
	rental_count INT;
begin
	select COUNT(*) into rental_count
	from rental
	where customer_id=p_customer_id;

	return rental_count;
end;
$$;

select fn_get_total_rentals(1);

-- Stored Procedure Questions
-- Write a stored procedure that updates the rental rate of a film by film ID and new rate.
-- Procedure: update_rental_rate(film_id INT, new_rate NUMERIC)
create or replace procedure sp_update_rental_rate(p_film_id INT, p_new_rental_rate NUMERIC)
language plpgsql
as
$$
begin
	update film
	set rental_rate=p_new_rental_rate
	where film_id=p_film_id;
end;
$$;
select * from film where film_id=133;
call sp_update_rental_rate(133,5.99)

-- Write a procedure to list overdue rentals (return date is NULL and rental date older than 7 days).
-- Procedure: get_overdue_rentals() that selects relevant columns.
create or replace procedure sp_get_overdue_rentals()
language plpgsql
as
$$
begin
	select * from rental
	where return_date is NULL
	and rental_date <= NOW() - '7 days'::INTERVAL;
end;
$$;

call sp_get_overdue_rentals();