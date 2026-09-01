using System;
using System.Collections.Generic;
using UnityEngine;

namespace BesiegeDlc
{
	[CreateAssetMenu(fileName = "DlcInfo", menuName = "Dlc Info Object")]
	internal class DlcInfo : ScriptableObject
	{
		[Serializable]
		internal class Dlc
		{
			public DlcManager.DlcType dlcType;

			public int LocID;

			public Sprite Icon;

			[Header("Steam")]
			public uint SteamAppId;

			public string SteamStoreLink = string.Empty;

			[Header("GDK")]
			public string GDKStoreId = string.Empty;

			public string GDKStoreLink = string.Empty;
		}

		public Sprite DlcNotFoundIcon;

		public Dlc[] DlcList = new Dlc[0];

		public Dictionary<DlcManager.DlcType, Dlc> GetInfo()
		{
			Dictionary<DlcManager.DlcType, Dlc> dictionary = new Dictionary<DlcManager.DlcType, Dlc>();
			for (int i = 0; i < DlcList.Length; i++)
			{
				Dlc dlc = DlcList[i];
				if (!dictionary.ContainsKey(dlc.dlcType))
				{
					dictionary.Add(dlc.dlcType, dlc);
				}
			}
			return dictionary;
		}
	}
}
