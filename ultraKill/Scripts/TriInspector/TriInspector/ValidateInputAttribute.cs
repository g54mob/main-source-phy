using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class ValidateInputAttribute : Attribute
	{
		public string Method { get; }

		public ValidateInputAttribute(string method)
		{
			Method = method;
		}
	}
}
