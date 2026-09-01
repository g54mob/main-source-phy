using System.Collections.Generic;
using System.Linq;

namespace CloudinaryDotNet.Actions
{
	public class AndValidationParams : MetadataValidationParams
	{
		public List<MetadataValidationParams> Rules { get; set; }

		public AndValidationParams(List<MetadataValidationParams> rules)
		{
			base.Type = MetadataValidationType.And;
			Rules = rules;
		}

		public override void Check()
		{
			Utils.ShouldNotBeEmpty(() => Rules);
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			List<SortedDictionary<string, object>> value = Rules.Select((MetadataValidationParams entry) => entry.ToParamsDictionary()).ToList();
			dict.Add("rules", value);
		}
	}
}
