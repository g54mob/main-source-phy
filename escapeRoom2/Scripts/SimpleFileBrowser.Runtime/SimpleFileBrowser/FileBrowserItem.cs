using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimpleFileBrowser
{
	public class FileBrowserItem : ListItem, IPointerClickHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
	{
		private const float DOUBLE_CLICK_TIME = 0.5f;

		private const float TOGGLE_MULTI_SELECTION_HOLD_TIME = 0.5f;

		protected FileBrowser fileBrowser;

		[SerializeField]
		private Image background;

		[SerializeField]
		private Image icon;

		[SerializeField]
		private Image multiSelectionToggle;

		[SerializeField]
		private TextMeshProUGUI nameText;

		private bool isSelected;

		private bool isHidden;

		private UISkin skin;

		private float pressTime = float.PositiveInfinity;

		private float prevClickTime;

		private RectTransform m_transform;

		public Image Icon => icon;

		public RectTransform TransformComponent
		{
			get
			{
				if (m_transform == null)
				{
					m_transform = (RectTransform)base.transform;
				}
				return m_transform;
			}
		}

		public string Name => nameText.text;

		public bool IsDirectory { get; private set; }

		public void SetFileBrowser(FileBrowser fileBrowser, UISkin skin)
		{
			this.fileBrowser = fileBrowser;
			OnSkinRefreshed(skin, isInitialized: false);
		}

		public void SetFile(Sprite icon, string name, bool isDirectory)
		{
			this.icon.sprite = icon;
			nameText.text = name;
			IsDirectory = isDirectory;
		}

		private void Update()
		{
			if (fileBrowser.AllowMultiSelection && Time.realtimeSinceStartup - pressTime >= 0.5f)
			{
				pressTime = float.PositiveInfinity;
				fileBrowser.OnItemHeld(this);
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Middle)
			{
				return;
			}
			if (eventData.button == PointerEventData.InputButton.Right)
			{
				if (!isSelected)
				{
					prevClickTime = 0f;
					fileBrowser.OnItemSelected(this, isDoubleClick: false);
				}
				fileBrowser.OnContextMenuTriggered(eventData.position);
			}
			else if (Time.realtimeSinceStartup - prevClickTime < 0.5f)
			{
				prevClickTime = 0f;
				fileBrowser.OnItemSelected(this, isDoubleClick: true);
			}
			else
			{
				prevClickTime = Time.realtimeSinceStartup;
				fileBrowser.OnItemSelected(this, isDoubleClick: false);
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				pressTime = Time.realtimeSinceStartup;
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				if (pressTime != float.PositiveInfinity)
				{
					pressTime = float.PositiveInfinity;
				}
				else if (fileBrowser.MultiSelectionToggleSelectionMode)
				{
					eventData.eligibleForClick = false;
				}
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (!isSelected)
			{
				background.color = skin.FileHoveredBackgroundColor;
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (!isSelected)
			{
				background.color = ((base.Position % 2 == 0) ? skin.FileNormalBackgroundColor : skin.FileAlternatingBackgroundColor);
			}
		}

		public void SetSelected(bool isSelected)
		{
			this.isSelected = isSelected;
			background.color = (isSelected ? skin.FileSelectedBackgroundColor : ((base.Position % 2 == 0) ? skin.FileNormalBackgroundColor : skin.FileAlternatingBackgroundColor));
			nameText.color = (isSelected ? skin.FileSelectedTextColor : skin.FileNormalTextColor);
			if (isHidden)
			{
				Color color = nameText.color;
				color.a = 0.55f;
				nameText.color = color;
			}
			if (!multiSelectionToggle)
			{
				return;
			}
			if (fileBrowser.MultiSelectionToggleSelectionMode && (!IsDirectory || fileBrowser.PickerMode != FileBrowser.PickMode.Files))
			{
				if (!multiSelectionToggle.gameObject.activeSelf)
				{
					multiSelectionToggle.gameObject.SetActive(value: true);
					Vector2 vector = new Vector2(multiSelectionToggle.rectTransform.sizeDelta.x, 0f);
					icon.rectTransform.anchoredPosition += vector;
					nameText.rectTransform.anchoredPosition += vector;
				}
				multiSelectionToggle.sprite = (isSelected ? skin.FileMultiSelectionToggleOnIcon : skin.FileMultiSelectionToggleOffIcon);
			}
			else if (multiSelectionToggle.gameObject.activeSelf)
			{
				multiSelectionToggle.gameObject.SetActive(value: false);
				Vector2 vector2 = new Vector2(0f - multiSelectionToggle.rectTransform.sizeDelta.x, 0f);
				icon.rectTransform.anchoredPosition += vector2;
				nameText.rectTransform.anchoredPosition += vector2;
				prevClickTime = 0f;
			}
		}

		public void SetHidden(bool isHidden)
		{
			this.isHidden = isHidden;
			Color color = icon.color;
			color.a = (isHidden ? 0.5f : 1f);
			icon.color = color;
			color = nameText.color;
			color.a = (isHidden ? 0.55f : (isSelected ? skin.FileSelectedTextColor.a : skin.FileNormalTextColor.a));
			nameText.color = color;
		}

		public void OnSkinRefreshed(UISkin skin, bool isInitialized = true)
		{
			this.skin = skin;
			TransformComponent.sizeDelta = new Vector2(TransformComponent.sizeDelta.x, skin.FileHeight);
			skin.ApplyTo(nameText, isSelected ? skin.FileSelectedTextColor : skin.FileNormalTextColor);
			icon.rectTransform.sizeDelta = new Vector2(icon.rectTransform.sizeDelta.x, 0f - skin.FileIconsPadding);
			if (isInitialized)
			{
				SetSelected(isSelected);
			}
		}
	}
}
