using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleFileBrowser
{
	public class FileBrowserAccessRestrictedPanel : MonoBehaviour
	{
		[SerializeField]
		private HorizontalLayoutGroup contentLayoutGroup;

		[SerializeField]
		private TextMeshProUGUI messageLabel;

		[SerializeField]
		private Button okButton;

		private void Awake()
		{
			okButton.onClick.AddListener(OKButtonClicked);
		}

		internal void Show()
		{
			base.gameObject.SetActive(value: true);
		}

		private void LateUpdate()
		{
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Escape))
			{
				OKButtonClicked();
			}
		}

		internal void RefreshSkin(UISkin skin)
		{
			contentLayoutGroup.padding.bottom = 22 + (int)(skin.RowSpacing + skin.RowHeight);
			Image componentInChildren = GetComponentInChildren<Image>();
			componentInChildren.color = skin.PopupPanelsBackgroundColor;
			componentInChildren.sprite = skin.PopupPanelsBackground;
			RectTransform obj = (RectTransform)okButton.transform.parent;
			obj.sizeDelta = new Vector2(obj.sizeDelta.x, skin.RowHeight);
			skin.ApplyTo(okButton);
			skin.ApplyTo(messageLabel, skin.PopupPanelsTextColor);
		}

		private void OKButtonClicked()
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
