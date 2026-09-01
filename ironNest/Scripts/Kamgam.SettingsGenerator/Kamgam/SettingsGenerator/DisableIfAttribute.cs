using System;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class DisableIfAttribute : PropertyAttribute
	{
		public enum BehaviourType
		{
			Disable = 0,
			Hide = 1
		}

		public string PropertyName { get; private set; }

		public object CompareValue { get; private set; }

		public string PropertyName2 { get; private set; }

		public object CompareValue2 { get; private set; }

		public BehaviourType Behaviour { get; private set; }

		public bool InvertBehaviour { get; private set; }

		public DisableIfAttribute(string propertyName, object comparedValue = null, BehaviourType behaviour = BehaviourType.Disable, bool invertBehaviour = false, string propertyName2 = null, object comparedValue2 = null)
		{
		}
	}
}
