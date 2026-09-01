using UnityEngine;

public class SetGravityMenu : MonoBehaviour
{
	private void Start()
	{
		Physics.gravity = new Vector3(Physics.gravity.x, -32.81f, Physics.gravity.z);
	}
}
