-- Locking mechanisms
-- Preventing multiple users from reading or writing the data at the same time
-- Prevents inconsistencies, lost updates and read anomalies

-- Types of Locks
-- Row-level locks
-- table-level locks
-- advisory lock

/* Row-level locks / implicit locks
- Locks a specific row during update or delete
- blocks other writers but not readers
*/

/* Table-level locks / explicit locks
- Locks the whole table
*/

/* Advisory Lock
User-defined lock (custom)
*/

/* MVCC vs Locks
MVCC allows readers and writers to work together without blocking
Locks are needed when multiple writers try to touch the same row or table
*/

/* Simple rules of lock
- Readers dont's block each other
- Writers block other writers on the same row
*/

/* Deadlock
Two or more transactions waiting on each other to release their lock for execution
*/

/*
Table-level lock / explicit table lock
Access share
row share
exclusive
access exclusive
*/

Begin;
lock table accounts
in access share mode;
-- allows other selects, even INSERT/DELETE at the same time

Begin;
lock table accounts
in row share mode;
-- SELECT ... FOR update -> lock the selected row for later update
-- SELECT ... FOR update, reads are allowed, conflicting row locks are blocked, writes allowed

Begin;
lock table accounts
in exclusive mode;
-- Blocks writes(insert, update,delete) but allows reads (selects)

Begin;
lock table accounts
in access exclusive mode;
-- blocks all reads (selects) and writes (insert,update,delete)
-- used by alter table, drop table, truncate

-- Explicit row locks -> SELECT .. FOR UPDATE
-- A
select * from accounts
where id=1
for update;
-- row-id is locked

-- B
update accounts
set balance = 199
where id=1;
-- B is blocked till A finishes

-- SELECT ... FOR UPDATE -> Locks the row early so no one can change it midway
-- Commonly used in banking applications, Ticket booking and inventory management systems

/*
PSQL automatically detects deadlocks and automatically aborts a transaction to resolve deadlocks
ERROR: deadlock detected
*/

-- Advisory Lock
-- select a lock with id 12345
SELECT pg_advisory_lock(12345);
-- critical ops
select pg_advisory_unlock(12345);
