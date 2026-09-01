using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public abstract class BaseUserGroupParams : BaseParams
	{
		public string Name { get; set; }

		protected BaseUserGroupParams(string name)
		{
			Name = name;
		}

		public override void Check()
		{
			Utils.ShouldNotBeEmpty(() => Name);
		}

		protected override void AddParamsToDictionary(SortedDictionary<string, object> dict)
		{
			BaseParams.AddParam(dict, "name", Name);
		}
	}
}
