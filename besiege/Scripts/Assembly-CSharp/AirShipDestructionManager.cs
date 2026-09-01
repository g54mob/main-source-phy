using UnityEngine;

public class AirShipDestructionManager : MonoBehaviour
{
	public GameObject ShipBody;

	public GameObject Balloons;

	private void Start()
	{
	}

	private void Update()
	{
		if (!Balloons.activeSelf)
		{
			Debug.Log("balloondead");
			ShipBody.GetComponent<Rigidbody>().isKinematic = false;
		}
	}
}
