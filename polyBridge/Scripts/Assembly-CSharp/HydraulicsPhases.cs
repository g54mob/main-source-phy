using System.Collections.Generic;
using UnityEngine;

public class HydraulicsPhases
{
	public static List<HydraulicsPhase> m_Phases = new List<HydraulicsPhase>();

	public static HydraulicsPhase CreatePhase(Vector3 pos, string guid)
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_HydraulicsPhase, pos, Quaternion.identity);
		if (!gameObject)
		{
			return null;
		}
		HydraulicsPhase component = gameObject.GetComponent<HydraulicsPhase>();
		if (!component)
		{
			return null;
		}
		component.name = "HydraulicsPhase";
		component.m_Guid = guid;
		m_Phases.Add(component);
		return component;
	}

	public static void DestroyPhase(HydraulicsPhase phase)
	{
		EventEditor.RemoveUnit(phase.gameObject);
		Object.Destroy(phase.gameObject);
	}

	public static void DestroyAll()
	{
		foreach (HydraulicsPhase phase in m_Phases)
		{
			Object.Destroy(phase.gameObject);
		}
		m_Phases.Clear();
	}

	public static HydraulicsPhase FindByGuid(string guid)
	{
		foreach (HydraulicsPhase phase in m_Phases)
		{
			if (phase.m_Guid == guid)
			{
				return phase;
			}
		}
		return null;
	}

	public static void AddToHydraulicController()
	{
		if (!SandboxSettings.m_ThreeWaySplitJointsEnabled)
		{
			HydraulicsController.FixupSplitJointStateInAllPhases();
		}
		foreach (HydraulicsPhase phase in m_Phases)
		{
			phase.AddToPhysicsHydraulicController();
		}
	}

	public static List<HydraulicsPhaseProxy> Serialize()
	{
		List<HydraulicsPhaseProxy> list = new List<HydraulicsPhaseProxy>();
		foreach (HydraulicsPhase phase in m_Phases)
		{
			if (phase.gameObject.activeInHierarchy)
			{
				list.Add(new HydraulicsPhaseProxy(phase));
			}
		}
		return list;
	}

	public static void Deserialize(List<HydraulicsPhaseProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (HydraulicsPhaseProxy proxy in proxies)
		{
			CreatePhaseFromProxy(proxy);
		}
	}

	public static HydraulicsPhase CreatePhaseFromProxy(HydraulicsPhaseProxy proxy)
	{
		HydraulicsPhase hydraulicsPhase = CreatePhase(Vector3.zero, proxy.m_Guid);
		if ((bool)hydraulicsPhase)
		{
			ApplyProxyToPhase(hydraulicsPhase, proxy);
		}
		return hydraulicsPhase;
	}

	public static void ApplyProxyToPhase(HydraulicsPhase phase, HydraulicsPhaseProxy proxy)
	{
		phase.m_TimeDelaySeconds = proxy.m_TimeDelaySeconds;
		HydraulicsController.AddPhase(phase, new List<Piston>(), new List<BridgeJoint>());
	}
}
