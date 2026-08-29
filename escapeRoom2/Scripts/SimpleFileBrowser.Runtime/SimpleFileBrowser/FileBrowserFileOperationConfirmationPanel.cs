using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleFileBrowser
{
	public class FileBrowserFileOperationConfirmationPanel : MonoBehaviour
	{
		public enum OperationType
		{
			Delete = 0,
			Overwrite = 1
		}

		public delegate void OnOperationConfirmed();

		[SerializeField]
		private VerticalLayoutGroup contentLayoutGroup;

		[SerializeField]
		private TextMeshProUGUI[] titleLabels;

		[SerializeField]
		private GameObject[] targetItems;

		[SerializeField]
		private Image[] targetItemIcons;

		[SerializeField]
		private TextMeshProUGUI[] targetItemNames;

		[SerializeField]
		private GameObject targetItemsRest;

		[SerializeField]
		private TextMeshProUGUI targetItemsRestLabel;

		[SerializeField]
		private Button yesButton;

		[SerializeField]
		private Button noButton;

		[SerializeField]
		private float narrowScreenWidth = 380f;

		private OnOperationConfirmed onOperationConfirmed;

		private void Awake()
		{
			yesButton.onClick.AddListener(OnYesButtonClicked);
			noButton.onClick.AddListener(OnNoButtonClicked);
		}

		internal void Show(FileBrowser fileBrowser, List<FileSystemEntry> items, OperationType operationType, OnOperationConfirmed onOperationConfirmed)
		{
			Show(fileBrowser, items, null, operationType, onOperationConfirmed);
		}

		internal void Show(FileBrowser fileBrowser, List<FileSystemEntry> items, List<int> selectedItemIndices, OperationType operationType, OnOperationConfirmed onOperationConfirmed)
		{
			this.onOperationConfirmed = onOperationConfirmed;
			int num = selectedItemIndices?.Count ?? items.Count;
			for (int i = 0; i < titleLabels.Length; i++)
			{
				titleLabels[i].gameObject.SetActive(operationType == (OperationType)i);
			}
			for (int j = 0; j < targetItems.Length; j++)
			{
				targetItems[j].SetActive(j < num);
			}
			for (int k = 0; k < targetItems.Length && k < num; k++)
			{
				FileSystemEntry fileInfo = items[selectedItemIndices?[k] ?? k];
				targetItemIcons[k].sprite = fileBrowser.GetIconForFileEntry(in fileInfo);
				targetItemNames[k].text = fileInfo.Name;
			}
			if (num > targetItems.Length)
			{
				targetItemsRestLabel.text = "...and " + (num - targetItems.Length) + " other";
				targetItemsRest.SetActive(value: true);
			}
			else
			{
				targetItemsRest.SetActive(value: false);
			}
			base.gameObject.SetActive(value: true);
		}

		internal void OnCanvasDimensionsChanged(Vector2 size)
		{
			if (size.x >= narrowScreenWidth)
			{
				(yesButton.transform as RectTransform).anchorMin = new Vector2(0.5f, 0f);
				(yesButton.transform as RectTransform).anchorMax = new Vector2(0.75f, 1f);
				(noButton.transform as RectTransform).anchorMin = new Vector2(0.75f, 0f);
			}
			else
			{
				(yesButton.transform as RectTransform).anchorMin = Vector2.zero;
				(yesButton.transform as RectTransform).anchorMax = new Vector2(0.5f, 1f);
				(noButton.transform as RectTransform).anchorMin = new Vector2(0.5f, 0f);
			}
		}

		private void LateUpdate()
		{
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
			{
				OnYesButtonClicked();
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				OnNoButtonClicked();
			}
		}

		internal void RefreshSkin(UISkin skin)
		{
			contentLayoutGroup.spacing = skin.RowSpacing;
			contentLayoutGroup.padding.bottom = 22 + (int)(skin.RowSpacing + skin.RowHeight);
			Image componentInChildren = GetComponentInChildren<Image>();
			componentInChildren.color = skin.PopupPanelsBackgroundColor;
			componentInChildren.sprite = skin.PopupPanelsBackground;
			RectTransform obj = yesButton.transform.parent as RectTransform;
			obj.sizeDelta = new Vector2(obj.sizeDelta.x, skin.RowHeight);
			skin.ApplyTo(yesButton);
			skin.ApplyTo(noButton);
			for (int i = 0; i < titleLabels.Length; i++)
			{
				skin.ApplyTo(titleLabels[i], skin.PopupPanelsTextColor);
			}
			skin.ApplyTo(targetItemsRestLabel, skin.PopupPanelsTextColor);
			for (int j = 0; j < targetItemNames.Length; j++)
			{
				skin.ApplyTo(targetItemNames[j], skin.PopupPanelsTextColor);
			}
			for (int k = 0; k < targetItems.Length; k++)
			{
				targetItems[k].GetComponent<LayoutElement>().preferredHeight = skin.FileHeight;
			}
		}

		private void OnYesButtonClicked()
		{
			base.gameObject.SetActive(value: false);
			if (onOperationConfirmed != null)
			{
				onOperationConfirmed();
			}
		}

		private void OnNoButtonClicked()
		{
			base.gameObject.SetActive(value: false);
			onOperationConfirmed = null;
		}
	}
}
