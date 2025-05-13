/* Concurrent update to same row */
-- Transaction A
Begin;
update accounts
set balance = 500
where account_id=1;

-- Transaction B
Begin;
update accounts
set balance = 600
where account_id=1;

/* Waiting for the query to complete */
-- TRANSACTION A
COMMIT;
-- Query returned successfully in 43 msec.

-- TRANSACTION B
-- Query returned successfully in 11 sec. 29 msec.

-- SELECT ... FOR UPDATE
-- TRANSACTION A
BEGIN;
SELECT * FROM accounts
WHERE account_id=1
FOR UPDATE;

-- TRANSACTION B
UPDATE accounts
SET balance = 500
WHERE account_id=1;
-- waiting for the query to complete

-- TRANSACTION A
COMMIT;
-- Query returned successfully in 48 msec.

-- TRANSACTION B
-- Query returned successfully in 11 sec. 21 msec.

/* Deadlock */
-- TRANSACTION A
BEGIN;
UPDATE accounts
SET balance = 500
WHERE account_id=1;

-- TRANSACTION B
BEGIN;
UPDATE accounts
SET balance = 600
WHERE account_id=2;

-- TRANSACTION A
UPDATE accounts
SET balance = 700
WHERE account_id=2;

-- TRANSACTION B
UPDATE accounts
SET balance = 800
WHERE account_id=1;

-- ERROR:  deadlock detected
-- Process 1172 waits for ShareLock on transaction 1068; blocked by process 4744.
-- Process 4744 waits for ShareLock on transaction 1069; blocked by process 1172. 

/* pg_locks */
Select * from pg_locks;