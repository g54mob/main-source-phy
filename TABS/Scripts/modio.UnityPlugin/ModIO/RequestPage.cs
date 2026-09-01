using System;
using Newtonsoft.Json;
using UnityEngine;

namespace ModIO
{
	[Serializable]
	[JsonObject]
	public class RequestPage<T>
	{
		[JsonProperty("result_limit")]
		public int size;

		[JsonProperty("result_offset")]
		public int resultOffset;

		[JsonProperty("result_total")]
		public int resultTotal;

		[JsonProperty("data")]
		public T[] items;

		public int CalculatePageCount()
		{
			if (size > 0)
			{
				return Mathf.CeilToInt((float)resultTotal / (float)size);
			}
			return -1;
		}

		public int CalculatePageIndex()
		{
			if (size > 0)
			{
				return resultOffset / size;
			}
			return -1;
		}
	}
}
