using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings
{
	public class OptionsButtonUGUI : MonoBehaviour
	{
		public delegate void OnValueChangedDelegate(int optionIndex);

		public static string UndefinedText;

		public TextMeshProUGUI TextTf;

		[Tooltip("Loop the options if either of the ends is reached?")]
		public bool Loop;

		[SerializeField]
		[Tooltip("If enabled and if this is selected (has focus) then the prev/next action will be triggered by keyboard/controller navigation too.\nNOTICE: This also means it will deny left/right selection navigation away from this is object. Useful for console type UIs.")]
		protected bool _enableButtonControls;

		protected AutoNavigationOverrides _autoNavigationOverrides;

		protected Selectable _selectable;

		public Func<string, string> OptionToTextFunc;

		public UnityEvent<int> OnValueChangedEvent;

		public OnValueChangedDelegate OnValueChanged;

		[SerializeField]
		protected List<string> _options;

		protected List<string> _getOptionsCache;

		protected int _value;

		public bool EnableButtonControls
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public AutoNavigationOverrides AutoNavigationOverrides => null;

		public Selectable Selectable => null;

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

		public int NumOfOptions => 0;

		public void Start()
		{
		}

		public virtual void Update()
		{
		}

		public void SetOptions(IList<string> options)
		{
		}

		public List<string> GetOptions()
		{
			return null;
		}

		public void UpdateText()
		{
		}

		public void ClearOptions()
		{
		}

		public void Prev()
		{
		}

		public void Next()
		{
		}

		public void SetSelected()
		{
		}
	}
}
