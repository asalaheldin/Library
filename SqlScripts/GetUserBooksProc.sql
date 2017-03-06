CREATE PROCEDURE GetUserBooks
@id int
AS
select b.Id, b.Name, b.IsActive, b.DisplayOrder, b.CreateDate, b.UpdateDate,
                        a.Id, a.FirstName, a.LastName, a.IsActive, a.DisplayOrder, a.CreateDate, a.UpdateDate, CAST(0 As BIT) IsAvailable
                        from Book b inner join BookAuthor ba 
                        on (b.Id = ba.BookId)
                        inner join Author a 
                        on(ba.AuthorId = a.Id)
                        where b.Id in (select BookId from UserBook where UserId = @id and CAST(DATEADD(day,BorrowDays,BorrowDate) as DATE) > CAST(GETDATE() as DATE))
return
GO


