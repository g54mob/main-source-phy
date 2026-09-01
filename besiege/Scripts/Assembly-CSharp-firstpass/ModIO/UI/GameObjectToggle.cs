using UnityEngine;

namespace ModIO.UI
{
	public class GameObjectToggle : StateToggleDisplay
	{
		[Header("UI Components")]
		public GameObject onDisplay;

		public GameObject offDisplay;

		[SerializeField]
		[Header("Display Data")]
		private bool m_isOn = true;

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
			}
			if (offDisplay != null)
			{
				offDisplay.SetActive(!m_isOn);
			}
		}

		private void Start()
		{
			UpdateDisplay();
		}
	}
}
