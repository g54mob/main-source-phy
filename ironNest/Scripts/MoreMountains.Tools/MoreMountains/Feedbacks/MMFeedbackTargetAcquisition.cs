using System;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[Serializable]
	public class MMFeedbackTargetAcquisition
	{
		public enum Modes
		{
			None = 0,
			Self = 1,
			AnyChild = 2,
			ChildAtIndex = 3,
			Parent = 4,
			FirstReferenceHolder = 5,
			PreviousReferenceHolder = 6,
			ClosestReferenceHolder = 7,
			NextReferenceHolder = 8,
			LastReferenceHolder = 9
		}

		[Tooltip("the selected mode for target acquisition\nNone : nothing will happen\nSelf : the target will be picked on the MMF Player's game object\nAnyChild : the target will be picked on any of the MMF Player's child objects\nChildAtIndex : the target will be picked on the child at index X of the MMF Player\nParent : the target will be picked on the first parent where a matching target is found\nVarious reference holders : the target will be picked on the specified reference holder in the list (either the first one, previous : first one found before this feedback in the list, closest in any direction from this feedback, the next one found, or the last one in the list)")]
		public Modes Mode;

		[MMFEnumCondition("Mode", new int[] { 3 })]
		public int ChildIndex;

		private static MMF_ReferenceHolder _referenceHolder;

		public static MMF_ReferenceHolder GetReferenceHolder(MMFeedbackTargetAcquisition settings, MMF_Player owner, int currentFeedbackIndex)
		{
			return null;
		}

		public static GameObject FindAutomatedTargetGameObject(MMFeedbackTargetAcquisition settings, MMF_Player owner, int currentFeedbackIndex)
		{
			return null;
		}

		public static T FindAutomatedTarget<T>(MMFeedbackTargetAcquisition settings, MMF_Player owner, int currentFeedbackIndex)
		{
			return default(T);
		}
	}
}
