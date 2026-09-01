using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[IncludeMyAttributes]
	[ShowInInspector]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class ButtonGroupAttribute : PropertyGroupAttribute
	{
		public ButtonGroupAttribute(string group = "_DefaultGroup", int order = 0)
			: base(group, order)
		{
		}
	}
}
