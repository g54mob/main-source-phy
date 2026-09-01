using System.Collections.Generic;
using UnityEngine;

namespace SRF
{
	public static class SRFTransformExtensions
	{
		public static IEnumerable<Transform> GetChildren(this Transform t)
		{
			for (int i = 0; i < t.childCount; i++)
			{
				yield return t.GetChild(i);
			}
		}

		public static void ResetLocal(this Transform t)
		{
			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;
			t.localScale = Vector3.one;
		}

		public static GameObject CreateChild(this Transform t, string name)
		{
			GameObject gameObject = new GameObject(name);
			gameObject.transform.parent = t;
			gameObject.transform.ResetLocal();
			gameObject.gameObject.layer = t.gameObject.layer;
			return gameObject;
		}

		public static void SetParentMaintainLocals(this Transform t, Transform parent)
		{
			t.SetParent(parent, false);
		}

		public static void SetLocals(this Transform t, Transform from)
		{
			t.localPosition = from.localPosition;
			t.localRotation = from.localRotation;
			t.localScale = from.localScale;
		}

		public static void Match(this Transform t, Transform from)
		{
			t.position = from.position;
			t.rotation = from.rotation;
		}

		public static void DestroyChildren(this Transform t)
		{
			foreach (object item in t)
			{
				Object.Destroy(((Transform)item).gameObject);
			}
		}

		public static string GetPath(this Transform t, Transform root = null)
		{
			List<string> path;
			List<int> siblingIndices;
			return (!t.GetPath(out path, out siblingIndices, root)) ? null : string.Join("/", path.ToArray());
		}

		public static bool GetPath(this Transform t, out List<string> path, out List<int> siblingIndices, Transform root = null)
		{
			path = new List<string>();
			siblingIndices = new List<int>();
			while (!object.ReferenceEquals(t, null))
			{
				if (!object.ReferenceEquals(root, null) && object.ReferenceEquals(t, root))
				{
					return true;
				}
				path.Insert(0, t.name);
				siblingIndices.Insert(0, t.GetSiblingIndex());
				t = t.parent;
			}
			return !object.ReferenceEquals(root, null);
		}

		public static Transform Find(this Transform t, List<int> siblingIndices)
		{
			for (int i = 0; i < siblingIndices.Count; i++)
			{
				if (siblingIndices[i] < 0 || siblingIndices[i] >= t.childCount)
				{
					return null;
				}
				t = t.GetChild(siblingIndices[i]);
			}
			return t;
		}

		public static Quaternion TransformRotation(this Transform t, Quaternion rotation)
		{
			return t.rotation * rotation;
		}

		public static Quaternion InverseTransformRotation(this Transform t, Quaternion rotation)
		{
			return Quaternion.Inverse(t.rotation) * rotation;
		}
	}
}
