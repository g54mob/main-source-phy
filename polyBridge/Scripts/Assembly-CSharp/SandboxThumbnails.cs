using System;
using System.Collections.Generic;
using UnityEngine;

public class SandboxThumbnails
{
	private static Dictionary<string, SandboxThumbnail> m_Thumbnails = new Dictionary<string, SandboxThumbnail>();

	public static SandboxThumbnail Create(string id, Action<SandboxThumbnail> callback, string locId, Sprite sprite, Transform parent)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Prefabs.m_Instance.m_SandboxThumbnail, parent);
		if (gameObject == null)
		{
			return null;
		}
		SandboxThumbnail component = gameObject.GetComponent<SandboxThumbnail>();
		if (component == null)
		{
			return null;
		}
		component.SetName(locId);
		component.SetSprite(sprite);
		component.SetCallback(id, callback);
		if (!m_Thumbnails.ContainsKey(id))
		{
			m_Thumbnails.Add(id, component);
		}
		else
		{
			m_Thumbnails[id] = component;
		}
		return component;
	}

	public static void RefreshLocalization()
	{
		foreach (KeyValuePair<string, SandboxThumbnail> thumbnail in m_Thumbnails)
		{
			thumbnail.Value.RefreshLocalization();
		}
	}

	public static SandboxThumbnail GetById(string id)
	{
		if (!m_Thumbnails.ContainsKey(id))
		{
			return null;
		}
		return m_Thumbnails[id];
	}
}
