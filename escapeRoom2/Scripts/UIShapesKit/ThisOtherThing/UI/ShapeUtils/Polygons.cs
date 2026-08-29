using System;
using ThisOtherThing.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisOtherThing.UI.ShapeUtils
{
	public class Polygons
	{
		[Serializable]
		public class PolygonProperties
		{
			public enum CenterTypes
			{
				Calculated = 0,
				Offset = 1,
				CustomPosition = 2,
				Cutout = 3
			}

			public CenterTypes CenterType;

			public Vector2 CenterOffset = Vector2.zero;

			public Vector2 CustomCenter = Vector2.zero;

			[HideInInspector]
			public Vector2 AdjustedCenter = Vector2.zero;

			public CutoutProperties CutoutProperties = new CutoutProperties();

			public void UpdateAdjusted(PointsList.PointListProperties pointListProperties)
			{
				AdjustedCenter.x = 0f;
				AdjustedCenter.y = 0f;
				if (CenterType == CenterTypes.CustomPosition)
				{
					AdjustedCenter.x = CustomCenter.x;
					AdjustedCenter.y = CustomCenter.y;
				}
				else
				{
					for (int i = 0; i < pointListProperties.Positions.Length; i++)
					{
						AdjustedCenter.x += pointListProperties.Positions[i].x;
						AdjustedCenter.y += pointListProperties.Positions[i].y;
					}
					AdjustedCenter.x /= pointListProperties.Positions.Length;
					AdjustedCenter.y /= pointListProperties.Positions.Length;
				}
				if (CenterType == CenterTypes.Cutout)
				{
					float num = CutoutProperties.RotationOffset;
					if (num < 0f)
					{
						num = GeoUtils.TwoPI + num;
					}
					float num2 = GeoUtils.TwoPI / (float)CutoutProperties.Resolution;
					num %= num2;
					num -= num2 * 0.5f;
					GeoUtils.SetUnitPositionData(ref CutoutProperties.UnitPositionData, CutoutProperties.Resolution, num);
				}
				if (CenterType == CenterTypes.Offset || CenterType == CenterTypes.Cutout)
				{
					AdjustedCenter.x += CenterOffset.x;
					AdjustedCenter.y += CenterOffset.y;
				}
			}
		}

		[Serializable]
		public class CutoutProperties
		{
			[Minimum(3)]
			public int Resolution = 4;

			[Minimum(0f)]
			public float Radius = 1f;

			[Range(-3.141592f, 3.141592f)]
			public float RotationOffset;

			public GeoUtils.UnitPositionData UnitPositionData;
		}

		private static Vector3 tmpPos = Vector3.zero;

		public static void AddPolygon(ref VertexHelper vh, PolygonProperties polygonProperties, PointsList.PointListProperties pointListProperties, Vector2 positionOffset, Color32 color, Vector2 uv, ref PointsList.PointsData pointsData, GeoUtils.EdgeGradientData edgeGradientData)
		{
			pointListProperties.SetPoints();
			PointsList.SetLineData(pointListProperties, ref pointsData);
			int currentVertCount = vh.currentVertCount;
			int num = vh.currentVertCount + polygonProperties.CutoutProperties.Resolution - 1;
			bool flag = polygonProperties.CenterType == PolygonProperties.CenterTypes.Cutout;
			if (flag)
			{
				float num2 = polygonProperties.CutoutProperties.Radius - edgeGradientData.ShadowOffset;
				num2 += Mathf.LerpUnclamped(pointsData.PositionNormals[0].magnitude * edgeGradientData.ShadowOffset * 3f, 0f, edgeGradientData.InnerScale);
				for (int i = 0; i < polygonProperties.CutoutProperties.Resolution; i++)
				{
					tmpPos.x = polygonProperties.AdjustedCenter.x + positionOffset.x + polygonProperties.CutoutProperties.UnitPositionData.UnitPositions[i].x * num2;
					tmpPos.y = polygonProperties.AdjustedCenter.y + positionOffset.y + polygonProperties.CutoutProperties.UnitPositionData.UnitPositions[i].y * num2;
					tmpPos.z = 0f;
					vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				}
			}
			else
			{
				tmpPos.x = polygonProperties.AdjustedCenter.x + positionOffset.x;
				tmpPos.y = polygonProperties.AdjustedCenter.y + positionOffset.y;
				tmpPos.z = 0f;
				vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			}
			tmpPos.x = positionOffset.x + Mathf.LerpUnclamped(polygonProperties.AdjustedCenter.x, pointsData.Positions[0].x + pointsData.PositionNormals[0].x * edgeGradientData.ShadowOffset, edgeGradientData.InnerScale);
			tmpPos.y = positionOffset.y + Mathf.LerpUnclamped(polygonProperties.AdjustedCenter.y, pointsData.Positions[0].y + pointsData.PositionNormals[0].y * edgeGradientData.ShadowOffset, edgeGradientData.InnerScale);
			tmpPos.z = 0f;
			vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			for (int j = 1; j < pointsData.NumPositions; j++)
			{
				tmpPos.x = positionOffset.x + Mathf.LerpUnclamped(polygonProperties.AdjustedCenter.x, pointsData.Positions[j].x + pointsData.PositionNormals[j].x * edgeGradientData.ShadowOffset, edgeGradientData.InnerScale);
				tmpPos.y = positionOffset.y + Mathf.LerpUnclamped(polygonProperties.AdjustedCenter.y, pointsData.Positions[j].y + pointsData.PositionNormals[j].y * edgeGradientData.ShadowOffset, edgeGradientData.InnerScale);
				vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				if (!flag)
				{
					vh.AddTriangle(currentVertCount, currentVertCount + j, currentVertCount + j + 1);
				}
			}
			if (flag)
			{
				for (int k = 1; k < pointsData.NumPositions; k++)
				{
					vh.AddTriangle(currentVertCount + GeoUtils.SimpleMap(k, pointsData.NumPositions, polygonProperties.CutoutProperties.Resolution), num + k, num + k + 1);
				}
				for (int l = 1; l < polygonProperties.CutoutProperties.Resolution; l++)
				{
					vh.AddTriangle(currentVertCount + l, currentVertCount + l - 1, num + Mathf.CeilToInt(GeoUtils.SimpleMap((float)l, (float)polygonProperties.CutoutProperties.Resolution, (float)pointsData.NumPositions)));
				}
			}
			if (flag)
			{
				vh.AddTriangle(currentVertCount, num + pointsData.NumPositions, num + 1);
				vh.AddTriangle(currentVertCount, num, num + pointsData.NumPositions);
			}
			else
			{
				vh.AddTriangle(currentVertCount, currentVertCount + pointsData.NumPositions, currentVertCount + 1);
			}
			if (!edgeGradientData.IsActive)
			{
				return;
			}
			color.a = 0;
			int num3 = currentVertCount + pointsData.NumPositions;
			if (flag)
			{
				num3 = num + pointsData.NumPositions;
			}
			else
			{
				num = currentVertCount;
			}
			float num4 = edgeGradientData.SizeAdd + edgeGradientData.ShadowOffset;
			vh.AddVert(positionOffset + pointsData.Positions[0] + pointsData.PositionNormals[0] * num4, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			for (int m = 1; m < pointsData.NumPositions; m++)
			{
				vh.AddVert(positionOffset + pointsData.Positions[m] + pointsData.PositionNormals[m] * num4, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				vh.AddTriangle(num + m + 1, num3 + m, num3 + m + 1);
				vh.AddTriangle(num + m + 1, num3 + m + 1, num + m + 2);
			}
			vh.AddTriangle(num + 1, num3, num3 + 1);
			vh.AddTriangle(num + 2, num + 1, num3 + 1);
			if (flag)
			{
				float num5 = polygonProperties.CutoutProperties.Radius - num4;
				for (int n = 0; n < polygonProperties.CutoutProperties.Resolution; n++)
				{
					tmpPos.x = polygonProperties.AdjustedCenter.x + positionOffset.x + polygonProperties.CutoutProperties.UnitPositionData.UnitPositions[n].x * num5;
					tmpPos.y = polygonProperties.AdjustedCenter.y + positionOffset.y + polygonProperties.CutoutProperties.UnitPositionData.UnitPositions[n].y * num5;
					tmpPos.z = 0f;
					vh.AddVert(tmpPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				}
				for (int num6 = 1; num6 < polygonProperties.CutoutProperties.Resolution; num6++)
				{
					vh.AddTriangle(currentVertCount + num6 - 1, currentVertCount + num6, num3 + pointsData.NumPositions + num6);
					vh.AddTriangle(currentVertCount + num6, num3 + pointsData.NumPositions + num6 + 1, num3 + pointsData.NumPositions + num6);
				}
				vh.AddTriangle(num, currentVertCount, num3 + pointsData.NumPositions + polygonProperties.CutoutProperties.Resolution);
				vh.AddTriangle(currentVertCount, num3 + pointsData.NumPositions + 1, num3 + pointsData.NumPositions + polygonProperties.CutoutProperties.Resolution);
			}
		}
	}
}
