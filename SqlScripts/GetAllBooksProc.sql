CREATE PROCEDURE GetAllBooks
AS
select b.Id, b.Name, b.IsActive, b.DisplayOrder, b.CreateDate, b.UpdateDate,
                        a.Id, a.FirstName, a.LastName, a.IsActive, a.DisplayOrder, a.CreateDate, a.UpdateDate
                        from Book b inner join BookAuthor ba 
                        on (b.Id = ba.BookId)
                        inner join Author a 
                        on(ba.AuthorId = a.Id)
return
GO


