using System.Collections.Generic;
using UnityEngine;

public class Platforms
{
	public static List<Platform> m_Platforms = new List<Platform>();

	private static GameObject m_PlatformNodesContainer;

	public static float THICKNESS = 0.2f;

	public static float MIN_WIDTH = 0.5f;

	public static float MIN_WIDTH_SLIDER = 2f;

	public static float MAX_WIDTH_SLIDER = 20f;

	public static float MAX_WIDTH = 64f;

	public static float DEFAULT_WIDTH = 6f;

	public static float MIN_HEIGHT = 0f;

	public static float MIN_HEIGHT_SLIDER = 0f;

	public static float MAX_HEIGHT_SLIDER = 12f;

	public static float MAX_HEIGHT = 24f;

	public static float DEFAULT_HEIGHT = 4f;

	public static Platform CreatePlatform(Vector3 pos, Quaternion rot)
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_Platform, pos, rot);
		if (!gameObject)
		{
			return null;
		}
		Platform component = gameObject.GetComponent<Platform>();
		if (!component)
		{
			return null;
		}
		component.name = Prefabs.m_Instance.m_Platform.name;
		m_Platforms.Add(component);
		return component;
	}

	public static void DestroyPlatform(Platform platform)
	{
		platform.gameObject.SetActive(value: false);
		Object.Destroy(platform.gameObject);
	}

	public static void DestroyAll()
	{
		foreach (Platform platform in m_Platforms)
		{
			DestroyPlatform(platform);
		}
		m_Platforms.Clear();
	}

	public static void AddToSimulation()
	{
		foreach (Platform platform in m_Platforms)
		{
			platform.AddToSimulation();
		}
	}

	public static Transform GetPlatformNodesContainerTransform()
	{
		if (!m_PlatformNodesContainer)
		{
			m_PlatformNodesContainer = new GameObject("SimPlatforms");
		}
		return m_PlatformNodesContainer.transform;
	}

	public static void DisableOutlines()
	{
		foreach (Platform platform in m_Platforms)
		{
			platform.DisableOutline();
		}
	}

	public static void UpdateOutlines()
	{
		foreach (Platform platform in m_Platforms)
		{
			platform.UpdateOutline();
		}
	}

	public static List<PlatformProxy> Serialize()
	{
		List<PlatformProxy> list = new List<PlatformProxy>();
		foreach (Platform platform in m_Platforms)
		{
			if (platform.gameObject.activeInHierarchy)
			{
				list.Add(new PlatformProxy(platform));
			}
		}
		return list;
	}

	public static void Deserialize(List<PlatformProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (PlatformProxy proxy in proxies)
		{
			CreatePlatformFromProxy(proxy);
		}
	}

	public static Platform CreatePlatformFromProxy(PlatformProxy proxy)
	{
		Platform platform = CreatePlatform(proxy.m_Pos, Quaternion.identity);
		if ((bool)platform)
		{
			ApplyProxyToPlatform(platform, proxy);
		}
		return platform;
	}

	public static void ApplyProxyToPlatform(Platform platform, PlatformProxy proxy)
	{
		platform.m_Height = proxy.m_Height;
		platform.m_Width = proxy.m_Width;
		platform.m_Flipped = proxy.m_Flipped;
		platform.m_Solid = proxy.m_Solid;
		platform.RefreshMesh();
	}

	public static void UpdateShaderProperties(bool buildMode)
	{
		foreach (Platform platform in m_Platforms)
		{
			if (platform.gameObject.activeInHierarchy)
			{
				platform.UpdateShaderProperties(buildMode);
			}
		}
	}

	public static void EnableMeshRendering()
	{
		foreach (Platform platform in m_Platforms)
		{
			if (platform.gameObject.activeInHierarchy)
			{
				platform.EnableMeshRendering();
			}
		}
	}

	public static void EnterBuildMode()
	{
		foreach (Platform platform in m_Platforms)
		{
			if (platform.gameObject.activeInHierarchy)
			{
				platform.EnterBuildMode();
			}
		}
	}

	public static void EnterSandboxMode()
	{
		foreach (Platform platform in m_Platforms)
		{
			if (platform.gameObject.activeInHierarchy)
			{
				platform.EnterSandboxMode();
			}
		}
	}
}
