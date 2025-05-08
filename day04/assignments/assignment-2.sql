with cte_Authors
as
(
select au_id, concat(au_fname,' ',au_lname) author_name from authors
)
select * from cte_Authors;

declare @page int = 1, @pageSize int = 10;
with cte_PaginatedBooks as
(select title_id,title,price, ROW_NUMBER() over (order by price desc) as Row_Num from titles)
select * from cte_PaginatedBooks where Row_Num between (@page-1)*@pageSize and @page*@pageSize;

-- create a sp that will take the page number and size as parameter and print the books
create or alter proc sp_CurrentPaginatedBooks(@pPage int, @pPageSize int)
as
begin
	with cte_PaginatedBooks as
	(select title_id,title,price, ROW_NUMBER() over (order by price desc) as Row_Num from titles)
	select * from cte_PaginatedBooks where Row_Num between (@pPage-1)*@pPageSize+1 and @pPage*@pPageSize; 
end

exec sp_CurrentPaginatedBooks 2,5;

-- offest fetch
select title_id, title from titles
order by price desc
offset 10 rows fetch next 10 rows only;

-- scalar valued function
create function fn_calculateTax(@baseprice float, @tax float)
returns float
as
begin
	return (@baseprice + (@baseprice*@tax/100))
end

select title, dbo.fn_calculateTax(price, 12) as tax from titles;

-- table valued function
create function fn_tableSample(@minprice float)
returns table
as
return select title,price from titles where price>=@minprice

select title,price from fn_tableSample(15)
order by price;

-- fetching with cursor
DECLARE titles_cursor CURSOR
    FOR SELECT * FROM titles

OPEN titles_cursor

FETCH NEXT FROM titles_cursor;
FETCH NEXT FROM titles_cursor;
FETCH NEXT FROM titles_cursor;

close titles_cursor

-- using cursor to select all the books written by an author, one author at a time
DECLARE author_cursor CURSOR
    FOR SELECT au_id,concat(au_lname,' ',au_fname) author_name FROM authors;

open author_cursor;

declare @current_author_id nvarchar(15), @current_author_name nvarchar(200);
fetch next from author_cursor into @current_author_id, @current_author_name;
select title,@current_author_name as author_name from titles
join titleauthor
on titles.title_id=titleauthor.title_id
where titleauthor.au_id=@current_author_id;

close author_cursor;

-- transaction using try catch
alter table titles
add constraint chk_price
check (price>0);

select * from titles;

create proc proc_updatePrice(@pTitleId nvarchar(10), @pPrice float)
as
begin
	update titles
	set price=@pPrice
	where title_id=@pTitleId;
end

create table price_log (
	log_id int identity primary key,
	title_id nvarchar(10),
	new_price float
);

declare @vtitle_id nvarchar(10) = 'BU1032', @vprice float = -15.99;
begin try
	begin transaction updatePrice
		insert into price_log (title_id,new_price) values (@vtitle_id,@vprice)
		exec proc_updatePrice @vtitle_id, @vprice
	commit transaction updatePrice
end try
begin catch
	print 'Error: Unable to update price'
	rollback transaction updatePrice
end catch

select * from price_log;
select * from titles;

-- using trigger to update overall book rating as the user reviews are inserted
create table reviews (
	review_id int identity primary key,
	title_id nvarchar(10) NOT NULL,
	review_score float NOT NULL,
	constraint chk_review_score check (review_score>0 and review_score<=5)
);

alter table titles
add rating float;

update titles
set rating=0.0;

select * from titles;

create or alter trigger update_ratings on reviews
after insert
as
begin
	declare @new_rating float;
	declare @title_id nvarchar(15);
	
	DECLARE inserted_cursor CURSOR FOR
    SELECT title_id FROM inserted;
	open inserted_cursor;
	fetch next from inserted_cursor into @title_id;
	close inserted_cursor;
	deallocate inserted_cursor;

	set @new_rating = (select top 1 AVG(review_score) from reviews where title_id=@title_id);
	update titles
	set rating=@new_rating
	where title_id=@title_id
end

insert into reviews (title_id,review_score) values ('BU1032',4.6);
insert into reviews (title_id,review_score) values ('BU1032',4);
select * from reviews;
select * from titles;