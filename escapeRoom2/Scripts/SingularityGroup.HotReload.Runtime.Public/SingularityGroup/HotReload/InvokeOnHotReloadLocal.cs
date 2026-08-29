using System;

namespace SingularityGroup.HotReload
{
	[AttributeUsage(AttributeTargets.Method)]
	public class InvokeOnHotReloadLocal : Attribute
	{
		public readonly string methodToInvoke;

		public InvokeOnHotReloadLocal(string methodToInvoke = null)
		{
			this.methodToInvoke = methodToInvoke;
		}
	}
}
