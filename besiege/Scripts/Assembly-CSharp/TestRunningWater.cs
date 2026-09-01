using System;
using UnityEngine;

public class TestRunningWater : MonoBehaviour
{
	public Material addMaterial;

	public bool addedWater;

	private void Update()
	{
		if (!addMaterial)
		{
			return;
		}
		if (!addedWater && Input.GetKeyUp(KeyCode.P))
		{
			addedWater = true;
			MeshRenderer[] array = (MeshRenderer[])UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer));
			for (int i = 0; i < array.Length; i++)
			{
				Material[] sharedMaterials = array[i].sharedMaterials;
				Material[] array2 = new Material[sharedMaterials.Length + 1];
				sharedMaterials.CopyTo(array2, 0);
				array2[sharedMaterials.Length] = addMaterial;
				array[i].sharedMaterials = array2;
			}
			SkinnedMeshRenderer[] array3 = (SkinnedMeshRenderer[])UnityEngine.Object.FindObjectsOfType(typeof(SkinnedMeshRenderer));
			for (int j = 0; j < array.Length; j++)
			{
				Material[] sharedMaterials2 = array3[j].sharedMaterials;
				Material[] array4 = new Material[sharedMaterials2.Length + 1];
				sharedMaterials2.CopyTo(array4, 0);
				array4[sharedMaterials2.Length] = addMaterial;
				array3[j].sharedMaterials = array4;
			}
		}
		else if (addedWater && Input.GetKeyUp(KeyCode.P))
		{
			addedWater = false;
			MeshRenderer[] array5 = (MeshRenderer[])UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer));
			for (int k = 0; k < array5.Length; k++)
			{
				Material[] sharedMaterials3 = array5[k].sharedMaterials;
				Material[] array6 = new Material[sharedMaterials3.Length - 1];
				Array.Copy(sharedMaterials3, 0, array6, 0, array6.Length);
				array5[k].sharedMaterials = array6;
			}
			SkinnedMeshRenderer[] array7 = (SkinnedMeshRenderer[])UnityEngine.Object.FindObjectsOfType(typeof(SkinnedMeshRenderer));
			for (int l = 0; l < array5.Length; l++)
			{
				Material[] sharedMaterials4 = array7[l].sharedMaterials;
				Material[] array8 = new Material[sharedMaterials4.Length - 1];
				Array.Copy(sharedMaterials4, 0, array8, 0, array8.Length);
				array7[l].sharedMaterials = array8;
			}
		}
	}
}
