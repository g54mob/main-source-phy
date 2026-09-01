using System.Collections.Generic;
using UnityEngine;

public class GameResolutions
{
	public static List<Resolution> m_Resolutions = new List<Resolution>();

	public static void Init()
	{
		InitializeResolutionChoices();
	}

	public static int GetResolutionIndex(int width, int height)
	{
		foreach (Resolution resolution in m_Resolutions)
		{
			if (width == resolution.width && height == resolution.height)
			{
				return m_Resolutions.IndexOf(resolution);
			}
		}
		return -1;
	}

	public static void SetGameToHighestResolution()
	{
		if (m_Resolutions.Count != 0)
		{
			Resolution resolution = m_Resolutions[m_Resolutions.Count - 1];
			if (resolution.width != Screen.width || resolution.height != Screen.height)
			{
				Screen.SetResolution(resolution.width, resolution.height, fullscreen: true);
			}
		}
	}

	public static string GetResolutionLabel(int index)
	{
		if (index >= 0 && index < m_Resolutions.Count)
		{
			return $"{m_Resolutions[index].width} x {m_Resolutions[index].height}";
		}
		return $"{Screen.width} x {Screen.height}";
	}

	private static void InitializeResolutionChoices()
	{
		m_Resolutions.Clear();
		Resolution[] resolutions = Screen.resolutions;
		for (int i = 0; i < resolutions.Length; i++)
		{
			Resolution item = resolutions[i];
			if (!ResolutionAlreadyAdded(item.width, item.height))
			{
				m_Resolutions.Add(item);
			}
		}
	}

	private static bool ResolutionAlreadyAdded(int width, int height)
	{
		foreach (Resolution resolution in m_Resolutions)
		{
			if (resolution.width == width && resolution.height == height)
			{
				return true;
			}
		}
		return false;
	}
}
