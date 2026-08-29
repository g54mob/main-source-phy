using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SimpleFileBrowser
{
	[CreateAssetMenu(fileName = "UI Skin", menuName = "yasirkula/SimpleFileBrowser/UI Skin", order = 111)]
	public class UISkin : ScriptableObject
	{
		private int m_version;

		[Header("General")]
		[SerializeField]
		private TMP_FontAsset m_font;

		[SerializeField]
		private int m_fontSize = 14;

		[SerializeField]
		private float m_rowHeight = 30f;

		[SerializeField]
		private float m_rowSpacing = 8f;

		[Header("File Browser Window")]
		[SerializeField]
		private Color m_windowColor = Color.grey;

		[SerializeField]
		private Color m_filesListColor = Color.white;

		[SerializeField]
		private Color m_filesVerticalSeparatorColor = Color.grey;

		[SerializeField]
		private Color m_titleBackgroundColor = Color.black;

		[SerializeField]
		private Color m_titleTextColor = Color.white;

		[SerializeField]
		private Color m_windowResizeGizmoColor = Color.black;

		[SerializeField]
		private Color m_headerButtonsColor = Color.white;

		[SerializeField]
		private Sprite m_windowResizeGizmo;

		[SerializeField]
		private Sprite m_headerBackButton;

		[SerializeField]
		private Sprite m_headerForwardButton;

		[SerializeField]
		private Sprite m_headerUpButton;

		[SerializeField]
		private Sprite m_headerContextMenuButton;

		[Header("Input Fields")]
		[SerializeField]
		private Color m_inputFieldNormalBackgroundColor = Color.white;

		[SerializeField]
		private Color m_inputFieldInvalidBackgroundColor = Color.red;

		[SerializeField]
		private Color m_inputFieldTextColor = Color.black;

		[SerializeField]
		private Color m_inputFieldPlaceholderTextColor = new Color(0f, 0f, 0f, 0.5f);

		[SerializeField]
		private Color m_inputFieldSelectedTextColor = Color.blue;

		[SerializeField]
		private Color m_inputFieldCaretColor = Color.black;

		[SerializeField]
		private Sprite m_inputFieldBackground;

		[Header("Buttons")]
		[SerializeField]
		private Color m_buttonColor = Color.white;

		[SerializeField]
		private Color m_buttonTextColor = Color.black;

		[SerializeField]
		private Sprite m_buttonBackground;

		[Header("Dropdowns")]
		[SerializeField]
		private Color m_dropdownColor = Color.white;

		[SerializeField]
		private Color m_dropdownTextColor = Color.black;

		[SerializeField]
		private Color m_dropdownArrowColor = Color.black;

		[SerializeField]
		private Color m_dropdownCheckmarkColor = Color.black;

		[SerializeField]
		private Sprite m_dropdownBackground;

		[SerializeField]
		private Sprite m_dropdownArrow;

		[SerializeField]
		private Sprite m_dropdownCheckmark;

		[Header("Toggles")]
		[SerializeField]
		private Color m_toggleColor = Color.white;

		[SerializeField]
		private Color m_toggleTextColor = Color.black;

		[SerializeField]
		private Color m_toggleCheckmarkColor = Color.black;

		[SerializeField]
		private Sprite m_toggleBackground;

		[SerializeField]
		private Sprite m_toggleCheckmark;

		[Header("Scrollbars")]
		[SerializeField]
		private Color m_scrollbarBackgroundColor = Color.grey;

		[SerializeField]
		private Color m_scrollbarColor = Color.black;

		[Header("Files")]
		[SerializeField]
		private float m_fileHeight = 30f;

		[SerializeField]
		private float m_fileIconsPadding = 6f;

		[SerializeField]
		private Color m_fileNormalBackgroundColor = Color.clear;

		[SerializeField]
		private Color m_fileAlternatingBackgroundColor = Color.clear;

		[SerializeField]
		private Color m_fileHoveredBackgroundColor = Color.cyan;

		[SerializeField]
		private Color m_fileSelectedBackgroundColor = Color.blue;

		[SerializeField]
		private Color m_fileNormalTextColor = Color.black;

		[SerializeField]
		private Color m_fileSelectedTextColor = Color.black;

		[Header("File Icons")]
		[SerializeField]
		private Sprite m_folderIcon;

		[SerializeField]
		private Sprite m_driveIcon;

		[SerializeField]
		private Sprite m_defaultFileIcon;

		[SerializeField]
		private FiletypeIcon[] m_filetypeIcons;

		[NonSerialized]
		private bool initializedFiletypeIcons;

		private Dictionary<string, Sprite> filetypeToIcon;

		[NonSerialized]
		private bool m_allIconExtensionsHaveSingleSuffix = true;

		[SerializeField]
		private Sprite m_fileMultiSelectionToggleOffIcon;

		[SerializeField]
		private Sprite m_fileMultiSelectionToggleOnIcon;

		[Header("Context Menu")]
		[SerializeField]
		private Color m_contextMenuBackgroundColor = Color.grey;

		[SerializeField]
		private Color m_contextMenuTextColor = Color.black;

		[SerializeField]
		private Color m_contextMenuSeparatorColor = Color.black;

		[Header("Popup Panels")]
		[SerializeField]
		[FormerlySerializedAs("m_deletePanelBackgroundColor")]
		private Color m_popupPanelsBackgroundColor = Color.grey;

		[SerializeField]
		[FormerlySerializedAs("m_deletePanelTextColor")]
		private Color m_popupPanelsTextColor = Color.black;

		[SerializeField]
		[FormerlySerializedAs("m_deletePanelBackground")]
		private Sprite m_popupPanelsBackground;

		public int Version => m_version;

		public TMP_FontAsset Font
		{
			get
			{
				return m_font;
			}
			set
			{
				if (m_font != value)
				{
					m_font = value;
					m_version++;
				}
			}
		}

		public int FontSize
		{
			get
			{
				return m_fontSize;
			}
			set
			{
				if (m_fontSize != value)
				{
					m_fontSize = value;
					m_version++;
				}
			}
		}

		public float RowHeight
		{
			get
			{
				return m_rowHeight;
			}
			set
			{
				if (m_rowHeight != value)
				{
					m_rowHeight = value;
					m_version++;
				}
			}
		}

		public float RowSpacing
		{
			get
			{
				return m_rowSpacing;
			}
			set
			{
				if (m_rowSpacing != value)
				{
					m_rowSpacing = value;
					m_version++;
				}
			}
		}

		public Color WindowColor
		{
			get
			{
				return m_windowColor;
			}
			set
			{
				if (m_windowColor != value)
				{
					m_windowColor = value;
					m_version++;
				}
			}
		}

		public Color FilesListColor
		{
			get
			{
				return m_filesListColor;
			}
			set
			{
				if (m_filesListColor != value)
				{
					m_filesListColor = value;
					m_version++;
				}
			}
		}

		public Color FilesVerticalSeparatorColor
		{
			get
			{
				return m_filesVerticalSeparatorColor;
			}
			set
			{
				if (m_filesVerticalSeparatorColor != value)
				{
					m_filesVerticalSeparatorColor = value;
					m_version++;
				}
			}
		}

		public Color TitleBackgroundColor
		{
			get
			{
				return m_titleBackgroundColor;
			}
			set
			{
				if (m_titleBackgroundColor != value)
				{
					m_titleBackgroundColor = value;
					m_version++;
				}
			}
		}

		public Color TitleTextColor
		{
			get
			{
				return m_titleTextColor;
			}
			set
			{
				if (m_titleTextColor != value)
				{
					m_titleTextColor = value;
					m_version++;
				}
			}
		}

		public Color WindowResizeGizmoColor
		{
			get
			{
				return m_windowResizeGizmoColor;
			}
			set
			{
				if (m_windowResizeGizmoColor != value)
				{
					m_windowResizeGizmoColor = value;
					m_version++;
				}
			}
		}

		public Color HeaderButtonsColor
		{
			get
			{
				return m_headerButtonsColor;
			}
			set
			{
				if (m_headerButtonsColor != value)
				{
					m_headerButtonsColor = value;
					m_version++;
				}
			}
		}

		public Sprite WindowResizeGizmo
		{
			get
			{
				return m_windowResizeGizmo;
			}
			set
			{
				if (m_windowResizeGizmo != value)
				{
					m_windowResizeGizmo = value;
					m_version++;
				}
			}
		}

		public Sprite HeaderBackButton
		{
			get
			{
				return m_headerBackButton;
			}
			set
			{
				if (m_headerBackButton != value)
				{
					m_headerBackButton = value;
					m_version++;
				}
			}
		}

		public Sprite HeaderForwardButton
		{
			get
			{
				return m_headerForwardButton;
			}
			set
			{
				if (m_headerForwardButton != value)
				{
					m_headerForwardButton = value;
					m_version++;
				}
			}
		}

		public Sprite HeaderUpButton
		{
			get
			{
				return m_headerUpButton;
			}
			set
			{
				if (m_headerUpButton != value)
				{
					m_headerUpButton = value;
					m_version++;
				}
			}
		}

		public Sprite HeaderContextMenuButton
		{
			get
			{
				return m_headerContextMenuButton;
			}
			set
			{
				if (m_headerContextMenuButton != value)
				{
					m_headerContextMenuButton = value;
					m_version++;
				}
			}
		}

		public Color InputFieldNormalBackgroundColor
		{
			get
			{
				return m_inputFieldNormalBackgroundColor;
			}
			set
			{
				if (m_inputFieldNormalBackgroundColor != value)
				{
					m_inputFieldNormalBackgroundColor = value;
					m_version++;
				}
			}
		}

		public Color InputFieldInvalidBackgroundColor
		{
			get
			{
				return m_inputFieldInvalidBackgroundColor;
			}
			set
			{
				if (m_inputFieldInvalidBackgroundColor != value)
				{
					m_inputFieldInvalidBackgroundColor = value;
					m_version++;
				}
			}
		}

		public Color InputFieldTextColor
		{
			get
			{
				return m_inputFieldTextColor;
			}
			set
			{
				if (m_inputFieldTextColor != value)
				{
					m_inputFieldTextColor = value;
					m_version++;
				}
			}
		}

		public Color InputFieldPlaceholderTextColor
		{
			get
			{
				return m_inputFieldPlaceholderTextColor;
			}
			set
			{
				if (m_inputFieldPlaceholderTextColor != value)
				{
					m_inputFieldPlaceholderTextColor = value;
					m_version++;
				}
			}
		}

		public Color InputFieldSelectedTextColor
		{
			get
			{
				return m_inputFieldSelectedTextColor;
			}
			set
			{
				if (m_inputFieldSelectedTextColor != value)
				{
					m_inputFieldSelectedTextColor = value;
					m_version++;
				}
			}
		}

		public Color InputFieldCaretColor
		{
			get
			{
				return m_inputFieldCaretColor;
			}
			set
			{
				if (m_inputFieldCaretColor != value)
				{
					m_inputFieldCaretColor = value;
					m_version++;
				}
			}
		}

		public Sprite InputFieldBackground
		{
			get
			{
				return m_inputFieldBackground;
			}
			set
			{
				if (m_inputFieldBackground != value)
				{
					m_inputFieldBackground = value;
					m_version++;
				}
			}
		}

		public Color ButtonColor
		{
			get
			{
				return m_buttonColor;
			}
			set
			{
				if (m_buttonColor != value)
				{
					m_buttonColor = value;
					m_version++;
				}
			}
		}

		public Color ButtonTextColor
		{
			get
			{
				return m_buttonTextColor;
			}
			set
			{
				if (m_buttonTextColor != value)
				{
					m_buttonTextColor = value;
					m_version++;
				}
			}
		}

		public Sprite ButtonBackground
		{
			get
			{
				return m_buttonBackground;
			}
			set
			{
				if (m_buttonBackground != value)
				{
					m_buttonBackground = value;
					m_version++;
				}
			}
		}

		public Color DropdownColor
		{
			get
			{
				return m_dropdownColor;
			}
			set
			{
				if (m_dropdownColor != value)
				{
					m_dropdownColor = value;
					m_version++;
				}
			}
		}

		public Color DropdownTextColor
		{
			get
			{
				return m_dropdownTextColor;
			}
			set
			{
				if (m_dropdownTextColor != value)
				{
					m_dropdownTextColor = value;
					m_version++;
				}
			}
		}

		public Color DropdownArrowColor
		{
			get
			{
				return m_dropdownArrowColor;
			}
			set
			{
				if (m_dropdownArrowColor != value)
				{
					m_dropdownArrowColor = value;
					m_version++;
				}
			}
		}

		public Color DropdownCheckmarkColor
		{
			get
			{
				return m_dropdownCheckmarkColor;
			}
			set
			{
				if (m_dropdownCheckmarkColor != value)
				{
					m_dropdownCheckmarkColor = value;
					m_version++;
				}
			}
		}

		public Sprite DropdownBackground
		{
			get
			{
				return m_dropdownBackground;
			}
			set
			{
				if (m_dropdownBackground != value)
				{
					m_dropdownBackground = value;
					m_version++;
				}
			}
		}

		public Sprite DropdownArrow
		{
			get
			{
				return m_dropdownArrow;
			}
			set
			{
				if (m_dropdownArrow != value)
				{
					m_dropdownArrow = value;
					m_version++;
				}
			}
		}

		public Sprite DropdownCheckmark
		{
			get
			{
				return m_dropdownCheckmark;
			}
			set
			{
				if (m_dropdownCheckmark != value)
				{
					m_dropdownCheckmark = value;
					m_version++;
				}
			}
		}

		public Color ToggleColor
		{
			get
			{
				return m_toggleColor;
			}
			set
			{
				if (m_toggleColor != value)
				{
					m_toggleColor = value;
					m_version++;
				}
			}
		}

		public Color ToggleTextColor
		{
			get
			{
				return m_toggleTextColor;
			}
			set
			{
				if (m_toggleTextColor != value)
				{
					m_toggleTextColor = value;
					m_version++;
				}
			}
		}

		public Color ToggleCheckmarkColor
		{
			get
			{
				return m_toggleCheckmarkColor;
			}
			set
			{
				if (m_toggleCheckmarkColor != value)
				{
					m_toggleCheckmarkColor = value;
					m_version++;
				}
			}
		}

		public Sprite ToggleBackground
		{
			get
			{
				return m_toggleBackground;
			}
			set
			{
				if (m_toggleBackground != value)
				{
					m_toggleBackground = value;
					m_version++;
				}
			}
		}

		public Sprite ToggleCheckmark
		{
			get
			{
				return m_toggleCheckmark;
			}
			set
			{
				if (m_toggleCheckmark != value)
				{
					m_toggleCheckmark = value;
					m_version++;
				}
			}
		}

		public Color ScrollbarBackgroundColor
		{
			get
			{
				return m_scrollbarBackgroundColor;
			}
			set
			{
				if (m_scrollbarBackgroundColor != value)
				{
					m_scrollbarBackgroundColor = value;
					m_version++;
				}
			}
		}

		public Color ScrollbarColor
		{
			get
			{
				return m_scrollbarColor;
			}
			set
			{
				if (m_scrollbarColor != value)
				{
					m_scrollbarColor = value;
					m_version++;
				}
			}
		}

		public float FileHeight
		{
			get
			{
				return m_fileHeight;
			}
			set
			{
				if (m_fileHeight != value)
				{
					m_fileHeight = value;
					m_version++;
				}
			}
		}

		public float FileIconsPadding
		{
			get
			{
				return m_fileIconsPadding;
			}
			set
			{
				if (m_fileIconsPadding != value)
				{
					m_fileIconsPadding = value;
					m_version++;
				}
			}
		}

		public Color FileNormalBackgroundColor
		{
			get
			{
				return m_fileNormalBackgroundColor;
			}
			set
			{
				if (m_fileNormalBackgroundColor != value)
				{
					m_fileNormalBackgroundColor = value;
					m_version++;
				}
			}
		}

		public Color FileAlternatingBackgroundColor
		{
			get
			{
				return m_fileAlternatingBackgroundColor;
			}
			set
			{
				if (m_fileAlternatingBackgroundColor != value)
				{
					m_fileAlternatingBackgroundColor = value;
					m_version++;
				}
			}
		}

		public Color FileHoveredBackgroundColor
		{
			get
			{
				return m_fileHoveredBackgroundColor;
			}
			set
			{
				if (m_fileHoveredBackgroundColor != value)
				{
					m_fileHoveredBackgroundColor = value;
					m_version++;
				}
			}
		}

		public Color FileSelectedBackgroundColor
		{
			get
			{
				return m_fileSelectedBackgroundColor;
			}
			set
			{
				if (m_fileSelectedBackgroundColor != value)
				{
					m_fileSelectedBackgroundColor = value;
					m_version++;
				}
			}
		}

		public Color FileNormalTextColor
		{
			get
			{
				return m_fileNormalTextColor;
			}
			set
			{
				if (m_fileNormalTextColor != value)
				{
					m_fileNormalTextColor = value;
					m_version++;
				}
			}
		}

		public Color FileSelectedTextColor
		{
			get
			{
				return m_fileSelectedTextColor;
			}
			set
			{
				if (m_fileSelectedTextColor != value)
				{
					m_fileSelectedTextColor = value;
					m_version++;
				}
			}
		}

		public Sprite FolderIcon
		{
			get
			{
				return m_folderIcon;
			}
			set
			{
				if (m_folderIcon != value)
				{
					m_folderIcon = value;
					m_version++;
				}
			}
		}

		public Sprite DriveIcon
		{
			get
			{
				return m_driveIcon;
			}
			set
			{
				if (m_driveIcon != value)
				{
					m_driveIcon = value;
					m_version++;
				}
			}
		}

		public Sprite DefaultFileIcon
		{
			get
			{
				return m_defaultFileIcon;
			}
			set
			{
				if (m_defaultFileIcon != value)
				{
					m_defaultFileIcon = value;
					m_version++;
				}
			}
		}

		public FiletypeIcon[] FiletypeIcons
		{
			get
			{
				return m_filetypeIcons;
			}
			set
			{
				if (m_filetypeIcons != value)
				{
					m_filetypeIcons = value;
					initializedFiletypeIcons = false;
					m_version++;
				}
			}
		}

		public bool AllIconExtensionsHaveSingleSuffix
		{
			get
			{
				if (!initializedFiletypeIcons)
				{
					InitializeFiletypeIcons();
				}
				return m_allIconExtensionsHaveSingleSuffix;
			}
		}

		public Sprite FileMultiSelectionToggleOffIcon
		{
			get
			{
				return m_fileMultiSelectionToggleOffIcon;
			}
			set
			{
				if (m_fileMultiSelectionToggleOffIcon != value)
				{
					m_fileMultiSelectionToggleOffIcon = value;
					m_version++;
				}
			}
		}

		public Sprite FileMultiSelectionToggleOnIcon
		{
			get
			{
				return m_fileMultiSelectionToggleOnIcon;
			}
			set
			{
				if (m_fileMultiSelectionToggleOnIcon != value)
				{
					m_fileMultiSelectionToggleOnIcon = value;
					m_version++;
				}
			}
		}

		public Color ContextMenuBackgroundColor
		{
			get
			{
				return m_contextMenuBackgroundColor;
			}
			set
			{
				if (m_contextMenuBackgroundColor != value)
				{
					m_contextMenuBackgroundColor = value;
					m_version++;
				}
			}
		}

		public Color ContextMenuTextColor
		{
			get
			{
				return m_contextMenuTextColor;
			}
			set
			{
				if (m_contextMenuTextColor != value)
				{
					m_contextMenuTextColor = value;
					m_version++;
				}
			}
		}

		public Color ContextMenuSeparatorColor
		{
			get
			{
				return m_contextMenuSeparatorColor;
			}
			set
			{
				if (m_contextMenuSeparatorColor != value)
				{
					m_contextMenuSeparatorColor = value;
					m_version++;
				}
			}
		}

		public Color PopupPanelsBackgroundColor
		{
			get
			{
				return m_popupPanelsBackgroundColor;
			}
			set
			{
				if (m_popupPanelsBackgroundColor != value)
				{
					m_popupPanelsBackgroundColor = value;
					m_version++;
				}
			}
		}

		public Color PopupPanelsTextColor
		{
			get
			{
				return m_popupPanelsTextColor;
			}
			set
			{
				if (m_popupPanelsTextColor != value)
				{
					m_popupPanelsTextColor = value;
					m_version++;
				}
			}
		}

		public Sprite PopupPanelsBackground
		{
			get
			{
				return m_popupPanelsBackground;
			}
			set
			{
				if (m_popupPanelsBackground != value)
				{
					m_popupPanelsBackground = value;
					m_version++;
				}
			}
		}

		[ContextMenu("Refresh UI")]
		private void Invalidate()
		{
			m_version = UnityEngine.Random.Range(-1073741824, 1073741823);
			initializedFiletypeIcons = false;
		}

		public void ApplyTo(TMP_Text text, Color textColor)
		{
			text.color = textColor;
			text.font = m_font;
			text.fontSize = m_fontSize;
		}

		public void ApplyTo(TMP_InputField inputField)
		{
			inputField.image.color = m_inputFieldNormalBackgroundColor;
			inputField.image.sprite = m_inputFieldBackground;
			inputField.selectionColor = m_inputFieldSelectedTextColor;
			inputField.caretColor = m_inputFieldCaretColor;
			ApplyTo(inputField.textComponent, m_inputFieldTextColor);
			if ((bool)(inputField.placeholder as TMP_Text))
			{
				ApplyTo((TMP_Text)inputField.placeholder, m_inputFieldPlaceholderTextColor);
			}
		}

		public void ApplyTo(Button button)
		{
			button.image.color = m_buttonColor;
			button.image.sprite = m_buttonBackground;
			ApplyTo(button.GetComponentInChildren<TMP_Text>(), m_buttonTextColor);
		}

		public void ApplyTo(TMP_Dropdown dropdown)
		{
			dropdown.image.color = m_dropdownColor;
			dropdown.image.sprite = m_dropdownBackground;
			dropdown.template.GetComponent<Image>().color = m_dropdownColor;
			Image component = dropdown.transform.Find("Arrow").GetComponent<Image>();
			component.color = m_dropdownArrowColor;
			component.sprite = m_dropdownArrow;
			ApplyTo(dropdown.captionText, m_dropdownTextColor);
			ApplyTo(dropdown.itemText, m_dropdownTextColor);
			RectTransform rectTransform = (RectTransform)dropdown.itemText.transform.parent;
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, m_rowHeight);
			rectTransform.Find("Item Background").GetComponent<Image>().color = m_dropdownColor;
			RectTransform obj = (RectTransform)rectTransform.parent;
			obj.sizeDelta = new Vector2(obj.sizeDelta.x, rectTransform.sizeDelta.y + 2f);
			Image component2 = rectTransform.Find("Item Checkmark").GetComponent<Image>();
			component2.color = m_dropdownCheckmarkColor;
			component2.sprite = m_dropdownCheckmark;
		}

		public void ApplyTo(Toggle toggle)
		{
			toggle.image.color = m_toggleColor;
			toggle.image.sprite = m_toggleBackground;
			toggle.graphic.color = m_toggleCheckmarkColor;
			((Image)toggle.graphic).sprite = m_toggleCheckmark;
			ApplyTo(toggle.GetComponentInChildren<TMP_Text>(), m_toggleTextColor);
		}

		public void ApplyTo(Scrollbar scrollbar)
		{
			scrollbar.GetComponent<Image>().color = m_scrollbarBackgroundColor;
			scrollbar.image.color = m_scrollbarColor;
		}

		public Sprite GetIconForFileEntry(in FileSystemEntry fileInfo, bool extensionMayHaveMultipleSuffixes)
		{
			if (!initializedFiletypeIcons)
			{
				InitializeFiletypeIcons();
			}
			if (fileInfo.IsDirectory)
			{
				return m_folderIcon;
			}
			if (filetypeToIcon.TryGetValue(fileInfo.Extension, out var value))
			{
				return value;
			}
			if (extensionMayHaveMultipleSuffixes)
			{
				for (int i = 0; i < m_filetypeIcons.Length; i++)
				{
					if (fileInfo.Extension.EndsWith(m_filetypeIcons[i].extension, StringComparison.Ordinal))
					{
						filetypeToIcon[fileInfo.Extension] = m_filetypeIcons[i].icon;
						return m_filetypeIcons[i].icon;
					}
				}
			}
			filetypeToIcon[fileInfo.Extension] = m_defaultFileIcon;
			return m_defaultFileIcon;
		}

		private void InitializeFiletypeIcons()
		{
			initializedFiletypeIcons = true;
			if (filetypeToIcon == null)
			{
				filetypeToIcon = new Dictionary<string, Sprite>(128);
			}
			else
			{
				filetypeToIcon.Clear();
			}
			m_allIconExtensionsHaveSingleSuffix = true;
			for (int i = 0; i < m_filetypeIcons.Length; i++)
			{
				m_filetypeIcons[i].extension = m_filetypeIcons[i].extension.ToLowerInvariant();
				if (m_filetypeIcons[i].extension[0] != '.')
				{
					m_filetypeIcons[i].extension = "." + m_filetypeIcons[i].extension;
				}
				filetypeToIcon[m_filetypeIcons[i].extension] = m_filetypeIcons[i].icon;
				m_allIconExtensionsHaveSingleSuffix &= m_filetypeIcons[i].extension.LastIndexOf('.') == 0;
			}
		}
	}
}
