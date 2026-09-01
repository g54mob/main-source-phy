using UnityEngine;
using UnityEngine.Events;

public class Cooldown : MonoBehaviour
{
	public UnityEvent unityEvent;

	public float cooldown;

	private float time;

	public void Go()
	{
		if (!(Time.time < time + cooldown))
		{
			time = Time.time;
			unityEvent.Invoke();
		}
	}
}
