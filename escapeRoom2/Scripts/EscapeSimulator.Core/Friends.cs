using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class Friends : MonoBehaviour
{
	public class FriendInfo
	{
		public CSteamID steamID;

		public EPersonaState status;

		public string name;

		public Texture2D avatar;

		public GamePlayed gamePlayed;
	}

	public enum GamePlayed
	{
		NotInGame = 0,
		EscapeSimulator2 = 1,
		InGame = 2
	}

	public List<FriendInfo> friends = new List<FriendInfo>();

	private Dictionary<CSteamID, Texture2D> friendsAvatarsCache = new Dictionary<CSteamID, Texture2D>();

	private Callback<PersonaStateChange_t> personaStateChange;

	private Callback<AvatarImageLoaded_t> avatarImageLoaded;

	private bool shouldSetChanged;

	public static bool didListChange;

	private static Friends instance;

	public static Friends get()
	{
		if (instance == null)
		{
			instance = new GameObject().AddComponent<Friends>();
			Object.DontDestroyOnLoad(instance.gameObject);
			instance.init();
		}
		return instance;
	}

	private void init()
	{
		personaStateChange = Callback<PersonaStateChange_t>.Create(OnPersonaStateChange);
		avatarImageLoaded = Callback<AvatarImageLoaded_t>.Create(OnAvatarImageLoaded);
		fetchFriendsList();
	}

	private void Update()
	{
		didListChange = false;
		if (shouldSetChanged)
		{
			shouldSetChanged = false;
			didListChange = true;
		}
	}

	private void OnPersonaStateChange(PersonaStateChange_t callback)
	{
		Debug.Log($"Persona state changed for: {callback.m_ulSteamID}, flags: {callback.m_nChangeFlags}");
		CSteamID cSteamID = new CSteamID(callback.m_ulSteamID);
		for (int i = 0; i < friends.Count; i++)
		{
			if (!(friends[i].steamID != cSteamID))
			{
				friends[i].name = SteamFriends.GetFriendPersonaName(cSteamID);
				friends[i].status = SteamFriends.GetFriendPersonaState(cSteamID);
				int mediumFriendAvatar = SteamFriends.GetMediumFriendAvatar(cSteamID);
				if (mediumFriendAvatar != 0)
				{
					friends[i].avatar = getSteamImageAsTexture2D(friends[i].steamID, mediumFriendAvatar);
				}
				fillGamePlayed(friends[i]);
				Debug.Log("Updated info for friend: " + friends[i].name);
				shouldSetChanged = true;
				break;
			}
		}
	}

	private void OnAvatarImageLoaded(AvatarImageLoaded_t callback)
	{
		foreach (FriendInfo friend in friends)
		{
			if (!(friend.steamID != callback.m_steamID))
			{
				friend.avatar = getSteamImageAsTexture2D(friend.steamID, callback.m_iImage);
				Debug.Log("Avatar loaded for: " + friend.name);
				shouldSetChanged = true;
				break;
			}
		}
	}

	public void fetchFriendsList()
	{
		friends.Clear();
		int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
		Debug.Log($"Found {friendCount} friends.");
		for (int i = 0; i < friendCount; i++)
		{
			CSteamID friendByIndex = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);
			if (friendByIndex == CSteamID.Nil || friendByIndex == SteamUser.GetSteamID())
			{
				continue;
			}
			FriendInfo friendInfo = new FriendInfo
			{
				steamID = friendByIndex,
				name = SteamFriends.GetFriendPersonaName(friendByIndex),
				status = SteamFriends.GetFriendPersonaState(friendByIndex)
			};
			int mediumFriendAvatar = SteamFriends.GetMediumFriendAvatar(friendByIndex);
			if (mediumFriendAvatar != 0)
			{
				friendInfo.avatar = getSteamImageAsTexture2D(friendInfo.steamID, mediumFriendAvatar);
				if (friendInfo.avatar == null)
				{
					Debug.Log("Avatar for " + friendInfo.name + " is loading or not yet available. Will be updated via callback.");
				}
			}
			else
			{
				Debug.LogWarning("Could not get avatar handle for " + friendInfo.name + ". It might be loading.");
			}
			fillGamePlayed(friendInfo);
			friends.Add(friendInfo);
			Debug.Log($"Friend: {friendInfo.name}, Status: {friendInfo.status}, Game: {friendInfo.gamePlayed}, Avatar Valid: {friendInfo.avatar != null}");
		}
		shouldSetChanged = true;
	}

	public List<FriendInfo> getSortedFriends()
	{
		List<FriendInfo> list = new List<FriendInfo>();
		list.AddRange(friends);
		list.Sort(delegate(FriendInfo a, FriendInfo b)
		{
			int num = b.gamePlayed.CompareTo(a.gamePlayed);
			if (num != 0)
			{
				return num;
			}
			int num2 = b.status.CompareTo(a.status);
			return (num2 != 0) ? num2 : a.name.CompareTo(b.name);
		});
		return list;
	}

	private void fillGamePlayed(FriendInfo friendInfo)
	{
		if (SteamFriends.GetFriendGamePlayed(friendInfo.steamID, out var pFriendGameInfo) && pFriendGameInfo.m_gameID.IsValid())
		{
			friendInfo.gamePlayed = ((pFriendGameInfo.m_gameID.m_GameID == Game.STEAM_APP_ID) ? GamePlayed.EscapeSimulator2 : GamePlayed.InGame);
		}
		else
		{
			friendInfo.gamePlayed = GamePlayed.NotInGame;
		}
	}

	private Texture2D getSteamImageAsTexture2D(CSteamID friendId, int iImage)
	{
		if (friendsAvatarsCache.TryGetValue(friendId, out var value))
		{
			return value;
		}
		Texture2D texture2D = null;
		if (SteamUtils.GetImageSize(iImage, out var pnWidth, out var pnHeight))
		{
			Debug.Log(pnWidth + " x " + pnHeight);
			byte[] array = new byte[pnWidth * pnHeight * 4];
			if (SteamUtils.GetImageRGBA(iImage, array, (int)(pnWidth * pnHeight * 4)))
			{
				texture2D = new Texture2D((int)pnWidth, (int)pnHeight, TextureFormat.RGBA32, mipChain: false, linear: true);
				texture2D.LoadRawTextureData(array);
				texture2D.Apply();
				Texture2D texture2D2 = Net.flipTexture(texture2D);
				Object.Destroy(texture2D);
				texture2D = texture2D2;
				friendsAvatarsCache[friendId] = texture2D;
			}
			else
			{
				Debug.LogWarning($"Failed to load RGBA data for image handle: {iImage}");
			}
		}
		else
		{
			Debug.LogWarning($"Invalid image handle or failed to get image size: {iImage}");
		}
		return texture2D;
	}
}
