using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DM;
using Landfall.TABS;
using Landfall.TABS.Workshop;
using ModIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TFBGames;
using UnityEngine;
using UnityEngine.Events;

public static class DMNewContentManager
{
	public class NewIdEvent : UnityEvent<NewContentID, WorkshopContentType>
	{
	}

	[Serializable]
	public struct NewContentID
	{
		public int modId;

		public string name;

		public bool isSavedToLocal;

		public WorkshopContentType contentType;

		public override bool Equals(object obj)
		{
			if (!(obj is NewContentID newContentID))
			{
				return false;
			}
			if ((newContentID.modId == modId || newContentID.isSavedToLocal) && newContentID.name.ToLower().Trim() == name.ToLower().Trim() && newContentID.isSavedToLocal == isSavedToLocal)
			{
				return newContentID.contentType == contentType;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (((521381238 * -1521134295 + modId.GetHashCode()) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name)) * -1521134295 + isSavedToLocal.GetHashCode()) * -1521134295 + contentType.GetHashCode();
		}
	}

	[Serializable]
	public struct NewContentIDs
	{
		public List<NewContentID> idList;

		public bool Contains(NewContentID contentId)
		{
			foreach (NewContentID id in idList)
			{
				if (id.Equals(contentId))
				{
					return true;
				}
			}
			return false;
		}

		public bool Contains(WorkshopContentType contentType)
		{
			foreach (NewContentID id in idList)
			{
				if (id.contentType == contentType)
				{
					return true;
				}
			}
			return false;
		}
	}

	public static NewIdEvent onIdAdded = new NewIdEvent();

	public static NewIdEvent onIdRemoved = new NewIdEvent();

	private const int version = 3;

	private static string GetPath()
	{
		return Path.Combine(GamePaths.PersistentDataPath, $"NewContentIDs_{3}.txt");
	}

	private static void CleanUpDeprecatedFiles()
	{
		List<string> list = new List<string> { Path.Combine(GamePaths.PersistentDataPath, "NewContentIDs.txt") };
		for (int i = 2; i < 3; i++)
		{
			string item = Path.Combine(GamePaths.PersistentDataPath, $"NewContentIDs_{i}.txt");
			list.Add(item);
		}
		FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
		foreach (string path in list)
		{
			fileIO.FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exist)
			{
				if (exist)
				{
					fileIO.DeleteFile(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception e)
					{
						if (e != null)
						{
							Debug.LogError(e.Message);
						}
					});
				}
			});
		}
	}

	public static bool TryParseJson<T>(this string @this, out T result)
	{
		bool success = true;
		JsonSerializerSettings settings = new JsonSerializerSettings
		{
			Error = delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
			{
				success = false;
				args.ErrorContext.Handled = true;
			},
			MissingMemberHandling = MissingMemberHandling.Error
		};
		result = JsonConvert.DeserializeObject<T>(@this, settings);
		return success;
	}

	private static void GetNewContentIDs(Action<NewContentIDs, bool> contentIDsCallback)
	{
		FileIOWrapper fileIO = ServiceLocator.GetService<FileIOWrapper>();
		string path = GetPath();
		fileIO.FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
		{
			if (!exists)
			{
				contentIDsCallback?.Invoke(default(NewContentIDs), arg2: false);
			}
			else
			{
				fileIO.ReadAllText(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(string jsonText, Exception readException)
				{
					if (jsonText.TryParseJson<NewContentIDs>(out var result))
					{
						contentIDsCallback?.Invoke(result, result.idList != null);
					}
					else
					{
						fileIO.DeleteFile(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate
						{
							contentIDsCallback?.Invoke(default(NewContentIDs), arg2: false);
						});
					}
				});
			}
		});
	}

	private static NewContentIDs RemoveSuscribedDuplicates(NewContentIDs ids)
	{
		NewContentIDs result = new NewContentIDs
		{
			idList = new List<NewContentID>()
		};
		ContentDatabase contentDatabase = ContentDatabase.Instance();
		IEnumerable<TABSCampaignLevelAsset> userCampaignLevels = contentDatabase.GetUserCampaignLevels();
		IEnumerable<TABSCampaignAsset> userCampaigns = contentDatabase.GetUserCampaigns();
		IEnumerable<Faction> userFactions = contentDatabase.GetUserFactions();
		IEnumerable<UnitBlueprint> userUnitBlueprints = contentDatabase.GetUserUnitBlueprints();
		foreach (NewContentID id in ids.idList)
		{
			bool flag = true;
			switch (id.contentType)
			{
			case WorkshopContentType.Layout:
			case WorkshopContentType.Battle:
				foreach (TABSCampaignLevelAsset item in userCampaignLevels)
				{
					if (item.Entity.Name == id.name && !item.IsModIOLevel && !id.isSavedToLocal)
					{
						flag = false;
						break;
					}
				}
				break;
			case WorkshopContentType.Campaign:
				foreach (TABSCampaignAsset item2 in userCampaigns)
				{
					if (item2.Entity.Name == id.name && !item2.IsModCampaign && !id.isSavedToLocal)
					{
						flag = false;
						break;
					}
				}
				break;
			case WorkshopContentType.Faction:
				foreach (Faction item3 in userFactions)
				{
					if (item3.Entity.Name == id.name && !item3.IsModFaction && !id.isSavedToLocal)
					{
						flag = false;
						break;
					}
				}
				break;
			case WorkshopContentType.Unit:
				foreach (UnitBlueprint item4 in userUnitBlueprints)
				{
					if (item4.Entity.Name == id.name && !item4.IsModUnit && !id.isSavedToLocal)
					{
						flag = false;
						break;
					}
				}
				break;
			}
			foreach (NewContentID id2 in ids.idList)
			{
				if (id.modId == id2.modId && id.isSavedToLocal != id2.isSavedToLocal && !id.isSavedToLocal)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				result.idList.Add(id);
			}
		}
		return result;
	}

	private static NewContentIDs RemoveDisabledIds(NewContentIDs ids)
	{
		if (LocalUser.AuthenticationState != AuthenticationState.ValidToken)
		{
			return ids;
		}
		List<NewContentID> list = new List<NewContentID>();
		List<int> enabledModIds = LocalUser.EnabledModIds;
		foreach (NewContentID id in ids.idList)
		{
			foreach (int item in enabledModIds)
			{
				if (id.isSavedToLocal || (id.modId != 0 && id.modId == item))
				{
					list.Add(id);
					break;
				}
			}
		}
		return new NewContentIDs
		{
			idList = list
		};
	}

	public static void RefreshNewContentIDs()
	{
		GetNewContentIDs(delegate(NewContentIDs contentIDs, bool success)
		{
			if (success)
			{
				SetNewContentIDs(contentIDs);
			}
		});
	}

	private static void SetNewContentIDs(NewContentIDs ids)
	{
		CleanUpDeprecatedFiles();
		ids = RemoveSuscribedDuplicates(ids);
		ids = RemoveDisabledIds(ids);
		string path = GetPath();
		string contents = JsonUtility.ToJson(ids);
		ServiceLocator.GetService<FileIOWrapper>().WriteAllText(path, contents, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception e)
		{
			if (e != null)
			{
				Debug.LogError(e.Message);
			}
		});
	}

	public static void AddNewContentID(int id, string name, bool isSavedToLocal, WorkshopContentType contentType)
	{
		NewContentID newId = CreateNewContentId(id, name, isSavedToLocal, contentType);
		GetNewContentIDs(delegate(NewContentIDs contentIDs, bool success)
		{
			if (!success)
			{
				SetNewContentIDs(new NewContentIDs
				{
					idList = new List<NewContentID>
					{
						new NewContentID
						{
							modId = id,
							name = name,
							isSavedToLocal = isSavedToLocal,
							contentType = contentType
						}
					}
				});
			}
			else if (!contentIDs.Contains(newId))
			{
				contentIDs.idList.Add(newId);
				SetNewContentIDs(contentIDs);
				onIdAdded?.Invoke(newId, contentType);
			}
		});
	}

	public static void AddNewContentID(ModProfile profile, bool isSavedToLocal)
	{
		ModTag[] tags = profile.tags;
		for (int i = 0; i < tags.Length; i++)
		{
			if (Enum.TryParse<WorkshopContentType>(tags[i].name, out var result))
			{
				AddNewContentID(profile.id, profile.name, isSavedToLocal, result);
				break;
			}
		}
	}

	public static void RemoveNewContentID(int id, string name, bool isSavedToLocal, WorkshopContentType contentType)
	{
		GetNewContentIDs(delegate(NewContentIDs contentIDs, bool success)
		{
			if (success)
			{
				NewContentID newContentID = CreateNewContentId(id, name, isSavedToLocal, contentType);
				if (contentIDs.Contains(newContentID))
				{
					contentIDs.idList.Remove(newContentID);
					SetNewContentIDs(contentIDs);
					onIdRemoved?.Invoke(newContentID, contentType);
				}
			}
		});
	}

	public static void RemoveNewContentID(ModProfile profile, bool isSavedToLocal)
	{
		ModTag[] tags = profile.tags;
		for (int i = 0; i < tags.Length; i++)
		{
			if (Enum.TryParse<WorkshopContentType>(tags[i].name, out var result))
			{
				RemoveNewContentID(profile.id, profile.name, isSavedToLocal, result);
				break;
			}
		}
	}

	public static void IsContentNew(int id, string name, bool isSavedToLocal, WorkshopContentType contentType, Action<bool> callback)
	{
		GetNewContentIDs(delegate(NewContentIDs contentIDs, bool success)
		{
			if (!success)
			{
				callback?.Invoke(obj: false);
			}
			else
			{
				NewContentID contentId = CreateNewContentId(id, name, isSavedToLocal, contentType);
				callback?.Invoke(contentIDs.Contains(contentId));
			}
		});
	}

	public static void HasNewContentOfType(WorkshopContentType contentType, Action<bool> callback)
	{
		GetNewContentIDs(delegate(NewContentIDs contentIDs, bool success)
		{
			if (!success)
			{
				callback?.Invoke(obj: false);
			}
			else
			{
				callback?.Invoke(contentIDs.Contains(contentType));
			}
		});
	}

	public static void HasNewContentOfType(WorkshopContentType contentType, bool isSavedToLocal, Action<bool> callback)
	{
		GetNewContentIDs(delegate(NewContentIDs contentIDs, bool success)
		{
			if (!success)
			{
				callback?.Invoke(obj: false);
			}
			else if (contentIDs.idList.Any((NewContentID id) => id.contentType == contentType && id.isSavedToLocal == isSavedToLocal))
			{
				callback?.Invoke(obj: true);
			}
			else
			{
				callback?.Invoke(obj: false);
			}
		});
	}

	public static void HasNewContent(Action<bool> callback)
	{
		GetNewContentIDs(delegate(NewContentIDs contentIDs, bool success)
		{
			if (!success)
			{
				callback?.Invoke(obj: false);
			}
			else
			{
				callback?.Invoke(contentIDs.idList.Count > 0);
			}
		});
	}

	public static NewContentID CreateNewContentId(int id, string name, bool isSavedToLocal, WorkshopContentType contentType)
	{
		return new NewContentID
		{
			modId = id,
			name = name,
			isSavedToLocal = isSavedToLocal,
			contentType = contentType
		};
	}
}
