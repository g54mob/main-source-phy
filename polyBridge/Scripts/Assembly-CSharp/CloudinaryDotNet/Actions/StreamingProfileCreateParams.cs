using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class StreamingProfileCreateParams : StreamingProfileBaseParams
	{
		public string Name { get; set; }

		public override void Check()
		{
			if (string.IsNullOrEmpty(Name))
			{
				throw new ArgumentException("Name field must be specified");
			}
			base.Check();
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			sortedDictionary.Add("name", Name);
			return sortedDictionary;
		}
	}
}
