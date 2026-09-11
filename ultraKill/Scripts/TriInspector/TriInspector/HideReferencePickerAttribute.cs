using System;

namespace TriInspector
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class HideReferencePickerAttribute : Attribute
	{
	}
}
