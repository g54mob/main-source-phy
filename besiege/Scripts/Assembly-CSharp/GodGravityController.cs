using UnityEngine;

public class GodGravityController : MonoBehaviour
{
	public float startGrav = -32.81f;

	public bool zeroG;

	private void Start()
	{
		startGrav = Physics.gravity.y;
	}

	private void Update()
	{
		if (Input.GetKeyDown("g"))
		{
			StatMaster.GodTools.GravityDisabled = !StatMaster.GodTools.GravityDisabled;
			if (StatMaster.GodTools.GravityDisabled)
			{
				Physics.gravity = new Vector3(Physics.gravity.x, 0f, Physics.gravity.z);
			}
			else
			{
				Physics.gravity = new Vector3(Physics.gravity.x, startGrav, Physics.gravity.z);
			}
		}
	}
}
