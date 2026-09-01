using UnityEngine;
using UnityEngine.Events;

public class UpdateEvent : MonoBehaviour
{
	public UnityEvent updateEvent;

	private void Update()
	{
		updateEvent.Invoke();
	}
}
