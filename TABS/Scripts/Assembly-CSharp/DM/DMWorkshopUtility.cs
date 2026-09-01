using System;
using Landfall.TABS.Workshop;
using ModIO;
using UnityEngine;

namespace DM
{
	public static class DMWorkshopUtility
	{
		public static void AddBlockedUser(string id, Action<Exception> doneCallback)
		{
			if (LocalUser.UserId.ToString() == id)
			{
				Debug.LogError("Will not block the logged in user!");
				doneCallback?.Invoke(null);
				return;
			}
			if (int.TryParse(id, out var result))
			{
				APIClient.MuteUser(result, null, null);
			}
			doneCallback?.Invoke(null);
		}

		public static void ClearBlockedUsers(Action<Exception> doneCallback = null)
		{
			RequestFilter filter = new RequestFilter();
			APIPaginationParameters pagination = new APIPaginationParameters();
			APIClient.GetMutedUsers(filter, pagination, OnFetchedUsersSuccess, delegate(WebRequestError e)
			{
				Debug.LogError(e?.errorMessage);
			});
			void OnFetchedUsersSuccess(RequestPage<UserProfile> page)
			{
				if (page == null)
				{
					doneCallback?.Invoke(null);
				}
				else
				{
					int responsesReceived = 0;
					UserProfile[] items = page.items;
					foreach (UserProfile userProfile in items)
					{
						if (userProfile != null)
						{
							APIClient.UnmuteUser(userProfile.id, delegate
							{
								responsesReceived++;
								if (responsesReceived >= page.items.Length)
								{
									doneCallback?.Invoke(null);
								}
							}, delegate(WebRequestError e)
							{
								Debug.LogError(e?.errorMessage);
								responsesReceived++;
								if (responsesReceived >= page.items.Length)
								{
									doneCallback?.Invoke(null);
								}
							});
						}
					}
				}
			}
		}

		public static WorkshopContentType GetContentTypeFromModProfile(ModProfile profile)
		{
			foreach (string tagName in profile.tagNames)
			{
				switch (tagName)
				{
				case "Battle":
					return WorkshopContentType.Battle;
				case "Campaign":
					return WorkshopContentType.Campaign;
				case "Unit":
					return WorkshopContentType.Unit;
				case "Faction":
					return WorkshopContentType.Faction;
				case "Map":
					return WorkshopContentType.Map;
				}
			}
			return WorkshopContentType.Any;
		}
	}
}
