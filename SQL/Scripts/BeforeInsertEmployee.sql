USE Employees
GO
CREATE OR ALTER TRIGGER EmployeeInsert
	ON Employee
	AFTER INSERT
AS
	DECLARE @companyName nvarchar(20), 
			@addressId nvarchar(20);
	SELECT @companyName = CompanyName, @addressId = AddressId FROM INSERTED;
	INSERT INTO [Company] VALUES (@companyName, @addressId);
GO

