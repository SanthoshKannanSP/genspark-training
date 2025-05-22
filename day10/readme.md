# Day 10 - May 16th, 2025
## Session Overview
- General DBMS tips
- Task covering all learnt topics

## General DBMS tips
- **Optimization:** Consider the trade-off between resource usage and performance
- **Security First:** Give extra importance to the security of the database and data stored
- **Normalization:** Too much normalization may reduce performace (due to requiring many joins)
- **Indexing:** Index columns used by `WHERE`, `JOIN` and `ORDER BY` clauses to improve read performance. But each index used will slow down write performance (inserts/updates)
- **Replication** - Use replication and automatic failover for high availability