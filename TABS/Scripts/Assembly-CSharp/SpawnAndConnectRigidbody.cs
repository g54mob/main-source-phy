using UnityEngine;

public class SpawnAndConnectRigidbody : MonoBehaviour, SetWeaponInterface
{
	public SerializedConfigurableJoint joint;

	public GameObject objectToSpawn;

	[HideInInspector]
	public GameObject spawnedObject;

	private GameObject Go()
	{
		spawnedObject = Object.Instantiate(objectToSpawn, base.transform.position, base.transform.rotation, base.transform.root);
		Rigidbody component = spawnedObject.GetComponent<Rigidbody>();
		if (!component)
		{
			spawnedObject.transform.SetParent(base.transform);
			return spawnedObject;
		}
		Weapon component2 = spawnedObject.GetComponent<Weapon>();
		if ((bool)component2 && component2.weaponAlignment == Weapon.WeaponAlignment.Up)
		{
			spawnedObject.transform.Rotate(Vector3.right * 90f);
			spawnedObject.transform.Rotate(Vector3.up * 90f);
		}
		joint.CreateJoint(component);
		return spawnedObject;
	}

	public GameObject SetWeapon(GameObject weapon, HoldingHandler.HandType handType)
	{
		objectToSpawn = weapon;
		return Go();
	}
}
