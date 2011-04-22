IF OBJECT_ID('ldn_DropTables', 'P') IS NOT NULL
	DROP PROCEDURE ldn_DropTables
GO


CREATE PROCEDURE ldn_DropTables
(
	@Prefix [NVarChar](10) 
)
AS
BEGIN

DECLARE @tableName nvarchar(250)
DECLARE @constrName nvarchar(250)
DECLARE @dropTableSql nvarchar(MAX)
DECLARE @sql NVarChar(300)
DECLARE @tableID int
DECLARE @IsTable bit
SET @dropTableSql = ''

DECLARE tableCursor CURSOR FOR
	SELECT t.object_id, t.name, 1 AS [IsTable]
	FROM sys.tables t 
	WHERE LOWER(t.name) like LOWER(@Prefix) + '%'
	UNION ALL
	SELECT t.object_id, t.name, 0 AS [IsTable]
	FROM sys.views t 
	WHERE LOWER(t.name) like LOWER(@Prefix) + '%'
	ORDER BY [IsTable]

OPEN tableCursor;
FETCH NEXT FROM tableCursor INTO @tableID, @tableName, @IsTable;
WHILE @@FETCH_STATUS = 0
	BEGIN
		-- Drop references from this table
		DECLARE constrCursor CURSOR FOR
			SELECT name FROM sys.foreign_keys 
			WHERE parent_object_id = @tableID
		OPEN constrCursor;
		FETCH NEXT FROM constrCursor INTO @constrName;
		WHILE @@FETCH_STATUS = 0
			BEGIN
				SET @sql = 'ALTER TABLE ' + @tableName + ' DROP CONSTRAINT [' + @constrName +']';
				EXEC ( @sql )	
			
				FETCH NEXT FROM constrCursor INTO @constrName;
			END;
		CLOSE constrCursor;
		DEALLOCATE constrCursor;

		-- Drop references to this table
		DECLARE @tableName2 nvarchar(250)
		DECLARE constrCursor2 CURSOR FOR
			SELECT k.name, t.name FROM sys.foreign_keys k INNER JOIN sys.tables t ON t.object_id = k.parent_object_id
			WHERE referenced_object_id = @tableID
		OPEN constrCursor2;
		FETCH NEXT FROM constrCursor2 INTO @constrName, @tableName2;
		WHILE @@FETCH_STATUS = 0
			BEGIN
				SET @sql = 'ALTER TABLE ' + @tableName2 + ' DROP CONSTRAINT [' + @constrName +']';
				EXEC ( @sql )	
			
				FETCH NEXT FROM constrCursor2 INTO @constrName, @tableName2;
			END;

		CLOSE constrCursor2;
		DEALLOCATE constrCursor2;
		IF(@IsTable = 1)
			SET @dropTableSql = @dropTableSql + ' DROP TABLE ' + @tableName + ';'
		ELSE
			SET @dropTableSql = @dropTableSql + ' DROP VIEW ' + @tableName + ';'

		FETCH NEXT FROM tableCursor INTO @tableID, @tableName, @IsTable;
   END;
CLOSE tableCursor;
DEALLOCATE tableCursor;

EXEC ( @dropTableSql )	
END