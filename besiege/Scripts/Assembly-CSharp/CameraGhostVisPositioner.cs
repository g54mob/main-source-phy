using UnityEngine;

public class CameraGhostVisPositioner : MonoBehaviour
{
	private float y;

	private float z;

	private void Start()
	{
		if (y == 0f || y == 0f)
		{
			y = base.transform.localPosition.y;
			z = base.transform.localPosition.z;
		}
	}

	private void Update()
	{
		if (!Machine.Active().isSimulating)
		{
			Transform buildingMachine = Machine.Active().BuildingMachine;
			if (Vector3.Angle(-base.transform.parent.forward, buildingMachine.up) < 45f)
			{
				base.transform.rotation = Quaternion.LookRotation(base.transform.parent.up, buildingMachine.up);
				base.transform.localPosition = new Vector3(0f, z, 0f - y);
			}
			else if (Vector3.Angle(-base.transform.parent.forward, -buildingMachine.up) < 45f)
			{
				base.transform.rotation = Quaternion.LookRotation(base.transform.parent.up, buildingMachine.up);
				base.transform.localPosition = new Vector3(0f, z, y);
			}
			else
			{
				base.transform.rotation = Quaternion.LookRotation(base.transform.parent.forward, buildingMachine.up);
				base.transform.localPosition = new Vector3(0f, y, z);
			}
		}
	}
}
