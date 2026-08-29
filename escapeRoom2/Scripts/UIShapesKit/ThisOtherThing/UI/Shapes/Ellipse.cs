using ThisOtherThing.UI.ShapeUtils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisOtherThing.UI.Shapes
{
	[AddComponentMenu("UI/Shapes/Ellipse", 1)]
	public class Ellipse : MaskableGraphic, IShape
	{
		public GeoUtils.OutlineShapeProperties ShapeProperties = new GeoUtils.OutlineShapeProperties();

		public Ellipses.EllipseProperties EllipseProperties = new Ellipses.EllipseProperties();

		public GeoUtils.OutlineProperties OutlineProperties = new GeoUtils.OutlineProperties();

		public GeoUtils.ShadowsProperties ShadowProperties = new GeoUtils.ShadowsProperties();

		public GeoUtils.AntiAliasingProperties AntiAliasingProperties = new GeoUtils.AntiAliasingProperties();

		public Sprite Sprite;

		private GeoUtils.UnitPositionData unitPositionData;

		private GeoUtils.EdgeGradientData edgeGradientData;

		private Vector2 radius = Vector2.one;

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
			OutlineProperties.UpdateAdjusted();
			ShadowProperties.UpdateAdjusted();
			Rect rect = RectTransformUtility.PixelAdjustRect(base.rectTransform, base.canvas);
			Ellipses.SetRadius(ref radius, rect.width, rect.height, EllipseProperties);
			EllipseProperties.UpdateAdjusted(radius, 0f);
			AntiAliasingProperties.UpdateAdjusted(base.canvas);
			if (ShadowProperties.ShadowsEnabled && ShapeProperties.DrawFill && ShapeProperties.DrawFillShadow)
			{
				for (int i = 0; i < ShadowProperties.Shadows.Length; i++)
				{
					edgeGradientData.SetActiveData(1f - ShadowProperties.Shadows[i].Softness, ShadowProperties.Shadows[i].Size, AntiAliasingProperties.Adjusted);
					Ellipses.AddCircle(ref vh, ShadowProperties.GetCenterOffset(rect.center, i), radius, EllipseProperties, ShadowProperties.Shadows[i].Color, GeoUtils.ZeroV2, ref unitPositionData, edgeGradientData);
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
				Ellipses.AddCircle(ref vh, (Vector3)rect.center, radius, EllipseProperties, ShapeProperties.FillColor, GeoUtils.ZeroV2, ref unitPositionData, edgeGradientData);
			}
			if (ShadowProperties.ShadowsEnabled && ShapeProperties.DrawOutline && ShapeProperties.DrawOutlineShadow)
			{
				for (int j = 0; j < ShadowProperties.Shadows.Length; j++)
				{
					edgeGradientData.SetActiveData(1f - ShadowProperties.Shadows[j].Softness, ShadowProperties.Shadows[j].Size, AntiAliasingProperties.Adjusted);
					Ellipses.AddRing(ref vh, ShadowProperties.GetCenterOffset(rect.center, j), radius, OutlineProperties, EllipseProperties, ShadowProperties.Shadows[j].Color, GeoUtils.ZeroV2, ref unitPositionData, edgeGradientData);
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
				Ellipses.AddRing(ref vh, (Vector3)rect.center, radius, OutlineProperties, EllipseProperties, ShapeProperties.OutlineColor, Vector2.zero, ref unitPositionData, edgeGradientData);
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
