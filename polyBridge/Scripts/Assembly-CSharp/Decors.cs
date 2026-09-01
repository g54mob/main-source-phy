using System.Collections.Generic;
using EPOOutline;
using UnityEngine;

public class Decors
{
	public static List<Decor> m_Decors = new List<Decor>();

	public static float MIN_NORMALIZED_SCALE = 0.01f;

	public static float MIN_NORMALIZED_SCALE_SLIDER_X = 0.25f;

	public static float MIN_NORMALIZED_SCALE_SLIDER_Y = 0.25f;

	public static float MIN_NORMALIZED_SCALE_SLIDER_Z = 0.25f;

	public static float MAX_NORMALIZED_SCALE_SLIDER_X = 3f;

	public static float MAX_NORMALIZED_SCALE_SLIDER_Y = 3f;

	public static float MAX_NORMALIZED_SCALE_SLIDER_Z = 3f;

	public static float MAX_NORMALIZED_SCALE_X = 10f;

	public static float MAX_NORMALIZED_SCALE_Y = 10f;

	public static float MAX_NORMALIZED_SCALE_Z = 10f;

	public static Decor Create(GameObject prefab, string id, string modId, Vector3 pos, Quaternion rot)
	{
		GameObject gameObject = Object.Instantiate(prefab, pos, rot);
		if (!gameObject)
		{
			return null;
		}
		gameObject.SetActive(value: true);
		Decor component = gameObject.GetComponent<Decor>();
		if (!component)
		{
			return null;
		}
		component.m_Id = id;
		component.name = prefab.name;
		component.m_ShowInBuildMode = true;
		component.m_SandboxItem = SandboxItems.AddSandboxItemComponent(gameObject, SandboxItemType.DECOR);
		component.m_Outline = component.GetComponent<Outlinable>();
		if (component.m_Outline == null)
		{
			component.m_Outline = component.gameObject.AddComponent<Outlinable>();
			component.m_Outline.AddAllChildRenderersToRenderingList(RenderersAddingMode.MeshRenderer | RenderersAddingMode.SkinnedMeshRenderer);
		}
		component.m_Outline.enabled = false;
		component.m_ModId = modId;
		if (Theme.m_Instance != null)
		{
			component.UpdateShaderProperties(buildMode: false, CuttingPlanes.m_Instance.m_Floor);
		}
		m_Decors.Add(component);
		return component;
	}

	public static void DestroyAll()
	{
		foreach (Decor decor in m_Decors)
		{
			DestroyDecor(decor);
		}
		m_Decors.Clear();
	}

	public static void DestroyDecor(Decor decor)
	{
		decor.gameObject.SetActive(value: false);
		Object.Destroy(decor.gameObject);
	}

	public static void SetVisibility(GameState gameState)
	{
		foreach (Decor decor in m_Decors)
		{
			decor.SetVisibility(gameState);
		}
	}

	public static void Hide(bool hide)
	{
		foreach (Decor decor in m_Decors)
		{
			decor.Hide(hide);
		}
	}

	public static void UpdateShaderProperties(bool buildMode, MeshRenderer plane)
	{
		foreach (Decor decor in m_Decors)
		{
			decor.UpdateShaderProperties(buildMode, plane);
		}
	}

	public static List<DecorProxy> Serialize()
	{
		List<DecorProxy> list = new List<DecorProxy>();
		foreach (Decor decor in m_Decors)
		{
			list.Add(new DecorProxy(decor));
		}
		return list;
	}

	public static void Deserialize(List<DecorProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (DecorProxy proxy in proxies)
		{
			CreateDecorFromProxy(proxy);
		}
	}

	public static Decor CreateDecorFromProxy(DecorProxy proxy)
	{
		DecorStub stubFromId = DecorStubs.GetStubFromId(proxy.m_ID);
		if (stubFromId == null)
		{
			Debug.LogWarningFormat("Could not find decor stub with id " + proxy.m_ID + " in DecorStubs dictionary");
			return null;
		}
		GameObject asyncPrefab = Prefabs.GetAsyncPrefab(stubFromId.m_PrefabAddress);
		if (asyncPrefab == null)
		{
			Debug.LogWarningFormat("Could not find preloaded decor prefab with address " + stubFromId.m_PrefabAddress);
			return null;
		}
		Decor decor = Create(asyncPrefab, stubFromId.m_PrefabAddress, proxy.m_ModId, proxy.m_Pos, Quaternion.identity);
		if ((bool)decor)
		{
			ApplyProxyToDecor(decor, proxy);
			if (Theme.m_Instance != null)
			{
				decor.UpdateShaderProperties(buildMode: false, CuttingPlanes.m_Instance.m_Floor);
			}
		}
		return decor;
	}

	public static void ApplyProxyToDecor(Decor decor, DecorProxy proxy)
	{
		decor.m_HeadingRotationDegrees = proxy.m_HeadingAngle;
		decor.m_PitchRotationDegrees = proxy.m_PitchAngle;
		decor.m_RollRotationDegrees = proxy.m_RollAngle;
		decor.m_ShowInBuildMode = proxy.m_ShowInBuildMode;
		decor.m_UniformScale = proxy.m_UniformScale;
		decor.transform.position = proxy.m_Pos;
		decor.transform.rotation = Quaternion.identity;
		decor.transform.rotation = Quaternion.Euler(0f - decor.m_PitchRotationDegrees, 0f - decor.m_HeadingRotationDegrees, 0f - decor.m_RollRotationDegrees);
		if (proxy.m_Scale.magnitude > Mathf.Epsilon)
		{
			decor.transform.localScale = proxy.m_Scale;
		}
	}
}
