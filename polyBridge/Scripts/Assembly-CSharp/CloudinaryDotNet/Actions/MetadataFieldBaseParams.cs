using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudinaryDotNet.Actions
{
	public abstract class MetadataFieldBaseParams<T> : BaseParams
	{
		public string ExternalId { get; set; }

		public MetadataFieldType Type { get; set; }

		public string Label { get; set; }

		public bool Mandatory { get; set; }

		public T DefaultValue { get; set; }

		public MetadataValidationParams Validation { get; set; }

		public MetadataDataSourceParams DataSource { get; set; }

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			BaseParams.AddParam(dict, "type", ApiShared.GetCloudinaryParam(Type));
			BaseParams.AddParam(dict, "mandatory", Mandatory);
			if (!string.IsNullOrEmpty(ExternalId))
			{
				BaseParams.AddParam(dict, "external_id", ExternalId);
			}
			if (Validation != null)
			{
				dict.Add("validation", Validation.ToParamsDictionary());
			}
			if (DataSource != null)
			{
				dict.Add("datasource", DataSource.ToParamsDictionary());
			}
		}

		protected void CheckScalarDataModel(List<Type> allowedValidationTypes)
		{
			Utils.ShouldNotBeSpecified(() => DataSource);
			if (Validation != null)
			{
				Type type = Validation.GetType();
				bool num = !allowedValidationTypes.Contains(type);
				string text = string.Join(", ", allowedValidationTypes.Select((Type type2) => type2.Name));
				if (num)
				{
					throw new ArgumentException("Only validations of types " + text + " can be applied to the metadata field");
				}
				Validation.Check();
			}
		}
	}
}
