using UnityEngine;

public class Hourglass : MonoBehaviour
{
	public Transform HourglassObject;

	public Rigidbody HourglassPhysics;

	public GameObject ThingToSpawn;

	public GameObject ThingsToDespawn;

	private void Start()
	{
	}

	private void Update()
	{
		if ((double)HourglassObject.rotation.x > -0.01)
		{
			Debug.Log("Hey");
			ThingToSpawn.SetActive(true);
			ThingsToDespawn.SetActive(false);
			HourglassPhysics.constraints = RigidbodyConstraints.FreezeAll;
		}
	}
}
