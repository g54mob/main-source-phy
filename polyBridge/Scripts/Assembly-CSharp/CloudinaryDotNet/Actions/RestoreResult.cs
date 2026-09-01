using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class RestoreResult : BaseResult
	{
		[DataMember(Name = "resource_type")]
		protected string m_resourceType;

		public ResourceType ResourceType => ApiShared.ParseCloudinaryParam<ResourceType>(m_resourceType);

		public Dictionary<string, RestoredResource> RestoredResources { get; set; }

		internal override void SetValues(JToken source)
		{
			base.SetValues(source);
			if (RestoredResources == null)
			{
				RestoredResources = new Dictionary<string, RestoredResource>();
			}
			if (source == null)
			{
				return;
			}
			foreach (JToken item in source.Children())
			{
				string name = item.ToObject<JProperty>().Name;
				RestoredResource value = item.ToObject<JProperty>().Value.ToObject<RestoredResource>();
				RestoredResources.Add(name, value);
			}
		}
	}
}
