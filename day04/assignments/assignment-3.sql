--1) List all orders with the customer name and the employee who handled the order.
--(Join Orders, Customers, and Employees)
select OrderID, CompanyName, concat(FirstName,' ',LastName) EmployeeName from orders
join customers on orders.CustomerID=customers.CustomerID
join employees on orders.EmployeeID=employees.EmployeeID
order by OrderDate;

--2) Get a list of products along with their category and supplier name.
--(Join Products, Categories, and Suppliers)
select ProductName,CategoryName,CompanyName from products
join Categories on Categories.CategoryID=Products.CategoryID
join Suppliers on Suppliers.SupplierID=Products.SupplierID
order by ProductName;

--3) Show all orders and the products included in each order with quantity and unit price.
--(Join Orders, Order Details, Products)
select Orders.OrderID, Orders.CustomerID, Products.ProductName, [Order Details].Quantity, [Order Details].UnitPrice from Orders
join [Order Details] on [Order Details].OrderID=Orders.OrderID
join Products on Products.ProductID=[Order Details].ProductID
order by OrderID;

--4) List employees who report to other employees (manager-subordinate relationship).
--(Self join on Employees)
select concat(sub.FirstName,' ',sub.LastName) Employee, concat(man.FirstName,' ',man.LastName) Manager from Employees as sub
join Employees as man on man.EmployeeID=sub.ReportsTo
order by Employee;

--5) Display each customer and their total order count.
--(Join Customers and Orders, then GROUP BY)
select CompanyName,COUNT(*) from Customers
join Orders on orders.CustomerID=Customers.CustomerID
group by CompanyName
order by CompanyName;

--6) Find the average unit price of products per category.
--Use AVG() with GROUP BY
select CategoryName, AVG(UnitPrice) as AverageUnitPrice from Products
join Categories on Categories.CategoryID=Products.CategoryID
group by CategoryName
order by CategoryName;

--7) List customers where the contact title starts with 'Owner'.
--Use LIKE or LEFT(ContactTitle, 5)
select * from Customers where ContactTitle Like 'Owner%';

--8) Show the top 5 most expensive products.
--Use ORDER BY UnitPrice DESC and TOP 5
select TOP 5 ProductName,UnitPrice from Products
order by UnitPrice desc;

--9) Return the total sales amount (quantity × unit price) per order.
--Use SUM(OrderDetails.Quantity * OrderDetails.UnitPrice) and GROUP BY
select OrderID, SUM(Quantity*UnitPrice) from [Order Details] as TotalSales
group by OrderID;

--10) Create a stored procedure that returns all orders for a given customer ID.
--Input: @CustomerID
create or alter proc sp_AllOrders(@CustomerID nvarchar(10))
as
begin
	select * from orders
	where CustomerID=@CustomerID;
end

exec sp_AllOrders 'VINET';

--11) Write a stored procedure that inserts a new product.
--Inputs: ProductName, SupplierID, CategoryID, UnitPrice, etc.
create or alter proc sp_InsertProduct(@ProductName nvarchar(40), @SupplierID int, @CategoryID int, @QuantityPerUnit nvarchar(20), @UnitPrice money)
as
begin
	insert into Products (ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued)
	values (@ProductName,@SupplierID,@CategoryID,@QuantityPerUnit,@UnitPrice,0,0,0,0)
end

exec sp_InsertProduct 'Tea',1,1,'10 boxes x 10 bags',17.00
select * from Products
--12) Create a stored procedure that returns total sales per employee.
--Join Orders, Order Details, and Employees
create or alter proc sp_TotalSalePerEmployee
as
begin
	select concat(FirstName,' ',LastName) Employee, SUM(Quantity*UnitPrice) TotalSales from orders
	join [Order Details] on orders.OrderID=[Order Details].OrderID
	join Employees on Employees.EmployeeID=orders.EmployeeID
	group by concat(FirstName,' ',LastName);
end

exec sp_TotalSalePerEmployee;
--13) Use a CTE to rank products by unit price within each category.
--Use ROW_NUMBER() or RANK() with PARTITION BY CategoryID
with CategoryPrices
as (select CategoryName, ProductName, UnitPrice from Categories join Products on Products.CategoryID=Categories.CategoryID)
select CategoryPrices.*, RANK() over (Partition by categoryName order by unitprice) as rank from CategoryPrices
order by CategoryName

--14) Create a CTE to calculate total revenue per product and filter products with revenue > 10,000.
with TotalRevenuePerProduct
as (
select ProductName, SUM(od.Quantity*od.UnitPrice*(1-od.Discount)) as TotalRevenue from [Order Details] as od
join Products on od.ProductID=Products.ProductID
group by ProductName
)
select * from TotalRevenuePerProduct
where TotalRevenue > 10000

--15) Use a CTE with recursion to display employee hierarchy.
--Start from top-level employee (ReportsTo IS NULL) and drill down
with Hierarchy
as (
select EmployeeID,concat(firstname,' ',lastname) as EmployeeName,ReportsTo from Employees where ReportsTo IS NULL
UNION ALL
select Employees.EmployeeID,concat(Employees.firstname,' ',Employees.lastname) as EmployeeName,Employees.ReportsTo from Employees inner join Hierarchy on Employees.ReportsTo=Hierarchy.EmployeeID
)
select EmployeeName,concat(firstname,' ',lastname) as ManagerName from Hierarchy
left outer join Employees on Employees.EmployeeID=Hierarchy.ReportsTo;