using UnityEngine;

public class TerrainParticleSystem : MonoBehaviour
{
	public TerrainIsland m_TerrainIsland;

	public ParticleSystem m_ParticleSystem;

	public float m_Radius = 1f;

	private void Awake()
	{
		m_TerrainIsland.m_TerrainParticleSystems.Add(this);
		m_ParticleSystem.gameObject.SetActive(value: false);
		ParticleSystem.MainModule main = m_ParticleSystem.main;
		main.useUnscaledTime = true;
		ParticleSystem[] componentsInChildren = m_TerrainIsland.GetComponentsInChildren<ParticleSystem>(includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			ParticleSystem.MainModule main2 = componentsInChildren[i].main;
			main2.useUnscaledTime = true;
		}
	}

	private void OnDestroy()
	{
		if (m_TerrainIsland.m_TerrainParticleSystems.Contains(this))
		{
			m_TerrainIsland.m_TerrainParticleSystems.Remove(this);
		}
	}

	public bool IntersectsWater()
	{
		if (SandboxSettings.m_NoWater)
		{
			return false;
		}
		Bounds bounds = new Bounds(base.transform.position, new Vector3(m_Radius * 2f, m_Radius * 2f, m_Radius * 2f));
		return WaterBlocks.GetBounds().Intersects(bounds);
	}

	public bool BelowTerrain()
	{
		return base.transform.position.y <= m_Radius;
	}

	public void Play()
	{
		m_ParticleSystem.gameObject.SetActive(value: true);
		m_ParticleSystem.Play();
		if (m_TerrainIsland.m_Flipped)
		{
			m_ParticleSystem.transform.localScale = new Vector3(0f - Mathf.Abs(m_ParticleSystem.transform.localScale.x), m_ParticleSystem.transform.localScale.y, m_ParticleSystem.transform.localScale.z);
		}
	}

	public void Pause(bool pause)
	{
		if (pause && !m_ParticleSystem.isPaused)
		{
			m_ParticleSystem.Pause();
		}
		else if (!pause && m_ParticleSystem.isPaused)
		{
			m_ParticleSystem.Play();
		}
	}

	public void Stop()
	{
		m_ParticleSystem.gameObject.SetActive(value: false);
	}
}
