using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[DontApplyToListElements]
	[Conditional("UNITY_EDITOR")]
	public sealed class HideIfAttribute : Attribute
	{
		public string MemberName;

		public object Value;

		public bool Animate;

		public HideIfAttribute(string memberName, bool animate = true)
		{
			MemberName = memberName;
			Animate = animate;
		}

		public HideIfAttribute(string memberName, object optionalValue, bool animate = true)
		{
			MemberName = memberName;
			Value = optionalValue;
			Animate = animate;
		}
	}
}
