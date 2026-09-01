using System.Collections.Generic;
using UnityEngine;

namespace Landfall.TABS
{
	public class ExpandedLandfallFactionsGrid : MonoBehaviour
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

		private List<SpawnedFactionWrapper> spawnedFactions = new List<SpawnedFactionWrapper>();

		public GameObject factionPrefab;

		public GameObject SpawnFaction(Faction faction, ExpandedFactionUI factionUI)
		{
			GameObject obj = Object.Instantiate(factionPrefab, base.transform);
			obj.SetActive(value: true);
			ExpandedFactionButton component = obj.GetComponent<ExpandedFactionButton>();
			component.Setup(faction, factionUI);
			spawnedFactions.Add(new SpawnedFactionWrapper(faction, component));
			return obj;
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
	}
}
