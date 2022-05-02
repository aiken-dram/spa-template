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
  EventTargets: "EventTargets",
  EventActions: "EventActions",
  Districts: "Districts",
  UserDistricts: "UserDistricts",
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
  ProcessFile: "Account.User.Commands.ProcessFile",
};
