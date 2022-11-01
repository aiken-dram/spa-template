--//DICTIONARY//--
DELETE FROM [DICT].[DISTRICTS];
INSERT INTO [DICT].[DISTRICTS]
  ([IdDistrict], [Name]) VALUES
  (1 ,'District A'),
  (2 ,'District B'),
  (3 ,'District C');
  
DELETE FROM [DICT].[REQUEST_TYPES];
INSERT INTO [DICT].[REQUEST_TYPES] 
  ([IdType], [Type], [Desc]) VALUES
  (1, 'TABLE_EXPORT_AUDIT',  'Export audit table'),
  (2, 'TABLE_EXPORT_SAMPLE', 'Export sample table'),
  (3, 'RSCRIPT',             'Execute R script');
  
DELETE FROM [DICT].[REQUEST_STATES];
INSERT INTO [DICT].[REQUEST_STATES] 
  ([IdState], [State], [Desc]) VALUES
  (1, 'QUEUE'     , 'In queue'),
  (2, 'PROCESSING', 'Processing'),
  (3, 'READY'     , 'Ready'),
  (4, 'DELIVERED' , 'Delivered'),
  (5, 'ERROR'     , 'Error');

DELETE FROM [DICT].[AUDIT_ACTIONS];
INSERT INTO [DICT].[AUDIT_ACTIONS] 
  ([IdAction], [Action], [Desc]) VALUES
  (1, 'CREATE'            ,'Create'),
  (2, 'EDIT'              ,'Edit'),
  (3, 'DELETE'            ,'Delete'),
  (4, 'AUTH_LOGIN'        ,'Logging into program'),
  (5, 'AUTH_WRONGPASS'    ,'Wrong password'),
  (6, 'AUTH_EXPIRED'      ,'Password expired'),
  (7, 'AUTH_LOCK'         ,'User blocked'),
  (8, 'USER_UPDATE_PASS'  ,'Update password from file'),
  (9, 'MQ_REQUEST'        ,'Create new request'),
  (10,'MQ_ERROR'          ,'Error while processing request'),
  (11,'MQ_READY'          ,'Request successfully processed'),
  (12,'MQ_RECEIVED'       ,'Request result was received by user');
  
DELETE FROM [DICT].[AUDIT_TARGETS];
INSERT INTO [DICT].[AUDIT_TARGETS] 
  ([IdTarget], [Target], [Desc]) VALUES
  (1, 'AUTH'          ,'Authorization'),
  (2, 'ACCOUNT.USERS' ,'Access management'),
  (3, 'MQ.REQUESTS'   ,'Requests'),
  (4, 'DICT.DISTRICTS','District dictionary'),
  (5, 'R.RSCRIPT'     ,'R script'),
  (6, 'R.RSCRIPT_TREE','Statistics menu tree');

DELETE FROM [DICT].[AUDIT_DATA_TYPES];
INSERT INTO [DICT].[AUDIT_DATA_TYPES] 
  ([IdType], [Type], [Desc]) VALUES
  (1, 'VALUE'         ,'Value'),
  (2, 'FIELD_VALUE'   ,'Field and value'),
  (3, 'FIELD_OLD_NEW' ,'Field, old and new value'),
  (4, 'FIELD_OPERATION_VALUE' ,'Field, operation and value');
  
DELETE FROM [DICT].[RSCRIPT_PARAM_TYPES];
INSERT INTO [DICT].[RSCRIPT_PARAM_TYPES]
  ([IdType], [Type], [Desc]) VALUES
  (1, 'DICT.DISTRICTS', 'District dictionary');
  
--//ACCOUNT//--
DELETE FROM [ACCOUNT].[USERS];
--RESET IDENTITY--
--DBCC CHECKIDENT(‘tableName’, RESEED, 0)
INSERT INTO [ACCOUNT].[USERS]
  ([Login]     ,[Pass]                            ,[IsActive],[PassDate]   ,[Name]               ,[Desc]) VALUES
  ('admin'     ,'21232f297a57a5a743894a0e4a801fc3','T'       ,'01.01.2050' ,'Application admin'  ,'Application admin'),
  ('secadm'    ,'21232f297a57a5a743894a0e4a801fc3','T'       ,'01.01.2050' ,'Access admin'       ,'Access admin'),
  ('test-super','098f6bcd4621d373cade4e832627b4f6','T'       ,'01.01.2050' ,'Supervisor'         ,'Supervisor'),
  ('test-2'    ,'098f6bcd4621d373cade4e832627b4f6','T'       ,'01.01.2050' ,'User in district B' ,'District B user'),
  ('test-view' ,'098f6bcd4621d373cade4e832627b4f6','T'       ,'01.01.2050' ,'Viewer'             ,'Viewer');
   
DELETE FROM [ACCOUNT].[GROUPS];
--RESET IDENTITY--
INSERT INTO [ACCOUNT].[GROUPS]
  ([Name], [Desc]) VALUES
  ('Application admins' ,'Application admins'),
  ('Access admins'      ,'Access admins'),
  ('Supervisors'        ,'Supervisors'),
  ('Users'              ,'Users'),
  ('Viewers'            ,'Viewers');
  
DELETE FROM [ACCOUNT].[ROLES];
--RESET IDENTITY--
INSERT INTO [ACCOUNT].[ROLES]
  ([Name], [Desc]) VALUES
  ('Application admin' ,'Application admin'),
  ('Access admin'      ,'Роль администратора доступа к приложению'),
  ('Supervisor'        ,'Extended access to application'),
  ('Readonly'          ,'Limit access to read-only');
  
DELETE FROM [ACCOUNT].[MODULES];
--RESET IDENTITY--
INSERT INTO [ACCOUNT].[MODULES]
  ([Name], [Desc]) VALUES
  ('DICTADM'   ,'Dictionaries admin'),
  ('CFGADM'    ,'Configuration admin'),
  ('SECADM'    ,'Access admin'),
  ('SUPERVISE' ,'Extended access'),
  ('READONLY'  ,'Read-only access');
  
DELETE FROM [ACCOUNT].[USER_GROUPS];
--RESET IDENTITY--
INSERT INTO [ACCOUNT].[USER_GROUPS]
  ([IdUser], [IdGroup]) VALUES
  (1, 1),
  (2, 2),
  (3, 3),
  (4, 4),
  (5, 5);
  
DELETE FROM [ACCOUNT].[GROUP_ROLES];
INSERT INTO [ACCOUNT].[GROUP_ROLES]
  ([IdGroup], [IdRole]) VALUES
  (1, 1),
  (1, 2),
  (1, 3),
  (2, 2),
  (3, 3),
  (5, 4);
  
DELETE FROM [ACCOUNT].[ROLE_MODULES];
INSERT INTO [ACCOUNT].[ROLE_MODULES]
  ([IdRole], [IdModule]) VALUES
  (1, 1),
  (1, 2),
  (2, 3),
  (3, 4),
  (4, 5);

DELETE FROM [ACCOUNT].[USER_DISTRICTS];
INSERT INTO [ACCOUNT].[USER_DISTRICTS]
  ([IdUser], [IdDistrict]) VALUES
  (4, 2);


-- SAMPLE --
INSERT INTO [DICT].[AUDIT_ACTIONS] 
  ([IdAction], [Action], [Desc]) VALUES
  (13, 'SAMPLE_BATCH_UPDATE', 'Batch update samples');

INSERT INTO [DICT].[AUDIT_TARGETS] 
  ([IdTarget], [Target], [Desc]) VALUES
  (7, 'DICT.SAMPLE_DICTS' ,'Sample dictionary'),
  (8, 'SAMPLE.SAMPLE'     ,'Sample');
  
DELETE FROM [R].[RSCRIPTS];
--
INSERT INTO [R].[RSCRIPTS]
  ([ScriptFile], [Name], [ContentType], [ResultFile], [Desc]) VALUES
  ('test1.r', 'Sample 1', 'csv', 'Sample table for district {0}', 'Sample R statistics in table form'),
  ('test2.r', 'Sample 2', 'image', 'Sample plot', 'Sample R statistics in plot form');
  
DELETE FROM [R].[RSCRIPT_PARAMS];
INSERT INTO [R].[RSCRIPT_PARAMS]
  ([IdRScript], [IdType], [Name], [Hint], [Desc]) VALUES
  (1, 1, 'District', 'Choose district from list', 'District from dictionary');
  
DELETE FROM [R].[RSCRIPT_TREE];
--
INSERT INTO [R].[RSCRIPT_TREE]
  ([IdParent], [IdRScript], [Name], [Modules], [Icon], [Color], [Desc]) VALUES
  (NULL, NULL, 'Tables', NULL, 'fa-table', 'primary', 'Catalog'),
  (NULL, NULL, 'Plots', NULL, 'fa-bar-chart', 'primary', 'Catalog'),
  (1, NULL, 'Samples', NULL, NULL, '', 'Subcatalog'),
  (3, 1, 'Sample №1', NULL, 'fa-file-csv', 'success', 'Statistic form 1'),
  (2, 2, 'Sample №2', NULL, 'fa-file-image', 'warning', 'Statistic form 2');

DELETE FROM [DICT].[SAMPLE_DICTS];
--
INSERT INTO [DICT].[SAMPLE_DICTS]
  ([Dict], [Desc]) VALUES
  ('DICT_1','Sample dictionary 1');
  
DELETE FROM [DICT].[SAMPLE_TYPES];
--
INSERT INTO [DICT].[SAMPLE_TYPES]
  ([IdType], [Type], [Desc]) VALUES
  (1, 'TYPE_A', 'Sample type A'),
  (2, 'TYPE_B', 'Sample type B');
  
DELETE FROM [SAMPLE].[SAMPLE];
--
INSERT INTO [SAMPLE].[SAMPLE]
  ([IdDistrict], [IdType], [IdDict], [Text], [Number], [Date], [Timestamp], [Sum]) VALUES
  (1, 1, 1, 'Text', 24, '01.01.2001', CURRENT_TIMESTAMP, 123.32);
  
DELETE FROM [SAMPLE].[SAMPLE_CHILD];
INSERT INTO [SAMPLE].[SAMPLE_CHILD]
  ([IdSample], [Text]) VALUES
  (1, 'First child'),
  (1, 'Second child');

 
