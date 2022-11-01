CREATE VIEW [ACCOUNT].[V_AUDIT_DATA]
  AS
    SELECT
      'USER' as [Source],
      U.[Id],
      U.[IdAudit],
      U.[IdType],
      UT.[Type],
      U.[JSON]
    FROM [ACCOUNT].[USER_AUDIT_DATA] U
      JOIN [DICT].[AUDIT_DATA_TYPES] UT
      ON U.[IdType] = UT.[IdType]
  UNION ALL
    SELECT
      'SAMPLE' as [Source],
      S.[Id],
      S.[IdAudit],
      S.[IdType],
      ST.[Type],
      S.[JSON]
    FROM [SAMPLE].[SAMPLE_AUDIT_DATA] S
      JOIN [DICT].[AUDIT_DATA_TYPES] ST
      ON S.[IdType] = ST.[IdType]


