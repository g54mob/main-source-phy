using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class ListResourceTypesResult : BaseResult
	{
		[DataMember(Name = "resource_types")]
		protected string[] m_resourceTypes;

		public ResourceType[] ResourceTypes { get; set; }

		internal override void SetValues(JToken source)
		{
			base.SetValues(source);
			List<ResourceType> list = new List<ResourceType>();
			string[] resourceTypes = m_resourceTypes;
			foreach (string s in resourceTypes)
			{
				list.Add(ApiShared.ParseCloudinaryParam<ResourceType>(s));
			}
			ResourceTypes = list.ToArray();
		}
	}
}
