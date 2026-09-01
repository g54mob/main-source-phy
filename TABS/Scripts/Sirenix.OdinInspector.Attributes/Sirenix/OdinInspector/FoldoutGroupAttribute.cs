using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class FoldoutGroupAttribute : PropertyGroupAttribute
	{
		public bool Expanded;

		public bool HasDefinedExpanded { get; private set; }

		public FoldoutGroupAttribute(string groupName, int order = 0)
			: base(groupName, order)
		{
		}

		public FoldoutGroupAttribute(string groupName, bool expanded, int order = 0)
			: base(groupName, order)
		{
			Expanded = expanded;
			HasDefinedExpanded = true;
		}

		protected override void CombineValuesWith(PropertyGroupAttribute other)
		{
			FoldoutGroupAttribute foldoutGroupAttribute = other as FoldoutGroupAttribute;
			if (foldoutGroupAttribute.HasDefinedExpanded)
			{
				HasDefinedExpanded = true;
				Expanded = foldoutGroupAttribute.Expanded;
			}
			if (HasDefinedExpanded)
			{
				foldoutGroupAttribute.HasDefinedExpanded = true;
				foldoutGroupAttribute.Expanded = Expanded;
			}
		}
	}
}
