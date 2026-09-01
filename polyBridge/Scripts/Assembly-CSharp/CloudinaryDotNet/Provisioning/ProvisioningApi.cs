using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;

namespace CloudinaryDotNet.Provisioning
{
	public class ProvisioningApi : ApiShared
	{
		public ProvisioningApiAccount ProvisioningApiAccount { get; private set; }

		public Url AccountApiUrlV => new Url("provisioning").CloudinaryAddr(m_apiAddr).ApiVersion("v1_1").Add("accounts")
			.Add(ProvisioningApiAccount.AccountId);

		public ProvisioningApi()
		{
			ProvisioningApiAccount = new ProvisioningApiAccount();
		}

		internal Task<T> CallAccountApiAsync<T>(HttpMethod method, string url, BaseParams parameters, FileDescription file, Dictionary<string, string> extraHeaders = null, CancellationToken? cancellationToken = null) where T : BaseResult, new()
		{
			ValidateAccountApiCredentials();
			parameters?.Check();
			SortedDictionary<string, object> parameters2 = ((method != HttpMethod.PUT && method != HttpMethod.POST) ? null : parameters?.ToParamsDictionary());
			return CallAndParseAsync<T>(method, url, parameters2, file, extraHeaders, cancellationToken);
		}

		internal T CallAccountApi<T>(HttpMethod method, string url, BaseParams parameters, FileDescription file, Dictionary<string, string> extraHeaders = null) where T : BaseResult, new()
		{
			ValidateAccountApiCredentials();
			parameters?.Check();
			return CallAndParse<T>(method, url, (method != HttpMethod.PUT && method != HttpMethod.POST) ? null : parameters?.ToParamsDictionary(), file, extraHeaders);
		}

		protected override string GetApiCredentials()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", ProvisioningApiAccount.ProvisioningApiKey, ProvisioningApiAccount.ProvisioningApiSecret);
		}

		private void ValidateAccountApiCredentials()
		{
			Utils.ShouldNotBeEmpty(() => ProvisioningApiAccount.ProvisioningApiKey, "for account provisioning API cannot be null");
			Utils.ShouldNotBeEmpty(() => ProvisioningApiAccount.ProvisioningApiSecret, "for account provisioning API cannot be null");
		}
	}
}
