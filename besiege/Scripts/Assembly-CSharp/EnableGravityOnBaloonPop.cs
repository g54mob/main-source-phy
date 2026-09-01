using UnityEngine;

public class EnableGravityOnBaloonPop : MonoBehaviour
{
	private Rigidbody targetRB;

	private void Start()
	{
		targetRB = GameObject.Find("ship").GetComponent<Rigidbody>();
		targetRB.useGravity = true;
	}
}
