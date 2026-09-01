using System;
using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class DelDerivedResParams : BaseParams
	{
		private string m_publicId = string.Empty;

		private List<Transformation> m_tranformations = new List<Transformation>();

		public List<string> DerivedResources { get; set; }

		public List<Transformation> Transformations
		{
			get
			{
				return m_tranformations;
			}
			set
			{
				m_tranformations = value;
			}
		}

		public string PublicId
		{
			get
			{
				return m_publicId;
			}
			set
			{
				m_publicId = value;
			}
		}

		public DelDerivedResParams()
		{
			DerivedResources = new List<string>();
		}

		public override void Check()
		{
			if ((DerivedResources == null || DerivedResources.Count == 0) && (m_tranformations == null || m_tranformations.Count == 0))
			{
				throw new ArgumentException("At least one derived resource or transformation must be specified!");
			}
			if (m_tranformations != null && m_tranformations.Count > 0 && string.IsNullOrWhiteSpace(m_publicId))
			{
				throw new ArgumentException("PublicId must be specified!");
			}
		}

		public override SortedDictionary<string, object> ToParamsDictionary()
		{
			SortedDictionary<string, object> sortedDictionary = base.ToParamsDictionary();
			if (DerivedResources != null && DerivedResources.Count > 0)
			{
				sortedDictionary.Add("derived_resource_ids", DerivedResources);
			}
			if (m_tranformations != null && m_tranformations.Count > 0)
			{
				List<string> list = new List<string>();
				foreach (Transformation tranformation in m_tranformations)
				{
					list.Add(tranformation.Generate());
				}
				sortedDictionary.Add("transformations", list);
			}
			return sortedDictionary;
		}
	}
}
