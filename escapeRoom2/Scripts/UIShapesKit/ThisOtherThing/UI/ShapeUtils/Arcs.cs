using System;
using UnityEngine;
using UnityEngine.UI;

namespace ThisOtherThing.UI.ShapeUtils
{
	public class Arcs
	{
		[Serializable]
		public class ArcProperties
		{
			public enum ArcDirection
			{
				Backward = 0,
				Centered = 1,
				Forward = 2
			}

			public ArcDirection Direction = ArcDirection.Forward;

			[Range(0f, 1f)]
			public float Length = 1f;

			private Vector3 endSegmentUnitPosition = Vector3.zero;

			private Vector3 startTangent = Vector3.zero;

			private Vector3 endTangent = Vector3.zero;

			private Vector3 centerNormal = Vector3.zero;

			public int AdjustedResolution { get; private set; }

			public float AdjustedBaseAngle { get; private set; }

			public float AdjustedDirection { get; private set; }

			public float SegmentAngle { get; private set; }

			public float EndSegmentAngle { get; private set; }

			public Vector3 EndSegmentUnitPosition => endSegmentUnitPosition;

			public Vector3 StartTangent => startTangent;

			public Vector3 EndTangent => endTangent;

			public Vector3 CenterNormal => centerNormal;

			public void UpdateAdjusted(int FullCircleResolution, float BaseAngle)
			{
				switch (Direction)
				{
				case ArcDirection.Backward:
					AdjustedDirection = -1f;
					break;
				case ArcDirection.Centered:
					AdjustedDirection = 1f;
					BaseAngle -= Length;
					break;
				case ArcDirection.Forward:
					AdjustedDirection = 1f;
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				AdjustedResolution = Mathf.CeilToInt((float)FullCircleResolution * Length);
				AdjustedBaseAngle = BaseAngle * MathF.PI;
				SegmentAngle = MathF.PI * 2f / (float)AdjustedResolution;
				EndSegmentAngle = AdjustedBaseAngle + MathF.PI * 2f * Length * AdjustedDirection;
				endSegmentUnitPosition.x = Mathf.Sin(EndSegmentAngle);
				endSegmentUnitPosition.y = Mathf.Cos(EndSegmentAngle);
				endTangent.x = endSegmentUnitPosition.y * AdjustedDirection;
				endTangent.y = endSegmentUnitPosition.x * (0f - AdjustedDirection);
				startTangent.x = Mathf.Cos(AdjustedBaseAngle) * (0f - AdjustedDirection);
				startTangent.y = Mathf.Sin(AdjustedBaseAngle) * AdjustedDirection;
				float f = AdjustedBaseAngle + MathF.PI * Length * AdjustedDirection;
				float b = 1f / Mathf.Sin(MathF.PI * Length);
				b = Mathf.Min(4f, b);
				centerNormal.x = (0f - Mathf.Sin(f)) * b;
				centerNormal.y = (0f - Mathf.Cos(f)) * b;
			}
		}

		private static Vector3 tmpPosition = Vector3.zero;

		private static Vector2 tmpInnerRadius = Vector2.one;

		private static Vector2 tmpOuterRadius = Vector2.one;

		private static Vector2 tmpArcInnerRadius = Vector2.one;

		private static Vector2 tmpArcOuterRadius = Vector2.one;

		private static Vector3 tmpOffsetCenter = Vector3.one;

		private static Vector3 noOverlapInnerOffset = Vector3.zero;

		private static Vector3 noOverlapOuterOffset = Vector3.zero;

		public static void AddSegment(ref VertexHelper vh, Vector2 center, Vector2 radius, Ellipses.EllipseProperties circleProperties, ArcProperties arcProperties, Color32 color, Vector2 uv, ref GeoUtils.UnitPositionData unitPositionData, GeoUtils.EdgeGradientData edgeGradientData)
		{
			if (arcProperties.Length <= 0f)
			{
				return;
			}
			bool flag = arcProperties.Direction == ArcProperties.ArcDirection.Backward;
			int num = ((!flag) ? (-1) : 0);
			int num2 = (flag ? (-1) : 0);
			int num3 = (flag ? 1 : 2);
			int num4 = ((!flag) ? 1 : 2);
			int num5 = (flag ? 1 : 0);
			int num6 = ((!flag) ? 1 : 0);
			GeoUtils.SetUnitPositionData(ref unitPositionData, circleProperties.AdjustedResolution, arcProperties.AdjustedBaseAngle, arcProperties.AdjustedDirection);
			int currentVertCount = vh.currentVertCount;
			tmpOuterRadius.x = (radius.x + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale;
			tmpOuterRadius.y = (radius.y + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale;
			float num7 = edgeGradientData.ShadowOffset * edgeGradientData.InnerScale;
			tmpOffsetCenter.x = center.x + arcProperties.CenterNormal.x * radius.x * (edgeGradientData.InnerScale - 1f) * 0.2f;
			tmpOffsetCenter.y = center.y + arcProperties.CenterNormal.y * radius.y * (edgeGradientData.InnerScale - 1f) * 0.2f;
			tmpOffsetCenter.z = 0f;
			if (arcProperties.Length >= 1f)
			{
				num7 = 0f;
				tmpOffsetCenter = center;
			}
			vh.AddVert(tmpOffsetCenter + arcProperties.CenterNormal * num7, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			tmpPosition.x = tmpOffsetCenter.x + unitPositionData.UnitPositions[0].x * tmpOuterRadius.x + arcProperties.StartTangent.x * num7;
			tmpPosition.y = tmpOffsetCenter.y + unitPositionData.UnitPositions[0].y * tmpOuterRadius.y + arcProperties.StartTangent.y * num7;
			tmpPosition.z = tmpOffsetCenter.z;
			vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			for (int i = 1; i < arcProperties.AdjustedResolution; i++)
			{
				tmpPosition.x = tmpOffsetCenter.x + unitPositionData.UnitPositions[i].x * tmpOuterRadius.x;
				tmpPosition.y = tmpOffsetCenter.y + unitPositionData.UnitPositions[i].y * tmpOuterRadius.y;
				tmpPosition.z = tmpOffsetCenter.z;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				vh.AddTriangle(currentVertCount, currentVertCount + i + num, currentVertCount + i + num2);
			}
			int num8 = currentVertCount + arcProperties.AdjustedResolution;
			tmpPosition.x = tmpOffsetCenter.x + arcProperties.EndSegmentUnitPosition.x * tmpOuterRadius.x + arcProperties.EndTangent.x * num7;
			tmpPosition.y = tmpOffsetCenter.y + arcProperties.EndSegmentUnitPosition.y * tmpOuterRadius.y + arcProperties.EndTangent.y * num7;
			tmpPosition.z = tmpOffsetCenter.z + arcProperties.EndTangent.z * num7;
			vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			if (flag)
			{
				vh.AddTriangle(currentVertCount, num8, num8 - 1);
				vh.AddTriangle(currentVertCount, num8 + 1, num8);
			}
			else
			{
				vh.AddTriangle(currentVertCount, num8 - 1, num8);
				vh.AddTriangle(currentVertCount, num8, num8 + 1);
			}
			if (!edgeGradientData.IsActive)
			{
				return;
			}
			radius.x += edgeGradientData.SizeAdd + edgeGradientData.ShadowOffset;
			radius.y += edgeGradientData.SizeAdd + edgeGradientData.ShadowOffset;
			color.a = 0;
			tmpPosition.x = center.x + unitPositionData.UnitPositions[0].x * radius.x;
			tmpPosition.y = center.y + unitPositionData.UnitPositions[0].y * radius.y;
			tmpPosition.z = 0f;
			vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			for (int j = 1; j <= arcProperties.AdjustedResolution; j++)
			{
				if (j < arcProperties.AdjustedResolution)
				{
					tmpPosition.x = center.x + unitPositionData.UnitPositions[j].x * radius.x;
					tmpPosition.y = center.y + unitPositionData.UnitPositions[j].y * radius.y;
					tmpPosition.z = 0f;
					vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				}
				else
				{
					tmpPosition.x = center.x + arcProperties.EndSegmentUnitPosition.x * radius.x;
					tmpPosition.y = center.y + arcProperties.EndSegmentUnitPosition.y * radius.y;
					tmpPosition.z = 0f;
					vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				}
				int num9 = currentVertCount + j;
				int num10 = num9 + arcProperties.AdjustedResolution;
				vh.AddTriangle(num10 + 2, num9 + num6, num9 + num5);
				vh.AddTriangle(num9, num10 + num4, num10 + num3);
			}
			if (!(arcProperties.Length >= 1f))
			{
				tmpOuterRadius.x = edgeGradientData.SizeAdd + edgeGradientData.ShadowOffset;
				tmpOuterRadius.y = tmpOuterRadius.x;
				tmpPosition.x = center.x + unitPositionData.UnitPositions[0].x * radius.x + arcProperties.StartTangent.x * tmpOuterRadius.x;
				tmpPosition.y = center.y + unitPositionData.UnitPositions[0].y * radius.y + arcProperties.StartTangent.y * tmpOuterRadius.y;
				tmpPosition.z = 0f;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				tmpPosition.x = center.x + arcProperties.EndSegmentUnitPosition.x * radius.x + arcProperties.EndTangent.x * tmpOuterRadius.x;
				tmpPosition.y = center.y + arcProperties.EndSegmentUnitPosition.y * radius.y + arcProperties.EndTangent.y * tmpOuterRadius.y;
				tmpPosition.z = 0f;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				radius.x -= edgeGradientData.SizeAdd;
				radius.y -= edgeGradientData.SizeAdd;
				tmpPosition.x = center.x + unitPositionData.UnitPositions[0].x * radius.x + arcProperties.StartTangent.x * tmpOuterRadius.x;
				tmpPosition.y = center.y + unitPositionData.UnitPositions[0].y * radius.y + arcProperties.StartTangent.y * tmpOuterRadius.y;
				tmpPosition.z = 0f;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				tmpPosition.x = center.x + arcProperties.EndSegmentUnitPosition.x * radius.x + arcProperties.EndTangent.x * tmpOuterRadius.x;
				tmpPosition.y = center.y + arcProperties.EndSegmentUnitPosition.y * radius.y + arcProperties.EndTangent.y * tmpOuterRadius.y;
				tmpPosition.z = 0f;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				tmpPosition.x = center.x + arcProperties.CenterNormal.x * tmpOuterRadius.x;
				tmpPosition.y = center.y + arcProperties.CenterNormal.y * tmpOuterRadius.y;
				tmpPosition.z = 0f;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				int num11 = vh.currentVertCount - 5;
				int num12 = currentVertCount + arcProperties.AdjustedResolution;
				int num13 = currentVertCount + arcProperties.AdjustedResolution * 2;
				if (flag)
				{
					vh.AddTriangle(num11, num11 + 2, currentVertCount + 1);
					vh.AddTriangle(num11, currentVertCount + 1, num12 + 2);
					vh.AddTriangle(num11 + 1, num12 + 1, num11 + 3);
					vh.AddTriangle(num11 + 1, num13 + 2, num12 + 1);
					vh.AddTriangle(num11 + 2, currentVertCount, currentVertCount + 1);
					vh.AddTriangle(num11 + 2, num11 + 4, currentVertCount);
					vh.AddTriangle(num11 + 3, num12 + 1, num11 + 4);
					vh.AddTriangle(num11 + 4, num12 + 1, currentVertCount);
				}
				else
				{
					vh.AddTriangle(num11, currentVertCount + 1, num11 + 2);
					vh.AddTriangle(num11, num12 + 2, currentVertCount + 1);
					vh.AddTriangle(num11 + 1, num11 + 3, num12 + 1);
					vh.AddTriangle(num11 + 1, num12 + 1, num13 + 2);
					vh.AddTriangle(num11 + 2, currentVertCount + 1, currentVertCount);
					vh.AddTriangle(num11 + 2, currentVertCount, num11 + 4);
					vh.AddTriangle(num11 + 3, num11 + 4, num12 + 1);
					vh.AddTriangle(num11 + 4, currentVertCount, num12 + 1);
				}
			}
		}

		public static void AddArcRing(ref VertexHelper vh, Vector2 center, Vector2 radius, Ellipses.EllipseProperties ellipseProperties, ArcProperties arcProperties, GeoUtils.OutlineProperties outlineProperties, Color32 color, Vector2 uv, ref GeoUtils.UnitPositionData unitPositionData, GeoUtils.EdgeGradientData edgeGradientData)
		{
			if (arcProperties.Length <= 0f)
			{
				return;
			}
			GeoUtils.SetUnitPositionData(ref unitPositionData, ellipseProperties.AdjustedResolution, arcProperties.AdjustedBaseAngle, arcProperties.AdjustedDirection);
			radius.x += outlineProperties.GetCenterDistace();
			radius.y += outlineProperties.GetCenterDistace();
			float num = (outlineProperties.HalfLineWeight + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale;
			if (arcProperties.Direction == ArcProperties.ArcDirection.Backward)
			{
				tmpInnerRadius.x = radius.x + num;
				tmpInnerRadius.y = radius.y + num;
				tmpOuterRadius.x = radius.x - num;
				tmpOuterRadius.y = radius.y - num;
			}
			else
			{
				tmpInnerRadius.x = radius.x - num;
				tmpInnerRadius.y = radius.y - num;
				tmpOuterRadius.x = radius.x + num;
				tmpOuterRadius.y = radius.y + num;
			}
			float num2 = edgeGradientData.ShadowOffset * edgeGradientData.InnerScale;
			if (arcProperties.Length >= 1f)
			{
				num2 = 0f;
			}
			int currentVertCount = vh.currentVertCount;
			int num3 = currentVertCount - 1;
			tmpPosition.x = center.x + unitPositionData.UnitPositions[0].x * tmpInnerRadius.x + arcProperties.StartTangent.x * num2;
			tmpPosition.y = center.y + unitPositionData.UnitPositions[0].y * tmpInnerRadius.y + arcProperties.StartTangent.y * num2;
			tmpPosition.z = 0f;
			vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			tmpPosition.x = center.x + unitPositionData.UnitPositions[0].x * tmpOuterRadius.x + arcProperties.StartTangent.x * num2;
			tmpPosition.y = center.y + unitPositionData.UnitPositions[0].y * tmpOuterRadius.y + arcProperties.StartTangent.y * num2;
			tmpPosition.z = 0f;
			vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			int num4;
			for (int i = 1; i < arcProperties.AdjustedResolution; i++)
			{
				tmpPosition.x = center.x + unitPositionData.UnitPositions[i].x * tmpInnerRadius.x;
				tmpPosition.y = center.y + unitPositionData.UnitPositions[i].y * tmpInnerRadius.y;
				tmpPosition.z = 0f;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				tmpPosition.x = center.x + unitPositionData.UnitPositions[i].x * tmpOuterRadius.x;
				tmpPosition.y = center.y + unitPositionData.UnitPositions[i].y * tmpOuterRadius.y;
				tmpPosition.z = 0f;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				num4 = num3 + i * 2;
				vh.AddTriangle(num4 - 1, num4, num4 + 1);
				vh.AddTriangle(num4, num4 + 2, num4 + 1);
			}
			tmpPosition.x = center.x + arcProperties.EndSegmentUnitPosition.x * tmpInnerRadius.x + arcProperties.EndTangent.x * num2;
			tmpPosition.y = center.y + arcProperties.EndSegmentUnitPosition.y * tmpInnerRadius.y + arcProperties.EndTangent.y * num2;
			tmpPosition.z = 0f;
			vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			tmpPosition.x = center.x + arcProperties.EndSegmentUnitPosition.x * tmpOuterRadius.x + arcProperties.EndTangent.x * num2;
			tmpPosition.y = center.y + arcProperties.EndSegmentUnitPosition.y * tmpOuterRadius.y + arcProperties.EndTangent.y * num2;
			tmpPosition.z = 0f;
			vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			num4 = num3 + arcProperties.AdjustedResolution * 2;
			vh.AddTriangle(num4 - 1, num4, num4 + 1);
			vh.AddTriangle(num4, num4 + 2, num4 + 1);
			if (!edgeGradientData.IsActive)
			{
				return;
			}
			num = outlineProperties.HalfLineWeight + edgeGradientData.ShadowOffset + edgeGradientData.SizeAdd;
			if (arcProperties.Direction == ArcProperties.ArcDirection.Backward)
			{
				tmpOuterRadius.x = radius.x - num;
				tmpOuterRadius.y = radius.y - num;
				tmpInnerRadius.x = radius.x + num;
				tmpInnerRadius.y = radius.y + num;
			}
			else
			{
				tmpOuterRadius.x = radius.x + num;
				tmpOuterRadius.y = radius.y + num;
				tmpInnerRadius.x = radius.x - num;
				tmpInnerRadius.y = radius.y - num;
			}
			color.a = 0;
			tmpArcInnerRadius.x = Mathf.Max(0f, tmpInnerRadius.x);
			tmpArcInnerRadius.y = Mathf.Max(0f, tmpInnerRadius.y);
			tmpArcOuterRadius.x = Mathf.Max(0f, tmpOuterRadius.x);
			tmpArcOuterRadius.y = Mathf.Max(0f, tmpOuterRadius.y);
			noOverlapInnerOffset.x = arcProperties.CenterNormal.x * (0f - Mathf.Min(0f, tmpInnerRadius.x));
			noOverlapInnerOffset.y = arcProperties.CenterNormal.y * (0f - Mathf.Min(0f, tmpInnerRadius.y));
			noOverlapInnerOffset.z = 0f;
			noOverlapOuterOffset.x = arcProperties.CenterNormal.x * (0f - Mathf.Min(0f, tmpOuterRadius.x));
			noOverlapOuterOffset.y = arcProperties.CenterNormal.y * (0f - Mathf.Min(0f, tmpOuterRadius.y));
			noOverlapOuterOffset.z = 0f;
			if (arcProperties.Length >= 1f)
			{
				noOverlapInnerOffset.x = 0f;
				noOverlapInnerOffset.y = 0f;
				noOverlapInnerOffset.z = 0f;
				noOverlapOuterOffset.x = 0f;
				noOverlapOuterOffset.y = 0f;
				noOverlapOuterOffset.z = 0f;
			}
			int num5;
			int num6;
			for (int j = 0; j < arcProperties.AdjustedResolution; j++)
			{
				tmpPosition.x = center.x + unitPositionData.UnitPositions[j].x * tmpArcInnerRadius.x + noOverlapInnerOffset.x;
				tmpPosition.y = center.y + unitPositionData.UnitPositions[j].y * tmpArcInnerRadius.y + noOverlapInnerOffset.y;
				tmpPosition.z = noOverlapInnerOffset.z;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				tmpPosition.x = center.x + unitPositionData.UnitPositions[j].x * tmpArcOuterRadius.x + noOverlapOuterOffset.x;
				tmpPosition.y = center.y + unitPositionData.UnitPositions[j].y * tmpArcOuterRadius.y + noOverlapOuterOffset.y;
				tmpPosition.z = noOverlapOuterOffset.z;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				num5 = num4 + j * 2;
				num6 = num3 + j * 2;
				if (j > 0)
				{
					vh.AddTriangle(num6 - 1, num6 + 1, num5 + 3);
					vh.AddTriangle(num5 + 1, num6 - 1, num5 + 3);
					vh.AddTriangle(num6, num5 + 2, num6 + 2);
					vh.AddTriangle(num5 + 2, num5 + 4, num6 + 2);
				}
			}
			tmpPosition.x = center.x + arcProperties.EndSegmentUnitPosition.x * tmpArcInnerRadius.x + noOverlapInnerOffset.x;
			tmpPosition.y = center.y + arcProperties.EndSegmentUnitPosition.y * tmpArcInnerRadius.y + noOverlapInnerOffset.y;
			tmpPosition.z = noOverlapInnerOffset.z;
			vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			tmpPosition.x = center.x + arcProperties.EndSegmentUnitPosition.x * tmpArcOuterRadius.x + noOverlapOuterOffset.x;
			tmpPosition.y = center.y + arcProperties.EndSegmentUnitPosition.y * tmpArcOuterRadius.y + noOverlapOuterOffset.y;
			tmpPosition.z = noOverlapOuterOffset.z;
			vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			num5 = num4 + arcProperties.AdjustedResolution * 2;
			num6 = num3 + arcProperties.AdjustedResolution * 2;
			vh.AddTriangle(num6 - 1, num6 + 1, num5 + 3);
			vh.AddTriangle(num5 + 1, num6 - 1, num5 + 3);
			vh.AddTriangle(num6, num5 + 2, num6 + 2);
			vh.AddTriangle(num5 + 2, num5 + 4, num6 + 2);
			if (!(arcProperties.Length >= 1f))
			{
				num2 = edgeGradientData.SizeAdd + edgeGradientData.ShadowOffset;
				tmpPosition.x = center.x + unitPositionData.UnitPositions[0].x * tmpInnerRadius.x + arcProperties.StartTangent.x * num2;
				tmpPosition.y = center.y + unitPositionData.UnitPositions[0].y * tmpInnerRadius.y + arcProperties.StartTangent.y * num2;
				tmpPosition.z = arcProperties.StartTangent.z * num2;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				tmpPosition.x = center.x + unitPositionData.UnitPositions[0].x * tmpOuterRadius.x + arcProperties.StartTangent.x * num2;
				tmpPosition.y = center.y + unitPositionData.UnitPositions[0].y * tmpOuterRadius.y + arcProperties.StartTangent.y * num2;
				tmpPosition.z = 0f;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				tmpPosition.x = center.x + arcProperties.EndSegmentUnitPosition.x * tmpInnerRadius.x + arcProperties.EndTangent.x * num2;
				tmpPosition.y = center.y + arcProperties.EndSegmentUnitPosition.y * tmpInnerRadius.y + arcProperties.EndTangent.y * num2;
				tmpPosition.z = arcProperties.EndTangent.z * num2;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				tmpPosition.x = center.x + arcProperties.EndSegmentUnitPosition.x * tmpOuterRadius.x + arcProperties.EndTangent.x * num2;
				tmpPosition.y = center.y + arcProperties.EndSegmentUnitPosition.y * tmpOuterRadius.y + arcProperties.EndTangent.y * num2;
				tmpPosition.z = arcProperties.EndTangent.z * num2;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				if (arcProperties.Direction == ArcProperties.ArcDirection.Backward)
				{
					tmpOuterRadius.x += edgeGradientData.SizeAdd;
					tmpOuterRadius.y += edgeGradientData.SizeAdd;
					tmpInnerRadius.x -= edgeGradientData.SizeAdd;
					tmpInnerRadius.y -= edgeGradientData.SizeAdd;
				}
				else
				{
					tmpOuterRadius.x -= edgeGradientData.SizeAdd;
					tmpOuterRadius.y -= edgeGradientData.SizeAdd;
					tmpInnerRadius.x += edgeGradientData.SizeAdd;
					tmpInnerRadius.y += edgeGradientData.SizeAdd;
				}
				tmpPosition.x = center.x + unitPositionData.UnitPositions[0].x * tmpInnerRadius.x + arcProperties.StartTangent.x * num2;
				tmpPosition.y = center.y + unitPositionData.UnitPositions[0].y * tmpInnerRadius.y + arcProperties.StartTangent.y * num2;
				tmpPosition.z = arcProperties.StartTangent.z * num2;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				tmpPosition.x = center.x + unitPositionData.UnitPositions[0].x * tmpOuterRadius.x + arcProperties.StartTangent.x * num2;
				tmpPosition.y = center.y + unitPositionData.UnitPositions[0].y * tmpOuterRadius.y + arcProperties.StartTangent.y * num2;
				tmpPosition.z = arcProperties.StartTangent.z * num2;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				tmpPosition.x = center.x + arcProperties.EndSegmentUnitPosition.x * tmpInnerRadius.x + arcProperties.EndTangent.x * num2;
				tmpPosition.y = center.y + arcProperties.EndSegmentUnitPosition.y * tmpInnerRadius.y + arcProperties.EndTangent.y * num2;
				tmpPosition.z = arcProperties.EndTangent.z * num2;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				tmpPosition.x = center.x + arcProperties.EndSegmentUnitPosition.x * tmpOuterRadius.x + arcProperties.EndTangent.x * num2;
				tmpPosition.y = center.y + arcProperties.EndSegmentUnitPosition.y * tmpOuterRadius.y + arcProperties.EndTangent.y * num2;
				tmpPosition.z = arcProperties.EndTangent.z * num2;
				vh.AddVert(tmpPosition, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				int currentVertCount2 = vh.currentVertCount;
				vh.AddTriangle(currentVertCount2 - 1, currentVertCount2 - 2, num6 + 1);
				vh.AddTriangle(currentVertCount2 - 1, num6 + 1, num6 + 2);
				vh.AddTriangle(num5 + 3, num6 + 1, currentVertCount2 - 6);
				vh.AddTriangle(currentVertCount2 - 6, num6 + 1, currentVertCount2 - 2);
				vh.AddTriangle(num5 + 4, currentVertCount2 - 5, num6 + 2);
				vh.AddTriangle(currentVertCount2 - 5, currentVertCount2 - 1, num6 + 2);
				vh.AddTriangle(currentVertCount2 - 3, currentVertCount, currentVertCount2 - 4);
				vh.AddTriangle(currentVertCount2 - 3, currentVertCount + 1, currentVertCount);
				vh.AddTriangle(currentVertCount2 - 4, currentVertCount, currentVertCount2 - 8);
				vh.AddTriangle(num6 + 3, currentVertCount2 - 8, currentVertCount);
				vh.AddTriangle(currentVertCount2 - 7, num6 + 4, currentVertCount + 1);
				vh.AddTriangle(currentVertCount2 - 7, currentVertCount + 1, currentVertCount2 - 3);
			}
		}
	}
}
