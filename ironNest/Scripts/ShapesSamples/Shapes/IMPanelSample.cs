using UnityEngine;

namespace Shapes
{
	public class IMPanelSample : ImmediateModePanel
	{
		[Range(0f, 1f)]
		public float fillAmount;

		public Gradient colorGradient;

		public string title;

		public override void DrawPanelShapes(Rect rect, ImCanvasContext ctx)
		{
		}

		private Rect Inset(Rect r, float amount)
		{
			return default(Rect);
		}
	}
}
