using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UI.Widgets
{
	public class UIButtonInvert : MonoBehaviour
	{
		private Image m_backgroundImage;

		private TextMeshProUGUI m_text;

		private Button m_button;

		[SerializeField]
		private float m_flashTime;

		private void Start()
		{
			m_button = GetComponent<Button>();
			m_backgroundImage = base.transform.Find("Checkmark").GetComponent<Image>();
			m_text = GetComponentInChildren<TextMeshProUGUI>();
			m_button.onClick.AddListener(OnClicked);
			m_backgroundImage.color = new Color(0f, 0f, 0f, 0f);
			m_text.color = Color.white;
		}

		private void OnClicked()
		{
			if (base.isActiveAndEnabled)
			{
				StartCoroutine(OnToggleColors());
			}
		}

		private IEnumerator OnToggleColors()
		{
			m_backgroundImage.color = Color.white;
			m_text.color = Color.black;
			yield return new WaitForSeconds(m_flashTime);
			m_backgroundImage.color = new Color(0f, 0f, 0f, 0f);
			m_text.color = Color.white;
		}
	}
}
