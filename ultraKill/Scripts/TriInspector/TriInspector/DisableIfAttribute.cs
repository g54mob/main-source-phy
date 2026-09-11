using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class DisableIfAttribute : Attribute
	{
		public string Condition { get; }

		public object Value { get; }

		public bool Inverse { get; protected set; }

		public DisableIfAttribute(string condition)
			: this(condition, true)
		{
		}

		public DisableIfAttribute(string condition, object value)
		{
			Condition = condition;
			Value = value;
		}
	}
}
