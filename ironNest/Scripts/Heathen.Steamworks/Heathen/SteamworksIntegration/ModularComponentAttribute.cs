using System;

namespace Heathen.SteamworksIntegration
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class ModularComponentAttribute : Attribute
	{
		public Type ParentType { get; }

		public string Header { get; }

		public string FieldName { get; }

		public ModularComponentAttribute(Type type, string header, string field)
		{
		}
	}
}
