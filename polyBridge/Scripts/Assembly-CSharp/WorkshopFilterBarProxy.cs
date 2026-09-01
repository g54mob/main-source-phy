using System.Collections.Generic;

public class WorkshopFilterBarProxy
{
	public List<string> m_LevelIncludeTags;

	public List<string> m_LevelExcludeTags;

	public List<string> m_ModIncludeTags;

	public List<string> m_ModExcludeTags;

	public WorkshopFilterBarProxy()
	{
		m_LevelIncludeTags = new List<string>();
		m_LevelExcludeTags = new List<string>();
		m_ModIncludeTags = new List<string>();
		m_ModExcludeTags = new List<string>();
	}
}
