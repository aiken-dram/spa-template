CREATE VIEW [ACCOUNT].[V_AUDIT]
  AS 
    SELECT
      'USER' AS [Source],
      U.[IdAudit],
      U.[IdUser],
      UU.[Login],
      U.[IdTarget],
      UAT.[Target],
      UAT.[Desc] AS [TargetDesc],
      U.[IdAction],
      UAA.[Desc] AS [Action],
      U.[Stamp],
      U.[TargetId],
      U.[TargetName],
      U.[Message]
    FROM [ACCOUNT].[USER_AUDIT] U
      JOIN [ACCOUNT].[USERS] UU ON U.[IdUser] = UU.[IdUser]
      JOIN [DICT].[AUDIT_ACTIONS] UAA ON U.[IdAction] = UAA.[IdAction]
      JOIN [DICT].[AUDIT_TARGETS] UAT ON U.[IdTarget] = UAT.[IdTarget]
  UNION ALL
    SELECT
      'SAMPLE' as [Source],
      S.[IdAudit],
      S.[IdUser],
      SU.[Login],
      S.[IdTarget],
      SAT.[Target],
      SAT.[Desc] AS [TargetDesc],
      S.[IdAction],
      SAA.[Desc] AS [Action],
      S.[Stamp],
      S.[TargetId],
      S.[TargetName],
      S.[Message]
    FROM [SAMPLE].[SAMPLE_AUDIT] S
      JOIN [ACCOUNT].[USERS] SU ON S.[IdUser] = SU.[IdUser]
      JOIN [DICT].[AUDIT_ACTIONS] SAA ON S.[IdAction] = SAA.[IdAction]
      JOIN [DICT].[AUDIT_TARGETS] SAT ON S.[IdTarget] = SAT.[IdTarget]
