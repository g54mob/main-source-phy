using System;
using ThisOtherThing.UI.ShapeUtils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisOtherThing.UI.Shapes
{
	[AddComponentMenu("UI/Shapes/Line", 30)]
	public class Line : MaskableGraphic, IShape
	{
		public GeoUtils.ShapeProperties ShapeProperties = new GeoUtils.ShapeProperties();

		public PointsList.PointListsProperties PointListsProperties = new PointsList.PointListsProperties();

		public Lines.LineProperties LineProperties = new Lines.LineProperties();

		public GeoUtils.OutlineProperties OutlineProperties = new GeoUtils.OutlineProperties();

		public GeoUtils.ShadowsProperties ShadowProperties = new GeoUtils.ShadowsProperties();

		public GeoUtils.AntiAliasingProperties AntiAliasingProperties = new GeoUtils.AntiAliasingProperties();

		public Sprite Sprite;

		private PointsList.PointsData[] pointsListData = new PointsList.PointsData[1];

		private GeoUtils.EdgeGradientData edgeGradientData;

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

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			OutlineProperties.UpdateAdjusted();
			ShadowProperties.UpdateAdjusted();
			if (pointsListData == null || pointsListData.Length != PointListsProperties.PointListProperties.Length)
			{
				Array.Resize(ref pointsListData, PointListsProperties.PointListProperties.Length);
				for (int i = 0; i < pointsListData.Length; i++)
				{
					pointsListData[i].NeedsUpdate = true;
					PointListsProperties.PointListProperties[i].GeneratorData.NeedsUpdate = true;
				}
			}
			for (int j = 0; j < PointListsProperties.PointListProperties.Length; j++)
			{
				PointListsProperties.PointListProperties[j].SetPoints();
			}
			for (int k = 0; k < PointListsProperties.PointListProperties.Length; k++)
			{
				if (PointListsProperties.PointListProperties[k].Positions == null || PointListsProperties.PointListProperties[k].Positions.Length <= 1)
				{
					continue;
				}
				AntiAliasingProperties.UpdateAdjusted(base.canvas);
				if (ShadowProperties.ShadowsEnabled)
				{
					for (int l = 0; l < ShadowProperties.Shadows.Length; l++)
					{
						edgeGradientData.SetActiveData(1f - ShadowProperties.Shadows[l].Softness, ShadowProperties.Shadows[l].Size, AntiAliasingProperties.Adjusted);
						Lines.AddLine(ref vh, LineProperties, PointListsProperties.PointListProperties[k], ShadowProperties.GetCenterOffset(GeoUtils.ZeroV2, l), OutlineProperties, ShadowProperties.Shadows[l].Color, GeoUtils.ZeroV2, ref pointsListData[k], edgeGradientData);
					}
				}
			}
			for (int m = 0; m < PointListsProperties.PointListProperties.Length; m++)
			{
				if (PointListsProperties.PointListProperties[m].Positions != null && PointListsProperties.PointListProperties[m].Positions.Length > 1 && ShadowProperties.ShowShape)
				{
					if (AntiAliasingProperties.Adjusted > 0f)
					{
						edgeGradientData.SetActiveData(1f, 0f, AntiAliasingProperties.Adjusted);
					}
					else
					{
						edgeGradientData.Reset();
					}
					Lines.AddLine(ref vh, LineProperties, PointListsProperties.PointListProperties[m], GeoUtils.ZeroV2, OutlineProperties, ShapeProperties.FillColor, GeoUtils.ZeroV2, ref pointsListData[m], edgeGradientData);
				}
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
