using System.Runtime.CompilerServices;
using UnityEngine;

namespace Vertx.Debugging
{
	public static class DrawPhysics
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Raycast(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.Raycast(origin, direction, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance)
		{
			return Physics.Raycast(origin, direction, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Raycast(Vector3 origin, Vector3 direction)
		{
			return Physics.Raycast(origin, direction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Raycast(origin, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.Raycast(origin, direction, out hitInfo, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.Raycast(origin, direction, out hitInfo, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo)
		{
			return Physics.Raycast(origin, direction, out hitInfo);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Raycast(Ray ray, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Raycast(ray, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Raycast(Ray ray, float maxDistance, int layerMask)
		{
			return Physics.Raycast(ray, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Raycast(Ray ray, float maxDistance)
		{
			return Physics.Raycast(ray, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Raycast(Ray ray)
		{
			return Physics.Raycast(ray);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Raycast(ray, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.Raycast(ray, out hitInfo, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.Raycast(ray, out hitInfo, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo)
		{
			return Physics.Raycast(ray, out hitInfo);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Linecast(Vector3 start, Vector3 end, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Linecast(start, end, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Linecast(Vector3 start, Vector3 end, int layerMask)
		{
			return Physics.Linecast(start, end, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Linecast(Vector3 start, Vector3 end)
		{
			return Physics.Linecast(start, end);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.Linecast(start, end, out hitInfo, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, int layerMask)
		{
			return Physics.Linecast(start, end, out hitInfo, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo)
		{
			return Physics.Linecast(start, end, out hitInfo);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.SphereCast(origin, radius, direction, out hitInfo, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo)
		{
			return Physics.SphereCast(origin, radius, direction, out hitInfo);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(Ray ray, float radius, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.SphereCast(ray, radius, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(Ray ray, float radius, float maxDistance, int layerMask)
		{
			return Physics.SphereCast(ray, radius, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(Ray ray, float radius, float maxDistance)
		{
			return Physics.SphereCast(ray, radius, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(Ray ray, float radius)
		{
			return Physics.SphereCast(ray, radius);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.SphereCast(ray, radius, out hitInfo, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.SphereCast(ray, radius, out hitInfo, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo)
		{
			return Physics.SphereCast(ray, radius, out hitInfo);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.BoxCast(center, halfExtents, direction, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layerMask)
		{
			return Physics.BoxCast(center, halfExtents, direction, orientation, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance)
		{
			return Physics.BoxCast(center, halfExtents, direction, orientation, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation)
		{
			return Physics.BoxCast(center, halfExtents, direction, orientation);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction)
		{
			return Physics.BoxCast(center, halfExtents, direction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation, float maxDistance, int layerMask)
		{
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation, float maxDistance)
		{
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation)
		{
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo, orientation);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo)
		{
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.RaycastAll(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.RaycastAll(origin, direction, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance)
		{
			return Physics.RaycastAll(origin, direction, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction)
		{
			return Physics.RaycastAll(origin, direction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.RaycastAll(ray, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance, int layerMask)
		{
			return Physics.RaycastAll(ray, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance)
		{
			return Physics.RaycastAll(ray, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] RaycastAll(Ray ray)
		{
			return Physics.RaycastAll(ray);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.RaycastNonAlloc(ray, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.RaycastNonAlloc(ray, results, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance)
		{
			return Physics.RaycastNonAlloc(ray, results, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results)
		{
			return Physics.RaycastNonAlloc(ray, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.RaycastNonAlloc(origin, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.RaycastNonAlloc(origin, direction, results, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance)
		{
			return Physics.RaycastNonAlloc(origin, direction, results, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results)
		{
			return Physics.RaycastNonAlloc(origin, direction, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CapsuleCastAll(point1, point2, radius, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.CapsuleCastAll(point1, point2, radius, direction, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance)
		{
			return Physics.CapsuleCastAll(point1, point2, radius, direction, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction)
		{
			return Physics.CapsuleCastAll(point1, point2, radius, direction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.SphereCastAll(origin, radius, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.SphereCastAll(origin, radius, direction, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance)
		{
			return Physics.SphereCastAll(origin, radius, direction, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction)
		{
			return Physics.SphereCastAll(origin, radius, direction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.SphereCastAll(ray, radius, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance, int layerMask)
		{
			return Physics.SphereCastAll(ray, radius, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance)
		{
			return Physics.SphereCastAll(ray, radius, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius)
		{
			return Physics.SphereCastAll(ray, radius);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			Collider[] array = Physics.OverlapCapsule(point0, point1, radius, layerMask, queryTriggerInteraction);
			Collider[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius, int layerMask)
		{
			Collider[] array = Physics.OverlapCapsule(point0, point1, radius, layerMask);
			Collider[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius)
		{
			Collider[] array = Physics.OverlapCapsule(point0, point1, radius);
			Collider[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapSphere(Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			Collider[] array = Physics.OverlapSphere(position, radius, layerMask, queryTriggerInteraction);
			Collider[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapSphere(Vector3 position, float radius, int layerMask)
		{
			Collider[] array = Physics.OverlapSphere(position, radius, layerMask);
			Collider[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapSphere(Vector3 position, float radius)
		{
			Collider[] array = Physics.OverlapSphere(position, radius);
			Collider[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool ComputePenetration(Collider colliderA, Vector3 positionA, Quaternion rotationA, Collider colliderB, Vector3 positionB, Quaternion rotationB, out Vector3 direction, out float distance)
		{
			return Physics.ComputePenetration(colliderA, positionA, rotationA, colliderB, positionB, rotationB, out direction, out distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ClosestPoint(Vector3 point, Collider collider, Vector3 position, Quaternion rotation)
		{
			return Physics.ClosestPoint(point, collider, position, rotation);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			int num = Physics.OverlapSphereNonAlloc(position, radius, results, layerMask, queryTriggerInteraction);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results, int layerMask)
		{
			int num = Physics.OverlapSphereNonAlloc(position, radius, results, layerMask);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results)
		{
			int num = Physics.OverlapSphereNonAlloc(position, radius, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckSphere(Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CheckSphere(position, radius, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckSphere(Vector3 position, float radius, int layerMask)
		{
			return Physics.CheckSphere(position, radius, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckSphere(Vector3 position, float radius)
		{
			return Physics.CheckSphere(position, radius);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, float maxDistance)
		{
			return Physics.CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results)
		{
			return Physics.CapsuleCastNonAlloc(point1, point2, radius, direction, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.SphereCastNonAlloc(origin, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.SphereCastNonAlloc(origin, radius, direction, results, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance)
		{
			return Physics.SphereCastNonAlloc(origin, radius, direction, results, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results)
		{
			return Physics.SphereCastNonAlloc(origin, radius, direction, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.SphereCastNonAlloc(ray, radius, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.SphereCastNonAlloc(ray, radius, results, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, float maxDistance)
		{
			return Physics.SphereCastNonAlloc(ray, radius, results, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results)
		{
			return Physics.SphereCastNonAlloc(ray, radius, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CheckCapsule(start, end, radius, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layerMask)
		{
			return Physics.CheckCapsule(start, end, radius, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius)
		{
			return Physics.CheckCapsule(start, end, radius);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layermask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CheckBox(center, halfExtents, orientation, layermask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layermask)
		{
			return Physics.CheckBox(center, halfExtents, orientation, layermask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation)
		{
			return Physics.CheckBox(center, halfExtents, orientation);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool CheckBox(Vector3 center, Vector3 halfExtents)
		{
			return Physics.CheckBox(center, halfExtents);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			Collider[] array = Physics.OverlapBox(center, halfExtents, orientation, layerMask, queryTriggerInteraction);
			Collider[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask)
		{
			Collider[] array = Physics.OverlapBox(center, halfExtents, orientation, layerMask);
			Collider[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation)
		{
			Collider[] array = Physics.OverlapBox(center, halfExtents, orientation);
			Collider[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents)
		{
			Collider[] array = Physics.OverlapBox(center, halfExtents);
			Collider[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			int num = Physics.OverlapBoxNonAlloc(center, halfExtents, results, orientation, mask, queryTriggerInteraction);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation, int mask)
		{
			int num = Physics.OverlapBoxNonAlloc(center, halfExtents, results, orientation, mask);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation)
		{
			int num = Physics.OverlapBoxNonAlloc(center, halfExtents, results, orientation);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results)
		{
			int num = Physics.OverlapBoxNonAlloc(center, halfExtents, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation)
		{
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation, float maxDistance)
		{
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation, float maxDistance, int layerMask)
		{
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results)
		{
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layerMask)
		{
			return Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance)
		{
			return Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation)
		{
			return Physics.BoxCastAll(center, halfExtents, direction, orientation);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction)
		{
			return Physics.BoxCastAll(center, halfExtents, direction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCapsuleNonAlloc(Vector3 point0, Vector3 point1, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			int num = Physics.OverlapCapsuleNonAlloc(point0, point1, radius, results, layerMask, queryTriggerInteraction);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCapsuleNonAlloc(Vector3 point0, Vector3 point1, float radius, Collider[] results, int layerMask)
		{
			int num = Physics.OverlapCapsuleNonAlloc(point0, point1, radius, results, layerMask);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCapsuleNonAlloc(Vector3 point0, Vector3 point1, float radius, Collider[] results)
		{
			int num = Physics.OverlapCapsuleNonAlloc(point0, point1, radius, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}
	}
}
