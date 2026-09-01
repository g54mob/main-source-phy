using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelCreator
{
	[Serializable]
	public class SeedCollectionRow : DataTableRow
	{
		[FormerlySerializedAs("Name")]
		public string name;

		[FormerlySerializedAs("Icon")]
		public Sprite icon;

		[FormerlySerializedAs("SpawnRate")]
		public float spawnRate = 0.045f;

		[FormerlySerializedAs("MaxDensity")]
		public float densityRangeCheck = 6f;

		[FormerlySerializedAs("IsVegetationSeed")]
		public bool isVegetationSeed;

		[FormerlySerializedAs("Seeds")]
		[ReorderableList]
		public SeedCollectionData[] seeds;

		[Space]
		[FormerlySerializedAs("RadialMenuTheme")]
		public RadialMenu.RadialThemes category;

		[FormerlySerializedAs("RadialMenuGroup")]
		public string group;

		[FormerlySerializedAs("RadialMenuSlotName")]
		public string slot;

		public string Path => category.ToString() + "/" + ((group != "") ? group : "None") + "/" + ((slot != "") ? slot : name);

		public string GetRowName()
		{
			return name;
		}

		public string GetLocalizedRowName()
		{
			return "LC_ITEMGRID_" + name.ToUpper();
		}
	}
}
