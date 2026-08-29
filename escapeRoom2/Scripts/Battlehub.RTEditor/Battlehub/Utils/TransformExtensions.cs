using UnityEngine;

namespace Battlehub.Utils
{
	public static class TransformExtensions
	{
		private static Vector3[] s_Corners = new Vector3[4];

		public static Bounds CalculateBounds(this Transform t, bool includeInactive = false)
		{
			Renderer componentInChildren = t.GetComponentInChildren<Renderer>(includeInactive);
			if ((bool)componentInChildren)
			{
				Bounds totalBounds = componentInChildren.bounds;
				if (totalBounds.size == Vector3.zero && totalBounds.center != componentInChildren.transform.position)
				{
					totalBounds = TransformBounds(componentInChildren.transform.localToWorldMatrix, totalBounds);
				}
				CalculateBounds(t, ref totalBounds);
				if (totalBounds.extents == Vector3.zero)
				{
					totalBounds.extents = new Vector3(0.5f, 0.5f, 0.5f);
				}
				return totalBounds;
			}
			return new Bounds(t.position, new Vector3(0.5f, 0.5f, 0.5f));
		}

		private static void CalculateBounds(Transform t, ref Bounds totalBounds)
		{
			foreach (Transform item in t)
			{
				Renderer component = item.GetComponent<Renderer>();
				if ((bool)component)
				{
					Bounds bounds = component.bounds;
					if (bounds.size == Vector3.zero && bounds.center != component.transform.position)
					{
						bounds = TransformBounds(component.transform.localToWorldMatrix, bounds);
					}
					totalBounds.Encapsulate(bounds.min);
					totalBounds.Encapsulate(bounds.max);
				}
				CalculateBounds(item, ref totalBounds);
			}
		}

		public static Bounds TransformBounds(Matrix4x4 matrix, Bounds bounds)
		{
			return TransformUtility.TransformBounds(ref matrix, ref bounds);
		}

		public static Bounds CalculateRelativeRectTransformBounds(Transform root, Transform child)
		{
			RectTransform rectTransform = child as RectTransform;
			if (rectTransform == null)
			{
				return new Bounds(Vector3.zero, Vector3.zero);
			}
			Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			Matrix4x4 worldToLocalMatrix = root.worldToLocalMatrix;
			rectTransform.GetWorldCorners(s_Corners);
			for (int i = 0; i < 4; i++)
			{
				Vector3 lhs = worldToLocalMatrix.MultiplyPoint3x4(s_Corners[i]);
				vector = Vector3.Min(lhs, vector);
				vector2 = Vector3.Max(lhs, vector2);
			}
			Bounds result = new Bounds(vector, Vector3.zero);
			result.Encapsulate(vector2);
			return result;
		}

		public static Bounds CalculateRelativeRectTransformBounds(this Transform trans)
		{
			return CalculateRelativeRectTransformBounds(trans, trans);
		}

		public static int CalculateDepth(this Transform transform)
		{
			int num = 0;
			while (transform.parent != null)
			{
				transform = transform.parent;
				num++;
			}
			return num;
		}
	}
}
