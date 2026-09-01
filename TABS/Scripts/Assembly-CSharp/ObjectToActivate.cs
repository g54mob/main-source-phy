using UnityEngine;
using UnityEngine.Events;

public class ObjectToActivate : MonoBehaviour
{
	public UnityEvent firstActivateEvent;

	public UnityEvent secondActivateEvent;

	private void Start()
	{
	}

	public void FirstActivateEvent()
	{
		firstActivateEvent.Invoke();
	}

	public void SecondActivateEvent()
	{
		secondActivateEvent.Invoke();
	}
}
