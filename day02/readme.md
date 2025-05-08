# Day 02 - May 6th, 2025
## Session Overview
- Assignment on SELECT statements with Pubs DB
- Assignment on DDL statements
- Normalization
- Assignment on DML statements
- Referential Integrity

## Pubs Sample Data
[Link](https://raw.githubusercontent.com/microsoft/sql-server-samples/refs/heads/master/samples/databases/northwind-pubs/instnwnd.sql)

## SOUNDEX() and DIFFERENCE() functions
[Reference](https://www.sqlshack.com/an-overview-of-difference-and-soundex-sql-functions/)
**SOUNDEX**
- Soundex is a phonetic algorithm used to encode words as they are pronounced in English such that similar sounding words (homophones) have the same representation. 
- SOUNDEX() function takes a string value as input and returns a four-character string.

**Example:**
- SOUNDEX("H") = "H000"
- SOUNDEX("Had") = "H300"
- SOUNDEX("Hadi") = "H300"

**DIFFERENCE**
- DIFFERENCE() is a scalar function that computes the similarity between two strings using SOUNDEX()
- DIFFERENCE() functions takes two strings as input and outputs an integer value between 0 and 4.
- When the value is closer to 4, it means the strings are very similar.
 
**Example**
- DIFFERENCE('MySQL','MSSQL') = 4
- DIFFERENCE('MySQL','PostgreSQL') = 1

## Normalization
[Reference](https://www.freecodecamp.org/news/database-normalization-1nf-2nf-3nf-table-examples/)
- database design principle for organizing data
- avoids redundancy and maintains integrity of data
- avoids undesirable anomalies associated with insertion, updation and deletion

## First Normal Form (1NF)
- No multivalued attributes (EG: multiple phone numbers or email ids)
- There must be a primary key to uniquely identify a row
- No duplicate rows or columns
- All the values in a column should be of the same type

## Second Normal Form (2NF)
- Already in 1NF
- has no partial dependency - non-key attributes should depend fully on the primary key and not a part of it

## Third Normal Form (3NF)
- Already in 2NF
- had no transitive dependency - non-key attributes should not depend on another non-key attribute that is fully dependent on the primary key

## Types of Attributes
- **Simple:** cannot be divided into more attributes (*EG:* Age)
- **Composite:** made of two or more simple attributes (*EG:* Address is made of Street, City and State)
- **Single-Valued:** Only has a single value (*EG:* A person can have only one Age)
- **Multi-Valued:** Can have multiple valid values (*EG:* A person can have multiple phone numbers)
- **Complex:** When composite and multi-valued attributes combine together an attribute (*EG:* A person can have multiple delivery addresses) 

## Types of keys
- **Primary Key:** a column that can uniquely identify every row in a table
- **Foreign Key:** a column that forms a relationship between two tables
- **Composite Key** two or more columns that together can uniquely identify every row in a table, but individually may not be able to.

## Referential integrity
[Reference](https://database.guide/what-is-referential-integrity/)
- whenever a foreign key value is used, it must reference a valid, existing primary key in the parent table.
- Cannot add rows to a related table if there is no associated row in the primary table.
- Cannot change values in a primary table that result in orphaned records in a related table.
- Cannot delete rows from a primary table if there are matching related rows