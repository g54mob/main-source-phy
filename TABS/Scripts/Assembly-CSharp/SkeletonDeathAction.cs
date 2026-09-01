using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeathAction : MonoBehaviour
{
	private DisableAllSkinnedClothes disableAllSkinnedClothes;

	private HealthHandler healthHandler;

	private List<MeshRenderer> meshes;

	private List<BoxCollider> boxColliders;

	private List<SetParent> setParents;

	private void Start()
	{
		healthHandler = base.transform.root.GetComponentInChildren<HealthHandler>();
		disableAllSkinnedClothes = base.transform.root.GetComponent<DisableAllSkinnedClothes>();
		meshes = new List<MeshRenderer>();
		meshes.AddRange(GetComponentsInChildren<MeshRenderer>());
		boxColliders = new List<BoxCollider>();
		boxColliders.AddRange(GetComponentsInChildren<BoxCollider>());
		setParents = new List<SetParent>();
		setParents.AddRange(GetComponentsInChildren<SetParent>());
		StartAction();
		if ((bool)healthHandler)
		{
			healthHandler.AddDieAction(OnDeathAction);
		}
	}

	private void StartAction()
	{
		for (int i = 0; i < meshes.Count; i++)
		{
			if (meshes[i].enabled)
			{
				meshes[i].enabled = false;
			}
		}
		for (int j = 0; j < boxColliders.Count; j++)
		{
			if (boxColliders[j].enabled)
			{
				boxColliders[j].enabled = false;
			}
		}
		for (int k = 0; k < setParents.Count; k++)
		{
			setParents[k].Doit();
		}
	}

	private void OnDeathAction()
	{
		if ((bool)disableAllSkinnedClothes)
		{
			disableAllSkinnedClothes.DoIt();
		}
		for (int i = 0; i < meshes.Count; i++)
		{
			meshes[i].enabled = true;
		}
		for (int j = 0; j < boxColliders.Count; j++)
		{
			boxColliders[j].enabled = true;
		}
	}
}
