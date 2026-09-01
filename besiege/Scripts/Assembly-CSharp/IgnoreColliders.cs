using UnityEngine;

public class IgnoreColliders : MonoBehaviour
{
	public Collider[] targets;

	private void Start()
	{
		Collider component = GetComponent<Collider>();
		for (int i = 0; i < targets.Length; i++)
		{
			Physics.IgnoreCollision(targets[i], component);
		}
	}

	private void OnDestroy()
	{
		Collider component = GetComponent<Collider>();
		for (int i = 0; i < targets.Length; i++)
		{
			Physics.IgnoreCollision(targets[i], component, false);
		}
	}
}
