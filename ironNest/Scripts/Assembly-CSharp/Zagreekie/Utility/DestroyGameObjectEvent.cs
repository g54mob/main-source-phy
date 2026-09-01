using UnityEngine;

namespace Zagreekie.Utility
{
	[DisallowMultipleComponent]
	public sealed class DestroyGameObjectEvent : MonoBehaviour
	{
		public enum TargetMode
		{
			ThisGameObject = 0,
			SpecificGameObject = 1
		}

		public enum AutoStartMode
		{
			ManualOnly = 0,
			OnEnable = 1,
			Start = 2
		}

		[Header("Target")]
		[SerializeField]
		[Tooltip("Which GameObject will be destroyed when Trigger() runs.\n\n- ThisGameObject: destroys the GameObject this component is on.\n- SpecificGameObject: destroys the referenced Target GameObject.")]
		private TargetMode _targetMode;

		[SerializeField]
		[Tooltip("The GameObject to destroy when Target Mode is set to SpecificGameObject.\n\nIgnored when Target Mode is ThisGameObject.\nSafe default: leave null unless you explicitly want to destroy a different object.")]
		private GameObject _target;

		[Header("Timer")]
		[SerializeField]
		[Min(0f)]
		[Tooltip("Seconds to wait before destroying the target after Trigger() is called.\n\n0 = destroy on the same frame (immediate).")]
		private float _delaySeconds;

		[SerializeField]
		[Tooltip("If enabled, the timer starts automatically based on Auto Start.\n\nIf disabled, destruction only happens when Trigger() is called (e.g., from a UnityEvent).")]
		private bool _useAutoStart;

		[SerializeField]
		[Tooltip("When to automatically start the timer (only if Use Auto Start is enabled).\n\n- ManualOnly: never auto-start.\n- OnEnable: starts whenever this component's GameObject becomes enabled.\n- Start: starts once when the component starts after instantiation.")]
		private AutoStartMode _autoStart;

		[Header("Safety / Behaviour")]
		[SerializeField]
		[Tooltip("If enabled, repeated Trigger() calls while a timer is already running are ignored.\n\nRecommended for robustness with multiple UnityEvents firing.")]
		private bool _preventRetriggerWhileRunning;

		[SerializeField]
		[Tooltip("If enabled, destroying a null/missing target does nothing instead of logging a warning.\n\nUseful for pooled objects or cases where another system might already destroy the target.")]
		private bool _silentIfNoTarget;

		private bool _isRunning;

		private void OnEnable()
		{
		}

		private void Start()
		{
		}

		public void Trigger()
		{
		}

		public void Cancel()
		{
		}

		private GameObject ResolveTarget()
		{
			return null;
		}

		private void DestroyResolvedTarget()
		{
		}

		private void OnDisable()
		{
		}
	}
}
