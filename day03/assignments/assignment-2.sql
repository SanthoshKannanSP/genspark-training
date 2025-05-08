create table Products(
	id int identity,
	name varchar(20) NOT NULL,
	details varchar(200) NOT NULL
);

create procedure proc_InsertProduct(@pname varchar(29),@pdetails varchar(200))
as
begin
	insert into Products(name, details) values (@pname,@pdetails)
end

exec proc_InsertProduct 'Laptop','{"brand":"Dell","spec":{"ram":"16GB","cpu":"15"}}'

select * from Products;

select JSON_QUERY(details,'$.spec') from Products;

create proc proc_UpdateProductRam(@pid int,@newvalue varchar(20))
as
begin
   update products set details = JSON_MODIFY(details, '$.spec.ram',@newvalue) where id = @pid
end

proc_UpdateProductRam 1, '24GB'

select id, name, JSON_VALUE(details, '$.brand') Brand_Name from Products;

  create table Posts
  (id int primary key,
  title nvarchar(100),
  user_id int,
  body nvarchar(max))
Go

  select * from Posts

  create proc proc_BulInsertPosts(@jsondata nvarchar(max))
  as
  begin
		insert into Posts(user_id,id,title,body)
	  select userId,id,title,body from openjson(@jsondata)
	  with (userId int,id int, title varchar(100), body varchar(max))
  end

  proc_BulInsertPosts '
[
  {
    "userId": 1,
    "id": 1,
    "title": "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
    "body": "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
  },
  {
    "userId": 1,
    "id": 2,
    "title": "qui est esse",
    "body": "est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla"
  }]'

  select * from products
  where TRY_CAST(json_value(details,'$.spec.cpu') as nvarchar(20)) = '15';

-- create a procedure that brings post by taking the user_id as parameter
select * from Posts;

create proc proc_GetPostById(@pid int)
as
begin 
	select * from Posts where id=@pid;
end

exec proc_GetPostById 2

create proc proc_GetPostByUserId(@puserid int)
as
begin 
	select * from Posts where user_id=@puserid;
end

exec proc_GetPostByUserId 1