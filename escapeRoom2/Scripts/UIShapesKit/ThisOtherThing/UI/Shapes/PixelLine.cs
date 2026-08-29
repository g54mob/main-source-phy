using System;
using ThisOtherThing.UI.ShapeUtils;
using UnityEngine;
using UnityEngine.UI;

namespace ThisOtherThing.UI.Shapes
{
	[AddComponentMenu("UI/Shapes/Pixel Line", 100)]
	public class PixelLine : MaskableGraphic, IShape
	{
		public float LineWeight = 1f;

		public GeoUtils.SnappedPositionAndOrientationProperties SnappedProperties = new GeoUtils.SnappedPositionAndOrientationProperties();

		private Vector3 center = Vector3.zero;

		public void ForceMeshUpdate()
		{
			SetVerticesDirty();
			SetMaterialDirty();
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			Rect rect = RectTransformUtility.PixelAdjustRect(base.rectTransform, base.canvas);
			float num = 1f;
			if (base.canvas != null)
			{
				num = 1f / base.canvas.scaleFactor;
			}
			float num2 = LineWeight * num;
			switch (SnappedProperties.Position)
			{
			case GeoUtils.SnappedPositionAndOrientationProperties.PositionTypes.Center:
				center.x = rect.center.x;
				center.y = rect.center.y;
				break;
			case GeoUtils.SnappedPositionAndOrientationProperties.PositionTypes.Top:
				center.x = rect.center.x;
				center.y = rect.yMax - num2;
				break;
			case GeoUtils.SnappedPositionAndOrientationProperties.PositionTypes.Bottom:
				center.x = rect.center.x;
				center.y = rect.yMin;
				break;
			case GeoUtils.SnappedPositionAndOrientationProperties.PositionTypes.Left:
				center.x = rect.xMin;
				center.y = rect.center.y;
				break;
			case GeoUtils.SnappedPositionAndOrientationProperties.PositionTypes.Right:
				center.x = rect.xMax;
				center.y = rect.center.y;
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			float num3 = 0f;
			float num4 = 0f;
			switch (SnappedProperties.Orientation)
			{
			case GeoUtils.SnappedPositionAndOrientationProperties.OrientationTypes.Horizontal:
				num3 = rect.width;
				num4 = num2;
				break;
			case GeoUtils.SnappedPositionAndOrientationProperties.OrientationTypes.Vertical:
				num3 = num2;
				num4 = rect.height;
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			Rects.AddRect(ref vh, center, num3, num4, color, GeoUtils.ZeroV2);
		}
	}
}
