using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class UpdateTransformParams : BaseParams
	{
		public string Transformation { get; set; }

		[Obsolete("Property UnsafeTransform is deprecated, please use UnsafeUpdate instead")]
		public Transformation UnsafeTransform
		{
			get
			{
				return UnsafeUpdate;
			}
			set
			{
				UnsafeUpdate = value;
			}
		}

		public Transformation UnsafeUpdate { get; set; }

		[Obsolete("Property Strict is deprecated, please use AllowedForStrict instead")]
		public bool Strict
		{
			get
			{
				return AllowedForStrict;
			}
			set
			{
				AllowedForStrict = value;
			}
		}

		public bool AllowedForStrict { get; set; }

		public UpdateTransformParams()
		{
			Transformation = string.Empty;
		}

		public override void Check()
		{
			if (string.IsNullOrEmpty(Transformation))
			{
				throw new ArgumentException("Transformation must be set!");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "allowed_for_strict", AllowedForStrict);
			BaseParams.AddParam(sortedDictionary, "unsigned", "true");
			BaseParams.AddParam(sortedDictionary, "removeUnsignedParam", "true");
			if (UnsafeUpdate != null)
			{
				BaseParams.AddParam(sortedDictionary, "unsafe_update", UnsafeUpdate.Generate());
			}
			if (!string.IsNullOrEmpty(Transformation))
			{
				BaseParams.AddParam(sortedDictionary, "transformation", Transformation);
			}
			return sortedDictionary;
		}
	}
}
