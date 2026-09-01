using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class ToolControlSlider : MonoBehaviour
	{
		[Serializable]
		public struct SliderInfo
		{
			public float min;

			public float max;

			public float increment;

			public float initialValue;

			public bool useDecimals;
		}

		[HideInInspector]
		public Sprite m_iconSprite;

		[HideInInspector]
		public SliderInfo m_sliderInfo;

		[HideInInspector]
		public OnValueChanged m_onValueChanged;

		[SerializeField]
		private Image m_smallIcon;

		[SerializeField]
		private Image m_largeIcon;

		[SerializeField]
		private TMP_Text m_bindingText;

		[HideInInspector]
		public Slider m_slider;

		public void Init(Sprite icon, InputState inputState, OnValueChanged onValueChanged, SliderInfo sliderInfo)
		{
			m_iconSprite = icon;
			m_sliderInfo = sliderInfo;
			m_onValueChanged = onValueChanged;
			m_slider = GetComponentInChildren<Slider>();
			m_slider.minValue = sliderInfo.min;
			m_slider.maxValue = sliderInfo.max;
			m_slider.wholeNumbers = !sliderInfo.useDecimals;
			if (onValueChanged != null)
			{
				m_slider.onValueChanged.AddListener(delegate(float val)
				{
					onValueChanged.Invoke(val);
					Utility.PlayUIHoverSound();
					m_slider.handleRect.transform.LeanScale(Vector3.one * 1.3f, 0.05f).setOnComplete((System.Action)delegate
					{
						m_slider.handleRect.transform.LeanScale(Vector3.one * 1f, 0.1f);
					});
				});
			}
			m_slider.value = sliderInfo.initialValue;
			m_smallIcon.sprite = icon;
			m_largeIcon.sprite = icon;
		}
	}
}
