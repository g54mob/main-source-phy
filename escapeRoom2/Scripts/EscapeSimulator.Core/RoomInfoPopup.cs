using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RoomInfoPopupUI))]
public class RoomInfoPopup : MonoBehaviour
{
	public Action onClosePopup;

	public Action onDownloadRoom;

	public Action onDeleteRoom;

	public Action onPlayRoom;

	public LevelProvider.LevelItem roomInfo;

	private ESUGC esUgc;

	private RoomInfoPopupUI roomInfoUI;

	private PineTweenSystem tween = new PineTweenSystem();

	private ScrollRect scrollRect;

	private float wantedScrollPosition;

	private bool isFromCoopWorkshop;

	private List<(string, string)> difficultyTagsList = new List<(string, string)>
	{
		("Easy", "tagEasy"),
		("Medium", "tagMedium"),
		("Hard", "tagHard")
	};

	private List<(string, string)> playTimeTagsList = new List<(string, string)>
	{
		("5 minutes", "tag5minutes"),
		("15 minutes", "tag15minutes"),
		("30 minutes", "tag30minutes"),
		("45 minutes", "tag45minutes"),
		("45+ minutes", "tag45Moreminutes")
	};

	private List<(string, string)> numOfPlayersTagsList = new List<(string, string)>
	{
		("Singleplayer", "tagSingleplayer"),
		("2 player only", "tag2PlayerOnly"),
		("2 or more players", "tag2OrMorePlayers"),
		("3 or more players", "tag3OrMorePlayers"),
		("4 or more players", "tag4OrMorePlayers"),
		("5 or more players", "tag5OrMorePlayers")
	};

	private List<(string, string)> themesTagsList = new List<(string, string)>
	{
		("Historic", "tagHistoric"),
		("Modern", "tagModern"),
		("Futuristic", "tagFuturistic"),
		("Horror", "tagHorror"),
		("Adventure", "tagAdventure"),
		("Mystery", "tagMystery"),
		("Meme", "tagMeme"),
		("Other", "tagOther")
	};

	private List<(string, string)> languagesTagsList = new List<(string, string)>
	{
		("English", "English"),
		("简体中文 (Simplified Chinese)", "简体中文 (Simplified Chinese)"),
		("繁體中文 (Traditional Chinese)", "繁體中文 (Traditional Chinese)"),
		("日本語 (Japanese)", "日本語 (Japanese)"),
		("한국어 (Korean)", "한국어 (Korean)"),
		("Deutsch (German)", "Deutsch (German)"),
		("Francais (French)", "Francais (French)"),
		("Türk (Turkish)", "Türk (Turkish)"),
		("Other language", "tagOtherLanguages")
	};

	private List<(string, string)> miscellaneousTagsList = new List<(string, string)>
	{
		("External knowledge required", "tagExternalKnowledgeRequired"),
		("Sound required", "tagSoundRequired"),
		("Reading required", "tagReadingRequired"),
		("Colorblind frendly", "tagColorblindFrendly"),
		("Has profanity", "tagHasProfanity")
	};

	private void onButtonClick(Button button)
	{
		if (button == roomInfoUI.Info_DeleteButton)
		{
			activateDeleteModal(activate: true);
		}
		else if (button == roomInfoUI.DeleteRoomModal_no)
		{
			activateDeleteModal(activate: false);
		}
		else if (button == roomInfoUI.DeleteRoomModal_yes)
		{
			deleteRoom();
		}
		else if (button == roomInfoUI.Info_PlayButton && hasDownloadedInfo())
		{
			onPlayRoom?.Invoke();
		}
		else if (button == roomInfoUI.Info_DownloadButton)
		{
			downloadRoom();
		}
	}

	private bool hasDownloadedInfo()
	{
		return roomInfo != null;
	}

	private void deleteRoom()
	{
		if (hasDownloadedInfo())
		{
			onDeleteRoom?.Invoke();
		}
		activateDeleteModal(activate: false);
		deactivateRoomInfo();
	}

	private void downloadRoom()
	{
		onDownloadRoom?.Invoke();
	}

	public void init(ESUGC esUgc)
	{
		this.esUgc = esUgc;
		roomInfoUI = GetComponent<RoomInfoPopupUI>();
		PineUI.addButtonClickDelegate(onButtonClick);
		deactivateRoomInfo();
	}

	public void updateRoomState()
	{
		tween.processFixedUpdateTweens(Time.deltaTime);
		if (roomInfo != null)
		{
			CustomRoomState customRoomState = esUgc.customRoomState(roomInfo.id);
			bool flag = customRoomState == CustomRoomState.Installed;
			if (roomInfoUI.Info_LevelImage.texture == null && roomInfo.texture != null)
			{
				roomInfoUI.Info_LevelImage.texture = roomInfo.texture;
			}
			roomInfoUI.Info_PlayButton.gameObject.SetActive(!Controller.isActive() && !isFromCoopWorkshop && flag);
			roomInfoUI.Info_DeleteButton.gameObject.SetActive(!Controller.isActive() && flag);
			roomInfoUI.Info_DownloadButton.gameObject.SetActive(!Controller.isActive() && customRoomState == CustomRoomState.NotInstalled);
			roomInfoUI.Info_PlayControllerHint.gameObject.SetActive(Controller.isActive() && !isFromCoopWorkshop && flag);
			roomInfoUI.Info_DeleteControllerHint.gameObject.SetActive(Controller.isActive() && flag);
			roomInfoUI.Info_DownloadControllerHint.gameObject.SetActive(Controller.isActive() && customRoomState == CustomRoomState.NotInstalled);
		}
	}

	public void Update()
	{
		if (Controller.isActive())
		{
			roomInfoUI.Info_DescriptionScrollView.verticalNormalizedPosition += Controller.getAxis(ControllerAxisActionType.UIMoveAndRotate).y * Time.deltaTime * 2f;
		}
	}

	public void handleInput(bool confirmPressed, bool deletePressed, bool cancelPressed, float scrollInput)
	{
		if (roomInfoUI.DeleteRoomModal.gameObject.activeInHierarchy)
		{
			if (confirmPressed)
			{
				deleteRoom();
			}
			else if (cancelPressed)
			{
				activateDeleteModal(activate: false);
			}
		}
		else
		{
			if (!isActive())
			{
				return;
			}
			CustomRoomState customRoomState = esUgc.customRoomState(roomInfo.id);
			if (confirmPressed && roomInfo != null)
			{
				if (customRoomState == CustomRoomState.Installed && hasDownloadedInfo())
				{
					if (!isFromCoopWorkshop)
					{
						onPlayRoom?.Invoke();
					}
				}
				else if (customRoomState == CustomRoomState.NotInstalled || customRoomState == CustomRoomState.NeedsUpdate)
				{
					downloadRoom();
				}
			}
			else if (deletePressed && hasDownloadedInfo())
			{
				activateDeleteModal(activate: true);
			}
			else if (cancelPressed)
			{
				deactivateRoomInfo();
			}
			else if (Controller.isActive() && scrollInput != 0f && roomInfoUI.Info_DescriptionScrollView_ScrollbarVertical.gameObject.activeSelf)
			{
				wantedScrollPosition = Mathf.Clamp01(wantedScrollPosition + scrollInput * 0.1f);
			}
			if (Controller.isActive())
			{
				scrollRect.verticalNormalizedPosition = Mathf.MoveTowards(scrollRect.verticalNormalizedPosition, wantedScrollPosition, 0.1f);
			}
		}
	}

	public void activateRoomInfo(LevelProvider.LevelItem room, bool isFromCoop)
	{
		isFromCoopWorkshop = isFromCoop;
		roomInfo = room;
		roomInfoUI.gameObject.SetActive(value: true);
		roomInfoUI.Info_LevelName.text = room.levelName;
		roomInfoUI.Info_AuthorName.text = Localization.translate("%madeByAuthor%").Replace("[AUTHOR]", room.creator);
		roomInfoUI.Info_DiskSize.text = getTextFileSize(room.size);
		roomInfoUI.Info_Difficulty.gameObject.SetActive(value: false);
		roomInfoUI.Info_Playtime.gameObject.SetActive(value: false);
		roomInfoUI.Info_NumOfPlayers.gameObject.SetActive(value: false);
		roomInfoUI.Info_Language.gameObject.SetActive(value: false);
		roomInfoUI.Info_Themes.gameObject.SetActive(value: false);
		if (!string.IsNullOrEmpty(room.tags))
		{
			roomInfoUI.Info_Difficulty.text = "";
			roomInfoUI.Info_Playtime.text = "";
			roomInfoUI.Info_NumOfPlayers.text = "";
			roomInfoUI.Info_Language.text = "";
			roomInfoUI.Info_Themes.text = "";
			string[] array = room.tags.Split(';');
			foreach (string potentialTag in array)
			{
				setTagText("Difficulty", roomInfoUI.Info_Difficulty, difficultyTagsList, potentialTag);
				setTagText("PlayTime", roomInfoUI.Info_Playtime, playTimeTagsList, potentialTag);
				setTagText("NumberOfPlayers", roomInfoUI.Info_NumOfPlayers, numOfPlayersTagsList, potentialTag);
				setTagText("tagTitleThemes", roomInfoUI.Info_Themes, themesTagsList, potentialTag);
				setTagText("tagTitleLanguages", roomInfoUI.Info_Language, languagesTagsList, potentialTag, isLanguage: true);
			}
			string text = "</i>";
			roomInfoUI.Info_Difficulty.text += text;
			roomInfoUI.Info_Playtime.text += text;
			roomInfoUI.Info_NumOfPlayers.text += text;
			roomInfoUI.Info_Language.text += text;
			roomInfoUI.Info_Themes.text += text;
		}
		string text2 = "...";
		if (!string.IsNullOrEmpty(room.description))
		{
			text2 = room.description;
		}
		wantedScrollPosition = 1f;
		scrollRect.verticalNormalizedPosition = 1f;
		roomInfoUI.Info_DescriptionScrollView_Viewport_Content_Decription.text = text2;
		roomInfoUI.Info_LevelImage.texture = room.texture;
		updateRoomState();
		roomInfoUI.Info.gameObject.SetActive(value: true);
		static void setTagText(string tagLabel, Text text4, List<(string, string)> tagList, string text5, bool isLanguage = false)
		{
			int num = tagList.FindIndex(((string, string) x) => x.Item1 == text5);
			if (num < 0)
			{
				num = tagList.FindIndex(((string, string) x) => x.Item2 == text5);
			}
			if (num >= 0)
			{
				bool flag = !isLanguage;
				if (isLanguage)
				{
					flag = tagList[num].Item2 == "tagOtherLanguages";
				}
				string text3 = text4.text;
				text3 = ((!string.IsNullOrEmpty(text3)) ? (text3 + ", ") : ("<color=#94A0AB>" + Localization.lookupInDictionary(tagLabel) + ":</color> <i>"));
				text3 = ((!flag) ? (text3 + tagList[num].Item1) : (text3 + Localization.lookupInDictionary(tagList[num].Item2)));
				text4.text = text3;
				text4.gameObject.SetActive(value: true);
			}
		}
	}

	public static string getTextFileSize(string rawFileSize)
	{
		string result = "";
		if (!string.IsNullOrEmpty(rawFileSize))
		{
			float num = (float)int.Parse(rawFileSize) / 1024f;
			result = ((!(num < 1024f)) ? ((num / 1024f).ToString("F2") + " MB") : (num.ToString("F1") + " KB"));
		}
		return result;
	}

	public void deactivateRoomInfo()
	{
		roomInfo = null;
		roomInfoUI.Info.gameObject.SetActive(value: false);
		onClosePopup?.Invoke();
	}

	public void activateDeleteModal(bool activate)
	{
		roomInfoUI.DeleteRoomModal.gameObject.SetActive(activate);
	}

	public bool isActive()
	{
		if (roomInfoUI != null)
		{
			return roomInfoUI.Info.gameObject.activeInHierarchy;
		}
		return false;
	}
}
