using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class DelResParams : BaseParams
	{
		private List<string> m_publicIds = new List<string>();

		private string m_prefix;

		private string m_tag;

		private bool m_all;

		public ResourceType ResourceType { get; set; }

		public string Type { get; set; }

		public bool KeepOriginal { get; set; }

		public bool Invalidate { get; set; }

		public string NextCursor { get; set; }

		public List<string> PublicIds
		{
			get
			{
				return m_publicIds;
			}
			set
			{
				m_publicIds = value;
				m_prefix = string.Empty;
				m_tag = string.Empty;
				m_all = false;
			}
		}

		public string Prefix
		{
			get
			{
				return m_prefix;
			}
			set
			{
				m_publicIds = null;
				m_tag = string.Empty;
				m_prefix = value;
				m_all = false;
			}
		}

		public string Tag
		{
			get
			{
				return m_tag;
			}
			set
			{
				m_publicIds = null;
				m_prefix = string.Empty;
				m_tag = value;
				m_all = false;
			}
		}

		public bool All
		{
			get
			{
				return m_all;
			}
			set
			{
				if (value)
				{
					m_publicIds = null;
					m_prefix = string.Empty;
					m_tag = string.Empty;
					m_all = value;
				}
				else
				{
					m_all = value;
				}
			}
		}

		public List<Transformation> Transformations { get; set; }

		public DelResParams()
		{
			Type = "upload";
		}

		public override void Check()
		{
			if ((PublicIds == null || PublicIds.Count == 0) && string.IsNullOrEmpty(Prefix) && string.IsNullOrEmpty(Tag) && !All)
			{
				throw new ArgumentException("Either PublicIds or Prefix or Tag must be specified!");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			BaseParams.AddParam(sortedDictionary, "invalidate", Invalidate);
			BaseParams.AddParam(sortedDictionary, "next_cursor", NextCursor);
			if (Transformations != null && Transformations.Count > 0)
			{
				BaseParams.AddParam(sortedDictionary, "transformations", string.Join("|", Transformations));
			}
			BaseParams.AddParam(sortedDictionary, "keep_original", KeepOriginal);
			if (!string.IsNullOrEmpty(Tag))
			{
				return sortedDictionary;
			}
			if (!string.IsNullOrEmpty(Prefix))
			{
				sortedDictionary.Add("prefix", Prefix);
			}
			else if (PublicIds != null && PublicIds.Count > 0)
			{
				sortedDictionary.Add("public_ids", PublicIds);
			}
			if (m_all)
			{
				BaseParams.AddParam(sortedDictionary, "all", value: true);
			}
			return sortedDictionary;
		}
	}
}
