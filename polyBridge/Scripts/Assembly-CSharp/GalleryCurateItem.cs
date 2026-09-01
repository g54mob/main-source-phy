public class GalleryCurateItem
{
	public string m_ID;

	public string m_LevelID;

	public string m_WorldID;

	public string m_Budget;

	public string m_Stress;

	public GalleryCurateItem(string id, string worldID, string levelID, string budget, string stress)
	{
		m_ID = id;
		m_WorldID = worldID;
		m_LevelID = levelID;
		m_Budget = budget;
		m_Stress = stress;
	}
}
