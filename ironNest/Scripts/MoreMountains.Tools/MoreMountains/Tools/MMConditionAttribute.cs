using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
	public class MMConditionAttribute : PropertyAttribute
	{
		public string ConditionBoolean;

		public bool Hidden;

		public bool Negative;

		public MMConditionAttribute(string conditionBoolean)
		{
		}

		public MMConditionAttribute(string conditionBoolean, bool hideInInspector)
		{
		}

		public MMConditionAttribute(string conditionBoolean, bool hideInInspector, bool negative)
		{
		}
	}
}
