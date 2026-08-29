using System;
using UnityEngine;
using UnityEngine.UI;

namespace ThisOtherThing.UI.ShapeUtils
{
	public class RoundedRects
	{
		public struct RoundedCornerUnitPositionData
		{
			public Vector2[] TLUnitPositions;

			public Vector2[] TRUnitPositions;

			public Vector2[] BRUnitPositions;

			public Vector2[] BLUnitPositions;
		}

		[Serializable]
		public class RoundedProperties
		{
			public enum RoundedType
			{
				None = 0,
				Uniform = 1,
				Individual = 2
			}

			public enum ResolutionType
			{
				Uniform = 0,
				Individual = 1
			}

			public RoundedType Type;

			public ResolutionType ResolutionMode;

			public float UniformRadius = 15f;

			public bool UseMaxRadius;

			public float TLRadius = 15f;

			public GeoUtils.RoundingProperties TLResolution = new GeoUtils.RoundingProperties();

			public float TRRadius = 15f;

			public GeoUtils.RoundingProperties TRResolution = new GeoUtils.RoundingProperties();

			public float BRRadius = 15f;

			public GeoUtils.RoundingProperties BRResolution = new GeoUtils.RoundingProperties();

			public float BLRadius = 15f;

			public GeoUtils.RoundingProperties BLResolution = new GeoUtils.RoundingProperties();

			public GeoUtils.RoundingProperties UniformResolution = new GeoUtils.RoundingProperties();

			public float AdjustedTLRadius { get; private set; }

			public float AdjustedTRRadius { get; private set; }

			public float AdjustedBRRadius { get; private set; }

			public float AdjustedBLRadius { get; private set; }

			public void UpdateAdjusted(Rect rect, float offset)
			{
				switch (Type)
				{
				case RoundedType.Uniform:
					if (UseMaxRadius)
					{
						AdjustedTLRadius = Mathf.Min(rect.width, rect.height) * 0.5f;
						AdjustedTRRadius = AdjustedTLRadius;
						AdjustedBRRadius = AdjustedTLRadius;
						AdjustedBLRadius = AdjustedTLRadius;
					}
					else
					{
						AdjustedTLRadius = UniformRadius;
						AdjustedTRRadius = AdjustedTLRadius;
						AdjustedBRRadius = AdjustedTLRadius;
						AdjustedBLRadius = AdjustedTLRadius;
					}
					break;
				case RoundedType.Individual:
					AdjustedTLRadius = TLRadius;
					AdjustedTRRadius = TRRadius;
					AdjustedBRRadius = BRRadius;
					AdjustedBLRadius = BLRadius;
					break;
				case RoundedType.None:
					AdjustedTLRadius = 0f;
					AdjustedTRRadius = AdjustedTLRadius;
					AdjustedBRRadius = AdjustedTLRadius;
					AdjustedBLRadius = AdjustedTLRadius;
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				if (ResolutionMode == ResolutionType.Uniform)
				{
					TLResolution.UpdateAdjusted(AdjustedTLRadius, offset, UniformResolution, 4f);
					TRResolution.UpdateAdjusted(AdjustedTRRadius, offset, UniformResolution, 4f);
					BRResolution.UpdateAdjusted(AdjustedBRRadius, offset, UniformResolution, 4f);
					BLResolution.UpdateAdjusted(AdjustedBLRadius, offset, UniformResolution, 4f);
				}
				else
				{
					TLResolution.UpdateAdjusted(AdjustedTLRadius, offset, 4f);
					TRResolution.UpdateAdjusted(AdjustedTRRadius, offset, 4f);
					BRResolution.UpdateAdjusted(AdjustedBRRadius, offset, 4f);
					BLResolution.UpdateAdjusted(AdjustedBLRadius, offset, 4f);
				}
			}

			public void OnCheck(Rect rect)
			{
				float max = Mathf.Min(rect.width, rect.height) * 0.5f;
				switch (Type)
				{
				case RoundedType.Uniform:
					UniformRadius = Mathf.Clamp(UniformRadius, 0f, max);
					break;
				case RoundedType.Individual:
					TLRadius = Mathf.Max(TLRadius, 0f);
					TRRadius = Mathf.Max(TRRadius, 0f);
					BRRadius = Mathf.Max(BRRadius, 0f);
					BLRadius = Mathf.Max(BLRadius, 0f);
					break;
				}
				TLResolution.OnCheck();
				TRResolution.OnCheck();
				BRResolution.OnCheck();
				BLResolution.OnCheck();
				UniformResolution.OnCheck();
			}
		}

		private static Vector3 tmpV3 = Vector3.zero;

		private static Vector3 tmpPos = Vector3.zero;

		private static Vector2 tmpUV = Vector2.zero;

		private static void SetCornerUnitPositions(RoundedProperties roundedProperties, ref RoundedCornerUnitPositionData cornerUnitPositions)
		{
			SetUnitPosition(ref cornerUnitPositions.TLUnitPositions, roundedProperties.TLResolution.AdjustedResolution, GeoUtils.HalfPI + MathF.PI, roundedProperties.TLResolution.MakeSharpCorner);
			SetUnitPosition(ref cornerUnitPositions.TRUnitPositions, roundedProperties.TRResolution.AdjustedResolution, 0f, roundedProperties.TRResolution.MakeSharpCorner);
			SetUnitPosition(ref cornerUnitPositions.BRUnitPositions, roundedProperties.BRResolution.AdjustedResolution, GeoUtils.HalfPI, roundedProperties.BRResolution.MakeSharpCorner);
			SetUnitPosition(ref cornerUnitPositions.BLUnitPositions, roundedProperties.BLResolution.AdjustedResolution, MathF.PI, roundedProperties.BLResolution.MakeSharpCorner);
		}

		private static void SetUnitPosition(ref Vector2[] unitPositions, int resolution, float baseAngle, bool makeSharpCorner)
		{
			bool flag = false;
			if (unitPositions == null || unitPositions.Length != resolution)
			{
				unitPositions = new Vector2[resolution];
				for (int i = 0; i < unitPositions.Length; i++)
				{
					unitPositions[i] = GeoUtils.ZeroV2;
				}
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			float num = GeoUtils.HalfPI / ((float)resolution - 1f);
			if (makeSharpCorner)
			{
				float f = baseAngle + GeoUtils.HalfPI * 0.5f;
				float num2 = Mathf.Sqrt(2f);
				for (int j = 0; j < resolution; j++)
				{
					unitPositions[j].x = Mathf.Sin(f) * num2;
					unitPositions[j].y = Mathf.Cos(f) * num2;
				}
			}
			else
			{
				for (int k = 0; k < resolution; k++)
				{
					float f = baseAngle + num * (float)k;
					unitPositions[k].x = Mathf.Sin(f);
					unitPositions[k].y = Mathf.Cos(f);
				}
			}
		}

		public static void AddRoundedRect(ref VertexHelper vh, Vector2 center, float width, float height, RoundedProperties roundedProperties, Color32 color, Vector2 uv, ref RoundedCornerUnitPositionData cornerUnitPositions, GeoUtils.EdgeGradientData edgeGradientData)
		{
			if (roundedProperties.Type == RoundedProperties.RoundedType.None)
			{
				Rects.AddRect(ref vh, center, width, height, color, edgeGradientData);
				return;
			}
			SetCornerUnitPositions(roundedProperties, ref cornerUnitPositions);
			int currentVertCount = vh.currentVertCount;
			tmpUV.x = 0.5f;
			tmpUV.y = 0.5f;
			vh.AddVert(center, color, tmpUV, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			float num = Mathf.Min(height, width);
			num *= 1f - edgeGradientData.InnerScale;
			AddRoundedRectVerticesRing(ref vh, center, width - num, height - num, width - num, height - num, roundedProperties.AdjustedTLRadius * edgeGradientData.InnerScale, (roundedProperties.AdjustedTLRadius + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale, roundedProperties.AdjustedTRRadius * edgeGradientData.InnerScale, (roundedProperties.AdjustedTRRadius + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale, roundedProperties.AdjustedBRRadius * edgeGradientData.InnerScale, (roundedProperties.AdjustedBRRadius + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale, roundedProperties.AdjustedBLRadius * edgeGradientData.InnerScale, (roundedProperties.AdjustedBLRadius + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale, cornerUnitPositions, color, uv, addIndices: false);
			int num2 = vh.currentVertCount - currentVertCount;
			for (int i = 0; i < num2 - 1; i++)
			{
				vh.AddTriangle(currentVertCount, currentVertCount + i, currentVertCount + i + 1);
			}
			vh.AddTriangle(currentVertCount, vh.currentVertCount - 1, currentVertCount + 1);
			if (edgeGradientData.IsActive)
			{
				float num3 = 0f;
				num3 += edgeGradientData.ShadowOffset;
				num3 += edgeGradientData.SizeAdd;
				color.a = 0;
				AddRoundedRectVerticesRing(ref vh, center, width, height, width, height, roundedProperties.AdjustedTLRadius, roundedProperties.AdjustedTLRadius + num3, roundedProperties.AdjustedTRRadius, roundedProperties.AdjustedTRRadius + num3, roundedProperties.AdjustedBRRadius, roundedProperties.AdjustedBRRadius + num3, roundedProperties.AdjustedBLRadius, roundedProperties.AdjustedBLRadius + num3, cornerUnitPositions, color, uv, addIndices: true);
			}
		}

		public static void AddRoundedRectLine(ref VertexHelper vh, Vector2 center, float width, float height, GeoUtils.OutlineProperties outlineProperties, RoundedProperties roundedProperties, Color32 color, Vector2 uv, ref RoundedCornerUnitPositionData cornerUnitPositions, GeoUtils.EdgeGradientData edgeGradientData)
		{
			float fullWidth = width + outlineProperties.GetOuterDistace() * 2f;
			float fullHeight = height + outlineProperties.GetOuterDistace() * 2f;
			if (roundedProperties.Type == RoundedProperties.RoundedType.None)
			{
				Rects.AddRectRing(ref vh, outlineProperties, center, width, height, color, uv, edgeGradientData);
				return;
			}
			SetCornerUnitPositions(roundedProperties, ref cornerUnitPositions);
			byte a = color.a;
			float num;
			if (edgeGradientData.IsActive)
			{
				color.a = 0;
				num = outlineProperties.GetCenterDistace() - outlineProperties.HalfLineWeight - edgeGradientData.ShadowOffset;
				num -= edgeGradientData.SizeAdd;
				AddRoundedRectVerticesRing(ref vh, center, width, height, fullWidth, fullHeight, roundedProperties.AdjustedTLRadius, roundedProperties.AdjustedTLRadius + num, roundedProperties.AdjustedTRRadius, roundedProperties.AdjustedTRRadius + num, roundedProperties.AdjustedBRRadius, roundedProperties.AdjustedBRRadius + num, roundedProperties.AdjustedBLRadius, roundedProperties.AdjustedBLRadius + num, cornerUnitPositions, color, uv, addIndices: false);
				color.a = a;
			}
			num = Mathf.LerpUnclamped(outlineProperties.GetCenterDistace(), outlineProperties.GetCenterDistace() - outlineProperties.HalfLineWeight - edgeGradientData.ShadowOffset, edgeGradientData.InnerScale);
			AddRoundedRectVerticesRing(ref vh, center, width, height, fullWidth, fullHeight, roundedProperties.AdjustedTLRadius, roundedProperties.AdjustedTLRadius + num, roundedProperties.AdjustedTRRadius, roundedProperties.AdjustedTRRadius + num, roundedProperties.AdjustedBRRadius, roundedProperties.AdjustedBRRadius + num, roundedProperties.AdjustedBLRadius, roundedProperties.AdjustedBLRadius + num, cornerUnitPositions, color, uv, edgeGradientData.IsActive);
			num = outlineProperties.GetCenterDistace() + (outlineProperties.HalfLineWeight + edgeGradientData.ShadowOffset) * edgeGradientData.InnerScale;
			AddRoundedRectVerticesRing(ref vh, center, width, height, fullWidth, fullHeight, roundedProperties.AdjustedTLRadius, roundedProperties.AdjustedTLRadius + num, roundedProperties.AdjustedTRRadius, roundedProperties.AdjustedTRRadius + num, roundedProperties.AdjustedBRRadius, roundedProperties.AdjustedBRRadius + num, roundedProperties.AdjustedBLRadius, roundedProperties.AdjustedBLRadius + num, cornerUnitPositions, color, uv, addIndices: true);
			if (edgeGradientData.IsActive)
			{
				num = outlineProperties.GetCenterDistace() + outlineProperties.HalfLineWeight + edgeGradientData.ShadowOffset;
				num += edgeGradientData.SizeAdd;
				color.a = 0;
				AddRoundedRectVerticesRing(ref vh, center, width, height, fullWidth, fullHeight, roundedProperties.AdjustedTLRadius, roundedProperties.AdjustedTLRadius + num, roundedProperties.AdjustedTRRadius, roundedProperties.AdjustedTRRadius + num, roundedProperties.AdjustedBRRadius, roundedProperties.AdjustedBRRadius + num, roundedProperties.AdjustedBLRadius, roundedProperties.AdjustedBLRadius + num, cornerUnitPositions, color, uv, addIndices: true);
			}
		}

		private static void AddRoundedRectVerticesRing(ref VertexHelper vh, Vector2 center, float width, float height, float fullWidth, float fullHeight, float tlRadius, float tlOuterRadius, float trRadius, float trOuterRadius, float brRadius, float brOuterRadius, float blRadius, float blOuterRadius, RoundedCornerUnitPositionData cornerUnitPositions, Color32 color, Vector2 uv, bool addIndices)
		{
			float num = center.x - width * 0.5f;
			float num2 = center.y - height * 0.5f;
			float num3 = center.x + width * 0.5f;
			float num4 = center.y + height * 0.5f;
			float num5 = center.x - fullWidth * 0.5f;
			float num6 = center.y - fullHeight * 0.5f;
			tmpV3.x = num3 - trRadius;
			tmpV3.y = num4 - trRadius;
			if (trOuterRadius < 0f)
			{
				tmpV3.x += trOuterRadius;
				tmpV3.y += trOuterRadius;
				trOuterRadius = 0f;
			}
			for (int i = 0; i < cornerUnitPositions.TRUnitPositions.Length; i++)
			{
				tmpPos.x = tmpV3.x + cornerUnitPositions.TRUnitPositions[i].x * trOuterRadius;
				tmpPos.y = tmpV3.y + cornerUnitPositions.TRUnitPositions[i].y * trOuterRadius;
				tmpPos.z = tmpV3.z;
				tmpUV.x = (tmpPos.x - num5) / fullWidth;
				tmpUV.y = (tmpPos.y - num6) / fullHeight;
				vh.AddVert(tmpPos, color, tmpUV, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			}
			tmpV3.x = num3 - brRadius;
			tmpV3.y = num2 + brRadius;
			if (brOuterRadius < 0f)
			{
				tmpV3.x += brOuterRadius;
				tmpV3.y -= brOuterRadius;
				brOuterRadius = 0f;
			}
			for (int j = 0; j < cornerUnitPositions.BRUnitPositions.Length; j++)
			{
				tmpPos.x = tmpV3.x + cornerUnitPositions.BRUnitPositions[j].x * brOuterRadius;
				tmpPos.y = tmpV3.y + cornerUnitPositions.BRUnitPositions[j].y * brOuterRadius;
				tmpPos.z = tmpV3.z;
				tmpUV.x = (tmpPos.x - num5) / fullWidth;
				tmpUV.y = (tmpPos.y - num6) / fullHeight;
				vh.AddVert(tmpPos, color, tmpUV, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			}
			tmpV3.x = num + blRadius;
			tmpV3.y = num2 + blRadius;
			if (blOuterRadius < 0f)
			{
				tmpV3.x -= blOuterRadius;
				tmpV3.y -= blOuterRadius;
				blOuterRadius = 0f;
			}
			for (int k = 0; k < cornerUnitPositions.BLUnitPositions.Length; k++)
			{
				tmpPos.x = tmpV3.x + cornerUnitPositions.BLUnitPositions[k].x * blOuterRadius;
				tmpPos.y = tmpV3.y + cornerUnitPositions.BLUnitPositions[k].y * blOuterRadius;
				tmpPos.z = tmpV3.z;
				tmpUV.x = (tmpPos.x - num5) / fullWidth;
				tmpUV.y = (tmpPos.y - num6) / fullHeight;
				vh.AddVert(tmpPos, color, tmpUV, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			}
			tmpV3.x = num + tlRadius;
			tmpV3.y = num4 - tlRadius;
			if (tlOuterRadius < 0f)
			{
				tmpV3.x -= tlOuterRadius;
				tmpV3.y += tlOuterRadius;
				tlOuterRadius = 0f;
			}
			for (int l = 0; l < cornerUnitPositions.TLUnitPositions.Length; l++)
			{
				tmpPos.x = tmpV3.x + cornerUnitPositions.TLUnitPositions[l].x * tlOuterRadius;
				tmpPos.y = tmpV3.y + cornerUnitPositions.TLUnitPositions[l].y * tlOuterRadius;
				tmpPos.z = tmpV3.z;
				tmpUV.x = (tmpPos.x - num5) / fullWidth;
				tmpUV.y = (tmpPos.y - num6) / fullHeight;
				vh.AddVert(tmpPos, color, tmpUV, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			}
			tmpPos.x = tmpV3.x + cornerUnitPositions.TRUnitPositions[0].x * tlOuterRadius;
			tmpPos.y = tmpV3.y + cornerUnitPositions.TRUnitPositions[0].y * tlOuterRadius;
			tmpPos.z = tmpV3.z;
			tmpUV.x = (tmpPos.x - num5) / fullWidth;
			tmpUV.y = (tmpPos.y - num6) / fullHeight;
			vh.AddVert(tmpPos, color, tmpUV, GeoUtils.ZeroV2, GeoUtils.UINormal, GeoUtils.UITangent);
			if (addIndices)
			{
				AddRoundedRingIndices(ref vh, cornerUnitPositions);
			}
		}

		private static void AddRoundedRingIndices(ref VertexHelper vh, RoundedCornerUnitPositionData cornerUnitPositions)
		{
			int num = cornerUnitPositions.TLUnitPositions.Length + cornerUnitPositions.TRUnitPositions.Length + cornerUnitPositions.BRUnitPositions.Length + cornerUnitPositions.BLUnitPositions.Length;
			int num2 = num + 1;
			int num3 = vh.currentVertCount - num2 - num2 - 1;
			int num4 = vh.currentVertCount - num2;
			for (int i = 0; i < num; i++)
			{
				vh.AddTriangle(num3 + i + 1, num4 + i, num4 + i + 1);
				vh.AddTriangle(num3 + i + 1, num4 + i + 1, num3 + i + 2);
			}
			vh.AddTriangle(num3 + 1, num4 + num, num4);
			vh.AddTriangle(num3 + 1, num4 - 1, num4 + num);
		}
	}
}
