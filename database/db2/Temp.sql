SELECT
 'USER' as SRS,
 U.*
FROM ACCOUNT.USER_AUDIT U
UNION ALL
SELECT
 'SAMPLE' as SRS,
 S.* 
FROM SAMPLE.SAMPLE_AUDIT S;

SELECT
 'USER' as SRS,
 U.* 
FROM ACCOUNT.USER_AUDIT_DATA U
UNION ALL
SELECT
 'SAMPLE' as SRS,
 S.* 
FROM SAMPLE.SAMPLE_AUDIT_DATA S;