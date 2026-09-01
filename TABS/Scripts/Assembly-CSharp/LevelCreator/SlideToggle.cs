using System;
using InControl;
using Landfall.TABS_Input;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LevelCreator
{
	public class SlideToggle : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		[Serializable]
		public class SlideAction : UnityEvent<bool>
		{
		}

		[HideInInspector]
		public bool m_isOn;

		[HideInInspector]
		public Sprite m_iconSprite;

		[HideInInspector]
		public string m_playerAction;

		[HideInInspector]
		public OnStateChanged m_onStateChanged;

		[SerializeField]
		private Image m_handle;

		[SerializeField]
		private Image m_iconMask;

		[SerializeField]
		private Image m_icon;

		[SerializeField]
		private TMP_Text m_bindingText;

		[SerializeField]
		private Color m_offColor = Color.white;

		[SerializeField]
		private Color m_onColor = Color.white;

		private SlideAction m_onToggle = new SlideAction();

		private Slider m_slider;

		public void Init(Sprite icon, InputState inputState, string playerAction, bool initialState, OnStateChanged onToggle)
		{
			m_iconSprite = icon;
			m_playerAction = playerAction;
			m_onStateChanged = onToggle;
			m_icon.sprite = icon;
			m_iconMask.sprite = icon;
			PlayerAction playerActionByName = PlayerActions.Instance.GetPlayerActionByName(playerAction);
			inputState.AddOnKeyDownListener(playerActionByName, delegate
			{
				Toggle();
			});
			m_bindingText.gameObject.AddComponent<DMActionGlyph>().SetAction(playerAction);
			m_onToggle.AddListener(delegate(bool state)
			{
				m_onStateChanged.Invoke(state);
				Utility.PlayUIHoverSound();
			});
			SetOn(initialState);
		}

		private void Start()
		{
			m_slider = GetComponentInChildren<Slider>();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			Toggle();
		}

		public void Toggle()
		{
			SetOn(!m_isOn);
		}

		public void SetOn(bool on)
		{
			m_isOn = on;
			float num = ((!m_isOn) ? 1 : 0);
			float to = 1f - num;
			LeanTween.value(num, to, 0.25f).setOnUpdate(delegate(float val)
			{
				if (m_slider != null)
				{
					m_slider.value = val;
				}
			}).setEaseOutExpo();
			Color color = (m_isOn ? m_onColor : m_offColor);
			m_handle.color = color;
			m_icon.color = (m_isOn ? Color.clear : Color.white);
			m_onToggle?.Invoke(m_isOn);
		}
	}
}
