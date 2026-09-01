using System;
using System.Collections.Generic;
using Poly.Extension;
using Poly.Geometry;
using Poly.Math;
using Poly.Physics;
using Poly.Physics.ThirdParty;
using UnityEngine;

namespace Poly.Collide.Unity
{
	[DisallowMultipleComponent]
	public class PolygonCollider : MonoBehaviour
	{
		public float radius;

		public PhysicsMaterial2D physicsMaterial;

		public Layer layer;

		public List<Vec2> points = new List<Vec2>();

		public bool hasInternalPoints => 0 < points.Count;

		public int vertCount
		{
			get
			{
				if (0 >= points.Count)
				{
					return base.transform.childCount;
				}
				return points.Count;
			}
		}

		public PolygonShape[] CreateConvexPolygons(in Transform2 shapeOrigin, bool flipSourceVertexX = false)
		{
			ConvertTransformsToInternalPoints_Runtime();
			if (points.Count <= 3)
			{
				PolygonShape polygonShape = CreatePolygon(in shapeOrigin, flipSourceVertexX);
				return new PolygonShape[1] { polygonShape };
			}
			float num = (flipSourceVertexX ? (-1f) : 1f);
			List<Vec2> list = new List<Vec2>(points.Count);
			for (int i = 0; i < points.Count; i++)
			{
				Vec2 vec = (Vec2)base.transform.TransformPoint(points[i]);
				vec.x *= num;
				list.Add(shapeOrigin.rotation.InvMul(vec - shapeOrigin.position));
				list[i] = new Vec2((float)Mathf.RoundToInt(list[i].x / 0.0001f) * 0.0001f, (float)Mathf.RoundToInt(list[i].y / 0.0001f) * 0.0001f);
			}
			MergeNearNeighboringVerticesAndNearParallelNeighboringPolygonEdges(list);
			if (list.Count < 3)
			{
				return new PolygonShape[0];
			}
			List<List<Vec2>> list2 = null;
			try
			{
				list2 = ConvexDecomposer.ConvexPartition(list);
			}
			catch (SystemException ex)
			{
				Debug.LogWarning("Convex decomposition failed due to '" + ex.Message + "'");
				return new PolygonShape[0];
			}
			PolygonUtil.SubdivideConvexShapeVertices_ForMultipleShapes(list2, 32);
			int num2 = 0;
			PolygonShape[] array = new PolygonShape[list2.Count];
			foreach (List<Vec2> item in list2)
			{
				item.Reverse();
				PolygonShape polygonShape2 = new PolygonShape();
				polygonShape2.SetFromPointCloud(item.ToArray());
				float num3 = base.transform.lossyScale.MaxCoordValue() * radius;
				polygonShape2.radius = num3;
				array[num2++] = polygonShape2;
			}
			return array;
		}

		private PolygonShape CreatePolygon(in Transform2 shapeOrigin, bool flipSourceVertexX = false)
		{
			float num = (flipSourceVertexX ? (-1f) : 1f);
			Vec2[] array = new Vec2[points.Count];
			for (int i = 0; i < points.Count; i++)
			{
				Vec2 vec = (Vec2)base.transform.TransformPoint(points[i]);
				vec.x *= num;
				array[i] = shapeOrigin.rotation.InvMul(vec - shapeOrigin.position);
			}
			PolygonShape polygonShape = new PolygonShape();
			polygonShape.SetFromPointCloud(array);
			float num2 = base.transform.lossyScale.MaxCoordValue() * radius;
			polygonShape.radius = num2;
			return polygonShape;
		}

		private void MergeNearNeighboringVerticesAndNearParallelNeighboringPolygonEdges(List<Vec2> vertsInLocal)
		{
			Vec2 a = vertsInLocal[0];
			int num = vertsInLocal.Count - 1;
			while (0 <= num && 1 < vertsInLocal.Count)
			{
				Vec2 b = vertsInLocal[num];
				if (Vec2.DistanceSqr(in a, in b) < 9.999999E-09f)
				{
					if (!(a == b))
					{
						Debug.LogWarning("Near-identical neighboring vertices found in a Rigidbody shape");
					}
					vertsInLocal.RemoveAt(num);
				}
				else
				{
					a = b;
				}
				num--;
			}
			if (3 > vertsInLocal.Count)
			{
				return;
			}
			Vec2 a2 = vertsInLocal[1];
			Vec2 b2 = vertsInLocal[0];
			int num2 = vertsInLocal.Count - 1;
			while (0 <= num2 && 2 < vertsInLocal.Count)
			{
				Vec2 b3 = vertsInLocal[num2];
				float num3 = Vec2.DistanceSqr(in a2, in b2);
				float num4 = Vec2.DistanceSqr(in b2, in b3);
				float num5 = Vec2.DistanceSqr(in b3, in a2);
				bool flag = false;
				if (num3 < num5 && num4 < num5)
				{
					Vec2.setRotated90(b3 - a2, out var v);
					v.Normalize();
					if (Mathf.Abs(Vec2.Dot(b2 - b3, in v)) < 0.0001f)
					{
						vertsInLocal.RemoveAt((num2 + 1) % vertsInLocal.Count);
						flag = true;
						b2 = b3;
					}
				}
				if (!flag)
				{
					a2 = b2;
					b2 = b3;
				}
				num2--;
			}
		}

		private void ConvertTransformsToInternalPoints_Runtime()
		{
			if (points.Count == 0)
			{
				for (int i = 0; i < base.transform.childCount; i++)
				{
					Transform child = base.transform.GetChild(i);
					points.Add((Vec2)child.localPosition);
				}
				int num = base.transform.childCount - 1;
				while (0 <= num)
				{
					UnityEngine.Object.DestroyImmediate(base.transform.GetChild(num).gameObject);
					num--;
				}
			}
			else if ((1 == base.transform.childCount && !base.transform.GetChild(0).GetComponent<Renderer>()) || 1 < base.transform.childCount)
			{
				string text = GetComponentInParent<Poly.Physics.Vehicle>()?.name;
				if (text == null)
				{
					text = GetComponentInParent<Poly.Physics.Rigidbody>()?.name;
				}
				if (text == null)
				{
					text = base.name;
				}
				Debug.LogWarning("PolygonCollider has both internal points defined, and has child transforms that define points. Child transforms are ignored: " + text);
			}
		}
	}
}
