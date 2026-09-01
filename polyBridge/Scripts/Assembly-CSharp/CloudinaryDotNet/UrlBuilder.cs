using System;
using System.Collections.Generic;

namespace CloudinaryDotNet
{
	public class UrlBuilder : UriBuilder
	{
		private StringDictionary queryString;

		public StringDictionary QueryString
		{
			get
			{
				if (queryString == null)
				{
					queryString = new StringDictionary();
				}
				return queryString;
			}
		}

		public string PageName
		{
			get
			{
				string path = base.Path;
				return path.Substring(path.LastIndexOf("/", StringComparison.Ordinal) + 1);
			}
			set
			{
				string path = base.Path;
				path = path.Substring(0, path.LastIndexOf("/", StringComparison.Ordinal));
				base.Path = path + "/" + value;
			}
		}

		public UrlBuilder()
		{
		}

		public UrlBuilder(string uri)
			: base(uri)
		{
			PopulateQueryString();
		}

		public UrlBuilder(string uri, IDictionary<string, object> @params)
			: base(uri)
		{
			PopulateQueryString();
			SetParameters(@params);
		}

		public UrlBuilder(Uri uri)
			: base(uri)
		{
			PopulateQueryString();
		}

		public UrlBuilder(string schemeName, string hostName)
			: base(schemeName, hostName)
		{
		}

		public UrlBuilder(string scheme, string host, int portNumber)
			: base(scheme, host, portNumber)
		{
		}

		public UrlBuilder(string scheme, string host, int port, string pathValue)
			: base(scheme, host, port, pathValue)
		{
		}

		public UrlBuilder(string scheme, string host, int port, string path, string extraValue)
			: base(scheme, host, port, path, extraValue)
		{
		}

		public void SetParameters(IDictionary<string, object> @params)
		{
			foreach (KeyValuePair<string, object> param in @params)
			{
				if (param.Value is IEnumerable<string>)
				{
					foreach (string item in (IEnumerable<string>)param.Value)
					{
						QueryString.Add(param.Key + "[]", item);
					}
				}
				else
				{
					QueryString[param.Key] = param.Value.ToString();
				}
			}
		}

		public new string ToString()
		{
			BuildQueryString();
			return base.Uri.AbsoluteUri;
		}

		private void PopulateQueryString()
		{
			string query = base.Query;
			if (!string.IsNullOrEmpty(query))
			{
				if (queryString == null)
				{
					queryString = new StringDictionary();
				}
				queryString.Clear();
				query = query.Substring(1);
				string[] array = query.Split(new char[1] { '&' });
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[1] { '=' });
					queryString[array2[0]] = ((array2.Length > 1) ? array2[1] : string.Empty);
				}
			}
		}

		private void BuildQueryString()
		{
			if (queryString == null)
			{
				return;
			}
			int count = queryString.Count;
			if (count == 0)
			{
				base.Query = string.Empty;
				return;
			}
			string[] array = new string[count];
			string[] array2 = new string[count];
			string[] array3 = new string[count];
			queryString.Keys.CopyTo(array, 0);
			queryString.Values.CopyTo(array2, 0);
			for (int i = 0; i < count; i++)
			{
				array3[i] = array[i] + "=" + array2[i];
			}
			base.Query = string.Join("&", array3);
		}
	}
}
