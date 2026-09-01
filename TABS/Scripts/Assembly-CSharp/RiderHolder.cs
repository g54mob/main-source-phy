using System.Collections.Generic;
using UnityEngine;

public class RiderHolder : MonoBehaviour
{
	public List<GameObject> riders;

	public void Addriders(List<GameObject> spawnedRiders)
	{
		riders = spawnedRiders;
	}
}
