using UnityEngine;
using UnityEngine.Events;

public class AlternatingSpawnPos : MonoBehaviour
{
	public bool useLastSpawnEvent = true;

	public UnityEvent spawnEvent;

	public UnityEvent LastSpawnEvent;

	private void Start()
	{
	}

	public void InvokeSpawnEvent()
	{
		spawnEvent?.Invoke();
	}

	public void InvokeLastSpawnEvent()
	{
		LastSpawnEvent?.Invoke();
	}
}
