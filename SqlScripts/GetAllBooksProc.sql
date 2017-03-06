CREATE PROCEDURE GetAllBooks
AS
DECLARE @T1 TABLE (
BookIdd int,
Name nvarchar(50),
IsActive bit,
DisplayOrder int,
CreateDate datetime,
UpdateDate datetime,
AuhtorId int,
AuthorFName nvarchar(50),
AuthorLName nvarchar(50),
AuthorIsActive bit,
AuthorDisplayOrder int,
AuthorCreateDate datetime,
AuthorUpdateDate datetime,
IsAvailable bit
)
insert into @T1
select b.Id, b.Name, b.IsActive, b.DisplayOrder, b.CreateDate, b.UpdateDate,
                        a.Id, a.FirstName, a.LastName, a.IsActive, a.DisplayOrder, a.CreateDate, a.UpdateDate, 1
                        from Book b inner join BookAuthor ba 
                        on (b.Id = ba.BookId)
                        inner join Author a 
                        on(ba.AuthorId = a.Id)


update @T1
set IsAvailable = 0
	where exists(
select ub.BookId 
from book bb inner join UserBook ub on (bb.Id = ub.BookId) 
where CAST(DATEADD(day,ub.BorrowDays,ub.BorrowDate) as DATE) >= CAST(GETDATE() as DATE) and bb.Id = BookIdd)


select * from @T1
return
GO


