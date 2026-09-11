using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class OnValueChangedAttribute : Attribute
	{
		public string Method { get; }

		public OnValueChangedAttribute(string method)
		{
			Method = method;
		}
	}
}
