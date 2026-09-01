using System.Collections.Generic;
using UnityEngine;

namespace TwitchUnitInfo
{
	public class TwitchUnitNameHandler
	{
		private List<TwitchUserData> LoopingUnitNames = new List<TwitchUserData>();

		private List<TwitchUserData> EventUnitNames = new List<TwitchUserData>();

		public bool IsCurrentListGeneratedWithLurkers;

		private int currNameIndex;

		public void ClearRandomNameList()
		{
			LoopingUnitNames.Clear();
			currNameIndex = 0;
		}

		public void GenerateUnitNameArray(ViewerTypes viewerType, bool includeLurkers)
		{
			ClearRandomNameList();
			IsCurrentListGeneratedWithLurkers = includeLurkers;
			TwitchHandler service = ServiceLocator.GetService<TwitchHandler>();
			TwitchChatters activeChatters = service.ActiveChatters;
			AddTwitchUsers(activeChatters.admins, ViewerTypes.mod);
			AddTwitchUsers(activeChatters.broadcaster, ViewerTypes.broadcaster);
			AddTwitchUsers(activeChatters.global_mods, ViewerTypes.mod);
			AddTwitchUsers(activeChatters.moderators, ViewerTypes.mod);
			AddTwitchUsers(activeChatters.staff, ViewerTypes.mod);
			switch (viewerType)
			{
			case ViewerTypes.viewer:
				AddTwitchUsers(activeChatters.viewers, ViewerTypes.viewer);
				AddTwitchUsers(activeChatters.subscribers, ViewerTypes.subscriber);
				AddTwitchUsers(activeChatters.vips, ViewerTypes.vip);
				if (includeLurkers)
				{
					AddLurkers(service.ViewerInfo.Viewers.chatters.viewers, ViewerTypes.viewer);
					AddLurkers(service.ViewerInfo.Viewers.chatters.vips, ViewerTypes.vip);
				}
				break;
			case ViewerTypes.subscriber:
				AddTwitchUsers(activeChatters.subscribers, ViewerTypes.subscriber);
				AddTwitchUsers(activeChatters.vips, ViewerTypes.vip);
				if (includeLurkers)
				{
					AddLurkers(service.ViewerInfo.Viewers.chatters.vips, ViewerTypes.vip);
				}
				break;
			case ViewerTypes.vip:
				AddTwitchUsers(activeChatters.vips, ViewerTypes.vip);
				if (includeLurkers)
				{
					AddLurkers(service.ViewerInfo.Viewers.chatters.vips, ViewerTypes.vip);
				}
				break;
			}
			if (includeLurkers)
			{
				AddLurkers(service.ViewerInfo.Viewers.chatters.broadcaster, ViewerTypes.broadcaster);
				AddLurkers(service.ViewerInfo.Viewers.chatters.global_mods, ViewerTypes.mod);
				AddLurkers(service.ViewerInfo.Viewers.chatters.moderators, ViewerTypes.mod);
				AddLurkers(service.ViewerInfo.Viewers.chatters.admins, ViewerTypes.mod);
				AddLurkers(service.ViewerInfo.Viewers.chatters.staff, ViewerTypes.mod);
			}
			RandomizeList(LoopingUnitNames);
		}

		public void Clear()
		{
			EventUnitNames.Clear();
			ClearRandomNameList();
		}

		private void AddTwitchUser(ActiveChatter chatter, ViewerTypes type)
		{
			TwitchUserData item = new TwitchUserData
			{
				name = chatter.name,
				color = chatter.color,
				type = type
			};
			LoopingUnitNames.Add(item);
		}

		private void AddLurkers(List<string> names, ViewerTypes type)
		{
			List<string> list = new List<string>(LoopingUnitNames.Count);
			foreach (TwitchUserData loopingUnitName in LoopingUnitNames)
			{
				list.Add(loopingUnitName.name.ToLower());
			}
			foreach (string name in names)
			{
				if (!list.Contains(name))
				{
					AddTwitchUser(new ActiveChatter
					{
						name = name,
						color = Color.white
					}, type);
				}
			}
		}

		private void AddTwitchUsers(Dictionary<string, ActiveChatter> names, ViewerTypes type)
		{
			foreach (KeyValuePair<string, ActiveChatter> name in names)
			{
				AddTwitchUser(name.Value, type);
			}
		}

		private static void RandomizeList<T>(IList<T> list)
		{
			int num = list.Count;
			while (num > 1)
			{
				num--;
				int index = Random.Range(0, num + 1);
				T value = list[index];
				list[index] = list[num];
				list[num] = value;
			}
		}

		public TwitchUserData GetNextViewer()
		{
			TwitchUserData result = default(TwitchUserData);
			if (LoopingUnitNames.Count == 0)
			{
				return result;
			}
			if (currNameIndex++ >= LoopingUnitNames.Count)
			{
				currNameIndex = 1;
			}
			return LoopingUnitNames[currNameIndex - 1];
		}

		public bool HasNames()
		{
			return currNameIndex < LoopingUnitNames.Count;
		}
	}
}
