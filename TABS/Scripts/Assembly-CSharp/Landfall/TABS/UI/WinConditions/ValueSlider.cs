using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Landfall.TABS.UI.WinConditions
{
	public class ValueSlider : MonoBehaviour
	{
		[Serializable]
		public class OnValueChangedEvent : UnityEvent<int>
		{
		}

		private int m_selectedIndex;

		private List<string> m_options = new List<string>();

		private LocalizeText m_displayText;

		[SerializeField]
		private Image m_ConditionTypeIconImage;

		[SerializeField]
		private OnValueChangedEvent m_onValueChangedEvent;

		public int SelectedIndex => m_selectedIndex;

		public int OptionsCount => m_options.Count;

		public LocalizeText DisplayText => m_displayText;

		public Image ConditionIcon
		{
			get
			{
				return m_ConditionTypeIconImage;
			}
			set
			{
				m_ConditionTypeIconImage = value;
			}
		}

		private void Start()
		{
			m_displayText = GetComponentInChildren<LocalizeText>();
		}

		public void NextValue()
		{
			m_selectedIndex++;
			if (m_selectedIndex == m_options.Count)
			{
				m_selectedIndex = 0;
			}
			m_displayText.LocaleID = m_options[m_selectedIndex];
			m_onValueChangedEvent.Invoke(m_selectedIndex);
		}

		public void PreviousValue()
		{
			m_selectedIndex--;
			if (m_selectedIndex < 0)
			{
				m_selectedIndex = m_options.Count - 1;
			}
			m_displayText.LocaleID = m_options[m_selectedIndex];
			m_onValueChangedEvent.Invoke(m_selectedIndex);
		}

		public void SetValueIndex(int index, bool triggerOnChange = true)
		{
			m_selectedIndex = index;
			m_displayText.LocaleID = m_options[m_selectedIndex];
			if (triggerOnChange)
			{
				m_onValueChangedEvent.Invoke(m_selectedIndex);
			}
		}

		public void SetIcon(Sprite sprite)
		{
			m_ConditionTypeIconImage.sprite = sprite;
		}

		public void AddOption(string option)
		{
			m_options.Add(option);
		}

		public void AddOptions(string[] options)
		{
			m_options.AddRange(options);
		}

		public void AddOptions(List<string> options)
		{
			m_options.AddRange(options);
		}

		public void ClearOptions()
		{
			m_options.Clear();
			m_selectedIndex = 0;
		}

		private void Update()
		{
		}
	}
}
