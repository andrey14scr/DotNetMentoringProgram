USE Employees;
GO
CREATE OR ALTER PROCEDURE AddEmployee
    @EmployeeName nvarchar(100)=NULL,
    @FirstName nvarchar(50)=NULL,
	@LastName nvarchar(50)=NULL,
	@CompanyName nvarchar(20),
	@Position nvarchar(30)=NULL,
	@Street nvarchar(50),
	@City nvarchar(20)=NULL,
	@State nvarchar(50)=NULL,
	@ZipCode nvarchar(50)=NULL
AS
	DECLARE @lastAddressId INT;
	DECLARE @lastPersonId INT;

	IF RTRIM(ISNULL(@EmployeeName, '')) LIKE '' AND RTRIM(ISNULL(@FirstName, '')) LIKE '' AND RTRIM(ISNULL(@LastName, '')) LIKE ''
		THROW 50005, 'At least one of name variables must be not null, empty or white spaced', 1;

	INSERT INTO [Person] VALUES (@FirstName, @LastName);
	SELECT @lastPersonId = SCOPE_IDENTITY();
	INSERT INTO [Address] VALUES (@Street, @City, @State, @ZipCode);
	SELECT @lastAddressId = SCOPE_IDENTITY();
	
	INSERT INTO [Employee] VALUES (@lastAddressId, @lastPersonId, @CompanyName, @Position, @EmployeeName);
GO