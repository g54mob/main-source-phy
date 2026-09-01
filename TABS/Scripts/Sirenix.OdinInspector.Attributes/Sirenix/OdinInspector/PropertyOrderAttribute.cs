using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class PropertyOrderAttribute : Attribute
	{
		public int Order;

		public PropertyOrderAttribute()
		{
		}

		public PropertyOrderAttribute(int order)
		{
			Order = order;
		}
	}
}
