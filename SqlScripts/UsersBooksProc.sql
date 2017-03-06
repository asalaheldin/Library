CREATE PROCEDURE UsersBooks
AS
select u.Id, u.FirstName, u.LastName, u.Email, u.IsActive ,
                        b.Id, b.Name, ub.BorrowDate, CAST(DATEADD(day,ub.BorrowDays,ub.BorrowDate) as DATE) EndBorrowDate
                        from [User] u
                        inner join UserBook ub on (u.Id = ub.UserId)
                        inner join Book b on (ub.BookId = b.Id)
                        where CAST(DATEADD(day,ub.BorrowDays,ub.BorrowDate) as DATE) >= CAST(GETDATE() as DATE)
return
GO


