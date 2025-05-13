# Day 05 - May 9th, 2025
## Session Overview
- Installing PostgreSQL
- Cursors
- Triggers
- Transactions
- Savepoint

## Installing PostgreSQL
[Link](https://www.postgresql.org/download/windows/)

## Cursors
[Reference](https://postgresql-tutorial.com/pl-pgsql-cursors/)
- Used to retrieve a result set, one at a time or in small chucks
- Useful for large datasets - without loading everything into memory
```
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
```

## Triggers
[Reference](https://neon.tech/postgresql/postgresql-triggers/introduction-postgresql-trigger)
- A function that is executed automatically whenever an associated event occurs.
- First a user-defined trigger function is created and then it is binded to an event happening on a table
- **Trigger Timings:** `BEFORE`, `AFTER`, `INSTEAD OF`
- **Trigger Level:** `FOR EACH ROW` (runs once for each row), `FOR EACH STATEMENT` (runs once per operation, irrespective of number of rows)
- **Trigger Function**
```
CREATE FUNCTION trigger_function()
RETURNS TRIGGER
LANGUAGE PLPGSQL
AS $$
BEGIN
   -- trigger logic
END;
$$
```
- **CREATE TRIGGER**
```
CREATE TRIGGER trigger_name
{BEFORE | AFTER} { event }
ON table_name
[FOR [EACH] { ROW | STATEMENT }]
EXECUTE PROCEDURE trigger_function
```
- **NEW:** Trigger variable that holds the new row (for `INSERT` or `UPDATE`)
- **OLD:** Trigger variable that holds the old row (for `UPDATE` or `DELETE`)

## Transactions
[Reference](https://neon.tech/postgresql/postgresql-tutorial/postgresql-transaction)

- A single unit of work
- either all operation succeed or none of them do
```
BEGIN TRANSACTION;
-- SQL statements
{COMMIT | ROLLBACK}; 
```
- **COMMIT:** Ends the transaction and applies the changes
- **ROLLBACK:** Cancels the transaction and reverts the changes
- In PostgreSQL, each statement is its own transaction by default. Hence any statement executed is auto-committed unless inside a `BEGIN` block.

## Savepoints
[Reference](https://postgresql-tutorial.com/postgresql-savepoint/)
- A named point within a transaction to rollback to
- Any changes made before the savepoint is kept intact while changes after the savepoint are reverted
- Useful for handling errors and exceptions within a transaction
```
BEGIN TRANSACTION;
-- SQL statements 1
SAVEPOINT <savepoint_name>
-- SQL statements 2
{ROLLBACK TO | RELEASE} <savepoint_name>;
COMMIT;
```
- **ROLLBACK TO:** Revert any changes made after the specified savepoint
- **RELEASE:** Removes the specified savepoint