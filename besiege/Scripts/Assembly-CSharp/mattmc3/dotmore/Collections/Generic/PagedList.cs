using System.Collections.Generic;
using System.Linq;

namespace mattmc3.dotmore.Collections.Generic
{
	public class PagedList<T> : List<T>, IPagedList
	{
		public int TotalCount { get; set; }

		public int TotalPages { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		public bool HasPreviousPage
		{
			get
			{
				return PageIndex > 0;
			}
		}

		public bool HasNextPage
		{
			get
			{
				return PageIndex * PageSize <= TotalCount;
			}
		}

		public PagedList(IEnumerable<T> source, int index, int pageSize)
		{
			int num = (TotalCount = source.Count());
			TotalPages = num / pageSize;
			if (num % pageSize > 0)
			{
				TotalPages++;
			}
			PageSize = pageSize;
			PageIndex = index;
			AddRange(source.Skip(index * pageSize).Take(pageSize).ToList());
		}
	}
}
