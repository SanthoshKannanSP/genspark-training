select * from products
where TRY_CAST(json_value(details,'$.spec.cpu') as nvarchar(20)) = '15';

create proc proc_count(@pcpu nvarchar(20), @pcount int out)
as
begin
	set @pcount = (select Count(*) from products
		where TRY_CAST(json_value(details,'$.spec.cpu') as nvarchar(20)) = @pcpu)
end

declare @cnt int;
exec proc_count '15', @cnt out
print concat('The number of computers is ',@cnt)

create table people (
id int primary key,
name nvarchar(30),
age int
);

create or alter proc proc_BulkInsert(@filepath nvarchar(500))
as
begin
	declare @insertQuery nvarchar(max)

   set @insertQuery = 'BULK INSERT people from '''+ @filepath +'''
   with(
   FIRSTROW =2,
   FIELDTERMINATOR='','',
   ROWTERMINATOR = ''\n'')'
   exec sp_executesql @insertQuery
end

exec proc_BulkInsert 'C:\training\genspark-training\day04\data.csv'

select * from people;

create table BulkInsertLog(
logId int identity primary key,
filepath nvarchar(1000),
status nvarchar(50) constraint chk_status Check(status in('Success','Failed')),
message nvarchar(1000)
)

create or alter proc proc_BulkInsert(@filepath nvarchar(500))
as
begin
	begin try

	declare @insertQuery nvarchar(max)

	set @insertQuery = 'BULK INSERT people from '''+ @filepath +'''
	with(
	FIRSTROW =2,
	FIELDTERMINATOR='','',
	ROWTERMINATOR = ''\n'')'
	exec sp_executesql @insertQuery

	insert into BulkInsertLog(filepath,status,message)
	values (@filepath,'Success','Bulk insert was successful')

	end try
	begin catch

	insert into BulkInsertLog (filepath,status,message)
	values (@filepath,'Failed',ERROR_MESSAGE());

	end catch
end

exec proc_BulkInsert 'C:\training\genspark-training\day04\data.csv'

select * from people;
select * from BulkInsertLog;

truncate table people;

exec proc_BulkInsert 'C:\training\genspark-training\day04\data.csv'

select * from people;
select * from BulkInsertLog;