using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class ToolControlsBuilder : MonoBehaviour
	{
		[SerializeField]
		private Transform m_toggleContainer;

		[SerializeField]
		private Transform m_sliderContainer;

		[SerializeField]
		private Transform m_sliderMenuHint;

		[SerializeField]
		private SlideToggle m_toggleTemplate;

		[SerializeField]
		private ToolControlSlider m_sliderTemplate;

		public void ClearUI()
		{
			Utility.DestroyChildren(m_toggleContainer);
			Utility.DestroyChildren(m_sliderContainer);
		}

		public void BuildToolUI(List<ToolControlUIEntry> uiEntries, InputState inputState)
		{
			if (uiEntries == null)
			{
				return;
			}
			bool flag = false;
			foreach (ToolControlUIEntry uiEntry in uiEntries)
			{
				if (uiEntry is ToolToggleEntry toolToggleEntry)
				{
					SlideToggle slideToggle = Object.Instantiate(m_toggleTemplate, m_toggleContainer);
					slideToggle.Init(toolToggleEntry.m_icon, inputState, toolToggleEntry.m_actionName, toolToggleEntry.m_initialState, toolToggleEntry.m_onStateChanged);
					slideToggle.gameObject.SetActive(value: true);
				}
				else if (uiEntry is ToolSliderEntry toolSliderEntry)
				{
					ToolControlSlider toolControlSlider = Object.Instantiate(m_sliderTemplate, m_sliderContainer);
					toolControlSlider.Init(toolSliderEntry.m_icon, inputState, toolSliderEntry.m_onValueChanged, toolSliderEntry.m_sliderInfo);
					toolControlSlider.gameObject.SetActive(value: true);
					flag = true;
				}
			}
			EnableSliderMenuHint(flag);
			Utility.DelayAction(this, delegate
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(m_sliderContainer.GetComponent<RectTransform>());
			});
		}

		public List<ToolControlUIEntry> FetchEntries()
		{
			List<ToolControlUIEntry> list = new List<ToolControlUIEntry>();
			if (m_toggleContainer != null)
			{
				for (int i = 0; i < m_toggleContainer.childCount; i++)
				{
					if (i < m_toggleContainer.childCount)
					{
						SlideToggle component = m_toggleContainer.GetChild(i).GetComponent<SlideToggle>();
						if (component != null)
						{
							list.Add(new ToolToggleEntry
							{
								m_actionName = component.m_playerAction,
								m_icon = component.m_iconSprite,
								m_initialState = component.m_isOn,
								m_onStateChanged = component.m_onStateChanged
							});
						}
					}
				}
			}
			if (m_sliderContainer != null)
			{
				for (int j = 0; j < m_sliderContainer.childCount; j++)
				{
					if (j < m_sliderContainer.childCount)
					{
						ToolControlSlider component2 = m_sliderContainer.GetChild(j).GetComponent<ToolControlSlider>();
						if (component2 != null)
						{
							ToolControlSlider.SliderInfo sliderInfo = component2.m_sliderInfo;
							sliderInfo.initialValue = component2.m_slider.value;
							list.Add(new ToolSliderEntry
							{
								m_icon = component2.m_iconSprite,
								m_sliderInfo = sliderInfo,
								m_onValueChanged = component2.m_onValueChanged
							});
						}
					}
				}
			}
			return list;
		}

		public void EnableSliderMenuHint(bool enabled)
		{
			m_sliderMenuHint.gameObject.SetActive(enabled);
		}
	}
}
