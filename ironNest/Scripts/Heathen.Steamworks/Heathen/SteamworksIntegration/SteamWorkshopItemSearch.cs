using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu("Steamworks/Workshop Item Search")]
	public class SteamWorkshopItemSearch : MonoBehaviour
	{
		[Header("Elements")]
		public TMP_InputField searchText;

		public TextMeshProUGUI currentPageLabel;

		public TextMeshProUGUI pageCountLabel;

		public SteamWorkshopItemDetailData template;

		public Transform content;

		public UgcQuery ActiveQuery;

		[Header("Events")]
		public UnityEvent<UgcQuery> onResultsReady;

		public UnityEvent<UgcQuery> onQueryUpdated;

		private readonly List<SteamWorkshopItemDetailData> _currentRecords;

		private string _lastSearchString;

		public int CurrentFrom => 0;

		public int CurrentTo => 0;

		public int TotalCount => 0;

		public int CurrentPage => 0;

		private string GetSearchString()
		{
			return null;
		}

		public void SearchMyPublished()
		{
		}

		public void SearchAll()
		{
		}

		public void SearchSubscribed()
		{
		}

		public void PrepareSearchAll()
		{
		}

		public void SearchFavorites()
		{
		}

		public void PrepareSearchFavorites()
		{
		}

		public void SearchFollowed()
		{
		}

		public void PrepareSearchFollowed()
		{
		}

		public void ExecuteSearch()
		{
		}

		public void SetNextSearchPage()
		{
		}

		public void SetPreviousSearchPage()
		{
		}

		public void SetSearchPage(uint page)
		{
		}

		private void HandleResults(UgcQuery query)
		{
		}
	}
}
