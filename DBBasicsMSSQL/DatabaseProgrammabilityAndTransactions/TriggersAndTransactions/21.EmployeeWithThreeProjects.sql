CREATE PROC usp_AssignProject (@EmployeeID INT, @ProjectId INT)
AS
BEGIN
	DECLARE @maxEmployeeProjectsCount INT = 3;
	DECLARE @employeeProjectsCount INT
	SET @employeeProjectsCount = (SELECT COUNT(*)
									FROM EmployeesProjects AS ep
									WHERE ep.EmployeeId = @EmployeeID)
	BEGIN TRANSACTION
		INSERT INTO dbo.EmployeesProjects
		(
		    EmployeeID,
		    ProjectID
		)
		VALUES
		(
		    @EmployeeID,
			@ProjectId
		)
		IF(@employeeProjectsCount>= @maxEmployeeProjectsCount)
		BEGIN
			RAISERROR ('The employee has too many projects!', 16,1)
			ROLLBACK
		END
		ELSE
			COMMIT
END