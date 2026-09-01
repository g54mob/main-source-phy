using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;

namespace CloudinaryDotNet.Provisioning
{
	public class AccountProvisioning
	{
		public ProvisioningApi ProvisioningApi { get; } = new ProvisioningApi();

		public SubAccountResult SubAccount(string subAccountId)
		{
			Utils.ShouldNotBeEmpty(() => subAccountId);
			string subAccountsUrl = GetSubAccountsUrl(subAccountId);
			return CallAccountApi<SubAccountResult>(HttpMethod.GET, subAccountsUrl);
		}

		public Task<SubAccountResult> SubAccountAsync(string subAccountId, CancellationToken? cancellationToken = null)
		{
			Utils.ShouldNotBeEmpty(() => subAccountId);
			string subAccountsUrl = GetSubAccountsUrl(subAccountId);
			return CallAccountApiAsync<SubAccountResult>(HttpMethod.GET, subAccountsUrl, cancellationToken);
		}

		public ListSubAccountsResult SubAccounts(ListSubAccountsParams parameters)
		{
			UrlBuilder urlBuilder = new UrlBuilder(GetSubAccountsUrl(), parameters.ToParamsDictionary());
			return CallAccountApi<ListSubAccountsResult>(HttpMethod.GET, urlBuilder.ToString());
		}

		public Task<ListSubAccountsResult> SubAccountsAsync(ListSubAccountsParams parameters, CancellationToken? cancellationToken = null)
		{
			UrlBuilder urlBuilder = new UrlBuilder(GetSubAccountsUrl(), parameters.ToParamsDictionary());
			return CallAccountApiAsync<ListSubAccountsResult>(HttpMethod.GET, urlBuilder.ToString(), cancellationToken);
		}

		public SubAccountResult CreateSubAccount(CreateSubAccountParams parameters)
		{
			string subAccountsUrl = GetSubAccountsUrl();
			return CallAccountApi<SubAccountResult>(HttpMethod.POST, subAccountsUrl, parameters);
		}

		public Task<SubAccountResult> CreateSubAccountAsync(CreateSubAccountParams parameters, CancellationToken? cancellationToken = null)
		{
			string subAccountsUrl = GetSubAccountsUrl();
			return CallAccountApiAsync<SubAccountResult>(HttpMethod.POST, subAccountsUrl, cancellationToken, parameters);
		}

		public SubAccountResult UpdateSubAccount(UpdateSubAccountParams parameters)
		{
			string subAccountsUrl = GetSubAccountsUrl(parameters.SubAccountId);
			return CallAccountApi<SubAccountResult>(HttpMethod.PUT, subAccountsUrl, parameters);
		}

		public Task<SubAccountResult> UpdateSubAccountAsync(UpdateSubAccountParams parameters, CancellationToken? cancellationToken = null)
		{
			string subAccountsUrl = GetSubAccountsUrl(parameters.SubAccountId);
			return CallAccountApiAsync<SubAccountResult>(HttpMethod.PUT, subAccountsUrl, cancellationToken, parameters);
		}

		public DelSubAccountResult DeleteSubAccount(string subAccountId)
		{
			Utils.ShouldNotBeEmpty(() => subAccountId);
			string subAccountsUrl = GetSubAccountsUrl(subAccountId);
			return CallAccountApi<DelSubAccountResult>(HttpMethod.DELETE, subAccountsUrl);
		}

		public Task<DelSubAccountResult> DeleteSubAccountAsync(string subAccountId, CancellationToken? cancellationToken = null)
		{
			Utils.ShouldNotBeEmpty(() => subAccountId);
			string subAccountsUrl = GetSubAccountsUrl(subAccountId);
			return CallAccountApiAsync<DelSubAccountResult>(HttpMethod.DELETE, subAccountsUrl, cancellationToken);
		}

		public UserResult User(string userId)
		{
			Utils.ShouldNotBeEmpty(() => userId);
			string usersUrl = GetUsersUrl(userId);
			return CallAccountApi<UserResult>(HttpMethod.GET, usersUrl);
		}

		public Task<UserResult> UserAsync(string userId, CancellationToken? cancellationToken = null)
		{
			Utils.ShouldNotBeEmpty(() => userId);
			string usersUrl = GetUsersUrl(userId);
			return CallAccountApiAsync<UserResult>(HttpMethod.GET, usersUrl, cancellationToken);
		}

		public ListUsersResult Users(ListUsersParams parameters)
		{
			UrlBuilder urlBuilder = new UrlBuilder(GetUsersUrl(), parameters.ToParamsDictionary());
			return CallAccountApi<ListUsersResult>(HttpMethod.GET, urlBuilder.ToString());
		}

		public Task<ListUsersResult> UsersAsync(ListUsersParams parameters, CancellationToken? cancellationToken = null)
		{
			UrlBuilder urlBuilder = new UrlBuilder(GetUsersUrl(), parameters.ToParamsDictionary());
			return CallAccountApiAsync<ListUsersResult>(HttpMethod.GET, urlBuilder.ToString(), cancellationToken);
		}

		public UserResult CreateUser(CreateUserParams parameters)
		{
			string usersUrl = GetUsersUrl();
			return CallAccountApi<UserResult>(HttpMethod.POST, usersUrl, parameters);
		}

		public Task<UserResult> CreateUserAsync(CreateUserParams parameters, CancellationToken? cancellationToken = null)
		{
			string usersUrl = GetUsersUrl();
			return CallAccountApiAsync<UserResult>(HttpMethod.POST, usersUrl, cancellationToken, parameters);
		}

		public UserResult UpdateUser(UpdateUserParams parameters)
		{
			string usersUrl = GetUsersUrl(parameters.UserId);
			return CallAccountApi<UserResult>(HttpMethod.PUT, usersUrl, parameters);
		}

		public Task<UserResult> UpdateUserAsync(UpdateUserParams parameters, CancellationToken? cancellationToken = null)
		{
			string usersUrl = GetUsersUrl(parameters.UserId);
			return CallAccountApiAsync<UserResult>(HttpMethod.PUT, usersUrl, cancellationToken, parameters);
		}

		public DelUserResult DeleteUser(string userId)
		{
			Utils.ShouldNotBeEmpty(() => userId);
			string usersUrl = GetUsersUrl(userId);
			return CallAccountApi<DelUserResult>(HttpMethod.DELETE, usersUrl);
		}

		public Task<DelUserResult> DeleteUserAsync(string userId, CancellationToken? cancellationToken = null)
		{
			Utils.ShouldNotBeEmpty(() => userId);
			string usersUrl = GetUsersUrl(userId);
			return CallAccountApiAsync<DelUserResult>(HttpMethod.DELETE, usersUrl, cancellationToken);
		}

		public UserGroupResult CreateUserGroup(CreateUserGroupParams parameters)
		{
			string userGroupsUrl = GetUserGroupsUrl();
			return CallAccountApi<UserGroupResult>(HttpMethod.POST, userGroupsUrl, parameters);
		}

		public Task<UserGroupResult> CreateUserGroupAsync(CreateUserGroupParams parameters, CancellationToken? cancellationToken = null)
		{
			string userGroupsUrl = GetUserGroupsUrl();
			return CallAccountApiAsync<UserGroupResult>(HttpMethod.POST, userGroupsUrl, cancellationToken, parameters);
		}

		public UserGroupResult UpdateUserGroup(UpdateUserGroupParams parameters)
		{
			string userGroupsUrl = GetUserGroupsUrl(parameters.UserGroupId);
			return CallAccountApi<UserGroupResult>(HttpMethod.PUT, userGroupsUrl, parameters);
		}

		public Task<UserGroupResult> UpdateUserGroupAsync(UpdateUserGroupParams parameters, CancellationToken? cancellationToken = null)
		{
			string userGroupsUrl = GetUserGroupsUrl(parameters.UserGroupId);
			return CallAccountApiAsync<UserGroupResult>(HttpMethod.PUT, userGroupsUrl, cancellationToken, parameters);
		}

		public DelUserGroupResult DeleteUserGroup(string groupId)
		{
			Utils.ShouldNotBeEmpty(() => groupId);
			string userGroupsUrl = GetUserGroupsUrl(groupId);
			return CallAccountApi<DelUserGroupResult>(HttpMethod.DELETE, userGroupsUrl);
		}

		public Task<DelUserGroupResult> DeleteUserGroupAsync(string groupId, CancellationToken? cancellationToken = null)
		{
			Utils.ShouldNotBeEmpty(() => groupId);
			string userGroupsUrl = GetUserGroupsUrl(groupId);
			return CallAccountApiAsync<DelUserGroupResult>(HttpMethod.DELETE, userGroupsUrl, cancellationToken);
		}

		public UserGroupResult UserGroup(string groupId)
		{
			Utils.ShouldNotBeEmpty(() => groupId);
			string userGroupsUrl = GetUserGroupsUrl(groupId);
			return CallAccountApi<UserGroupResult>(HttpMethod.GET, userGroupsUrl);
		}

		public Task<UserGroupResult> UserGroupAsync(string groupId, CancellationToken? cancellationToken = null)
		{
			Utils.ShouldNotBeEmpty(() => groupId);
			string userGroupsUrl = GetUserGroupsUrl(groupId);
			return CallAccountApiAsync<UserGroupResult>(HttpMethod.GET, userGroupsUrl, cancellationToken);
		}

		public ListUserGroupsResult UserGroups()
		{
			string userGroupsUrl = GetUserGroupsUrl();
			return CallAccountApi<ListUserGroupsResult>(HttpMethod.GET, userGroupsUrl);
		}

		public Task<ListUserGroupsResult> UserGroupsAsync(CancellationToken? cancellationToken = null)
		{
			string userGroupsUrl = GetUserGroupsUrl();
			return CallAccountApiAsync<ListUserGroupsResult>(HttpMethod.GET, userGroupsUrl, cancellationToken);
		}

		public ListUsersResult AddUserToGroup(string groupId, string userId)
		{
			return ChangeUserGroup(groupId, userId, HttpMethod.POST);
		}

		public Task<ListUsersResult> AddUserToGroupAsync(string groupId, string userId, CancellationToken? cancellationToken = null)
		{
			return ChangeUserGroupAsync(groupId, userId, HttpMethod.POST, cancellationToken);
		}

		public ListUsersResult RemoveUserFromGroup(string groupId, string userId)
		{
			return ChangeUserGroup(groupId, userId, HttpMethod.DELETE);
		}

		public Task<ListUsersResult> RemoveUserFromGroupAsync(string groupId, string userId, CancellationToken? cancellationToken = null)
		{
			return ChangeUserGroupAsync(groupId, userId, HttpMethod.DELETE, cancellationToken);
		}

		public ListUsersResult UsersGroupUsers(string groupId)
		{
			Utils.ShouldNotBeEmpty(() => groupId);
			string userGroupsUrlForUsers = GetUserGroupsUrlForUsers(groupId);
			return CallAccountApi<ListUsersResult>(HttpMethod.GET, userGroupsUrlForUsers);
		}

		public Task<ListUsersResult> UsersGroupUsersAsync(string groupId, CancellationToken? cancellationToken = null)
		{
			Utils.ShouldNotBeEmpty(() => groupId);
			string userGroupsUrlForUsers = GetUserGroupsUrlForUsers(groupId);
			return CallAccountApiAsync<ListUsersResult>(HttpMethod.GET, userGroupsUrlForUsers, cancellationToken);
		}

		private static string UrlWithOptionalParameter(Url baseUrl, string urlParameter)
		{
			if (!string.IsNullOrEmpty(urlParameter))
			{
				baseUrl.Add(urlParameter);
			}
			return baseUrl.BuildUrl();
		}

		private ListUsersResult ChangeUserGroup(string groupId, string userId, HttpMethod httpMethod)
		{
			Utils.ShouldNotBeEmpty(() => groupId);
			Utils.ShouldNotBeEmpty(() => userId);
			string userGroupsUrlForUsers = GetUserGroupsUrlForUsers(groupId, userId);
			return CallAccountApi<ListUsersResult>(httpMethod, userGroupsUrlForUsers);
		}

		private Task<ListUsersResult> ChangeUserGroupAsync(string groupId, string userId, HttpMethod httpMethod, CancellationToken? cancellationToken)
		{
			Utils.ShouldNotBeEmpty(() => groupId);
			Utils.ShouldNotBeEmpty(() => userId);
			string userGroupsUrlForUsers = GetUserGroupsUrlForUsers(groupId, userId);
			return CallAccountApiAsync<ListUsersResult>(httpMethod, userGroupsUrlForUsers, cancellationToken);
		}

		private T CallAccountApi<T>(HttpMethod httpMethod, string url, BaseParams parameters = null) where T : BaseResult, new()
		{
			if (httpMethod == HttpMethod.POST || httpMethod == HttpMethod.PUT)
			{
				return ProvisioningApi.CallAccountApi<T>(httpMethod, url, parameters, null, Utils.PrepareJsonHeaders());
			}
			return ProvisioningApi.CallAccountApi<T>(httpMethod, url, parameters, null);
		}

		private Task<T> CallAccountApiAsync<T>(HttpMethod httpMethod, string url, CancellationToken? cancellationToken, BaseParams parameters = null) where T : BaseResult, new()
		{
			if (httpMethod == HttpMethod.POST || httpMethod == HttpMethod.PUT)
			{
				return ProvisioningApi.CallAccountApiAsync<T>(httpMethod, url, parameters, null, Utils.PrepareJsonHeaders(), cancellationToken);
			}
			return ProvisioningApi.CallAccountApiAsync<T>(httpMethod, url, parameters, null, null, cancellationToken);
		}

		private string GetSubAccountsUrl(string subAccountId = null)
		{
			return BuildAccountApiUrl("sub_accounts", subAccountId);
		}

		private string GetUsersUrl(string userId = null)
		{
			return BuildAccountApiUrl("users", userId);
		}

		private string GetUserGroupsUrl(string groupId = null)
		{
			return BuildAccountApiUrl("user_groups", groupId);
		}

		private string GetUserGroupsUrlForUsers(string groupId, string userId = null)
		{
			return UrlWithOptionalParameter(ProvisioningApi.AccountApiUrlV.Add("user_groups").Add(groupId).Add("users"), userId);
		}

		private string BuildAccountApiUrl(string resourceName, string urlParameter)
		{
			return UrlWithOptionalParameter(ProvisioningApi.AccountApiUrlV.Add(resourceName), urlParameter);
		}
	}
}
