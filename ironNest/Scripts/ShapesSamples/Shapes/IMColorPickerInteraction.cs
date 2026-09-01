using UnityEngine;

namespace Shapes
{
	public class IMColorPickerInteraction : MonoBehaviour
	{
		private enum ColorPickerElement
		{
			None = 0,
			HueStrip = 1,
			Rectangle = 2
		}

		public IMColorPickerRenderer picker;

		private ColorPickerElement currentInteraction;

		private void Update()
		{
		}

		private void OnDisable()
		{
		}

		public void RaycastInteract(Ray ray, bool onPress, bool whileHeld, bool onRelease)
		{
		}

		private void UpdatePickerColor(Vector2 pt)
		{
		}

		private ColorPickerElement GetPickerElementAt(Vector2 pt)
		{
			return default(ColorPickerElement);
		}

		private bool HueStripContains(Vector2 pt)
		{
			return false;
		}
	}
}
