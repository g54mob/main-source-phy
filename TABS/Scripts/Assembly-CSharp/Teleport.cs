using Landfall.TABS;
using UnityEngine;

public class Teleport : MonoBehaviour
{
	public enum Dir
	{
		Backwards = 0,
		Forward = 1,
		AwayFromTarget = 2
	}

	public float distance;

	public Dir direction;

	private DataHandler data;

	private void Start()
	{
		data = base.transform.root.GetComponent<Unit>().data;
	}

	public void Go()
	{
		Vector3 vector = data.characterForwardObject.forward;
		if (direction == Dir.Backwards)
		{
			vector = data.characterForwardObject.forward * -1f;
		}
		if (direction == Dir.AwayFromTarget)
		{
			vector = ((!data.targetMainRig) ? (data.characterForwardObject.forward * -1f) : (data.mainRig.position - data.targetMainRig.position));
		}
		vector = vector.normalized;
		Vector3 vector2 = base.transform.root.position + vector * distance;
		RaycastHit hitInfo = default(RaycastHit);
		Physics.Raycast(new Ray(vector2 + Vector3.up * 10f, Vector3.down), out hitInfo, 20f);
		if ((bool)hitInfo.transform)
		{
			Vector3 vector3 = hitInfo.point + Vector3.up * 1.5f - data.mainRig.position;
			base.transform.root.position += vector3;
		}
	}
}
