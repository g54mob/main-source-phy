using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[DisallowMultipleComponent]
	[AddComponentMenu("Steamworks/Inventory Item")]
	[HelpURL("https://heathen.group/kb/inventory/")]
	public class SteamInventoryItemData : MonoBehaviour
	{
		public int id;

		[FormerlySerializedAs("m_Delegates")]
		[SerializeField]
		private List<string> mDelegates;

		private SteamInventoryItemDataEvents _mEvents;

		public ItemData Data
		{
			get
			{
				return default(ItemData);
			}
			set
			{
			}
		}

		private void Awake()
		{
		}

		public void ConsumeOne()
		{
		}

		public void ConsumeMany(int quantity)
		{
		}

		public void AddPromo()
		{
		}

		public void GetAll()
		{
		}

		public void StartPurchase()
		{
		}

		private void HandleAddPromoResults(InventoryResult results)
		{
		}
	}
}
