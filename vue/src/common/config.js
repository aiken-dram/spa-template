/** API URL */
export const API_URL = "/api/";

/** Modules for access restrictions */
export const MODULES = {
  SecurityAdmin: "SECADM",
  DictionaryAdmin: "DICTADM",
  ConfigurationAdmin: "CFGADM",
  Supervisor: "SUPERVISE",
  Readonly: "READONLY",
};

/** Dictionaries in store */
export const DICT = {
  AccessGroups: "AccessGroups",
  AccessRoles: "AccessRoles",
  AuditTargets: "AuditTargets",
  AuditActions: "AuditActions",
  RScriptParamTypes: "RScriptParamTypes",
  Districts: "Districts",
  UserDistricts: "UserDistricts",
  SampleTypes: "SampleTypes",
  SampleDicts: "SampleDicts",
};

/** Documentation links */
export const HELP = {
  root: "/../doc",
  routes: [
    {
      route: null,
      name: "common.userManual",
      href: "/manuals/user.html",
    },
  ],
};

/**
 * SignalR hub subjects for notifications
 */
export const SUBJECTS = {
  UserProcessFile: "Account.User.Commands.ProcessFile",
  MessageQuery: "MessageQuery",
  BatchProcessRequest: "MessageQuery.Commands.BatchProcessRequest",
  BatchUpdateSample: "Sample.Commands.BatchUpdateSample",
};

/**
 * Message query types
 */
export const MQ = {
  TableExportAudit: "TABLE_EXPORT_AUDIT",
  TableExportSample: "TABLE_EXPORT_SAMPLE",
  RScript: "RSCRIPT",
};
