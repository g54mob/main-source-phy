using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class InlineButtonAttribute : Attribute
	{
		public string MemberMethod { get; private set; }

		public string Label { get; private set; }

		public InlineButtonAttribute(string memberMethod, string label = null)
		{
			MemberMethod = memberMethod;
			Label = label;
		}
	}
}
