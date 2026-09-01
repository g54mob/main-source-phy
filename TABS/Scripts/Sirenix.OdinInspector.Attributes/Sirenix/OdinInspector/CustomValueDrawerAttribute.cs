using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class CustomValueDrawerAttribute : Attribute
	{
		public string MethodName;

		public CustomValueDrawerAttribute(string methodName)
		{
			MethodName = methodName;
		}
	}
}
