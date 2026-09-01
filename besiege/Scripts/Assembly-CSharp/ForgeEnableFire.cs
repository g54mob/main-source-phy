using System.Collections.Generic;
using UnityEngine;

public class ForgeEnableFire : MonoBehaviour
{
	public FireController fireController;

	private List<StructuralPhysTile> physTile = new List<StructuralPhysTile>();

	private void Start()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		StructuralPhysTile component = other.attachedRigidbody.GetComponent<StructuralPhysTile>();
		if (!physTile.Contains(component))
		{
			physTile.Add(component);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		StructuralPhysTile component = other.attachedRigidbody.GetComponent<StructuralPhysTile>();
		physTile.Remove(component);
	}

	private void Update()
	{
		if (!fireController.onFire)
		{
			return;
		}
		foreach (StructuralPhysTile item in physTile)
		{
			item.enabled = true;
			item.DestroyTile(item.basicInfo.Rigidbody.velocity);
		}
	}
}
