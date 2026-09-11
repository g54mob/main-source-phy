using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class LabelTextAttribute : Attribute
	{
		public string Text { get; }

		public LabelTextAttribute(string text)
		{
			Text = text;
		}
	}
}
