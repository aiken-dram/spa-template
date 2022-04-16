--initial database values--
--//DICTIONARY//--
DELETE FROM "DICT"."REQUEST_TYPES";
ALTER SEQUENCE "DICT"."REQUEST_TYPES_ID_TYPE_seq" RESTART WITH 1;
INSERT INTO "DICT"."REQUEST_TYPES" 
  ("TYPE", "DESC") VALUES
  ('USER_EXPORT', 'Export all users to csv');
  
DELETE FROM "DICT"."REQUEST_STATES";
ALTER SEQUENCE "DICT"."REQUEST_STATES_ID_STATE_seq" RESTART WITH 1;
INSERT INTO "DICT"."REQUEST_STATES" 
  ("STATE", "DESC") VALUES
  ('QUEUE'     , 'In queue'),
  ('PROCESSING', 'Processing'),
  ('READY'     , 'Ready'),
  ('DELIVERED' , 'Delivered'),
  ('ERROR'     , 'Error');

DELETE FROM "DICT"."AUTH_ACTIONS";
ALTER SEQUENCE "DICT"."AUTH_ACTIONS_ID_ACTION_seq" RESTART WITH 1;
INSERT INTO "DICT"."AUTH_ACTIONS" 
  ("ACTION", "DESC") VALUES
  ('LOGIN'    , 'Logging into program'),
  ('WRONGPASS', 'Entered wrong password'),
  ('EXPIRED'  , 'Password was expired'),
  ('LOCK'     , 'User was locked');
  
--//ACCOUNT//--
DELETE FROM "ACCOUNT"."USERS";
ALTER SEQUENCE "ACCOUNT"."USERS_ID_USER_seq" RESTART WITH 1;
INSERT INTO "ACCOUNT"."USERS"
  ("LOGIN"      ,"PASS"                            ,"IS_ACTIVE" ,"PASS_DATE"  ,"NAME"              ,"DESC") VALUES
  ('admin'      ,'21232f297a57a5a743894a0e4a801fc3','T'         ,'01.01.2050' ,'Application admin' ,'Application administrator description'),
  ('secadm'     ,'21232f297a57a5a743894a0e4a801fc3','T'         ,'01.01.2050' ,'Account admin'     ,'Application account administrator description'),
  ('supervisor' ,'098f6bcd4621d373cade4e832627b4f6','T'         ,'01.01.2050' ,'Supervisor'        ,'Supervisor description'),
  ('user'       ,'098f6bcd4621d373cade4e832627b4f6','T'         ,'01.01.2050' ,'User'              ,'User description'),
  ('viewer'     ,'098f6bcd4621d373cade4e832627b4f6','T'         ,'01.01.2050' ,'Viewer'            ,'Viewer description');
   
DELETE FROM "ACCOUNT"."GROUPS";
ALTER SEQUENCE "ACCOUNT"."GROUPS_ID_GROUP_seq" RESTART WITH 1;
INSERT INTO "ACCOUNT"."GROUPS"
  ("NAME", "DESC") VALUES
  ('Administrators'         ,'Application administrators'),
  ('Account administrators' ,'Application account administrators'),
  ('Supervisors'            ,'Users with extended access to data'),
  ('Users'                  ,'Application users'),
  ('Viewers'                ,'Users with restricted access to read only');
  
DELETE FROM "ACCOUNT"."ROLES";
ALTER SEQUENCE "ACCOUNT"."ROLES_ID_ROLE_seq" RESTART WITH 1;
INSERT INTO "ACCOUNT"."ROLES"
  ("NAME", "DESC") VALUES
  ('Application admin'       ,'Full access to application'),
  ('Access admin'            ,'Access to account administration'),
  ('Extended data access'    ,'Extended access to data'),
  ('Restricted to view only' ,'Restricted access to read only');
  
DELETE FROM "ACCOUNT"."MODULES";
ALTER SEQUENCE "ACCOUNT"."MODULES_ID_MODULE_seq" RESTART WITH 1;
INSERT INTO "ACCOUNT"."MODULES"
  ("NAME", "DESC") VALUES
  ('DICTADM'   ,'Access to dictionary administration'),
  ('CFGADM'    ,'Access to configuration administration'),
  ('SECADM'    ,'Access to account administration'),
  ('SUPERVISE' ,'Extended data access'),
  ('READONLY'  ,'Restricted access to read only');
  
DELETE FROM "ACCOUNT"."USER_GROUPS";
INSERT INTO "ACCOUNT"."USER_GROUPS"
  ("ID_USER", "ID_GROUP") VALUES
  (1, 1),
  (2, 2),
  (3, 3),
  (4, 4),
  (5, 5);
  
DELETE FROM "ACCOUNT"."GROUP_ROLES";
INSERT INTO "ACCOUNT"."GROUP_ROLES"
  ("ID_GROUP", "ID_ROLE") VALUES
  (1, 1),
  (1, 2),
  (1, 3),
  (2, 2),
  (3, 3),
  (5, 4);
  
DELETE FROM "ACCOUNT"."ROLE_MODULES";
INSERT INTO "ACCOUNT"."ROLE_MODULES"
  ("ID_ROLE", "ID_MODULE") VALUES
  (1, 1),
  (1, 2),
  (2, 3),
  (3, 4),
  (4, 5);