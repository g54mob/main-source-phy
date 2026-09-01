using System.Collections.Generic;
using UnityEngine;

public class VehicleRestartPhases
{
	public static List<VehicleRestartPhase> m_Phases = new List<VehicleRestartPhase>();

	public static VehicleRestartPhase CreatePhase(Vector3 pos, string guid, string vehicleGuid)
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_VehicleRestartPhase, pos, Quaternion.identity);
		if (!gameObject)
		{
			return null;
		}
		VehicleRestartPhase component = gameObject.GetComponent<VehicleRestartPhase>();
		if (!component)
		{
			return null;
		}
		component.name = "VehicleRestartPhase";
		component.m_Guid = guid;
		component.m_VehicleGuid = vehicleGuid;
		m_Phases.Add(component);
		return component;
	}

	public static void DestroyPhase(VehicleRestartPhase phase)
	{
		EventEditor.RemoveUnit(phase.gameObject);
		if (m_Phases.Contains(phase))
		{
			m_Phases.Remove(phase);
		}
		phase.gameObject.SetActive(value: false);
		Object.Destroy(phase.gameObject);
	}

	public static void DestroyAll()
	{
		for (int num = m_Phases.Count - 1; num >= 0; num--)
		{
			DestroyPhase(m_Phases[num]);
		}
		m_Phases.Clear();
	}

	public static VehicleRestartPhase FindByGuid(string guid)
	{
		foreach (VehicleRestartPhase phase in m_Phases)
		{
			if (phase.m_Guid == guid)
			{
				return phase;
			}
		}
		return null;
	}

	public static void RefreshEventUnitLabels()
	{
		foreach (VehicleRestartPhase phase in m_Phases)
		{
			phase.RefreshEventUnitLabel();
		}
	}

	public static List<VehicleRestartPhaseProxy> Serialize()
	{
		List<VehicleRestartPhaseProxy> list = new List<VehicleRestartPhaseProxy>();
		foreach (VehicleRestartPhase phase in m_Phases)
		{
			if (phase.gameObject.activeInHierarchy)
			{
				list.Add(new VehicleRestartPhaseProxy(phase));
			}
		}
		return list;
	}

	public static void Deserialize(List<VehicleRestartPhaseProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (VehicleRestartPhaseProxy proxy in proxies)
		{
			CreatePhaseFromProxy(proxy);
		}
	}

	public static VehicleRestartPhase CreatePhaseFromProxy(VehicleRestartPhaseProxy proxy)
	{
		VehicleRestartPhase vehicleRestartPhase = CreatePhase(Vector3.zero, proxy.m_Guid, proxy.m_VehicleGuid);
		if ((bool)vehicleRestartPhase)
		{
			ApplyProxyToPhase(vehicleRestartPhase, proxy);
		}
		return vehicleRestartPhase;
	}

	public static void ApplyProxyToPhase(VehicleRestartPhase phase, VehicleRestartPhaseProxy proxy)
	{
		phase.m_TimeDelaySeconds = proxy.m_TimeDelaySeconds;
		phase.AddToEventEdtior();
	}
}
