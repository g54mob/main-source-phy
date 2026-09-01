using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class SearchResource
	{
		[DataMember(Name = "resource_type")]
		protected string m_resourceType;

		[DataMember(Name = "public_id")]
		public string PublicId { get; set; }

		[DataMember(Name = "folder")]
		public string Folder { get; set; }

		[DataMember(Name = "filename")]
		public string FileName { get; set; }

		[DataMember(Name = "format")]
		public string Format { get; set; }

		[DataMember(Name = "version")]
		public string Version { get; set; }

		public ResourceType ResourceType => ApiShared.ParseCloudinaryParam<ResourceType>(m_resourceType);

		[DataMember(Name = "type")]
		public string Type { get; set; }

		[Obsolete("Property Created is deprecated, please use CreatedAt instead")]
		public string Created
		{
			get
			{
				return CreatedAt;
			}
			set
			{
				CreatedAt = value;
			}
		}

		[DataMember(Name = "created_at")]
		public string CreatedAt { get; set; }

		[Obsolete("Property Uploaded is deprecated, please use UploadedAt instead")]
		public string Uploaded
		{
			get
			{
				return UploadedAt;
			}
			set
			{
				UploadedAt = value;
			}
		}

		[DataMember(Name = "uploaded_at")]
		public string UploadedAt { get; set; }

		[Obsolete("Property Length is deprecated, please use Bytes instead")]
		public long Length
		{
			get
			{
				return Bytes;
			}
			set
			{
				Bytes = value;
			}
		}

		[DataMember(Name = "bytes")]
		public long Bytes { get; set; }

		[DataMember(Name = "backup_bytes")]
		public long BackupBytes { get; set; }

		[DataMember(Name = "width")]
		public int Width { get; set; }

		[DataMember(Name = "height")]
		public int Height { get; set; }

		[DataMember(Name = "aspect_ratio")]
		public double AspectRatio { get; set; }

		[DataMember(Name = "pixels")]
		public long Pixels { get; set; }

		[DataMember(Name = "pages")]
		public int Pages { get; set; }

		[DataMember(Name = "url")]
		public string Url { get; set; }

		[DataMember(Name = "secure_url")]
		public string SecureUrl { get; set; }

		[DataMember(Name = "status")]
		public string Status { get; set; }

		[DataMember(Name = "access_mode")]
		public string AccessMode { get; set; }

		[DataMember(Name = "access_control")]
		public List<AccessControlRule> AccessControl { get; set; }

		[DataMember(Name = "etag")]
		public string Etag { get; set; }

		[DataMember(Name = "tags")]
		public string[] Tags { get; set; }

		[DataMember(Name = "image_metadata")]
		public Dictionary<string, object> ImageMetadata { get; set; }

		[DataMember(Name = "metadata")]
		public JToken MetadataFields { get; set; }

		[DataMember(Name = "context")]
		public Dictionary<string, string> Context { get; set; }

		[DataMember(Name = "image_analysis")]
		public ImageAnalysis ImageAnalysis { get; set; }

		[DataMember(Name = "created_by")]
		public IdentityInfo CreatedBy { get; set; }

		[DataMember(Name = "uploaded_by")]
		public IdentityInfo UploadedBy { get; set; }
	}
}
