using UnityEngine;

public class TerrainLight : MonoBehaviour
{
	private void Awake()
	{
		TerrainLights.m_Lights.Add(this);
	}

	private void OnDestroy()
	{
		if (TerrainLights.m_Lights.Contains(this))
		{
			TerrainLights.m_Lights.Remove(this);
		}
	}
}
