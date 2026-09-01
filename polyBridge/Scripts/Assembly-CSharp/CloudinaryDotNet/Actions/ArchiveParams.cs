using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudinaryDotNet.Actions
{
	public class ArchiveParams : BaseParams
	{
		private List<string> m_publicIds;

		private List<string> m_fullyQualifiedPublicIds;

		private List<string> m_tags;

		private List<string> m_prefixes;

		private string m_resourceType;

		private string m_type;

		private List<Transformation> m_transformations;

		private ArchiveCallMode m_mode = ArchiveCallMode.Create;

		private ArchiveFormat m_targetFormat;

		private bool m_flattenFolders;

		private bool m_flattenTransformations;

		private int m_expiresAt;

		private bool m_useOriginalFilename;

		private string m_notificationUrl;

		private bool m_keepDerived;

		private bool m_skipTransformationName;

		private bool m_allow_missing;

		private string m_targetPublicId;

		private bool m_async;

		private List<string> m_targetTags;

		public ArchiveParams()
		{
			m_resourceType = "image";
		}

		public List<string> PublicIds()
		{
			return m_publicIds;
		}

		public ArchiveParams PublicIds(List<string> publicIds)
		{
			m_publicIds = publicIds;
			return this;
		}

		public List<string> FullyQualifiedPublicIds()
		{
			return m_fullyQualifiedPublicIds;
		}

		public ArchiveParams FullyQualifiedPublicIds(List<string> fullyQualifiedPublicIds)
		{
			m_fullyQualifiedPublicIds = fullyQualifiedPublicIds;
			return this;
		}

		public List<string> Tags()
		{
			return m_tags;
		}

		public ArchiveParams Tags(List<string> tags)
		{
			m_tags = tags;
			return this;
		}

		public List<string> Prefixes()
		{
			return m_prefixes;
		}

		public ArchiveParams Prefixes(List<string> prefixes)
		{
			m_prefixes = prefixes;
			return this;
		}

		public override void Check()
		{
			List<string> publicIds = m_publicIds;
			if (publicIds == null || !publicIds.Any())
			{
				List<string> fullyQualifiedPublicIds = m_fullyQualifiedPublicIds;
				if (fullyQualifiedPublicIds == null || !fullyQualifiedPublicIds.Any())
				{
					List<string> prefixes = m_prefixes;
					if (prefixes == null || !prefixes.Any())
					{
						List<string> tags = m_tags;
						if (tags == null || !tags.Any())
						{
							throw new ArgumentException("At least one of the following \"filtering\" parameters needs to be specified: PublicIds, FullyQualifiedPublicIds, Tags or Prefixes.");
						}
					}
				}
			}
			if ((m_resourceType == "auto") ^ (m_fullyQualifiedPublicIds?.Any() ?? false))
			{
				throw new ArgumentException("To create an archive with multiple types of assets, you must set ResourceType to \"auto\" and provide FullyQualifiedPublicIds (For example, 'video/upload/my_video.mp4')");
			}
		}

		public virtual ArchiveCallMode Mode()
		{
			return m_mode;
		}

		public ArchiveParams Mode(ArchiveCallMode mode)
		{
			m_mode = mode;
			return this;
		}

		public string ResourceType()
		{
			return m_resourceType;
		}

		public ArchiveParams ResourceType(string resourceType)
		{
			m_resourceType = resourceType;
			return this;
		}

		public string Type()
		{
			return m_type;
		}

		public ArchiveParams Type(string type)
		{
			m_type = type;
			return this;
		}

		public List<Transformation> Transformations()
		{
			return m_transformations;
		}

		public ArchiveParams Transformations(List<Transformation> transformations)
		{
			m_transformations = transformations;
			return this;
		}

		public ArchiveFormat TargetFormat()
		{
			return m_targetFormat;
		}

		public ArchiveParams TargetFormat(ArchiveFormat targetFormat)
		{
			m_targetFormat = targetFormat;
			return this;
		}

		public string TargetPublicId()
		{
			return m_targetPublicId;
		}

		public ArchiveParams TargetPublicId(string targetPublicId)
		{
			m_targetPublicId = targetPublicId;
			return this;
		}

		public bool IsFlattenFolders()
		{
			return m_flattenFolders;
		}

		public ArchiveParams FlattenFolders(bool flattenFolders)
		{
			m_flattenFolders = flattenFolders;
			return this;
		}

		public bool IsFlattenTransformations()
		{
			return m_flattenTransformations;
		}

		public ArchiveParams FlattenTransformations(bool flattenTransformations)
		{
			m_flattenTransformations = flattenTransformations;
			return this;
		}

		public int ExpiresAt()
		{
			return m_expiresAt;
		}

		public ArchiveParams ExpiresAt(int expiresAt)
		{
			m_expiresAt = expiresAt;
			return this;
		}

		public bool IsUseOriginalFilename()
		{
			return m_useOriginalFilename;
		}

		public ArchiveParams UseOriginalFilename(bool useOriginalFilename)
		{
			m_useOriginalFilename = useOriginalFilename;
			return this;
		}

		public bool IsAsync()
		{
			return m_async;
		}

		public ArchiveParams Async(bool async)
		{
			m_async = async;
			return this;
		}

		public string NotificationUrl()
		{
			return m_notificationUrl;
		}

		public ArchiveParams NotificationUrl(string notificationUrl)
		{
			m_notificationUrl = notificationUrl;
			return this;
		}

		public List<string> TargetTags()
		{
			return m_targetTags;
		}

		public ArchiveParams TargetTags(List<string> targetTags)
		{
			m_targetTags = targetTags;
			return this;
		}

		public bool IsKeepDerived()
		{
			return m_keepDerived;
		}

		public ArchiveParams KeepDerived(bool keepDerived)
		{
			m_keepDerived = keepDerived;
			return this;
		}

		public bool IsSkipTransformationName()
		{
			return m_skipTransformationName;
		}

		public ArchiveParams SkipTransformationName(bool skipTransformationName)
		{
			m_skipTransformationName = skipTransformationName;
			return this;
		}

		public bool AllowMissing()
		{
			return m_allow_missing;
		}

		public ArchiveParams AllowMissing(bool allowMissing)
		{
			m_allow_missing = allowMissing;
			return this;
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			Check();
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "mode", ApiShared.GetCloudinaryParam(Mode()));
			if (m_tags != null && m_tags.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "tags", m_tags);
			}
			if (m_publicIds != null && m_publicIds.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "public_ids", m_publicIds);
			}
			if (m_fullyQualifiedPublicIds != null && m_fullyQualifiedPublicIds.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "fully_qualified_public_ids", m_fullyQualifiedPublicIds);
			}
			if (m_prefixes != null && m_prefixes.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "prefixes", m_prefixes);
			}
			if (!string.IsNullOrEmpty(m_type))
			{
				BaseParams.AddParam(sortedDictionary, "type", m_type);
			}
			if (m_transformations != null && m_transformations.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "transformations", string.Join("/", m_transformations));
			}
			if (m_targetFormat != ArchiveFormat.Zip)
			{
				BaseParams.AddParam(sortedDictionary, "target_format", ApiShared.GetCloudinaryParam(m_targetFormat));
			}
			if (m_flattenFolders)
			{
				BaseParams.AddParam(sortedDictionary, "flatten_folders", m_flattenFolders);
			}
			if (m_flattenTransformations)
			{
				BaseParams.AddParam(sortedDictionary, "flatten_transformations", m_flattenTransformations);
			}
			if (m_useOriginalFilename)
			{
				BaseParams.AddParam(sortedDictionary, "use_original_filename", m_useOriginalFilename);
			}
			if (!string.IsNullOrEmpty(m_notificationUrl))
			{
				BaseParams.AddParam(sortedDictionary, "notification_url", m_notificationUrl);
			}
			if (m_keepDerived)
			{
				BaseParams.AddParam(sortedDictionary, "keep_derived", m_keepDerived);
			}
			if (m_skipTransformationName)
			{
				BaseParams.AddParam(sortedDictionary, "skip_transformation_name", m_skipTransformationName);
			}
			if (!string.IsNullOrEmpty(m_targetPublicId))
			{
				BaseParams.AddParam(sortedDictionary, "target_public_id", m_targetPublicId);
			}
			if (m_mode == ArchiveCallMode.Create)
			{
				if (m_async)
				{
					BaseParams.AddParam(sortedDictionary, "async", m_async);
				}
				if (m_targetTags != null && m_targetTags.Count > 0)
				{
					BaseParams.AddParam(sortedDictionary, "target_tags", m_targetTags);
				}
			}
			if (m_expiresAt > 0 && m_mode == ArchiveCallMode.Download)
			{
				BaseParams.AddParam(sortedDictionary, "expires_at", m_expiresAt);
			}
			if (m_allow_missing)
			{
				BaseParams.AddParam(sortedDictionary, "allow_missing", m_allow_missing);
			}
			return sortedDictionary;
		}
	}
}
