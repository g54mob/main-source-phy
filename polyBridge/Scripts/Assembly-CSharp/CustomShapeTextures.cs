using System.Collections.Generic;
using UnityEngine;

public class CustomShapeTextures : MonoBehaviour
{
	public CustomShapeTexture[] m_Textures;

	public static CustomShapeTextures m_Instance;

	private List<CustomShapeTexture> m_ModTextures = new List<CustomShapeTexture>();

	private void Awake()
	{
		m_Instance = this;
	}

	public CustomShapeTexture GetDefaultCustomShapeTexture()
	{
		if (m_Textures != null && m_Textures.Length != 0)
		{
			return m_Textures[0];
		}
		return null;
	}

	public List<CustomShapeTexture> GetAllTextures()
	{
		List<CustomShapeTexture> list = new List<CustomShapeTexture>(m_Textures);
		list.AddRange(m_ModTextures);
		return list;
	}

	public CustomShapeTexture GetTextureFromId(string id)
	{
		CustomShapeTexture[] textures = m_Textures;
		foreach (CustomShapeTexture customShapeTexture in textures)
		{
			if (customShapeTexture.m_ID == id)
			{
				return customShapeTexture;
			}
		}
		foreach (CustomShapeTexture modTexture in m_ModTextures)
		{
			if (modTexture.m_ID == id)
			{
				return modTexture;
			}
		}
		return null;
	}

	public void ClearModTextures()
	{
		m_ModTextures.Clear();
	}

	public void AddModTexture(CustomShapeTexture modTexture)
	{
		m_ModTextures.Add(modTexture);
	}
}
