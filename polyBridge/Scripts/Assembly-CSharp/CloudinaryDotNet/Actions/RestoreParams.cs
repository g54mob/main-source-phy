using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class RestoreParams : BaseParams
	{
		private List<string> m_publicIds = new List<string>();

		private List<string> m_versions = new List<string>();

		private ResourceType m_resourceType;

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

		public List<string> Versions
		{
			get
			{
				return m_versions;
			}
			set
			{
				m_versions = value;
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

		public AssetType Type { get; set; }

		private bool VersionsExist
		{
			get
			{
				if (Versions != null)
				{
					return Versions.Count > 0;
				}
				return false;
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
			if (!PublicIdsExist)
			{
				throw new ArgumentException("At least one PublicId must be specified!");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			if (PublicIdsExist)
			{
				sortedDictionary.Add("public_ids", PublicIds);
			}
			if (VersionsExist)
			{
				sortedDictionary.Add("versions", Versions);
			}
			BaseParams.AddParam(sortedDictionary, "type", ApiShared.GetCloudinaryParam(Type));
			return sortedDictionary;
		}
	}
}
