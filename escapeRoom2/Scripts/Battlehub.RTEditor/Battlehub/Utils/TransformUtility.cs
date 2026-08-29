using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battlehub.Utils
{
	public static class TransformUtility
	{
		private class CalculateBoundsResult
		{
			public Bounds Bounds;

			public bool Initialized;
		}

		private static CalculateBoundsResult s_result = new CalculateBoundsResult();

		public static bool ScreenRectToLocalRectInRectangle(RectTransform rt, Rect screenRect, Camera cam, out Rect localRect)
		{
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, screenRect.min, cam, out var localPoint) && RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, screenRect.max, cam, out var localPoint2))
			{
				localRect = new Rect(localPoint.x, localPoint.y, localPoint2.x - localPoint.x, localPoint2.y - localPoint.y);
				return true;
			}
			localRect = Rect.zero;
			return false;
		}

		public static Rect BoundsToScreenRect(Camera cam, Bounds[] bounds, bool isInFrustumCheck)
		{
			if (!isInFrustumCheck)
			{
				return BoundsToScreenRect(cam, bounds);
			}
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
			Vector2 vector = Vector2.zero;
			Vector2 vector2 = Vector2.zero;
			for (int i = 0; i < bounds.Length; i++)
			{
				if (GeometryUtility.TestPlanesAABB(planes, bounds[i]))
				{
					Rect rect = BoundsToScreenRect(cam, bounds[i]);
					vector = rect.min;
					vector2 = rect.max;
					break;
				}
			}
			for (int i = 0; i < bounds.Length; i++)
			{
				if (GeometryUtility.TestPlanesAABB(planes, bounds[i]))
				{
					Rect rect2 = BoundsToScreenRect(cam, bounds[i]);
					vector.x = Mathf.Min(rect2.min.x, vector.x);
					vector.y = Mathf.Min(rect2.min.y, vector.y);
					vector2.x = Mathf.Max(rect2.max.x, vector2.x);
					vector2.y = Mathf.Max(rect2.max.y, vector2.y);
				}
			}
			return new Rect(vector.x, vector.y, vector2.x - vector.x, vector2.y - vector.y);
		}

		public static Rect BoundsToScreenRect(Camera cam, Bounds[] bounds)
		{
			if (bounds.Length == 0)
			{
				return Rect.zero;
			}
			Rect rect = BoundsToScreenRect(cam, bounds[0]);
			Vector2 min = rect.min;
			Vector2 max = rect.max;
			for (int i = 1; i < bounds.Length; i++)
			{
				rect = BoundsToScreenRect(cam, bounds[i]);
				min.x = Mathf.Min(rect.min.x, min.x);
				min.y = Mathf.Min(rect.min.y, min.y);
				max.x = Mathf.Max(rect.max.x, max.x);
				max.y = Mathf.Max(rect.max.y, max.y);
			}
			return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
		}

		public static Rect BoundsToScreenRect(Camera cam, Bounds bounds)
		{
			Vector3 center = bounds.center;
			Vector3 extents = bounds.extents;
			Vector2 min = RectTransformUtility.WorldToScreenPoint(cam, new Vector3(center.x - extents.x, center.y - extents.y, center.z - extents.z));
			Vector2 max = min;
			GetMinMax(min, ref min, ref max);
			GetMinMax(RectTransformUtility.WorldToScreenPoint(cam, new Vector3(center.x + extents.x, center.y - extents.y, center.z - extents.z)), ref min, ref max);
			GetMinMax(RectTransformUtility.WorldToScreenPoint(cam, new Vector3(center.x - extents.x, center.y - extents.y, center.z + extents.z)), ref min, ref max);
			GetMinMax(RectTransformUtility.WorldToScreenPoint(cam, new Vector3(center.x + extents.x, center.y - extents.y, center.z + extents.z)), ref min, ref max);
			GetMinMax(RectTransformUtility.WorldToScreenPoint(cam, new Vector3(center.x - extents.x, center.y + extents.y, center.z - extents.z)), ref min, ref max);
			GetMinMax(RectTransformUtility.WorldToScreenPoint(cam, new Vector3(center.x + extents.x, center.y + extents.y, center.z - extents.z)), ref min, ref max);
			GetMinMax(RectTransformUtility.WorldToScreenPoint(cam, new Vector3(center.x - extents.x, center.y + extents.y, center.z + extents.z)), ref min, ref max);
			GetMinMax(RectTransformUtility.WorldToScreenPoint(cam, new Vector3(center.x + extents.x, center.y + extents.y, center.z + extents.z)), ref min, ref max);
			return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
		}

		private static void GetMinMax(Vector2 point, ref Vector2 min, ref Vector2 max)
		{
			min = new Vector2((min.x >= point.x) ? point.x : min.x, (min.y >= point.y) ? point.y : min.y);
			max = new Vector2((max.x <= point.x) ? point.x : max.x, (max.y <= point.y) ? point.y : max.y);
		}

		public static Vector3 GetCenter(this Transform target)
		{
			MeshFilter component = target.GetComponent<MeshFilter>();
			if (component != null && component.sharedMesh != null)
			{
				return target.TransformPoint(component.sharedMesh.bounds.center);
			}
			SkinnedMeshRenderer component2 = target.GetComponent<SkinnedMeshRenderer>();
			if (component2 != null && component2.sharedMesh != null)
			{
				return target.TransformPoint(component2.sharedMesh.bounds.center);
			}
			return target.position;
		}

		public static Vector3 GetCommonCenter(IList<Transform> transforms)
		{
			Vector3 center = transforms[0].GetCenter();
			for (int i = 1; i < transforms.Count; i++)
			{
				Transform target = transforms[i];
				center += target.GetCenter();
			}
			return center / transforms.Count;
		}

		public static Vector3 CenterPoint(Vector3[] vectors)
		{
			Vector3 zero = Vector3.zero;
			if (vectors == null || vectors.Length == 0)
			{
				return zero;
			}
			foreach (Vector3 vector in vectors)
			{
				zero += vector;
			}
			return zero / vectors.Length;
		}

		public static Bounds CalculateBounds(Transform[] transforms, int layerMask = -1, bool includeInactive = true)
		{
			CalculateBoundsResult calculateBoundsResult = new CalculateBoundsResult();
			for (int i = 0; i < transforms.Length; i++)
			{
				CalculateBounds(transforms[i], calculateBoundsResult, layerMask, includeInactive);
			}
			if (calculateBoundsResult.Initialized)
			{
				return calculateBoundsResult.Bounds;
			}
			return new Bounds(CenterPoint(transforms.Select((Transform t) => t.position).ToArray()), Vector3.zero);
		}

		public static Bounds CalculateBounds(Transform transform, int layerMask = -1, bool includeInactive = true)
		{
			s_result.Initialized = false;
			CalculateBounds(transform, s_result, layerMask, includeInactive);
			if (s_result.Initialized)
			{
				return s_result.Bounds;
			}
			return new Bounds(transform.position, Vector3.zero);
		}

		private static void CalculateBounds(Transform t, CalculateBoundsResult result, int layerMask, bool includeInactive)
		{
			if ((!includeInactive && !t.gameObject.activeSelf) || ((1 << t.gameObject.layer) & layerMask) == 0)
			{
				return;
			}
			if (t is RectTransform)
			{
				CalculateBounds((RectTransform)t, result);
			}
			else
			{
				Renderer component = t.GetComponent<Renderer>();
				if (component != null && (includeInactive || component.enabled))
				{
					CalculateBounds(component, result);
				}
			}
			foreach (Transform item in t)
			{
				CalculateBounds(item, result, layerMask, includeInactive);
			}
		}

		private static void CalculateBounds(RectTransform rt, CalculateBoundsResult result)
		{
			Bounds bounds = rt.CalculateRelativeRectTransformBounds();
			Matrix4x4 matrix = rt.localToWorldMatrix;
			Bounds bounds2 = TransformBounds(ref matrix, ref bounds);
			if (!result.Initialized)
			{
				result.Bounds = bounds2;
				result.Initialized = true;
			}
			else
			{
				result.Bounds.Encapsulate(bounds2.min);
				result.Bounds.Encapsulate(bounds2.max);
			}
		}

		private static void CalculateBounds(Renderer renderer, CalculateBoundsResult result)
		{
			if (!(renderer is ParticleSystemRenderer))
			{
				Bounds bounds = renderer.bounds;
				if (bounds.size == Vector3.zero && bounds.center != renderer.transform.position)
				{
					Matrix4x4 matrix = renderer.transform.localToWorldMatrix;
					bounds = TransformBounds(ref matrix, ref bounds);
				}
				if (!result.Initialized)
				{
					result.Bounds = bounds;
					result.Initialized = true;
				}
				else
				{
					result.Bounds.Encapsulate(bounds.min);
					result.Bounds.Encapsulate(bounds.max);
				}
			}
		}

		public static Bounds TransformBounds(ref Matrix4x4 matrix, ref Bounds bounds)
		{
			Vector3 center = matrix.MultiplyPoint(bounds.center);
			Vector3 extents = bounds.extents;
			Vector3 vector = matrix.MultiplyVector(new Vector3(extents.x, 0f, 0f));
			Vector3 vector2 = matrix.MultiplyVector(new Vector3(0f, extents.y, 0f));
			Vector3 vector3 = matrix.MultiplyVector(new Vector3(0f, 0f, extents.z));
			extents.x = Mathf.Abs(vector.x) + Mathf.Abs(vector2.x) + Mathf.Abs(vector3.x);
			extents.y = Mathf.Abs(vector.y) + Mathf.Abs(vector2.y) + Mathf.Abs(vector3.y);
			extents.z = Mathf.Abs(vector.z) + Mathf.Abs(vector2.z) + Mathf.Abs(vector3.z);
			return new Bounds
			{
				center = center,
				extents = extents
			};
		}
	}
}
