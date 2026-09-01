using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class ExpandedCustomFactionGrid : MonoBehaviour
	{
		public struct SpawnedFactionWrapper
		{
			public Faction faction;

			public ExpandedFactionButton factionButton;

			public SpawnedFactionWrapper(Faction faction, ExpandedFactionButton factionButton)
			{
				this.faction = faction;
				this.factionButton = factionButton;
			}
		}

		[SerializeField]
		private ExpandedFactionUI expandedFactionUI;

		[SerializeField]
		private GameObject m_factionPrefab;

		[SerializeField]
		public TMP_InputField m_filterInputField;

		[SerializeField]
		private PageCounter m_pageCounter;

		[SerializeField]
		private int m_rows = 3;

		[SerializeField]
		private GameObject m_pageButtons;

		private Faction[] m_customFactions;

		private List<SpawnedFactionWrapper> spawnedFactions = new List<SpawnedFactionWrapper>();

		private GridLayoutGroup m_grid;

		private int m_currentPage;

		private int m_totalPages = 1;

		private int MaxItemsPerPage => GetRowCount() * m_rows;

		public void SetupFactions(Faction[] customFactions)
		{
			m_grid = GetComponent<GridLayoutGroup>();
			m_customFactions = customFactions;
			Populate();
		}

		private void Clear()
		{
			for (int i = 0; i < spawnedFactions.Count; i++)
			{
				Object.Destroy(spawnedFactions[i].factionButton.gameObject);
			}
			spawnedFactions.Clear();
		}

		public void Populate(int page = 0)
		{
			Clear();
			Faction[] factions = GetFactions(m_filterInputField.text);
			m_currentPage = page;
			m_totalPages = Mathf.CeilToInt((float)factions.Length / (float)MaxItemsPerPage);
			int num = factions.Length - MaxItemsPerPage * m_currentPage;
			m_pageCounter?.Set(m_currentPage + 1, m_totalPages);
			m_pageButtons.SetActive(m_totalPages > 1);
			int num2 = Mathf.Min(num, MaxItemsPerPage);
			for (int i = 0; i < num2; i++)
			{
				int num3 = factions.Length - (num - i);
				SpawnButton(factions[num3]);
			}
		}

		private void SpawnButton(Faction faction)
		{
			GameObject obj = Object.Instantiate(m_factionPrefab, base.transform);
			obj.SetActive(value: true);
			ExpandedFactionButton component = obj.GetComponent<ExpandedFactionButton>();
			component.Setup(faction, expandedFactionUI);
			spawnedFactions.Add(new SpawnedFactionWrapper(faction, component));
		}

		public void ApplyFilter()
		{
			Populate();
		}

		public void IncreasePage(int value)
		{
			int num = m_currentPage + value;
			if (num >= 0 && num < m_totalPages)
			{
				Populate(num);
			}
		}

		private Faction[] GetFactions(string filter)
		{
			if (string.IsNullOrEmpty(filter))
			{
				return m_customFactions;
			}
			filter = filter.ToLower();
			List<Faction> list = new List<Faction>();
			for (int i = 0; i < m_customFactions.Length; i++)
			{
				if (m_customFactions[i].Entity.Name.ToLower().Contains(filter.ToLower()))
				{
					list.Add(m_customFactions[i]);
				}
			}
			return list.ToArray();
		}

		public void SetFactionAvailability(Faction[] alreadySelectedFactions)
		{
			bool[] array = new bool[spawnedFactions.Count];
			for (int i = 0; i < spawnedFactions.Count; i++)
			{
				SpawnedFactionWrapper spawnedFactionWrapper = spawnedFactions[i];
				foreach (Faction faction in alreadySelectedFactions)
				{
					if (spawnedFactionWrapper.faction == faction)
					{
						array[i] = true;
					}
				}
			}
			for (int k = 0; k < spawnedFactions.Count; k++)
			{
				spawnedFactions[k].factionButton.SetAvailability(!array[k]);
			}
		}

		private int GetRowCount()
		{
			float num = 0f;
			float num2 = 987f;
			for (int i = 0; i < 20; i++)
			{
				num += m_grid.cellSize.x;
				if (num > num2)
				{
					return i;
				}
				num += m_grid.spacing.x;
			}
			return 20;
		}
	}
}
