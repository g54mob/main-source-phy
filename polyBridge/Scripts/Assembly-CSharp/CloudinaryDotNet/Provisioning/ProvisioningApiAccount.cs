using System;

namespace CloudinaryDotNet.Provisioning
{
	public class ProvisioningApiAccount
	{
		private const string CloudinaryAccountUrl = "CLOUDINARY_ACCOUNT_URL";

		public string AccountId { get; set; }

		public string ProvisioningApiKey { get; set; }

		public string ProvisioningApiSecret { get; set; }

		public ProvisioningApiAccount()
			: this(Environment.GetEnvironmentVariable("CLOUDINARY_ACCOUNT_URL"))
		{
		}

		public ProvisioningApiAccount(string accountUrl)
		{
			if (!string.IsNullOrEmpty(accountUrl))
			{
				Uri uri = new Uri(accountUrl);
				AccountId = uri.Host;
				string[] array = uri.UserInfo.Split(':');
				ProvisioningApiKey = array[0];
				ProvisioningApiSecret = array[1];
			}
		}

		public ProvisioningApiAccount(string accountId, string provisioningApiKey, string provisioningApiSecret)
		{
			AccountId = accountId;
			ProvisioningApiKey = provisioningApiKey;
			ProvisioningApiSecret = provisioningApiSecret;
		}
	}
}
