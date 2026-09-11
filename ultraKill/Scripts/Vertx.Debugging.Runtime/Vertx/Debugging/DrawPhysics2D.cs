using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Vertx.Debugging
{
	public static class DrawPhysics2D
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ColliderDistance2D Distance(Collider2D colliderA, Collider2D colliderB)
		{
			ColliderDistance2D result = Physics2D.Distance(colliderA, colliderB);
			_ = (float2)result.pointA;
			float2 obj = result.pointB;
			float2 float5 = result.normal;
			_ = obj + float5 * (result.distance * 0.5f);
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 ClosestPoint(Vector2 position, Collider2D collider)
		{
			return Physics2D.ClosestPoint(position, collider);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 ClosestPoint(Vector2 position, Rigidbody2D rigidbody)
		{
			return Physics2D.ClosestPoint(position, rigidbody);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end)
		{
			return Physics2D.Linecast(start, end);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask)
		{
			return Physics2D.Linecast(start, end, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask, float minDepth)
		{
			return Physics2D.Linecast(start, end, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask, float minDepth, float maxDepth)
		{
			return Physics2D.Linecast(start, end, layerMask, minDepth, maxDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return Physics2D.Linecast(start, end, contactFilter, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, List<RaycastHit2D> results)
		{
			return Physics2D.Linecast(start, end, contactFilter, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end)
		{
			return Physics2D.LinecastAll(start, end);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask)
		{
			return Physics2D.LinecastAll(start, end, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask, float minDepth)
		{
			return Physics2D.LinecastAll(start, end, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask, float minDepth, float maxDepth)
		{
			return Physics2D.LinecastAll(start, end, layerMask, minDepth, maxDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results)
		{
			return Physics2D.LinecastNonAlloc(start, end, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask)
		{
			return Physics2D.LinecastNonAlloc(start, end, results, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask, float minDepth)
		{
			return Physics2D.LinecastNonAlloc(start, end, results, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask, float minDepth, float maxDepth)
		{
			return Physics2D.LinecastNonAlloc(start, end, results, layerMask, minDepth, maxDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction)
		{
			return Physics2D.Raycast(origin, direction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance)
		{
			return Physics2D.Raycast(origin, direction, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask)
		{
			return Physics2D.Raycast(origin, direction, distance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			return Physics2D.Raycast(origin, direction, distance, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth)
		{
			return Physics2D.Raycast(origin, direction, distance, layerMask, minDepth, maxDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return Physics2D.Raycast(origin, direction, contactFilter, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance)
		{
			return Physics2D.Raycast(origin, direction, contactFilter, results, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, float distance = float.PositiveInfinity)
		{
			return Physics2D.Raycast(origin, direction, contactFilter, results, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results)
		{
			return Physics2D.RaycastNonAlloc(origin, direction, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			return Physics2D.RaycastNonAlloc(origin, direction, results, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			return Physics2D.RaycastNonAlloc(origin, direction, results, distance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			return Physics2D.RaycastNonAlloc(origin, direction, results, distance, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, float maxDepth)
		{
			return Physics2D.RaycastNonAlloc(origin, direction, results, distance, layerMask, minDepth, maxDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction)
		{
			return Physics2D.RaycastAll(origin, direction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance)
		{
			return Physics2D.RaycastAll(origin, direction, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask)
		{
			return Physics2D.RaycastAll(origin, direction, distance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			return Physics2D.RaycastAll(origin, direction, distance, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth)
		{
			return Physics2D.RaycastAll(origin, direction, distance, layerMask, minDepth, maxDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction)
		{
			return Physics2D.CircleCast(origin, radius, direction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance)
		{
			return Physics2D.CircleCast(origin, radius, direction, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask)
		{
			return Physics2D.CircleCast(origin, radius, direction, distance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			return Physics2D.CircleCast(origin, radius, direction, distance, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth)
		{
			return Physics2D.CircleCast(origin, radius, direction, distance, layerMask, minDepth, maxDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return Physics2D.CircleCast(origin, radius, direction, contactFilter, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance)
		{
			return Physics2D.CircleCast(origin, radius, direction, contactFilter, results, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, float distance = float.PositiveInfinity)
		{
			return Physics2D.CircleCast(origin, radius, direction, contactFilter, results, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction)
		{
			return Physics2D.CircleCastAll(origin, radius, direction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance)
		{
			return Physics2D.CircleCastAll(origin, radius, direction, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask)
		{
			return Physics2D.CircleCastAll(origin, radius, direction, distance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			return Physics2D.CircleCastAll(origin, radius, direction, distance, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth)
		{
			return Physics2D.CircleCastAll(origin, radius, direction, distance, layerMask, minDepth, maxDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results)
		{
			return Physics2D.CircleCastNonAlloc(origin, radius, direction, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			return Physics2D.CircleCastNonAlloc(origin, radius, direction, results, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			return Physics2D.CircleCastNonAlloc(origin, radius, direction, results, distance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			return Physics2D.CircleCastNonAlloc(origin, radius, direction, results, distance, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, float maxDepth)
		{
			return Physics2D.CircleCastNonAlloc(origin, radius, direction, results, distance, layerMask, minDepth, maxDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction)
		{
			return Physics2D.BoxCast(origin, size, angleDegrees, direction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction, float distance)
		{
			return Physics2D.BoxCast(origin, size, angleDegrees, direction, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction, float distance, int layerMask)
		{
			return Physics2D.BoxCast(origin, size, angleDegrees, direction, distance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			return Physics2D.BoxCast(origin, size, angleDegrees, direction, distance, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth)
		{
			return Physics2D.BoxCast(origin, size, angleDegrees, direction, distance, layerMask, minDepth, maxDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCast(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return Physics2D.BoxCast(origin, size, angleDegrees, direction, contactFilter, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCast(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance)
		{
			return Physics2D.BoxCast(origin, size, angleDegrees, direction, contactFilter, results, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCast(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, float distance = float.PositiveInfinity)
		{
			return Physics2D.BoxCast(origin, size, angleDegrees, direction, contactFilter, results, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction)
		{
			return Physics2D.BoxCastAll(origin, size, angleDegrees, direction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction, float distance)
		{
			return Physics2D.BoxCastAll(origin, size, angleDegrees, direction, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction, float distance, int layerMask)
		{
			return Physics2D.BoxCastAll(origin, size, angleDegrees, direction, distance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			return Physics2D.BoxCastAll(origin, size, angleDegrees, direction, distance, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth)
		{
			return Physics2D.BoxCastAll(origin, size, angleDegrees, direction, distance, layerMask, minDepth, maxDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction, RaycastHit2D[] results)
		{
			return Physics2D.BoxCastNonAlloc(origin, size, angleDegrees, direction, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			return Physics2D.BoxCastNonAlloc(origin, size, angleDegrees, direction, results, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			return Physics2D.BoxCastNonAlloc(origin, size, angleDegrees, direction, results, distance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			return Physics2D.BoxCastNonAlloc(origin, size, angleDegrees, direction, results, distance, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angleDegrees, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, float maxDepth)
		{
			return Physics2D.BoxCastNonAlloc(origin, size, angleDegrees, direction, results, distance, layerMask, minDepth, maxDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction)
		{
			return Physics2D.CapsuleCast(origin, size, capsuleDirection, angleDegrees, direction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction, float distance)
		{
			return Physics2D.CapsuleCast(origin, size, capsuleDirection, angleDegrees, direction, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction, float distance, int layerMask)
		{
			return Physics2D.CapsuleCast(origin, size, capsuleDirection, angleDegrees, direction, distance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			return Physics2D.CapsuleCast(origin, size, capsuleDirection, angleDegrees, direction, distance, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth)
		{
			return Physics2D.CapsuleCast(origin, size, capsuleDirection, angleDegrees, direction, distance, layerMask, minDepth, maxDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return Physics2D.CapsuleCast(origin, size, capsuleDirection, angleDegrees, direction, contactFilter, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance)
		{
			return Physics2D.CapsuleCast(origin, size, capsuleDirection, angleDegrees, direction, contactFilter, results, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, float distance = float.PositiveInfinity)
		{
			return Physics2D.CapsuleCast(origin, size, capsuleDirection, angleDegrees, direction, contactFilter, results, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction)
		{
			return Physics2D.CapsuleCastAll(origin, size, capsuleDirection, angleDegrees, direction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction, float distance)
		{
			return Physics2D.CapsuleCastAll(origin, size, capsuleDirection, angleDegrees, direction, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction, float distance, int layerMask)
		{
			return Physics2D.CapsuleCastAll(origin, size, capsuleDirection, angleDegrees, direction, distance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			return Physics2D.CapsuleCastAll(origin, size, capsuleDirection, angleDegrees, direction, distance, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth)
		{
			return Physics2D.CapsuleCastAll(origin, size, capsuleDirection, angleDegrees, direction, distance, layerMask, minDepth, maxDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction, RaycastHit2D[] results)
		{
			return Physics2D.CapsuleCastNonAlloc(origin, size, capsuleDirection, angleDegrees, direction, results);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			return Physics2D.CapsuleCastNonAlloc(origin, size, capsuleDirection, angleDegrees, direction, results, distance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			return Physics2D.CapsuleCastNonAlloc(origin, size, capsuleDirection, angleDegrees, direction, results, distance, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			return Physics2D.CapsuleCastNonAlloc(origin, size, capsuleDirection, angleDegrees, direction, results, distance, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angleDegrees, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, float maxDepth)
		{
			return Physics2D.CapsuleCastNonAlloc(origin, size, capsuleDirection, angleDegrees, direction, results, distance, layerMask, minDepth, maxDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D GetRayIntersection(Ray ray)
		{
			RaycastHit2D rayIntersection = Physics2D.GetRayIntersection(ray);
			_ = (bool)rayIntersection.collider;
			return rayIntersection;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D GetRayIntersection(Ray ray, float distance)
		{
			RaycastHit2D rayIntersection = Physics2D.GetRayIntersection(ray, distance);
			_ = (bool)rayIntersection.collider;
			return rayIntersection;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D GetRayIntersection(Ray ray, float distance, int layerMask)
		{
			RaycastHit2D rayIntersection = Physics2D.GetRayIntersection(ray, distance, layerMask);
			_ = (bool)rayIntersection.collider;
			return rayIntersection;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray)
		{
			RaycastHit2D[] rayIntersectionAll = Physics2D.GetRayIntersectionAll(ray);
			RaycastHit2D[] array = rayIntersectionAll;
			for (int i = 0; i < array.Length; i++)
			{
				_ = ref array[i];
			}
			return rayIntersectionAll;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, float distance)
		{
			RaycastHit2D[] rayIntersectionAll = Physics2D.GetRayIntersectionAll(ray, distance);
			RaycastHit2D[] array = rayIntersectionAll;
			for (int i = 0; i < array.Length; i++)
			{
				_ = ref array[i];
			}
			return rayIntersectionAll;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, float distance, int layerMask)
		{
			RaycastHit2D[] rayIntersectionAll = Physics2D.GetRayIntersectionAll(ray, distance, layerMask);
			RaycastHit2D[] array = rayIntersectionAll;
			for (int i = 0; i < array.Length; i++)
			{
				_ = ref array[i];
			}
			return rayIntersectionAll;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results)
		{
			int rayIntersectionNonAlloc = Physics2D.GetRayIntersectionNonAlloc(ray, results);
			for (int i = 0; i < rayIntersectionNonAlloc; i++)
			{
			}
			return rayIntersectionNonAlloc;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, float distance)
		{
			int rayIntersectionNonAlloc = Physics2D.GetRayIntersectionNonAlloc(ray, results, distance);
			for (int i = 0; i < rayIntersectionNonAlloc; i++)
			{
			}
			return rayIntersectionNonAlloc;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, float distance, int layerMask)
		{
			int rayIntersectionNonAlloc = Physics2D.GetRayIntersectionNonAlloc(ray, results, distance, layerMask);
			for (int i = 0; i < rayIntersectionNonAlloc; i++)
			{
			}
			return rayIntersectionNonAlloc;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapPoint(Vector2 point)
		{
			return Physics2D.OverlapPoint(point);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapPoint(Vector2 point, int layerMask)
		{
			return Physics2D.OverlapPoint(point, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapPoint(Vector2 point, int layerMask, float minDepth)
		{
			return Physics2D.OverlapPoint(point, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapPoint(Vector2 point, int layerMask, float minDepth, float maxDepth)
		{
			Collider2D result = Physics2D.OverlapPoint(point, layerMask, minDepth, maxDepth);
			_ = maxDepth - minDepth;
			_ = 0.001f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, Collider2D[] results)
		{
			int num = Physics2D.OverlapPoint(point, contactFilter, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			int num = Physics2D.OverlapPoint(point, contactFilter, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapPointAll(Vector2 point)
		{
			Collider2D[] array = Physics2D.OverlapPointAll(point);
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask)
		{
			Collider2D[] array = Physics2D.OverlapPointAll(point, layerMask);
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask, float minDepth)
		{
			Collider2D[] array = Physics2D.OverlapPointAll(point, layerMask, minDepth);
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask, float minDepth, float maxDepth)
		{
			Collider2D[] array = Physics2D.OverlapPointAll(point, layerMask, minDepth, maxDepth);
			_ = maxDepth - minDepth;
			_ = 0.001f;
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results)
		{
			int num = Physics2D.OverlapPointNonAlloc(point, results);
			for (int i = 0; i < num; i++)
			{
				_ = results[i];
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask)
		{
			int num = Physics2D.OverlapPointNonAlloc(point, results, layerMask);
			for (int i = 0; i < num; i++)
			{
				_ = results[i];
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask, float minDepth)
		{
			int num = Physics2D.OverlapPointNonAlloc(point, results, layerMask, minDepth);
			for (int i = 0; i < num; i++)
			{
				_ = results[i];
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask, float minDepth, float maxDepth)
		{
			int num = Physics2D.OverlapPointNonAlloc(point, results, layerMask, minDepth, maxDepth);
			_ = maxDepth - minDepth;
			_ = 0.001f;
			for (int i = 0; i < num; i++)
			{
				_ = results[i];
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapCircle(Vector2 point, float radius)
		{
			return Physics2D.OverlapCircle(point, radius);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask)
		{
			return Physics2D.OverlapCircle(point, radius, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask, float minDepth)
		{
			return Physics2D.OverlapCircle(point, radius, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask, float minDepth, float maxDepth)
		{
			Collider2D result = Physics2D.OverlapCircle(point, radius, layerMask, minDepth, maxDepth);
			_ = maxDepth - minDepth;
			_ = 0.001f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, Collider2D[] results)
		{
			int num = Physics2D.OverlapCircle(point, radius, contactFilter, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			int num = Physics2D.OverlapCircle(point, radius, contactFilter, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius)
		{
			Collider2D[] array = Physics2D.OverlapCircleAll(point, radius);
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask)
		{
			Collider2D[] array = Physics2D.OverlapCircleAll(point, radius, layerMask);
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask, float minDepth)
		{
			Collider2D[] array = Physics2D.OverlapCircleAll(point, radius, layerMask, minDepth);
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask, float minDepth, float maxDepth)
		{
			Collider2D[] array = Physics2D.OverlapCircleAll(point, radius, layerMask, minDepth, maxDepth);
			_ = maxDepth - minDepth;
			_ = 0.001f;
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results)
		{
			int num = Physics2D.OverlapCircleNonAlloc(point, radius, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask)
		{
			int num = Physics2D.OverlapCircleNonAlloc(point, radius, results, layerMask);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask, float minDepth)
		{
			int num = Physics2D.OverlapCircleNonAlloc(point, radius, results, layerMask, minDepth);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask, float minDepth, float maxDepth)
		{
			int num = Physics2D.OverlapCircleNonAlloc(point, radius, results, layerMask, minDepth, maxDepth);
			_ = maxDepth - minDepth;
			_ = 0.001f;
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angleDegrees)
		{
			return Physics2D.OverlapBox(point, size, angleDegrees);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angleDegrees, int layerMask)
		{
			return Physics2D.OverlapBox(point, size, angleDegrees, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angleDegrees, int layerMask, float minDepth)
		{
			return Physics2D.OverlapBox(point, size, angleDegrees, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angleDegrees, int layerMask, float minDepth, float maxDepth)
		{
			Collider2D result = Physics2D.OverlapBox(point, size, angleDegrees, layerMask, minDepth, maxDepth);
			_ = maxDepth - minDepth;
			_ = 0.001f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapBox(Vector2 point, Vector2 size, float angleDegrees, ContactFilter2D contactFilter, Collider2D[] results)
		{
			int num = Physics2D.OverlapBox(point, size, angleDegrees, contactFilter, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapBox(Vector2 point, Vector2 size, float angleDegrees, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			int num = Physics2D.OverlapBox(point, size, angleDegrees, contactFilter, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angleDegrees)
		{
			Collider2D[] array = Physics2D.OverlapBoxAll(point, size, angleDegrees);
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angleDegrees, int layerMask)
		{
			Collider2D[] array = Physics2D.OverlapBoxAll(point, size, angleDegrees, layerMask);
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angleDegrees, int layerMask, float minDepth)
		{
			Collider2D[] array = Physics2D.OverlapBoxAll(point, size, angleDegrees, layerMask, minDepth);
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angleDegrees, int layerMask, float minDepth, float maxDepth)
		{
			Collider2D[] array = Physics2D.OverlapBoxAll(point, size, angleDegrees, layerMask, minDepth, maxDepth);
			_ = maxDepth - minDepth;
			_ = 0.001f;
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angleDegrees, Collider2D[] results)
		{
			int num = Physics2D.OverlapBoxNonAlloc(point, size, angleDegrees, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angleDegrees, Collider2D[] results, int layerMask)
		{
			int num = Physics2D.OverlapBoxNonAlloc(point, size, angleDegrees, results, layerMask);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angleDegrees, Collider2D[] results, int layerMask, float minDepth)
		{
			int num = Physics2D.OverlapBoxNonAlloc(point, size, angleDegrees, results, layerMask, minDepth);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angleDegrees, Collider2D[] results, int layerMask, float minDepth, float maxDepth)
		{
			int num = Physics2D.OverlapBoxNonAlloc(point, size, angleDegrees, results, layerMask, minDepth, maxDepth);
			_ = maxDepth - minDepth;
			_ = 0.001f;
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB)
		{
			return Physics2D.OverlapArea(pointA, pointB);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask)
		{
			return Physics2D.OverlapArea(pointA, pointB, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth)
		{
			return Physics2D.OverlapArea(pointA, pointB, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth, float maxDepth)
		{
			Collider2D result = Physics2D.OverlapArea(pointA, pointB, layerMask, minDepth, maxDepth);
			_ = maxDepth - minDepth;
			_ = 0.001f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, Collider2D[] results)
		{
			int num = Physics2D.OverlapArea(pointA, pointB, contactFilter, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			int num = Physics2D.OverlapArea(pointA, pointB, contactFilter, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB)
		{
			Collider2D[] array = Physics2D.OverlapAreaAll(pointA, pointB);
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask)
		{
			Collider2D[] array = Physics2D.OverlapAreaAll(pointA, pointB, layerMask);
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth)
		{
			Collider2D[] array = Physics2D.OverlapAreaAll(pointA, pointB, layerMask, minDepth);
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth, float maxDepth)
		{
			Collider2D[] array = Physics2D.OverlapAreaAll(pointA, pointB, layerMask, minDepth, maxDepth);
			_ = maxDepth - minDepth;
			_ = 0.001f;
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results)
		{
			int num = Physics2D.OverlapAreaNonAlloc(pointA, pointB, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask)
		{
			int num = Physics2D.OverlapAreaNonAlloc(pointA, pointB, results, layerMask);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask, float minDepth)
		{
			int num = Physics2D.OverlapAreaNonAlloc(pointA, pointB, results, layerMask, minDepth);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask, float minDepth, float maxDepth)
		{
			int num = Physics2D.OverlapAreaNonAlloc(pointA, pointB, results, layerMask, minDepth, maxDepth);
			_ = maxDepth - minDepth;
			_ = 0.001f;
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angleDegrees)
		{
			return Physics2D.OverlapCapsule(point, size, direction, angleDegrees);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angleDegrees, int layerMask)
		{
			return Physics2D.OverlapCapsule(point, size, direction, angleDegrees, layerMask);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angleDegrees, int layerMask, float minDepth)
		{
			return Physics2D.OverlapCapsule(point, size, direction, angleDegrees, layerMask, minDepth);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angleDegrees, int layerMask, float minDepth, float maxDepth)
		{
			Collider2D result = Physics2D.OverlapCapsule(point, size, direction, angleDegrees, layerMask, minDepth, maxDepth);
			_ = maxDepth - minDepth;
			_ = 0.001f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angleDegrees, ContactFilter2D contactFilter, Collider2D[] results)
		{
			int num = Physics2D.OverlapCapsule(point, size, direction, angleDegrees, contactFilter, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angleDegrees, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			int num = Physics2D.OverlapCapsule(point, size, direction, angleDegrees, contactFilter, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angleDegrees)
		{
			Collider2D[] array = Physics2D.OverlapCapsuleAll(point, size, direction, angleDegrees);
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angleDegrees, int layerMask)
		{
			Collider2D[] array = Physics2D.OverlapCapsuleAll(point, size, direction, angleDegrees, layerMask);
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angleDegrees, int layerMask, float minDepth)
		{
			Collider2D[] array = Physics2D.OverlapCapsuleAll(point, size, direction, angleDegrees, layerMask, minDepth);
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angleDegrees, int layerMask, float minDepth, float maxDepth)
		{
			Collider2D[] array = Physics2D.OverlapCapsuleAll(point, size, direction, angleDegrees, layerMask, minDepth, maxDepth);
			_ = maxDepth - minDepth;
			_ = 0.001f;
			Collider2D[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				_ = array2[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angleDegrees, Collider2D[] results)
		{
			int num = Physics2D.OverlapCapsuleNonAlloc(point, size, direction, angleDegrees, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angleDegrees, Collider2D[] results, int layerMask)
		{
			int num = Physics2D.OverlapCapsuleNonAlloc(point, size, direction, angleDegrees, results, layerMask);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angleDegrees, Collider2D[] results, int layerMask, float minDepth)
		{
			int num = Physics2D.OverlapCapsuleNonAlloc(point, size, direction, angleDegrees, results, layerMask, minDepth);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angleDegrees, Collider2D[] results, int layerMask, float minDepth, float maxDepth)
		{
			int num = Physics2D.OverlapCapsuleNonAlloc(point, size, direction, angleDegrees, results, layerMask, minDepth, maxDepth);
			_ = maxDepth - minDepth;
			_ = 0.001f;
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCollider(Collider2D collider, ContactFilter2D contactFilter, Collider2D[] results)
		{
			int num = Physics2D.OverlapCollider(collider, contactFilter, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int OverlapCollider(Collider2D collider, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			int num = Physics2D.OverlapCollider(collider, contactFilter, results);
			for (int i = 0; i < num; i++)
			{
			}
			return num;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Collider2D collider1, Collider2D collider2, ContactFilter2D contactFilter, ContactPoint2D[] contacts)
		{
			int contacts2 = Physics2D.GetContacts(collider1, collider2, contactFilter, contacts);
			for (int i = 0; i < contacts2; i++)
			{
			}
			return contacts2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Collider2D collider, ContactPoint2D[] contacts)
		{
			int contacts2 = Physics2D.GetContacts(collider, contacts);
			for (int i = 0; i < contacts2; i++)
			{
			}
			return contacts2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, ContactPoint2D[] contacts)
		{
			int contacts2 = Physics2D.GetContacts(collider, contactFilter, contacts);
			for (int i = 0; i < contacts2; i++)
			{
			}
			return contacts2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Collider2D collider, Collider2D[] colliders)
		{
			int contacts = Physics2D.GetContacts(collider, colliders);
			for (int i = 0; i < contacts; i++)
			{
			}
			return contacts;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, Collider2D[] colliders)
		{
			int contacts = Physics2D.GetContacts(collider, contactFilter, colliders);
			for (int i = 0; i < contacts; i++)
			{
			}
			return contacts;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Rigidbody2D rigidbody, ContactPoint2D[] contacts)
		{
			int contacts2 = Physics2D.GetContacts(rigidbody, contacts);
			for (int i = 0; i < contacts2; i++)
			{
			}
			return contacts2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, ContactPoint2D[] contacts)
		{
			int contacts2 = Physics2D.GetContacts(rigidbody, contactFilter, contacts);
			for (int i = 0; i < contacts2; i++)
			{
			}
			return contacts2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Rigidbody2D rigidbody, Collider2D[] colliders)
		{
			int contacts = Physics2D.GetContacts(rigidbody, colliders);
			for (int i = 0; i < contacts; i++)
			{
			}
			return contacts;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, Collider2D[] colliders)
		{
			int contacts = Physics2D.GetContacts(rigidbody, contactFilter, colliders);
			for (int i = 0; i < contacts; i++)
			{
			}
			return contacts;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Collider2D collider1, Collider2D collider2, ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
		{
			int contacts2 = Physics2D.GetContacts(collider1, collider2, contactFilter, contacts);
			for (int i = 0; i < contacts2; i++)
			{
			}
			return contacts2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Collider2D collider, List<ContactPoint2D> contacts)
		{
			int contacts2 = Physics2D.GetContacts(collider, contacts);
			for (int i = 0; i < contacts2; i++)
			{
			}
			return contacts2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
		{
			int contacts2 = Physics2D.GetContacts(collider, contactFilter, contacts);
			for (int i = 0; i < contacts2; i++)
			{
			}
			return contacts2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Collider2D collider, List<Collider2D> colliders)
		{
			int contacts = Physics2D.GetContacts(collider, colliders);
			for (int i = 0; i < contacts; i++)
			{
			}
			return contacts;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, List<Collider2D> colliders)
		{
			int contacts = Physics2D.GetContacts(collider, contactFilter, colliders);
			for (int i = 0; i < contacts; i++)
			{
			}
			return contacts;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Rigidbody2D rigidbody, List<ContactPoint2D> contacts)
		{
			int contacts2 = Physics2D.GetContacts(rigidbody, contacts);
			for (int i = 0; i < contacts2; i++)
			{
			}
			return contacts2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
		{
			int contacts2 = Physics2D.GetContacts(rigidbody, contactFilter, contacts);
			for (int i = 0; i < contacts2; i++)
			{
			}
			return contacts2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Rigidbody2D rigidbody, List<Collider2D> colliders)
		{
			int contacts = Physics2D.GetContacts(rigidbody, colliders);
			for (int i = 0; i < contacts; i++)
			{
			}
			return contacts;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, List<Collider2D> colliders)
		{
			int contacts = Physics2D.GetContacts(rigidbody, contactFilter, colliders);
			for (int i = 0; i < contacts; i++)
			{
			}
			return contacts;
		}
	}
}
