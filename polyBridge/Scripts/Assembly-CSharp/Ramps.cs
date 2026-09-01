using System.Collections.Generic;
using UnityEngine;

public class Ramps
{
	public static List<Ramp> m_Ramps = new List<Ramp>();

	public static float THICKNESS = 0.2f;

	public static float MIN_WIDTH = 0.5f;

	public static float MIN_HEIGHT = 0f;

	public static float MIN_HEIGHT_SLIDER = 0f;

	public static float MAX_HEIGHT_SLIDER = 8f;

	public static float MAX_HEIGHT = 24f;

	public static int MIN_NUM_SEGMENTS = 1;

	public static Ramp CreateRamp(Vector3 pos, Quaternion rot)
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_Ramp, pos, rot);
		if (!gameObject)
		{
			return null;
		}
		Ramp component = gameObject.GetComponent<Ramp>();
		if (!component)
		{
			return null;
		}
		component.name = Prefabs.m_Instance.m_Ramp.name;
		component.m_SplineType = component.m_SplineComputer.type;
		component.RecalulateNumSegments();
		component.RefreshCollider();
		m_Ramps.Add(component);
		return component;
	}

	public static void DestroyRamp(Ramp ramp)
	{
		ramp.gameObject.SetActive(value: false);
		Object.Destroy(ramp.gameObject);
	}

	public static void DestroyAll()
	{
		foreach (Ramp ramp in m_Ramps)
		{
			DestroyRamp(ramp);
		}
		m_Ramps.Clear();
	}

	public static void AddToSimulation()
	{
		foreach (Ramp ramp in m_Ramps)
		{
			ramp.AddToSimulation();
		}
	}

	public static void DisableOutlines()
	{
		foreach (Ramp ramp in m_Ramps)
		{
			ramp.DisableOutline();
		}
	}

	public static void EnableMeshRendering()
	{
		foreach (Ramp ramp in m_Ramps)
		{
			ramp.EnableMeshRendering();
		}
	}

	public static void UpdateOutlines()
	{
		foreach (Ramp ramp in m_Ramps)
		{
			ramp.UpdateOutline();
		}
	}

	public static List<RampProxy> Serialize()
	{
		List<RampProxy> list = new List<RampProxy>();
		foreach (Ramp ramp in m_Ramps)
		{
			if (ramp.gameObject.activeInHierarchy)
			{
				list.Add(new RampProxy(ramp));
			}
		}
		return list;
	}

	public static void Deserialize(List<RampProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (RampProxy proxy in proxies)
		{
			CreateRampFromProxy(proxy);
		}
	}

	public static Ramp CreateRampFromProxy(RampProxy proxy)
	{
		Ramp ramp = CreateRamp(proxy.m_Pos, Quaternion.identity);
		if ((bool)ramp)
		{
			ApplyProxyToRamp(ramp, proxy);
		}
		return ramp;
	}

	public static void ApplyProxyToRamp(Ramp ramp, RampProxy proxy)
	{
		ramp.SetSplineType(proxy.m_SplineType);
		ramp.SetNumSegments(proxy.m_NumSegments);
		ramp.SetControlPoints(proxy.m_ControlPoints);
		ramp.m_FlippedVertical = proxy.m_FlippedVertical;
		ramp.m_FlippedHorizontal = proxy.m_FlippedHorizontal;
		ramp.m_FlippedLegs = proxy.m_FlippedLegs;
		ramp.m_HideLegs = proxy.m_HideLegs;
		ramp.m_Height = proxy.m_Height;
		if (ramp.m_ControlPoints[0].transform.position.x > ramp.m_ControlPoints[ramp.m_ControlPoints.Count - 1].transform.position.x)
		{
			ramp.m_ControlPoints.Reverse();
		}
		if (ramp.m_HideLegs)
		{
			ramp.m_PolesParent.gameObject.SetActive(value: false);
		}
		ramp.RecalulateNumSegments();
		ramp.RefreshMesh();
		ramp.SetLinePoints(proxy.m_LinePoints);
	}

	public static void UpdateShaderProperties(bool buildMode)
	{
		foreach (Ramp ramp in m_Ramps)
		{
			if (ramp.gameObject.activeInHierarchy)
			{
				ramp.UpdateShaderProperties(buildMode);
			}
		}
	}

	public static void EnterBuildMode()
	{
		foreach (Ramp ramp in m_Ramps)
		{
			if (ramp.gameObject.activeInHierarchy)
			{
				ramp.EnterBuildMode();
			}
		}
	}

	public static void EnterSandboxMode()
	{
		foreach (Ramp ramp in m_Ramps)
		{
			if (ramp.gameObject.activeInHierarchy)
			{
				ramp.EnterSandboxMode();
			}
		}
	}
}
