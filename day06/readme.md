# Day 06 - May 12, 2025
## Session Overview
- ACID properties
- Concurrency
- Problems with concurrency
- Concurrency Control
- Pessimistic Locking
- Optimistic Locking
- Multi-Version Concurrency Control
- Isolation Levels

## ACID Properties
[Reference](https://database.guide/what-is-acid-in-databases/)
- **Atomicity:** either all operations in a transaction succeeds or none does
- **Consistency:** all data will be valid according to all defined rules and constraints
- **Isolation:** multiple transactions can execute concurrently without interfering with each other
- **Durability:** Once committed changes are permanent and will survive any subsequent failure

## Concurrency
- Execution of multiple transactions at the same time without interfering with each other
- Improves performance

## Problems with concurrency
### Lost Update
- two transactions read the same data and update it, but the second transaction overwrites the changes made by the first one
- **Example**
```
T1 reads balance = 100
T2 reads balance = 100
T1 adds 50 to balance and writes -> 150
T2 is unaware of the changes made by T1
T2 adds 20 to the balance and writes -> 120
Final balance should be 170, but it's instead 120 (T1's update is lost)
```

### Dirty Read
- One transaction reads the changes made by another transaction that has not been committed yet. If the changes are rolled back, the data read by the transaction is invalid.
- **Example**
```
T1 reads balance = 100
T1 updates balance = 200 (not committed)
T2 reads balance = 200
T1 rolls back balance to 100
The value read by T2 is now invalid
```

### Non-repeatable Read
- A transaction reads the same data multiple times and gets different values because another transaction modified it in between
- **Example**
```
T1 reads balance = 100
T2 updated balance to 150 and commits
T1 reads balance again, but now balance = 150
```

### Phantom Read
- A transaction reads a set of rows matching a criteria, but another transaction inserts or deletes the rows that match the same criteria
- **Example**
```
T1 reads all the employees in AppDev department
T2 inserts an employee who is part of AppDev department
T1 runs the same query and sees an extra row - a phantom row
```

### Deadlock
- two or more transactions are waiting for each other to release resources and none of them can proceed
- **Example**
```
T1 locks row R1 and updates it
T2 locks row R2 and updates it
T1 now wants to update R2 and waits for T2 to complete and release the lock
T2 now wants to update R1 and waits for T1 to complete and release the lock
Both the transactions are waiting for each other to complete, causing a deadlock
```

## Concurrency Control
[Reference](https://www.red-gate.com/simple-talk/databases/postgresql/database-concurrency-in-postgresql/)
- techniques used to manage concurrent execution of transactions without interfering with each other
- Maintain data consistency, isolation and integrity

## Pessimistic Locking
- assumes conflicts between transactions are **likely** to occur
- transactions lock the data as soon as it accesses it
- other transactions are blocked until the lock is released
- provides exclusive access to data
- can lead to blocking and reduced concurrency

## Optimistic Locking
- assumes conflicts between transaction are **rare**
- transactions proceed without acquiring a lock on data
- Conflicts are checked and resolved only at the time of commit
- Each record has a version number or timestamp
- Conflicts are checked by comparing the version number at read and during write
- increased concurrency and scalable solution
- high abort rate due to conflicts at commit time

## Multi-Version Concurrency Control
- allows multiple copies of the data to exist simultaneously
- uses snapshots of data at different time to ensure consistency
- Readers always see a consistent view of data, even while others are writing
- Each data has a timestamp or version number
- Readers access the latest version that is valid when their transaction started
- Writers don't block readers, but work on their own copy of data instead of overwriting
- Writers can only commit their changes if the lastest version of data has a timestamp matching their copy (a newer version indicates conflict)
- Committing a write updates the data's timestamp.
- Old versions are eventually cleaned up by the garbage collector

## Isolation Levels
[Reference](https://antondevtips.com/blog/complete-guide-to-transaction-isolation-levels-in-sql)
- controls what data changes are visible for a transaction currently running
- **READ UNCOMMITTED:** Transactions can read changes made by other transactions, even if the changes are not committed yet. Not supported in PostgreSQL
- **READ COMMITTED:** Transactions can only read changes made by other transactions that are already committed before the start of the transaction
- **REPEATABLE READ:** If a transaction reads a row, other transactions cannot modify or delete that row until the first transaction completes
- **SERIALIZABLE READ:** Full isolation as if the transactions are executed serially

| Isolation Level  | Dirty Read         | Non-Repeatable Read | Phantom Read       |
|------------------|--------------------|---------------------|--------------------|
| READ UNCOMMITTED | :white_check_mark: | :white_check_mark:  | :white_check_mark: |
| READ COMMITTED   | :x:                | :white_check_mark:  | :white_check_mark: |
| REPEATABLE READ  | :x:                | :x:                 | :white_check_mark: |
| SERIALIZABLE     | :x:                | :x:                 | :x:                |

:white_check_mark: - possible anomaly

:x: - prevented