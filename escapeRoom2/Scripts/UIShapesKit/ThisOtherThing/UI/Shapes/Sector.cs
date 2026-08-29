using ThisOtherThing.UI.ShapeUtils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisOtherThing.UI.Shapes
{
	[AddComponentMenu("UI/Shapes/Sector", 50)]
	public class Sector : MaskableGraphic, IShape
	{
		public GeoUtils.ShapeProperties ShapeProperties = new GeoUtils.ShapeProperties();

		public Ellipses.EllipseProperties EllipseProperties = new Ellipses.EllipseProperties();

		public Arcs.ArcProperties ArcProperties = new Arcs.ArcProperties();

		public GeoUtils.ShadowsProperties ShadowProperties = new GeoUtils.ShadowsProperties();

		public GeoUtils.AntiAliasingProperties AntiAliasingProperties = new GeoUtils.AntiAliasingProperties();

		private GeoUtils.UnitPositionData unitPositionData;

		private GeoUtils.EdgeGradientData edgeGradientData;

		private Vector2 radius = Vector2.one;

		private Rect pixelRect;

		public void ForceMeshUpdate()
		{
			SetVerticesDirty();
			SetMaterialDirty();
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			pixelRect = RectTransformUtility.PixelAdjustRect(base.rectTransform, base.canvas);
			Ellipses.SetRadius(ref radius, pixelRect.width, pixelRect.height, EllipseProperties);
			EllipseProperties.UpdateAdjusted(radius, 0f);
			ArcProperties.UpdateAdjusted(EllipseProperties.AdjustedResolution, EllipseProperties.BaseAngle);
			AntiAliasingProperties.UpdateAdjusted(base.canvas);
			ShadowProperties.UpdateAdjusted();
			if (ShadowProperties.ShadowsEnabled)
			{
				for (int i = 0; i < ShadowProperties.Shadows.Length; i++)
				{
					edgeGradientData.SetActiveData(1f - ShadowProperties.Shadows[i].Softness, ShadowProperties.Shadows[i].Size, AntiAliasingProperties.Adjusted);
					Arcs.AddSegment(ref vh, ShadowProperties.GetCenterOffset(pixelRect.center, i), radius, EllipseProperties, ArcProperties, ShadowProperties.Shadows[i].Color, GeoUtils.ZeroV2, ref unitPositionData, edgeGradientData);
				}
			}
			if (ShadowProperties.ShowShape)
			{
				if (AntiAliasingProperties.Adjusted > 0f)
				{
					edgeGradientData.SetActiveData(1f, 0f, AntiAliasingProperties.Adjusted);
				}
				else
				{
					edgeGradientData.Reset();
				}
				Arcs.AddSegment(ref vh, (Vector3)pixelRect.center, radius, EllipseProperties, ArcProperties, ShapeProperties.FillColor, GeoUtils.ZeroV2, ref unitPositionData, edgeGradientData);
			}
		}
	}
}
