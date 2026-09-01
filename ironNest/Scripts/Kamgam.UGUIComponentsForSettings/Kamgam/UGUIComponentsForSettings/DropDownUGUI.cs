using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Kamgam.UGUIComponentsForSettings
{
	public class DropDownUGUI : MonoBehaviour
	{
		public delegate void OnSelectionChangedDelegate(int optionIndex);

		public UnityEvent<int> OnSelectionChangedEvent;

		public OnSelectionChangedDelegate OnSelectionChanged;

		public TMP_Dropdown DropDown;

		protected List<string> _getOptionsCache;

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

		public void Start()
		{
		}

		protected void onValueChanged(int index)
		{
		}

		public void SetOptions(IList<string> options)
		{
		}

		public List<string> GetOptions()
		{
			return null;
		}

		public void ClearOptions()
		{
		}

		public void AddOptions(List<Sprite> options)
		{
		}

		public void AddOptions(List<string> options)
		{
		}

		public void AddOptions(List<TMP_Dropdown.OptionData> options)
		{
		}
	}
}
