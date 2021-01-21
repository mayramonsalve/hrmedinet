DECLARE @Location_Id INT
DECLARE @Company_Id INT
SET @Company_Id = 22 --33,34,35
DECLARE cLocations CURSOR FOR
SELECT  ID FROM LOCATIONS
WHERE COMPANY_ID = @Company_Id
DECLARE @Test_Id INT
SET @Test_Id = 36--35,36(SELECT ID FROM TESTS WHERE COMPANY_ID = @Company_Id)
OPEN cLocations
FETCH cLocations INTO @Location_Id
DECLARE @Count INT
DECLARE @Total INT
SET @Total = ROUND(((40 - 20 - 1) * RAND() + 20), 0)
DECLARE @sex VARCHAR(10)
SET @sex = 'Female'
WHILE (@@FETCH_STATUS = 0 )
BEGIN
	SET @Count = 0
	WHILE(@Count < @Total)
	BEGIN
		DECLARE @Evaluation_Id INT
		DECLARE @aux INT
		DECLARE @age INT
		DECLARE @instruction INT
		DECLARE @position INT
		DECLARE @seniority INT
		SET @age = ROUND(((select (count(id)-2) from ages where company_id = @Company_Id ) * RAND() + (select min(id) from ages where company_id = @Company_Id) ), 0)
		SET @instruction = ROUND(((select (count(id)-2) from instructionlevels where company_id = @Company_Id ) * RAND() + (select min(id) from instructionlevels where company_id = @Company_Id) ), 0)
		SET @position = ROUND(((select (count(id)-2) from positionlevels where company_id = @Company_Id ) * RAND() + (select min(id) from positionlevels where company_id = @Company_Id) ), 0)
		SET @seniority = ROUND(((select (count(id)-2) from seniorities where company_id = @Company_Id ) * RAND() + (select min(id) from seniorities where company_id = @Company_Id) ), 0)
		INSERT INTO evaluations(creationdate, ipaddress, remotehostname, remoteusername, sex,age_id, instructionlevel_id, positionlevel_id, seniority_id, test_id, location_id)
		VALUES(GETDATE(), '-', '-', '-', @sex, @age, @instruction, @position, @seniority,@Test_Id, @Location_Id)
		SET @Evaluation_Id = SCOPE_IDENTITY()
		DECLARE @fo1 INT
		DECLARE @fo2 INT
		SET @fo1 =  ROUND(((select (count(id)-2) from functionalorganization where type_id = 19 ) * RAND() + (select min(id) from functionalorganization where type_id = 19) ), 0)
		SET @fo2 =  ROUND(((select (count(id)-2) from functionalorganization where type_id = 20 ) * RAND() + (select min(id) from functionalorganization where type_id = 20) ), 0)
		INSERT INTO evaluationfo(evaluation_id, functionalorganization_id)
		VALUES(@Evaluation_Id, @fo1)
		INSERT INTO evaluationfo(evaluation_id, functionalorganization_id)
		VALUES(@Evaluation_Id, @fo2)
		INSERT INTO selectionanswers(creationdate, option_id, evaluation_id, question_id)
		SELECT getdate(), ROUND(((35 - 31 - 1) * RAND() + 31), 0), @Evaluation_Id, q.id
		FROM questions q WHERE q.id BETWEEN 194 AND 226
		IF @sex = 'Male'
			SET @sex = 'Female'
		ELSE
			SET @sex = 'Male'
		SET @Count = @Count + 1	
	END
	FETCH cLocations INTO @Location_Id
END
CLOSE cLocations
DEALLOCATE cLocations