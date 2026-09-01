using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CloudinaryDotNet
{
	public class FetchLayer : BaseLayer<FetchLayer>
	{
		protected string m_url;

		public FetchLayer()
		{
			m_resourceType = "fetch";
		}

		public FetchLayer Url(string url)
		{
			m_url = UrlEncode(url);
			return this;
		}

		public override string AdditionalParams()
		{
			if (string.IsNullOrEmpty(m_url))
			{
				throw new ArgumentException("Must supply url.");
			}
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(m_url))
			{
				list.Add(string.Format(CultureInfo.InvariantCulture, "fetch:{0}", m_url));
			}
			return string.Join(":", list.ToArray());
		}

		public override string ToString()
		{
			return AdditionalParams();
		}

		private static string UrlEncode(string url)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(url));
		}
	}
}
