using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	public class TransformRecord : IValid, ITransform
	{
		public Transform transform;

		public int id;

		public Vector3 localPosition;

		public Quaternion localRotation;

		public Vector3 position;

		public Quaternion rotation;

		public Vector3 scale;

		public Matrix4x4 localToWorldMatrix;

		public Matrix4x4 worldToLocalMatrix;

		public int pid;

		public TransformRecord(Transform t, bool read)
		{
			if ((bool)t)
			{
				transform = t;
				id = t.GetInstanceID();
				if (read)
				{
					localPosition = t.localPosition;
					localRotation = t.localRotation;
					position = t.position;
					rotation = t.rotation;
					scale = t.lossyScale;
					localToWorldMatrix = t.localToWorldMatrix;
					worldToLocalMatrix = t.worldToLocalMatrix;
				}
				if ((bool)t.parent)
				{
					pid = t.parent.GetInstanceID();
				}
			}
		}

		public Vector3 InverseTransformDirection(Vector3 dir)
		{
			return Quaternion.Inverse(rotation) * dir;
		}

		public bool IsValid()
		{
			return id != 0;
		}

		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			transformSet.Add(transform);
		}

		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			if (replaceDict.ContainsKey(id))
			{
				Transform transform = replaceDict[id];
				this.transform = transform;
				id = this.transform.GetInstanceID();
			}
			if (replaceDict.ContainsKey(pid))
			{
				Transform transform2 = replaceDict[pid];
				pid = transform2.GetInstanceID();
			}
		}
	}
}
