using System.Collections.Generic;
using UnityChan;
using UnityEngine;

public class SpringBoneAssistant : MonoBehaviour
{
	public SpringManager mSpringManager;

	public Vector3 mTargetBoneAxis = new Vector3(0f, 1f, 0f);

	public void MarkChildren()
	{
		SpringBoneMarker[] array = Object.FindObjectsOfType<SpringBoneMarker>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].MarkChildren();
		}
		array = Object.FindObjectsOfType<SpringBoneMarker>();
		List<SpringBone> list = new List<SpringBone>();
		for (int j = 0; j < array.Length; j++)
		{
			list.Add(array[j].AddSpringBone());
			list[j].boneAxis = mTargetBoneAxis;
			array[j].UnmarkSelf();
		}
		mSpringManager.springBones = list.ToArray();
	}

	public void CleanUp()
	{
		SpringBoneMarker[] array = Object.FindObjectsOfType<SpringBoneMarker>();
		SpringBone[] array2 = Object.FindObjectsOfType<SpringBone>();
		for (int i = 0; i < array.Length; i++)
		{
			Object.DestroyImmediate(array[i]);
		}
		for (int j = 0; j < array2.Length; j++)
		{
			Object.DestroyImmediate(array2[j]);
		}
	}
}
