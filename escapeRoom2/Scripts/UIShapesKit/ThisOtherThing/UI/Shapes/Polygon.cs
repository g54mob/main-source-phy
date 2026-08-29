using System;
using ThisOtherThing.UI.ShapeUtils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisOtherThing.UI.Shapes
{
	[AddComponentMenu("UI/Shapes/Polygon", 30)]
	public class Polygon : MaskableGraphic, IShape
	{
		public GeoUtils.ShapeProperties ShapeProperties = new GeoUtils.ShapeProperties();

		public PointsList.PointListsProperties PointListsProperties = new PointsList.PointListsProperties();

		public Polygons.PolygonProperties PolygonProperties = new Polygons.PolygonProperties();

		public GeoUtils.ShadowsProperties ShadowProperties = new GeoUtils.ShadowsProperties();

		public GeoUtils.AntiAliasingProperties AntiAliasingProperties = new GeoUtils.AntiAliasingProperties();

		private PointsList.PointsData[] pointsListData = new PointsList.PointsData[1];

		private GeoUtils.EdgeGradientData edgeGradientData;

		private Rect pixelRect;

		public void ForceMeshUpdate()
		{
			if (pointsListData == null || pointsListData.Length != PointListsProperties.PointListProperties.Length)
			{
				Array.Resize(ref pointsListData, PointListsProperties.PointListProperties.Length);
			}
			for (int i = 0; i < pointsListData.Length; i++)
			{
				pointsListData[i].NeedsUpdate = true;
				PointListsProperties.PointListProperties[i].GeneratorData.NeedsUpdate = true;
			}
			SetVerticesDirty();
			SetMaterialDirty();
		}

		protected override void OnEnable()
		{
			for (int i = 0; i < pointsListData.Length; i++)
			{
				pointsListData[i].IsClosed = true;
			}
			base.OnEnable();
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			if (pointsListData == null || pointsListData.Length != PointListsProperties.PointListProperties.Length)
			{
				Array.Resize(ref pointsListData, PointListsProperties.PointListProperties.Length);
				for (int i = 0; i < pointsListData.Length; i++)
				{
					pointsListData[i].NeedsUpdate = true;
					pointsListData[i].IsClosed = true;
				}
			}
			pixelRect = RectTransformUtility.PixelAdjustRect(base.rectTransform, base.canvas);
			AntiAliasingProperties.UpdateAdjusted(base.canvas);
			ShadowProperties.UpdateAdjusted();
			for (int j = 0; j < PointListsProperties.PointListProperties.Length; j++)
			{
				PointListsProperties.PointListProperties[j].GeneratorData.SkipLastPosition = true;
				PointListsProperties.PointListProperties[j].SetPoints();
			}
			for (int k = 0; k < PointListsProperties.PointListProperties.Length; k++)
			{
				if (PointListsProperties.PointListProperties[k].Positions == null || PointListsProperties.PointListProperties[k].Positions.Length <= 2)
				{
					continue;
				}
				PolygonProperties.UpdateAdjusted(PointListsProperties.PointListProperties[k]);
				if (ShadowProperties.ShadowsEnabled)
				{
					for (int l = 0; l < ShadowProperties.Shadows.Length; l++)
					{
						edgeGradientData.SetActiveData(1f - ShadowProperties.Shadows[l].Softness, ShadowProperties.Shadows[l].Size, AntiAliasingProperties.Adjusted);
						Polygons.AddPolygon(ref vh, PolygonProperties, PointListsProperties.PointListProperties[k], ShadowProperties.GetCenterOffset(pixelRect.center, l), ShadowProperties.Shadows[l].Color, GeoUtils.ZeroV2, ref pointsListData[k], edgeGradientData);
					}
				}
			}
			for (int m = 0; m < PointListsProperties.PointListProperties.Length; m++)
			{
				if (PointListsProperties.PointListProperties[m].Positions == null || PointListsProperties.PointListProperties[m].Positions.Length <= 2)
				{
					continue;
				}
				PolygonProperties.UpdateAdjusted(PointListsProperties.PointListProperties[m]);
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
					Polygons.AddPolygon(ref vh, PolygonProperties, PointListsProperties.PointListProperties[m], pixelRect.center, ShapeProperties.FillColor, GeoUtils.ZeroV2, ref pointsListData[m], edgeGradientData);
				}
			}
		}
	}
}
