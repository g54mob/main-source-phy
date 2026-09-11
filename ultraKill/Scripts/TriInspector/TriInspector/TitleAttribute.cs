using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class TitleAttribute : Attribute
	{
		public string Title { get; }

		public bool HorizontalLine { get; set; } = true;

		public TitleAttribute(string title)
		{
			Title = title;
		}
	}
}
