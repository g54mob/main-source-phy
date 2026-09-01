using System;
using System.Collections.Generic;
using System.IO;
using DM;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class SaveMenu : DMUIPanel
	{
		[SerializeField]
		private Transform m_thumbnailParent;

		[SerializeField]
		private SaveMenuThumbnailTemplate m_thumbnailTemplate;

		[SerializeField]
		private Transform m_thumbnailContainer;

		[SerializeField]
		private Transform m_thumbnailHeader;

		[SerializeField]
		private Transform m_thumbnailHeaderDivider;

		[SerializeField]
		private TMP_InputField m_levelNameInputField;

		[SerializeField]
		private GameObject m_overwritePanel;

		private SaveMenuThumbnailTemplate m_selectedThumbnail;

		public static string PreservedNameInput;

		public event System.Action OnOpenSaveMenu;

		private void AssertionCheck()
		{
		}

		private void Start()
		{
			AssertionCheck();
			PlayerActions instance = PlayerActions.Instance;
			m_inputState.AddOnKeyDownListener(instance.m_back, delegate
			{
				DMUIManager.Instance.PopPanel();
			});
			m_inputState.AddOnKeyDownListener(instance.m_enterExitBattle, delegate
			{
				DMUIManager.Instance.PopPanel();
			});
			m_inputState.AddOnKeyDownListener(instance.m_addThumbnail, delegate
			{
				OpenScreenshotTool();
			});
		}

		public override void OnOpen()
		{
			this.OnOpenSaveMenu?.Invoke();
			base.OnOpen();
			BuildThumbnails();
			m_levelNameInputField.text = PreservedNameInput;
		}

		public override void OnClose()
		{
			base.OnClose();
			PreservedNameInput = string.Empty;
			m_levelNameInputField.text = string.Empty;
		}

		private void BuildThumbnails()
		{
			Utility.DestroyChildren(m_thumbnailParent);
			new List<Action<Texture>>();
			foreach (Texture2D screenshot in ScreenshotTool.Screenshots)
			{
				SaveMenuThumbnailTemplate thumbnailObj = UnityEngine.Object.Instantiate(m_thumbnailTemplate, m_thumbnailParent);
				thumbnailObj.gameObject.SetActive(value: true);
				thumbnailObj.m_thumbnail = screenshot;
				thumbnailObj.GetComponent<Button>().onClick.AddListener(delegate
				{
					SelectThumbnail(thumbnailObj);
				});
				RawImage img = thumbnailObj.GetComponentInChildren<RawImage>();
				img.color = new Color(1f, 1f, 1f, 0f);
				img.texture = screenshot;
				LeanTween.value(img.gameObject, delegate(Color col)
				{
					img.color = col;
				}, img.color, Color.white, 0.15f);
			}
			Utility.DelayAction(this, delegate
			{
				m_thumbnailParent.GetComponentInParent<ScrollRect>().horizontalScrollbar.value = 0f;
				if (m_thumbnailParent.childCount > 0)
				{
					Transform child = m_thumbnailParent.GetChild(0);
					SelectThumbnail(child.GetComponent<SaveMenuThumbnailTemplate>());
				}
			});
		}

		private void SelectThumbnail(SaveMenuThumbnailTemplate templateComponent)
		{
			if (m_selectedThumbnail != null)
			{
				m_selectedThumbnail.Deselect();
			}
			m_selectedThumbnail = templateComponent;
			if (m_selectedThumbnail != null)
			{
				m_selectedThumbnail.Select();
				m_selectedThumbnail.GetComponent<Button>().Select();
			}
		}

		public void SaveLevel(bool isOverwritePanel)
		{
			if (m_selectedThumbnail == null || m_selectedThumbnail.m_thumbnail == null)
			{
				MessageDisplay.DisplayMessage("LC_NO_THUMBNAIL_SELECTED");
				return;
			}
			if (string.IsNullOrEmpty(m_levelNameInputField.text))
			{
				MessageDisplay.DisplayMessage("LC_ENTER_A_MAP_NAME");
				return;
			}
			string empty = string.Empty;
			CustomMap existingMap = ContentDatabase.Instance().GetUserMapByExactNameAndType(m_levelNameInputField.text, WorkshopTypeFilter.Local);
			empty = Path.Combine(Paths.PlayerLevelDirectory, m_levelNameInputField.text, m_levelNameInputField.text + ".tld");
			if (!isOverwritePanel && existingMap != null)
			{
				ServiceLocator.GetService<ModalPanel>().Choice("LC_MAP_SAVE_OVERWRITE_POPUP_HEADER", "LC_MAP_SAVE_OVERWRITE_POPUP_QUESTION", delegate
				{
					BattleCreatorSharedCommands.DeleteContentFolder(new CustomContentDataPackage(existingMap.Entity.GUID, existingMap.FolderPath, ContentTypeFilter.Maps), delegate
					{
						SaveLevel(isOverwritePanel: true);
					});
				}, null);
				return;
			}
			Texture2D thumbnail = null;
			if (m_selectedThumbnail != null)
			{
				thumbnail = m_selectedThumbnail.m_thumbnail;
			}
			DMEditor.Instance.SaveLevel(empty, m_levelNameInputField.text, thumbnail);
			LeanTween.delayedCall(0.2f, (System.Action)delegate
			{
				PopUp.CreatePopUp(Vector3.zero, "LC_MAP_SAVED_POPUP", demandFocus: false, 1f).Show();
			});
			DMUIManager.Instance.PopPanel();
		}

		public void OpenScreenshotTool()
		{
			ScreenshotTool.PreservedLevelName = m_levelNameInputField.text;
			DMUIManager.Instance.PopAll();
			ToolTableRow rowValue = DMEditor.Instance.toolTable.GetRowValue("424af5ce-3dc6-4d98-957f-08025b47aee9");
			DMEditor.Instance.SwitchAction(rowValue);
		}
	}
}
