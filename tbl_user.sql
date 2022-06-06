USE [Basic]
GO
/****** Object:  StoredProcedure [dbo].[sp_user]    Script Date: 6/6/2022 9:55:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_user] (
	@nameorcitizen VARCHAR(50)
	, @password VARCHAR(max)
	, @email VARCHAR(100)= NULL
	, @flag VARCHAR(10)= NULL
	, @username VARCHAR(100)= NULL
	, @firstname VARCHAR(100)= NULL
	, @middlename VARCHAR(100)= NULL
	, @lastname VARCHAR(100)= NULL
	, @citizenshipnumber VARCHAR(100)= NULL
	, @iscitizen CHAR(1)= NULL
	, @phonenumber VARCHAR(10)= NULL
	)
AS
BEGIN
	IF (@flag = 'Login')
	BEGIN
		DECLARE @Check NVARCHAR(max)

		CREATE TABLE #temp (
			password VARBINARY(max)
			, lockcount INT
			, isblock CHAR(1)
			)

		SET @Check = 'select password,isnull(lockcount,0),isblock from tbl_user where '
		SET @Check = @Check + 'username = ''' + @nameorcitizen + '''' + ' or citizenshipnumber= ''' + @nameorcitizen + ''''

		INSERT INTO #temp
		EXEC (@Check)

		SELECT *
		FROM #temp

		IF @@ROWCOUNT > 0
		BEGIN
			IF (
					(
						SELECT lockcount
						FROM #temp
						) < 5
					)
			BEGIN
				IF (
						PWDCOMPARE(@password, (
								SELECT password
								FROM #temp
								)) = 1
						)
				BEGIN
					IF (
							(
								SELECT isblock
								FROM #temp
								) = 'y'
							)
					BEGIN
						SELECT 400 StatusCode
							, 'User is blocked please contact admin' Message

						DROP TABLE #temp
					END
					ELSE
					BEGIN
						SELECT 200 StatusCode
							, 'Login Successfully' Message

						SELECT u.*,d.*
						FROM tbl_user u
						Join tbl_designation d on u.id=d.userid
						WHERE username = @nameorcitizen
							OR citizenshipnumber = @nameorcitizen

						DROP TABLE #temp
					END
				END
				ELSE
				BEGIN
					UPDATE tbl_user
					SET lockcount = (
							SELECT (ISNULL(lockcount, 0) + 1)
							FROM #temp
							)
					WHERE username = @nameorcitizen
						OR citizenshipnumber = @nameorcitizen
				END
			END
			ELSE
			BEGIN
				SELECT 400 StatusCode
					, 'Your Account is locked please contact admin or do unlock' Message

				UPDATE tbl_user
				SET lockdate = Getdate()
					, islock = 'y'
				WHERE username = @nameorcitizen
					OR citizenshipnumber = @nameorcitizen

				DROP TABLE #temp
			END
		END
		ELSE
		BEGIN
			SELECT 400 StatusCode
				, 'CitizenShipNumber / UserName doesnot match' Message

			DROP TABLE #temp
		END
	END

	IF (@flag = 'Register')
	BEGIN
		IF NOT EXISTS (
				SELECT 'M'
				FROM tbl_user
				WHERE username = @username
				)
		BEGIN
			IF NOT EXISTS (
					SELECT 'M'
					FROM tbl_user
					WHERE citizenshipnumber = @citizenshipnumber
					)
			BEGIN
				INSERT INTO tbl_user (
					username
					, password
					, email
					, firstname
					, middlename
					, lastname
					, citizenshipnumber
					, iscitizen
					, phonenumber
					, isblock
					, islock
					, createddate
					)
				VALUES (
					@username
					, PWDENCRYPT(@password)
					, @email
					, @firstname
					, @middlename
					, @lastname
					, @citizenshipnumber
					, @iscitizen
					, @phonenumber
					, 'n'
					, 'n'
					, GETDATE()
					)
			END
			ELSE
			BEGIN
				SELECT 400 StatusCode
					, 'citizenship number already exists' Message

				DROP TABLE #temp
			END
		END
		ELSE
		BEGIN
			SELECT 400 StatusCode
				, 'Username already exists' Message

			DROP TABLE #temp
		END
	END
END