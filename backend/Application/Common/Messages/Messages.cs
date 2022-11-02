using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common
{
    /// <summary>
    /// Static strings for application
    /// </summary>
    public static class Messages
    {
        #region SHARED
        public static string MaximumLength(int length) => $"Maximum length is {length} letters";
        public static string MustNotBeEmpty(string field) => $"{field} cannot be empty";
        #endregion

        #region ACCOUNT
        public static string UserPasswordUpdated(string login) => $"Password updated for user <code>{login}</code>";
        public static string FoundMultipleUsersWithLogin(string login, int cnt) => $"Found {cnt} users with login <code>{login}</code>";
        public static string InvalidFormatLine(string line) => $"Invalid string format: <code>{line}</code>";
        public const string NotMatchingUserId = "Identity of user does not match authorized one";
        public const string UserWithProvidedLoginAlreadyExists = "User with provided login already exists";
        public const string Login = "Login";
        public const string Password = "Password";
        public const string UserName = "User's name";
        public static string UserTableFileName(DateTime d) => $"List of users {d:yyyy-MM-dd}.csv";
        public const string NoAccessToAuditUsers = "Access to user's audit denied";
        public const string UserGroupNotInDictionary = "Group not found in dictionary";
        public const string UserRoleNotInDictionary = "Role not found in dictionary";
        public const string UserDistrictNotInDictionary = "Dictrict not found in dictionary";
        #endregion

        #region COMMON
        public const string LogRequest = "Request: {0} {1} {2}"; //DEBUG
        public const string LongRunningRequest = "Long Running Request: {0} ({1} milliseconds) {2} {3}"; //DEBUG
        public const string IterationHasNotBeenSet = "Iteration has not been set";
        public const string ConnectionHasNotBeenSet = "IdConnection has not been set";
        public const string CharBooleanTrue = "Yes";
        public const string CharBooleanFalse = "No";
        #endregion

        #region DICTIONARY
        public static string DictionaryNotFound(string dict) => $"Requested dictionary not found: '{dict}'.";
        public const string Name = "Name";
        #endregion

        #region MESSAGE QUERY
        public const string RequestQueueOverflow = "Limit for maximum allowed requests in the queue for user has been reached. Wait or cancel some of the created requests before creating new ones.";
        public const string NoAccessToDeleteRequest = "You dont have access to delete this request";
        public const string RequestHasNotBeenProcessed = "Request has not been processed";
        public const string RequestHasNoGuid = "Missing file with result of request";
        public const string RequestNotFound = "Request not found";
        public static string AuditTableFileName(DateTime? d) => $"List of audit {d:yyyy-MM-dd}.csv";
        public static string SampleTableFileName(DateTime? d) => $"List of samples {d:yyyy-MM-dd}.csv";
        public const string RequestScriptFile = "Script's file name";
        public const string RequestName = "Name of script";
        public const string RequestContentType = "Type of script result content";
        public const string RequestResultFile = "Script result file name";
        public const string RequestDeleted = "Request deleted";
        #endregion

        #region R SCRIPT
        public const string CanOnlyDeleteEmptyBranches = "Can only delete empty branches. Delete all children before deleting parent node";
        public const string MissingParentRScriptTreeNode = "Missing parent node with selected identity";
        #endregion

        #region SAMPLE
        public static string SampleBatchUpdated(string? name, string field, string target) => $"Sample '{name}' had field {field} replaced with {target}";
        public static string SampleIdNotFound(long id) => $"Couldnt find sample with identity {id}";
        public const string SampleBatchUpdateActionNotSupported = "Action not supported";
        #endregion
    }
}