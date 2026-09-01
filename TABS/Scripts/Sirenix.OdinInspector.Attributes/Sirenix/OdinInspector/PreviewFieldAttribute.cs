using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class PreviewFieldAttribute : Attribute
	{
		public float Height;

		public ObjectFieldAlignment Alignment;

		public bool AlignmentHasValue;

		public PreviewFieldAttribute()
		{
			Height = 0f;
		}

		public PreviewFieldAttribute(float height)
		{
			Height = height;
		}

		public PreviewFieldAttribute(float height, ObjectFieldAlignment alignment)
		{
			Height = height;
			Alignment = alignment;
			AlignmentHasValue = true;
		}

		public PreviewFieldAttribute(ObjectFieldAlignment alignment)
		{
			Alignment = alignment;
			AlignmentHasValue = true;
		}
	}
}
