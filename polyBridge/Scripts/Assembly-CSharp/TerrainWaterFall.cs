using System.Collections.Generic;
using UnityEngine;

public class TerrainWaterFall : MonoBehaviour
{
	public TerrainIsland m_TerrainIsland;

	public ParticleSystem m_ParticleSystem;

	public MeshFilter m_MeshFilter;

	public MeshRenderer m_MeshRenderer;

	private List<int> m_MeshBottomVertIndicies = new List<int>();

	private void Awake()
	{
		m_TerrainIsland.m_TerrainWaterFalls.Add(this);
		if (m_ParticleSystem != null)
		{
			m_ParticleSystem.gameObject.SetActive(value: false);
		}
	}

	private void OnDestroy()
	{
		if (m_TerrainIsland.m_TerrainWaterFalls.Contains(this))
		{
			m_TerrainIsland.m_TerrainWaterFalls.Remove(this);
		}
	}

	public void Play()
	{
		float num = base.transform.position.y - WaterBlocks.GetHeight();
		bool flag = !SandboxSettings.m_NoWater && num > 1f;
		if (m_ParticleSystem != null)
		{
			m_ParticleSystem.gameObject.SetActive(flag);
		}
		m_MeshRenderer.gameObject.SetActive(flag);
		if (flag && m_ParticleSystem != null)
		{
			m_ParticleSystem.transform.position = new Vector3(m_ParticleSystem.transform.position.x, WaterBlocks.GetHeight(), m_ParticleSystem.transform.position.z);
			m_ParticleSystem.Simulate(m_ParticleSystem.main.duration);
			m_ParticleSystem.Play();
		}
	}

	public void Stop()
	{
		if (m_ParticleSystem != null)
		{
			m_ParticleSystem.gameObject.SetActive(value: false);
		}
	}

	public void Pause(bool pause)
	{
		if (m_ParticleSystem != null)
		{
			if (pause && !m_ParticleSystem.isPaused)
			{
				m_ParticleSystem.Pause(withChildren: true);
			}
			else if (!pause && m_ParticleSystem.isPaused)
			{
				m_ParticleSystem.Play(withChildren: true);
			}
		}
	}

	public void TranslateMeshVerts(float height)
	{
		m_MeshBottomVertIndicies.Clear();
		m_MeshBottomVertIndicies.AddRange(TerrainIslands.GetMeshBottomVertIndicies(m_MeshFilter.mesh));
		Vector3[] vertices = m_MeshFilter.mesh.vertices;
		for (int i = 0; i < m_MeshBottomVertIndicies.Count; i++)
		{
			vertices[m_MeshBottomVertIndicies[i]].y = 0f - height;
		}
		m_MeshFilter.mesh.vertices = vertices;
		m_MeshFilter.mesh.RecalculateBounds();
	}
}
