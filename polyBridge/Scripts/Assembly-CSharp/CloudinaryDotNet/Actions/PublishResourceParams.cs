using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class PublishResourceParams : BaseParams
	{
		private List<string> m_publicIds = new List<string>();

		private ResourceType m_resourceType;

		private string m_type = string.Empty;

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
			if (!string.IsNullOrWhiteSpace(m_type))
			{
				sortedDictionary.Add("type", m_type);
			}
			return sortedDictionary;
		}
	}
}
