using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class MetadataUpdateParams : BaseParams
	{
		public List<string> PublicIds { get; set; } = new List<string>();

		public StringDictionary Metadata { get; set; } = new StringDictionary();

		public ResourceType ResourceType { get; set; }

		public string Type { get; set; }

		public MetadataUpdateParams()
		{
			Type = "upload";
			ResourceType = ResourceType.Image;
		}

		public override void Check()
		{
			Utils.ShouldNotBeEmpty(() => PublicIds);
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			BaseParams.AddParam(dict, "public_ids", PublicIds);
			BaseParams.AddParam(dict, "metadata", Utils.SafeJoin("|", Metadata.SafePairs));
			BaseParams.AddParam(dict, "type", Type);
		}
	}
}
