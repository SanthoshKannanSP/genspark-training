# Day 04 - May 8, 2025
## Session Overview
- Bulk insert
- Common Table Expression
- OFFSET and FETCH clauses
- Functions

## Bulk insert
[Reference](https://learn.microsoft.com/en-us/sql/t-sql/statements/bulk-insert-transact-sql?view=sql-server-ver16)

Import external data file into a database or view
```
BULK INSERT <table_name>
FROM '<filepath>'
WITH (<options>);
```

**Bulk Insert CSV file**

[Reference](https://www.sqlservertutorial.net/sql-server-administration/sql-server-bulk-insert/)
```
BULK INSERT table1
FROM 'data.csv'
WITH (
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '\n'
);
```

## Common Table Expressions (CTEs)
[Reference](https://www.sqlservertutorial.net/sql-server-basics/sql-server-cte/)
- A temporary named result set
- available in current execution scope of a statement
```
WITH expression_name (column_names)
AS
    (CTE_definition)
SQL_statement;
```
- can have multiple CTEs in one query
- **Nested CTEs:** one CTE references another CTE
- Improve query readability and breaks computation into parts
```
WITH cte_sales AS (
    SELECT 
        staff_id, 
        COUNT(*) order_count  
    FROM
        sales.orders
    WHERE 
        YEAR(order_date) = 2018
    GROUP BY
        staff_id
)
SELECT
    AVG(order_count) average_orders_by_staff
FROM 
    cte_sales;
```

### Recursive CTE
[Reference](https://www.sqlservertutorial.net/sql-server-basics/sql-server-recursive-cte/)
- A CTE that references itself
- Useful for querying hierarchical data
```
WITH expression_name
AS
(
    initial_query  
    UNION ALL
    recursive_query  
)

SELECT *
FROM expression_name
```
- **Example:**
```
with Hierarchy
as (
select EmployeeID,concat(firstname,' ',lastname) as EmployeeName,ReportsTo from Employees where ReportsTo IS NULL
UNION ALL
select Employees.EmployeeID,concat(Employees.firstname,' ',Employees.lastname) as EmployeeName,Employees.ReportsTo from Employees inner join Hierarchy on Employees.ReportsTo=Hierarchy.EmployeeID
)
select EmployeeName,concat(firstname,' ',lastname) as ManagerName from Hierarchy
left outer join Employees on Employees.EmployeeID=Hierarchy.ReportsTo;
```

## OFFSET and FETCH clauses
[Reference](https://www.sqlservertutorial.net/sql-server-basics/sql-server-offset-fetch/)
- options of `ORDER BY` clause
- used to limit the number of rows returned by the query
```
ORDER BY column_list [ASC |DESC]
OFFSET offset_row_count {ROW | ROWS}
FETCH {FIRST | NEXT} fetch_row_count {ROW | ROWS} ONLY
```
- **OFFSET:** specifies the number of rows to skip
- **FETCH:** specifies the number of rows to return
- `OFFSET` clause is mandatory, while `FETCH` clause is optional.
- `FIRST` and `NEXT` are synonyms and can be used interchangably
- `ROW` and `ROWS` can be used interchangably


## Functions
- named block of business logic that accepts parameters and returns a result
- mandatorily returns a value (atleast `void`)
- code reusability
```
CREATE FUNCTION function_name (parameter_list)
RETURNS data_type AS
BEGIN
    statements
    RETURN value
END
```

### Scalar function
[Reference](https://www.sqlservertutorial.net/sql-server-user-defined-functions/sql-server-scalar-functions/)
- returns a single value
```
CREATE FUNCTION func_NetSale(
    @quantity INT,
    @list_price INT
)
RETURNS INT
AS 
BEGIN
    RETURN @quantity * @list_price;
END;
```
- Can be called using a `SELECT` statement
```
SELECT func_NetSale(quantity,list_price) sales FROM Orders;
```

### Table-valued Functions
[Reference](https://www.sqlservertutorial.net/sql-server-user-defined-functions/sql-server-table-valued-functions/)
- returns a data of type table
```
CREATE FUNCTION func_ProductInYear (
    @model_year INT
)
RETURNS TABLE
AS
RETURN
    SELECT 
        product_name,
        model_year,
        list_price
    FROM
        production.products
    WHERE
        model_year = @model_year;
```
- Can be called in the `FROM` clause of `SELECT` statement
```
SELECT * from func_ProductInYear(2018);
```