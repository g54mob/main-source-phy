using System.Collections.Generic;
using DarkTonic.MasterAudio;
using UnityEngine;

public class Pistons
{
	public static List<Piston> m_Pistons = new List<Piston>();

	public static PistonSlider m_SliderFollowingMouse;

	public static float MAX_SPEED = 1f;

	public static float MAX_ACCELERATION = 0.25f;

	private static GameObject m_PistonsContainer;

	private static Vector2 m_OffsetFromPointer;

	private static float m_NormalizedSliderValueWhenStartMoving;

	private static SoundGroupVariation m_PistonUILoop;

	private static float m_LastSliderValue;

	private static float m_targetDelta;

	private static readonly int MAX_RAYCAST_HITS = 32;

	private static RaycastHit2D[] m_RaycastHitsBuffer = new RaycastHit2D[MAX_RAYCAST_HITS];

	public static Piston CreatePiston(BridgeJoint jointA, BridgeJoint jointB, float normalizedValue, string guid)
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_Piston, GetPistonsContainerTransform());
		if (!gameObject)
		{
			return null;
		}
		Piston component = gameObject.GetComponent<Piston>();
		if (!component)
		{
			return null;
		}
		component.name = Prefabs.m_Instance.m_Piston.name;
		component.m_JointA = jointA;
		component.m_JointB = jointB;
		component.m_Guid = guid;
		component.m_Edge = BridgeEdges.GetEdgeFromJoints(jointA, jointB);
		component.m_EdgeCollider = component.m_Edge.m_MeshRenderer.GetComponent<BoxCollider>();
		component.m_Slider.SetNormalizedValue(normalizedValue);
		component.m_Slider.UpdateManual();
		component.m_ExtenderLimits.gameObject.SetActive(value: false);
		component.UpdatePinions();
		component.CreateToolTip();
		m_Pistons.Add(component);
		component.m_Edge.CreateHydraulicVisualization();
		return component;
	}

	public static void DestroyPiston(Piston piston)
	{
		Object.Destroy(piston.gameObject);
		if (m_Pistons.Contains(piston))
		{
			m_Pistons.Remove(piston);
		}
	}

	public static void UpdateManual()
	{
		if ((bool)m_SliderFollowingMouse && !m_SliderFollowingMouse.gameObject.activeInHierarchy)
		{
			ForceStopSliderFollowingMouse();
		}
		if (GameStateManager.GetState() == GameState.BUILD)
		{
			if (GameInput.JustReleased(BindingType.DRAW_BUILD) || GameUI.SaveLoadPanelIsActive() || GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
			{
				ForceSliderRelease();
			}
			PistonSlider pistonSliderUnderMouse = GetPistonSliderUnderMouse();
			if ((bool)pistonSliderUnderMouse && !pistonSliderUnderMouse.m_Piston.m_Edge.IsLocked() && GameInput.GetMouseButtonJustPressed(0) && GameToolMode.GetMode() == GameToolModeType.BUILD && !GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
			{
				StartMovingSlider(pistonSliderUnderMouse, GameInput.GetMousePosition());
				BridgeJointPlacement.CancelSelection();
			}
			UpdateMovingSlider();
		}
		foreach (Piston piston in m_Pistons)
		{
			if (piston.gameObject.activeInHierarchy)
			{
				piston.UpdateManual();
			}
		}
		if (!m_SliderFollowingMouse && m_PistonUILoop != null)
		{
			m_PistonUILoop.FadeOutNowAndStop();
			m_PistonUILoop = null;
		}
	}

	public static void ForceStopSliderFollowingMouse()
	{
		m_SliderFollowingMouse = null;
		if (m_PistonUILoop != null)
		{
			m_PistonUILoop.FadeOutNowAndStop();
			m_PistonUILoop = null;
		}
	}

	public static Piston GetPistonOnEdge(BridgeEdge edge)
	{
		foreach (Piston piston in m_Pistons)
		{
			if (piston.m_Edge == edge)
			{
				return piston;
			}
		}
		return null;
	}

	public static void EnableOnEdge(BridgeEdge edge)
	{
		Piston pistonOnEdge = GetPistonOnEdge(edge);
		if ((bool)pistonOnEdge)
		{
			pistonOnEdge.gameObject.SetActive(value: true);
		}
	}

	public static void DisableOnEdge(BridgeEdge edge)
	{
		Piston pistonOnEdge = GetPistonOnEdge(edge);
		if ((bool)pistonOnEdge)
		{
			pistonOnEdge.gameObject.SetActive(value: false);
		}
	}

	public static void DestroyAll()
	{
		foreach (Piston piston in m_Pistons)
		{
			Object.Destroy(piston.gameObject);
		}
		m_Pistons.Clear();
	}

	public static void HideAllUI()
	{
		foreach (Piston piston in m_Pistons)
		{
			piston.m_Slider.gameObject.SetActive(value: false);
			piston.m_ExtenderLimits.gameObject.SetActive(value: false);
		}
	}

	public static void UnHideAllUI()
	{
		foreach (Piston piston in m_Pistons)
		{
			piston.m_Slider.gameObject.SetActive(value: true);
			piston.m_ExtenderLimits.gameObject.SetActive(value: true);
		}
	}

	public static void DisablePinions()
	{
		foreach (Piston piston in m_Pistons)
		{
			if (piston.gameObject.activeInHierarchy)
			{
				piston.m_PinionA.SetActive(value: false);
				piston.m_PinionB.SetActive(value: false);
			}
		}
	}

	public static void EnablePinions()
	{
		foreach (Piston piston in m_Pistons)
		{
			if (piston.gameObject.activeInHierarchy)
			{
				if (!PinionIsActiveOnJoint(piston.m_JointA))
				{
					piston.m_PinionA.SetActive(value: true);
				}
				if (!PinionIsActiveOnJoint(piston.m_JointB))
				{
					piston.m_PinionB.SetActive(value: true);
				}
			}
		}
	}

	public static bool PinionIsActiveOnJoint(BridgeJoint joint)
	{
		foreach (Piston piston in m_Pistons)
		{
			if (piston.m_JointA == joint && piston.m_PinionA.activeInHierarchy)
			{
				return true;
			}
			if (piston.m_JointB == joint && piston.m_PinionB.activeInHierarchy)
			{
				return true;
			}
		}
		return false;
	}

	public static Transform GetPistonsContainerTransform()
	{
		if (!m_PistonsContainer)
		{
			m_PistonsContainer = new GameObject("Pistons");
		}
		return m_PistonsContainer.transform;
	}

	public static bool MouseIsOverPistonSlider()
	{
		PistonSlider pistonSliderUnderMouse = GetPistonSliderUnderMouse();
		if (!pistonSliderUnderMouse || pistonSliderUnderMouse.m_Piston.m_Edge.IsLocked())
		{
			return false;
		}
		return true;
	}

	public static PistonSlider GetPistonSliderUnderMouse()
	{
		if ((bool)Utils.GetClosestRaycastHit(GameInput.GetMousePosition(), Utils.JOINT_SELECTOR_LAYER_MASK))
		{
			return null;
		}
		return GetPistonSliderUnderMouseSkipJointSelectorCheck();
	}

	public static PistonSlider GetPistonSliderUnderMouseSkipJointSelectorCheck()
	{
		int rayIntersectionNonAlloc = Physics2D.GetRayIntersectionNonAlloc(Cameras.MainCamera().ScreenPointToRay(GameInput.GetMousePosition()), m_RaycastHitsBuffer, float.PositiveInfinity, Utils.PISTON_LAYER_MASK);
		for (int i = 0; i < rayIntersectionNonAlloc; i++)
		{
			RaycastHit2D raycastHit2D = m_RaycastHitsBuffer[i];
			if (raycastHit2D.collider != null)
			{
				PistonSlider component = raycastHit2D.collider.GetComponent<PistonSlider>();
				if ((bool)component && component.m_Handle.gameObject.activeInHierarchy)
				{
					return component;
				}
			}
		}
		return null;
	}

	public static List<Piston> GetPistonsConnectedToJoint(BridgeJoint joint)
	{
		List<Piston> list = new List<Piston>();
		foreach (Piston piston in m_Pistons)
		{
			if (piston.gameObject.activeInHierarchy && (piston.m_JointA == joint || piston.m_JointB == joint))
			{
				list.Add(piston);
			}
		}
		return list;
	}

	public static Piston FindByGuid(string guid)
	{
		foreach (Piston piston in m_Pistons)
		{
			if (piston.m_Guid == guid)
			{
				return piston;
			}
		}
		return null;
	}

	public static int GetNumPistons()
	{
		int num = 0;
		foreach (Piston piston in m_Pistons)
		{
			if (piston.gameObject.activeInHierarchy)
			{
				num++;
			}
		}
		return num;
	}

	public static List<PistonProxy> Serialize()
	{
		List<PistonProxy> list = new List<PistonProxy>();
		foreach (Piston piston in m_Pistons)
		{
			if (piston.gameObject.activeInHierarchy)
			{
				list.Add(new PistonProxy(piston));
			}
		}
		return list;
	}

	public static void Deserialize(List<PistonProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (PistonProxy proxy in proxies)
		{
			CreatePistonFromProxy(proxy);
		}
	}

	public static Piston CreatePistonFromProxy(PistonProxy proxy)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(proxy.m_NodeA_Guid);
		BridgeJoint bridgeJoint2 = BridgeJoints.FindByGuid(proxy.m_NodeB_Guid);
		if ((bool)bridgeJoint && (bool)bridgeJoint2)
		{
			Piston piston = CreatePiston(bridgeJoint, bridgeJoint2, proxy.m_NormalizedValue, string.IsNullOrEmpty(proxy.m_Guid) ? Utils.GenerateUniqueId() : proxy.m_Guid);
			if ((bool)piston)
			{
				piston.m_Slider.gameObject.SetActive(value: false);
				return piston;
			}
		}
		return null;
	}

	public static void StartMovingSlider(PistonSlider slider, Vector3 mousePosition)
	{
		if (!CampaignTutorial.CanShowPistonSlider(slider) || !slider || (bool)m_SliderFollowingMouse)
		{
			return;
		}
		if (m_PistonUILoop == null)
		{
			PlaySoundResult playSoundResult = MasterAudio.PlaySound("ui_build_hydraulics_slider_drag_lp");
			if (playSoundResult != null)
			{
				m_PistonUILoop = playSoundResult.ActingVariation;
			}
		}
		m_SliderFollowingMouse = slider;
		m_NormalizedSliderValueWhenStartMoving = slider.GetNormalizedValue();
		Vector2 vector = (Vector2)Cameras.MainCamera().WorldToScreenPoint(slider.transform.position) - Utils.V3toV2(mousePosition);
		m_OffsetFromPointer = new Vector2(vector.x, vector.y);
	}

	public static void UpdateMovingSlider()
	{
		if ((bool)m_SliderFollowingMouse)
		{
			Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(m_OffsetFromPointer + Utils.V3toV2(GameInput.GetMousePosition()));
			Piston piston = m_SliderFollowingMouse.m_Piston;
			Vector3 normalized = (piston.m_JointB.transform.position - piston.m_JointA.transform.position).normalized;
			float length = piston.m_Edge.GetLength();
			Vector3 vector = (piston.m_JointA.transform.position + piston.m_JointB.transform.position) / 2f;
			Vector3 end = piston.m_JointB.transform.position + normalized * (length / 2f);
			float num = Mathf.Clamp01((Utils.NearestPointOnLineSegment(vector, end, worldPointFromScreenPos) - vector).magnitude / length);
			UpdateSliderAudio(num - m_LastSliderValue);
			m_LastSliderValue = num;
			piston.m_Slider.SetNormalizedValue(num);
			piston.m_Edge.ReinitHydraulicVisualization();
		}
	}

	public static void UpdateSliderAudio(float delta)
	{
		if (delta * m_targetDelta < 0f || (delta > 0f && delta > m_targetDelta) || (delta < 0f && delta < m_targetDelta))
		{
			m_targetDelta = delta;
		}
		if (m_PistonUILoop != null)
		{
			m_PistonUILoop.VarAudio.volume = Mathf.Lerp(m_PistonUILoop.VarAudio.volume, Mathf.Abs(m_targetDelta), 0.5f);
		}
		m_targetDelta = ((m_targetDelta > 0f) ? Mathf.Max(0f, m_targetDelta - 0.07f) : Mathf.Min(0f, m_targetDelta + 0.07f));
	}

	public static void ForceSliderRelease()
	{
		if ((bool)m_SliderFollowingMouse)
		{
			float num = m_SliderFollowingMouse.GetNormalizedValue() - m_NormalizedSliderValueWhenStartMoving;
			if (!Mathf.Approximately(num, 0f))
			{
				BridgeActions.StartRecording();
				BridgeActions.TranslatePistonSlider(m_SliderFollowingMouse.m_Piston, num);
				BridgeActions.FlushRecording();
			}
			ForceStopSliderFollowingMouse();
		}
	}
}
