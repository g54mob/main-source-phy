using UnityEngine;

namespace Landfall.TABS.Workshop
{
	[CreateAssetMenu(fileName = "Landfall BattleCreator DataHolder", menuName = "TABS/BattleCreatorDataHolder", order = 1)]
	public class BattleCreatorDataHolder : ScriptableObject
	{
		[Header("Map UI Colors")]
		[SerializeField]
		private Color m_TribalColor;

		[SerializeField]
		private Color m_AncientColor;

		[SerializeField]
		private Color m_FarmerColor;

		[SerializeField]
		private Color m_MedievalColor;

		[SerializeField]
		private Color m_VikingColor;

		[SerializeField]
		private Color m_DynastyColor;

		[Header("Asset TAB")]
		[SerializeField]
		private Color m_ActiveTabColor;

		[SerializeField]
		private Color m_PassiveTabColor;

		private static BattleCreatorDataHolder _instance;

		public static BattleCreatorDataHolder GetDataHolder()
		{
			if (_instance == null)
			{
				_instance = Resources.Load<BattleCreatorDataHolder>("LandfallBattleCreatorDataHolder");
			}
			return _instance;
		}

		public Color GetTabColor(bool active)
		{
			if (active)
			{
				return m_ActiveTabColor;
			}
			return m_PassiveTabColor;
		}

		public Color GetMapColor(MapAsset mapAsset)
		{
			if (mapAsset == null)
			{
				return Color.black;
			}
			string value = "tribal";
			string value2 = "farmer";
			string value3 = "ancient";
			string value4 = "medieval";
			string value5 = "viking";
			string value6 = "dynasty";
			string text = mapAsset.Entity.Name.ToLower();
			if (text.Contains(value))
			{
				return m_TribalColor;
			}
			if (text.Contains(value2))
			{
				return m_FarmerColor;
			}
			if (text.Contains(value3))
			{
				return m_AncientColor;
			}
			if (text.Contains(value4))
			{
				return m_MedievalColor;
			}
			if (text.Contains(value5))
			{
				return m_VikingColor;
			}
			if (text.Contains(value6))
			{
				return m_DynastyColor;
			}
			return Color.black;
		}
	}
}
