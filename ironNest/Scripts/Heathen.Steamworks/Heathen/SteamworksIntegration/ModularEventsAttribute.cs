using System;

namespace Heathen.SteamworksIntegration
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class ModularEventsAttribute : Attribute
	{
		public Type ParentType { get; }

		public ModularEventsAttribute(Type type)
		{
		}
	}
}
