using UnityEngine;

namespace Shapes
{
	[ExecuteAlways]
	public class ImmediateModePanel : MonoBehaviour
	{
		private ImmediateModeCanvas imCanvas;

		private ImmediateModeCanvas ImCanvas => null;

		public bool Valid => false;

		public virtual void OnEnable()
		{
		}

		public virtual void OnDisable()
		{
		}

		internal void DrawPanel(ImCanvasContext ctx)
		{
		}

		public virtual void DrawPanelShapes(Rect rect, ImCanvasContext ctx)
		{
		}
	}
}
