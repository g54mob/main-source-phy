using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	[RequireComponent(typeof(Toggle))]
	public class SlideToggle : MonoBehaviour
	{
		[SerializeField]
		private RectTransform checkMark;

		[SerializeField]
		private TextMeshProUGUI onOffText;

		private Toggle toggle;

		private void Awake()
		{
			toggle = GetComponent<Toggle>();
			toggle.onValueChanged.AddListener(OnToggleValueChanged);
			OnToggleValueChanged(toggle.isOn);
		}

		public void OnToggleValueChanged(bool isOn)
		{
			onOffText.text = (isOn ? "ON" : "OFF");
			MoveCheckMark(isOn);
		}

		private void MoveCheckMark(bool isOn)
		{
			int num = (isOn ? 1 : (-1));
			Vector2 anchoredPosition = checkMark.anchoredPosition;
			anchoredPosition.x = (float)num * Mathf.Abs(anchoredPosition.x);
			checkMark.anchoredPosition = anchoredPosition;
		}
	}
}
