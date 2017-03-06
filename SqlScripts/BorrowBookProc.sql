CREATE PROCEDURE BorrowBook
@BookId int,
@UserId int,
@BorrowDate datetime,
@NumberofDays int,
@result int OUTPUT		
AS
if not exists (select * from UserBook where CAST(DATEADD(day,BorrowDays,BorrowDate) as DATE) > CAST(GETDATE() as DATE) and BookId = @BookId)
begin
insert into UserBook(BorrowDate, BorrowDays, IsActive, DisplayOrder, CreateDate, UpdateDate, BookId, UserId)
select @BorrowDate, @NumberofDays, 1, 1, GETDATE(), GETDATE(), @BookId, @UserId
select @result = 1
end
else
begin
select @result = -1 
end
return 
GO


