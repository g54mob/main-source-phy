using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class LevelProvider
{
	public class LevelItem
	{
		public string id;

		public DisplayMode displayMode;

		public string levelName;

		public string creator;

		public string tags;

		public string description;

		public string size;

		public int tokensMin = -1;

		public int tokensMax = -1;

		public bool finished;

		public bool gotTrophy;

		public bool isNew;

		public bool isLocked;

		public DateTime unlockTime;

		public string date;

		public string playTime;

		public long lastSaveTimeInTicks;

		public DateTime dateFolderCreated = DateTime.Now;

		public DateTime dateRoomModified = DateTime.Now;

		public LevelItemType levelType;

		public float installProgress = -1f;

		public DataState textureState;

		public string textureURL;

		public string textureName;

		public Texture2D texture;

		public DataState installState;

		public bool isDirty;

		public float scheduledDirtyTime;

		public bool forceUseWebRequest;

		public string textureKey => textureURL ?? textureName;

		public LevelItem(string id)
		{
			this.id = id;
		}
	}

	public enum DataState
	{
		None = 0,
		Requested = 1,
		Fetching = 2,
		Done = 3
	}

	public enum DisplayMode
	{
		Normal = 0,
		Hidden = 1
	}

	private class TextureLoadResult
	{
		public UnityWebRequestAsyncOperation webOperation;

		public Texture2D textureFromBundle;

		public string id;

		public TextureLoadResult(UnityWebRequestAsyncOperation webOperation, string id)
		{
			this.webOperation = webOperation;
			this.id = id;
		}

		public TextureLoadResult(Texture2D textureFromBundle, string id)
		{
			this.textureFromBundle = textureFromBundle;
			this.id = id;
		}
	}

	public enum LevelItemType
	{
		Normal = 0,
		DLCNotInstalled = 1
	}

	public enum PackSize
	{
		StandardRoom = 0,
		CustomRoom = 1,
		DarkestRoom = 2
	}

	private Dictionary<string, List<string>> lists = new Dictionary<string, List<string>>();

	private List<LevelItem> levelItems = new List<LevelItem>();

	private List<TextureLoadResult> asyncTextures = new List<TextureLoadResult>();

	private Action<string> onLevelRefresh;

	private Dictionary<string, Texture2D> cachedTextures = new Dictionary<string, Texture2D>();

	private ESUGC ugc;

	public LevelProvider(ESUGC ugc, Action<string> onLevelRefresh)
	{
		this.ugc = ugc;
		this.onLevelRefresh = onLevelRefresh;
	}

	public void update()
	{
		foreach (LevelItem levelItem3 in levelItems)
		{
			levelItem3.isDirty = false;
			if (levelItem3.textureState == DataState.Requested)
			{
				if (levelItem3.textureKey == null)
				{
					levelItem3.textureState = DataState.Done;
					continue;
				}
				levelItem3.textureState = DataState.Fetching;
				if (cachedTextures.TryGetValue(levelItem3.textureKey, out var value))
				{
					Debug.Log("got cached texture " + levelItem3.textureURL);
					levelItem3.texture = value;
					levelItem3.textureState = DataState.Done;
					setDirty(levelItem3);
				}
				else if (levelItem3.textureURL != null)
				{
					UnityWebRequestAsyncOperation webOperation = UnityWebRequestTexture.GetTexture(levelItem3.textureURL, nonReadable: true).SendWebRequest();
					asyncTextures.Add(new TextureLoadResult(webOperation, levelItem3.id));
				}
				else if (levelItem3.textureName != null)
				{
					Texture2D asset = AssetBundleLoader.getAsset<Texture2D>(AssetBundleType.RoomMetaAssets, "Assets/_RoomMetaAssets/" + levelItem3.textureName);
					asyncTextures.Add(new TextureLoadResult(asset, levelItem3.id));
				}
				else
				{
					Debug.LogError("[LP Error] Requested texture but no URL or name provided");
				}
			}
			DataState installState = levelItem3.installState;
			if (installState != DataState.Requested && installState != DataState.Fetching)
			{
				continue;
			}
			if (!ugc.isCustomLevel(levelItem3.id))
			{
				levelItem3.installState = DataState.Done;
			}
			else
			{
				levelItem3.installState = DataState.Fetching;
				CustomRoomState customRoomState = ugc.customRoomState(levelItem3.id);
				if (customRoomState == CustomRoomState.Installed || customRoomState == CustomRoomState.NotInstalled)
				{
					levelItem3.installState = DataState.Done;
					levelItem3.installProgress = -1f;
				}
				else
				{
					levelItem3.installProgress = ugc.getInstallProgress(levelItem3.id);
					if (levelItem3.installProgress == 1f)
					{
						levelItem3.installState = DataState.Done;
						levelItem3.installProgress = -1f;
					}
				}
			}
			setDirty(levelItem3);
		}
		for (int num = asyncTextures.Count - 1; num >= 0; num--)
		{
			TextureLoadResult asyncTexture = asyncTextures[num];
			if (asyncTextures[num].webOperation != null)
			{
				if (asyncTexture.webOperation.isDone)
				{
					UnityWebRequestAsyncOperation webOperation2 = asyncTexture.webOperation;
					LevelItem levelItem = levelItems.Find((LevelItem x) => x.id == asyncTexture.id);
					asyncTextures.Remove(asyncTexture);
					if (webOperation2.webRequest.result == UnityWebRequest.Result.Success)
					{
						Texture2D content = DownloadHandlerTexture.GetContent(webOperation2.webRequest);
						cachedTextures[levelItem.textureKey] = content;
						levelItem.texture = content;
						levelItem.textureState = DataState.Done;
						setDirty(levelItem);
					}
				}
			}
			else
			{
				LevelItem levelItem2 = levelItems.Find((LevelItem x) => x.id == asyncTexture.id);
				asyncTextures.Remove(asyncTexture);
				Texture2D textureFromBundle = asyncTexture.textureFromBundle;
				cachedTextures[levelItem2.textureKey] = textureFromBundle;
				levelItem2.texture = textureFromBundle;
				levelItem2.textureState = DataState.Done;
				setDirty(levelItem2);
			}
		}
		void setDirty(LevelItem item)
		{
			item.isDirty = true;
			onLevelRefresh(item.id);
		}
	}

	public bool hasLevel(string levelId)
	{
		return levelItems.Exists((LevelItem x) => x.id == levelId);
	}

	public bool hasLevel(string listId, string id)
	{
		if (!lists.ContainsKey(listId))
		{
			return false;
		}
		if (lists[listId].Find((string x) => x == id) != null)
		{
			return true;
		}
		return false;
	}

	public bool hasList(string listId)
	{
		return lists.ContainsKey(listId);
	}

	public List<string> getAllListNames()
	{
		return lists.Keys.ToList();
	}

	public List<string> getList(string id)
	{
		List<string> list = null;
		if (lists.TryGetValue(id, out var value))
		{
			list = new List<string>(value);
		}
		else
		{
			list = new List<string>();
			lists[id] = list;
		}
		return list;
	}

	public LevelItem addLevelToList(string listId, string levelId)
	{
		List<string> list = null;
		if (lists.ContainsKey(listId))
		{
			list = lists[listId];
		}
		else
		{
			list = new List<string>();
			lists[listId] = list;
		}
		if (!list.Contains(levelId))
		{
			list.Add(levelId);
		}
		return ensureLevelItem(levelId);
	}

	public void removeLevelFromList(string listId, string levelId)
	{
		List<string> list = null;
		if (lists.ContainsKey(listId))
		{
			list = lists[listId];
		}
		else
		{
			list = new List<string>();
			lists[listId] = list;
		}
		list.Remove(levelId);
	}

	public void removeAllFromList(string listId)
	{
		if (lists.ContainsKey(listId))
		{
			lists[listId].Clear();
		}
	}

	public void requestData(string id)
	{
		LevelItem levelItem = levelItems.Find((LevelItem x) => x.id == id);
		if (levelItem.texture == null && !string.IsNullOrEmpty(levelItem.textureKey))
		{
			levelItem.textureState = DataState.Requested;
		}
		levelItem.installState = DataState.Requested;
	}

	public void requestAllItems()
	{
		foreach (LevelItem levelItem in levelItems)
		{
			levelItem.installState = DataState.Requested;
		}
	}

	public void requestAllTextures()
	{
		foreach (LevelItem levelItem in levelItems)
		{
			levelItem.textureState = DataState.Requested;
		}
	}

	public LevelItem getLevelItem(string id)
	{
		return levelItems.Find((LevelItem x) => x.id == id);
	}

	public void modifyLevel(string id, string name = null, string textureName = null, string textureUrl = null, int tokensMin = -1, int tokensMax = -1, bool? finished = null, bool? gotTrophy = null, bool? isNew = null, bool? isLocked = null, float? slider = null, string date = null, string size = null, long? lastSaveTimeInTicks = null, DateTime? dateFolderCreated = null, DateTime? dateRoomModified = null, string playTime = null, DisplayMode? displayMode = null, LevelItemType? type = null, string creator = null, string tags = null, string description = null, DateTime? unlockTime = null)
	{
		LevelItem levelItem = ensureLevelItem(id);
		if (name != null)
		{
			levelItem.levelName = name;
		}
		if (creator != null)
		{
			levelItem.creator = creator;
		}
		if (tags != null)
		{
			levelItem.tags = tags;
		}
		if (size != null)
		{
			levelItem.size = size;
		}
		if (description != null)
		{
			levelItem.description = description;
		}
		if (textureName != null)
		{
			levelItem.textureName = textureName;
		}
		if (textureUrl != null)
		{
			levelItem.textureURL = textureUrl;
		}
		if (tokensMin != -1)
		{
			levelItem.tokensMin = tokensMin;
		}
		if (tokensMax != -1)
		{
			levelItem.tokensMax = tokensMax;
		}
		if (finished.HasValue)
		{
			levelItem.finished = finished.Value;
		}
		if (gotTrophy.HasValue)
		{
			levelItem.gotTrophy = gotTrophy.Value;
		}
		if (isNew.HasValue)
		{
			levelItem.isNew = isNew.Value;
		}
		if (isLocked.HasValue)
		{
			levelItem.isLocked = isLocked.Value;
		}
		if (unlockTime.HasValue)
		{
			levelItem.unlockTime = unlockTime.Value;
		}
		if (date != null)
		{
			levelItem.date = date;
		}
		if (lastSaveTimeInTicks.HasValue)
		{
			levelItem.lastSaveTimeInTicks = lastSaveTimeInTicks.Value;
		}
		if (dateFolderCreated.HasValue)
		{
			levelItem.dateFolderCreated = dateFolderCreated.Value;
		}
		if (dateRoomModified.HasValue)
		{
			levelItem.dateRoomModified = dateRoomModified.Value;
		}
		if (playTime != null)
		{
			levelItem.playTime = playTime;
		}
		if (displayMode.HasValue)
		{
			levelItem.displayMode = displayMode.Value;
		}
		if (type.HasValue)
		{
			levelItem.levelType = type.Value;
		}
		if (slider.HasValue)
		{
			levelItem.installProgress = slider.Value;
		}
	}

	public LevelItem ensureLevelItem(string id)
	{
		LevelItem levelItem = levelItems.Find((LevelItem x) => x.id == id);
		if (levelItem == null)
		{
			levelItem = new LevelItem(id);
			levelItems.Add(levelItem);
		}
		return levelItem;
	}
}
