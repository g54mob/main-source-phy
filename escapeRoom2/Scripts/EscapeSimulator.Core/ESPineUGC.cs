using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ESPineUGC : ESUGC
{
	private class InstallRoomWebRequest
	{
		public UnityWebRequest downloadLinkwebRequest;

		public UnityWebRequest roomWebRequest;

		public string levelId;
	}

	private CustomRoomsLocalRepository repo = CustomRoomsLocalRepository.get();

	private Action<string, ESUGCChange, ESUGC> onChange;

	private List<InstallRoomWebRequest> installingRooms = new List<InstallRoomWebRequest>();

	public override void init(Action<string, ESUGCChange, ESUGC> onChange)
	{
		this.onChange = onChange;
	}

	public override void dispose()
	{
	}

	public override void update()
	{
		for (int num = installingRooms.Count - 1; num >= 0; num--)
		{
			InstallRoomWebRequest installRoomWebRequest = installingRooms[num];
			if (installRoomWebRequest.downloadLinkwebRequest != null && installRoomWebRequest.downloadLinkwebRequest.isDone)
			{
				UnityWebRequest unityWebRequest = new UnityWebRequest(installRoomWebRequest.downloadLinkwebRequest.downloadHandler.text);
				unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
				unityWebRequest.SendWebRequest();
				installRoomWebRequest.roomWebRequest = unityWebRequest;
				installRoomWebRequest.downloadLinkwebRequest = null;
			}
			if (installRoomWebRequest.roomWebRequest != null && installRoomWebRequest.roomWebRequest.isDone)
			{
				installingRooms.RemoveAt(num);
				byte[] data = installRoomWebRequest.roomWebRequest.downloadHandler.data;
				byte[] info = repo.roomDataToInfoBytes(installRoomWebRequest.levelId, data, "");
				string error;
				switch (repo.flushRoomToDisk(installRoomWebRequest.levelId, data, out error, info))
				{
				case CustomRoomsLocalRepository.FlushRoomResult.FailNoMemory:
					onChange(installRoomWebRequest.levelId, ESUGCChange.InstallFail_NoEnoughMemory, this);
					break;
				case CustomRoomsLocalRepository.FlushRoomResult.Fail:
					onChange(installRoomWebRequest.levelId, ESUGCChange.InstallFail, this);
					break;
				case CustomRoomsLocalRepository.FlushRoomResult.Success:
					onChange(installRoomWebRequest.levelId, ESUGCChange.Installed, this);
					break;
				}
			}
		}
	}

	public override void refreshAllInstalledRooms(Action<RefreshRoomData> onGetNameAndURL)
	{
		string[] installedRoomsFilenames = repo.getInstalledRoomsFilenames();
		List<RefreshRoomData> list = new List<RefreshRoomData>();
		string[] array = installedRoomsFilenames;
		foreach (string text in array)
		{
			Menu.WorkshopRoomInfo levelInfo = repo.readRoomInfo(text);
			DateTime.TryParse(levelInfo.pineServerModified, out var result);
			RefreshRoomData refreshRoomData = new RefreshRoomData(text, levelInfo.roomId, levelInfo.roomTitle, levelInfo.roomPreviewImageUrl, result, this);
			RefreshRoomData refreshRoomData2 = list.Find((RefreshRoomData x) => x.steamId == levelInfo.roomId);
			RefreshRoomData refreshRoomData3 = refreshRoomData2;
			if (refreshRoomData2 != null && DateTime.Compare(refreshRoomData2.modified, result) > 0)
			{
				refreshRoomData3 = refreshRoomData;
			}
			refreshRoomData.shouldBeDeleted = refreshRoomData3 == refreshRoomData;
			list.Add(refreshRoomData);
		}
		foreach (RefreshRoomData item in list)
		{
			onGetNameAndURL(item);
		}
	}

	public override CustomRoomState customRoomState(string levelId)
	{
		if (installingRooms.Find((InstallRoomWebRequest x) => x.levelId == levelId) != null)
		{
			return CustomRoomState.Installing;
		}
		if (repo.isRoomInstalled(levelId))
		{
			return CustomRoomState.Installed;
		}
		return CustomRoomState.NotInstalled;
	}

	public override bool installRoom(string levelId)
	{
		if (!isCustomLevel(levelId))
		{
			return false;
		}
		if (customRoomState(levelId) != CustomRoomState.NotInstalled)
		{
			return false;
		}
		if (installingRooms.Count > 1)
		{
			return false;
		}
		string[] array = levelId.Split("_");
		string text = "half";
		string text2 = "/download/" + array[0] + "/" + array[1] + "/" + text;
		UnityWebRequest downloadLinkwebRequest = Menu.sendWorkshopWebRequest(text2);
		installingRooms.Add(new InstallRoomWebRequest
		{
			downloadLinkwebRequest = downloadLinkwebRequest,
			levelId = levelId
		});
		Debug.Log("Downloading: '" + levelId + "' with '" + text2 + "'");
		onChange(levelId, ESUGCChange.Modify, this);
		return true;
	}

	public override void uninstallRoom(string levelId)
	{
		repo.deleteRoom(levelId);
		onChange(levelId, ESUGCChange.Uninstalled, this);
	}

	public override float getInstallProgress(string levelId)
	{
		InstallRoomWebRequest installRoomWebRequest = installingRooms.Find((InstallRoomWebRequest x) => x.levelId == levelId);
		if (installRoomWebRequest != null)
		{
			float a = ((installRoomWebRequest.downloadLinkwebRequest != null) ? Mathf.Min(installRoomWebRequest.downloadLinkwebRequest.downloadProgress, 0.5f) : (-1f));
			float b = ((installRoomWebRequest.roomWebRequest != null) ? installRoomWebRequest.roomWebRequest.downloadProgress : (-1f));
			return Mathf.Max(a, b);
		}
		return -1f;
	}

	public override bool isCustomLevel(string levelId)
	{
		if (string.IsNullOrEmpty(levelId))
		{
			return false;
		}
		string[] array = levelId.Split("_");
		if (array.Length == 2 && ulong.TryParse(array[0], out var result))
		{
			return ulong.TryParse(array[1], out result);
		}
		return false;
	}

	public override string getPath(string levelId)
	{
		return repo.getFilenameWithExtension(levelId);
	}

	public override ulong getId(string levelId)
	{
		return ulong.Parse(levelId.Split("_")[0]);
	}

	public override void refreshSpecificRooms(string[] roomIds, Action<RefreshRoomData> onGetNameAndURL)
	{
		for (int i = 0; i < roomIds.Length; i++)
		{
			string steamId = roomIds[i].Split("_")[0];
			Executor.executeCoroutine(refreshRoom(steamId));
		}
		IEnumerator refreshRoom(string text)
		{
			for (string workshopToken = Menu.getWorkshopToken(); workshopToken == null; workshopToken = Menu.getWorkshopToken())
			{
				yield return null;
			}
			UnityWebRequest request = Menu.sendWorkshopWebRequest("/query/exact_slim/" + text, autoSend: false);
			yield return request.SendWebRequest();
			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError($"Pine workshop request to exact failed: {text} {request.responseCode} {request.result}");
				onGetNameAndURL(new RefreshRoomData(text, "ERROR", "", DateTime.Now, this));
			}
			else
			{
				Menu.PineRoomsData pineRoomsData = JsonUtility.FromJson<Menu.PineRoomsData>(request.downloadHandler.text);
				if (pineRoomsData == null || pineRoomsData.Rooms == null || pineRoomsData.Rooms.Count <= 0)
				{
					Debug.LogError($"Data from server is invalid. {pineRoomsData} {request.downloadHandler.text}");
					onGetNameAndURL(new RefreshRoomData(text, "ERROR", "", DateTime.Now, this));
				}
				else
				{
					Menu.PineRoomData pineRoomData = pineRoomsData.Rooms[0];
					Debug.Log("Successfully got info for " + pineRoomData.id + " " + pineRoomData.Name);
					if (!DateTime.TryParse(pineRoomData.Modified, out var result))
					{
						result = DateTime.Now;
					}
					onGetNameAndURL(new RefreshRoomData(pineRoomData.id, pineRoomData.Name, pineRoomData.ImageURL, result, this));
				}
			}
		}
	}
}
