using ThisOtherThing.UI.ShapeUtils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisOtherThing.UI.Shapes
{
	[AddComponentMenu("UI/Shapes/Arc", 50)]
	public class Arc : MaskableGraphic, IShape
	{
		public GeoUtils.ShapeProperties ShapeProperties = new GeoUtils.ShapeProperties();

		public Ellipses.EllipseProperties EllipseProperties = new Ellipses.EllipseProperties();

		public Arcs.ArcProperties ArcProperties = new Arcs.ArcProperties();

		public Lines.LineProperties LineProperties = new Lines.LineProperties();

		public PointsList.PointListProperties PointListProperties = new PointsList.PointListProperties();

		private PointsList.PointsData PointsData;

		public GeoUtils.OutlineProperties OutlineProperties = new GeoUtils.OutlineProperties();

		public GeoUtils.ShadowsProperties ShadowProperties = new GeoUtils.ShadowsProperties();

		public GeoUtils.AntiAliasingProperties AntiAliasingProperties = new GeoUtils.AntiAliasingProperties();

		private GeoUtils.UnitPositionData unitPositionData;

		private GeoUtils.EdgeGradientData edgeGradientData;

		private Vector2 radius = Vector2.one;

		protected override void OnEnable()
		{
			PointListProperties.GeneratorData.Generator = PointsList.PointListGeneratorData.Generators.Round;
			PointListProperties.GeneratorData.Center.x = 0f;
			PointListProperties.GeneratorData.Center.y = 0f;
			base.OnEnable();
		}

		public void ForceMeshUpdate()
		{
			PointListProperties.GeneratorData.NeedsUpdate = true;
			PointsData.NeedsUpdate = true;
			SetVerticesDirty();
			SetMaterialDirty();
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			Rect rect = RectTransformUtility.PixelAdjustRect(base.rectTransform, base.canvas);
			OutlineProperties.UpdateAdjusted();
			ShadowProperties.UpdateAdjusted();
			Ellipses.SetRadius(ref radius, rect.width, rect.height, EllipseProperties);
			PointListProperties.GeneratorData.Width = radius.x * 2f;
			PointListProperties.GeneratorData.Height = radius.y * 2f;
			EllipseProperties.UpdateAdjusted(radius, OutlineProperties.GetOuterDistace());
			ArcProperties.UpdateAdjusted(EllipseProperties.AdjustedResolution, EllipseProperties.BaseAngle);
			AntiAliasingProperties.UpdateAdjusted(base.canvas);
			PointListProperties.GeneratorData.Resolution = EllipseProperties.AdjustedResolution * 2;
			PointListProperties.GeneratorData.Length = ArcProperties.Length;
			switch (ArcProperties.Direction)
			{
			case Arcs.ArcProperties.ArcDirection.Forward:
				PointListProperties.GeneratorData.Direction = 1f;
				PointListProperties.GeneratorData.FloatStartOffset = EllipseProperties.BaseAngle * 0.5f;
				break;
			case Arcs.ArcProperties.ArcDirection.Centered:
				PointListProperties.GeneratorData.Direction = -1f;
				PointListProperties.GeneratorData.FloatStartOffset = EllipseProperties.BaseAngle * 0.5f + ArcProperties.Length * 0.5f;
				break;
			case Arcs.ArcProperties.ArcDirection.Backward:
				PointListProperties.GeneratorData.Direction = -1f;
				PointListProperties.GeneratorData.FloatStartOffset = EllipseProperties.BaseAngle * 0.5f;
				break;
			}
			if (ShadowProperties.ShadowsEnabled)
			{
				if (AntiAliasingProperties.Adjusted > 0f)
				{
					edgeGradientData.SetActiveData(1f, 0f, AntiAliasingProperties.Adjusted);
				}
				else
				{
					edgeGradientData.Reset();
				}
				if ((OutlineProperties.Type == GeoUtils.OutlineProperties.LineType.Center || OutlineProperties.Type == GeoUtils.OutlineProperties.LineType.Inner) && (radius.x + OutlineProperties.GetInnerDistace() < 0f || radius.y + OutlineProperties.GetInnerDistace() < 0f))
				{
					if (OutlineProperties.Type == GeoUtils.OutlineProperties.LineType.Center)
					{
						radius *= 2f;
					}
					for (int i = 0; i < ShadowProperties.Shadows.Length; i++)
					{
						edgeGradientData.SetActiveData(1f - ShadowProperties.Shadows[i].Softness, ShadowProperties.Shadows[i].Size, AntiAliasingProperties.Adjusted);
						Arcs.AddSegment(ref vh, ShadowProperties.GetCenterOffset(rect.center, i), radius, EllipseProperties, ArcProperties, ShadowProperties.Shadows[i].Color, GeoUtils.ZeroV2, ref unitPositionData, edgeGradientData);
					}
				}
				else
				{
					for (int j = 0; j < ShadowProperties.Shadows.Length; j++)
					{
						edgeGradientData.SetActiveData(1f - ShadowProperties.Shadows[j].Softness, ShadowProperties.Shadows[j].Size, AntiAliasingProperties.Adjusted);
						if (LineProperties.LineCap == Lines.LineProperties.LineCapTypes.Close)
						{
							Arcs.AddArcRing(ref vh, ShadowProperties.GetCenterOffset(rect.center, j), radius, EllipseProperties, ArcProperties, OutlineProperties, ShadowProperties.Shadows[j].Color, GeoUtils.ZeroV2, ref unitPositionData, edgeGradientData);
						}
						else
						{
							Lines.AddLine(ref vh, LineProperties, PointListProperties, ShadowProperties.GetCenterOffset(rect.center, j), OutlineProperties, ShadowProperties.Shadows[j].Color, GeoUtils.ZeroV2, ref PointsData, edgeGradientData);
						}
					}
				}
			}
			if (!ShadowProperties.ShowShape)
			{
				return;
			}
			if (AntiAliasingProperties.Adjusted > 0f)
			{
				edgeGradientData.SetActiveData(1f, 0f, AntiAliasingProperties.Adjusted);
			}
			else
			{
				edgeGradientData.Reset();
			}
			if ((OutlineProperties.Type == GeoUtils.OutlineProperties.LineType.Center || OutlineProperties.Type == GeoUtils.OutlineProperties.LineType.Inner) && (radius.x + OutlineProperties.GetInnerDistace() < 0f || radius.y + OutlineProperties.GetInnerDistace() < 0f))
			{
				if (OutlineProperties.Type == GeoUtils.OutlineProperties.LineType.Center)
				{
					radius.x *= 2f;
					radius.y *= 2f;
				}
				Arcs.AddSegment(ref vh, rect.center, radius, EllipseProperties, ArcProperties, ShapeProperties.FillColor, GeoUtils.ZeroV2, ref unitPositionData, edgeGradientData);
			}
			else if (LineProperties.LineCap == Lines.LineProperties.LineCapTypes.Close)
			{
				Arcs.AddArcRing(ref vh, rect.center, radius, EllipseProperties, ArcProperties, OutlineProperties, ShapeProperties.FillColor, GeoUtils.ZeroV2, ref unitPositionData, edgeGradientData);
			}
			else
			{
				Lines.AddLine(ref vh, LineProperties, PointListProperties, rect.center, OutlineProperties, ShapeProperties.FillColor, GeoUtils.ZeroV2, ref PointsData, edgeGradientData);
			}
		}
	}
}
