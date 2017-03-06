CREATE PROCEDURE BookHistory 
@id int
AS
select  b.Name ,(u.FirstName + ' ' + u.LastName) FullName, ub.BorrowDate, CAST(DATEADD(day,ub.BorrowDays,ub.BorrowDate) as DATE) EndBorrowDate from Book b 
                            inner join UserBook ub
                            on (b.Id = ub.BookId)
                            inner join [User] u
                            on (u.Id = ub.UserId)
                            where b.Id = @id
return
GO