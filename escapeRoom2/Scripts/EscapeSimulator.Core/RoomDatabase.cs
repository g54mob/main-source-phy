using System;
using System.Collections.Generic;

public static class RoomDatabase
{
	public class Pack
	{
		public string name;

		public DLC dlc;

		public List<Room> rooms = new List<Room>();

		public string assetsPath;

		public bool isDLC => dlc != DLC.None;
	}

	public class Room
	{
		public string name;

		public Pack pack;

		public bool shouldBeBuilt = true;

		public bool shouldBeDisplayed = true;

		public int localVersion;

		public bool isInDemo;

		public bool isAvailableOnOculus;

		public DateTime unlockTime;

		public int index
		{
			get
			{
				string text = name;
				return int.Parse(text[text.Length - 1].ToString());
			}
		}
	}

	public static bool forgeBuildAll;

	public const string SPLASH_SCREEN_SCENE_NAME = "SplashScreen";

	public const string TUTORIAL_SCENE_NAME = "Tutorial1";

	public const string LOBBY_SCENE_NAME = "Lobby1";

	public const string ROOM_EDITOR_SCENE_NAME = "RoomEditor";

	public const string CUSTOM_LEVEL_NAME = "CustomLevel";

	public const string SECRET_LEVEL_NAME = "SecretLevel1";

	private static readonly string[] demoLevels;

	private static List<Pack> packs;

	static RoomDatabase()
	{
		forgeBuildAll = false;
		demoLevels = new string[5] { "Tutorial1", "Lobby1", "Dracula1", "DraculaDarkest1", "Space1" };
		packs = new List<Pack>();
		defineDefaultPack("Test", 1, DLC.None, "Assets/__Test/Scenes");
		defineDefaultPack("Lobby", 1, DLC.None, "Assets/__Lobby/Scenes");
		defineDefaultPack("Tutorial", 1, DLC.None, "Assets/__Tutorial/Scenes");
		defineDefaultPack("SecretLevel", 1, DLC.None, "Assets/__SecretLevel/Scenes");
		defineDefaultPack("Dracula", 4, DLC.None, "Assets/__Dracula/Scenes");
		defineDefaultPack("Space", 4, DLC.None, "Assets/__Space/Scenes");
		defineDefaultPack("Pirate", 4, DLC.None, "Assets/__Pirate/Scenes");
		defineDefaultPack("Zombie", 4, DLC.None, "Assets/__Zombie/Scenes");
		defineDefaultPack("DraculaDarkest", 4, DLC.None, "Assets/_DarkestPuzzles/_Dracula/Scenes");
		defineDefaultPack("SpaceDarkest", 4, DLC.None, "Assets/_DarkestPuzzles/_Space/Scenes");
		defineDefaultPack("PirateDarkest", 4, DLC.None, "Assets/_DarkestPuzzles/_Pirates/Scenes");
		defineDefaultPack("ZombieDarkest", 4, DLC.None, "Assets/_DarkestPuzzles/_Zombie/Scenes");
		string[] array = demoLevels;
		for (int i = 0; i < array.Length; i++)
		{
			getRoom(array[i]).isInDemo = true;
		}
		getRoom("Zombie1").shouldBeBuilt = false;
		getRoom("Zombie2").shouldBeBuilt = false;
		getRoom("Zombie3").shouldBeBuilt = false;
		getRoom("Zombie4").shouldBeBuilt = false;
		if (Is.DebugBuild)
		{
			packs.ForEach(delegate(Pack pack)
			{
				pack.rooms.ForEach(delegate(Room room)
				{
					room.isAvailableOnOculus = true;
				});
			});
		}
		else
		{
			if (!Is.Android)
			{
				return;
			}
			foreach (DLC oculusPlannedDLC in VR.oculusPlannedDLCs)
			{
				if (VR.oculusAvailableDLCs.Contains(oculusPlannedDLC))
				{
					getPack(oculusPlannedDLC.ToString()).rooms.ForEach(delegate(Room room)
					{
						room.isAvailableOnOculus = true;
					});
					continue;
				}
				DateTime comingSoonUnlockTime = DateTime.UtcNow.AddYears(10);
				getPack(oculusPlannedDLC.ToString()).rooms.ForEach(delegate(Room room)
				{
					room.unlockTime = comingSoonUnlockTime;
				});
			}
		}
		static void defineDefaultPack(string name, int numOfRooms, DLC dlc, string assetsPath)
		{
			Pack pack = new Pack
			{
				name = name,
				dlc = dlc,
				assetsPath = assetsPath
			};
			for (int j = 0; j < numOfRooms; j++)
			{
				string name2 = name + (j + 1);
				int assetBundleVersion = Version.getAssetBundleVersion(name2);
				Room room = new Room
				{
					name = name2,
					localVersion = assetBundleVersion
				};
				addToPack(pack, room);
				static void addToPack(Pack pack2, Room room2)
				{
					pack2.rooms.Add(room2);
					room2.pack = pack2;
				}
			}
			packs.Add(pack);
		}
	}

	public static void setBuildAll()
	{
		forgeBuildAll = true;
		foreach (Pack pack in packs)
		{
			foreach (Room room in pack.rooms)
			{
				room.shouldBeBuilt = true;
			}
		}
	}

	public static string getVersion(string levelId)
	{
		return getRoom(levelId)?.localVersion.ToString() ?? string.Empty;
	}

	public static Pack getPack(DLC dlc)
	{
		return packs.Find((Pack pack) => pack.name == dlc.ToString());
	}

	public static Pack getPack(OriginalRoom originalRoom)
	{
		return packs.Find((Pack pack) => pack.name == originalRoom.ToString());
	}

	public static Pack getPack(string name)
	{
		return packs.Find((Pack pack) => pack.name == name);
	}

	public static List<string> getAllPacks()
	{
		return packs.ConvertAll((Pack pack) => pack.name);
	}

	public static bool isDlc(string levelId)
	{
		return getRoom(levelId)?.pack.isDLC ?? false;
	}

	public static DLC getDlc(string levelId)
	{
		return getRoom(levelId)?.pack.dlc ?? DLC.None;
	}

	public static Room getRoom(string roomName)
	{
		if (string.IsNullOrEmpty(roomName))
		{
			return null;
		}
		string packName = roomName.Substring(0, roomName.Length - 1);
		return packs.Find((Pack x) => x.name == packName)?.rooms.Find((Room x) => x.name == roomName);
	}

	public static Room getRoomByScenePath(string scenePath)
	{
		foreach (Pack pack in packs)
		{
			foreach (Room allRoom in getAllRooms(pack.name, includeUnbuiltRooms: true))
			{
				if (scenePath.Contains(allRoom.name))
				{
					return allRoom;
				}
			}
		}
		return null;
	}

	public static List<Room> getAllRooms(string packName, bool includeUnbuiltRooms = false)
	{
		List<Room> list = new List<Room>(packs.Find((Pack x) => x.name == packName).rooms);
		if (!includeUnbuiltRooms)
		{
			list.RemoveAll((Room x) => !x.shouldBeBuilt);
		}
		return list;
	}

	public static List<Room> getAllRooms(DLC dlc, bool includeUnbuiltRooms = false)
	{
		return getAllRooms(dlc.ToString(), includeUnbuiltRooms);
	}

	public static List<Room> getAllRooms(OriginalRoom originalRoom, bool includeUnbuiltRooms = false)
	{
		return getAllRooms(originalRoom.ToString(), includeUnbuiltRooms);
	}

	public static List<Room> getAllRooms(DarkestPuzzles darkestPuzzles, bool includeUnbuiltRooms = false)
	{
		return getAllRooms(darkestPuzzles.ToString(), includeUnbuiltRooms);
	}

	public static List<Room> getAllRooms(bool includeUnbuiltRooms = false)
	{
		List<Room> list = new List<Room>();
		foreach (Pack pack in packs)
		{
			list.AddRange(getAllRooms(pack.name, includeUnbuiltRooms));
		}
		return list;
	}

	public static IEnumerable<string> getAllScenePaths(bool includeFileExtension = false)
	{
		foreach (string packName in getAllPacks())
		{
			string fileExtension = (includeFileExtension ? ".unity" : string.Empty);
			int roomCount = getPack(packName).rooms.Count;
			string path = getPack(packName).assetsPath;
			for (int roomIndex = 1; roomIndex <= roomCount; roomIndex++)
			{
				yield return $"{path}/{packName}{roomIndex}{fileExtension}";
			}
		}
	}

	public static List<Room> getAllDarkestPuzzlesRooms(bool includeUnbuiltRooms = false)
	{
		List<Room> list = new List<Room>();
		foreach (Pack pack in packs)
		{
			if (pack.name.Contains("Darkest"))
			{
				list.AddRange(getAllRooms(pack.name, includeUnbuiltRooms));
			}
		}
		return list;
	}

	public static List<Room> getAllRegularRooms(bool includeUnbuiltRooms = false)
	{
		List<Room> list = new List<Room>();
		foreach (Pack pack in packs)
		{
			if (!(pack.name == "Test") && !(pack.name == "Lobby") && !(pack.name == "Tutorial") && !(pack.name == "SecretLevel") && !pack.name.Contains("Darkest"))
			{
				list.AddRange(getAllRooms(pack.name, includeUnbuiltRooms));
			}
		}
		return list;
	}

	public static List<Room> getPackOfARoom(string roomName)
	{
		return getRoom(roomName).pack.rooms;
	}

	public static bool isLevelScene(string sceneName)
	{
		string text = sceneName.ToLower();
		foreach (string allPack in getAllPacks())
		{
			foreach (Room allRoom in getAllRooms(allPack, includeUnbuiltRooms: true))
			{
				if (text == allRoom.name.ToLower())
				{
					return true;
				}
			}
		}
		return false;
	}

	public static string roomNameToLocalizationKey(string name)
	{
		return (name + "title").ToLower();
	}
}
