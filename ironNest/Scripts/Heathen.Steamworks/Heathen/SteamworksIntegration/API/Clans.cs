using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration.API
{
	public static class Clans
	{
		public static class Client
		{
			public static readonly List<ChatRoom> JoinedRooms;

			private static CallResult<DownloadClanActivityCountsResult_t> _downloadClanActivityCountsResultT;

			private static CallResult<ClanOfficerListResponse_t> _clanOfficerListResponseT;

			private static CallResult<JoinClanChatRoomCompletionResult_t> _joinClanChatRoomCompletionResultT;

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
			private static void Init()
			{
			}

			public static void JoinChatRoom(ClanData clan, Action<ChatRoom, bool> callback)
			{
			}

			public static bool LeaveChatRoom(CSteamID clanChatId)
			{
				return false;
			}

			public static bool LeaveChatRoom(ChatRoom clanChat)
			{
				return false;
			}

			public static UserData GetChatMemberByIndex(CSteamID clan, int index)
			{
				return default(UserData);
			}

			public static bool GetActivityCounts(CSteamID clan, out int online, out int inGame, out int chatting)
			{
				online = default(int);
				inGame = default(int);
				chatting = default(int);
				return false;
			}

			public static ClanData GetClanByIndex(int clanIndex)
			{
				return default(ClanData);
			}

			public static ClanData[] GetClans()
			{
				return null;
			}

			public static int GetChatMemberCount(ClanData clanId)
			{
				return 0;
			}

			public static UserData[] GetChatMembers(ClanData clanId)
			{
				return null;
			}

			public static string GetChatMessage(CSteamID clanChatId, int index, out EChatEntryType type, out CSteamID chatter)
			{
				type = default(EChatEntryType);
				chatter = default(CSteamID);
				return null;
			}

			public static string GetChatMessage(ChatRoom clanChat, int index, out EChatEntryType type, out CSteamID chatter)
			{
				type = default(EChatEntryType);
				chatter = default(CSteamID);
				return null;
			}

			public static int GetClanCount()
			{
				return 0;
			}

			public static string GetName(ClanData clanId)
			{
				return null;
			}

			public static UserData GetOfficerByIndex(ClanData clanId, int officerIndex)
			{
				return default(UserData);
			}

			public static UserData[] GetOfficers(ClanData clanId)
			{
				return null;
			}

			public static int GetOfficerCount(ClanData clanId)
			{
				return 0;
			}

			public static UserData GetOwner(ClanData clanId)
			{
				return default(UserData);
			}

			public static string GetTag(ClanData clanId)
			{
				return null;
			}

			public static bool OpenChatWindowInSteam(CSteamID clanChatRoomId)
			{
				return false;
			}

			public static bool OpenChatWindowInSteam(ChatRoom clanChat)
			{
				return false;
			}

			public static bool SendChatMessage(CSteamID clanChatId, string message)
			{
				return false;
			}

			public static bool SendChatMessage(ChatRoom clanChat, string message)
			{
				return false;
			}

			public static bool IsClanChatAdmin(CSteamID clanChatId, CSteamID userId)
			{
				return false;
			}

			public static bool IsClanChatAdmin(ChatRoom clanChat, CSteamID userId)
			{
				return false;
			}

			public static bool IsClanPublic(ClanData clanId)
			{
				return false;
			}

			public static bool IsClanOfficialGameGroup(ClanData clanId)
			{
				return false;
			}

			public static bool IsClanChatWindowOpenInSteam(CSteamID clanChatId)
			{
				return false;
			}

			public static void RequestClanOfficerList(CSteamID clanId, Action<ClanOfficerListResponse_t, bool> callback)
			{
			}

			public static bool CloseClanChatWindowInSteam(CSteamID clanChatId)
			{
				return false;
			}

			public static bool CloseClanChatWindowInSteam(ChatRoom clanChat)
			{
				return false;
			}

			public static void DownloadClanActivityCounts(CSteamID[] clans, Action<DownloadClanActivityCountsResult_t, bool> callback)
			{
			}
		}
	}
}
