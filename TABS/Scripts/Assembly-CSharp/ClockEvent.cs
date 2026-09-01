using UnityEngine;
using UnityEngine.Events;

public class ClockEvent : MonoBehaviour
{
	public int tickToSpawn = 1;

	public UnityEvent tickEvent;

	private void Start()
	{
		Clock.instance.AssignTickAction(TickEvent);
	}

	public void TickEvent()
	{
		tickEvent.Invoke();
	}
}
