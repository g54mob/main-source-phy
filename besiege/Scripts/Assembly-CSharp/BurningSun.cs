using System.Collections.Generic;
using UnityEngine;

public class BurningSun : MonoBehaviour
{
	public GameObject machine;

	public bool machineFound;

	public float timer;

	public float storedTimer;

	private FireTag[] fireTags;

	public List<FireTag> fireTagsList;

	private void Start()
	{
		storedTimer = timer;
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		if (!machineFound)
		{
			machine = GameObject.Find("Simulation Machine");
			machineFound = true;
			fireTags = machine.GetComponentsInChildren<FireTag>();
			FireTag[] array = fireTags;
			foreach (FireTag item in array)
			{
				fireTagsList.Add(item);
			}
		}
		if (fireTagsList.Count != 0)
		{
			timer -= Time.deltaTime;
			if (timer <= 0f)
			{
				SetBlocksOnFire();
			}
		}
	}

	private void SetBlocksOnFire()
	{
		foreach (FireTag fireTags in fireTagsList)
		{
			fireTags.Ignite(1f);
		}
		timer = storedTimer;
	}
}
