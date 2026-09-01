using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class MetadataFieldResult : BaseResult
	{
		[DataMember(Name = "external_id")]
		public string ExternalId { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; set; }

		[DataMember(Name = "label")]
		public string Label { get; set; }

		[DataMember(Name = "mandatory")]
		public bool Mandatory { get; set; }

		[DataMember(Name = "default_value")]
		public object DefaultValue { get; set; }

		[DataMember(Name = "validation")]
		public MetadataValidationResult Validation { get; set; }

		[DataMember(Name = "datasource")]
		public MetadataDataSourceResult DataSource { get; set; }
	}
}
