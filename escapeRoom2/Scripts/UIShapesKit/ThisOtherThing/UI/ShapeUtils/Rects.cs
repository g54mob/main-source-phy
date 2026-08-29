using UnityEngine;
using UnityEngine.UI;

namespace ThisOtherThing.UI.ShapeUtils
{
	public class Rects
	{
		private static Vector3 tmpPos = Vector3.zero;

		private static Vector2 tmpUVPos = Vector2.zero;

		public static void AddRect(ref VertexHelper vh, Vector2 center, float width, float height, Color32 color, Vector2 uv)
		{
			AddRectVertRing(ref vh, center, width, height, color, width, height);
			AddRectQuadIndices(ref vh);
		}

		public static void AddRect(ref VertexHelper vh, Vector2 center, float width, float height, Color32 color, GeoUtils.EdgeGradientData edgeGradientData)
		{
			width += edgeGradientData.ShadowOffset * 2f;
			height += edgeGradientData.ShadowOffset * 2f;
			float num = Mathf.Min(width, height) * (1f - edgeGradientData.InnerScale);
			AddRectVertRing(ref vh, center, width - num, height - num, color, width, height);
			AddRectQuadIndices(ref vh);
			if (edgeGradientData.IsActive)
			{
				color.a = 0;
				GeoUtils.AddOffset(ref width, ref height, edgeGradientData.SizeAdd);
				AddRectVertRing(ref vh, center, width, height, color, width - edgeGradientData.SizeAdd * 2f, height - edgeGradientData.SizeAdd * 2f, addRingIndices: true);
			}
		}

		public static void AddRectRing(ref VertexHelper vh, GeoUtils.OutlineProperties OutlineProperties, Vector2 center, float width, float height, Color32 color, Vector2 uv, GeoUtils.EdgeGradientData edgeGradientData)
		{
			byte a = color.a;
			float totalWidth = width + OutlineProperties.GetOuterDistace() * 2f;
			float totalHeight = height + OutlineProperties.GetOuterDistace() * 2f;
			width += OutlineProperties.GetCenterDistace() * 2f;
			height += OutlineProperties.GetCenterDistace() * 2f;
			float num = OutlineProperties.HalfLineWeight * 2f + edgeGradientData.ShadowOffset;
			float num2 = num * edgeGradientData.InnerScale;
			if (edgeGradientData.IsActive)
			{
				color.a = 0;
				AddRectVertRing(ref vh, center, width - num - edgeGradientData.SizeAdd, height - num - edgeGradientData.SizeAdd, color, totalWidth, totalHeight);
				color.a = a;
			}
			AddRectVertRing(ref vh, center, width - num2, height - num2, color, totalWidth, totalHeight, edgeGradientData.IsActive);
			AddRectVertRing(ref vh, center, width + num2, height + num2, color, totalWidth, totalHeight, addRingIndices: true);
			if (edgeGradientData.IsActive)
			{
				color.a = 0;
				AddRectVertRing(ref vh, center, width + num + edgeGradientData.SizeAdd, height + num + edgeGradientData.SizeAdd, color, totalWidth, totalHeight, addRingIndices: true);
			}
		}

		public static void AddRectVertRing(ref VertexHelper vh, Vector2 center, float width, float height, Color32 color, float totalWidth, float totalHeight, bool addRingIndices = false)
		{
			float num = 0.5f - width / totalWidth * 0.5f;
			float num2 = 0.5f - height / totalHeight * 0.5f;
			tmpPos.x = center.x - width * 0.5f;
			tmpPos.y = center.y + height * 0.5f;
			tmpUVPos.x = num;
			tmpUVPos.y = 1f - num2;
			vh.AddVert(tmpPos, color, tmpUVPos, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			tmpPos.x += width;
			tmpUVPos.x = 1f - num;
			vh.AddVert(tmpPos, color, tmpUVPos, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			tmpPos.y -= height;
			tmpUVPos.y = num2;
			vh.AddVert(tmpPos, color, tmpUVPos, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			tmpPos.x -= width;
			tmpUVPos.x = num;
			vh.AddVert(tmpPos, color, tmpUVPos, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			if (addRingIndices)
			{
				int num3 = vh.currentVertCount - 8;
				vh.AddTriangle(num3 + 4, num3 + 5, num3);
				vh.AddTriangle(num3, num3 + 5, num3 + 1);
				vh.AddTriangle(num3 + 1, num3 + 5, num3 + 6);
				vh.AddTriangle(num3 + 1, num3 + 6, num3 + 2);
				vh.AddTriangle(num3 + 2, num3 + 6, num3 + 7);
				vh.AddTriangle(num3 + 7, num3 + 3, num3 + 2);
				vh.AddTriangle(num3 + 4, num3 + 3, num3 + 7);
				vh.AddTriangle(num3 + 4, num3, num3 + 3);
			}
		}

		public static void AddRectQuadIndices(ref VertexHelper vh)
		{
			int num = vh.currentVertCount - 4;
			vh.AddTriangle(num, num + 1, num + 3);
			vh.AddTriangle(num + 3, num + 1, num + 2);
		}

		public static void AddVerticalTwoColorRect(ref VertexHelper vh, Vector3 topLeft, float height, float width, Color32 topColor, Color32 bottomColor, Vector2 uv)
		{
			int currentVertCount = vh.currentVertCount;
			vh.AddVert(topLeft, topColor, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			vh.AddVert(topLeft + GeoUtils.RightV3 * width, topColor, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			vh.AddVert(topLeft + GeoUtils.DownV3 * height, bottomColor, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			vh.AddVert(topLeft + GeoUtils.RightV3 * width + GeoUtils.DownV3 * height, bottomColor, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
			vh.AddTriangle(currentVertCount + 2, currentVertCount + 1, currentVertCount + 3);
		}

		public static void AddHorizontalTwoColorRect(ref VertexHelper vh, Vector3 topLeft, float height, float width, Color32 leftColor, Color32 rightColor, Vector2 uv)
		{
			int currentVertCount = vh.currentVertCount;
			vh.AddVert(topLeft, leftColor, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			vh.AddVert(topLeft + GeoUtils.RightV3 * width, rightColor, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			vh.AddVert(topLeft + GeoUtils.DownV3 * height, leftColor, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			vh.AddVert(topLeft + GeoUtils.RightV3 * width + GeoUtils.DownV3 * height, rightColor, uv, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
			vh.AddTriangle(currentVertCount + 2, currentVertCount + 1, currentVertCount + 3);
		}
	}
}
