using System.Collections.Generic;
using System.Linq;

namespace CloudinaryDotNet.Actions
{
	public class MetadataDataSourceParams : BaseParams
	{
		public List<EntryParams> Values { get; set; }

		public MetadataDataSourceParams(List<EntryParams> entries)
		{
			Values = entries;
		}

		public override void Check()
		{
			Utils.ShouldNotBeEmpty(() => Values);
			Values.ForEach(delegate(EntryParams value)
			{
				value.Check();
			});
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			base.AddParamsToDictionary(dict);
			List<SortedDictionary<string, object>> value = Values.Select((EntryParams entry) => entry.ToParamsDictionary()).ToList();
			dict.Add("values", value);
		}
	}
}
