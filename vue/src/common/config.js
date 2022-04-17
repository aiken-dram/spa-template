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
  AuthActions: "AuthActions",
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
