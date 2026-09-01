using UnityEngine;
using UnityEngine.Events;

public class Unparent : MonoBehaviour
{
	private GameObject parent;

	public bool resetRotation;

	public bool destoryWithParent = true;

	public bool unparentOnStart = true;

	public float removeAfterSeconds;

	private DestroyEvent m_destroyEvent;

	public UnityEvent unparentEvent;

	public UnityEvent parentDestoryEvent;

	private void Start()
	{
		if (unparentOnStart)
		{
			Go();
		}
	}

	public void Go()
	{
		unparentEvent?.Invoke();
		parent = base.transform.parent.gameObject;
		m_destroyEvent = parent.FetchComponent<DestroyEvent>();
		m_destroyEvent.AddDestroyAction(Die);
		m_destroyEvent.AddDestroyAction(InvokeParentDestoryEvent);
		base.transform.SetParent(null, worldPositionStays: true);
		if (removeAfterSeconds != 0f)
		{
			base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = removeAfterSeconds;
		}
		if (resetRotation)
		{
			base.transform.rotation = Quaternion.identity;
		}
	}

	private void OnDestroy()
	{
		if (m_destroyEvent != null)
		{
			m_destroyEvent.RemoveDestroyAction(Die);
		}
	}

	public void Die()
	{
		if (destoryWithParent)
		{
			Object.Destroy(base.gameObject);
		}
	}

	public void InvokeParentDestoryEvent()
	{
		parentDestoryEvent?.Invoke();
	}
}
