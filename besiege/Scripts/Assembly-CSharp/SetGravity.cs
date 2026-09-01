using UnityEngine;

public class SetGravity : MonoBehaviour
{
	public Vector3 gravityAmount = new Vector3(0f, -9.8f, 0f);

	private void Awake()
	{
		Physics.gravity = gravityAmount;
	}
}
