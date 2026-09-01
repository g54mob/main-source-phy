using UnityEngine;
using UnityEngine.Events;

namespace MoreMountains.Tools
{
	public class MMDebugMenuSwitch : MMTouchButton
	{
		[Header("Switch")]
		public MMDebugMenuSpriteReplace SwitchKnob;

		[MMReadOnly]
		public bool SwitchState;

		public bool InitialState;

		[Header("Binding")]
		public UnityEvent OnSwitchOn;

		public UnityEvent OnSwitchOff;

		public UnityEvent<bool> OnSwitchToggle;

		protected override void Initialization()
		{
		}

		public virtual void InitializeState()
		{
		}

		public virtual void SetTrue()
		{
		}

		public virtual void SetFalse()
		{
		}

		public virtual void ToggleState()
		{
		}
	}
}
