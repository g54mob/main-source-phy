using System;
using System.Collections.Generic;
using System.Linq;
using Dreamteck.Splines;
using Poly.Extension;
using Poly.Physics;
using UnityEngine;

namespace Poly.Collide.Unity
{
	public static class SplineComputerToPolygonCollider
	{
		public static GameObject BuildRigidTerrainFromSurfaceSpline(SplineComputer spline, bool isFlipped, bool zeroXOnLastElem)
		{
			float terrainRadius = 0.1f;
			GameObject gameObject = new GameObject("RB Terrain");
			Transform transform = gameObject.transform;
			transform.parent = spline.transform.parent;
			transform.position = spline.transform.position;
			Poly.Physics.Rigidbody rigidbody = gameObject.AddComponent<Poly.Physics.Rigidbody>();
			rigidbody._mass = 0f;
			rigidbody.requestFullRecollision = true;
			List<Vector2> list = (from splinePoint in spline.GetPoints(SplineComputer.Space.Local)
				select (Vector2)splinePoint.position - terrainRadius * Vector2.up).ToList();
			if (zeroXOnLastElem)
			{
				int index = list.Count - 1;
				Vector2 value = list[index];
				value.x = 0f + terrainRadius * Mathf.Sign(list[0].x);
				list[index] = value;
				list[0] -= Vector2.right * terrainRadius * Mathf.Sign(list[0].x);
			}
			if (isFlipped)
			{
				list.Reverse();
			}
			if (list.Last().x < list.First().x)
			{
				for (int num = 0; num < list.Count; num++)
				{
					Vector2 value2 = list[num];
					value2.x *= -1f;
					list[num] = value2;
				}
			}
			float num2 = 0.001f;
			for (int num3 = list.Count - 2; num3 >= 0; num3--)
			{
				if (Vector2.Distance(list[num3 + 1], list[num3]) < num2)
				{
					list.RemoveAt(num3);
				}
			}
			List<List<Vector2>> list2 = new List<List<Vector2>>();
			list2.Add(list);
			foreach (List<Vector2> item in list2)
			{
				GameObject gameObject2 = new GameObject("PolyCollider");
				Transform transform2 = gameObject2.transform;
				transform2.parent = gameObject.transform;
				transform2.SetLocalTransformToIdentity();
				PolygonCollider polygonCollider = gameObject2.AddComponent<PolygonCollider>();
				polygonCollider.layer = Layer.Terrain;
				polygonCollider.radius = terrainRadius;
				Transform transform3 = new GameObject($"Vert {0:00}").transform;
				transform3.parent = polygonCollider.transform;
				transform3.localPosition = new Vector2(item.Last().x, 0f);
				transform3.SetY(0f + terrainRadius);
				Transform transform4 = new GameObject($"Vert {1:00}").transform;
				transform4.parent = polygonCollider.transform;
				transform4.localPosition = new Vector2(item.First().x, 0f);
				transform4.SetY(0f + terrainRadius);
				for (int num4 = 0; num4 < item.Count; num4++)
				{
					Transform transform5 = new GameObject($"Vert {num4 + 2:00}").transform;
					transform5.parent = polygonCollider.transform;
					transform5.localPosition = item[num4];
				}
			}
			return gameObject;
		}

		public static GameObject BuildRigidBodyFromSplines(SplineComputer[] splines, Layer layer, bool isFlipped = false, bool isTerrainIsland = false, bool isMiddleIsland = false)
		{
			GameObject gameObject = new GameObject("RB Placeable");
			Transform transform = gameObject.transform;
			transform.parent = splines[0].transform.parent;
			transform.position = splines[0].transform.position;
			Poly.Physics.Rigidbody rigidbody = gameObject.AddComponent<Poly.Physics.Rigidbody>();
			rigidbody._mass = 0f;
			rigidbody.requestFullRecollision = true;
			foreach (SplineComputer splineComputer in splines)
			{
				Vec2[] array = (from splinePoint in splineComputer.GetPoints(SplineComputer.Space.Local)
					select (Vec2)splinePoint.position).ToArray();
				if (Vector3.Distance(array.Last(), array.First()) < 0.001f)
				{
					Array.Resize(ref array, array.Length - 1);
				}
				if (isTerrainIsland)
				{
					RoundLeftTopMostVertex(ref array);
					if (isMiddleIsland)
					{
						for (int num = 0; num < array.Length; num++)
						{
							array[num].x *= -1f;
						}
						RoundLeftTopMostVertex(ref array);
						for (int num2 = 0; num2 < array.Length; num2++)
						{
							array[num2].x *= -1f;
						}
					}
				}
				for (int num3 = 0; num3 < array.Length; num3++)
				{
					if (isFlipped)
					{
						array[num3].x *= -1f;
					}
					array[num3] = (Vector2)splineComputer.transform.TransformPoint(array[num3]);
				}
				if ((splineComputer.transform.lossyScale.x * splineComputer.transform.lossyScale.y < 0f) ^ isFlipped)
				{
					Array.Reverse(array);
				}
				GameObject gameObject2 = new GameObject("PolyCollider");
				Transform transform2 = gameObject2.transform;
				transform2.parent = gameObject.transform;
				transform2.SetLocalTransformToIdentity();
				PolygonCollider polygonCollider = gameObject2.AddComponent<PolygonCollider>();
				polygonCollider.layer = layer;
				for (int num4 = 0; num4 < array.Length; num4++)
				{
					Transform transform3 = new GameObject($"Vert {num4 + 2:00}").transform;
					transform3.parent = polygonCollider.transform;
					transform3.position = array[num4];
				}
			}
			return gameObject;
		}

		private static void RoundLeftTopMostVertex(ref Vec2[] outline)
		{
			Vec2 vec = new Vec2(float.MinValue, float.MinValue);
			int num = -1;
			for (int i = 0; i < outline.Length; i++)
			{
				Vec2 vec2 = outline[i];
				if (vec.x + 0.05f < vec2.x || (vec.x - 0.05f < vec2.x && vec.y < vec2.y))
				{
					num = i;
					vec = vec2;
				}
			}
			if (num < 0)
			{
				return;
			}
			Vec2 vec3 = outline[(num - 1 + outline.Length) % outline.Length] - vec;
			Vec2 vec4 = outline[(num + 1) % outline.Length] - vec;
			float num2 = Vector2.Angle(vec3, vec4);
			float num3 = num2 * (MathF.PI / 180f);
			if (170f < num2 || num2 < 30f)
			{
				return;
			}
			float num4 = 0.1f / Mathf.Tan(num3 / 2f);
			float num5 = num4 * num4;
			if (vec3.sqrMagnitude < num5 || vec4.sqrMagnitude < num5)
			{
				return;
			}
			Vec2 normalized = (vec3.normalized + vec4.normalized).normalized;
			float num6 = 0.1f / Mathf.Cos(num3 / 2f);
			Vec2 vec5 = vec + num6 * normalized;
			int num7 = Mathf.FloorToInt((180f - num2) / 22.5f);
			if (num7 >= 1)
			{
				int num8 = outline.Length;
				Array.Resize(ref outline, outline.Length + num7);
				int num9 = num8 - 1;
				while (num + 1 <= num9)
				{
					outline[num9 + num7] = outline[num9];
					num9--;
				}
				Vec2 a = vec + vec3.normalized * num4;
				Vec2 b = vec + vec4.normalized * num4;
				int num10 = 0;
				int num11 = num;
				while (num11 <= num + num7)
				{
					float t = (float)num10 / ((float)num7 + 1f);
					Vec2 vec6 = Vec2.LerpUnclamped(in a, in b, t);
					vec6 = (vec6 - vec5).normalized * 0.1f + vec5;
					outline[num11] = vec6;
					num11++;
					num10++;
				}
			}
		}
	}
}
