using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class LookAtTargetEventRelay : MonoBehaviour
{
	private LookAtTarget _target;

	private UnityEvent _onLookAt;

	private UnityEvent _onLookAway;

	private UnityEvent _onClickDown;

	private UnityEvent _onClickUp;

	private void Start()
	{
		LookAtTarget target = _target;
		UnityAction call = FireOnLookAt;
		target.onLookAt.AddListener(call);
		LookAtTarget target2 = _target;
		UnityAction call2 = FireOnLookAway;
		target2.onLookAway.AddListener(call2);
		LookAtTarget target3 = _target;
		UnityAction call3 = FireOnClickDown;
		target3.onClickDown.AddListener(call3);
		LookAtTarget target4 = _target;
		UnityAction call4 = FireOnClickUp;
		target4.onClickUp.AddListener(call4);
	}

	private void OnDestroy()
	{
		LookAtTarget target = _target;
		UnityAction call = FireOnLookAt;
		target.onLookAt.RemoveListener(call);
		LookAtTarget target2 = _target;
		UnityAction call2 = FireOnLookAway;
		target2.onLookAway.RemoveListener(call2);
		LookAtTarget target3 = _target;
		UnityAction call3 = FireOnClickDown;
		target3.onClickDown.RemoveListener(call3);
		LookAtTarget target4 = _target;
		UnityAction call4 = FireOnClickUp;
		target4.onClickUp.RemoveListener(call4);
	}

	private void FireOnLookAt()
	{
		if (_onLookAt != null)
		{
			_onLookAt.Invoke();
		}
	}

	private void FireOnLookAway()
	{
		if (_onLookAway != null)
		{
			_onLookAway.Invoke();
		}
	}

	private void FireOnClickDown()
	{
		if (_onClickDown != null)
		{
			_onClickDown.Invoke();
		}
	}

	private void FireOnClickUp()
	{
		if (_onClickUp != null)
		{
			_onClickUp.Invoke();
		}
	}

	private void Reset()
	{
		if (_target == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			LookAtTarget target = default(LookAtTarget);
			_target = target;
		}
	}
}
