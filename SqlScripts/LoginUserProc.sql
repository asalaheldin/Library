CREATE PROCEDURE LoginUser 
@Email nvarchar(50)
AS
select * from [User] u where u.Email = @Email
return
GO