using System.Collections.Generic;
using UnityEngine;

namespace ArticleSystem
{
	[CreateAssetMenu(menuName = "Articles/Article Pool", fileName = "ArticlePool")]
	public class ArticlePoolDefinition : ScriptableObject
	{
		public enum SelectionMode
		{
			Random = 0,
			Sequential = 1
		}

		public string ID;

		[Tooltip("How articles are selected when this pool is consumed by the queue or the controller's fallback:\n- Random: Picks a random prefab each time (uniform). Within a single population pass, duplicates are prevented globally by the controller.\n- Sequential: Picks the next prefab in list order and wraps around. The per-pool index is tracked at runtime by the ArticlePoolQueueManager and persists across scene loads (within the same play session).")]
		[SerializeField]
		private SelectionMode selection;

		[Tooltip("UI Prefabs that represent articles for this pool.\nRequirements:\n- Each prefab must be a UI prefab with a RectTransform at its root.\n- Prefabs should be self-contained (fonts, images) for safe runtime instantiation.\nNotes:\n- Null entries are ignored at runtime.\n- Duplicates in this list are allowed in the asset, but the controller prevents duplicates within a single population pass globally.")]
		[SerializeField]
		private List<GameObject> articlePrefabs;

		private List<GameObject> articlePrefefsDistinctCache;

		public IReadOnlyList<GameObject> UniqueArticlePrefabs => null;

		public SelectionMode Mode => default(SelectionMode);

		private void OnValidate()
		{
		}
	}
}
