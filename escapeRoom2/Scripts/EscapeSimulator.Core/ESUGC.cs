using System;
using System.Collections.Generic;

public abstract class ESUGC
{
	public class CachedRoomData
	{
		public string name;

		public string imageUrl;

		public DateTime modified;

		public string installLocation;
	}

	public static ESUGC standard;

	public static ESUGC pineOverride;

	public static Dictionary<string, CachedRoomData> cachedRoomData = new Dictionary<string, CachedRoomData>();

	private static bool inCrossplatformLobby = false;

	public static ESUGC current
	{
		get
		{
			init();
			if (!inCrossplatformLobby)
			{
				return standard;
			}
			return pineOverride;
		}
	}

	public static void init()
	{
		if (standard == null)
		{
			standard = new ESSteamUGC();
			pineOverride = new ESPineUGC();
			standard.internalInit();
			pineOverride.internalInit();
		}
	}

	public static void setCrossplatformLobby(bool inCrossplatformLobby)
	{
		ESUGC.inCrossplatformLobby = inCrossplatformLobby;
	}

	protected virtual void internalInit()
	{
	}

	public virtual void init(Action<string, ESUGCChange, ESUGC> onChange)
	{
	}

	public virtual void dispose()
	{
	}

	public virtual void update()
	{
	}

	public virtual void refreshAllInstalledRooms(Action<RefreshRoomData> onGetNameAndURL)
	{
	}

	public virtual CustomRoomState customRoomState(string levelId)
	{
		return CustomRoomState.NotInstalled;
	}

	public virtual bool installRoom(string levelId)
	{
		return false;
	}

	public virtual void uninstallRoom(string levelId)
	{
	}

	public virtual float getInstallProgress(string levelId)
	{
		return -1f;
	}

	public virtual bool isCustomLevel(string levelId)
	{
		return false;
	}

	public virtual string getPath(string levelId)
	{
		return "";
	}

	public virtual ulong getId(string levelId)
	{
		return 0uL;
	}

	public virtual void getVersion(string levelId, Action<uint> onSuccess, Action<string> onFail)
	{
	}

	public virtual void refreshSpecificRooms(string[] roomIds, Action<RefreshRoomData> onGetNameAndURL)
	{
	}
}
