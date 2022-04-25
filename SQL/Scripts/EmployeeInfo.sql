CREATE OR ALTER VIEW [EmployeeInfo] AS
SELECT TOP 1000
	e.Id, 
	ISNULL(e.EmployeeName, p.FirstName + ' ' + p.LastName) AS EmployeeFullName,
	ISNULL(a.ZipCode, 'Unknown zip') + '_' + ISNULL(a.State, 'Unknown state') + ', ' + ISNULL(a.City, 'Unknown city') + '-' + ISNULL(a.Street, 'Unknown street') AS EmployeeFullAddress,
	ISNULL(e.CompanyName, 'Unknown company') + '(' + ISNULL(e.Position, 'Unknown position') + ')' AS CompanyInfo
FROM Employee e 
	Join Person p ON e.PersonId = p.Id
	Join Address a ON e.AddressId = a.Id
ORDER BY e.CompanyName, a.City ASC;

