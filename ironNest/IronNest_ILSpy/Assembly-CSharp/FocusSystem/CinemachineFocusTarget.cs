using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

namespace FocusSystem;

public class CinemachineFocusTarget : MonoBehaviour
{
	public string key;

	public int priority;

	public bool overrideFollow;

	public bool overrideLookAt;

	public Transform lookAtOverride;

	public Transform followOverride;

	public bool ToggleOn;

	public UnityEvent onFocusGrabbed;

	public UnityEvent onFocusReleased;

	private bool _lastToggleState;

	private bool _isRegistered;

	private bool _warnedUnknownKey;

	public Transform LookAtTransform
	{
		get
		{
			if ((bool)lookAtOverride)
			{
				return lookAtOverride;
			}
			return base.transform;
		}
	}

	public Transform FollowTransform
	{
		get
		{
			if ((bool)followOverride)
			{
				return followOverride;
			}
			return base.transform;
		}
	}

	private void Awake()
	{
		bool lastToggleState = !ToggleOn;
		_lastToggleState = lastToggleState;
	}

	private void OnEnable()
	{
		if (!_isRegistered)
		{
			if (CinemachineFocusService.HasInstance)
			{
				CinemachineFocusService._003CInstance_003Ek__BackingField.RegisterTarget(this);
				_isRegistered = true;
				ApplyIfChanged(force: true);
				return;
			}
			string text = base.name;
			string message = "[CinemachineFocusTarget] No CinemachineFocusService instance found for '" + text + "'. Will remain unregistered until the service exists.";
			Debug.LogWarning(message);
		}
		ApplyIfChanged(force: true);
	}

	private void OnDisable()
	{
		if (ToggleOn && CinemachineFocusService.HasInstance)
		{
			CinemachineFocusService._003CInstance_003Ek__BackingField.ReleaseFocus(this);
			if (onFocusReleased != null)
			{
				onFocusReleased.Invoke();
			}
		}
		if (_isRegistered)
		{
			if ((object)CinemachineFocusService._003CInstance_003Ek__BackingField != null)
			{
				CinemachineFocusService._003CInstance_003Ek__BackingField.UnregisterTarget(this);
			}
			_isRegistered = false;
		}
	}

	private void Update()
	{
		ApplyIfChanged(force: false);
	}

	private void ApplyIfChanged(bool force)
	{
		if (!force && ToggleOn == _lastToggleState)
		{
			return;
		}
		_lastToggleState = ToggleOn;
		UnityEvent unityEvent;
		if (!ToggleOn)
		{
			if (CinemachineFocusService.HasInstance)
			{
				CinemachineFocusService._003CInstance_003Ek__BackingField.ReleaseFocus(this);
			}
			unityEvent = onFocusReleased;
		}
		else
		{
			if (!CinemachineFocusService.HasInstance)
			{
				string text = base.name;
				string message = "[CinemachineFocusTarget] No CinemachineFocusService instance found when '" + text + "' requested focus.";
				Debug.LogWarning(message);
				return;
			}
			bool flag = CinemachineFocusService._003CInstance_003Ek__BackingField.RequestFocus(this);
			if (!flag)
			{
				if (_warnedUnknownKey == flag)
				{
					_warnedUnknownKey = true;
				}
				return;
			}
			unityEvent = onFocusGrabbed;
		}
		unityEvent?.Invoke();
	}

	private void TryRegister()
	{
		if (!_isRegistered)
		{
			if (!CinemachineFocusService.HasInstance)
			{
				string text = base.name;
				string message = "[CinemachineFocusTarget] No CinemachineFocusService instance found for '" + text + "'. Will remain unregistered until the service exists.";
				Debug.LogWarning(message);
			}
			else
			{
				CinemachineFocusService._003CInstance_003Ek__BackingField.RegisterTarget(this);
				_isRegistered = true;
			}
		}
	}

	private void MaybeWarnUnknownKey()
	{
		if (!_warnedUnknownKey)
		{
			_warnedUnknownKey = true;
		}
	}

	public void RemoteSetToggle(bool state)
	{
		ToggleOn = state;
		ApplyIfChanged(force: true);
	}

	private void ContextSetTrue()
	{
		ToggleOn = true;
		ApplyIfChanged(force: true);
	}

	private void ContextSetFalse()
	{
		ToggleOn = false;
		_lastToggleState = ToggleOn;
		UnityEvent unityEvent;
		if (!ToggleOn)
		{
			if (CinemachineFocusService.HasInstance)
			{
				CinemachineFocusService._003CInstance_003Ek__BackingField.ReleaseFocus(this);
			}
			unityEvent = onFocusReleased;
		}
		else
		{
			if (!CinemachineFocusService.HasInstance)
			{
				string text = base.name;
				string message = "[CinemachineFocusTarget] No CinemachineFocusService instance found when '" + text + "' requested focus.";
				Debug.LogWarning(message);
				return;
			}
			bool flag = CinemachineFocusService._003CInstance_003Ek__BackingField.RequestFocus(this);
			if (!flag)
			{
				if (_warnedUnknownKey == flag)
				{
					_warnedUnknownKey = true;
				}
				return;
			}
			unityEvent = onFocusGrabbed;
		}
		unityEvent?.Invoke();
	}

	private void ContextForceRefresh()
	{
		ApplyIfChanged(force: true);
	}

	public CinemachineFocusTarget()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A84C]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		key = "Player";
		overrideFollow = true;
		base._002Ector();
	}
}
