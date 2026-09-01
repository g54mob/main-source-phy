using System;
using System.Collections;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
	public class MMFEnumConditionAttribute : PropertyAttribute
	{
		public string ConditionEnum;

		public bool Hidden;

		private BitArray bitArray;

		public bool ContainsBitFlag(int enumValue)
		{
			return false;
		}

		public MMFEnumConditionAttribute(string conditionBoolean, params int[] enumValues)
		{
		}
	}
}
