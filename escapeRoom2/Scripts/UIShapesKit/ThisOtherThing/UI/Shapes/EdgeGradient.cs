using System;
using ThisOtherThing.UI.ShapeUtils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisOtherThing.UI.Shapes
{
	[AddComponentMenu("UI/Shapes/Edge Gradient", 100)]
	public class EdgeGradient : MaskableGraphic, IShape
	{
		public enum Positions
		{
			Top = 0,
			Bottom = 1,
			Left = 2,
			Right = 3,
			OuterTop = 4,
			OuterBottom = 5,
			OuterLeft = 6,
			OuterRight = 7
		}

		[Serializable]
		public class GradientProperty
		{
			public float Size = 20f;

			public Color32 Color = new Color32(127, 127, 127, byte.MaxValue);

			public Positions Position;
		}

		public GradientProperty[] Properties = new GradientProperty[1]
		{
			new GradientProperty()
		};

		private Vector3 topLeft = Vector3.zero;

		private Color32 gradientColor = new Color32(127, 127, 127, byte.MaxValue);

		public void ForceMeshUpdate()
		{
			SetVerticesDirty();
			SetMaterialDirty();
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			Rect rect = RectTransformUtility.PixelAdjustRect(base.rectTransform, base.canvas);
			for (int i = 0; i < Properties.Length; i++)
			{
				gradientColor.r = Properties[i].Color.r;
				gradientColor.g = Properties[i].Color.g;
				gradientColor.b = Properties[i].Color.b;
				gradientColor.a = 0;
				switch (Properties[i].Position)
				{
				case Positions.Top:
					topLeft.x = rect.xMin;
					topLeft.y = rect.yMax;
					Rects.AddVerticalTwoColorRect(ref vh, topLeft, Properties[i].Size, rect.width, Properties[i].Color, gradientColor, GeoUtils.ZeroV2);
					break;
				case Positions.Bottom:
					topLeft.x = rect.xMin;
					topLeft.y = rect.yMin + Properties[i].Size;
					Rects.AddVerticalTwoColorRect(ref vh, topLeft, Properties[i].Size, rect.width, gradientColor, Properties[i].Color, GeoUtils.ZeroV2);
					break;
				case Positions.Left:
					topLeft.x = rect.xMin;
					topLeft.y = rect.yMax;
					Rects.AddHorizontalTwoColorRect(ref vh, topLeft, rect.height, Properties[i].Size, Properties[i].Color, gradientColor, GeoUtils.ZeroV2);
					break;
				case Positions.Right:
					topLeft.x = rect.xMax - Properties[i].Size;
					topLeft.y = rect.yMax;
					Rects.AddHorizontalTwoColorRect(ref vh, topLeft, rect.height, Properties[i].Size, gradientColor, Properties[i].Color, GeoUtils.ZeroV2);
					break;
				case Positions.OuterTop:
					topLeft.x = rect.xMin;
					topLeft.y = rect.yMax + Properties[i].Size;
					Rects.AddVerticalTwoColorRect(ref vh, topLeft, Properties[i].Size, rect.width, gradientColor, Properties[i].Color, GeoUtils.ZeroV2);
					break;
				case Positions.OuterBottom:
					topLeft.x = rect.xMin;
					topLeft.y = rect.yMin;
					Rects.AddVerticalTwoColorRect(ref vh, topLeft, Properties[i].Size, rect.width, Properties[i].Color, gradientColor, GeoUtils.ZeroV2);
					break;
				case Positions.OuterLeft:
					topLeft.x = rect.xMin - Properties[i].Size;
					topLeft.y = rect.yMax;
					Rects.AddHorizontalTwoColorRect(ref vh, topLeft, rect.height, Properties[i].Size, gradientColor, Properties[i].Color, GeoUtils.ZeroV2);
					break;
				case Positions.OuterRight:
					topLeft.x = rect.xMax;
					topLeft.y = rect.yMax;
					Rects.AddHorizontalTwoColorRect(ref vh, topLeft, rect.height, Properties[i].Size, Properties[i].Color, gradientColor, GeoUtils.ZeroV2);
					break;
				}
			}
		}
	}
}
