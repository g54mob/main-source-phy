using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class CategoryButton : MonoBehaviour
	{
		[SerializeField]
		private GameObject m_on;

		[SerializeField]
		private GameObject m_off;

		[SerializeField]
		private Image m_onIcon;

		[SerializeField]
		private Image m_offIcon;

		[SerializeField]
		private LocalizeText m_text;

		private bool m_isOn;

		private void Awake()
		{
			GetComponent<Button>().onClick.AddListener(delegate
			{
				SetState(!m_isOn);
			});
		}

		public void Init(Sprite icon, string name, bool isOn = false)
		{
			m_onIcon.sprite = icon;
			m_offIcon.sprite = icon;
			m_text.LocaleID = name;
			SetState(isOn);
		}

		public void SetState(bool on)
		{
			m_isOn = on;
			m_on.SetActive(on);
			m_off.SetActive(!on);
		}
	}
}
