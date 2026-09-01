using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CloudinaryDotNet
{
	public class Api : ApiShared
	{
		public Api()
			: base(Environment.GetEnvironmentVariable("CLOUDINARY_URL"))
		{
		}

		public Api(string cloudinaryUrl)
			: base(cloudinaryUrl)
		{
		}

		public Api(Account account, bool usePrivateCdn, string privateCdn, bool shortenUrl, bool cSubDomain)
			: base(account, usePrivateCdn, privateCdn, shortenUrl, cSubDomain)
		{
		}

		public Api(Account account)
			: base(account)
		{
		}

		public override string BuildCallbackUrl(string path = "")
		{
			if (!Regex.IsMatch(CultureInfo.InvariantCulture.TextInfo.ToLower(path), "^https?:/.*"))
			{
				throw new ArgumentException("Provide an absolute path to file!");
			}
			return path;
		}
	}
}
