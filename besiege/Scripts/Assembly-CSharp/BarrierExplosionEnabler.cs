using System.Collections.Generic;
using UnityEngine;

public class BarrierExplosionEnabler : MonoBehaviour
{
	public List<EntityAI> Targets;

	public int targetsToEliminate;

	public GameObject barrierToEnable;

	public GameObject objectToSteal;

	[HideInInspector]
	public int targetEliminated;

	private void Start()
	{
	}

	private void Update()
	{
		if (StatMaster.levelSimulating && targetEliminated >= targetsToEliminate)
		{
			barrierToEnable.SetActive(true);
			base.gameObject.SetActive(false);
			objectToSteal.AddComponent<Rigidbody>();
			objectToSteal.AddComponent<SphereCollider>();
		}
	}
}
