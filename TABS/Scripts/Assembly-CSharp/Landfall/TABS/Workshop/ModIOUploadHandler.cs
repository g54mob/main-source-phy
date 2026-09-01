using System;
using System.Collections.Generic;
using DM;
using ModIO;
using TFBGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

namespace Landfall.TABS.Workshop
{
	public class ModIOUploadHandler
	{
		private ModIOModCreator m_WorkshopLevelCreator = new ModIOModCreator();

		private static readonly ModIOUploadHandler _instance = new ModIOUploadHandler();

		private static int m_InternalIndex;

		private static int m_WantedIndex;

		private static List<GenericCustomContentWrapper> m_ContentToUpload;

		private static UnityAction m_OnDoneAction;

		public static ModIOUploadHandler Instance => _instance;

		public static bool UploadingSeqence { get; private set; }

		[Obsolete]
		public static void UploadSeveralItems(List<GenericCustomContentWrapper> unitsToUpload, UnityAction onDoneAction)
		{
			Debug.Log("Beginning uploading several Workshop Items: " + unitsToUpload.Count + " Time: " + Time.time);
			UploadingSeqence = true;
			m_InternalIndex = 0;
			m_WantedIndex = unitsToUpload.Count;
			m_ContentToUpload = unitsToUpload;
			m_OnDoneAction = onDoneAction;
			OnUploadSequence();
		}

		public static void CreateNewItem(List<GenericCustomContentWrapper> contentToUpload, string title, List<string> tags, string description, ContentTypeFilter contentType, ModVisibility visibility, SpriteAtlas factionAtlas, Action doneCallback)
		{
			if (_instance == null || _instance.m_WorkshopLevelCreator == null)
			{
				doneCallback?.Invoke();
			}
			else
			{
				_instance.m_WorkshopLevelCreator.CreateNewMod(contentToUpload, title, tags, description, contentType, visibility, factionAtlas, doneCallback);
			}
		}

		public static void UpdateExistingItem(int modID, GenericCustomContentWrapper[] contentToUpload, string title, string description)
		{
			_instance.m_WorkshopLevelCreator.UpdateMod(modID, contentToUpload, title, description, updatingExisting: true);
		}

		public static void SetOnCreateItemAction(Action a)
		{
			_instance.m_WorkshopLevelCreator.OnItemCreatedAction(a);
		}

		public static void SetOnItemUpdatedAction(Action a)
		{
			_instance.m_WorkshopLevelCreator.OnItemUpdatedAction(a);
		}

		private static void OnUnitCreated(int id)
		{
			Debug.Log("Unit Created: " + id);
			if (m_InternalIndex > 0)
			{
				switch (m_ContentToUpload[m_InternalIndex - 1].ContentType)
				{
				}
			}
		}

		[Obsolete]
		private static void OnUploadSequence()
		{
			if (m_InternalIndex >= m_ContentToUpload.Count)
			{
				SequenceDone();
				return;
			}
			Debug.Log("OnUpLoadSequence Index: " + m_InternalIndex);
			GenericCustomContentWrapper curr = m_ContentToUpload[m_InternalIndex++];
			_instance.m_WorkshopLevelCreator.Reset();
			_instance.m_WorkshopLevelCreator.OnItemCreatedAction(delegate
			{
				OnUnitCreated(_instance.m_WorkshopLevelCreator.GetModID());
			});
			_instance.m_WorkshopLevelCreator.OnItemUpdatedAction(delegate
			{
				switch (curr.ContentType)
				{
				case WorkshopContentType.Layout:
				case WorkshopContentType.Battle:
					ContentDatabase.Instance().GetCampaignLevel(curr.ID).SetCustomIDButOnlySometimes(_instance.m_WorkshopLevelCreator.GetModID());
					break;
				}
				ServiceLocator.GetService<FileIOWrapper>().DeleteDirectory(curr.DirectoryPath, recursive: true, FileHandlingFileType.CustomContentOrLocalStorageFile, null);
				OnUploadSequence();
			});
		}

		private static void SequenceDone()
		{
			_instance.m_WorkshopLevelCreator.Reset();
			m_OnDoneAction?.Invoke();
			Reset();
			Debug.Log("Upload Sequence Done! Time: " + Time.time);
		}

		private static void Reset()
		{
			UploadingSeqence = false;
			m_InternalIndex = -1;
			m_WantedIndex = -1;
			m_ContentToUpload = null;
			m_OnDoneAction = null;
		}
	}
}
