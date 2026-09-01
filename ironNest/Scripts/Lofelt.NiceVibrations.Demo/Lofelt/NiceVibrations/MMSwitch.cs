using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations
{
	public class MMSwitch : MMTouchButton
	{
		public enum SwitchStates
		{
			Off = 0,
			On = 1
		}

		[Header("Switch")]
		public Image SwitchKnob;

		[Header("Knob")]
		public SwitchStates InitialState;

		public Transform OffPosition;

		public Transform OnPosition;

		public AnimationCurve KnobMovementCurve;

		public float KnobMovementDuration;

		[Header("Binding")]
		public UnityEvent SwitchOn;

		public UnityEvent SwitchOff;

		protected float _knobMovementStartedAt;

		public SwitchStates CurrentSwitchState { get; set; }

		protected override void Initialization()
		{
		}

		public virtual void InitializeState()
		{
		}

		protected override void Update()
		{
		}

		public virtual void SwitchState()
		{
		}
	}
}
