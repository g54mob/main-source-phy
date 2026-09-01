using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback allows you to change the state of the target gameobject from active to inactive (or the opposite), on init, play, stop or reset. For each of these you can specify if you want to force a state (active or inactive), or toggle it (active becomes inactive, inactive becomes active).")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("GameObject/Set Active")]
	public class MMF_SetActive : MMF_Feedback
	{
		public enum PossibleStates
		{
			Active = 0,
			Inactive = 1,
			Toggle = 2
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Set Active Target", true, 12, true, false)]
		[Tooltip("the gameobject we want to change the active state of")]
		public GameObject TargetGameObject;

		[Tooltip("a list of extra gameobjects we want to change the active state of")]
		public List<GameObject> ExtraTargetGameObjects;

		[MMFInspectorGroup("States", true, 14, false, false)]
		[Tooltip("whether or not we should alter the state of the target object on init")]
		public bool SetStateOnInit;

		[MMFCondition("SetStateOnInit", true)]
		[Tooltip("how to change the state on init")]
		public PossibleStates StateOnInit;

		[Tooltip("whether or not we should alter the state of the target object on play")]
		public bool SetStateOnPlay;

		[Tooltip("how to change the state on play")]
		[MMFCondition("SetStateOnPlay", true)]
		public PossibleStates StateOnPlay;

		[Tooltip("whether or not we should alter the state of the target object on stop")]
		public bool SetStateOnStop;

		[Tooltip("how to change the state on stop")]
		[MMFCondition("SetStateOnStop", true)]
		public PossibleStates StateOnStop;

		[Tooltip("whether or not we should alter the state of the target object on reset")]
		public bool SetStateOnReset;

		[Tooltip("how to change the state on reset")]
		[MMFCondition("SetStateOnReset", true)]
		public PossibleStates StateOnReset;

		[Tooltip("whether or not we should alter the state of the target object on skip")]
		public bool SetStateOnSkip;

		[Tooltip("how to change the state on skip")]
		[MMFCondition("SetStateOnSkip", true)]
		public PossibleStates StateOnSkip;

		[Tooltip("whether or not we should alter the state of the target object when the player this feedback belongs to is done playing all its feedbacks")]
		public bool SetStateOnPlayerComplete;

		[Tooltip("how to change the state on player complete")]
		[MMFCondition("SetStateOnPlayerComplete", true)]
		public PossibleStates StateOnPlayerComplete;

		protected bool _initialState;

		protected List<bool> _initialStates;

		public override bool HasAutomatedTargetAcquisition => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomReset()
		{
		}

		protected override void CustomPlayerComplete()
		{
		}

		protected override void CustomSkipToTheEnd(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void SetStatus(PossibleStates state)
		{
		}

		protected virtual void ApplyStatus(GameObject target, bool newState)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
