using UnityEngine;
using UnityEngine.Events;

public class DeathEvent : MonoBehaviour
{
	private HealthHandler healthHandler;

	public UnityEvent onDeathEvents;

	private void Start()
	{
		healthHandler = base.transform.root.GetComponentInChildren<HealthHandler>();
		healthHandler.AddDieAction(Die);
	}

	public void Die()
	{
		onDeathEvents.Invoke();
	}
}
