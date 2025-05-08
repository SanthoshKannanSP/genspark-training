-- print the publisher details of the publisher who has never published
select * from publishers
where pub_id NOT IN (
	SELECT DISTINCT(pub_id) from titles
)

-- Select the author_id for all the books. Print the author_id and the book name
select au_id, title from titleauthor
right outer join titles
on titleauthor.title_id=titles.title_id;

select concat(au_fname, au_lname), title from authors
join titleauthor on authors.au_id=titleauthor.au_id
join titles on titleauthor.title_id=titles.title_id;

--Print the publisher's name, book name and the order date of the  books
select pub_name 'Publisher name', title 'Book name', ord_date 'Order date' from publishers
join titles on titles.pub_id=publishers.pub_id
join sales on sales.title_id=titles.title_id;

select * from sales;

-- Print the publisher name and the first book sale date for all the publisher
select pub_name Publisher_name, MIN(ord_date) Order_date from publishers
left outer join titles on titles.pub_id=publishers.pub_id
left outer join sales on sales.title_id=titles.title_id
group by pub_name
order by 2 desc;

-- print the book name and the store address of the sale
select title Book_Name, stor_address Store_Address from sales
join stores on stores.stor_id=sales.stor_id
join titles on sales.title_id=titles.title_id
order by 1
;

create procedure proc_FirstProcedure
as
begin
	print 'Hello World'
end

exec proc_FirstProcedure