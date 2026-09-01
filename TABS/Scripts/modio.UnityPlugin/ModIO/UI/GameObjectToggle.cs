using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class GameObjectToggle : StateToggleDisplay
	{
		[Header("UI Components")]
		public GameObject onDisplay;

		public GameObject offDisplay;

		[Header("Display Data")]
		[SerializeField]
		private bool m_isOn = true;

		[SerializeField]
		private bool m_switchToggleTargetGraphic;

		private Toggle toggle;

		public override bool isOn
		{
			get
			{
				return m_isOn;
			}
			set
			{
				if (m_isOn != value)
				{
					m_isOn = value;
					UpdateDisplay();
				}
			}
		}

		private void UpdateDisplay()
		{
			if (onDisplay != null)
			{
				onDisplay.SetActive(m_isOn);
				UpdateToggleComponent(onDisplay, m_isOn);
			}
			if (offDisplay != null)
			{
				offDisplay.SetActive(!m_isOn);
				UpdateToggleComponent(offDisplay, !m_isOn);
			}
			void UpdateToggleComponent(GameObject display, bool show)
			{
				if (m_switchToggleTargetGraphic)
				{
					if (toggle == null)
					{
						toggle = GetComponent<Toggle>();
					}
					Image component = display.GetComponent<Image>();
					if (component != null)
					{
						component.enabled = show;
						if (show && toggle != null)
						{
							toggle.targetGraphic = component;
						}
					}
				}
			}
		}

		private void Start()
		{
			UpdateDisplay();
		}
	}
}
