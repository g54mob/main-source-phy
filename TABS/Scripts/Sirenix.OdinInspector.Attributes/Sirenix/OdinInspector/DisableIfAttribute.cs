using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class DisableIfAttribute : Attribute
	{
		public string MemberName;

		public object Value;

		public DisableIfAttribute(string memberName)
		{
			MemberName = memberName;
		}

		public DisableIfAttribute(string memberName, object optionalValue)
		{
			MemberName = memberName;
			Value = optionalValue;
		}
	}
}
