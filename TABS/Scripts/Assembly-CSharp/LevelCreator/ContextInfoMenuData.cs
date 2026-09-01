using NaughtyAttributes;
using UnityEngine;

namespace LevelCreator
{
	[CreateAssetMenu(fileName = "ContextInfoMenuData", menuName = "TABS/ContextInfoMenuData", order = 0)]
	public class ContextInfoMenuData : ScriptableObject
	{
		[ReorderableList]
		public ContextInfoSet[] ContextInfoSets;
	}
}
