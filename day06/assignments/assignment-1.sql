Create table accounts (
	account_id serial primary key,
	account_name varchar(100),
	balance numeric
)

insert into accounts (account_name, balance) values ('Alice',4300)
insert into accounts (account_name, balance) values ('Bob',3700)


BEGIN;
DO $$
DECLARE current_balance numeric;
Begin
select balance INTO current_balance
FROM accounts where account_name = 'Alice';
IF current_balance >=4500 THEN
Update accounts SET balance = balance-4500 where account_name='Alice';
Update accounts SET balance = balance+4500 where account_name='Bob';
else
raise notice 'Insufficient Funds';
ROLLBACK;
END IF;
END
$$;
end;

select * from accounts;

-- Concurrency

/* 
Isolation Levels: 4
1. READ UNCOMMITED -> PSQL not supported
2. READ COMMITED -> Default
3. REPEATABLE READ -> ensures repeatable reads
4. SERIALIZABLE READ -> Full isolation (safest	but slow)
*/

/* Problems without concurrency control
1. Inconsistent Reads: Reading uncommited data from another transaction which might later disapper.
Transaction A updates a row but doesn't commit it
Transaction B reads that updated row
Transaction A rolls back the update


2. Lost Update
Transaction A reads a record
Transaction B reads the same record
Transaction A updates the record and writes it back
Transaction B (still holding the old value) writings it back, overwritting A's changes

Solution to Avoid lost update
1. Pessimistic Locking (Explicit Locks)
Lock the record when someone reads it, so no one can read or write until lock is released
DIsadv: reduce performance due to locking

2. Optimistic Locking (Versioning)
Common and scalable solution
Each record has timestamp or version number
When updating, you check that the version number hasn't changed since you last read it
If it is changed, you reject the update (must retry)

3. Serializable isolation level
Can cause performance issues

Which solution is best?
FOr webapps and APIs -> Optimistic locking is often the best balance (fast reads + safe writes)
FOr critical finance apps -> Pessimistic locking

Inconsistent Reads
- Dirty Reads
- Phantom Reads
- Non-repeatable read
*/

/*
12 May 2025: Transactions and Concurrency
1️⃣ Question:
In a transaction, if I perform multiple updates and an error happens in the third statement, but I have not used SAVEPOINT, what will happen if I issue a ROLLBACK?
Will my first two updates persist?

2️⃣ Question:
Suppose Transaction A updates Alice’s balance but does not commit. Can Transaction B read the new balance if the isolation level is set to READ COMMITTED?

3️⃣ Question:
What will happen if two concurrent transactions both execute:
UPDATE tbl_bank_accounts SET balance = balance - 100 WHERE account_name = 'Alice';
at the same time? Will one overwrite the other?

4️⃣ Question:
If I issue ROLLBACK TO SAVEPOINT after_alice;, will it only undo changes made after the savepoint or everything?

5️⃣ Question:
Which isolation level in PostgreSQL prevents phantom reads?

6️⃣ Question:
Can Postgres perform a dirty read (reading uncommitted data from another transaction)?

7️⃣ Question:
If autocommit is ON (default in Postgres), and I execute an UPDATE, is it safe to assume the change is immediately committed?

8️⃣ Question:
If I do this:

BEGIN;
UPDATE accounts SET balance = balance - 500 WHERE id = 1;
-- (No COMMIT yet)
And from another session, I run:

SELECT balance FROM accounts WHERE id = 1;
Will the second session see the deducted balance?
*/

-- Question 1
/* Since no savepoint is used, the transaction will completely rollback to the beginning state */

select * from accounts;

Begin Transaction;
update accounts
set balance = balance + 100 where account_id=1;

update accounts
set balance = balance - 100 where account_id=2;

update accounts
set balanc = 100 where account_id = 2; 

rollback;

select * from accounts;

-- Question 2
/* No, transaction B will no be able to read the updates made by the transaction A 
on Alice's balance */

-- Question 3
/* Yes. based on the order of execution of transaction, the transaction that completes first
will be overwritten by the other transaction */

-- Question 4
/* Only changes made after the savepoint will be rolled back. The changes made before the
savepoint will persisit. */

-- Question 5
/* The SERIALIZABLE isolation level prevents phatom reads. */

-- Question 6
/* No. By default, the isolation level is READ COMMITTED and further, the READ UNCOMMITTED
isolation level is not supported by PostgreSQL */

-- Question 7
/* Yes. If autocommit is ON, then all statements outside transaction blocks are automatically
commited after execution. */

-- Question 8
/* No, the second session will not see the deducted balance. Since the default isolation level
is READ COMMITTED, the second session will not be able to read the uncommited changes in first
session */