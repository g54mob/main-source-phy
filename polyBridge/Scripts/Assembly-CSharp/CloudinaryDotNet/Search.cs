using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;

namespace CloudinaryDotNet
{
	public class Search
	{
		private List<Dictionary<string, object>> sortByParam;

		private List<string> aggregateParam;

		private List<string> withFieldParam;

		private Dictionary<string, object> searchParams;

		private ApiShared m_api;

		private Url SearchResourcesUrl => m_api?.ApiUrlV?.Add("resources").Add("search");

		public Search(ApiShared api)
		{
			m_api = api;
			searchParams = new Dictionary<string, object>();
			sortByParam = new List<Dictionary<string, object>>();
			aggregateParam = new List<string>();
			withFieldParam = new List<string>();
		}

		public Search Expression(string value)
		{
			searchParams.Add("expression", value);
			return this;
		}

		public Search MaxResults(int value)
		{
			searchParams.Add("max_results", value);
			return this;
		}

		public Search NextCursor(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				searchParams.Add("next_cursor", value);
			}
			return this;
		}

		public Search Direction(string value)
		{
			searchParams.Add("direction", value);
			return this;
		}

		public Search Aggregate(string field)
		{
			aggregateParam.Add(field);
			return this;
		}

		public Search WithField(string field)
		{
			withFieldParam.Add(field);
			return this;
		}

		public Search SortBy(string field, string dir)
		{
			if (string.IsNullOrEmpty(field))
			{
				return this;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add(field, dir);
			sortByParam.Add(dictionary);
			return this;
		}

		public Dictionary<string, object> ToQuery()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(searchParams);
			if (withFieldParam.Count > 0)
			{
				dictionary.Add("with_field", withFieldParam.Distinct());
			}
			if (sortByParam.Count > 0)
			{
				dictionary.Add("sort_by", from d in sortByParam
					group d by d.Keys.First() into l
					select l.Last());
			}
			if (aggregateParam.Count > 0)
			{
				dictionary.Add("aggregate", aggregateParam.Distinct());
			}
			return dictionary;
		}

		public SearchResult Execute()
		{
			return m_api.CallAndParse<SearchResult>(HttpMethod.POST, SearchResourcesUrl.BuildUrl(), PrepareSearchParams(), null, Utils.PrepareJsonHeaders());
		}

		public Task<SearchResult> ExecuteAsync(CancellationToken? cancellationToken = null)
		{
			return m_api.CallAndParseAsync<SearchResult>(HttpMethod.POST, SearchResourcesUrl.BuildUrl(), PrepareSearchParams(), null, Utils.PrepareJsonHeaders(), cancellationToken);
		}

		private SortedDictionary<string, object> PrepareSearchParams()
		{
			return new SortedDictionary<string, object>(ToQuery())
			{
				{
					"unsigned",
					string.Empty
				},
				{
					"removeUnsignedParam",
					string.Empty
				}
			};
		}
	}
}
