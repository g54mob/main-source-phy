using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class TypeFilterAttribute : Attribute
	{
		public string MemberName;

		public string DropdownTitle;

		public TypeFilterAttribute(string memberName)
		{
			MemberName = memberName;
		}
	}
}
