using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public abstract class MetadataValidationParams : BaseParams
	{
		public MetadataValidationType Type { get; set; }

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			BaseParams.AddParam(dict, "type", ApiShared.GetCloudinaryParam(Type));
		}
	}
}
