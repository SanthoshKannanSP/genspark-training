# Day 03 - May 7th, 2025

## Session Overview
- Joins
- Stored Procedures
- JSON Data

## Joins
[Reference](https://www.sqlservertutorial.net/sql-server-basics/sql-server-joins/)

Combine rows of two or more tables based on a related column.
```
select * from table1
<join type> table2 on <join condition>;
```

### Inner Join
Only returns rows that have matching values in both the tables. The default join type is inner join. The order of the tables doesn't matter.
```
select title, pub_name from titles 
<inner> join publishers on titles.pub_id = publishers.pub_id;
```

### Left Outer Join
Returns all the rows from the left table and the matching rows from the right table. If there are no matching rows in the right table, then the columns of the right table will have *NULL* values. The **OUTER** keyword is optional.
```
select title, pub_name from titles 
left <outer> join publishers on titles.pub_id = publishers.pub_id;
```

### Right Outer Join
Returns all the rows from the right table and the matching rows from the left table. If there are no matching rows in the left table, then the columns of the left table will have *NULL* values. The **OUTER** keyword is optional.
```
select title, pub_name from titles 
right <outer> join publishers on titles.pub_id = publishers.pub_id;
```

### Full Outer Join
Returns all the rows from both the tables, even if there is no match. If there is no match, then the respective table columns will have *NULL* values. The **OUTER** keyword is optional. The order of the tables doesn't matter.
```
select title, pub_name from titles 
full <outer> join publishers on titles.pub_id = publishers.pub_id;
```

### Cross Join
[Reference](https://www.sqlservertutorial.net/sql-server-basics/sql-server-cross-join/)

Returns all the rows of the left table combined with every row in the right table (Cartesian Product). Doesn't require a join condition.
```
select title, pub_name from titles 
cross join publishers;
```

### Consecutive joins
More than two tables can be combined by using consecutive join clauses.
```
select concat(au_fname, au_lname), title from authors
join titleauthor on authors.au_id=titleauthor.au_id
join titles on titleauthor.title_id=titles.title_id;
```

## Stored Procedure
- Group one or more SQL statements into a named logical unit
```
CREATE PROCEDURE <ProcedureName>(
   @<ParameterName1> <data type>,
   @<ParameterName2> <data type>
)
AS
BEGIN
   <SQL Statements>
END;
```
- The created stored procedure can be executed by using the `EXEC` keyword
```
EXEC <ProcedureName> <ParameterValue1>, <ParameterValue2>;
```
- **Improved Performance:** When a stored procedure is executed for the first time, the execution plan created is cached. Subsequent executions reuse the cached plan, hence taking less time to process.
- **Greater Security** - Prevents SQL injection by avoiding dynamic SQL
- Encapsulation and hiding of business logic
- reduces complexity for the programmer
- Can encrypting stored procedure for safety and high confidentiality

## Parameter of stored procedure
**Input Parameter**

[Reference](https://www.sqlservertutorial.net/sql-server-stored-procedures/sql-server-stored-procedure-parameters/)
- Default type of parameter
- Used to pass values into the procedure
```
CREATE PROCEDURE proc_GetTitlesByAuthor(@au_id INT)
AS
BEGIN
	SELECT title from titles where au_id = @au_id;
END;

EXEC proc_GetTitlesByAuthor 10;
```

**Output parameter**

[Reference](https://www.sqlservertutorial.net/sql-server-stored-procedures/stored-procedure-output-parameters/)
- Used to return value out from the procedure
- The **OUTPUT** keyword is used to declare output parameters
```
CREATE PROCEDURE proc_GetTitleCountByAuthor(
	@au_id INT,
	@title_count INT OUTPUT
)
AS
BEGIN
	SELECT COUNT(*) INTO @title_count FROM titles
	WHERE au_id = @au_id;
END;

DECLARE @count INT;
EXEC proc_GetTitlesByAuthor 10, @count OUTPUT;
```

## JSON Data
[Reference](https://learn.microsoft.com/en-us/sql/relational-databases/json/json-data-sql-server?view=sql-server-ver16)

JSON data is stored in the table as text. Built-in functions can be used to query, validate and manipulate stored JSON data.
```
CREATE TABLE Products(
	id INT IDENTITY,
	name NVARCHAR(20) NOT NULL,
	details NVARCHAR(MAX) NOT NULL
);

INSERT INTO Products(name, details) VALUES
('Laptop','{"brand":"Dell","spec":{"ram":"16GB","cpu":"15"}}');
```

### ISJSON() FUNCTION
Used to check if a string is a valid JSON
```
SELECT * FROM Products
WHERE ISJSON(details)=1;
```

### JSON_VALUE() FUNCTION
Used to extract a scalar value from a JSON string
```
select name, JSON_VALUE(details, '$.brand') Brand_Name from Products;
```

### JSON_QUERY() FUNCTION
Used to extract an object or an array from a JSON string
```
select name, JSON_QUERY(details,'$.spec') from Products;
```

### JSON_MODIFY() FUNCTION
Used to update the value of a property in a JSON string and returns the modified JSON string.
```
update products set details = JSON_MODIFY(details, '$.spec.ram','8GB') where id = 1;
```