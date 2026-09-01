using System;
using System.Collections.Generic;
using System.ComponentModel;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration.API
{
	public static class Friends
	{
		public static class Client
		{
			private class ImageRequestCallbackLink
			{
				public UserData Owner;

				public Action<Texture2D> Callback;
			}

			private static bool _isListeningForFriendMessages;

			private static List<ImageRequestCallbackLink> _pendingLinks;

			private static Dictionary<int, Texture2D> _loadedImages;

			private static Dictionary<UserData, Texture2D> _userAvatarMapping;

			private static CallResult<FriendsEnumerateFollowingList_t> _friendsEnumerateFollowingListT;

			private static CallResult<FriendsGetFollowerCount_t> _friendsGetFollowerCountT;

			private static CallResult<FriendsIsFollowing_t> _friendsIsFollowingT;

			private static Callback<AvatarImageLoaded_t> _avatarImageLoadedT;

			private static bool _loadingFollowed;

			public static bool IsListenForFriendsMessages
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			public static string PersonaName => null;

			public static EPersonaState PersonaState => default(EPersonaState);

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
			private static void Init()
			{
			}

			public static void ClearRichPresence()
			{
			}

			public static void GetFollowed(Action<CSteamID[]> callback)
			{
			}

			public static void EnumerateFollowingList(uint index, Action<FriendsEnumerateFollowingList_t, bool> callback)
			{
			}

			public static UserData GetCoplayFriend(int coplayFriendIndex)
			{
				return default(UserData);
			}

			public static int GetCoplayFriendCount()
			{
				return 0;
			}

			public static UserData[] GetCoplayFriends()
			{
				return null;
			}

			public static void GetFollowerCount(UserData userId, Action<FriendsGetFollowerCount_t, bool> callback)
			{
			}

			public static UserData GetFriendByIndex(int index, EFriendFlags flags)
			{
				return default(UserData);
			}

			public static AppId_t GetFriendCoplayGame(UserData userId)
			{
				return default(AppId_t);
			}

			public static DateTime GetFriendCoplayTime(UserData userId)
			{
				return default(DateTime);
			}

			public static int GetFriendCount(EFriendFlags flags)
			{
				return 0;
			}

			public static UserData[] GetFriends(EFriendFlags flags)
			{
				return null;
			}

			public static int GetFriendCountFromSource(CSteamID source)
			{
				return 0;
			}

			public static UserData GetFriendFromSourceByIndex(CSteamID source, int index)
			{
				return default(UserData);
			}

			public static UserData[] GetFriendsFromSource(CSteamID source)
			{
				return null;
			}

			public static bool GetFriendGamePlayed(UserData userId, out FriendGameInfo results)
			{
				results = default(FriendGameInfo);
				return false;
			}

			public static string GetFriendMessage(UserData userId, int index, out EChatEntryType type)
			{
				type = default(EChatEntryType);
				return null;
			}

			public static string GetFriendPersonaName(UserData userId)
			{
				return null;
			}

			public static string GetFriendPersonaNameHistory(UserData userId, int index)
			{
				return null;
			}

			public static string[] GetFriendPersonaNameHistory(UserData userId)
			{
				return null;
			}

			public static EPersonaState GetFriendPersonaState(UserData userId)
			{
				return default(EPersonaState);
			}

			public static string GetFriendRichPresence(UserData userId, string key)
			{
				return null;
			}

			public static string GetFriendRichPresenceKeyByIndex(UserData userId, int index)
			{
				return null;
			}

			public static int GetFriendRichPresenceKeyCount(UserData userId)
			{
				return 0;
			}

			public static Dictionary<string, string> GetFriendRichPresence(UserData userId)
			{
				return null;
			}

			public static int GetFriendsGroupCount()
			{
				return 0;
			}

			public static FriendsGroupID_t GetFriendsGroupIDByIndex(int index)
			{
				return default(FriendsGroupID_t);
			}

			public static FriendsGroupID_t[] GetFriendsGroups()
			{
				return null;
			}

			public static CSteamID[] GetFriendsGroupMembersList(FriendsGroupID_t groupId)
			{
				return null;
			}

			public static string GetFriendsGroupName(FriendsGroupID_t groupId)
			{
				return null;
			}

			public static int GetFriendSteamLevel(UserData userId)
			{
				return 0;
			}

			public static void GetFriendAvatar(CSteamID userId, Action<Texture2D> callback)
			{
			}

			public static void UnloadAvatarImages()
			{
			}

			public static void UnloadAvatarImage(Texture2D image)
			{
			}

			public static string GetPlayerNickname(UserData userId)
			{
				return null;
			}

			public static bool HasFriend(UserData userId, EFriendFlags flags)
			{
				return false;
			}

			public static bool InviteUserToGame(UserData userId, string connectString)
			{
				return false;
			}

			public static void IsFollowing(CSteamID id, Action<FriendsIsFollowing_t, bool> callback)
			{
			}

			public static bool IsUserInSource(UserData userId, CSteamID sourceId)
			{
				return false;
			}

			public static bool ReplyToFriendMessage(UserData userId, string message)
			{
				return false;
			}

			public static void RequestFriendRichPresence(UserData userId)
			{
			}

			public static bool RequestUserInformation(UserData userId, bool nameOnly)
			{
				return false;
			}

			public static void SetInGameVoiceSpeaking(bool speaking)
			{
			}

			public static void SetListenForFriendsMessages(bool enabled)
			{
			}

			public static void SetPlayedWith(UserData userId)
			{
			}

			public static bool SetRichPresence(string key, string value)
			{
				return false;
			}

			[EditorBrowsable(EditorBrowsableState.Never)]
			public static Texture2D GetLoadedAvatar(CSteamID id)
			{
				return null;
			}

			private static void HandleAvatarImageLoaded(AvatarImageLoaded_t results)
			{
			}

			internal static void HandlePersonaStateChange(PersonaStateChange_t results)
			{
			}

			private static bool LoadAvatar(int imageHandle, CSteamID user)
			{
				return false;
			}

			public static bool PersonaChangeHasFlag(EPersonaChange value, EPersonaChange checkflag)
			{
				return false;
			}

			public static bool PersonaChangeHasAllFlags(EPersonaChange value, params EPersonaChange[] checkflags)
			{
				return false;
			}
		}
	}
}
