CREATE PROCEDURE GetAvailableBooks
AS
select b.Id, b.Name, b.IsActive, b.DisplayOrder, b.CreateDate, b.UpdateDate,
                        a.Id, a.FirstName, a.LastName, a.IsActive, a.DisplayOrder, a.CreateDate, a.UpdateDate
                        from Book b inner join BookAuthor ba 
                        on (b.Id = ba.BookId)
                        inner join Author a 
                        on(ba.AuthorId = a.Id)
                        where b.Id not in (select BookId from UserBook where CAST(DATEADD(day,BorrowDays,BorrowDate) as DATE) > CAST(GETDATE() as DATE))
return
GO


