using System.Runtime.CompilerServices;
using UnityEngine;

namespace Poly.Extension
{
	public static class TransformExtension
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetX(this Transform t, float x)
		{
			Vector3 position = t.position;
			position.x = x;
			t.position = position;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetY(this Transform t, float y)
		{
			Vector3 position = t.position;
			position.y = y;
			t.position = position;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetZ(this Transform t, float z)
		{
			Vector3 position = t.position;
			position.z = z;
			t.position = position;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLocalX(this Transform t, float x)
		{
			Vector3 localPosition = t.localPosition;
			localPosition.x = x;
			t.localPosition = localPosition;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLocalY(this Transform t, float y)
		{
			Vector3 localPosition = t.localPosition;
			localPosition.y = y;
			t.localPosition = localPosition;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLocalZ(this Transform t, float z)
		{
			Vector3 localPosition = t.localPosition;
			localPosition.z = z;
			t.localPosition = localPosition;
		}

		public static void SetLocalTransformToIdentity(this Transform t)
		{
			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;
			t.localScale = Vector3.one;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLocalScaleX(this Transform t, float x)
		{
			Vector3 localScale = t.localScale;
			localScale.x = x;
			t.localScale = localScale;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLocalScaleY(this Transform t, float y)
		{
			Vector3 localScale = t.localScale;
			localScale.y = y;
			t.localScale = localScale;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLocalScaleZ(this Transform t, float z)
		{
			Vector3 localScale = t.localScale;
			localScale.z = z;
			t.localScale = localScale;
		}

		public static void DestroyAllChildren(this Transform t)
		{
			for (int i = 0; i < t.childCount; i++)
			{
				Object.Destroy(t.GetChild(i).gameObject);
			}
		}
	}
}
