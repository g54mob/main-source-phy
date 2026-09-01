using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Landfall.TABC;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using ModIO;
using ModIO.API;
using ModIO.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DMConfigurePanel : MonoBehaviour
{
	private const string LocalizedSaveFailedText = "POPUP_SAVEFAILED";

	private const string LocalizedDeletionFailedText = "POPUP_DELETIONFAILED";

	[SerializeField]
	private DMWorkshopHandler m_workshopHandler;

	[SerializeField]
	private ModView m_inspectorModView;

	[SerializeField]
	private ExplorerView m_explorerView;

	[SerializeField]
	private TMP_InputField m_modName;

	[SerializeField]
	private TMP_InputField m_modDescription;

	[SerializeField]
	private GameTagCategoryDisplay m_tagContainer;

	[SerializeField]
	private ToggleEvent m_visibilityToggle;

	private List<string> m_selectedTags = new List<string>();

	private ModVisibility m_visibility;

	private bool m_modDescriptionDirty;

	private bool m_visibilityDirty;

	[SerializeField]
	private GameObject m_previewItem;

	[SerializeField]
	private LocalizeText m_infoText;

	[SerializeField]
	private CanvasGroup explorerView;

	private ModalPanel m_modalPanel;

	private bool m_updateQueued;

	private void Awake()
	{
		m_modalPanel = ServiceLocator.GetService<ModalPanel>();
	}

	public void Open()
	{
		Refresh(m_inspectorModView.profile);
	}

	public void Close()
	{
		if (!m_updateQueued)
		{
			m_updateQueued = true;
			StartCoroutine(WaitUntilBrowserIsOpen());
		}
		IEnumerator WaitUntilBrowserIsOpen()
		{
			yield return new WaitUntil(() => explorerView.interactable);
			explorerView.GetComponent<ExplorerView>().Refresh();
			m_updateQueued = false;
		}
	}

	private void Refresh(ModProfile modProfile)
	{
		m_modName.text = modProfile.name;
		m_modDescription.text = modProfile.summary;
		RefreshTags(modProfile);
		m_visibilityToggle.SetState(modProfile.visibility == ModVisibility.Public);
		ImageRequestManager.instance.RequestModLogo(modProfile.id, modProfile.logoLocator, LogoSize.Thumbnail_640x360, UpdatePreviewWithSprite, UpdatePreviewWithSprite, WebRequestError.LogAsWarning);
		m_visibilityDirty = false;
		m_modDescriptionDirty = false;
		m_infoText.Args = new string[1] { "\n" };
		m_infoText.LocaleID = "LABEL_CONFIGUREINFO";
		void UpdatePreviewWithSprite(Texture2D tex)
		{
			UpdatePreviewObject(m_previewItem, modProfile.name, UIUtilities.CreateSpriteFromTexture(tex));
		}
	}

	private void RefreshTags(ModProfile modProfile)
	{
		m_selectedTags = modProfile.tagNames.ToList();
		m_tagContainer.UpdateTagStates(m_selectedTags);
	}

	public void ToggleTag(GameObject tagCell)
	{
		string text = tagCell.GetComponentInChildren<TMP_Text>().text;
		if (!m_selectedTags.Contains(text))
		{
			m_selectedTags.Add(text);
		}
		else
		{
			m_selectedTags.Remove(text);
		}
	}

	public void ToggleVisibility(bool enabled)
	{
		m_visibility = (enabled ? ModVisibility.Public : ModVisibility.Hidden);
		m_visibilityDirty = m_visibility != m_inspectorModView.profile.visibility;
	}

	public void OnDescriptionChanged(string input)
	{
		m_modDescriptionDirty = input != m_inspectorModView.profile.summary;
	}

	private void UpdatePreviewObject(GameObject previewObject, string name, Sprite icon, WorkshopContentType contentType = WorkshopContentType.Any)
	{
		previewObject.GetComponentInChildren<TMP_Text>().text = name;
		DMModItemImageFitter componentInChildren = previewObject.GetComponentInChildren<DMModItemImageFitter>();
		componentInChildren.UpdateAspectRatio(contentType.ToString());
		componentInChildren.GetComponentInChildren<Image>().sprite = icon;
	}

	private bool ValidateChanges()
	{
		if (string.IsNullOrEmpty(m_modName.text))
		{
			return false;
		}
		if (string.IsNullOrEmpty(m_modDescription.text))
		{
			return false;
		}
		return true;
	}

	public void SaveChanges()
	{
		if (!ValidateChanges())
		{
			m_modalPanel.PopUp("POPUP_EMPTYDESCRIPTION");
			return;
		}
		m_modalPanel.WaitPopUp("POPUP_SAVING", -1f, null, null);
		EditableModProfile modEdits = EditableModProfile.CreateFromProfile(m_inspectorModView.profile);
		modEdits.summary.value = m_modDescription.text;
		modEdits.summary.isDirty = m_modDescriptionDirty;
		modEdits.visibility.value = m_visibility;
		modEdits.visibility.isDirty = m_visibilityDirty;
		Action<WebRequestError> onError = delegate(WebRequestError e)
		{
			m_modalPanel.CloseWaitPopup();
			m_modalPanel.PopUp("POPUP_SAVEFAILED", Localizer.GetSinglePhrase(e.displayMessage));
			Debug.Log(e.errorMessage);
		};
		List<string> tags = new List<string>();
		APIClient.GetGameTagOptions(delegate(RequestPage<ModTagCategory> cat)
		{
			string[] tags2 = cat.items[0].tags;
			foreach (string text in tags2)
			{
				if (modEdits.tags.value.Contains(text))
				{
					tags.Add(text);
				}
			}
			List<string> list = new List<string>();
			foreach (TagContainerItem tagItem in m_tagContainer.tagItems)
			{
				string tagName = tagItem.TagName;
				if (tagItem.GetComponentInChildren<Toggle>().isOn)
				{
					tags.Add(tagName);
				}
				else
				{
					list.Add(tagName);
				}
			}
			APIClient.DeleteModTags(m_inspectorModView.profile.id, new DeleteModTagsParameters
			{
				tagNames = list.ToArray()
			}, delegate
			{
				APIClient.AddModTags(m_inspectorModView.profile.id, new AddModTagsParameters
				{
					tagNames = tags.ToArray()
				}, delegate
				{
					ModManager.SubmitModChanges(m_inspectorModView.profile.id, modEdits, Finish, onError);
				}, onError);
			}, onError);
		}, onError);
		void Finish(ModProfile profile)
		{
			m_modalPanel.CloseWaitPopup();
			m_modalPanel.PopUp("POPUP_SAVED", delegate
			{
				m_explorerView.ClearCacheAndRefresh();
				m_inspectorModView.profile = profile;
				m_workshopHandler.Back();
			});
		}
	}

	public void DeletionCheck()
	{
		m_modalPanel.Choice("POPUP_DELETE_TITLE", "POPUP_DELETIONCHECK_TEXT", delegate
		{
			DeleteMod();
		}, null, "BUTTON_DELETE", "BUTTON_CANCEL", true, m_inspectorModView.profile.name, "\n");
	}

	public void DeleteMod()
	{
		Action action = delegate
		{
			m_modalPanel.WaitPopUp("POPUP_DELETING", -1f, null, null);
			APIClient.DeleteMod(m_inspectorModView.profile.id, delegate
			{
				m_modalPanel.CloseWaitPopup();
				m_modalPanel.PopUp("POPUP_DELETED", delegate
				{
					m_explorerView.ClearCacheAndRefresh();
					m_workshopHandler.Back(2);
				});
			}, delegate(WebRequestError e)
			{
				m_modalPanel.CloseWaitPopup();
				m_modalPanel.PopUp("POPUP_DELETIONFAILED", Localizer.GetSinglePhrase(e.displayMessage));
				Debug.Log(e.errorMessage);
			});
		};
		if (LocalUser.SubscribedModIds.Contains(m_inspectorModView.profile.id))
		{
			APIClient.UnsubscribeFromMod(m_inspectorModView.profile.id, action, WebRequestError.LogAsWarning);
		}
		else
		{
			action();
		}
	}
}
