using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CloudinaryDotNet.Actions
{
	public class StreamingProfileBaseParams : BaseParams
	{
		public string DisplayName { get; set; }

		public List<Representation> Representations { get; set; }

		public override void Check()
		{
			if (Representations == null || !Representations.Any())
			{
				throw new ArgumentException("Representations field must be specified and not empty");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			if (!string.IsNullOrEmpty(DisplayName))
			{
				sortedDictionary.Add("display_name", DisplayName);
			}
			if (Representations != null)
			{
				sortedDictionary.Add("representations", JsonConvert.SerializeObject(Representations, new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore
				}));
			}
			return sortedDictionary;
		}
	}
}
