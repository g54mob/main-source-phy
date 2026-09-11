using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class InfoBoxAttribute : Attribute
	{
		public string Text { get; }

		public TriMessageType MessageType { get; }

		public string VisibleIf { get; }

		public InfoBoxAttribute(string text, TriMessageType messageType = TriMessageType.Info, string visibleIf = null)
		{
			Text = text;
			MessageType = messageType;
			VisibleIf = visibleIf;
		}
	}
}
