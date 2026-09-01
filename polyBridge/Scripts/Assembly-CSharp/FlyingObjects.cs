using System.Collections.Generic;
using Poly.Collide;
using Poly.Collide.Unity;
using UnityEngine;

public class FlyingObjects
{
	public static List<FlyingObject> m_FlyingObjects = new List<FlyingObject>();

	public static float MIN_NORMALIZED_SCALE = 0.05f;

	public static float MIN_NORMALIZED_SCALE_SLIDER = 0.2f;

	public static float MAX_NORMALIZED_SCALE_SLIDER = 2f;

	public static float MAX_NORMALIZED_SCALE = 5f;

	public static FlyingObject CreateFlyingObject(GameObject prefab, Vector3 pos, Quaternion rot)
	{
		GameObject gameObject = Object.Instantiate(prefab, pos, rot);
		if (!gameObject)
		{
			return null;
		}
		FlyingObject component = gameObject.GetComponent<FlyingObject>();
		if (!component)
		{
			return null;
		}
		component.name = prefab.name;
		m_FlyingObjects.Add(component);
		return component;
	}

	public static void DestroyAll()
	{
		foreach (FlyingObject flyingObject in m_FlyingObjects)
		{
			DestroyFlyingObject(flyingObject);
		}
		m_FlyingObjects.Clear();
	}

	public static void DestroyFlyingObject(FlyingObject flyingObject)
	{
		flyingObject.gameObject.SetActive(value: false);
		Object.Destroy(flyingObject.gameObject);
	}

	public static void AddToSimulation()
	{
		foreach (FlyingObject flyingObject in m_FlyingObjects)
		{
			PlaceableCollisionInfo componentInChildren = flyingObject.GetComponentInChildren<PlaceableCollisionInfo>();
			if ((bool)componentInChildren)
			{
				componentInChildren.OnAddedToWorld();
			}
		}
	}

	public static void DisableOutlines()
	{
		foreach (FlyingObject flyingObject in m_FlyingObjects)
		{
			flyingObject.DisableOutline();
		}
	}

	public static void EnableMeshRendering()
	{
		foreach (FlyingObject flyingObject in m_FlyingObjects)
		{
			flyingObject.EnableMeshRendering();
		}
	}

	public static void UpdateOutlines()
	{
		foreach (FlyingObject flyingObject in m_FlyingObjects)
		{
			flyingObject.UpdateOutline();
		}
	}

	public static bool OverlapsPolygonShape(PolygonShape shape)
	{
		foreach (FlyingObject flyingObject in m_FlyingObjects)
		{
			if (flyingObject.OverlapsPolygonShape(shape))
			{
				return true;
			}
		}
		return false;
	}

	public static void UpdatePolygonShapes()
	{
		foreach (FlyingObject flyingObject in m_FlyingObjects)
		{
			flyingObject.UpdatePolygonShapes();
		}
	}

	public static FlyingObject GetClosestThatOverlapPolygonShape(Vector2 pos, PolygonShape shape)
	{
		FlyingObject result = null;
		float num = float.MaxValue;
		foreach (FlyingObject flyingObject in m_FlyingObjects)
		{
			if (flyingObject.OverlapsPolygonShape(shape))
			{
				float num2 = Vector2.Distance(pos, flyingObject.transform.position);
				if (num2 < num)
				{
					num = num2;
					result = flyingObject;
				}
			}
		}
		return result;
	}

	public static List<FlyingObjectProxy> Serialize()
	{
		List<FlyingObjectProxy> list = new List<FlyingObjectProxy>();
		foreach (FlyingObject flyingObject in m_FlyingObjects)
		{
			list.Add(new FlyingObjectProxy(flyingObject));
		}
		return list;
	}

	public static void Deserialize(List<FlyingObjectProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (FlyingObjectProxy proxy in proxies)
		{
			CreateFlyingObjectFromProxy(proxy);
		}
	}

	public static FlyingObject CreateFlyingObjectFromProxy(FlyingObjectProxy proxy)
	{
		if (!Prefabs.m_PrefabsDict.ContainsKey(proxy.m_PrefabName))
		{
			Debug.LogWarningFormat("Could not find prefab {0} in Prefab Dictionary", proxy.m_PrefabName);
			return null;
		}
		FlyingObject flyingObject = CreateFlyingObject(Prefabs.m_PrefabsDict[proxy.m_PrefabName], proxy.m_Pos, Quaternion.identity);
		if ((bool)flyingObject)
		{
			ApplyProxyToFlyingObject(flyingObject, proxy);
		}
		return flyingObject;
	}

	public static void ApplyProxyToFlyingObject(FlyingObject flyingObject, FlyingObjectProxy proxy)
	{
		flyingObject.transform.position = proxy.m_Pos;
		if (proxy.m_Scale.magnitude > Mathf.Epsilon)
		{
			flyingObject.transform.localScale = proxy.m_Scale;
		}
		flyingObject.UpdatePolygonShapes();
	}

	public static void UpdateShaderProperties(bool buildMode)
	{
		foreach (FlyingObject flyingObject in m_FlyingObjects)
		{
			if (flyingObject.gameObject.activeInHierarchy)
			{
				flyingObject.UpdateShaderProperties(buildMode);
			}
		}
	}

	public static void EnterBuildMode()
	{
		foreach (FlyingObject flyingObject in m_FlyingObjects)
		{
			if (flyingObject.gameObject.activeInHierarchy)
			{
				flyingObject.EnterBuildMode();
			}
		}
	}
}
