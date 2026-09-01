using UnityEngine;
using UnityEngine.UI;

namespace SRDebugger.UI.Other
{
	[RequireComponent(typeof(ScrollRect))]
	[ExecuteInEditMode]
	public class ScrollRectPatch : MonoBehaviour
	{
		public RectTransform Content;

		public Mask ReplaceMask;

		public RectTransform Viewport;

		private void Awake()
		{
			ScrollRect component = GetComponent<ScrollRect>();
			component.content = Content;
			component.viewport = Viewport;
			if (ReplaceMask != null)
			{
				GameObject gameObject = ReplaceMask.gameObject;
				Object.Destroy(gameObject.GetComponent<Graphic>());
				Object.Destroy(gameObject.GetComponent<CanvasRenderer>());
				Object.Destroy(ReplaceMask);
				gameObject.AddComponent<RectMask2D>();
			}
		}
	}
}
