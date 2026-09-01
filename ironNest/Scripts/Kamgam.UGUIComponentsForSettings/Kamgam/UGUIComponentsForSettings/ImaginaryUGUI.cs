using UnityEngine;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings
{
	public class ImaginaryUGUI : MaskableGraphic
	{
		[Tooltip("Enable to change to a circular hit area.")]
		public bool Circular;

		public float Radius;

		public override bool Raycast(Vector2 sp, Camera eventCamera)
		{
			return false;
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
		}
	}
}
