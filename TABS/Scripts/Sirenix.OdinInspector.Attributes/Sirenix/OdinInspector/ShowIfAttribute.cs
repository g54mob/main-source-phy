using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class ShowIfAttribute : Attribute
	{
		public string MemberName;

		public bool Animate;

		public object Value;

		public ShowIfAttribute(string memberName, bool animate = true)
		{
			MemberName = memberName;
			Animate = animate;
		}

		public ShowIfAttribute(string memberName, object optionalValue, bool animate = true)
		{
			MemberName = memberName;
			Value = optionalValue;
			Animate = animate;
		}
	}
}
