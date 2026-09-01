using UnityEngine;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings
{
	public class ColorPickerButtonUGUI : MonoBehaviour
	{
		public Image ColorImage;

		[SerializeField]
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

		public void Start()
		{
		}

		protected void updateImageColor()
		{
		}
	}
}
