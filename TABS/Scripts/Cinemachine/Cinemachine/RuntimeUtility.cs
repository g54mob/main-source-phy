using UnityEngine;

namespace Cinemachine
{
	[DocumentationSorting(DocumentationSortingAttribute.Level.Undoc)]
	public static class RuntimeUtility
	{
		public static void DestroyObject(Object obj)
		{
			if (obj != null)
			{
				Object.Destroy(obj);
			}
		}

		public static bool RaycastIgnoreTag(Ray ray, out RaycastHit hitInfo, float rayLength, int layerMask, in string ignoreTag)
		{
			float num = 0f;
			while (Physics.Raycast(ray, out hitInfo, rayLength, layerMask, QueryTriggerInteraction.Ignore))
			{
				if (ignoreTag.Length == 0 || !hitInfo.collider.CompareTag(ignoreTag))
				{
					hitInfo.distance += num;
					return true;
				}
				Ray ray2 = new Ray(ray.GetPoint(rayLength), -ray.direction);
				if (!hitInfo.collider.Raycast(ray2, out hitInfo, rayLength))
				{
					break;
				}
				float num2 = rayLength - (hitInfo.distance - 0.001f);
				if (num2 < 0.001f)
				{
					break;
				}
				num += num2;
				rayLength = hitInfo.distance - 0.001f;
				if (rayLength < 0.001f)
				{
					break;
				}
				ray.origin = ray2.GetPoint(rayLength);
			}
			return false;
		}

		public static bool SphereCastIgnoreTag(Vector3 rayStart, float radius, Vector3 dir, out RaycastHit hitInfo, float rayLength, int layerMask, in string ignoreTag)
		{
			float num = 0f;
			while (Physics.SphereCast(rayStart, radius, dir, out hitInfo, rayLength, layerMask, QueryTriggerInteraction.Ignore))
			{
				if (ignoreTag.Length == 0 || !hitInfo.collider.CompareTag(ignoreTag))
				{
					hitInfo.distance += num;
					return true;
				}
				Ray ray = new Ray(rayStart + rayLength * dir, -dir);
				if (!hitInfo.collider.Raycast(ray, out hitInfo, rayLength))
				{
					break;
				}
				float num2 = rayLength - (hitInfo.distance - 0.001f);
				if (num2 < 0.001f)
				{
					break;
				}
				num += num2;
				rayLength = hitInfo.distance - 0.001f;
				if (rayLength < 0.001f)
				{
					break;
				}
				rayStart = ray.GetPoint(rayLength);
			}
			return false;
		}
	}
}
