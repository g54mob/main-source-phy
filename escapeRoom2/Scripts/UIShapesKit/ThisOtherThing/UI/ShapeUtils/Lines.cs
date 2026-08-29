using System;
using UnityEngine;
using UnityEngine.UI;

namespace ThisOtherThing.UI.ShapeUtils
{
	public class Lines
	{
		[Serializable]
		public class LineProperties
		{
			public enum LineCapTypes
			{
				Close = 0,
				Projected = 1,
				Round = 2
			}

			public LineCapTypes LineCap;

			public bool Closed;

			public GeoUtils.RoundingProperties RoundedCapResolution = new GeoUtils.RoundingProperties();

			public void OnCheck()
			{
				RoundedCapResolution.OnCheck(1);
			}
		}

		private static Vector3 tmpPos = Vector3.zero;

		private static Vector2 tmpPos2 = Vector2.zero;

		public static void AddLine(ref VertexHelper vh, LineProperties lineProperties, PointsList.PointListProperties pointListProperties, Vector2 positionOffset, GeoUtils.OutlineProperties outlineProperties, Color32 color, Vector2 uv, ref PointsList.PointsData pointsData, GeoUtils.EdgeGradientData edgeGradientData)
		{
			pointListProperties.SetPoints();
			pointsData.IsClosed = lineProperties.Closed && pointListProperties.Positions.Length > 2;
			pointsData.GenerateRoundedCaps = lineProperties.LineCap == LineProperties.LineCapTypes.Round;
			pointsData.LineWeight = outlineProperties.LineWeight;
			if (pointsData.GenerateRoundedCaps)
			{
				lineProperties.RoundedCapResolution.UpdateAdjusted(outlineProperties.HalfLineWeight, 0f, 2f);
				pointsData.RoundedCapResolution = lineProperties.RoundedCapResolution.AdjustedResolution;
			}
			if (!PointsList.SetLineData(pointListProperties, ref pointsData))
			{
				return;
			}
			float num = 0f;
			float num2 = 1f;
			if (!lineProperties.Closed && lineProperties.LineCap != LineProperties.LineCapTypes.Close)
			{
				num = outlineProperties.LineWeight / pointsData.TotalLength * 0.5f;
				num2 = 1f - num * 2f;
			}
			float num3 = outlineProperties.GetCenterDistace() - (outlineProperties.HalfLineWeight + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale;
			float num4 = outlineProperties.GetCenterDistace() + (outlineProperties.HalfLineWeight + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale;
			float num5 = 0f;
			if (!lineProperties.Closed && lineProperties.LineCap == LineProperties.LineCapTypes.Close)
			{
				num5 = edgeGradientData.ShadowOffset * (edgeGradientData.InnerScale * 2f - 1f);
			}
			int currentVertCount = vh.currentVertCount;
			int num6 = currentVertCount - 1;
			uv.x = num + pointsData.NormalizedPositionDistances[0] * num2;
			uv.y = 0f;
			tmpPos.x = positionOffset.x + pointsData.Positions[0].x + pointsData.PositionNormals[0].x * num3 + pointsData.StartCapOffset.x * num5;
			tmpPos.y = positionOffset.y + pointsData.Positions[0].y + pointsData.PositionNormals[0].y * num3 + pointsData.StartCapOffset.y * num5;
			vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			uv.y = 1f;
			tmpPos.x = positionOffset.x + pointsData.Positions[0].x + pointsData.PositionNormals[0].x * num4 + pointsData.StartCapOffset.x * num5;
			tmpPos.y = positionOffset.y + pointsData.Positions[0].y + pointsData.PositionNormals[0].y * num4 + pointsData.StartCapOffset.y * num5;
			vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			int num7;
			for (int i = 1; i < pointsData.NumPositions - 1; i++)
			{
				uv.x = num + pointsData.NormalizedPositionDistances[i] * num2;
				uv.y = 0f;
				tmpPos.x = positionOffset.x + pointsData.Positions[i].x + pointsData.PositionNormals[i].x * num3;
				tmpPos.y = positionOffset.y + pointsData.Positions[i].y + pointsData.PositionNormals[i].y * num3;
				vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				uv.y = 1f;
				tmpPos.x = positionOffset.x + pointsData.Positions[i].x + pointsData.PositionNormals[i].x * num4;
				tmpPos.y = positionOffset.y + pointsData.Positions[i].y + pointsData.PositionNormals[i].y * num4;
				vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				num7 = num6 + i * 2;
				vh.AddTriangle(num7 - 1, num7, num7 + 1);
				vh.AddTriangle(num7, num7 + 2, num7 + 1);
			}
			int num8 = pointsData.NumPositions - 1;
			uv.x = num + pointsData.NormalizedPositionDistances[num8] * num2;
			uv.y = 0f;
			tmpPos.x = positionOffset.x + pointsData.Positions[num8].x + pointsData.PositionNormals[num8].x * num3 + pointsData.EndCapOffset.x * num5;
			tmpPos.y = positionOffset.y + pointsData.Positions[num8].y + pointsData.PositionNormals[num8].y * num3 + pointsData.EndCapOffset.y * num5;
			vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			uv.y = 1f;
			tmpPos.x = positionOffset.x + pointsData.Positions[num8].x + pointsData.PositionNormals[num8].x * num4 + pointsData.EndCapOffset.x * num5;
			tmpPos.y = positionOffset.y + pointsData.Positions[num8].y + pointsData.PositionNormals[num8].y * num4 + pointsData.EndCapOffset.y * num5;
			vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			num7 = num6 + num8 * 2;
			vh.AddTriangle(num7 - 1, num7, num7 + 1);
			vh.AddTriangle(num7, num7 + 2, num7 + 1);
			if (lineProperties.Closed)
			{
				uv.x = 1f;
				uv.y = 0f;
				tmpPos.x = positionOffset.x + pointsData.Positions[0].x + pointsData.PositionNormals[0].x * num3 + pointsData.StartCapOffset.x * num5;
				tmpPos.y = positionOffset.y + pointsData.Positions[0].y + pointsData.PositionNormals[0].y * num3 + pointsData.StartCapOffset.y * num5;
				vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				uv.y = 1f;
				tmpPos.x = positionOffset.x + pointsData.Positions[0].x + pointsData.PositionNormals[0].x * num4 + pointsData.StartCapOffset.x * num5;
				tmpPos.y = positionOffset.y + pointsData.Positions[0].y + pointsData.PositionNormals[0].y * num4 + pointsData.StartCapOffset.y * num5;
				vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				num7 = num6 + num8 * 2 + 2;
				vh.AddTriangle(num7 - 1, num7, num7 + 1);
				vh.AddTriangle(num7, num7 + 2, num7 + 1);
			}
			if (edgeGradientData.IsActive)
			{
				byte a = color.a;
				num3 = outlineProperties.GetCenterDistace() - (outlineProperties.HalfLineWeight + edgeGradientData.ShadowOffset);
				num4 = outlineProperties.GetCenterDistace() + (outlineProperties.HalfLineWeight + edgeGradientData.ShadowOffset);
				num3 -= edgeGradientData.SizeAdd;
				num4 += edgeGradientData.SizeAdd;
				color.a = 0;
				int num9 = currentVertCount + pointsData.NumPositions * 2;
				if (lineProperties.Closed)
				{
					num9 += 2;
				}
				uv.x = num + pointsData.NormalizedPositionDistances[0] * num2;
				uv.y = 0f;
				tmpPos.x = positionOffset.x + pointsData.Positions[0].x + pointsData.PositionNormals[0].x * num3;
				tmpPos.y = positionOffset.y + pointsData.Positions[0].y + pointsData.PositionNormals[0].y * num3;
				vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				uv.y = 1f;
				tmpPos.x = positionOffset.x + pointsData.Positions[0].x + pointsData.PositionNormals[0].x * num4;
				tmpPos.y = positionOffset.y + pointsData.Positions[0].y + pointsData.PositionNormals[0].y * num4;
				vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				for (int j = 1; j < pointsData.NumPositions; j++)
				{
					uv.x = num + pointsData.NormalizedPositionDistances[j] * num2;
					uv.y = 0f;
					tmpPos.x = positionOffset.x + pointsData.Positions[j].x + pointsData.PositionNormals[j].x * num3;
					tmpPos.y = positionOffset.y + pointsData.Positions[j].y + pointsData.PositionNormals[j].y * num3;
					vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
					uv.y = 1f;
					tmpPos.x = positionOffset.x + pointsData.Positions[j].x + pointsData.PositionNormals[j].x * num4;
					tmpPos.y = positionOffset.y + pointsData.Positions[j].y + pointsData.PositionNormals[j].y * num4;
					vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
					vh.AddTriangle(num6 + j * 2 - 1, num6 + j * 2 + 1, num9 + j * 2);
					vh.AddTriangle(num6 + j * 2 - 1, num9 + j * 2, num9 + j * 2 - 2);
					vh.AddTriangle(num6 + j * 2, num9 + j * 2 - 1, num6 + j * 2 + 2);
					vh.AddTriangle(num6 + j * 2 + 2, num9 + j * 2 - 1, num9 + j * 2 + 1);
				}
				if (lineProperties.Closed)
				{
					int numPositions = pointsData.NumPositions;
					uv.x = 1f;
					uv.y = 0f;
					tmpPos.x = positionOffset.x + pointsData.Positions[0].x + pointsData.PositionNormals[0].x * num3;
					tmpPos.y = positionOffset.y + pointsData.Positions[0].y + pointsData.PositionNormals[0].y * num3;
					vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
					uv.y = 1f;
					tmpPos.x = positionOffset.x + pointsData.Positions[0].x + pointsData.PositionNormals[0].x * num4;
					tmpPos.y = positionOffset.y + pointsData.Positions[0].y + pointsData.PositionNormals[0].y * num4;
					vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
					vh.AddTriangle(num6 + numPositions * 2 - 1, num6 + numPositions * 2 + 1, num9 + numPositions * 2);
					vh.AddTriangle(num6 + numPositions * 2 - 1, num9 + numPositions * 2, num9 + numPositions * 2 - 2);
					vh.AddTriangle(num6 + numPositions * 2, num9 + numPositions * 2 - 1, num6 + numPositions * 2 + 2);
					vh.AddTriangle(num6 + numPositions * 2 + 2, num9 + numPositions * 2 - 1, num9 + numPositions * 2 + 1);
				}
				color.a = a;
			}
			if (!lineProperties.Closed)
			{
				AddStartCap(ref vh, lineProperties, positionOffset, outlineProperties, color, uv, num, num2, pointsData, edgeGradientData);
				AddEndCap(ref vh, lineProperties, positionOffset, outlineProperties, color, uv, num, num2, pointsData, edgeGradientData);
			}
		}

		public static void AddStartCap(ref VertexHelper vh, LineProperties lineProperties, Vector2 positionOffset, GeoUtils.OutlineProperties outlineProperties, Color32 color, Vector2 uv, float uvXMin, float uvXLength, PointsList.PointsData pointsData, GeoUtils.EdgeGradientData edgeGradientData)
		{
			int currentVertCount = vh.currentVertCount;
			int num = currentVertCount - pointsData.NumPositions * 2;
			if (edgeGradientData.IsActive)
			{
				num -= pointsData.NumPositions * 2;
			}
			tmpPos2.x = positionOffset.x + pointsData.Positions[0].x;
			tmpPos2.y = positionOffset.y + pointsData.Positions[0].y;
			switch (lineProperties.LineCap)
			{
			case LineProperties.LineCapTypes.Close:
				AddCloseCap(ref vh, isStart: true, num, tmpPos2, pointsData.PositionNormals[0], pointsData.StartCapOffset, 0, lineProperties, outlineProperties, color, uv, pointsData, edgeGradientData, currentVertCount);
				break;
			case LineProperties.LineCapTypes.Projected:
				AddProjectedCap(ref vh, isStart: true, num, tmpPos2, pointsData.PositionNormals[0], pointsData.StartCapOffset, 0, lineProperties, outlineProperties, color, uv, pointsData, edgeGradientData, currentVertCount);
				break;
			case LineProperties.LineCapTypes.Round:
				AddRoundedCap(ref vh, isStart: true, num, tmpPos2, pointsData.PositionNormals[0], pointsData.StartCapOffset, 0, lineProperties, outlineProperties, color, uv, pointsData, edgeGradientData, pointsData.StartCapOffsets, pointsData.StartCapUVs, uvXMin, uvXLength, currentVertCount);
				break;
			}
		}

		public static void AddEndCap(ref VertexHelper vh, LineProperties lineProperties, Vector2 positionOffset, GeoUtils.OutlineProperties outlineProperties, Color32 color, Vector2 uv, float uvXMin, float uvXLength, PointsList.PointsData pointsData, GeoUtils.EdgeGradientData edgeGradientData)
		{
			int currentVertCount = vh.currentVertCount;
			int num = currentVertCount;
			if (edgeGradientData.IsActive)
			{
				num -= pointsData.NumPositions * 2;
			}
			int num2 = pointsData.NumPositions - 1;
			tmpPos2.x = positionOffset.x + pointsData.Positions[num2].x;
			tmpPos2.y = positionOffset.y + pointsData.Positions[num2].y;
			switch (lineProperties.LineCap)
			{
			case LineProperties.LineCapTypes.Close:
				num -= 4;
				AddCloseCap(ref vh, isStart: false, num, tmpPos2, pointsData.PositionNormals[num2], pointsData.EndCapOffset, 1, lineProperties, outlineProperties, color, uv, pointsData, edgeGradientData, currentVertCount);
				break;
			case LineProperties.LineCapTypes.Projected:
				num -= 6;
				AddProjectedCap(ref vh, isStart: false, num, tmpPos2, pointsData.PositionNormals[num2], pointsData.EndCapOffset, 1, lineProperties, outlineProperties, color, uv, pointsData, edgeGradientData, currentVertCount);
				break;
			case LineProperties.LineCapTypes.Round:
				num -= pointsData.RoundedCapResolution + 2;
				if (edgeGradientData.IsActive)
				{
					num -= pointsData.RoundedCapResolution;
				}
				AddRoundedCap(ref vh, isStart: false, num, tmpPos2, pointsData.PositionNormals[num2], pointsData.EndCapOffset, 1, lineProperties, outlineProperties, color, uv, pointsData, edgeGradientData, pointsData.EndCapOffsets, pointsData.EndCapUVs, uvXMin, uvXLength, currentVertCount);
				break;
			}
		}

		public static void AddCloseCap(ref VertexHelper vh, bool isStart, int firstVertIndex, Vector2 position, Vector2 normal, Vector2 capOffset, int invertIndices, LineProperties lineProperties, GeoUtils.OutlineProperties outlineProperties, Color32 color, Vector2 uv, PointsList.PointsData pointsData, GeoUtils.EdgeGradientData edgeGradientData, int currentVertCount)
		{
			if (edgeGradientData.IsActive)
			{
				float num = outlineProperties.GetCenterDistace() - (outlineProperties.HalfLineWeight + edgeGradientData.ShadowOffset) - edgeGradientData.SizeAdd;
				float num2 = outlineProperties.GetCenterDistace() + (outlineProperties.HalfLineWeight + edgeGradientData.ShadowOffset) + edgeGradientData.SizeAdd;
				float num3 = edgeGradientData.SizeAdd + edgeGradientData.ShadowOffset;
				color.a = 0;
				uv.y = 0f;
				tmpPos.x = position.x + normal.x * num + capOffset.x * num3;
				tmpPos.y = position.y + normal.y * num + capOffset.y * num3;
				vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				uv.y = 1f;
				tmpPos.x = position.x + normal.x * num2 + capOffset.x * num3;
				tmpPos.y = position.y + normal.y * num2 + capOffset.y * num3;
				vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				vh.AddTriangle(firstVertIndex, currentVertCount + invertIndices, currentVertCount + 1 - invertIndices);
				vh.AddTriangle(firstVertIndex + invertIndices, currentVertCount + 1, firstVertIndex + 1 - invertIndices);
				int num4 = firstVertIndex + pointsData.NumPositions * 2;
				if (invertIndices != 0)
				{
					vh.AddTriangle(firstVertIndex, currentVertCount, num4);
					vh.AddTriangle(firstVertIndex + 1, num4 + 1, currentVertCount + 1);
				}
				else
				{
					vh.AddTriangle(firstVertIndex, num4, currentVertCount);
					vh.AddTriangle(firstVertIndex + 1, currentVertCount + 1, num4 + 1);
				}
			}
		}

		public static void AddProjectedCap(ref VertexHelper vh, bool isStart, int firstVertIndex, Vector2 position, Vector2 normal, Vector2 capOffset, int invertIndices, LineProperties lineProperties, GeoUtils.OutlineProperties outlineProperties, Color32 color, Vector2 uv, PointsList.PointsData pointsData, GeoUtils.EdgeGradientData edgeGradientData, int currentVertCount)
		{
			int num = currentVertCount;
			if (isStart)
			{
				uv.x = 0f;
			}
			else
			{
				uv.x = 1f;
			}
			float num2 = outlineProperties.GetCenterDistace() - (outlineProperties.HalfLineWeight + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale;
			float num3 = outlineProperties.GetCenterDistace() + (outlineProperties.HalfLineWeight + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale;
			float num4 = edgeGradientData.ShadowOffset + outlineProperties.LineWeight * 0.5f;
			num4 *= edgeGradientData.InnerScale;
			tmpPos.x = position.x + normal.x * num2 + capOffset.x * num4;
			tmpPos.y = position.y + normal.y * num2 + capOffset.y * num4;
			uv.y = 0f;
			vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			tmpPos.x = position.x + normal.x * num3 + capOffset.x * num4;
			tmpPos.y = position.y + normal.y * num3 + capOffset.y * num4;
			uv.y = 1f;
			vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			vh.AddTriangle(firstVertIndex, num + invertIndices, num + 1 - invertIndices);
			vh.AddTriangle(firstVertIndex + invertIndices, num + 1, firstVertIndex + 1 - invertIndices);
			if (edgeGradientData.IsActive)
			{
				num2 = outlineProperties.GetCenterDistace() - (outlineProperties.HalfLineWeight + edgeGradientData.ShadowOffset) - edgeGradientData.SizeAdd;
				num3 = outlineProperties.GetCenterDistace() + (outlineProperties.HalfLineWeight + edgeGradientData.ShadowOffset) + edgeGradientData.SizeAdd;
				num4 = outlineProperties.HalfLineWeight + edgeGradientData.SizeAdd + edgeGradientData.ShadowOffset;
				color.a = 0;
				tmpPos.x = position.x + normal.x * num2 + capOffset.x * num4;
				tmpPos.y = position.y + normal.y * num2 + capOffset.y * num4;
				uv.y = 0f;
				vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				tmpPos.x = position.x + normal.x * num3 + capOffset.x * num4;
				tmpPos.y = position.y + normal.y * num3 + capOffset.y * num4;
				uv.y = 1f;
				vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				int num5 = firstVertIndex + pointsData.NumPositions * 2;
				num += 2;
				if (invertIndices != 0)
				{
					vh.AddTriangle(firstVertIndex, num, num5);
					vh.AddTriangle(firstVertIndex + 1, num5 + 1, num + 1);
					vh.AddTriangle(num - 2, num - 1, num);
					vh.AddTriangle(num + 1, num, num - 1);
					vh.AddTriangle(firstVertIndex, num - 2, num);
					vh.AddTriangle(firstVertIndex + 1, num + 1, num - 1);
				}
				else
				{
					vh.AddTriangle(firstVertIndex, num5, num);
					vh.AddTriangle(firstVertIndex + 1, num + 1, num5 + 1);
					vh.AddTriangle(num - 2, num, num - 1);
					vh.AddTriangle(num + 1, num - 1, num);
					vh.AddTriangle(firstVertIndex, num, num - 2);
					vh.AddTriangle(firstVertIndex + 1, num - 1, num + 1);
				}
			}
		}

		public static void AddRoundedCap(ref VertexHelper vh, bool isStart, int firstVertIndex, Vector2 position, Vector2 normal, Vector2 capOffset, int invertIndices, LineProperties lineProperties, GeoUtils.OutlineProperties outlineProperties, Color32 color, Vector2 uv, PointsList.PointsData pointsData, GeoUtils.EdgeGradientData edgeGradientData, Vector2[] capOffsets, Vector2[] uvOffsets, float uvXMin, float uvXLength, int currentVertCount)
		{
			float centerDistace = outlineProperties.GetCenterDistace();
			float num = (edgeGradientData.ShadowOffset + outlineProperties.HalfLineWeight) * edgeGradientData.InnerScale;
			if (isStart)
			{
				uv.x = uvXMin;
			}
			else
			{
				uv.x = uvXMin + uvXLength;
			}
			for (int i = 0; i < capOffsets.Length; i++)
			{
				tmpPos.x = position.x + normal.x * centerDistace + capOffsets[i].x * num;
				tmpPos.y = position.y + normal.y * centerDistace + capOffsets[i].y * num;
				if (isStart)
				{
					uv.x = Mathf.LerpUnclamped(uvXMin, 0f, uvOffsets[i].x);
				}
				else
				{
					uv.x = Mathf.LerpUnclamped(uvXMin + uvXLength, 1f, uvOffsets[i].x);
				}
				uv.y = uvOffsets[i].y;
				vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				if (i > 0)
				{
					vh.AddTriangle(firstVertIndex, currentVertCount + i - 1, currentVertCount + i);
				}
			}
			if (isStart)
			{
				vh.AddTriangle(currentVertCount + capOffsets.Length - 1, firstVertIndex + 1, firstVertIndex);
			}
			else
			{
				vh.AddTriangle(currentVertCount, firstVertIndex, firstVertIndex + 1);
			}
			if (!edgeGradientData.IsActive)
			{
				return;
			}
			color.a = 0;
			centerDistace = outlineProperties.GetCenterDistace();
			num = outlineProperties.HalfLineWeight + edgeGradientData.SizeAdd + edgeGradientData.ShadowOffset;
			int num2 = firstVertIndex + pointsData.NumPositions * 2;
			for (int j = 0; j < capOffsets.Length; j++)
			{
				tmpPos.x = position.x + normal.x * centerDistace + capOffsets[j].x * num;
				tmpPos.y = position.y + normal.y * centerDistace + capOffsets[j].y * num;
				if (isStart)
				{
					uv.x = Mathf.LerpUnclamped(uvXMin, 0f, uvOffsets[j].x);
				}
				else
				{
					uv.x = Mathf.LerpUnclamped(uvXMin + uvXLength, 1f, uvOffsets[j].x);
				}
				uv.y = uvOffsets[j].y;
				vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				if (j > 0)
				{
					vh.AddTriangle(currentVertCount + j - 1, currentVertCount + capOffsets.Length + j - 1, currentVertCount + j);
					vh.AddTriangle(currentVertCount + capOffsets.Length + j, currentVertCount + j, currentVertCount + capOffsets.Length + j - 1);
				}
			}
			if (!isStart)
			{
				vh.AddTriangle(currentVertCount, firstVertIndex + 1, num2 + 1);
				vh.AddTriangle(num2 + 1, currentVertCount + capOffsets.Length, currentVertCount);
				vh.AddTriangle(currentVertCount + capOffsets.Length * 2 - 1, num2, firstVertIndex);
				vh.AddTriangle(currentVertCount + capOffsets.Length - 1, currentVertCount + capOffsets.Length * 2 - 1, firstVertIndex);
			}
			else
			{
				vh.AddTriangle(firstVertIndex + 1, currentVertCount + capOffsets.Length - 1, currentVertCount + capOffsets.Length * 2 - 1);
				vh.AddTriangle(num2 + 1, firstVertIndex + 1, currentVertCount + capOffsets.Length * 2 - 1);
				vh.AddTriangle(num2, currentVertCount, firstVertIndex);
				vh.AddTriangle(currentVertCount + capOffsets.Length, currentVertCount, num2);
			}
		}
	}
}
