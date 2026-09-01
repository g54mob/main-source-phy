using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[DisallowMultipleComponent]
	[AddComponentMenu("Steamworks/Achievement")]
	[HelpURL("https://heathen.group/kb/steam-features-achievements/")]
	public class SteamAchievementData : MonoBehaviour
	{
		public string apiName;

		[FormerlySerializedAs("m_Delegates")]
		[SerializeField]
		private List<string> mDelegates;

		public AchievementData Data
		{
			get
			{
				return default(AchievementData);
			}
			set
			{
			}
		}

		public void Unlock()
		{
		}

		public void Clear()
		{
		}

		public void Store()
		{
		}

		public void SetAchieved(bool value)
		{
		}
	}
}
