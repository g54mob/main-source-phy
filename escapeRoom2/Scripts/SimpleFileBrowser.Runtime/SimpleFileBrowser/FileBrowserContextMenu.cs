using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleFileBrowser
{
	public class FileBrowserContextMenu : MonoBehaviour
	{
		[SerializeField]
		private FileBrowser fileBrowser;

		[SerializeField]
		private RectTransform rectTransform;

		[SerializeField]
		private Button selectAllButton;

		[SerializeField]
		private Button deselectAllButton;

		[SerializeField]
		private Button createFolderButton;

		[SerializeField]
		private Button deleteButton;

		[SerializeField]
		private Button renameButton;

		[SerializeField]
		private GameObject selectAllButtonSeparator;

		[SerializeField]
		private LayoutElement[] allButtonLayoutElements;

		[SerializeField]
		private TextMeshProUGUI[] allButtonTexts;

		[SerializeField]
		private Image[] allButtonSeparators;

		[SerializeField]
		private float minDistanceToEdges = 10f;

		private void Awake()
		{
			selectAllButton.onClick.AddListener(OnSelectAllButtonClicked);
			deselectAllButton.onClick.AddListener(OnDeselectAllButtonClicked);
			createFolderButton.onClick.AddListener(OnCreateFolderButtonClicked);
			deleteButton.onClick.AddListener(OnDeleteButtonClicked);
			renameButton.onClick.AddListener(OnRenameButtonClicked);
		}

		internal void Show(bool selectAllButtonVisible, bool deselectAllButtonVisible, bool deleteButtonVisible, bool renameButtonVisible, Vector2 position, bool isMoreOptionsMenu)
		{
			selectAllButton.gameObject.SetActive(selectAllButtonVisible);
			deselectAllButton.gameObject.SetActive(deselectAllButtonVisible);
			deleteButton.gameObject.SetActive(deleteButtonVisible);
			renameButton.gameObject.SetActive(renameButtonVisible);
			selectAllButtonSeparator.SetActive(!deselectAllButtonVisible);
			rectTransform.anchoredPosition = position;
			base.gameObject.SetActive(value: true);
			if (isMoreOptionsMenu)
			{
				rectTransform.pivot = Vector2.one;
				return;
			}
			LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
			Vector2 sizeDelta = rectTransform.sizeDelta;
			Vector2 sizeDelta2 = fileBrowser.rectTransform.sizeDelta;
			Vector2 vector = sizeDelta2;
			vector.Scale(fileBrowser.rectTransform.pivot);
			position += vector;
			Vector2 vector2 = position + new Vector2(sizeDelta.x + minDistanceToEdges, 0f - sizeDelta.y - minDistanceToEdges);
			if (vector2.x <= sizeDelta2.x && vector2.y >= 0f)
			{
				rectTransform.pivot = new Vector2(0f, 1f);
				return;
			}
			vector2 = position - new Vector2(sizeDelta.x + minDistanceToEdges, sizeDelta.y + minDistanceToEdges);
			if (vector2.x >= 0f && vector2.y >= 0f)
			{
				rectTransform.pivot = Vector2.one;
				return;
			}
			vector2 = position + new Vector2(sizeDelta.x + minDistanceToEdges, sizeDelta.y + minDistanceToEdges);
			if (vector2.x <= sizeDelta2.x && vector2.y <= sizeDelta2.y)
			{
				rectTransform.pivot = Vector2.zero;
			}
			else
			{
				rectTransform.pivot = new Vector2(1f, 0f);
			}
		}

		internal void Hide()
		{
			base.gameObject.SetActive(value: false);
		}

		internal void RefreshSkin(UISkin skin)
		{
			rectTransform.GetComponent<Image>().color = skin.ContextMenuBackgroundColor;
			deselectAllButton.image.color = skin.ContextMenuBackgroundColor;
			selectAllButton.image.color = skin.ContextMenuBackgroundColor;
			createFolderButton.image.color = skin.ContextMenuBackgroundColor;
			deleteButton.image.color = skin.ContextMenuBackgroundColor;
			renameButton.image.color = skin.ContextMenuBackgroundColor;
			for (int i = 0; i < allButtonLayoutElements.Length; i++)
			{
				allButtonLayoutElements[i].preferredHeight = skin.RowHeight + 1f;
			}
			for (int j = 0; j < allButtonTexts.Length; j++)
			{
				skin.ApplyTo(allButtonTexts[j], skin.ContextMenuTextColor);
			}
			for (int k = 0; k < allButtonSeparators.Length; k++)
			{
				allButtonSeparators[k].color = skin.ContextMenuSeparatorColor;
			}
		}

		private void OnSelectAllButtonClicked()
		{
			Hide();
			fileBrowser.SelectAllFiles();
		}

		private void OnDeselectAllButtonClicked()
		{
			Hide();
			fileBrowser.DeselectAllFiles();
		}

		private void OnCreateFolderButtonClicked()
		{
			Hide();
			fileBrowser.CreateNewFolder();
		}

		private void OnDeleteButtonClicked()
		{
			Hide();
			fileBrowser.DeleteSelectedFiles();
		}

		private void OnRenameButtonClicked()
		{
			Hide();
			fileBrowser.RenameSelectedFile();
		}
	}
}
