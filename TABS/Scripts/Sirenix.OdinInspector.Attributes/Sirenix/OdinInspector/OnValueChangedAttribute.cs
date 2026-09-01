using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class OnValueChangedAttribute : Attribute
	{
		public string MethodName;

		public bool IncludeChildren;

		public OnValueChangedAttribute(string methodName, bool includeChildren = false)
		{
			MethodName = methodName;
			IncludeChildren = includeChildren;
		}
	}
}
