using System.Collections.Generic;
using UnityEngine;

namespace Shapes
{
	[ExecuteAlways]
	[RequireComponent(typeof(Canvas))]
	public class ImmediateModeCanvas : ImmediateModeShapeDrawer
	{
		private static ImCanvasContext canvasContext;

		private Canvas canvas;

		private RectTransform canvasRectTf;

		private Camera camUI;

		private List<ImmediateModePanel> panels;

		private Canvas Canvas => null;

		private RectTransform CanvasRectTf => null;

		private Camera CamUI => null;

		private bool IsCameraBasedUI => false;

		public void Add(ImmediateModePanel panel)
		{
		}

		public void Remove(ImmediateModePanel panel)
		{
		}

		protected void DrawPanels()
		{
		}

		private bool CameraShouldRenderUI(Camera cam)
		{
			return false;
		}

		public override void DrawShapes(Camera cam)
		{
		}

		private bool DisplayAsWorldSpacePanel(Camera cam)
		{
			return false;
		}

		private Matrix4x4 GetOverlayToWorldMatrix(Camera cam)
		{
			return default(Matrix4x4);
		}

		public virtual void DrawCanvasShapes(ImCanvasContext ctx)
		{
		}
	}
}
