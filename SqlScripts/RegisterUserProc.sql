CREATE PROCEDURE RegisterUser 
@Email nvarchar(50),
@FirstName nvarchar(50),
@LastName nvarchar(50),
@id int OUTPUT
AS
Insert into [User](FirstName, LastName, Email, IsActive)
select @FirstName, @LastName, @Email, 1 

select @id = SCOPE_IDENTITY()
return
GO