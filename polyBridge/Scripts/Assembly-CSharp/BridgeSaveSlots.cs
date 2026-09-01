using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Sirenix.Serialization;
using UnityEngine;

public class BridgeSaveSlots
{
	public static List<BridgeSaveSlotData> m_Slots = new List<BridgeSaveSlotData>();

	public static int CURRENT_VERSION = 3;

	public static readonly string SAVE_DIRECTORY = "SaveSlots";

	public static readonly string SAVE_EXTENSION = ".slot";

	public static readonly string AUTOSAVE_SLOT_NAME = "Auto-Save";

	public static readonly string BUDGET_SLOT_NAME = "Lowest Budget";

	public static readonly string BUDGET_PERFECTION_SLOT_NAME = "Lowest Budget (No Breaks)";

	public static readonly string LOWEST_STRESS_SLOT_NAME = "Lowest Stress (Under Budget)";

	public static int NUM_RESERVED_SLOTS = 10;

	private static byte[] m_LastThumbnail;

	private static FileSlot m_SlotToDelete;

	private static FileSlot m_SlotToRename;

	private static string m_SlotRenameOldName;

	private static string m_SlotRenameNewName;

	private static string m_LastSlotNameSaved;

	private static string m_LastSlotNameSavedDirectory;

	public static string GetDirectoryForSaveSlot()
	{
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN)
		{
			return Path.GetFileNameWithoutExtension(Campaign.GetCurrentLayoutFilename());
		}
		if (GameManager.GetGameMode() == GameMode.WORKSHOP && Workshop.m_LastPlayedWorkshopItem != null)
		{
			return Utils.GenerateCaseInsenstiveString(Workshop.m_LastPlayedWorkshopItem.GetId());
		}
		return Sandbox.GetCurrentLayoutName();
	}

	public static bool Save(string directory, BridgeSaveSlotData slotData)
	{
		try
		{
			byte[] array = SerializationUtility.SerializeValue(slotData, DataFormat.Binary);
			if (array.Length == 0)
			{
				return false;
			}
			return Write(directory, slotData.m_SlotFilename, array);
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Failed to save bridge save slot '{0}' due to exception {1}", ex.Message.ToString());
			return false;
		}
	}

	public static BridgeSaveSlotData Load(string pathAndFilename)
	{
		try
		{
			if (!File.Exists(pathAndFilename))
			{
				return null;
			}
			byte[] array = Utils.ReadAllBytes(pathAndFilename);
			if (array.Length == 0)
			{
				Debug.LogWarningFormat("Loaded bridge save slot '{0}' that is zero bytes", pathAndFilename);
				return null;
			}
			return SerializationUtility.DeserializeValue<BridgeSaveSlotData>(array, DataFormat.Binary);
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Caught exception '{0}' trying load '{1}'", ex.Message.ToString(), pathAndFilename);
			return null;
		}
	}

	public static List<BridgeSaveSlotData> LoadSlots(string directory, string profileName)
	{
		string path = Path.Combine(GetSavePath(profileName), directory);
		if (!Directory.Exists(path))
		{
			return null;
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(path);
		if (directoryInfo == null)
		{
			return null;
		}
		FileInfo[] files = directoryInfo.GetFiles("*" + SAVE_EXTENSION);
		foreach (FileInfo fileInfo in files)
		{
			BridgeSaveSlotData bridgeSaveSlotData = Load(fileInfo.FullName);
			if (bridgeSaveSlotData != null)
			{
				bridgeSaveSlotData.m_SlotFilename = Path.GetFileName(fileInfo.FullName);
				if (bridgeSaveSlotData.m_SlotID < NUM_RESERVED_SLOTS)
				{
					bridgeSaveSlotData.m_DisplayName = GetLocalizedSlotName((ReservedSlot)bridgeSaveSlotData.m_SlotID);
				}
				else
				{
					bridgeSaveSlotData.m_DisplayName = Path.GetFileNameWithoutExtension(bridgeSaveSlotData.m_SlotFilename);
				}
				if ((bridgeSaveSlotData.m_SlotID >= NUM_RESERVED_SLOTS || !SlotExistsWithId(bridgeSaveSlotData.m_SlotID)) && bridgeSaveSlotData != null)
				{
					bridgeSaveSlotData.m_LastWriteTimeTicks = fileInfo.LastWriteTime.Ticks;
					m_Slots.Add(bridgeSaveSlotData);
				}
			}
		}
		m_Slots.Sort(SortBySlotID);
		return m_Slots;
	}

	public static void ClearSlots()
	{
		m_Slots.Clear();
	}

	public static BridgeSaveSlotData GetAutoSave()
	{
		return FindByID(GetReservedSlotID(ReservedSlot.AUTOSAVE));
	}

	public static int GetBudgetForReservedSlot(ReservedSlot reservedSlot)
	{
		int reservedSlotID = GetReservedSlotID(reservedSlot);
		if (reservedSlotID == -1)
		{
			return int.MaxValue;
		}
		return GetBudgetForSlotID(reservedSlotID);
	}

	public static int GetBudgetForSlotID(int slotID)
	{
		return FindByID(slotID)?.m_Budget ?? int.MaxValue;
	}

	public static int GetEncodedLowestStressForSlotID(int slotID)
	{
		return FindByID(slotID)?.m_MaxStress ?? 10000;
	}

	public static bool SaveReserved(string directory, ReservedSlot reservedSlot, GameState nextState)
	{
		int reservedSlotID = GetReservedSlotID(reservedSlot);
		if (reservedSlotID == -1)
		{
			return false;
		}
		BridgeSaveSlotData bridgeSaveSlotData = FindByID(reservedSlotID);
		if (bridgeSaveSlotData == null)
		{
			bridgeSaveSlotData = Add(GetReservedSlotName(reservedSlot), reservedSlotID);
		}
		if (bridgeSaveSlotData == null)
		{
			return false;
		}
		bridgeSaveSlotData.m_Bridge = ((GameStateManager.GetState() == GameState.SIM) ? Bridge.m_BridgeRestore.SerializeBinary() : BridgeSave.SerializeBinary());
		bridgeSaveSlotData.m_Budget = Mathf.RoundToInt(Budget.m_BridgeCost);
		bridgeSaveSlotData.m_MaxStress = GameLeaderboards.ConvertStressToScore(StressSamples.m_MaxStressNormalized);
		bridgeSaveSlotData.m_UsingUnlimitedMaterials = Budget.m_UsingForcedUnlimitedMaterial;
		bridgeSaveSlotData.m_UsingUnlimitedBudget = Budget.m_UsingForcedUnlimitedBudget;
		bridgeSaveSlotData.m_LevelID = Game.GetLevelId();
		bridgeSaveSlotData.m_PhysicsVersion = GameManager.GetPhysicsEngineVersion();
		if (reservedSlot == ReservedSlot.AUTOSAVE)
		{
			bridgeSaveSlotData.m_Thumb = SaveSlotImageMaker.CaptureImage(nextState);
			m_LastThumbnail = bridgeSaveSlotData.m_Thumb;
		}
		else
		{
			bridgeSaveSlotData.m_Thumb = m_LastThumbnail;
		}
		return Save(directory, bridgeSaveSlotData);
	}

	public static void DeleteReserved(string directory, ReservedSlot reservedSlot)
	{
		int reservedSlotID = GetReservedSlotID(reservedSlot);
		if (reservedSlotID != -1)
		{
			BridgeSaveSlotData bridgeSaveSlotData = FindByID(reservedSlotID);
			if (bridgeSaveSlotData != null)
			{
				DeleteFile(bridgeSaveSlotData.m_SlotFilename);
			}
		}
	}

	public static BridgeSaveSlotData Create(string slotName, int slotIndex)
	{
		return new BridgeSaveSlotData
		{
			m_Version = CURRENT_VERSION,
			m_PhysicsVersion = GameManager.GetPhysicsEngineVersion(),
			m_DisplayName = slotName,
			m_SlotID = slotIndex,
			m_LevelID = Game.GetLevelId(),
			m_SlotFilename = AddFileExtension(slotName)
		};
	}

	public static BridgeSaveSlotData Add(string slotName, int slotIndex)
	{
		BridgeSaveSlotData bridgeSaveSlotData = Create(slotName, slotIndex);
		m_Slots.Add(bridgeSaveSlotData);
		return bridgeSaveSlotData;
	}

	public static string GetLocalizedSlotName(ReservedSlot reservedSlot)
	{
		return reservedSlot switch
		{
			ReservedSlot.AUTOSAVE => Localize.Get("UI_AUTO_SAVE"), 
			ReservedSlot.BUDGET => Localize.Get("UI_LOWEST_BUDGET"), 
			ReservedSlot.BUDGET_PERFECTION => Localize.Get("UI_LOWEST_BUDGET_NO_BREAKS"), 
			ReservedSlot.LOWEST_STRESS => Localize.Get("UI_LOWEST_STRESS_SLOTNAME"), 
			_ => string.Empty, 
		};
	}

	public static void ForceReservedSlotsToTop(Panel_FileLoader fileLoader, bool sortByDate)
	{
		List<BridgeSaveSlotData> list = new List<BridgeSaveSlotData>();
		List<BridgeSaveSlotData> list2 = new List<BridgeSaveSlotData>();
		foreach (BridgeSaveSlotData slot in m_Slots)
		{
			if (slot.m_SlotID < NUM_RESERVED_SLOTS)
			{
				list.Add(slot);
			}
			else
			{
				list2.Add(slot);
			}
		}
		list.Sort(SortBySlotID);
		if (sortByDate)
		{
			list2.Sort(SortByDate);
		}
		else
		{
			list2.Sort(SortByFilename);
		}
		List<FileSlot> list3 = new List<FileSlot>();
		foreach (BridgeSaveSlotData item in list)
		{
			FileSlot fileSlot = fileLoader.FindSlotByFilename(item.m_SlotFilename);
			if ((bool)fileSlot)
			{
				list3.Add(fileSlot);
			}
		}
		foreach (BridgeSaveSlotData item2 in list2)
		{
			FileSlot fileSlot2 = fileLoader.FindSlotByFilename(item2.m_SlotFilename);
			if ((bool)fileSlot2)
			{
				list3.Add(fileSlot2);
			}
		}
		fileLoader.m_Slots.Clear();
		fileLoader.m_Slots.AddRange(list3);
		fileLoader.MatchLayoutWithSlots();
	}

	private static int SortBySlotID(BridgeSaveSlotData a, BridgeSaveSlotData b)
	{
		return a.m_SlotID.CompareTo(b.m_SlotID);
	}

	private static int SortByFilename(BridgeSaveSlotData a, BridgeSaveSlotData b)
	{
		return a.m_SlotFilename.CompareTo(b.m_SlotFilename);
	}

	private static int SortByDate(BridgeSaveSlotData a, BridgeSaveSlotData b)
	{
		return b.m_LastWriteTimeTicks.CompareTo(a.m_LastWriteTimeTicks);
	}

	private static bool Write(string directory, string filename, byte[] bytes)
	{
		string text = Path.Combine(GetSavePath(Profiles.GetActiveProfileName()), directory);
		Utils.CreateDirectory(text);
		if (!Directory.Exists(text))
		{
			Debug.LogWarningFormat("Failed to write save slot since {0} not created", text);
			return false;
		}
		try
		{
			Utils.WriteBytesWithBackup(text, filename, bytes);
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Failed to write {0} due to exception {1}", Path.Combine(text, filename), ex.Message);
			return false;
		}
	}

	public static string AddFileExtension(string filename)
	{
		if (Path.GetExtension(filename) == SAVE_EXTENSION)
		{
			return filename;
		}
		return filename + SAVE_EXTENSION;
	}

	public static string RemoveFileExtenstion(string filename)
	{
		return Path.GetFileNameWithoutExtension(filename);
	}

	public static string GetSavePath(string profileName)
	{
		return Path.Combine(Application.persistentDataPath, Profiles.ROOT_DIRECTORY_NAME, profileName, SAVE_DIRECTORY);
	}

	public static string GetDefaultNewSlotName()
	{
		return DateTime.Now.ToString("MMM d yyyy, h.mm tt ", CultureInfo.InvariantCulture);
	}

	public static BridgeSaveSlotData FindByFilename(string filename)
	{
		foreach (BridgeSaveSlotData slot in m_Slots)
		{
			if (slot.m_SlotFilename == filename)
			{
				return slot;
			}
		}
		return null;
	}

	public static bool FilenameExists(string directory, string filename)
	{
		string text = Path.Combine(GetSavePath(Profiles.GetActiveProfileName()), directory);
		if (!Directory.Exists(text))
		{
			return false;
		}
		return File.Exists(Path.Combine(text, filename));
	}

	public static BridgeSaveSlotData FindByID(int id)
	{
		foreach (BridgeSaveSlotData slot in m_Slots)
		{
			if (slot.m_SlotID == id)
			{
				return slot;
			}
		}
		return null;
	}

	public static int GetHighestSlotID()
	{
		if (m_Slots.Count <= 0)
		{
			return 0;
		}
		return m_Slots[m_Slots.Count - 1].m_SlotID;
	}

	public static int GetReservedSlotID(ReservedSlot reservedSlot)
	{
		switch (reservedSlot)
		{
		case ReservedSlot.AUTOSAVE:
			return 0;
		case ReservedSlot.BUDGET:
			return 1;
		case ReservedSlot.BUDGET_PERFECTION:
			return 2;
		case ReservedSlot.LOWEST_STRESS:
			return 3;
		default:
			Debug.LogWarningFormat("Unexpected ReservedSlot type {0}", reservedSlot.ToString());
			return -1;
		}
	}

	public static string GetReservedSlotName(ReservedSlot reservedSlot)
	{
		switch (reservedSlot)
		{
		case ReservedSlot.AUTOSAVE:
			return AUTOSAVE_SLOT_NAME;
		case ReservedSlot.BUDGET:
			return BUDGET_SLOT_NAME;
		case ReservedSlot.BUDGET_PERFECTION:
			return BUDGET_PERFECTION_SLOT_NAME;
		case ReservedSlot.LOWEST_STRESS:
			return LOWEST_STRESS_SLOT_NAME;
		default:
			Debug.LogWarningFormat("Unexpected ReservedSlot type {0}", reservedSlot.ToString());
			return string.Empty;
		}
	}

	public static void SlotDeleteCallback(FileSlot slot)
	{
		m_SlotToDelete = slot;
		PopUpMessage.DisplayWarning(Localize.Get("POPUP_DELETE_SLOT", m_SlotToDelete.m_DisplayName.text), useYesNoLables: false, DeleteSaveSlotCallback);
	}

	public static void SlotRenameCallback(FileSlot slot)
	{
		m_SlotToRename = slot;
		PopupInputField.Display(Localize.Get("POPUP_RENAME_SLOT", m_SlotToRename.m_DisplayName.text), m_SlotToRename.m_DisplayName.text, isFilename: true, isDirectory: false, RenameSaveSlotCallback);
	}

	public static void DeleteSaveSlot(FileSlot deleteSlot)
	{
		DeleteFile(deleteSlot.m_FileName);
		if (GameUI.m_Instance.m_LoadBridge.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_LoadBridge.m_FileLoader.DeleteSlot(deleteSlot);
		}
		else
		{
			GameUI.m_Instance.m_SaveBridge.m_FileLoader.DeleteSlot(deleteSlot);
		}
		foreach (BridgeSaveSlotData slot in m_Slots)
		{
			if (slot.m_SlotFilename == deleteSlot.m_FileName)
			{
				m_Slots.Remove(slot);
				break;
			}
		}
	}

	public static void DeleteFile(string filename)
	{
		string directoryForSaveSlot = GetDirectoryForSaveSlot();
		Utils.DeleteFile(Path.Combine(Path.Combine(GetSavePath(Profiles.GetActiveProfileName()), directoryForSaveSlot), filename));
	}

	public static void RenameSaveSlot(FileSlot renameSlot, string newName)
	{
		m_SlotRenameOldName = renameSlot.m_DisplayName.text;
		m_SlotRenameNewName = newName;
		string filename = AddFileExtension(newName);
		if (FilenameExists(GetDirectoryForSaveSlot(), filename))
		{
			PopUpMessage.Display(Localize.Get("POPUP_SAVE_SLOT_EXISTS", newName), RenameAfterConfirmation);
		}
		else
		{
			RenameAfterConfirmation();
		}
	}

	private static void RenameAfterConfirmation()
	{
		BridgeSaveSlotData bridgeSaveSlotData = FindByFilename(AddFileExtension(m_SlotRenameOldName));
		if (bridgeSaveSlotData != null)
		{
			DeleteFile(bridgeSaveSlotData.m_SlotFilename);
			bridgeSaveSlotData.m_DisplayName = m_SlotRenameNewName;
			bridgeSaveSlotData.m_SlotFilename = AddFileExtension(m_SlotRenameNewName);
			if (!Save(GetDirectoryForSaveSlot(), bridgeSaveSlotData))
			{
				PopUpMessage.DisplayErrorOkOnly(Localize.Get("WARN_FAILED_SLOT_RENAME"));
			}
		}
		if ((bool)m_SlotToRename)
		{
			GameUI.SetAndEnableText(m_SlotToRename.m_DisplayName, m_SlotRenameNewName);
		}
	}

	public static void DeleteAllReservedSlotSaves()
	{
		string savePath = GetSavePath(Profiles.GetActiveProfileName());
		if (!Directory.Exists(savePath))
		{
			return;
		}
		string[] files = Directory.GetFiles(savePath, "*" + SAVE_EXTENSION, SearchOption.AllDirectories);
		foreach (string text in files)
		{
			BridgeSaveSlotData bridgeSaveSlotData = Load(text);
			if (bridgeSaveSlotData != null && bridgeSaveSlotData.m_SlotID < NUM_RESERVED_SLOTS)
			{
				try
				{
					File.Delete(text);
				}
				catch (Exception ex)
				{
					Debug.LogWarningFormat("Exception {0} trying to delete {1}", ex.Message, text);
				}
			}
		}
	}

	public static bool HasCompletedLevel(string levelId)
	{
		string lowestBudgetFullPath = GetLowestBudgetFullPath(levelId);
		string lowestBudgetNoBreaksFullPath = GetLowestBudgetNoBreaksFullPath(levelId);
		if (!Utils.FileExists(lowestBudgetFullPath))
		{
			return Utils.FileExists(lowestBudgetNoBreaksFullPath);
		}
		return true;
	}

	public static bool HasCompletedLevelUnderBudget(string levelId, int budget)
	{
		BridgeSaveSlotData bridgeSaveSlotData = Load(GetLowestBudgetFullPath(levelId));
		if (bridgeSaveSlotData != null && bridgeSaveSlotData.m_Budget <= budget)
		{
			return true;
		}
		BridgeSaveSlotData bridgeSaveSlotData2 = Load(GetLowestBudgetNoBreaksFullPath(levelId));
		if (bridgeSaveSlotData2 != null && bridgeSaveSlotData2.m_Budget <= budget)
		{
			return true;
		}
		return false;
	}

	public static bool HasCompletedLevelUnderBudgetNoBreaks(string levelId, int budget)
	{
		BridgeSaveSlotData bridgeSaveSlotData = Load(GetLowestBudgetNoBreaksFullPath(levelId));
		if (bridgeSaveSlotData != null && bridgeSaveSlotData.m_Budget <= budget)
		{
			return true;
		}
		return false;
	}

	public static string GetLowestBudgetFullPath(string levelId)
	{
		return Path.Combine(GetSavePath(Profiles.GetActiveProfileName()), levelId, BUDGET_SLOT_NAME + SAVE_EXTENSION);
	}

	public static string GetLowestBudgetNoBreaksFullPath(string levelId)
	{
		return Path.Combine(GetSavePath(Profiles.GetActiveProfileName()), levelId, BUDGET_PERFECTION_SLOT_NAME + SAVE_EXTENSION);
	}

	public static void RecordLastSlotSavedForFutureQuicksave(string slotName)
	{
		m_LastSlotNameSaved = slotName;
		m_LastSlotNameSavedDirectory = GetDirectoryForSaveSlot();
	}

	public static void ClearLastSlotSavedForFutureQuicksave()
	{
		m_LastSlotNameSaved = string.Empty;
		m_LastSlotNameSavedDirectory = string.Empty;
	}

	public static bool CanQuickSave()
	{
		if (GetDirectoryForSaveSlot() == m_LastSlotNameSavedDirectory)
		{
			return !string.IsNullOrEmpty(m_LastSlotNameSaved);
		}
		return false;
	}

	public static bool TryQuickSave()
	{
		GetDirectoryForSaveSlot();
		if (CanQuickSave())
		{
			GameUI.m_Instance.m_SaveBridge.Save(AddFileExtension(m_LastSlotNameSaved), quicksave: true);
			return true;
		}
		return false;
	}

	private static void DeleteSaveSlotCallback()
	{
		if ((bool)m_SlotToDelete)
		{
			DeleteSaveSlot(m_SlotToDelete);
			m_SlotToDelete = null;
		}
	}

	private static void RenameSaveSlotCallback(string name)
	{
		if (!string.IsNullOrEmpty(name))
		{
			name = name.Trim();
			if (string.IsNullOrEmpty(name) || Utils.HasInvalidFileNameChars(name))
			{
				PopUpMessage.DisplayWarningOkOnly(Localize.Get("UI_INVALID_FILENAME", name));
				m_SlotToRename = null;
			}
			if ((bool)m_SlotToRename)
			{
				RenameSaveSlot(m_SlotToRename, name);
			}
		}
	}

	private static bool SlotExistsWithId(int id)
	{
		foreach (BridgeSaveSlotData slot in m_Slots)
		{
			if (slot.m_SlotID == id)
			{
				return true;
			}
		}
		return false;
	}
}
