CREATE TABLE [ACCOUNT].[GROUP_ROLES]
(
  [Id] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [IdGroup] BIGINT NOT NULL FOREIGN KEY REFERENCES [ACCOUNT].[GROUPS],
  [IdRole] BIGINT NOT NULL FOREIGN KEY REFERENCES [ACCOUNT].[ROLES]
)
