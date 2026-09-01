namespace CloudinaryDotNet
{
	public class Account
	{
		public string Cloud { get; set; }

		public string ApiKey { get; set; }

		public string ApiSecret { get; set; }

		public Account()
		{
			Cloud = CloudinaryConfiguration.CloudName;
			ApiKey = CloudinaryConfiguration.ApiKey;
			ApiSecret = CloudinaryConfiguration.ApiSecret;
		}

		public Account(string cloud, string apiKey, string apiSecret)
		{
			Cloud = cloud;
			ApiKey = apiKey;
			ApiSecret = apiSecret;
		}

		public Account(string cloud)
		{
			Cloud = cloud;
		}
	}
}
