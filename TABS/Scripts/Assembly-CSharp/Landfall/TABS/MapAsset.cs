using UnityEngine;

namespace Landfall.TABS
{
	[CreateAssetMenu(fileName = "MapReference", menuName = "TABS/Map", order = 1)]
	public class MapAsset : ScriptableObject, IDatabaseEntity
	{
		public enum MapType
		{
			Main = 0,
			Simulation = 1
		}

		[SerializeField]
		private DatabaseEntity m_entity;

		[SerializeField]
		private Object m_SceneRef;

		public MapType m_MapType;

		public string MapName;

		public string SongCategoryName;

		public int m_mapIndex;

		public bool m_isSecret;

		public DatabaseEntity Entity => m_entity;

		public Object SceneReference => m_SceneRef;
	}
}
