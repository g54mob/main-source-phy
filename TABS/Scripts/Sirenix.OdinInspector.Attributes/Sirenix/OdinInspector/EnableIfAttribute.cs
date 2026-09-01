using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class EnableIfAttribute : Attribute
	{
		public string MemberName;

		public object Value;

		public EnableIfAttribute(string memberName)
		{
			MemberName = memberName;
		}

		public EnableIfAttribute(string memberName, object optionalValue)
		{
			MemberName = memberName;
			Value = optionalValue;
		}
	}
}
