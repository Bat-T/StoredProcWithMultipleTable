# StoredProcWithMultipleTable
Created a stored Procedure in LocalDB and fecthed data using minimal API - 

"CREATE PROCEDURE MyCustomProcedure
                               AS
                               SELECT [Name] FROM Student;
							   Select [ClassName],B.[Name] from Classes A join Student B on A.ClassId = B.ClassId;" 

SP Used .
