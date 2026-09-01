using System;
using System.Collections.Generic;
using Poly.Physics;
using UnityEngine;

public class HydraulicsPhase : MonoBehaviour
{
	public Sprite m_Sprite;

	[NonSerialized]
	public float m_TimeDelaySeconds;

	[NonSerialized]
	public string m_Guid;

	[NonSerialized]
	public SandboxItem m_SandboxItem;

	[NonSerialized]
	public int m_PhysicsSetIndex;

	private bool m_AudioLoopPlaying;

	private void Awake()
	{
		m_SandboxItem = GetComponent<SandboxItem>();
	}

	private void Update()
	{
		if (m_AudioLoopPlaying && IsComplete() && GameStateManager.GetState() == GameState.SIM)
		{
			StopPistonAudio();
		}
	}

	private void OnDestroy()
	{
		if (HydraulicsPhases.m_Phases.Contains(this))
		{
			HydraulicsPhases.m_Phases.Remove(this);
		}
		HydraulicsController.RemovePhase(this);
	}

	public void AddToPhysicsHydraulicController()
	{
		if (HydraulicsController.FindControllerPhaseWithHydraulicsPhase(this) == null)
		{
			return;
		}
		HydraulicController hydraulicController = Main.m_Instance.m_HydraulicController;
		Array.Resize(ref Main.m_Instance.m_HydraulicController.sets, Main.m_Instance.m_HydraulicController.sets.Length + 1);
		HydraulicController.Set set = new HydraulicController.Set();
		hydraulicController.sets[hydraulicController.sets.Length - 1] = set;
		m_PhysicsSetIndex = hydraulicController.sets.Length - 1;
		set.hydraulicEdges = new List<Edge>();
		foreach (Piston item in HydraulicsController.GetPistonsForPhase(this))
		{
			if (PistonWillSimulate(item))
			{
				set.hydraulicEdges.Add(item.m_Edge.m_PhysicsEdge);
			}
		}
		set.nodeParts = new List<NodePart>();
		foreach (BridgeSplitJoint item2 in HydraulicsController.GetSplitJointsForPhase(this))
		{
			Part part = Part.All;
			bool flag = true;
			switch (item2.m_SplitJointState)
			{
			case SplitJointState.ALL_SPLIT:
				part = Part.All;
				break;
			case SplitJointState.NONE_SPLIT:
				flag = false;
				break;
			case SplitJointState.A_SPLIT_ONLY:
				part = Part.A;
				break;
			case SplitJointState.B_SPLIT_ONLY:
				part = Part.B;
				break;
			case SplitJointState.C_SPLIT_ONLY:
				part = Part.C;
				break;
			}
			if (flag)
			{
				set.nodeParts.Add(new NodePart(item2.m_BridgeJoint.m_PhysicsNode, part));
			}
		}
	}

	public void StartSimulation()
	{
		HydraulicsControllerPhase hydraulicsControllerPhase = HydraulicsController.FindControllerPhaseWithHydraulicsPhase(this);
		if (hydraulicsControllerPhase == null)
		{
			return;
		}
		int num = 0;
		foreach (Piston piston in hydraulicsControllerPhase.m_Pistons)
		{
			if (PistonWillSimulate(piston))
			{
				num++;
			}
		}
		Main.m_Instance.m_HydraulicController.Activate(m_PhysicsSetIndex);
		if (num > 0)
		{
			StartPistonAudio();
		}
	}

	public bool IsComplete()
	{
		return !Main.m_Instance.m_HydraulicController.IsMoving();
	}

	private bool PistonWillSimulate(Piston piston)
	{
		if ((bool)piston && piston.gameObject.activeInHierarchy)
		{
			return piston.m_Edge.m_PhysicsEdge;
		}
		return false;
	}

	private void StartPistonAudio()
	{
		foreach (Piston item in HydraulicsController.GetPistonsForPhase(this))
		{
			item.PlayLoopingAudio();
		}
		m_AudioLoopPlaying = true;
	}

	private void StopPistonAudio()
	{
		foreach (Piston item in HydraulicsController.GetPistonsForPhase(this))
		{
			item.StopLoopingAudio();
		}
		m_AudioLoopPlaying = false;
	}
}
