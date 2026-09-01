using System;
using UnityEngine;

namespace BitCode.Profiles.Rules
{
	[Serializable]
	public abstract class HasCapabilityRule<TCapabilityLevel> : ISelectionRule where TCapabilityLevel : Enum
	{
		internal const string LevelFieldName = "requiredCapability";

		internal const string RuleTypeFieldName = "matchingType";

		[SerializeField]
		protected TCapabilityLevel requiredCapability;

		[SerializeField]
		protected RuleMatchingType matchingType;

		public bool RuleMatches(IProfileSelectionState state)
		{
			return state.GetCapability<TCapabilityLevel>().Matches(matchingType, requiredCapability);
		}
	}
}
