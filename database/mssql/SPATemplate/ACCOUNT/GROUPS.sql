CREATE TABLE [ACCOUNT].[GROUPS]
(
  [IdGroup] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [Name] VARCHAR(120) NOT NULL,
  [Desc] VARCHAR(255)
)
