using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will allow you to send to an animator (bound in its inspector) a bool, int, float or trigger parameter, allowing you to trigger an animation, with or without randomness.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Animation/Animation Parameter")]
	public class MMF_Animation : MMF_Feedback
	{
		public enum TriggerModes
		{
			SetTrigger = 0,
			ResetTrigger = 1
		}

		public enum ValueModes
		{
			None = 0,
			Constant = 1,
			Random = 2,
			Incremental = 3
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Animation", true, 12, true, false)]
		[Tooltip("the animator whose parameters you want to update")]
		public Animator BoundAnimator;

		[Tooltip("the list of extra animators whose parameters you want to update")]
		public List<Animator> ExtraBoundAnimators;

		[Tooltip("the duration for the player to consider. This won't impact your animation, but is a way to communicate to the MMF Player the duration of this feedback. Usually you'll want it to match your actual animation, and setting it can be useful to have this feedback work with holding pauses.")]
		public float DeclaredDuration;

		[MMFInspectorGroup("Trigger", true, 16, false, false)]
		[Tooltip("if this is true, will update the specified trigger parameter")]
		public bool UpdateTrigger;

		[Tooltip("the selected mode to interact with this trigger")]
		[MMFCondition("UpdateTrigger", true)]
		public TriggerModes TriggerMode;

		[Tooltip("the trigger animator parameter to, well, trigger when the feedback is played")]
		[MMFCondition("UpdateTrigger", true)]
		public string TriggerParameterName;

		[MMFInspectorGroup("Random Trigger", true, 20, false, false)]
		[Tooltip("if this is true, will update a random trigger parameter, picked from the list below")]
		public bool UpdateRandomTrigger;

		[Tooltip("the selected mode to interact with this trigger")]
		[MMFCondition("UpdateRandomTrigger", true)]
		public TriggerModes RandomTriggerMode;

		[Tooltip("the trigger animator parameters to trigger at random when the feedback is played")]
		public List<string> RandomTriggerParameterNames;

		[MMFInspectorGroup("Bool", true, 17, false, false)]
		[Tooltip("if this is true, will update the specified bool parameter")]
		public bool UpdateBool;

		[Tooltip("the bool parameter to turn true when the feedback gets played")]
		[MMFCondition("UpdateBool", true)]
		public string BoolParameterName;

		[Tooltip("when in bool mode, whether to set the bool parameter to true or false")]
		[MMFCondition("UpdateBool", true)]
		public bool BoolParameterValue;

		[MMFInspectorGroup("Random Bool", true, 19, false, false)]
		[Tooltip("if this is true, will update a random bool parameter picked from the list below")]
		public bool UpdateRandomBool;

		[Tooltip("when in bool mode, whether to set the bool parameter to true or false")]
		[MMFCondition("UpdateRandomBool", true)]
		public bool RandomBoolParameterValue;

		[Tooltip("the bool parameter to turn true when the feedback gets played")]
		public List<string> RandomBoolParameterNames;

		[MMFInspectorGroup("Int", true, 24, false, false)]
		[Tooltip("the int parameter to turn true when the feedback gets played")]
		public ValueModes IntValueMode;

		[Tooltip("the int parameter to turn true when the feedback gets played")]
		[MMFEnumCondition("IntValueMode", new int[] { 1, 2, 3 })]
		public string IntParameterName;

		[Tooltip("the value to set to that int parameter")]
		[MMFEnumCondition("IntValueMode", new int[] { 1 })]
		public int IntValue;

		[Tooltip("the min value (inclusive) to set at random to that int parameter")]
		[MMFEnumCondition("IntValueMode", new int[] { 2 })]
		public int IntValueMin;

		[Tooltip("the max value (exclusive) to set at random to that int parameter")]
		[MMFEnumCondition("IntValueMode", new int[] { 2 })]
		public int IntValueMax;

		[Tooltip("the value to increment that int parameter by")]
		[MMFEnumCondition("IntValueMode", new int[] { 3 })]
		public int IntIncrement;

		[MMFInspectorGroup("Float", true, 22, false, false)]
		[Tooltip("the Float parameter to turn true when the feedback gets played")]
		public ValueModes FloatValueMode;

		[Tooltip("the float parameter to turn true when the feedback gets played")]
		[MMFEnumCondition("FloatValueMode", new int[] { 1, 2, 3 })]
		public string FloatParameterName;

		[Tooltip("the value to set to that float parameter")]
		[MMFEnumCondition("FloatValueMode", new int[] { 1 })]
		public float FloatValue;

		[Tooltip("the min value (inclusive) to set at random to that float parameter")]
		[MMFEnumCondition("FloatValueMode", new int[] { 2 })]
		public float FloatValueMin;

		[Tooltip("the max value (exclusive) to set at random to that float parameter")]
		[MMFEnumCondition("FloatValueMode", new int[] { 2 })]
		public float FloatValueMax;

		[Tooltip("the value to increment that float parameter by")]
		[MMFEnumCondition("FloatValueMode", new int[] { 3 })]
		public float FloatIncrement;

		[MMFInspectorGroup("Layer Weights", true, 22, false, false)]
		[Tooltip("whether or not to set layer weights on the specified layer when playing this feedback")]
		public bool SetLayerWeight;

		[Tooltip("the index of the layer to target when changing layer weights")]
		[MMFCondition("SetLayerWeight", true)]
		public int TargetLayerIndex;

		[Tooltip("the new weight to set on the target animator layer")]
		[MMFCondition("SetLayerWeight", true)]
		public float NewWeight;

		protected int _triggerParameter;

		protected int _boolParameter;

		protected int _intParameter;

		protected int _floatParameter;

		protected List<int> _randomTriggerParameters;

		protected List<int> _randomBoolParameters;

		public override float FeedbackDuration
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public override bool HasRandomness => false;

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

		protected virtual void ApplyValue(Animator targetAnimator, float intensityMultiplier)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
