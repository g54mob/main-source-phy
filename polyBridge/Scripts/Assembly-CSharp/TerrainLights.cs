using System.Collections.Generic;

public class TerrainLights
{
	public static List<TerrainLight> m_Lights = new List<TerrainLight>();

	public static void TurnOn(bool on)
	{
		foreach (TerrainLight light in m_Lights)
		{
			light.gameObject.SetActive(on);
		}
	}
}
