using System.Collections.Generic;
using UnityEngine;

public class HydraulicsController
{
	public static List<HydraulicsControllerPhase> m_ControllerPhases = new List<HydraulicsControllerPhase>();

	public static Dictionary<string, bool> m_DisableNewAdditionsMap = new Dictionary<string, bool>();

	public static Color m_DisabledColor = new Color(0.59f, 0.59f, 0.59f, 0.5f);

	public static void Reset()
	{
		foreach (HydraulicsControllerPhase controllerPhase in m_ControllerPhases)
		{
			controllerPhase.m_Pistons.Clear();
			controllerPhase.m_SplitJoints.Clear();
			controllerPhase.m_DisableNewAdditions = false;
		}
		m_DisableNewAdditionsMap.Clear();
	}

	public static void DestroyAll()
	{
		m_ControllerPhases.Clear();
	}

	public static void AddPhase(HydraulicsPhase phase, List<Piston> pistons, List<BridgeJoint> joints)
	{
		HydraulicsControllerPhase item = new HydraulicsControllerPhase(phase, pistons, joints, disableNewAdditions: false);
		m_ControllerPhases.Add(item);
	}

	public static void RemovePhase(HydraulicsPhase phase)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = FindControllerPhaseWithHydraulicsPhase(phase);
		if (hydraulicsControllerPhase != null && m_ControllerPhases.Contains(hydraulicsControllerPhase))
		{
			m_ControllerPhases.Remove(hydraulicsControllerPhase);
		}
	}

	public static HydraulicsControllerPhase FindControllerPhaseWithHydraulicsPhase(HydraulicsPhase hydraulicsPhase)
	{
		foreach (HydraulicsControllerPhase controllerPhase in m_ControllerPhases)
		{
			if (controllerPhase.m_HydraulicsPhase == hydraulicsPhase)
			{
				return controllerPhase;
			}
		}
		return null;
	}

	public static HydraulicsControllerPhase FindControllerPhaseWithHydraulicsPhase(string hydraulicsPhaseGuid)
	{
		foreach (HydraulicsControllerPhase controllerPhase in m_ControllerPhases)
		{
			if (controllerPhase.m_HydraulicsPhase.m_Guid == hydraulicsPhaseGuid)
			{
				return controllerPhase;
			}
		}
		return null;
	}

	public static bool PhaseAffectsPiston(HydraulicsPhase hydraulicsPhase, Piston piston)
	{
		return FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase)?.m_Pistons.Contains(piston) ?? false;
	}

	public static bool PhaseAffectsSplitJoint(HydraulicsPhase hydraulicsPhase, BridgeJoint joint)
	{
		return FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase)?.AffectsSplitJoint(joint) ?? false;
	}

	public static List<Piston> GetPistonsForPhase(HydraulicsPhase hydraulicsPhase)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase);
		if (hydraulicsControllerPhase == null)
		{
			return new List<Piston>();
		}
		return hydraulicsControllerPhase.m_Pistons;
	}

	public static void AddAllPistonsToPhase(HydraulicsPhase hydraulicsPhase)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase);
		if (hydraulicsControllerPhase == null)
		{
			return;
		}
		foreach (Piston piston in Pistons.m_Pistons)
		{
			if (!hydraulicsControllerPhase.m_Pistons.Contains(piston))
			{
				hydraulicsControllerPhase.m_Pistons.Add(piston);
			}
		}
	}

	public static void RemoveAllPistonsFromPhase(HydraulicsPhase hydraulicsPhase)
	{
		FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase)?.m_Pistons.Clear();
	}

	public static void TogglePiston(HydraulicsPhase hydraulicsPhase, Piston piston)
	{
		if (!hydraulicsPhase || !piston)
		{
			return;
		}
		HydraulicsControllerPhase hydraulicsControllerPhase = FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase);
		if (hydraulicsControllerPhase != null)
		{
			BridgeActions.StartRecording();
			if (hydraulicsControllerPhase.m_Pistons.Contains(piston))
			{
				hydraulicsControllerPhase.m_Pistons.Remove(piston);
				BridgeActions.HydraulicsControllerRemovePiston(hydraulicsPhase, piston);
			}
			else
			{
				hydraulicsControllerPhase.m_Pistons.Add(piston);
				BridgeActions.HydraulicsControllerAddPiston(hydraulicsPhase, piston);
			}
			BridgeActions.FlushRecording();
			InterfaceAudio.Play("ui_build_select");
		}
	}

	public static void AddPistonToHydraulicsPhase(HydraulicsPhase hydraulicsPhase, Piston piston)
	{
		if ((bool)hydraulicsPhase && (bool)piston)
		{
			HydraulicsControllerPhase hydraulicsControllerPhase = FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase);
			if (hydraulicsControllerPhase != null && !hydraulicsControllerPhase.m_Pistons.Contains(piston))
			{
				hydraulicsControllerPhase.m_Pistons.Add(piston);
			}
		}
	}

	public static void AddPistonToAllPhasesAcceptingNewAdditions(Piston piston)
	{
		if (!piston)
		{
			return;
		}
		foreach (HydraulicsControllerPhase controllerPhase in m_ControllerPhases)
		{
			if (!controllerPhase.m_DisableNewAdditions && !controllerPhase.m_Pistons.Contains(piston))
			{
				controllerPhase.m_Pistons.Add(piston);
			}
		}
	}

	public static void RemovePistonFromAllPhases(Piston piston)
	{
		if (!piston)
		{
			return;
		}
		foreach (HydraulicsControllerPhase controllerPhase in m_ControllerPhases)
		{
			if (controllerPhase.m_Pistons.Contains(piston))
			{
				controllerPhase.m_Pistons.Remove(piston);
			}
		}
	}

	public static void RemoveJointFromAllPhases(BridgeJoint joint)
	{
		if (!joint)
		{
			return;
		}
		foreach (HydraulicsControllerPhase controllerPhase in m_ControllerPhases)
		{
			foreach (BridgeSplitJoint splitJoint in controllerPhase.m_SplitJoints)
			{
				if (splitJoint.m_BridgeJoint == joint)
				{
					controllerPhase.m_SplitJoints.Remove(splitJoint);
					break;
				}
			}
		}
	}

	public static List<BridgeSplitJoint> GetSplitJointsForPhase(HydraulicsPhase hydraulicsPhase)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase);
		if (hydraulicsControllerPhase == null)
		{
			return new List<BridgeSplitJoint>();
		}
		return hydraulicsControllerPhase.m_SplitJoints;
	}

	public static void AddAllSplitJointsToPhase(HydraulicsPhase hydraulicsPhase)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase);
		if (hydraulicsControllerPhase == null)
		{
			return;
		}
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (joint.m_IsSplit && joint.gameObject.activeInHierarchy)
			{
				if (!hydraulicsControllerPhase.AffectsSplitJoint(joint))
				{
					hydraulicsControllerPhase.AddSplitJoint(joint, SplitJointState.ALL_SPLIT);
				}
				else
				{
					hydraulicsControllerPhase.SetStateForJoint(joint, SplitJointState.ALL_SPLIT);
				}
			}
		}
	}

	public static void AddSplitJointToPhase(BridgeJoint joint, HydraulicsPhase hydraulicsPhase)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase);
		if (hydraulicsControllerPhase != null)
		{
			hydraulicsControllerPhase.AddSplitJoint(joint, (joint.m_SplitJointState != SplitJointState.NONE_SPLIT) ? joint.m_SplitJointState : SplitJointState.ALL_SPLIT);
			BridgeActions.StartRecording();
			BridgeActions.HydraulicsControllerAddSplitJoint(hydraulicsPhase, joint);
			BridgeActions.FlushRecording();
		}
	}

	public static void RemoveAllSplitJointsFromPhase(HydraulicsPhase hydraulicsPhase)
	{
		FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase)?.RemoveAllSplitJoints();
	}

	public static void RemoveSplitJointFromPhase(BridgeJoint joint, SplitJointState prevSplitJointState, HydraulicsPhase hydraulicsPhase)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase);
		if (hydraulicsControllerPhase != null)
		{
			hydraulicsControllerPhase.RemoveSplitJoint(joint);
			BridgeActions.StartRecording();
			BridgeActions.HydraulicsControllerRemoveSplitJoint(hydraulicsPhase, joint, prevSplitJointState);
			BridgeActions.FlushRecording();
		}
	}

	public static void ToggleSplitJoint(HydraulicsPhase hydraulicsPhase, BridgeJoint joint)
	{
		if (!hydraulicsPhase || !joint)
		{
			return;
		}
		HydraulicsControllerPhase hydraulicsControllerPhase = FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase);
		if (hydraulicsControllerPhase != null)
		{
			if (hydraulicsControllerPhase.AffectsSplitJoint(joint))
			{
				RemoveSplitJointFromPhase(joint, joint.m_SplitJointState, hydraulicsPhase);
				InterfaceAudio.Play("ui_build_splitJoint_remove");
			}
			else
			{
				joint.m_SplitJointState = SplitJointState.ALL_SPLIT;
				AddSplitJointToPhase(joint, hydraulicsPhase);
				InterfaceAudio.Play("ui_build_splitJoint_create");
			}
		}
	}

	public static void AddSplitJointToAllPhasesAcceptingNewAdditions(BridgeJoint joint)
	{
		if (!joint)
		{
			return;
		}
		foreach (HydraulicsControllerPhase controllerPhase in m_ControllerPhases)
		{
			if (!controllerPhase.m_DisableNewAdditions && !controllerPhase.AffectsSplitJoint(joint))
			{
				controllerPhase.AddSplitJoint(joint, SplitJointState.ALL_SPLIT);
			}
		}
	}

	public static void RemoveSplitJointFromAllPhases(BridgeJoint joint)
	{
		if (!joint)
		{
			return;
		}
		foreach (HydraulicsControllerPhase controllerPhase in m_ControllerPhases)
		{
			controllerPhase.RemoveSplitJoint(joint);
		}
	}

	public static void FixupSplitJointStateInAllPhases()
	{
		foreach (HydraulicsControllerPhase controllerPhase in m_ControllerPhases)
		{
			controllerPhase.FixupSplitJointState();
		}
	}

	public static SplitJointState GetSplitJointStateForPhase(HydraulicsPhase hydraulicsPhase, BridgeJoint joint)
	{
		return FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase)?.GetStateForJoint(joint) ?? SplitJointState.ALL_SPLIT;
	}

	public static void SetSplitJointStateForPhase(HydraulicsPhase hydraulicsPhase, BridgeJoint joint, SplitJointState state)
	{
		FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase)?.SetStateForJoint(joint, state);
	}

	public static void CopySplitJointState(BridgeJoint sourceBridgeJoint, BridgeJoint destBridgeJoint)
	{
		foreach (HydraulicsControllerPhase controllerPhase in m_ControllerPhases)
		{
			if (!controllerPhase.AffectsSplitJoint(sourceBridgeJoint))
			{
				controllerPhase.RemoveSplitJoint(destBridgeJoint);
				continue;
			}
			SplitJointState splitJointStateForPhase = GetSplitJointStateForPhase(controllerPhase.m_HydraulicsPhase, sourceBridgeJoint);
			SetSplitJointStateForPhase(controllerPhase.m_HydraulicsPhase, destBridgeJoint, splitJointStateForPhase);
		}
	}

	public static void EnableNewAdditionsFromPhase(HydraulicsPhase hydraulicsPhase)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase);
		if (hydraulicsControllerPhase != null)
		{
			hydraulicsControllerPhase.m_DisableNewAdditions = false;
		}
	}

	public static void DisableNewAdditionsFromPhase(HydraulicsPhase hydraulicsPhase)
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase);
		if (hydraulicsControllerPhase != null)
		{
			hydraulicsControllerPhase.m_DisableNewAdditions = true;
		}
	}

	public static void SaveDisableNewAdditionsState()
	{
		m_DisableNewAdditionsMap.Clear();
		foreach (HydraulicsControllerPhase controllerPhase in m_ControllerPhases)
		{
			if (!m_DisableNewAdditionsMap.ContainsKey(controllerPhase.m_HydraulicsPhase.m_Guid))
			{
				m_DisableNewAdditionsMap.Add(controllerPhase.m_HydraulicsPhase.m_Guid, controllerPhase.m_DisableNewAdditions);
			}
		}
	}

	public static void RestoreDisableNewAdditionsState()
	{
		foreach (KeyValuePair<string, bool> item in m_DisableNewAdditionsMap)
		{
			HydraulicsPhase hydraulicsPhase = HydraulicsPhases.FindByGuid(item.Key);
			if (hydraulicsPhase != null)
			{
				HydraulicsControllerPhase hydraulicsControllerPhase = FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase);
				if (hydraulicsControllerPhase != null)
				{
					hydraulicsControllerPhase.m_DisableNewAdditions = item.Value;
				}
			}
		}
	}

	public static void OnLayoutLoaded()
	{
		m_DisableNewAdditionsMap.Clear();
	}

	public static HydraulicsControllerProxy Serialize()
	{
		return new HydraulicsControllerProxy();
	}

	public static void Deserialize(int version, HydraulicsControllerProxy proxy)
	{
		if (proxy == null)
		{
			return;
		}
		m_ControllerPhases.Clear();
		if (HydraulicsPhases.m_Phases.Count > 0 && proxy.m_Phases.Count == 0)
		{
			foreach (HydraulicsPhase phase in HydraulicsPhases.m_Phases)
			{
				AddPhase(phase, Pistons.m_Pistons, BridgeJoints.GetSplitjoints());
			}
		}
		foreach (HydraulicsControllerPhaseProxy phase2 in proxy.m_Phases)
		{
			HydraulicsPhase hydraulicsPhase = HydraulicsPhases.FindByGuid(phase2.m_HydraulicsPhaseGuid);
			if (hydraulicsPhase == null)
			{
				Debug.LogWarningFormat("Could not find HydraulicsPhase with GUID {0} when deserializing Hydraulic Controller", phase2.m_HydraulicsPhaseGuid);
				continue;
			}
			List<Piston> list = new List<Piston>();
			foreach (string pistonGuid in phase2.m_PistonGuids)
			{
				Piston piston = Pistons.FindByGuid(pistonGuid);
				if ((bool)piston)
				{
					list.Add(piston);
				}
			}
			List<BridgeJoint> list2 = new List<BridgeJoint>();
			foreach (BridgeSplitJointProxy bridgeSplitJoint in phase2.m_BridgeSplitJoints)
			{
				BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(bridgeSplitJoint.m_BridgeJointGuid);
				if ((bool)bridgeJoint)
				{
					bridgeJoint.SetSplitJointState(bridgeSplitJoint.m_SplitJointState);
					list2.Add(bridgeJoint);
					if (bridgeJoint.m_SplitJointState == SplitJointState.NONE_SPLIT)
					{
						bridgeJoint.SetSplitJointState(SplitJointState.ALL_SPLIT);
					}
				}
			}
			if (FindControllerPhaseWithHydraulicsPhase(hydraulicsPhase) == null)
			{
				m_ControllerPhases.Add(new HydraulicsControllerPhase(hydraulicsPhase, list, list2, phase2.m_DisableNewAdditions));
			}
			if (version < 3)
			{
				AddAllSplitJointsToPhase(hydraulicsPhase);
			}
		}
	}

	public static bool HasDataToClear()
	{
		foreach (HydraulicsControllerPhase controllerPhase in m_ControllerPhases)
		{
			if (controllerPhase.HasDataToClear())
			{
				return true;
			}
		}
		return false;
	}
}
