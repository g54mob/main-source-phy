using UnityEngine;

namespace Shapes
{
	public class ImCanvasContext
	{
		public Camera camera;

		public Canvas canvas;

		public Rect canvasRect;

		public Matrix4x4 worldToCanvas;

		public Matrix4x4 canvasToWorld;

		public Matrix4x4 canvasToWorldNet;

		internal void UpdateParams(Canvas canvas, Camera camera, RectTransform cnvTf, Matrix4x4 canvasToWorldNet)
		{
		}
	}
}
