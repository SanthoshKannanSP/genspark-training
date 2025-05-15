# Day 07 - May 13th, 2025
## Session Overview
- Explicit Locking
- Types of Locks
- Row-level Locks
- Table-level Locks
- Advisory Locks
- Arguments to Trigger Function

## Explicit Locking
[Reference](https://www.postgresql.org/docs/current/explicit-locking.html)
- Manually control concurrency access to data in tables, rather than relying only on automatic locks that PostgreSQL applies
- Gives precise control over when and how data is locked
- Enforce custom concurrency control logic

## Types of Locks
- Row-Level Locks
- Table-Level Locks
- Advisory Locks

## Row-Level Locks
- Locks applied to individual rows in a table
- Prevents two transactions from modifying the same row at the same time
- Doesn't block readers. Only blocks writers and lockers to the same row
- `SELECT ... FOR UPDATE` - Locks rows for update or delete by current transaction only. Other transactions cannot modify or lock these rows until the current transaction ends.
- `SELECT ... FOR NO KEY UPDATE` - Similar to `FOR UPDATE` but weaker. Prevents other transactions that would change the row's primary key or any foreign key references, but allows non-key updates.
- `SELECT ... FOR SHARE` - Acquires a shared lock. Allows other transactions that also take a `FOR SHARE` or `FOR KEY SHARE` lock on the same rows. Blocks transactions that try to update or delete the rows.
- `SELECT ... FOR KEY SHARE` - The weakest lock. Also acquires a shared lock. Prevents only updates that would affect the row's primary key or foreign key references.

## Table-Level Locks
- Locks applied to the entire tables
- Prevents two transactions from modifying the same table at the same time
```
BEGIN;
LOCK TABLE <table_name> IN <lock_mode> MODE;
-- SQL statements
COMMIT;
```
- `ACCESS SHARE` - Acquired by ordinary `SELECT` queries. Doesn't block other reads or writes
- `ROW SHARE` - Acquired by commands that intend to modify rows in a table (`FOR UPDATE`, `FOR NO KEY UPDATE`,`FOR SHARE`,`FOR KEY SHARE`)
- `ROW EXCLUSIVE` - Acquired by commands that actually modify rows in a table (`INSERT`,`UPDATE`,`DELETE`)
- `SHARE UPDATE EXCLUSIVE` - Acquired by commands that perform schema changes and `VACUUM` runs
- `SHARE` - Acquired by `CREATE INDEX`(without `CONCURRENTLY`). Prevents writes, but allows reads and other share locks
- `SHARE ROW EXCLUSIVE` - Similar to `SHARE`, but only one transaction can hold it at a time. Acquired by `CREATE TRIGGER` and some forms of `ALTER TABLE`
- `EXCLUSIVE` - Only allows concurrent `ACCESS SHARE` locks (other transactions could only read). Acquired by `REFRESH MATERIALIZED VIEW CONCURRENTLY`
- `ACCESS EXCLUSIVE` - Prevents all other types of locks. Only one transaction can hold it at a time. Guarentees that the holder is the only transaction accessing the table. Default lock mode for `LOCK TABLE` statements that do not specify a type explicitly. Only lock that could block reads.

## Advisory Locks
- Application-level locks managed manually by the user
- Allow custom, user-defined control over access to shared resources
- Faster, avoid table bloat and are automatically cleaned up at the end of the session
- Can be session-level (held until connection closes) or transaction-level (released at end of transaction)
- Not tied to tables or rows but on user-defined unique numbers of IDs
- To acquire and release session-level locks
```
SELECT pg_advisory_lock(key);
SELECT pg_advisory_unlock(key);
```
- To check if session-level lock can be acquired (returns boolean)
```
SELECT pg_try_advisory_lock(key);
```
- To release all held session-level locks
```
SELECT pg_advisory_unlock_all();
```
- To acquire transaction-level locks (no manual unlock needed - PostgreSQL handles it at commit/rollback)
```
SELECT pg_advisory_xact_lock(key);
```
- To view all currently held advisory locks
```
SELECT * FROM pg_locks WHERE locktype = 'advisory';
```

## Arguments to Trigger Function
[Reference](https://www.postgresql.org/docs/current/plpgsql-trigger.html)
- The trigger function must be declared with no arguments
- But it can receive arguments through special variables
- **TG_NAME:** Name of the trigger than fired the function
- **TG_TABLE_NAME:** Name of the table the trigger is on
- **TG_TABLE_SCHEMA:** Schema of the table
- **TG_OP:** The operation that fired the trigger
- **TG_WHEN:** When the trigger was fired relative to the operation
- **TG_NARGS:** number of arguments passed to the trigger function in the `CREATE TRIGGER` statement
- **TG_ARGV:** array of arguments passed to the trigger function in the `CREATE TRIGGER` statement