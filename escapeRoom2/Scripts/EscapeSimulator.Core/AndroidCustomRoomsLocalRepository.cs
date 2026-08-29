using System;
using System.IO;
using UnityEngine;

public class AndroidCustomRoomsLocalRepository : CustomRoomsLocalRepository
{
	private static string communityLevelsRootPath => Path.Combine(Application.persistentDataPath, "CommunityLevels");

	public override string[] getInstalledRoomsFilenames()
	{
		if (!Directory.Exists(communityLevelsRootPath))
		{
			Directory.CreateDirectory(communityLevelsRootPath);
		}
		return Array.ConvertAll(Directory.GetFiles(communityLevelsRootPath), Path.GetFileNameWithoutExtension);
	}

	public override Menu.WorkshopRoomInfo readRoomInfo(string roomFilename)
	{
		byte[] infoBytes = File.ReadAllBytes(Path.Combine(communityLevelsRootPath, getFilenameWithExtension(roomFilename) + "Info"));
		return infoBytesToRoomInfo(infoBytes);
	}

	public override UnpackedCustomRoom readCustomRoom(string roomFilename)
	{
		byte[] roomBytes = File.ReadAllBytes(Path.Combine(communityLevelsRootPath, getFilenameWithExtension(roomFilename)));
		return deserializeRoomData(roomBytes);
	}

	public override bool isRoomInstalled(string roomFilename)
	{
		return File.Exists(Path.Combine(communityLevelsRootPath, getFilenameWithExtension(roomFilename)));
	}

	public override void deleteRoom(string roomFilename)
	{
		try
		{
			string path = Path.Combine(communityLevelsRootPath, getFilenameWithExtension(roomFilename));
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			string path2 = Path.Combine(communityLevelsRootPath, getFilenameWithExtension(roomFilename) + "Info");
			if (File.Exists(path2))
			{
				File.Delete(path2);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("[deleteRoom] Exception occurred: " + ex.Message);
		}
	}

	public override FlushRoomResult flushRoomToDisk(string roomFilename, byte[] room, out string error, byte[] info)
	{
		if (!Directory.Exists(communityLevelsRootPath))
		{
			Directory.CreateDirectory(communityLevelsRootPath);
		}
		try
		{
			File.WriteAllBytes(Path.Combine(communityLevelsRootPath, getFilenameWithExtension(roomFilename)), room);
			File.WriteAllBytes(Path.Combine(communityLevelsRootPath, getFilenameWithExtension(roomFilename) + "Info"), info);
			error = string.Empty;
			return FlushRoomResult.Success;
		}
		catch (Exception ex)
		{
			error = ex.Message;
			return FlushRoomResult.Fail;
		}
	}

	public override long getFreeSpace()
	{
		return long.MaxValue;
	}
}
