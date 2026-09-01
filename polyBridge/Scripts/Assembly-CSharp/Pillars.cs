using System.Collections.Generic;
using UnityEngine;

public class Pillars
{
	public static List<Pillar> m_Pillars = new List<Pillar>();

	public static float MIN_HEIGHT = 1f;

	public static float MIN_HEIGHT_SLIDER = 1f;

	public static float MAX_HEIGHT_SLIDER = 12f;

	public static float MAX_HEIGHT = 100f;

	public static Pillar CreatePillar(GameObject prefab, Vector3 pos, Quaternion rot)
	{
		GameObject gameObject = Object.Instantiate(prefab, pos, rot);
		if (!gameObject)
		{
			return null;
		}
		Pillar component = gameObject.GetComponent<Pillar>();
		if (!component)
		{
			return null;
		}
		component.name = prefab.name;
		m_Pillars.Add(component);
		return component;
	}

	public static void DestroyAll()
	{
		foreach (Pillar pillar in m_Pillars)
		{
			DestroyPillar(pillar);
		}
		m_Pillars.Clear();
	}

	public static void DestroyPillar(Pillar pillar)
	{
		pillar.gameObject.SetActive(value: false);
		Object.Destroy(pillar.gameObject);
	}

	public static void DisableOutlines()
	{
		foreach (Pillar pillar in m_Pillars)
		{
			pillar.DisableOutline();
		}
	}

	public static void EnableMeshRendering()
	{
		foreach (Pillar pillar in m_Pillars)
		{
			pillar.EnableMeshRendering();
		}
	}

	public static void UpdateOutlines()
	{
		foreach (Pillar pillar in m_Pillars)
		{
			pillar.UpdateOutline();
		}
	}

	public static List<PillarProxy> Serialize()
	{
		List<PillarProxy> list = new List<PillarProxy>();
		foreach (Pillar pillar in m_Pillars)
		{
			list.Add(new PillarProxy(pillar));
		}
		return list;
	}

	public static void Deserialize(List<PillarProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (PillarProxy proxy in proxies)
		{
			CreatePillarFromProxy(proxy);
		}
	}

	public static Pillar CreatePillarFromProxy(PillarProxy proxy)
	{
		if (!Prefabs.m_PrefabsDict.ContainsKey(proxy.m_PrefabName))
		{
			Debug.LogWarningFormat("Could not find prefab {0} in Prefab Dictionary", proxy.m_PrefabName);
			return null;
		}
		Pillar pillar = CreatePillar(Prefabs.m_PrefabsDict[proxy.m_PrefabName], proxy.m_Pos, Quaternion.identity);
		if ((bool)pillar)
		{
			ApplyProxyToPillar(pillar, proxy);
		}
		return pillar;
	}

	public static void ApplyProxyToPillar(Pillar pillar, PillarProxy proxy)
	{
		pillar.transform.position = proxy.m_Pos;
		pillar.SetHeight(proxy.m_Height);
	}

	public static void Hide(bool hidden)
	{
		foreach (Pillar pillar in m_Pillars)
		{
			pillar.m_MeshRendererTop.enabled = !hidden;
			pillar.m_MeshRendererMiddle.enabled = !hidden;
			pillar.m_MeshRendererBottom.enabled = !hidden;
		}
	}

	public static void UpdateShaderProperties(bool buildMode)
	{
		foreach (Pillar pillar in m_Pillars)
		{
			pillar.UpdateShaderProperties(buildMode);
		}
	}
}
