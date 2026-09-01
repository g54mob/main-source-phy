using System;

namespace Modding.Serialization
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
	public class ReloadableAttribute : Attribute
	{
		public Action<IReloadable> CallOnNewBeforeOnReload;
	}
}
