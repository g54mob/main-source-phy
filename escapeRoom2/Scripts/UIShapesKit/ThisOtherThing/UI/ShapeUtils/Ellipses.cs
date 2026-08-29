using System;
using UnityEngine;
using UnityEngine.UI;

namespace ThisOtherThing.UI.ShapeUtils
{
	public class Ellipses
	{
		[Serializable]
		public class EllipseProperties
		{
			public enum EllipseFitting
			{
				Ellipse = 0,
				UniformInner = 1,
				UniformOuter = 2
			}

			public enum ResolutionType
			{
				Calculated = 0,
				Fixed = 1
			}

			public EllipseFitting Fitting = EllipseFitting.UniformInner;

			public float BaseAngle;

			public ResolutionType Resolution;

			public int FixedResolution = 50;

			public float ResolutionMaxDistance = 4f;

			public int AdjustedResolution { get; private set; }

			public void OnCheck()
			{
				FixedResolution = Mathf.Max(FixedResolution, 3);
				ResolutionMaxDistance = Mathf.Max(ResolutionMaxDistance, 0.1f);
			}

			public void UpdateAdjusted(Vector2 radius, float offset)
			{
				radius.x += offset;
				radius.y += offset;
				switch (Resolution)
				{
				case ResolutionType.Calculated:
				{
					float num = ((radius.x != radius.y) ? (MathF.PI * (3f * (radius.x + radius.y) - Mathf.Sqrt((3f * radius.x + radius.y) * (radius.x + 3f * radius.y)))) : (GeoUtils.TwoPI * radius.x));
					AdjustedResolution = Mathf.CeilToInt(num / ResolutionMaxDistance);
					break;
				}
				case ResolutionType.Fixed:
					AdjustedResolution = FixedResolution;
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		private static Vector3 tmpVertPos = Vector3.zero;

		private static Vector2 tmpUVPos = Vector2.zero;

		private static Vector3 tmpInnerRadius = Vector3.one;

		private static Vector3 tmpOuterRadius = Vector3.one;

		public static void SetRadius(ref Vector2 radius, float width, float height, EllipseProperties properties)
		{
			width *= 0.5f;
			height *= 0.5f;
			switch (properties.Fitting)
			{
			case EllipseProperties.EllipseFitting.UniformInner:
				radius.x = Mathf.Min(width, height);
				radius.y = radius.x;
				break;
			case EllipseProperties.EllipseFitting.UniformOuter:
				radius.x = Mathf.Max(width, height);
				radius.y = radius.x;
				break;
			case EllipseProperties.EllipseFitting.Ellipse:
				radius.x = width;
				radius.y = height;
				break;
			}
		}

		public static void AddCircle(ref VertexHelper vh, Vector2 center, Vector2 radius, EllipseProperties ellipseProperties, Color32 color, Vector2 uv, ref GeoUtils.UnitPositionData unitPositionData, GeoUtils.EdgeGradientData edgeGradientData)
		{
			GeoUtils.SetUnitPositionData(ref unitPositionData, ellipseProperties.AdjustedResolution, ellipseProperties.BaseAngle);
			int currentVertCount = vh.currentVertCount;
			tmpUVPos.x = 0.5f;
			tmpUVPos.y = 0.5f;
			vh.AddVert(center, color, tmpUVPos, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			tmpVertPos.x = center.x + unitPositionData.UnitPositions[0].x * (radius.x + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale;
			tmpVertPos.y = center.y + unitPositionData.UnitPositions[0].y * (radius.y + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale;
			tmpVertPos.z = 0f;
			tmpUVPos.x = (unitPositionData.UnitPositions[0].x * edgeGradientData.InnerScale + 1f) * 0.5f;
			tmpUVPos.y = (unitPositionData.UnitPositions[0].y * edgeGradientData.InnerScale + 1f) * 0.5f;
			vh.AddVert(tmpVertPos, color, tmpUVPos, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			for (int i = 1; i < ellipseProperties.AdjustedResolution; i++)
			{
				tmpVertPos.x = center.x + unitPositionData.UnitPositions[i].x * (radius.x + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale;
				tmpVertPos.y = center.y + unitPositionData.UnitPositions[i].y * (radius.y + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale;
				tmpVertPos.z = 0f;
				tmpUVPos.x = (unitPositionData.UnitPositions[i].x * edgeGradientData.InnerScale + 1f) * 0.5f;
				tmpUVPos.y = (unitPositionData.UnitPositions[i].y * edgeGradientData.InnerScale + 1f) * 0.5f;
				vh.AddVert(tmpVertPos, color, tmpUVPos, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				vh.AddTriangle(currentVertCount, currentVertCount + i, currentVertCount + i + 1);
			}
			vh.AddTriangle(currentVertCount, currentVertCount + ellipseProperties.AdjustedResolution, currentVertCount + 1);
			if (edgeGradientData.IsActive)
			{
				radius.x += edgeGradientData.ShadowOffset + edgeGradientData.SizeAdd;
				radius.y += edgeGradientData.ShadowOffset + edgeGradientData.SizeAdd;
				int num = currentVertCount + ellipseProperties.AdjustedResolution;
				color.a = 0;
				tmpVertPos.x = center.x + unitPositionData.UnitPositions[0].x * radius.x;
				tmpVertPos.y = center.y + unitPositionData.UnitPositions[0].y * radius.y;
				tmpVertPos.z = 0f;
				tmpUVPos.x = (unitPositionData.UnitPositions[0].x + 1f) * 0.5f;
				tmpUVPos.y = (unitPositionData.UnitPositions[0].y + 1f) * 0.5f;
				vh.AddVert(tmpVertPos, color, tmpUVPos, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				for (int j = 1; j < ellipseProperties.AdjustedResolution; j++)
				{
					tmpVertPos.x = center.x + unitPositionData.UnitPositions[j].x * radius.x;
					tmpVertPos.y = center.y + unitPositionData.UnitPositions[j].y * radius.y;
					tmpVertPos.z = 0f;
					tmpUVPos.x = (unitPositionData.UnitPositions[j].x + 1f) * 0.5f;
					tmpUVPos.y = (unitPositionData.UnitPositions[j].y + 1f) * 0.5f;
					vh.AddVert(tmpVertPos, color, tmpUVPos, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
					vh.AddTriangle(currentVertCount + j + 1, num + j, num + j + 1);
					vh.AddTriangle(currentVertCount + j + 1, num + j + 1, currentVertCount + j + 2);
				}
				vh.AddTriangle(currentVertCount + 1, num, num + 1);
				vh.AddTriangle(currentVertCount + 2, currentVertCount + 1, num + 1);
			}
		}

		public static void AddRing(ref VertexHelper vh, Vector2 center, Vector2 radius, GeoUtils.OutlineProperties outlineProperties, EllipseProperties ellipseProperties, Color32 color, Vector2 uv, ref GeoUtils.UnitPositionData unitPositionData, GeoUtils.EdgeGradientData edgeGradientData)
		{
			GeoUtils.SetUnitPositionData(ref unitPositionData, ellipseProperties.AdjustedResolution, ellipseProperties.BaseAngle);
			float num = (outlineProperties.HalfLineWeight + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale;
			tmpInnerRadius.x = radius.x + outlineProperties.GetCenterDistace() - num;
			tmpInnerRadius.y = radius.y + outlineProperties.GetCenterDistace() - num;
			tmpOuterRadius.x = radius.x + outlineProperties.GetCenterDistace() + num;
			tmpOuterRadius.y = radius.y + outlineProperties.GetCenterDistace() + num;
			int num2 = vh.currentVertCount - 1;
			float num3 = ellipseProperties.AdjustedResolution;
			int num4;
			for (int i = 0; i < ellipseProperties.AdjustedResolution; i++)
			{
				uv.x = (float)i / num3;
				tmpVertPos.x = center.x + unitPositionData.UnitPositions[i].x * tmpInnerRadius.x;
				tmpVertPos.y = center.y + unitPositionData.UnitPositions[i].y * tmpInnerRadius.y;
				tmpVertPos.z = 0f;
				uv.y = 0f;
				vh.AddVert(tmpVertPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				tmpVertPos.x = center.x + unitPositionData.UnitPositions[i].x * tmpOuterRadius.x;
				tmpVertPos.y = center.y + unitPositionData.UnitPositions[i].y * tmpOuterRadius.y;
				tmpVertPos.z = 0f;
				uv.y = 1f;
				vh.AddVert(tmpVertPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				if (i > 0)
				{
					num4 = num2 + i * 2;
					vh.AddTriangle(num4 - 1, num4, num4 + 1);
					vh.AddTriangle(num4, num4 + 2, num4 + 1);
				}
			}
			uv.x = 1f;
			tmpVertPos.x = center.x + unitPositionData.UnitPositions[0].x * tmpInnerRadius.x;
			tmpVertPos.y = center.y + unitPositionData.UnitPositions[0].y * tmpInnerRadius.y;
			tmpVertPos.z = 0f;
			uv.y = 0f;
			vh.AddVert(tmpVertPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			tmpVertPos.x = center.x + unitPositionData.UnitPositions[0].x * tmpOuterRadius.x;
			tmpVertPos.y = center.y + unitPositionData.UnitPositions[0].y * tmpOuterRadius.y;
			tmpVertPos.z = 0f;
			uv.y = 1f;
			vh.AddVert(tmpVertPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			num4 = num2 + ellipseProperties.AdjustedResolution * 2;
			vh.AddTriangle(num4 - 1, num4, num4 + 1);
			vh.AddTriangle(num4, num4 + 2, num4 + 1);
			if (!edgeGradientData.IsActive)
			{
				return;
			}
			num = outlineProperties.HalfLineWeight + edgeGradientData.ShadowOffset + edgeGradientData.SizeAdd;
			tmpInnerRadius.x = radius.x + outlineProperties.GetCenterDistace() - num;
			tmpInnerRadius.y = radius.y + outlineProperties.GetCenterDistace() - num;
			tmpOuterRadius.x = radius.x + outlineProperties.GetCenterDistace() + num;
			tmpOuterRadius.y = radius.y + outlineProperties.GetCenterDistace() + num;
			color.a = 0;
			int num5;
			int num6;
			for (int j = 0; j < ellipseProperties.AdjustedResolution; j++)
			{
				uv.x = (float)j / num3;
				tmpVertPos.x = center.x + unitPositionData.UnitPositions[j].x * tmpInnerRadius.x;
				tmpVertPos.y = center.y + unitPositionData.UnitPositions[j].y * tmpInnerRadius.y;
				tmpVertPos.z = 0f;
				uv.y = 0f;
				vh.AddVert(tmpVertPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				tmpVertPos.x = center.x + unitPositionData.UnitPositions[j].x * tmpOuterRadius.x;
				tmpVertPos.y = center.y + unitPositionData.UnitPositions[j].y * tmpOuterRadius.y;
				tmpVertPos.z = 0f;
				uv.y = 1f;
				vh.AddVert(tmpVertPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
				num5 = num4 + j * 2;
				num6 = num2 + j * 2;
				if (j > 0)
				{
					vh.AddTriangle(num6 - 1, num6 + 1, num5 + 3);
					vh.AddTriangle(num5 + 1, num6 - 1, num5 + 3);
					vh.AddTriangle(num6, num5 + 2, num6 + 2);
					vh.AddTriangle(num5 + 2, num5 + 4, num6 + 2);
				}
			}
			uv.x = 1f;
			tmpVertPos.x = center.x + unitPositionData.UnitPositions[0].x * tmpInnerRadius.x;
			tmpVertPos.y = center.y + unitPositionData.UnitPositions[0].y * tmpInnerRadius.y;
			tmpVertPos.z = 0f;
			uv.y = 0f;
			vh.AddVert(tmpVertPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			tmpVertPos.x = center.x + unitPositionData.UnitPositions[0].x * tmpOuterRadius.x;
			tmpVertPos.y = center.y + unitPositionData.UnitPositions[0].y * tmpOuterRadius.y;
			tmpVertPos.z = 0f;
			uv.y = 1f;
			vh.AddVert(tmpVertPos, color, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			num5 = num4 + ellipseProperties.AdjustedResolution * 2;
			num6 = num2 + ellipseProperties.AdjustedResolution * 2;
			vh.AddTriangle(num6 - 1, num6 + 1, num5 + 3);
			vh.AddTriangle(num5 + 1, num6 - 1, num5 + 3);
			vh.AddTriangle(num6, num5 + 2, num6 + 2);
			vh.AddTriangle(num5 + 2, num5 + 4, num6 + 2);
		}
	}
}
