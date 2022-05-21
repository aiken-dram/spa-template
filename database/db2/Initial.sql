--initial database values--
--//DICTIONARY//--
DELETE FROM DICT.DISTRICTS;
INSERT INTO DICT.DISTRICTS
  (ID_DISTRICT, "NAME") VALUES
  (1 ,'District A'),
  (2 ,'District B'),
  (3 ,'District C');
  
DELETE FROM DICT.REQUEST_TYPES;
INSERT INTO DICT.REQUEST_TYPES 
  ("ID_TYPE", "TYPE", "DESC") VALUES
  (1, 'TABLE_EXPORT_AUDIT',  'Export audit table'),
  (2, 'TABLE_EXPORT_SAMPLE', 'Export sample table'),
  (3, 'RSCRIPT',             'Run R script';
  
DELETE FROM DICT.REQUEST_STATES;
INSERT INTO DICT.REQUEST_STATES 
  ("ID_STATE", "STATE", "DESC") VALUES
  (1, 'QUEUE'     , 'In queue'),
  (2, 'PROCESSING', 'Processing'),
  (3, 'READY'     , 'Ready'),
  (4, 'DELIVERED' , 'Delivered'),
  (5, 'ERROR'     , 'Error');

DELETE FROM DICT.AUDIT_ACTIONS;
INSERT INTO DICT.AUDIT_ACTIONS 
  ("ID_ACTION", "ACTION", "DESC") VALUES
  (1, 'CREATE'            ,'Create'),
  (2, 'EDIT'              ,'Edit'),
  (3, 'DELETE'            ,'Delete'),
  (4, 'AUTH_LOGIN'        ,'Log in'),
  (5, 'AUTH_WRONGPASS'    ,'Wrong password'),
  (6, 'AUTH_EXPIRED'      ,'Password expired'),
  (7, 'AUTH_LOCK'         ,'User locked'),
  (8, 'USER_UPDATE_PASS'  ,'Password update from file');
  
DELETE FROM DICT.AUDIT_TARGETS;
INSERT INTO DICT.AUDIT_TARGETS 
  ("ID_TARGET", "TARGET", "DESC") VALUES
  (1, 'AUTH'          ,'Authorization'),
  (2, 'ACCOUNT.USERS' ,'Account management'),
  (3, 'MQ.REQUESTS'   ,'Requests'),
  (4, 'DICT.DISTRICTS'   ,'Districts dictionary'),
  (5, 'R.RSCRIPT'     ,'R script'),
  (6, 'R.RSCRIPT_TREE','Statistics menu');

DELETE FROM DICT.AUDIT_DATA_TYPES;
INSERT INTO DICT.AUDIT_DATA_TYPES 
  ("ID_TYPE", "TYPE", "DESC") VALUES
  (1, 'VALUE'         ,'Value'),
  (2, 'FIELD_VALUE'   ,'Field and value'),
  (3, 'FIELD_OLD_NEW' ,'Field with old and new values');
  
--//ACCOUNT//--
DELETE FROM ACCOUNT.USERS;
ALTER TABLE ACCOUNT.USERS ALTER COLUMN ID_USER RESTART WITH 1;
INSERT INTO ACCOUNT.USERS
  (LOGIN       ,PASS                              ,IS_ACTIVE ,"PASS_DATE"  ,"NAME"                  ,"DESC") VALUES
  ('admin'     ,'21232f297a57a5a743894a0e4a801fc3','T'       ,'01.01.2050' ,'Application admin'      ,'Application administrator'),
  ('secadm'    ,'21232f297a57a5a743894a0e4a801fc3','T'       ,'01.01.2050' ,'Access admin' ,'Access administrator'),
  ('supervisor' ,'098f6bcd4621d373cade4e832627b4f6','T'       ,'01.01.2050' ,'Supervisor'     ,'User with extended access to data'),
  ('test-2'   ,'098f6bcd4621d373cade4e832627b4f6','T'       ,'01.01.2050' ,'District B test'  ,'Test user with district B access'),
  ('viewer' ,'098f6bcd4621d373cade4e832627b4f6','T'       ,'01.01.2050' ,'Readonly test'              ,'Test user with readonly access');
   
DELETE FROM ACCOUNT.GROUPS;
ALTER TABLE ACCOUNT.GROUPS ALTER COLUMN ID_GROUP RESTART WITH 1;
INSERT INTO ACCOUNT.GROUPS
  ("NAME", "DESC") VALUES
  ('Admins'         ,'Group of application admins'),
  ('Access admins'  ,'Group of access admins'),
  ('Supervisors'    ,'Group of supervisors'),
  ('Users'          ,'Group of application users'),
  ('Viewers'        ,'Group of users with readonly access');
  
DELETE FROM ACCOUNT.ROLES;
ALTER TABLE ACCOUNT.ROLES ALTER COLUMN ID_ROLE RESTART WITH 1;
INSERT INTO ACCOUNT.ROLES
  ("NAME", "DESC") VALUES
  ('Admin'        ,'Admin role'),
  ('Access admin' ,'Access admin role'),
  ('Supervisor'   ,'Supervisor role'),
  ('Readonly'     ,'Read only role');
  
DELETE FROM ACCOUNT.MODULES;
ALTER TABLE ACCOUNT.MODULES ALTER COLUMN ID_MODULE RESTART WITH 1;
INSERT INTO ACCOUNT.MODULES
  ("NAME", "DESC") VALUES
  ('DICTADM'   ,'Dictionary admin module'),
  ('CFGADM'    ,'Application configuration module'),
  ('SECADM'    ,'Security admin module'),
  ('SUPERVISE' ,'Extended data access module'),
  ('READONLY'  ,'Read only access restriction module'); 
  
DELETE FROM ACCOUNT.USER_GROUPS;
ALTER TABLE ACCOUNT.USER_GROUPS ALTER COLUMN ID RESTART WITH 1;
INSERT INTO ACCOUNT.USER_GROUPS
  (ID_USER, ID_GROUP) VALUES
  (1, 1),
  (2, 2),
  (3, 3),
  (4, 4),
  (5, 5);
  
DELETE FROM ACCOUNT.GROUP_ROLES;
ALTER TABLE ACCOUNT.GROUP_ROLES ALTER COLUMN ID RESTART WITH 1;
INSERT INTO ACCOUNT.GROUP_ROLES
  (ID_GROUP, ID_ROLE) VALUES
  (1, 1),
  (1, 2),
  (1, 3),
  (2, 2),
  (3, 3),
  (5, 4);
  
DELETE FROM ACCOUNT.ROLE_MODULES;
ALTER TABLE ACCOUNT.ROLE_MODULES ALTER COLUMN ID RESTART WITH 1;
INSERT INTO ACCOUNT.ROLE_MODULES
  (ID_ROLE, ID_MODULE) VALUES
  (1, 1),
  (1, 2),
  (2, 3),
  (3, 4),
  (4, 5);

DELETE FROM ACCOUNT.USER_DISTRICTS;
ALTER TABLE ACCOUNT.USER_DISTRICTS ALTER COLUMN ID RESTART WITH 1;
INSERT INTO ACCOUNT.USER_DISTRICTS
  (ID_USER, ID_DISTRICT) VALUES
  (4, 1);