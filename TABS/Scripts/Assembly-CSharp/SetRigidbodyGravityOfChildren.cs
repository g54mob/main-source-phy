using UnityEngine;

public class SetRigidbodyGravityOfChildren : MonoBehaviour
{
	private Rigidbody[] rigs;

	private void Start()
	{
		rigs = GetComponentsInChildren<Rigidbody>();
	}

	public void SetGravityTrue()
	{
		for (int i = 0; i < rigs.Length; i++)
		{
			rigs[i].useGravity = true;
		}
	}
}
