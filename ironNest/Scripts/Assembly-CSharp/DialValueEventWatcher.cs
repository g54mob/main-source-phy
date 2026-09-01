using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Gameplay/Dial/Dial Value Event Watcher")]
public class DialValueEventWatcher : MonoBehaviour
{
	[Serializable]
	public class ValueTrigger
	{
		[Tooltip("Target dial output value that should trigger this event.\n\nNotes:\n- Compared against the DialInteractable's AccumulatedValue (which is its public output).\n- Because dial values are floats, matching uses the Watcher's 'Match Tolerance'.\n\nExamples:\n- 0\n- 1\n- 15\n- 30")]
		public float targetValue;

		[Tooltip("Event invoked when the dial output value matches 'Target Value' (within Match Tolerance).\n\nTips:\n- Use this to drive gameplay logic, SFX, UI, etc.\n- If 'Fire Once Until Exit' is enabled, this event will only fire again after the dial leaves the tolerance band and re-enters.")]
		public UnityEvent onMatched;

		[Tooltip("Optional: if true, the watcher will treat this trigger as 'armed' only when the dial is NOT currently within tolerance.\nWhen the dial enters the tolerance band, the event fires and the trigger becomes disarmed until the dial exits.\n\nRecommended: true for detented dials or values that can remain steady, to avoid repeated firing.")]
		public bool fireOnceUntilExit;

		[NonSerialized]
		public bool isInsideBand;
	}

	[Header("References")]
	[Tooltip("DialInteractable to watch.\n\nIf left empty, this component will try to find a DialInteractable on the same GameObject first, then in its children.\n\nSafe prefab default:\n- Leave empty if this watcher sits on the same prefab root as the DialInteractable.")]
	[SerializeField]
	private DialInteractable dial;

	[Header("Matching")]
	[Tooltip("Absolute tolerance used when comparing the dial output value to each target value.\n\nMatch rule:\n- A trigger matches when |dialValue - targetValue| <= Match Tolerance.\n\nGuidance:\n- For exact integer detents, 0.001 to 0.01 is usually safe.\n- For noisier/continuous motion, increase slightly (e.g., 0.05).")]
	[SerializeField]
	[Min(0f)]
	private float matchTolerance;

	[Tooltip("If true, evaluates the current dial value immediately when this component enables.\n\nUseful when:\n- The dial starts at a meaningful value and you want the event to fire right away.\n\nNote:\n- This evaluation uses the same tolerance and gating logic as normal changes.")]
	[SerializeField]
	private bool evaluateOnEnable;

	[Header("Triggers")]
	[Tooltip("List of value triggers.\n\nHow to use:\n- Add as many entries as you need (2, 3, 10...).\n- Set each Target Value and hook up its On Matched event.\n\nExamples:\n- Two triggers: 0 and 1\n- Three triggers: 0, 15, 30")]
	[SerializeField]
	private List<ValueTrigger> triggers;

	private void Reset()
	{
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void HandleDialValueChanged(float value)
	{
	}

	private void Evaluate(float dialValue)
	{
	}
}
