using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
	[Obsolete("No longer necessary. Access the staistics from ModProfile objects retrieved via the ModProfileRequestManager.")]
	public class ModStatisticsRequestManager : MonoBehaviour
	{
		private static ModStatisticsRequestManager _instance;

		public static ModStatisticsRequestManager instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = UIUtilities.FindComponentInAllScenes<ModStatisticsRequestManager>(true);
					if (_instance == null)
					{
						GameObject gameObject = new GameObject("Mod Statistics Request Manager");
						_instance = gameObject.AddComponent<ModStatisticsRequestManager>();
					}
				}
				return _instance;
			}
		}

		protected virtual void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
			}
		}

		public virtual void RequestModStatistics(int modId, Action<ModStatistics> onSuccess, Action<WebRequestError> onError)
		{
			ModManager.GetModProfile(modId, delegate(ModProfile profile)
			{
				if (onSuccess != null)
				{
					onSuccess(profile.statistics);
				}
			}, onError);
		}

		public virtual void RequestModStatistics(IList<int> orderedIdList, Action<ModStatistics[]> onSuccess, Action<WebRequestError> onError)
		{
			ModManager.GetModProfiles(orderedIdList, delegate(ModProfile[] profiles)
			{
				if (onSuccess != null)
				{
					if (profiles == null)
					{
						onSuccess(null);
					}
					ModStatistics[] array = new ModStatistics[profiles.Length];
					for (int i = 0; i < profiles.Length; i++)
					{
						ModStatistics modStatistics = null;
						if (profiles[i] != null)
						{
							modStatistics = profiles[i].statistics;
						}
						array[i] = modStatistics;
					}
					onSuccess(array);
				}
			}, onError);
		}
	}
}
