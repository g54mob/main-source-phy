using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Pine/FinishLevelShot")]
public class FinishLevelShot : MonoBehaviour
{
	public List<Transform> poseParents = new List<Transform>();

	public RuntimeAnimatorController animatorController;

	public Vector3 eyeLocalPosition;

	public Vector3 eyeLocalRotation;

	public Vector3 eyePosition
	{
		get
		{
			Transform obj = base.transform;
			Vector3 lossyScale = obj.lossyScale;
			Vector3 vector = obj.right * (eyeLocalPosition.x * lossyScale.x);
			Vector3 vector2 = obj.up * (eyeLocalPosition.y * lossyScale.y);
			Vector3 vector3 = obj.forward * (eyeLocalPosition.z * lossyScale.z);
			return obj.position + vector + vector2 + vector3;
		}
		set
		{
			eyeLocalPosition = base.transform.InverseTransformPoint(value);
		}
	}

	public Quaternion eyeRotation
	{
		get
		{
			return base.transform.rotation * Quaternion.Euler(eyeLocalRotation);
		}
		set
		{
			eyeLocalRotation = (Quaternion.Inverse(base.transform.rotation) * value).eulerAngles;
		}
	}

	public List<Transform> getPoses(int poseParentIndex)
	{
		List<Transform> list = poseParents;
		Transform transform = list[list.Count - 1];
		if (poseParentIndex < poseParents.Count)
		{
			transform = poseParents[poseParentIndex];
		}
		List<Transform> list2 = new List<Transform>();
		foreach (Transform item in transform)
		{
			if (Regex.IsMatch(item.name, "^\\d+p\\d+$"))
			{
				list2.Add(item);
			}
		}
		return list2;
	}
}
