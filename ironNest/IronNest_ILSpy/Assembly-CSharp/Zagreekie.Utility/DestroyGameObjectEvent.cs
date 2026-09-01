using Cpp2ILInjected;
using UnityEngine;

namespace Zagreekie.Utility;

public sealed class DestroyGameObjectEvent : MonoBehaviour
{
	public enum TargetMode
	{
		ThisGameObject,
		SpecificGameObject
	}

	public enum AutoStartMode
	{
		ManualOnly,
		OnEnable,
		Start
	}

	private TargetMode _targetMode;

	private GameObject _target;

	private float _delaySeconds;

	private bool _useAutoStart;

	private AutoStartMode _autoStart = AutoStartMode.OnEnable;

	private bool _preventRetriggerWhileRunning = true;

	private bool _silentIfNoTarget;

	private bool _isRunning;

	private void OnEnable()
	{
		if (_useAutoStart && _autoStart == AutoStartMode.OnEnable)
		{
			Trigger();
		}
	}

	private void Start()
	{
		if (_useAutoStart && _autoStart == AutoStartMode.Start)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 24 Invalid \"Jump target not found in method: 0x1804EB110\"");
		}
	}

	public void Trigger()
	{
		//IL_00b5: Invalid comparison between I4 and F4
		if (_preventRetriggerWhileRunning && _isRunning)
		{
			return;
		}
		GameObject gameObject;
		if (_targetMode != TargetMode.ThisGameObject && _targetMode == TargetMode.SpecificGameObject)
		{
			gameObject = _target;
		}
		else
		{
			GameObject gameObject2 = base.gameObject;
			gameObject = gameObject2;
		}
		if (gameObject != null)
		{
			if (0f < _delaySeconds)
			{
				_isRunning = true;
				Invoke("DestroyResolvedTarget", _delaySeconds);
			}
			else
			{
				Object.Destroy(gameObject);
			}
		}
		else if (!_silentIfNoTarget)
		{
			Debug.LogWarning("DestroyGameObjectEvent: No valid target to destroy.", this);
		}
	}

	public void Cancel()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A82B]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (IsInvoking("DestroyResolvedTarget"))
		{
			CancelInvoke("DestroyResolvedTarget");
		}
		_isRunning = false;
	}

	private GameObject ResolveTarget()
	{
		if (_targetMode != TargetMode.ThisGameObject && _targetMode == TargetMode.SpecificGameObject)
		{
			return _target;
		}
		return base.gameObject;
	}

	private void DestroyResolvedTarget()
	{
		_isRunning = false;
		GameObject gameObject;
		if (_targetMode != TargetMode.ThisGameObject && _targetMode == TargetMode.SpecificGameObject)
		{
			gameObject = _target;
		}
		else
		{
			GameObject gameObject2 = base.gameObject;
			gameObject = gameObject2;
		}
		if (!(gameObject == null))
		{
			Object.Destroy(gameObject);
		}
		else if (!_silentIfNoTarget)
		{
			Debug.LogWarning("DestroyGameObjectEvent: Timer finished but target is missing.", this);
		}
	}

	private void OnDisable()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A82B]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (IsInvoking("DestroyResolvedTarget"))
		{
			CancelInvoke("DestroyResolvedTarget");
		}
		_isRunning = false;
	}
}
