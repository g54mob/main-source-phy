using System.Collections;
using UnityEngine;

public class SetGravityAsWind : MonoBehaviour
{
	public float gravityAmountZ = -20f;

	private IEnumerator Start()
	{
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, (!StatMaster.levelSimulating) ? 0f : gravityAmountZ);
	}
}
