CREATE TABLE [R].[RSCRIPT_PARAMS]
(
  [Id] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [IdRScript] BIGINT NOT NULL FOREIGN KEY REFERENCES [R].[RSCRIPTS],
  [IdType] INT NOT NULL FOREIGN KEY REFERENCES [DICT].[RSCRIPT_PARAM_TYPES],
  [Name] VARCHAR(120) NOT NULL,
  [Hint] VARCHAR(255),
  [Desc] VARCHAR(255)
)
