using UnityEngine;

public class MoonGravButton : MonoBehaviour
{
	public float startGrav;

	public float newGrav;

	public float duration = 15f;

	public bool lowGravEnabled;

	private void Start()
	{
		startGrav = Physics.gravity.y;
	}

	private void SetGravity()
	{
		if (!lowGravEnabled)
		{
			Physics.gravity = new Vector3(Physics.gravity.x, 0f, Physics.gravity.z);
		}
		else
		{
			Physics.gravity = new Vector3(Physics.gravity.x, startGrav, Physics.gravity.z);
		}
	}
}
