using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings
{
	public class RandomColorUGUI : MonoBehaviour
	{
		public delegate void OnColorChangedDelegate(Color color);

		public Image ColorImage;

		public UnityEvent<Color> OnColorChangedEvent;

		public OnColorChangedDelegate OnColorChanged;

		protected Color _color;

		public Color Color
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public void Randomize()
		{
		}

		protected void updateColorImage(Color color)
		{
		}
	}
}
