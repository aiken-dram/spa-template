/* DICTIONARY */
DELETE FROM DICT.DISTRICTS;
INSERT INTO DICT.DISTRICTS
  (`ID_DISTRICT`, `NAME`) VALUES
  (1 ,'District A'),
  (2 ,'District B'),
  (3 ,'District C');
  
DELETE FROM DICT.REQUEST_TYPES;
INSERT INTO DICT.REQUEST_TYPES 
  (`ID_TYPE`, `TYPE`, `DESC`) VALUES
  (1, 'TABLE_EXPORT_AUDIT', 'Audit table export'),
  (2, 'TABLE_EXPORT_SAMPLE', 'Sample table export');
  
DELETE FROM DICT.REQUEST_STATES;
INSERT INTO DICT.REQUEST_STATES 
  (`ID_STATE`, `STATE`, `DESC`) VALUES
  (1, 'QUEUE'     , 'In queue'),
  (2, 'PROCESSING', 'Processing'),
  (3, 'READY'     , 'Ready'),
  (4, 'DELIVERED' , 'Delivered'),
  (5, 'ERROR'     , 'Error');

DELETE FROM DICT.EVENT_ACTIONS;
INSERT INTO DICT.EVENT_ACTIONS 
  (`ID_ACTION`, `ACTION`, `DESC`) VALUES
  (1, 'CREATE'            ,'Create'),
  (2, 'EDIT'              ,'Edit'),
  (3, 'DELETE'            ,'Delete'),
  (4, 'AUTH_LOGIN'        ,'Logging in'),
  (5, 'AUTH_WRONGPASS'    ,'Wrong password'),
  (6, 'AUTH_EXPIRED'      ,'Password expired'),
  (7, 'AUTH_LOCK'         ,'User locked'),
  (8, 'USER_UPDATE_PASS'  ,'Update password from file');
  
DELETE FROM DICT.EVENT_TARGETS;
INSERT INTO DICT.EVENT_TARGETS 
  (`ID_TARGET`, `TARGET`, `DESC`) VALUES
  (1, 'AUTH'          ,'Authorization'),
  (2, 'ACCOUNT.USERS' ,'Application users'),
  (3, 'MQ.REQUESTS'   ,'Requests'),
  (4, 'DICT.DISTRICTS'   ,'District dictionary');

DELETE FROM DICT.EVENT_DATA_TYPES;
INSERT INTO DICT.EVENT_DATA_TYPES 
  (`ID_TYPE`, `TYPE`, `DESC`) VALUES
  (1, 'VALUE'         ,'Value'),
  (2, 'FIELD_VALUE'   ,'Field and value'),
  (3, 'FIELD_OLD_NEW' ,'Field, old and new value');
  
/* ACCOUNT */
DELETE FROM ACCOUNT.USERS;
ALTER TABLE ACCOUNT.USERS AUTO_INCREMENT = 1;
INSERT INTO ACCOUNT.USERS
  (`LOGIN`     ,`PASS`                            ,`IS_ACTIVE` ,`PASS_DATE`  ,`NAME`                  ,`DESC`) VALUES
  ('admin'     ,'21232f297a57a5a743894a0e4a801fc3','T'         ,'2050-01-01' ,'Application admin'     ,'Application admin description'),
  ('secadm'    ,'21232f297a57a5a743894a0e4a801fc3','T'         ,'2050-01-01' ,'Access admmin'         ,'Access admin description'),
  ('supervise' ,'098f6bcd4621d373cade4e832627b4f6','T'         ,'2050-01-01' ,'Supervisor'            ,'Supervisor description'),
  ('user'      ,'098f6bcd4621d373cade4e832627b4f6','T'         ,'2050-01-01' ,'User'                  ,'User description'),
  ('view'      ,'098f6bcd4621d373cade4e832627b4f6','T'         ,'2050-01-01' ,'Viewer'                ,'Viewer description');
   
DELETE FROM ACCOUNT.GROUPS;
ALTER TABLE ACCOUNT.GROUPS AUTO_INCREMENT = 1;
INSERT INTO ACCOUNT.GROUPS
  (`NAME`, `DESC`) VALUES
  ('Admins'         ,'Group of application admins'),
  ('Access admins'  ,'Group of access admins'),
  ('Supervisors'    ,'Group of supervisors'),
  ('Users'          ,'Group of application users'),
  ('Viewers'        ,'Group of users with readonly access');
  
DELETE FROM ACCOUNT.ROLES;
ALTER TABLE ACCOUNT.ROLES AUTO_INCREMENT = 1;
INSERT INTO ACCOUNT.ROLES
  (`NAME`, `DESC`) VALUES
  ('Admin'        ,'Admin role'),
  ('Access admin' ,'Access admin role'),
  ('Supervisor'   ,'Supervisor role'),
  ('Readonly'     ,'Read only role');
  
DELETE FROM ACCOUNT.MODULES;
ALTER TABLE ACCOUNT.MODULES AUTO_INCREMENT = 1;
INSERT INTO ACCOUNT.MODULES
  (`NAME`, `DESC`) VALUES
  ('DICTADM'   ,'Dictionary admin module'),
  ('CFGADM'    ,'Application configuration module'),
  ('SECADM'    ,'Security admin module'),
  ('SUPERVISE' ,'Extended data access module'),
  ('READONLY'  ,'Read only access restriction module');
  
DELETE FROM ACCOUNT.USER_GROUPS;
ALTER TABLE ACCOUNT.USER_GROUPS AUTO_INCREMENT = 1;
INSERT INTO ACCOUNT.USER_GROUPS
  (ID_USER, ID_GROUP) VALUES
  (1, 1),
  (2, 2),
  (3, 3),
  (4, 4),
  (5, 5);
  
DELETE FROM ACCOUNT.GROUP_ROLES;
ALTER TABLE ACCOUNT.GROUP_ROLES AUTO_INCREMENT = 1;
INSERT INTO ACCOUNT.GROUP_ROLES
  (ID_GROUP, ID_ROLE) VALUES
  (1, 1),
  (1, 2),
  (1, 3),
  (2, 2),
  (3, 3),
  (5, 4);
  
DELETE FROM ACCOUNT.ROLE_MODULES;
ALTER TABLE ACCOUNT.ROLE_MODULES AUTO_INCREMENT = 1;
INSERT INTO ACCOUNT.ROLE_MODULES
  (ID_ROLE, ID_MODULE) VALUES
  (1, 1),
  (1, 2),
  (2, 3),
  (3, 4),
  (4, 5);

DELETE FROM ACCOUNT.USER_RAIONS;
ALTER TABLE ACCOUNT.USER_RAIONS AUTO_INCREMENT = 1;
INSERT INTO ACCOUNT.USER_RAIONS
  (ID_USER, ID_RAION) VALUES
  (4, 17);