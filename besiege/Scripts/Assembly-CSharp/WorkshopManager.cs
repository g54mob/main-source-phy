using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Localisation;
using PlayFab.Party;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class WorkshopManager : SingleInstance<WorkshopManager>
{
	public class WorkshopItem
	{
		public ItemTypes ItemType;

		public ItemListing Source;

		public bool IsInstalled;

		public string Title;

		public string RootFolder;

		public List<string> Folders = new List<string>();

		public string SkinName;

		public uint SubscribeTime;

		public string Tags;

		public ulong WorkshopId;

		public ulong Author;

		public bool IsOwner;

		public uint DlcDependencyMask;

		public bool AreDlcRequirementsMet;

		public WorkshopItem(string title)
		{
			Title = title;
		}
	}

	public enum UploadVisibility
	{
		Public = 0,
		FriendsOnly = 1,
		Private = 2
	}

	public enum ItemTypes
	{
		Machines = 0,
		Skins = 1,
		Levels = 2,
		Translations = 3,
		Mods = 4,
		All = 5,
		Unknown = 6
	}

	public enum ItemListing
	{
		Subscribed = 0,
		Published = 1
	}

	public enum InstallType
	{
		All = 0,
		Installed = 1,
		NotInstalled = 2
	}

	public enum VerifyStringResult
	{
		Unknown = 0,
		TooLong = 1,
		Offensive = 2,
		Success = 3
	}

	private static class MetadataTypeKeys
	{
		public const string DlcDependencyMask = "DD";
	}

	public static bool Offline;

	public Action OnSubscribe;

	public Action OnUnsubscribe;

	public Action OnReloadedSkins;

	public bool SupressPopup;

	protected bool loadedWorkshopSkinsAlready;

	protected bool gettingNewerItem;

	protected string uploadFolder;

	private WorkshopMessage workshopMessagePrefab;

	private WorkshopMessage currentWorkshopMessage;

	public bool pfSignedIn;

	protected Queue<string> remoteFileSyncQueue = new Queue<string>();

	protected List<string> remoteFiles = new List<string>();

	protected bool SkipSSO;

	public static int demoLevelCount = 5;

	public override string Name
	{
		get
		{
			return "WorkshopManager";
		}
	}

	public static WorkshopType DetermineWorkshopType()
	{
		if (SteamManager.Initialized)
		{
			return WorkshopType.Steam;
		}
		return WorkshopType.None;
	}

	public static bool IsInitialized()
	{
		return SteamManager.Initialized;
	}

	protected virtual void Awake()
	{
		ReferenceMaster.Unsubscribe += Unsubscribe;
		ReferenceMaster.RefreshWorkshopDel += RefreshSkins;
		BlockSkinLoader.SetupSkins += SetupWorkshopSkins;
		CacheUploadPath();
	}

	public void CacheUploadPath()
	{
		uploadFolder = StaticSettings.DataPath + "/WorkshopUpload/";
	}

	public virtual void PlayfabSignin(Action<bool> onComplete)
	{
		onComplete(pfSignedIn);
	}

	protected bool SyncFile(string path)
	{
		return path.StartsWith("SavedMachines") || path.StartsWith("CustomLevels");
	}

	protected virtual void Start()
	{
		workshopMessagePrefab = Resources.Load<WorkshopMessage>("Workshop/WorkshopMessage");
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	protected void PassToSkinLoader(List<WorkshopItem> items)
	{
		List<string> list = new List<string>();
		Dictionary<string, ulong> dictionary = new Dictionary<string, ulong>();
		Dictionary<ulong, WorkshopItem> dictionary2 = new Dictionary<ulong, WorkshopItem>();
		foreach (WorkshopItem item in items)
		{
			list.AddRange(item.Folders);
			AddFolderToWorkshopIdReferences(item, dictionary, dictionary2);
		}
		ReferenceMaster.FolderToWorkshop = dictionary;
		ReferenceMaster.FolderToWorkshopItem = dictionary2;
		StartCoroutine(KeepTryRefresh(list.ToArray()));
	}

	protected void NewSkinDownloaded(WorkshopItem item)
	{
		if (item != null && ReferenceMaster.FolderToWorkshop != null)
		{
			ImportItemData(item, item.RootFolder);
			AddFolderToWorkshopIdReferences(item, ReferenceMaster.FolderToWorkshop, ReferenceMaster.FolderToWorkshopItem);
			StartCoroutine(KeepTryRefresh(item.Folders.ToArray()));
		}
	}

	protected void SkinRemoved(ulong workshopId)
	{
		RemoveFolderToWorkshopIdReferences(workshopId, ReferenceMaster.FolderToWorkshop, ReferenceMaster.FolderToWorkshopItem);
		for (int num = BlockSkinLoader.SkinPacks.Count - 1; num >= 0; num--)
		{
			if (BlockSkinLoader.SkinPacks[num].id.Equals(workshopId.ToString()) && !BlockSkinLoader.SkinPacks[num].deleted)
			{
				BlockSkinLoader.SkinPacks[num].Delete();
			}
		}
	}

	private void RemoveFolderToWorkshopIdReferences(ulong modId, Dictionary<string, ulong> referenceDictionary, Dictionary<ulong, WorkshopItem> itemReferenceDictionary)
	{
		if (referenceDictionary == null)
		{
			return;
		}
		List<string> list = (from pair in referenceDictionary
			where pair.Value == modId
			select pair.Key).ToList();
		foreach (string item in list)
		{
			referenceDictionary.Remove(item);
		}
		itemReferenceDictionary.Remove(modId);
	}

	protected void ImportItemData(WorkshopItem item, string folderPath)
	{
		if (string.IsNullOrEmpty(folderPath))
		{
			return;
		}
		if (item.ItemType == ItemTypes.Skins && item.Source == ItemListing.Subscribed)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
			try
			{
				if (directoryInfo != null && directoryInfo.Exists)
				{
					DirectoryInfo[] directories = directoryInfo.GetDirectories();
					DirectoryInfo[] array = directories;
					foreach (DirectoryInfo directoryInfo2 in array)
					{
						item.Folders.Add(directoryInfo2.FullName);
					}
				}
				return;
			}
			catch (Exception message)
			{
				Debug.Log(message);
				return;
			}
		}
		if (item.ItemType == ItemTypes.Machines || item.ItemType == ItemTypes.Levels)
		{
			item.Folders.Add(folderPath);
		}
	}

	private void AddFolderToWorkshopIdReferences(WorkshopItem item, Dictionary<string, ulong> referenceDictionary, Dictionary<ulong, WorkshopItem> itemReferenceDictionary)
	{
		foreach (string folder in item.Folders)
		{
			string key = folder.Replace("\\", "/");
			if (referenceDictionary.ContainsKey(key))
			{
				referenceDictionary.Remove(key);
			}
			referenceDictionary.Add(key, item.WorkshopId);
			if (itemReferenceDictionary.ContainsKey(item.WorkshopId))
			{
				itemReferenceDictionary.Remove(item.WorkshopId);
			}
			itemReferenceDictionary.Add(item.WorkshopId, item);
		}
	}

	private IEnumerator KeepTryRefresh(string[] newWorkshopDirs)
	{
		if (!SingleInstance<BlockSkinLoader>.Instance.WorkshopTryRefresh(newWorkshopDirs))
		{
			yield return null;
		}
	}

	protected void DisplayPopup(string message)
	{
		DisplayPopup(message, false);
	}

	protected void DisplayPopup(string message, bool enableAnim)
	{
		DisplayPopup(message, enableAnim, 5f);
	}

	protected void DisplayPopup(string message, bool enableAnim, float destroyTime)
	{
		if (SupressPopup)
		{
			return;
		}
		if (currentWorkshopMessage == null)
		{
			currentWorkshopMessage = UnityEngine.Object.Instantiate(workshopMessagePrefab);
			currentWorkshopMessage.transform.position = new Vector3(4.8f, -23.2f, 30f);
			if (SceneManager.GetActiveScene().name == "TITLE SCREEN")
			{
				currentWorkshopMessage.gameObject.SetLayerRecursively(LayerMask.NameToLayer("HUD (Late)"));
			}
		}
		WorkshopType workshopType = DetermineWorkshopType();
		currentWorkshopMessage.Setup(workshopType, message, enableAnim, destroyTime);
	}

	protected void CreateUploadFolder()
	{
		IOHelper.DeleteDirectory(uploadFolder);
		Directory.CreateDirectory(uploadFolder);
	}

	public abstract void GetSubscribedWorkshopItemsAsync(ItemTypes itemType, InstallType installType, Action<List<WorkshopItem>> callbackHandler);

	public abstract void GetPublishedWorkshopItemsAsync(ItemTypes itemType, Action<List<WorkshopItem>> callbackHandler);

	public abstract void CreateWorkshopMod(UploadData uploadData);

	public abstract void CreateWorkshopSkin(UploadData uploadData);

	public abstract void CreateWorkshopMachine(UploadData uploadData);

	public abstract void CreateWorkshopLevel(UploadData uploadData);

	public abstract void Unsubscribe(ulong workshopFileId);

	public abstract void Download(ulong workshopFileId);

	protected abstract void SetupWorkshopSkins();

	protected abstract void RefreshSkins();

	public virtual void InitializePlayfabManager(PlayFabMultiplayerManager mpManager)
	{
	}

	public virtual void UpdateRecentPlayers(ulong[] xuids)
	{
	}

	public virtual void StartActivity(string pfNetworkId, bool isPublic)
	{
	}

	public virtual void UpdateActivity(bool isPublic)
	{
	}

	public virtual void DeleteActivity()
	{
	}

	public virtual void UpdateMute(PlayfabConnection.PlayfabNetworkPlayer player, PlayFabMultiplayerManager mpManager)
	{
	}

	public static void VerifyString(string input, Action<VerifyStringResult, string> onComplete)
	{
		if (SingleInstance<WorkshopManager>.hasInstance())
		{
			SingleInstance<WorkshopManager>.Instance.VerifyString(onComplete, input);
		}
		else if (onComplete != null)
		{
			onComplete(VerifyStringResult.Success, input);
		}
	}

	protected virtual void VerifyString(Action<VerifyStringResult, string> onComplete, string input)
	{
		if (onComplete != null)
		{
			onComplete(VerifyStringResult.Success, input);
		}
	}

	public virtual void AllowCommunicationWithUser(PlayerData playerData, Action<bool> onComplete)
	{
		if (onComplete != null)
		{
			onComplete(true);
		}
	}

	public virtual void GetMultiplayerPermission(Action<bool> onComplete)
	{
		if (onComplete != null)
		{
			onComplete(true);
		}
	}

	public void UpdateRemoteFileList(Action onComplete)
	{
		remoteFiles.Clear();
		GetRemoteFileList(delegate(List<string> fileList)
		{
			remoteFiles.AddRange(fileList);
			if (ReferenceMaster.onRemoteFilesUpdated != null)
			{
				ReferenceMaster.onRemoteFilesUpdated();
			}
			onComplete();
		});
	}

	protected void SyncRemoteFiles()
	{
		UpdateRemoteFileList(delegate
		{
			for (int i = 0; i < remoteFiles.Count; i++)
			{
				string text = remoteFiles[i];
				string path = Path.Combine(StaticSettings.DataPath, text);
				if (SyncFile(text) && !File.Exists(path))
				{
					remoteFileSyncQueue.Enqueue(text);
				}
			}
			SyncNextFile();
		});
	}

	private void SyncNextFile()
	{
		if (remoteFileSyncQueue.Count != 0)
		{
			string cloudPath = remoteFileSyncQueue.Dequeue();
			ReadRemoteFileAsync(cloudPath, OnSyncRemoteFile);
		}
	}

	private void OnSyncRemoteFile(string path, bool success, byte[] content)
	{
		if (success)
		{
			string path2 = Path.Combine(StaticSettings.DataPath, path);
			string directoryName = Path.GetDirectoryName(path2);
			if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			File.WriteAllBytes(path2, content);
			if (BesiegeLogFilter.logDebug)
			{
				Debug.Log("[WorkshopManager] OnSyncRemoteFile Successfully synced " + path);
			}
		}
		else if (BesiegeLogFilter.logDebug)
		{
			Debug.LogWarning("Couldn't retrieve '" + path + "' from cloud storage!");
		}
		SyncNextFile();
	}

	public void WriteRemoteFileAsync(FileInfo fileInfo, bool includeThumb)
	{
		string remotePath = GetRemotePath(FileSystemPath.Parse(fileInfo.Directory.FullName).Path);
		string path = FileSystemPath.Parse(Path.Combine(remotePath, fileInfo.Name)).Path;
		WriteRemoteFile(path, File.ReadAllBytes(fileInfo.FullName));
		if (includeThumb)
		{
			string path2 = FileSystemPath.Parse(Path.Combine(Path.Combine(remotePath, "Thumbnails"), Path.GetFileNameWithoutExtension(fileInfo.Name) + ".png")).Path;
			string path3 = Path.Combine(StaticSettings.DataPath, path2);
			if (File.Exists(path3))
			{
				WriteRemoteFile(path2, File.ReadAllBytes(path3));
			}
		}
	}

	public void WriteRemoteFile(string fullPath, byte[] content)
	{
		string remotePath = GetRemotePath(fullPath);
		WriteRemoteFileAsync(remotePath, content, delegate
		{
		});
		remoteFiles.Add(remotePath);
	}

	public virtual void GetRemoteFileList(Action<List<string>> onComplete)
	{
		onComplete(new List<string>());
	}

	public virtual void ReadRemoteFileAsync(string cloudPath, Action<string, bool, byte[]> onRemoteReadComplete)
	{
		onRemoteReadComplete(cloudPath, false, null);
	}

	public virtual void WriteRemoteFileAsync(string cloudPath, byte[] content, Action<string, bool> onRemoteWriteComplete)
	{
		onRemoteWriteComplete(cloudPath, false);
	}

	public bool RemoveRemoteFile(string fullPath)
	{
		string cloudPath = GetRemotePath(fullPath);
		if (!IsRemoteFile(cloudPath))
		{
			if (BesiegeLogFilter.logDebug)
			{
				Debug.Log("[WorkshopManager] RemoveRemoteFile Not a remote file " + cloudPath);
			}
			return false;
		}
		remoteFiles.Remove(cloudPath);
		DeleteRemoteFileAsync(cloudPath, delegate(string path, bool success)
		{
			if (BesiegeLogFilter.logDebug)
			{
				if (!success)
				{
					Debug.Log("[WorkshopManager] RemoveRemoteFile Failed to remove remote file " + cloudPath);
				}
				else
				{
					Debug.Log("[WorkshopManager] RemoveRemoteFile Removed remote file " + cloudPath);
				}
			}
		});
		return true;
	}

	public virtual void DeleteRemoteFileAsync(string cloudPath, Action<string, bool> onRemoteDeleteComplete)
	{
	}

	public void ClearRemoteFiles()
	{
		UpdateRemoteFileList(delegate
		{
			while (remoteFiles.Count > 0)
			{
				RemoveRemoteFile(remoteFiles[0]);
			}
		});
	}

	public string GetRemotePath(string fullPath)
	{
		string text = fullPath.Replace(FileSystemPath.Parse(StaticSettings.DataPath).Path, string.Empty);
		if (text.StartsWith(FileSystemPath.DirectorySeparator.ToString()))
		{
			text = text.Substring(1, text.Length - 1);
		}
		return text;
	}

	public bool IsRemoteFile(string path)
	{
		return remoteFiles.Contains(path);
	}

	public static string GetMetadataForItem(uint dlcDependencyMask)
	{
		if (dlcDependencyMask == 0)
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder();
		AddMetadataString(stringBuilder, "DD", dlcDependencyMask);
		return stringBuilder.ToString();
	}

	private static void AddMetadataString(StringBuilder builder, string key, object value)
	{
		if (builder.Length != 0)
		{
			builder.Append(";");
		}
		builder.Append(key);
		builder.Append("=");
		builder.Append(value);
	}

	public static void ParseItemMetadata(string metadata, out uint dlcDependencyMask)
	{
		dlcDependencyMask = 0u;
		if (string.IsNullOrEmpty(metadata))
		{
			return;
		}
		string[] array = metadata.Split(';');
		string[] array2 = array;
		foreach (string text in array2)
		{
			string[] array3 = text.Split('=');
			if (array3.Length == 2)
			{
				string text2 = array3[0];
				string s = array3[1];
				switch (text2)
				{
				case "DD":
					uint.TryParse(s, out dlcDependencyMask);
					break;
				}
			}
		}
	}

	public virtual string GetPlayerName()
	{
		return LocalisationManager.GetTranslation(1947);
	}

	protected virtual void OnDestroy()
	{
		ReferenceMaster.Unsubscribe -= Unsubscribe;
		ReferenceMaster.RefreshWorkshopDel -= RefreshSkins;
		BlockSkinLoader.SetupSkins -= SetupWorkshopSkins;
	}

	public abstract void UpdateWorkshopMachine(ulong workshopItemId, UploadData uploadData);

	public abstract void UpdateWorkshopSkin(ulong workshopItemId, UploadData uploadData);

	public abstract void UpdateWorkshopLevel(ulong workshopItemId, UploadData uploadData);

	public abstract void UpdateWorkshopMod(ulong workshopItemId, UploadData uploadData);
}
