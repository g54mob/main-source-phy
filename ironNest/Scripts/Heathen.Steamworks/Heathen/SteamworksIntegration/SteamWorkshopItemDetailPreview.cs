using System;
using UnityEngine;
using UnityEngine.UI;

namespace Heathen.SteamworksIntegration
{
	[Obsolete("Unity's built in Texture2D LoadImage cant handle many file types and this proves to be more of a liability than an aid.Recomend the developer use a 3rd party image loader and our WorkshopItemDetail.GetPreviewImage tool to get the file data.")]
	[ModularComponent(typeof(SteamWorkshopItemDetailData), "Preview Images", "image")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamWorkshopItemDetailDataEvents))]
	[RequireComponent(typeof(SteamWorkshopItemDetailData))]
	public class SteamWorkshopItemDetailPreview : MonoBehaviour
	{
		public RawImage image;

		private SteamWorkshopItemDetailData _inspector;

		private SteamWorkshopItemDetailDataEvents _events;

		private void Awake()
		{
		}

		private void HandleChanged()
		{
		}
	}
}
