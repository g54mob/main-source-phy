using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class TagParams : BaseParams
	{
		private List<string> m_publicIds = new List<string>();

		public List<string> PublicIds
		{
			get
			{
				return m_publicIds;
			}
			set
			{
				m_publicIds = value;
			}
		}

		public string Tag { get; set; }

		public ResourceType ResourceType { get; set; }

		public string Type { get; set; }

		public TagCommand Command { get; set; }

		public TagParams()
		{
			ResourceType = ResourceType.Image;
		}

		public override void Check()
		{
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "tag", Tag);
			BaseParams.AddParam(sortedDictionary, "public_ids", PublicIds);
			BaseParams.AddParam(sortedDictionary, "command", ApiShared.GetCloudinaryParam(Command));
			if (!string.IsNullOrEmpty(Type))
			{
				BaseParams.AddParam(sortedDictionary, "type", Type);
			}
			return sortedDictionary;
		}
	}
}
