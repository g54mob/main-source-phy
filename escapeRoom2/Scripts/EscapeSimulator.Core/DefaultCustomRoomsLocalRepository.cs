using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DefaultCustomRoomsLocalRepository : CustomRoomsLocalRepository
{
	private string basePath
	{
		get
		{
			if (Is.Switch && !Is.Editor)
			{
				return Application.dataPath.Replace("Assets", "SavedRooms");
			}
			return Path.Combine(Application.persistentDataPath, "PineSavedRooms");
		}
	}

	public override string[] getInstalledRoomsFilenames()
	{
		if (!Directory.Exists(basePath))
		{
			Directory.CreateDirectory(basePath);
		}
		List<string> filenames = new List<string>();
		Array.ForEach(Directory.GetFiles(basePath), delegate(string path)
		{
			filenames.Add(getFilenameWithoutExtension(Path.GetFileName(path)));
		});
		return filenames.ToArray();
	}

	public override Menu.WorkshopRoomInfo readRoomInfo(string roomFilename)
	{
		roomFilename = getFilenameWithExtension(roomFilename);
		byte[] array = File.ReadAllBytes(basePath + "/" + roomFilename + "Info");
		if (array != null)
		{
			return infoBytesToRoomInfo(array);
		}
		return null;
	}

	public override UnpackedCustomRoom readCustomRoom(string roomFilename)
	{
		roomFilename = getFilenameWithExtension(roomFilename);
		string path = basePath + "/" + roomFilename;
		return deserializeRoomData(File.ReadAllBytes(path));
	}

	public override bool isRoomInstalled(string roomFilename)
	{
		roomFilename = getFilenameWithExtension(roomFilename);
		if (File.Exists(basePath + "/" + roomFilename))
		{
			return true;
		}
		return false;
	}

	public override void deleteRoom(string roomFilename)
	{
		if (!Directory.Exists(basePath))
		{
			return;
		}
		string[] files = Directory.GetFiles(basePath);
		foreach (string text in files)
		{
			if (text.Contains(roomFilename))
			{
				File.Delete(text);
				Debug.Log("deleting room: " + text);
			}
		}
	}

	public override FlushRoomResult flushRoomToDisk(string roomFilename, byte[] room, out string error, byte[] info)
	{
		roomFilename = getFilenameWithExtension(roomFilename);
		if (!Directory.Exists(basePath))
		{
			Directory.CreateDirectory(basePath);
		}
		File.WriteAllBytes(basePath + "/" + roomFilename, room);
		File.WriteAllBytes(basePath + "/" + roomFilename + "Info", info);
		error = "";
		return FlushRoomResult.Success;
	}

	public override long getFreeSpace()
	{
		return long.MaxValue;
	}
}
