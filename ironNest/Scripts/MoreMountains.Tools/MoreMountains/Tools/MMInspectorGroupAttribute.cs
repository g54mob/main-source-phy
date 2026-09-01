using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
	public class MMInspectorGroupAttribute : PropertyAttribute
	{
		public string GroupName;

		public bool GroupAllFieldsUntilNextGroupAttribute;

		public int GroupColorIndex;

		public bool ClosedByDefault;

		public MMInspectorGroupAttribute(string groupName, bool groupAllFieldsUntilNextGroupAttribute = false, int groupColorIndex = 24, bool closedByDefault = false)
		{
		}
	}
}
