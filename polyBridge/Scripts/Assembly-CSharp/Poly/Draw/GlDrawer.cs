using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Poly.Base;
using Poly.Math;
using UnityEngine;

namespace Poly.Draw
{
	public class GlDrawer : Singleton<GlDrawer>
	{
		public struct Label
		{
			public Color color;

			public Vector3 position;

			public string label;
		}

		internal List<Vector3> lines = new List<Vector3>();

		internal List<Color> colors = new List<Color>();

		internal List<Label> labels = new List<Label>();

		private static Vector3[] circlePoints;

		public static Color color { get; set; }

		public static Vector3 offset { get; set; }

		static GlDrawer()
		{
			color = new Color(0.2f, 1f, 0.2f, 0.3f);
			InitCirclePoints(24);
		}

		private void _Clear()
		{
			lines.Clear();
			colors.Clear();
			labels.Clear();
		}

		public static void Clear()
		{
			Singleton<GlDrawer, int>.instance._Clear();
		}

		public static void DrawLine(Vector3 a, Vector3 b)
		{
			Singleton<GlDrawer, int>.instance._DrawLine(a, b);
		}

		public static void DrawLine(Vector3 a, Vector3 b, Color c)
		{
			Singleton<GlDrawer, int>.instance._DrawLine(a, b, c);
		}

		public static void DrawArrow(Vector3 origin, Vector3 direction)
		{
			Singleton<GlDrawer, int>.instance._DrawArrow(origin, direction, color);
		}

		public static void DrawArrow(Vector3 origin, Vector3 direction, Color c)
		{
			Singleton<GlDrawer, int>.instance._DrawArrow(origin, direction, c);
		}

		public static void DrawWireCube(Vector3 position, Quaternion rotation, Vector3 size)
		{
			Singleton<GlDrawer, int>.instance._DrawWireCube(position, rotation, size);
		}

		public static void DrawWireSquareXY(Vector3 position, Vector3 size)
		{
			Singleton<GlDrawer, int>.instance._DrawWireSquareXY(position, size);
		}

		public static void DrawCircle(Vector3 position, float radius, float angle = 0f)
		{
			Singleton<GlDrawer, int>.instance._DrawCircle(position, radius, angle);
		}

		public static void DrawCross(Vector3 position, float size, float angle = 0f)
		{
			Singleton<GlDrawer, int>.instance._DrawCross(position, size, angle);
		}

		public static void DrawRefFrame(Transform3 t, float size)
		{
			Singleton<GlDrawer, int>.instance._DrawRefFrame(t, size);
		}

		public static void DrawLabel(Vector3 position, string label)
		{
			Singleton<GlDrawer, int>.instance._DrawLabel(position, label);
		}

		public static void DrawLabel(Vector3 position, string label, Color c)
		{
			Singleton<GlDrawer, int>.instance._DrawLabel(position, label, c);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void _DrawLine(Vector3 a, Vector3 b)
		{
			lines.Add(a);
			lines.Add(b);
			colors.Add(color);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void _DrawLine(Vector3 a, Vector3 b, Color c)
		{
			lines.Add(a);
			lines.Add(b);
			colors.Add(c);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void _DrawArrow(Vector3 origin, Vector3 direction, Color color)
		{
			float magnitude = direction.magnitude;
			float num = 0.25f * Mathf.Min(1f, magnitude);
			float num2 = 0.33f * num;
			Vector3 a = origin + direction;
			Vector3 vector = origin + (1f - num / magnitude) * direction;
			Vector3 vector2 = 0.5f * num2 * ((Vec2)direction).rotated90.normalized;
			_DrawLine(origin, vector, color);
			_DrawLine(a, vector + vector2, color);
			_DrawLine(a, vector - vector2, color);
			_DrawLine(vector + vector2, vector - vector2, color);
		}

		private void _DrawWireCube(Vector3 position, Quaternion rotation, Vector3 size)
		{
			Vector3[] array = new Vector3[8];
			for (int i = 0; i < 8; i++)
			{
				array[i] = Vector3.Scale(0.5f * size, new Vector3(i / 4 * 2 - 1, i / 2 % 2 * 2 - 1, i % 2 * 2 - 1));
				array[i] = position + rotation * array[i];
			}
			for (int j = 0; j < 4; j++)
			{
				_DrawLine(array[j], array[j + 4]);
				_DrawLine(array[2 * j], array[2 * j + 1]);
				_DrawLine(array[j + j / 2 * 2], array[j + j / 2 * 2 + 2]);
			}
		}

		private void _DrawWireSquareXY(Vector3 position, Vector3 size)
		{
			Vector3[] array = new Vector3[4];
			for (int i = 0; i < 4; i++)
			{
				array[i] = position + Vector3.Scale(0.5f * size, new Vector3(i / 2 * 2 - 1, i % 2 * 2 - 1));
			}
			for (int j = 0; j < 2; j++)
			{
				_DrawLine(array[j], array[j + 2]);
				_DrawLine(array[2 * j], array[2 * j + 1]);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void _DrawCircle(Vector3 position, float radius, float angle)
		{
			Vector3 a = position + circlePoints[circlePoints.Length - 1] * radius;
			for (int i = 0; i < circlePoints.Length; i++)
			{
				Vector3 vector = position + circlePoints[i] * radius;
				_DrawLine(a, vector);
				a = vector;
			}
			_DrawCross(position, 2f * radius, angle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void _DrawCross(Vector3 position, float size, float angle)
		{
			Rotation2.SetRotation_Slow(angle, out var result);
			Vector3 vector = 0.5f * size * result.right;
			Vector3 vector2 = 0.5f * size * result.up;
			_DrawLine(position - vector, position + vector);
			_DrawLine(position - vector2, position + vector2);
		}

		private void _DrawRefFrame(Transform3 t, float size)
		{
			_DrawLine(t.position, t.position + t.rotation * Vector3.right * size, Color.red);
			_DrawLine(t.position, t.position + t.rotation * Vector3.up * size, Color.green);
			_DrawLine(t.position, t.position + t.rotation * Vector3.forward * size, Color.blue);
		}

		public void _DrawLabel(Vector3 position, string label)
		{
			labels.Add(new Label
			{
				label = label,
				position = position,
				color = color
			});
		}

		public void _DrawLabel(Vector3 position, string label, Color color)
		{
			labels.Add(new Label
			{
				label = label,
				position = position,
				color = color
			});
		}

		private static void InitCirclePoints(int numPoints)
		{
			circlePoints = new Vector3[numPoints];
			for (int i = 0; i < numPoints; i++)
			{
				float f = (float)i / (float)numPoints * 2f * MathF.PI;
				float y = Mathf.Sin(f);
				float x = Mathf.Cos(f);
				circlePoints[i] = new Vector3(x, y, 0f);
			}
		}
	}
}
