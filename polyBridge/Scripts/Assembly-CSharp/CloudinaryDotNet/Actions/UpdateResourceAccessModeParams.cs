using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class UpdateResourceAccessModeParams : BaseParams
	{
		private List<string> m_publicIds = new List<string>();

		private ResourceType m_resourceType;

		private string m_accessMode = "public";

		private string m_type = "upload";

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

		public string AccessMode
		{
			get
			{
				return m_accessMode;
			}
			set
			{
				m_accessMode = value;
			}
		}

		public ResourceType ResourceType
		{
			get
			{
				return m_resourceType;
			}
			set
			{
				m_resourceType = value;
			}
		}

		public string Type
		{
			get
			{
				return m_type;
			}
			set
			{
				m_type = value;
			}
		}

		public string Prefix { get; set; }

		public string Tag { get; set; }

		private bool PublicIdsExist
		{
			get
			{
				if (PublicIds != null)
				{
					return PublicIds.Count > 0;
				}
				return false;
			}
		}

		public override void Check()
		{
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			if (PublicIdsExist)
			{
				sortedDictionary.Add("public_ids", PublicIds);
			}
			else if (!string.IsNullOrEmpty(Prefix))
			{
				sortedDictionary.Add("prefix", Prefix);
			}
			else if (!string.IsNullOrEmpty(Tag))
			{
				sortedDictionary.Add("tag", Tag);
			}
			sortedDictionary.Add("access_mode", m_accessMode);
			return sortedDictionary;
		}
	}
}
