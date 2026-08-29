using ThisOtherThing.UI.ShapeUtils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisOtherThing.UI.Shapes
{
	[AddComponentMenu("UI/Shapes/Rectangle", 1)]
	public class Rectangle : MaskableGraphic, IShape
	{
		public GeoUtils.OutlineShapeProperties ShapeProperties = new GeoUtils.OutlineShapeProperties();

		public RoundedRects.RoundedProperties RoundedProperties = new RoundedRects.RoundedProperties();

		public GeoUtils.OutlineProperties OutlineProperties = new GeoUtils.OutlineProperties();

		public GeoUtils.ShadowsProperties ShadowProperties = new GeoUtils.ShadowsProperties();

		public GeoUtils.AntiAliasingProperties AntiAliasingProperties = new GeoUtils.AntiAliasingProperties();

		public Sprite Sprite;

		private RoundedRects.RoundedCornerUnitPositionData unitPositionData;

		private GeoUtils.EdgeGradientData edgeGradientData;

		public override Color color
		{
			get
			{
				return ShapeProperties.FillColor;
			}
			set
			{
				ShapeProperties.FillColor = value;
				base.color = value;
				ForceMeshUpdate();
			}
		}

		public override Texture mainTexture
		{
			get
			{
				if (Sprite == null)
				{
					if (material != null && material.mainTexture != null)
					{
						return material.mainTexture;
					}
					return Graphic.s_WhiteTexture;
				}
				return Sprite.texture;
			}
		}

		public void ForceMeshUpdate()
		{
			SetVerticesDirty();
			SetMaterialDirty();
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			Rect rect = RectTransformUtility.PixelAdjustRect(base.rectTransform, base.canvas);
			RoundedProperties.UpdateAdjusted(rect, 0f);
			AntiAliasingProperties.UpdateAdjusted(base.canvas);
			OutlineProperties.UpdateAdjusted();
			ShadowProperties.UpdateAdjusted();
			if (ShadowProperties.ShadowsEnabled && ShapeProperties.DrawFill && ShapeProperties.DrawFillShadow)
			{
				for (int i = 0; i < ShadowProperties.Shadows.Length; i++)
				{
					edgeGradientData.SetActiveData(1f - ShadowProperties.Shadows[i].Softness, ShadowProperties.Shadows[i].Size, AntiAliasingProperties.Adjusted);
					RoundedRects.AddRoundedRect(ref vh, ShadowProperties.GetCenterOffset(rect.center, i), rect.width, rect.height, RoundedProperties, ShadowProperties.Shadows[i].Color, GeoUtils.ZeroV2, ref unitPositionData, edgeGradientData);
				}
			}
			if (ShadowProperties.ShowShape && ShapeProperties.DrawFill)
			{
				if (AntiAliasingProperties.Adjusted > 0f)
				{
					edgeGradientData.SetActiveData(1f, 0f, AntiAliasingProperties.Adjusted);
				}
				else
				{
					edgeGradientData.Reset();
				}
				RoundedRects.AddRoundedRect(ref vh, rect.center, rect.width, rect.height, RoundedProperties, ShapeProperties.FillColor, GeoUtils.ZeroV2, ref unitPositionData, edgeGradientData);
			}
			if (ShadowProperties.ShadowsEnabled && ShapeProperties.DrawOutline && ShapeProperties.DrawOutlineShadow)
			{
				for (int j = 0; j < ShadowProperties.Shadows.Length; j++)
				{
					edgeGradientData.SetActiveData(1f - ShadowProperties.Shadows[j].Softness, ShadowProperties.Shadows[j].Size, AntiAliasingProperties.Adjusted);
					RoundedRects.AddRoundedRectLine(ref vh, ShadowProperties.GetCenterOffset(rect.center, j), rect.width, rect.height, OutlineProperties, RoundedProperties, ShadowProperties.Shadows[j].Color, GeoUtils.ZeroV2, ref unitPositionData, edgeGradientData);
				}
			}
			if (ShadowProperties.ShowShape && ShapeProperties.DrawOutline)
			{
				if (AntiAliasingProperties.Adjusted > 0f)
				{
					edgeGradientData.SetActiveData(1f, 0f, AntiAliasingProperties.Adjusted);
				}
				else
				{
					edgeGradientData.Reset();
				}
				RoundedRects.AddRoundedRectLine(ref vh, rect.center, rect.width, rect.height, OutlineProperties, RoundedProperties, ShapeProperties.OutlineColor, GeoUtils.ZeroV2, ref unitPositionData, edgeGradientData);
			}
		}

		protected override void UpdateMaterial()
		{
			base.UpdateMaterial();
			if (Sprite == null)
			{
				base.canvasRenderer.SetAlphaTexture(null);
				return;
			}
			Texture2D associatedAlphaSplitTexture = Sprite.associatedAlphaSplitTexture;
			if (associatedAlphaSplitTexture != null)
			{
				base.canvasRenderer.SetAlphaTexture(associatedAlphaSplitTexture);
			}
		}
	}
}
