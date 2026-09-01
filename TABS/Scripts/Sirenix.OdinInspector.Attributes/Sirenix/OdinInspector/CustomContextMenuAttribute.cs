using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class CustomContextMenuAttribute : Attribute
	{
		public string MenuItem;

		public string MethodName;

		public CustomContextMenuAttribute(string menuItem, string methodName)
		{
			MenuItem = menuItem;
			MethodName = methodName;
		}
	}
}
