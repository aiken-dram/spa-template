CREATE TABLE [ACCOUNT].[USER_GROUPS]
(
  [Id] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [IdUser] BIGINT NOT NULL FOREIGN KEY REFERENCES [ACCOUNT].[USERS],
  [IdGroup] BIGINT NOT NULL FOREIGN KEY REFERENCES [ACCOUNT].[GROUPS]
)