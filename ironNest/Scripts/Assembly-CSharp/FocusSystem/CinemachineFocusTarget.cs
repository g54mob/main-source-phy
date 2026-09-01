using UnityEngine;
using UnityEngine.Events;

namespace FocusSystem
{
	[DisallowMultipleComponent]
	public class CinemachineFocusTarget : MonoBehaviour
	{
		[Header("Key (Channel)")]
		[Tooltip("Focus key (channel) for this target. MUST match a key configured in CinemachineFocusService.\nFormat rules (strict):\n- Any non-empty string\n- Leading/trailing whitespace is trimmed\n- Case-sensitive comparisons\n- Whitespace within the string is allowed\nSafe examples:\n- \"Player\"\n- \"BossRoom\"\nOnly configured keys can grab focus; unknown keys are ignored with a warning.")]
		public string key;

		[Header("Priority")]
		[Tooltip("Higher values have higher priority. When multiple targets request focus simultaneously, the one with the highest priority is selected. Ties are broken deterministically.")]
		public int priority;

		[Header("Overrides")]
		[Tooltip("If true (default), this target is allowed to override/bind the camera's Follow when it becomes focused.\nIf false, the service will not bind Follow for this target (Follow will be set to null if the service is set to bind Follow).\nNotes:\n- The service's global 'Bind Follow' setting must also be enabled, otherwise Follow won't be bound regardless.\n- This only affects the binding while THIS target is the active focus.")]
		public bool overrideFollow;

		[Tooltip("If true (default), this target is allowed to override/bind the camera's LookAt when it becomes focused.\nIf false, the service will not bind LookAt for this target (LookAt will be set to null if the service is set to bind LookAt).\nNotes:\n- The service's global 'Bind LookAt' setting must also be enabled, otherwise LookAt won't be bound regardless.\n- This only affects the binding while THIS target is the active focus.")]
		public bool overrideLookAt;

		[Tooltip("Transform used for the camera's LookAt binding.\n- If null, defaults to this.transform.\n- Configure the service to bind LookAt for this to be used.\n- Also requires this target's 'Override LookAt' to be enabled.")]
		public Transform lookAtOverride;

		[Tooltip("Transform used for the camera's Follow binding.\n- If null, defaults to this.transform.\n- Configure the service to bind Follow for this to be used.\n- Also requires this target's 'Override Follow' to be enabled.")]
		public Transform followOverride;

		[Header("Animator / Remote Toggle")]
		[Tooltip("Drive this bool via Animator, script, or the RemoteSetToggle() method.\n- When true: the target requests camera focus for its key.\n- When false: the target releases focus.\nThe service determines if/when the request is granted based on key eligibility, usage limits, and priority.")]
		public bool ToggleOn;

		[Header("Events")]
		[Tooltip("Invoked when this target successfully becomes the current camera focus (i.e., the service binds to it).")]
		public UnityEvent onFocusGrabbed;

		[Tooltip("Invoked when this target releases focus (ToggleOn becomes false) or loses focus due to disable/unregister.")]
		public UnityEvent onFocusReleased;

		private bool _lastToggleState;

		private bool _isRegistered;

		private bool _warnedUnknownKey;

		public Transform LookAtTransform => null;

		public Transform FollowTransform => null;

		private void Awake()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void Update()
		{
		}

		private void ApplyIfChanged(bool force)
		{
		}

		private void TryRegister()
		{
		}

		private void MaybeWarnUnknownKey()
		{
		}

		public void RemoteSetToggle(bool state)
		{
		}

		[ContextMenu("Set True (Request Focus)")]
		private void ContextSetTrue()
		{
		}

		[ContextMenu("Set False (Release Focus)")]
		private void ContextSetFalse()
		{
		}

		[ContextMenu("Force Refresh")]
		private void ContextForceRefresh()
		{
		}
	}
}
