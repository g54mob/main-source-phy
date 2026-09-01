using System.Collections.Generic;
using UnityEngine;

public class CogFreeController : MonoBehaviour
{
	public ConfigurableJoint myJoint;

	public Transform rayPos;

	public float rayLength = 1.5f;

	public LayerMask layerMasky;

	public ConfigurableJoint connectedCog;

	public int drivenCogNeighbourCount;

	public List<Transform> hitCogs = new List<Transform>();

	public Rigidbody myDriver;

	private Machine machine;

	private int cogLayer = 17;

	private RaycastHit hit;

	private void Start()
	{
		machine = GetComponentInParent<Machine>();
	}

	private void SetDriver(Rigidbody driver)
	{
		myDriver = driver;
		RayOut();
	}

	private void RayOut()
	{
		hitCogs.Clear();
		if (Physics.Raycast(rayPos.position, -rayPos.up, out hit, rayLength, layerMasky) && hit.collider.gameObject.layer == cogLayer)
		{
			AssignCogNeighbour();
		}
		if (Physics.Raycast(rayPos.position, rayPos.up, out hit, rayLength, layerMasky) && hit.collider.gameObject.layer == cogLayer)
		{
			AssignCogNeighbour();
		}
		if (Physics.Raycast(rayPos.position, -rayPos.right, out hit, rayLength, layerMasky) && hit.collider.gameObject.layer == cogLayer)
		{
			AssignCogNeighbour();
		}
		if (Physics.Raycast(rayPos.position, rayPos.right, out hit, rayLength, layerMasky) && hit.collider.gameObject.layer == cogLayer)
		{
			AssignCogNeighbour();
		}
		SetNonDrivenCogs();
	}

	private void AssignCogNeighbour()
	{
		if (hit.collider.transform.parent.GetComponent<Rigidbody>() != myDriver)
		{
			hitCogs.Add(hit.collider.transform.parent);
		}
	}

	private void SetNonDrivenCogs()
	{
		for (int i = 0; i < hitCogs.Count; i++)
		{
			hitCogs[i].gameObject.GetComponent<CogFreeController>().SetDriver(GetComponent<Rigidbody>());
		}
	}

	private void Update()
	{
		if ((bool)machine && machine.isSimulating && myJoint != null && myDriver != null)
		{
			myJoint.targetAngularVelocity = new Vector3(0f - myDriver.transform.InverseTransformDirection(myDriver.angularVelocity).z, myJoint.targetAngularVelocity.y, myJoint.targetAngularVelocity.z);
		}
	}
}
