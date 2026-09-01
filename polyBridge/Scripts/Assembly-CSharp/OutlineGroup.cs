using System.Collections.Generic;
using UnityEngine;

public class OutlineGroup
{
	public List<Outline> m_Outlines = new List<Outline>();

	public Outline CreateOutline(Texture texture, float textureScale, float lineWidth)
	{
		Outline outline = Outlines.Create(texture, textureScale, lineWidth, Color.white);
		if (outline != null)
		{
			outline.UpdateForGameState(GameStateManager.GetState(), GameUI.m_Instance.GetOutlineWidth(GameStateManager.GetState()));
			m_Outlines.Add(outline);
		}
		return outline;
	}

	public void DestroyOutline()
	{
		foreach (Outline outline in m_Outlines)
		{
			outline.Destroy();
		}
		m_Outlines.Clear();
	}

	public void EnableOutline()
	{
		foreach (Outline outline in m_Outlines)
		{
			outline.SetActive(active: true);
		}
	}

	public void DisableOutline()
	{
		foreach (Outline outline in m_Outlines)
		{
			outline.SetActive(active: false);
		}
	}

	public void ClearCachedSplinePoints()
	{
		foreach (Outline outline in m_Outlines)
		{
			outline.ClearCachedSplinePoints();
		}
	}

	public void SetColor(Color color)
	{
		foreach (Outline outline in m_Outlines)
		{
			outline.SetColor(color);
		}
	}
}
