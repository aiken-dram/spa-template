--Mock data for application testing--
INSERT INTO ACCOUNT.USER_AUDIT
  (ID_USER, ID_TARGET, ID_ACTION, STAMP            , TARGET_ID, TARGET_NAME, MESSAGE) VALUES
  (1      , 1        , 1        , CURRENT_TIMESTAMP, 1        , 'Name A'   , 'message');
  
INSERT INTO SAMPLE.SAMPLE_AUDIT
  (ID_USER, ID_TARGET, ID_ACTION, STAMP            , TARGET_ID, TARGET_NAME, MESSAGE) VALUES
  (1      , 1        , 1        , CURRENT_TIMESTAMP, NULL     , 'Name A'   , 'message');