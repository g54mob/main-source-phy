using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public abstract class CustomRoomsLocalRepository
{
	public enum FlushRoomResult
	{
		Success = 0,
		FailNoMemory = 1,
		Fail = 2
	}

	private static CustomRoomsLocalRepository db;

	public static CustomRoomsLocalRepository get()
	{
		if (db == null)
		{
			db = new DefaultCustomRoomsLocalRepository();
		}
		return db;
	}

	public virtual string[] getInstalledRoomsFilenames()
	{
		return Array.Empty<string>();
	}

	public virtual Menu.WorkshopRoomInfo readRoomInfo(string roomFilename)
	{
		return null;
	}

	public virtual UnpackedCustomRoom readCustomRoom(string roomFilename)
	{
		return null;
	}

	public virtual bool isRoomInstalled(string roomFilename)
	{
		return false;
	}

	public virtual void deleteRoom(string roomFilename)
	{
	}

	public virtual FlushRoomResult flushRoomToDisk(string roomFilename, byte[] room, out string error, byte[] info)
	{
		error = "";
		return FlushRoomResult.Fail;
	}

	public virtual long getFreeSpace()
	{
		return -1L;
	}

	protected Menu.WorkshopRoomInfo deserializeRoomInfo(string roomFilename, byte[] bytes)
	{
		UnpackedCustomRoom unpackedCustomRoom = deserializeRoomData(bytes, withCustomFiles: false);
		Menu.WorkshopRoomInfo workshopRoomInfo = new Menu.WorkshopRoomInfo();
		if (unpackedCustomRoom.roomData != null)
		{
			Debug.Log("Found file: " + roomFilename);
			string[] array = roomFilename.Split("_");
			workshopRoomInfo.roomId = ulong.Parse(getFilenameWithoutExtension(array[0]));
			workshopRoomInfo.pineServerModified = getFilenameWithoutExtension(array[1]);
			Debug.Log("info.pineServerModified: " + workshopRoomInfo.pineServerModified);
			workshopRoomInfo.roomTitle = unpackedCustomRoom.roomData.name;
			workshopRoomInfo.roomDescription = unpackedCustomRoom.roomData.description;
			workshopRoomInfo.roomTags = new List<string>(unpackedCustomRoom.roomData.tags);
			workshopRoomInfo.roomWalkthrough = new List<string>(unpackedCustomRoom.roomData.walkthrough);
			workshopRoomInfo.roomLocalPath = roomFilename;
			workshopRoomInfo.helperRoomInfoPopulated = true;
			workshopRoomInfo.roomPreviewImage = unpackedCustomRoom.previewTexture;
			workshopRoomInfo.isInYourRooms = false;
			workshopRoomInfo.fileSize = bytes.Length;
			workshopRoomInfo.authorName = unpackedCustomRoom.authorName;
			workshopRoomInfo.finishState = PlayerSave.getFinishState(workshopRoomInfo.roomId.ToString());
			return workshopRoomInfo;
		}
		return null;
	}

	public Menu.WorkshopRoomInfo infoBytesToRoomInfo(byte[] infoBytes)
	{
		Menu.WorkshopRoomInfo workshopRoomInfo = new Menu.WorkshopRoomInfo();
		if (infoBytes != null)
		{
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(infoBytes)))
			{
				workshopRoomInfo.roomId = binaryReader.ReadUInt64();
				workshopRoomInfo.pineServerModified = binaryReader.ReadString();
				workshopRoomInfo.roomTitle = binaryReader.ReadString();
				workshopRoomInfo.roomDescription = binaryReader.ReadString();
				List<string> list = new List<string>();
				int num = binaryReader.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					list.Add(binaryReader.ReadString());
				}
				workshopRoomInfo.roomTags = list;
				List<string> list2 = new List<string>();
				int num2 = binaryReader.ReadInt32();
				for (int j = 0; j < num2; j++)
				{
					list2.Add(binaryReader.ReadString());
				}
				workshopRoomInfo.roomWalkthrough = list2;
				workshopRoomInfo.roomLocalPath = binaryReader.ReadString();
				workshopRoomInfo.helperRoomInfoPopulated = true;
				int count = binaryReader.ReadInt32();
				workshopRoomInfo.roomPreviewImage = new Texture2D(2, 2, TextureFormat.RGBA32, mipChain: true);
				workshopRoomInfo.roomPreviewImage.LoadImage(binaryReader.ReadBytes(count), markNonReadable: true);
				workshopRoomInfo.isInYourRooms = false;
				workshopRoomInfo.fileSize = binaryReader.ReadInt32();
				workshopRoomInfo.authorName = binaryReader.ReadString();
				workshopRoomInfo.finishState = (PlayerSave.FinishState)binaryReader.ReadInt32();
				return workshopRoomInfo;
			}
		}
		return null;
	}

	public byte[] roomDataToInfoBytes(string roomFilename, byte[] roomBytes, string authorName)
	{
		UnpackedCustomRoom unpackedCustomRoom = new UnpackedCustomRoom();
		MemoryStream memoryStream = new MemoryStream(roomBytes);
		byte[] array = new byte[0];
		using (BinaryReader binaryReader = new BinaryReader(memoryStream))
		{
			int num = binaryReader.ReadInt32();
			int num2 = -1;
			byte[] array2 = null;
			for (int i = 0; i < num; i++)
			{
				string text = binaryReader.ReadString();
				num2 = binaryReader.ReadInt32();
				array2 = binaryReader.ReadBytes(num2);
				if (text.EndsWith(".room"))
				{
					unpackedCustomRoom.roomData = JsonUtility.FromJson<RoomData>(Encoding.Default.GetString(array2));
				}
				if (text.ToLower().Equals("Preview.jpg".ToLower()))
				{
					array = array2;
				}
			}
		}
		memoryStream.Dispose();
		if (unpackedCustomRoom.roomData != null)
		{
			MemoryStream memoryStream2 = new MemoryStream();
			using BinaryWriter binaryWriter = new BinaryWriter(memoryStream2);
			string[] array3 = roomFilename.Split("_");
			ulong value = ulong.Parse(getFilenameWithoutExtension(array3[0]));
			binaryWriter.Write(value);
			binaryWriter.Write(getFilenameWithoutExtension(array3[1]));
			binaryWriter.Write(unpackedCustomRoom.roomData.name);
			binaryWriter.Write(unpackedCustomRoom.roomData.description);
			binaryWriter.Write(unpackedCustomRoom.roomData.tags.Count);
			for (int j = 0; j < unpackedCustomRoom.roomData.tags.Count; j++)
			{
				binaryWriter.Write(unpackedCustomRoom.roomData.tags[j]);
			}
			binaryWriter.Write(unpackedCustomRoom.roomData.walkthrough.Count);
			for (int k = 0; k < unpackedCustomRoom.roomData.walkthrough.Count; k++)
			{
				binaryWriter.Write(unpackedCustomRoom.roomData.walkthrough[k]);
			}
			binaryWriter.Write(roomFilename);
			binaryWriter.Write(array.Length);
			binaryWriter.Write(array);
			binaryWriter.Write(roomBytes.Length);
			binaryWriter.Write(authorName);
			binaryWriter.Write((int)PlayerSave.getFinishState(value.ToString()));
			binaryWriter.Flush();
			return memoryStream2.ToArray();
		}
		return null;
	}

	public UnpackedCustomRoom deserializeRoomData(byte[] roomBytes, bool withCustomFiles = true)
	{
		UnpackedCustomRoom unpackedCustomRoom = new UnpackedCustomRoom();
		MemoryStream memoryStream = new MemoryStream(roomBytes);
		using (BinaryReader binaryReader = new BinaryReader(memoryStream))
		{
			int num = binaryReader.ReadInt32();
			string text = "";
			int num2 = -1;
			byte[] array = null;
			for (int i = 0; i < num; i++)
			{
				text = binaryReader.ReadString();
				num2 = binaryReader.ReadInt32();
				array = binaryReader.ReadBytes(num2);
				if (text.EndsWith(".room"))
				{
					unpackedCustomRoom.roomData = JsonUtility.FromJson<RoomData>(Encoding.Default.GetString(array));
				}
				else if (withCustomFiles)
				{
					CustomRoomDataFile.CustomType customType = CustomRoomDataFile.getCustomType(text);
					unpackedCustomRoom.customFiles.Add(new CustomRoomDataFile(text, customType, array));
				}
				if (text.ToLower().Equals("Preview.jpg".ToLower()))
				{
					unpackedCustomRoom.previewTexture = new Texture2D(2, 2, TextureFormat.RGBA32, mipChain: true);
					unpackedCustomRoom.previewTexture.LoadImage(array, markNonReadable: true);
				}
			}
		}
		memoryStream.Dispose();
		return unpackedCustomRoom;
	}

	public string getFileIdName(string roomId, string pineServerModified)
	{
		return roomId + "_" + pineServerModified;
	}

	public string getFilenameWithExtension(string roomFilename)
	{
		string text = roomFilename;
		if (!roomFilename.EndsWith(".roomdata"))
		{
			text += ".roomdata";
		}
		return text;
	}

	public string getFilenameWithoutExtension(string roomFilenameWithExtension)
	{
		return Path.GetFileNameWithoutExtension(roomFilenameWithExtension);
	}
}
