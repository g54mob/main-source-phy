using System;
using System.Diagnostics;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class InlineEditorAttribute : Attribute
	{
		public InlineEditorModes Mode { get; set; } = InlineEditorModes.GUIOnly;

		public float PreviewHeight { get; set; } = 50f;

		public InlineEditorAttribute()
		{
		}

		public InlineEditorAttribute(InlineEditorModes mode)
		{
			Mode = mode;
		}
	}
}
