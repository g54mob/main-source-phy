using System.Collections.Generic;
using Poly.Collide;
using Poly.Collide.Unity;
using UnityEngine;

public class Rocks
{
	public static List<Rock> m_Rocks = new List<Rock>();

	public static float MIN_NORMALIZED_SCALE = 0.01f;

	public static float MIN_NORMALIZED_SCALE_SLIDER_X = 0.25f;

	public static float MIN_NORMALIZED_SCALE_SLIDER_Y = 0.25f;

	public static float MIN_NORMALIZED_SCALE_SLIDER_Z = 0.25f;

	public static float MAX_NORMALIZED_SCALE_SLIDER_X = 3f;

	public static float MAX_NORMALIZED_SCALE_SLIDER_Y = 3f;

	public static float MAX_NORMALIZED_SCALE_SLIDER_Z = 3f;

	public static float MAX_NORMALIZED_SCALE_X = 10f;

	public static float MAX_NORMALIZED_SCALE_Y = 10f;

	public static float MAX_NORMALIZED_SCALE_Z = 3f;

	public static Rock CreateRock(GameObject prefab, Vector3 pos, Quaternion rot)
	{
		GameObject gameObject = Object.Instantiate(prefab, pos, rot);
		if (!gameObject)
		{
			return null;
		}
		Rock component = gameObject.GetComponent<Rock>();
		if (!component)
		{
			return null;
		}
		component.name = prefab.name;
		m_Rocks.Add(component);
		return component;
	}

	public static void DestroyAll()
	{
		foreach (Rock rock in m_Rocks)
		{
			DestroyRock(rock);
		}
		m_Rocks.Clear();
	}

	public static void DestroyRock(Rock rock)
	{
		rock.gameObject.SetActive(value: false);
		Object.Destroy(rock.gameObject);
	}

	public static void AddToSimulation()
	{
		foreach (Rock rock in m_Rocks)
		{
			PlaceableCollisionInfo componentInChildren = rock.GetComponentInChildren<PlaceableCollisionInfo>();
			if ((bool)componentInChildren)
			{
				componentInChildren.OnAddedToWorld();
			}
		}
	}

	public static void DisableOutlines()
	{
		foreach (Rock rock in m_Rocks)
		{
			rock.DisableOutline();
		}
	}

	public static void EnableMeshRendering()
	{
		foreach (Rock rock in m_Rocks)
		{
			rock.EnableMeshRendering();
		}
	}

	public static void UpdateOutlines()
	{
		foreach (Rock rock in m_Rocks)
		{
			rock.UpdateOutline();
		}
	}

	public static bool OverlapsPolygonShape(PolygonShape shape)
	{
		foreach (Rock rock in m_Rocks)
		{
			if (rock.OverlapsPolygonShape(shape))
			{
				return true;
			}
		}
		return false;
	}

	public static void UpdatePolygonShapes()
	{
		foreach (Rock rock in m_Rocks)
		{
			rock.UpdatePolygonShapes();
		}
	}

	public static Rock GetClosestThatOverlapPolygonShape(Vector2 pos, PolygonShape shape)
	{
		Rock result = null;
		float num = float.MaxValue;
		foreach (Rock rock in m_Rocks)
		{
			if (rock.OverlapsPolygonShape(shape))
			{
				float num2 = Vector2.Distance(pos, rock.transform.position);
				if (num2 < num)
				{
					num = num2;
					result = rock;
				}
			}
		}
		return result;
	}

	public static List<RockProxy> Serialize()
	{
		List<RockProxy> list = new List<RockProxy>();
		foreach (Rock rock in m_Rocks)
		{
			list.Add(new RockProxy(rock));
		}
		return list;
	}

	public static void Deserialize(List<RockProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (RockProxy proxy in proxies)
		{
			CreateRockFromProxy(proxy);
		}
	}

	public static Rock CreateRockFromProxy(RockProxy proxy)
	{
		if (!Prefabs.m_PrefabsDict.ContainsKey(proxy.m_PrefabName))
		{
			Debug.LogWarningFormat("Could not find prefab {0} in Prefab Dictionary", proxy.m_PrefabName);
			return null;
		}
		Rock rock = CreateRock(Prefabs.m_PrefabsDict[proxy.m_PrefabName], proxy.m_Pos, Quaternion.identity);
		if ((bool)rock)
		{
			ApplyProxyToRock(rock, proxy);
		}
		return rock;
	}

	public static void ApplyProxyToRock(Rock rock, RockProxy proxy)
	{
		rock.transform.position = proxy.m_Pos;
		if (proxy.m_Scale.magnitude > Mathf.Epsilon)
		{
			rock.transform.localScale = proxy.m_Scale;
		}
		if (proxy.m_Flipped != rock.m_CollisionInfo.isFlipped)
		{
			rock.Flip(proxy.m_Flipped);
		}
		rock.m_LockToBottom = proxy.m_LockToBottom;
		rock.m_UniformScale = proxy.m_UniformScale;
		rock.UpdatePolygonShapes();
	}

	public static void Hide(bool hidden)
	{
		foreach (Rock rock in m_Rocks)
		{
			rock.m_MeshRenderer.enabled = !hidden;
		}
	}

	public static void UpdateShaderProperties(bool buildMode)
	{
		foreach (Rock rock in m_Rocks)
		{
			if (rock.gameObject.activeInHierarchy)
			{
				rock.UpdateShaderProperties(buildMode);
			}
		}
	}
}
