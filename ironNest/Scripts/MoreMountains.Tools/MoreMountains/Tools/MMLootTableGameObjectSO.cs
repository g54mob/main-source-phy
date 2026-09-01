using UnityEngine;

namespace MoreMountains.Tools
{
	[CreateAssetMenu(fileName = "LootDefinition", menuName = "MoreMountains/Loot Definition")]
	public class MMLootTableGameObjectSO : ScriptableObject
	{
		public MMLootTableGameObject LootTable;

		public virtual GameObject GetLoot()
		{
			return null;
		}

		public virtual void ComputeWeights()
		{
		}

		protected virtual void OnValidate()
		{
		}
	}
}
