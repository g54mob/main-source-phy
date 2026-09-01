using UnityEngine;
using UnityEngine.Events;

namespace Zagreekie.Tools
{
	[AddComponentMenu("Zagreekie/Combat/Armed Fire Relay (One Shot)")]
	public sealed class ArmedFireRelayOneShot : MonoBehaviour
	{
		[Header("Outputs (Invoked on TriggerFire if armed)")]
		[Tooltip("Invoked when TriggerFire() is called AND Left is armed.\n\nOne-shot behavior:\n- If Left is armed at the moment TriggerFire() runs, this event is invoked once.\n- Left then automatically disarms during the same TriggerFire() call.\n\nTypical listeners (wired in Inspector):\n- Weapon.FireLeft()\n- Play left muzzle VFX\n- Play left audio\n- Spawn left projectile")]
		[SerializeField]
		private UnityEvent _fireLeft;

		[Tooltip("Invoked when TriggerFire() is called AND Right is armed.\n\nOne-shot behavior:\n- If Right is armed at the moment TriggerFire() runs, this event is invoked once.\n- Right then automatically disarms during the same TriggerFire() call.\n\nTypical listeners (wired in Inspector):\n- Weapon.FireRight()\n- Play right muzzle VFX\n- Play right audio\n- Spawn right projectile")]
		[SerializeField]
		private UnityEvent _fireRight;

		[Header("State Change Events (Invoked only when state actually changes)")]
		[Tooltip("Invoked when Left transitions from Disarmed -> Armed.\n\nTriggered by:\n- ArmLeft()\n- ArmBoth()\n- ToggleLeft() (only if it ends up arming Left)\n\nNot triggered if Left is already armed (no state change).")]
		[SerializeField]
		private UnityEvent _leftArmedEvent;

		[Tooltip("Invoked when Left transitions from Armed -> Disarmed.\n\nTriggered by:\n- DisarmLeft()\n- DisarmAll()\n- ToggleLeft() (only if it ends up disarming Left)\n- TriggerFire() if Left was armed (one-shot auto-disarm)\n- OnEnable() if 'Clear On Enable' is enabled and Left was armed\n\nNot triggered if Left is already disarmed (no state change).")]
		[SerializeField]
		private UnityEvent _leftDisarmedEvent;

		[Tooltip("Invoked when Right transitions from Disarmed -> Armed.\n\nTriggered by:\n- ArmRight()\n- ArmBoth()\n- ToggleRight() (only if it ends up arming Right)\n\nNot triggered if Right is already armed (no state change).")]
		[SerializeField]
		private UnityEvent _rightArmedEvent;

		[Tooltip("Invoked when Right transitions from Armed -> Disarmed.\n\nTriggered by:\n- DisarmRight()\n- DisarmAll()\n- ToggleRight() (only if it ends up disarming Right)\n- TriggerFire() if Right was armed (one-shot auto-disarm)\n- OnEnable() if 'Clear On Enable' is enabled and Right was armed\n\nNot triggered if Right is already disarmed (no state change).")]
		[SerializeField]
		private UnityEvent _rightDisarmedEvent;

		[Header("Aggregate State Change Events (Invoked only when aggregate state actually changes)")]
		[Tooltip("Invoked when the relay transitions from NONE armed -> ANY armed.\n\nMeaning:\n- Fires when the first side becomes armed (Left, Right, or both).\n- Does NOT fire when transitioning from Left-only -> Both (still 'any armed').\n- Does NOT fire if already armed and you arm again (no aggregate state change).\n\nTypical uses:\n- Enable 'armed' UI indicator\n- Start aiming/charge VFX\n- Play 'armed' audio cue")]
		[SerializeField]
		private UnityEvent _anyArmedEvent;

		[Tooltip("Invoked when the relay transitions from ANY armed -> NONE armed (all guns disarmed).\n\nTriggered by:\n- DisarmLeft()/DisarmRight()/DisarmAll() when they result in no sides armed\n- TriggerFire() one-shot auto-disarm (when it spends the last armed side)\n- OnEnable() if 'Clear On Enable' is enabled and anything was armed\n\nDoes NOT fire if already fully disarmed (no aggregate state change).\n\nTypical uses:\n- Disable 'armed' UI indicator\n- Stop aiming/charge VFX\n- Play 'spent/empty' audio cue")]
		[SerializeField]
		private UnityEvent _allDisarmedEvent;

		[Header("Runtime State (Debug visibility)")]
		[Tooltip("True while Left is currently armed.\n\nThis is serialized for Inspector visibility and prefab defaults, but should normally be changed via:\n- ArmLeft()/DisarmLeft()/ToggleLeft()\n- ArmBoth()/DisarmAll()\n- TriggerFire() (auto-disarm one-shot)\n- OnEnable() if Clear On Enable is enabled")]
		[SerializeField]
		private bool _leftArmed;

		[Tooltip("True while Right is currently armed.\n\nThis is serialized for Inspector visibility and prefab defaults, but should normally be changed via:\n- ArmRight()/DisarmRight()/ToggleRight()\n- ArmBoth()/DisarmAll()\n- TriggerFire() (auto-disarm one-shot)\n- OnEnable() if Clear On Enable is enabled")]
		[SerializeField]
		private bool _rightArmed;

		[Header("Options")]
		[Tooltip("If true, any armed side(s) will be disarmed BEFORE invoking FireLeft/FireRight.\n\nWhy this is useful:\n- Prevents re-entrancy issues if a Fire listener calls TriggerFire() again.\n- Ensures callbacks observe the relay as already 'spent' (disarmed).\n\nIf false, disarming happens AFTER the Fire events are invoked.\n\nSafe default: enabled.")]
		[SerializeField]
		private bool _disarmBeforeInvoke;

		[Tooltip("If true, both sides will be disarmed when this component becomes enabled.\n\nRecommended when using pooling or toggling GameObjects/components on/off, to avoid 'stale' armed state.\n\nIf you want arming to persist across disable/enable, turn this off.\n\nSafe default: enabled.")]
		[SerializeField]
		private bool _clearOnEnable;

		private void OnEnable()
		{
		}

		public void ArmLeft()
		{
		}

		public void ArmRight()
		{
		}

		public void ArmBoth()
		{
		}

		public void DisarmLeft()
		{
		}

		public void DisarmRight()
		{
		}

		public void DisarmAll()
		{
		}

		public void ToggleLeft()
		{
		}

		public void ToggleRight()
		{
		}

		public void TriggerFire()
		{
		}

		public bool IsLeftArmed()
		{
			return false;
		}

		public bool IsRightArmed()
		{
			return false;
		}

		public bool IsAnyArmed()
		{
			return false;
		}

		private void SetLeftArmed(bool armed)
		{
		}

		private void SetRightArmed(bool armed)
		{
		}

		private void EmitAggregateStateChangeEvents(bool wasAnyArmed)
		{
		}
	}
}
