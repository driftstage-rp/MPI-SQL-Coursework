USE master;
USE [TSQL2012]
GO

DECLARE @rowCount INT

SELECT @rowCount = COUNT(*) FROM [dbo].[Nums]

PRINT '���������� ����� � ������� Nums: ' + CAST(@rowCount AS VARCHAR(10))
