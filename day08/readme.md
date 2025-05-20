# Day 08 - May 14th, 2025
## Session Overview
- Transaction best practices
- Availability
- Causes of non-availability
- Replication
- Master-Slave Architecture
- Replication best practices

## Transaction best practices
- Keep transactions short - longer transactions hold locks longer, blocking other queries
- Always have exception handling
- Monitor and log failed transactions - for debugging and audit purposes
- Use the appropriate isolation level
- Have cursors and transactions inside stored procedures
- Seperate the procedure's block and the transactional block inside it - to control exception handling and rollback behavior
```
CREATE OR REPLACE PROCEDURE <procedure_name>(<parameter_list>)
LANGUAGE plpgsql
AS $$
BEGIN
    BEGIN
        .....
    EXCEPTION
        .....
    END;
END;
```

## Availability
- Ability of the database to remain accessible and operational, even when experiencing failures
- **High availability** - minimizing the downtime
- **Failover Time** - time it takes to recover from failure
- **Recovery Time Objective** - how quickly a service must be restored
- **Recovery Point Objective** - how much data you can afford to lose

## Causes of non-availability
- Hardware Failure
- Software Crashes
- Maintenance
- Network Failure

## Replication
[Reference](https://www.postgresql.org/docs/current/runtime-config-replication.html)
- **Replication** or **Redundancy** - duplicating data from one database server to one or more other servers
- Same data exists in multiple locations
- Improves availability, scalability and recovery
- Can lead to lag, inconsistency and high maintenance and cost

### Physical Replication
- Replicating disk-level changes (like Write Ahead Log (WAL) files)
- Primary server writes all changes to WAL files before applying them to actual data files
- Replicas connect to the primary server and stream the WAL files and apply the changes as they receive them
- Duplicates the entire database cluster
- The replicas are read-only, changes can be written only in the primary server
- High performance and consistency
- Not flexible, replicas should have same DB version and architecture as primary server.

### Logical Replication
- Replicating data-level changes (`UPDATE`, `INSERT`, `DELETE`)
- The publisher (source) sends SQL level changes to one or more subscribers (targets)
- The subscribers receive the SQL-level changes and applies them to their local copy of the table
- Duplicates only a few tables
- Can work in a multi-master system (replicas can also be written to)
- Supports cross-version systems and is flexible
- No support for DDL changes and `TRUNCATE` statements and there can be replication lag under high load

## Master-Slave Architecture
- Master - primary server
- Slave - replica server
- Primary server handles all write operations
- Replica servers handle read operations and copy all the changes from the master
- **Promotion:** When the master server fails, a replica server will be promoted to be the master. Other replicas reconfigure to replicate from the new master
- Suitable for read-heavy applications
- High availability and read scalability

## Replication best practices
- Use asynchronous replication to achieve better performance with non-critical data
- Synchronous replication is strongly recommended but is slower
- Use logical replication when you need flexibility and physical replication for performance or complete mirroring
- Regularly monitor how far replicas are behind the master. Set up alerts for high lag.
- Always have automatic failover enabled
- Use connection pooling to balance read traffic across replicas
- Prefer archiving over deletion
- read-only replication is prefered