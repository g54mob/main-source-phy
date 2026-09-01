using System;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
	public class MMFConditionAttribute : PropertyAttribute
	{
		public string ConditionBoolean;

		public bool Hidden;

		public bool Negative;

		public MMFConditionAttribute(string conditionBoolean)
		{
		}

		public MMFConditionAttribute(string conditionBoolean, bool hideInInspector)
		{
		}

		public MMFConditionAttribute(string conditionBoolean, bool hideInInspector, bool negative)
		{
		}
	}
}
