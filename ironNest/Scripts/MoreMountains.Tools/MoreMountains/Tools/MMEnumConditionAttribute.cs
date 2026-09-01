using System;
using System.Collections;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
	public class MMEnumConditionAttribute : PropertyAttribute
	{
		public string ConditionEnum;

		public bool Hidden;

		private BitArray bitArray;

		public bool ContainsBitFlag(int enumValue)
		{
			return false;
		}

		public MMEnumConditionAttribute(string conditionBoolean, params int[] enumValues)
		{
		}
	}
}
