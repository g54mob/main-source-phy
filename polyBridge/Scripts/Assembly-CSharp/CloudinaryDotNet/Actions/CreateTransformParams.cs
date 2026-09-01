using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class CreateTransformParams : BaseParams
	{
		public string Name { get; set; }

		public Transformation Transform { get; set; }

		public string Format { get; set; }

		public override void Check()
		{
			if (string.IsNullOrEmpty(Name))
			{
				throw new ArgumentException("Name must be set!");
			}
			if (Transform == null)
			{
				throw new ArgumentException("Transform must be defined!");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			string text = Transform.Generate();
			if (Format != null)
			{
				text = text + "/" + Format;
			}
			sortedDictionary.Add("transformation", text);
			sortedDictionary.Add("name", Name);
			return sortedDictionary;
		}
	}
}
