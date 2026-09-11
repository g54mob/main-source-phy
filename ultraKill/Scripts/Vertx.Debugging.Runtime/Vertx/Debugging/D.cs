using System.Diagnostics;
using Unity.Burst;
using UnityEngine;

namespace Vertx.Debugging
{
	public static class D
	{
		[Conditional("UNITY_EDITOR")]
		public static void raw<T>(T shape, float duration = 0f) where T : struct, IDrawable
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw<T>(T shape, Color color, float duration = 0f) where T : struct, IDrawable
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw<T>(T shape, bool hit, float duration = 0f) where T : struct, IDrawable
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw<T>(T shape, Color castColor, Color hitColor, float duration = 0f) where T : struct, IDrawableCast
		{
		}

		[BurstDiscard]
		[Conditional("UNITY_EDITOR")]
		public static void raw(IDrawableManaged shape, float duration = 0f)
		{
		}

		[BurstDiscard]
		[Conditional("UNITY_EDITOR")]
		public static void raw(IDrawableManaged shape, Color color, float duration = 0f)
		{
		}

		[BurstDiscard]
		[Conditional("UNITY_EDITOR")]
		public static void raw(IDrawableManaged shape, bool hit, float duration = 0f)
		{
		}

		[BurstDiscard]
		[Conditional("UNITY_EDITOR")]
		public static void raw(IDrawableCastManaged shape, Color castColor, Color hitColor, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Ray ray, Color color, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Ray ray, bool hit, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Ray ray, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Ray2D ray, Color color, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Ray2D ray, bool hit, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Ray2D ray, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Vector3 position, Color color, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Vector3 position, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Vector3 position, bool hit, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Vector2 position, Color color, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Vector2 position, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Vector2 position, bool hit, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Bounds bounds, Color color, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Bounds bounds, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Bounds bounds, bool hit, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(BoundsInt bounds, Color color, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(BoundsInt bounds, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(BoundsInt bounds, bool hit, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Rect rect, Color color, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Rect rect, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Rect rect, bool hit, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(RectInt rect, Color color, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(RectInt rect, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(RectInt rect, bool hit, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(RaycastHit hit, Color color, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(RaycastHit hit, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Collider collider, Color color, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Collider collider, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Collider collider, bool hit, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(RaycastHit2D hit, Color color, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(RaycastHit2D hit, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Collider2D collider, Color color, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Collider2D collider, float duration = 0f)
		{
		}

		[Conditional("UNITY_EDITOR")]
		public static void raw(Collider2D collider, bool hit, float duration = 0f)
		{
		}
	}
}
