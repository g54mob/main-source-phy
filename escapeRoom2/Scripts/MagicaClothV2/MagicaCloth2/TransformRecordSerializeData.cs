using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	[Serializable]
	public class TransformRecordSerializeData : ITransform
	{
		public Transform transform;

		public Vector3 localPosition;

		public Quaternion localRotation;

		public Vector3 position;

		public Quaternion rotation;

		public Vector3 scale;

		public Matrix4x4 localToWorldMatrix;

		public Matrix4x4 worldToLocalMatrix;

		public void Serialize(TransformRecord tr)
		{
			transform = tr.transform;
			localPosition = tr.localPosition;
			localRotation = tr.localRotation;
			position = tr.position;
			rotation = tr.rotation;
			scale = tr.scale;
			localToWorldMatrix = tr.localToWorldMatrix;
			worldToLocalMatrix = tr.worldToLocalMatrix;
		}

		public void Deserialize(TransformRecord tr)
		{
			tr.localPosition = localPosition;
			tr.localRotation = localRotation;
			tr.position = position;
			tr.rotation = rotation;
			tr.scale = scale;
			tr.localToWorldMatrix = localToWorldMatrix;
			tr.worldToLocalMatrix = worldToLocalMatrix;
		}

		public int GetLocalHash()
		{
			int num = 0;
			if ((bool)transform)
			{
				num += 123 + transform.childCount * 345;
			}
			return num;
		}

		public int GetGlobalHash()
		{
			return 0 + localPosition.GetHashCode() + localRotation.GetHashCode();
		}

		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			if ((bool)transform)
			{
				transformSet.Add(transform);
			}
		}

		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			int num = ((transform != null) ? transform.GetInstanceID() : 0);
			if (num != 0 && replaceDict.ContainsKey(num))
			{
				transform = replaceDict[num];
			}
		}
	}
}
