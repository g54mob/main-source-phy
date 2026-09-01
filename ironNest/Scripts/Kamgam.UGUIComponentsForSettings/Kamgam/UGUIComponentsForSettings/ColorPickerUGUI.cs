using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings
{
	public class ColorPickerUGUI : MonoBehaviour
	{
		public delegate void OnColorChangedDelegate(Color color);

		public delegate void OnSelectionChangedDelegate(int selectedIndex);

		public GameObject Active;

		public Image ColorImage;

		public UnityEvent<Color> OnColorChangedEvent;

		public OnColorChangedDelegate OnColorChanged;

		public UnityEvent<int> OnSelectionChangedEvent;

		public OnSelectionChangedDelegate OnSelectionChanged;

		protected ColorPickerButtonUGUI[] _colorButtons;

		protected int _selectedIndex;

		public ColorPickerButtonUGUI[] ColorButtons => null;

		public bool IsActive => false;

		public int SelectedIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public void Update()
		{
		}

		public void Toggle()
		{
		}

		public void SetActive(bool active)
		{
		}

		protected void updateColorImage(Color color)
		{
		}

		private void onColorButtonClick(ColorPickerButtonUGUI button)
		{
		}

		public void SetColorOptions(IList<Color> colorOptions)
		{
		}

		public List<Color> GetColorOptions()
		{
			return null;
		}
	}
}
