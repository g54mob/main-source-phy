using UnityEngine;
using UnityEngine.Events;

public class LookAtTargetEventRelay : MonoBehaviour
{
	[SerializeField]
	private LookAtTarget _target;

	[SerializeField]
	private UnityEvent _onLookAt;

	[SerializeField]
	private UnityEvent _onLookAway;

	[SerializeField]
	private UnityEvent _onClickDown;

	[SerializeField]
	private UnityEvent _onClickUp;

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	private void FireOnLookAt()
	{
	}

	private void FireOnLookAway()
	{
	}

	private void FireOnClickDown()
	{
	}

	private void FireOnClickUp()
	{
	}

	private void Reset()
	{
	}
}
