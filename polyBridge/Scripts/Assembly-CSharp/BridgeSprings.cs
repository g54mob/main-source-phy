using System.Collections.Generic;
using DarkTonic.MasterAudio;
using UnityEngine;

public class BridgeSprings
{
	public static readonly float MIN_FREELENGTH_MULTIPLIER = 0.5f;

	public static readonly float MAX_FREELENGTH_MULTIPLIER = 2f;

	public static readonly float DEFAULT_NORMALIZED_VALUE = 0.5f;

	public static List<BridgeSpring> m_BridgeSprings = new List<BridgeSpring>();

	public static BridgeSpringSlider m_SliderFollowingMouse;

	private static Vector2 m_OffsetFromPointer;

	private static float m_NormalizedSliderValueWhenStartMoving;

	private static SoundGroupVariation m_SpringUILoop;

	private static int m_LastSliderValue;

	private static int m_targetDelta;

	private static readonly int SPRING_SLIDER_INTERVAL = 5;

	private static readonly int MAX_RAYCAST_HITS = 32;

	private static RaycastHit2D[] m_RaycastHitsBuffer = new RaycastHit2D[MAX_RAYCAST_HITS];

	public static BridgeSpring CreateSpring(BridgeEdge edge, float normalizedValue, string guid)
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_Spring, edge.transform);
		if (!gameObject)
		{
			return null;
		}
		BridgeSpring component = gameObject.GetComponent<BridgeSpring>();
		if (!component)
		{
			return null;
		}
		GameObject linkPrefabFromMaterial = BridgeMaterials.GetLinkPrefabFromMaterial(BridgeMaterialType.SPRING);
		component.Init(edge, linkPrefabFromMaterial, normalizedValue, guid);
		m_BridgeSprings.Add(component);
		return component;
	}

	public static void Remove(BridgeEdge edge)
	{
		BridgeSpring springCoilVisualization = edge.m_SpringCoilVisualization;
		edge.m_SpringCoilVisualization = null;
		m_BridgeSprings.Remove(springCoilVisualization);
		springCoilVisualization.DestroyManual();
	}

	public static void UpdateManual()
	{
		if ((bool)m_SliderFollowingMouse && !m_SliderFollowingMouse.gameObject.activeInHierarchy)
		{
			ForceStopSliderFollowingMouse();
		}
		if (GameStateManager.GetState() == GameState.BUILD)
		{
			if ((GameInput.JustReleased(BindingType.DRAW_BUILD) || GameUI.SaveLoadPanelIsActive()) && (bool)m_SliderFollowingMouse)
			{
				ForceSliderRelease();
			}
			BridgeSpringSlider springSliderUnderMouse = GetSpringSliderUnderMouse();
			if ((bool)springSliderUnderMouse && !springSliderUnderMouse.m_BridgeSpring.m_ParentEdge.IsLocked() && SandboxSettings.m_SpringAdjustmentsAllowed && springSliderUnderMouse.m_Handle.gameObject.activeInHierarchy && GameInput.GetMouseButtonJustPressed(0) && GameToolMode.GetMode() == GameToolModeType.BUILD && !GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
			{
				StartMovingSlider(springSliderUnderMouse, GameInput.GetMousePosition());
				BridgeJointPlacement.CancelSelection();
			}
			UpdateMovingSlider();
		}
		foreach (BridgeSpring bridgeSpring in m_BridgeSprings)
		{
			if (bridgeSpring.gameObject.activeInHierarchy)
			{
				bridgeSpring.UpdateManual();
			}
		}
		if (!m_SliderFollowingMouse && m_SpringUILoop != null)
		{
			m_SpringUILoop.FadeOutNowAndStop();
			m_SpringUILoop = null;
		}
	}

	public static void FixedUpdateManual()
	{
	}

	public static void DestroyAll()
	{
		foreach (BridgeSpring bridgeSpring in m_BridgeSprings)
		{
			bridgeSpring.DestroyManual();
		}
		m_BridgeSprings.Clear();
	}

	public static BridgeSpring FindByGuid(string guid)
	{
		foreach (BridgeSpring bridgeSpring in m_BridgeSprings)
		{
			if (bridgeSpring.m_Guid == guid)
			{
				return bridgeSpring;
			}
		}
		return null;
	}

	public static void SetStressColorForEdge(BridgeEdge edge, Color stressColor)
	{
		edge.m_SpringCoilVisualization.SetStressColor(stressColor);
	}

	public static void Desaturate(BridgeEdge edge, bool desaturate)
	{
		edge.m_SpringCoilVisualization.Desaturate(desaturate);
	}

	public static void StartMovingSlider(BridgeSpringSlider slider, Vector3 mousePosition)
	{
		if (!slider || (bool)m_SliderFollowingMouse)
		{
			return;
		}
		m_SliderFollowingMouse = slider;
		m_NormalizedSliderValueWhenStartMoving = slider.GetNormalizedValue();
		Vector2 vector = (Vector2)Cameras.MainCamera().WorldToScreenPoint(slider.transform.position) - Utils.V3toV2(mousePosition);
		m_OffsetFromPointer = new Vector2(vector.x, vector.y);
		if (!SandboxSettings.m_SpringAdjustmentsAllowed)
		{
			GameUI.ShowMessage(ScreenMessageLocation.TOP_CENTER, Localize.Get("UI_NO_SPRING_ADJUSTMENTS_WARNING"), 4f);
		}
		else if (m_SpringUILoop == null)
		{
			PlaySoundResult playSoundResult = MasterAudio.PlaySound("ui_build_spring_slider_drag_lp");
			if (playSoundResult != null)
			{
				m_SpringUILoop = playSoundResult.ActingVariation;
			}
		}
	}

	public static void UpdateMovingSlider()
	{
		if ((bool)m_SliderFollowingMouse && SandboxSettings.m_SpringAdjustmentsAllowed)
		{
			Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(m_OffsetFromPointer + Utils.V3toV2(GameInput.GetMousePosition()));
			BridgeSpring bridgeSpring = m_SliderFollowingMouse.m_BridgeSpring;
			Vector3 normalized = (bridgeSpring.m_ParentEdge.m_JointB.transform.position - bridgeSpring.m_ParentEdge.m_JointA.transform.position).normalized;
			float length = bridgeSpring.m_ParentEdge.GetLength();
			Vector3 adjustedStartPos = bridgeSpring.m_Slider.GetAdjustedStartPos(normalized, length);
			Vector3 position = bridgeSpring.m_ParentEdge.m_JointB.transform.position;
			Vector3 vector = Utils.NearestPointOnLineSegment(adjustedStartPos, position, worldPointFromScreenPos);
			float adjustedLen = bridgeSpring.m_Slider.GetAdjustedLen(length);
			int num = Mathf.RoundToInt(100f * Mathf.Clamp01((vector - adjustedStartPos).magnitude / adjustedLen) / (float)SPRING_SLIDER_INTERVAL) * SPRING_SLIDER_INTERVAL;
			UpdateSliderAudio(num - m_LastSliderValue);
			m_LastSliderValue = num;
			bridgeSpring.m_Slider.SetNormalizedValue((float)num / 100f);
		}
	}

	public static void UpdateSliderAudio(int delta)
	{
		if (delta * m_targetDelta < 0 || (delta > 0 && delta > m_targetDelta) || (delta < 0 && delta < m_targetDelta))
		{
			m_targetDelta = delta;
		}
		if (m_SpringUILoop != null)
		{
			m_SpringUILoop.VarAudio.volume = Mathf.Lerp(m_SpringUILoop.VarAudio.volume, (float)Mathf.Abs(m_targetDelta) / 100f, 0.5f);
		}
		m_targetDelta = ((m_targetDelta > 0) ? Mathf.Max(0, m_targetDelta - 7) : Mathf.Min(0, m_targetDelta + 7));
	}

	public static bool MouseIsOverSpringSlider()
	{
		BridgeSpringSlider springSliderUnderMouse = GetSpringSliderUnderMouse();
		if (!springSliderUnderMouse || springSliderUnderMouse.m_BridgeSpring.m_ParentEdge.IsLocked())
		{
			return false;
		}
		return true;
	}

	public static void HideAllUI()
	{
		foreach (BridgeSpring bridgeSpring in m_BridgeSprings)
		{
			bridgeSpring.m_Slider.gameObject.SetActive(value: false);
		}
	}

	public static void UnHideAllUI()
	{
		foreach (BridgeSpring bridgeSpring in m_BridgeSprings)
		{
			bridgeSpring.m_Slider.gameObject.SetActive(value: true);
		}
	}

	public static List<BridgeSpringProxy> Serialize()
	{
		List<BridgeSpringProxy> list = new List<BridgeSpringProxy>();
		foreach (BridgeSpring bridgeSpring in m_BridgeSprings)
		{
			if (bridgeSpring.gameObject.activeInHierarchy)
			{
				list.Add(new BridgeSpringProxy(bridgeSpring));
			}
		}
		return list;
	}

	public static void Deserialize(List<BridgeSpringProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (BridgeSpringProxy proxy in proxies)
		{
			CreateSpringFromProxy(proxy);
		}
	}

	public static BridgeSpring CreateSpringFromProxy(BridgeSpringProxy proxy)
	{
		BridgeJoint a = BridgeJoints.FindByGuid(proxy.m_NodeA_Guid);
		BridgeJoint b = BridgeJoints.FindByGuid(proxy.m_NodeB_Guid);
		BridgeEdge bridgeEdge = BridgeEdges.FindEnabledEdgeByJoints(a, b, BridgeMaterialType.SPRING);
		if (bridgeEdge != null)
		{
			return CreateSpring(bridgeEdge, proxy.m_NormalizedValue, proxy.m_Guid);
		}
		return null;
	}

	public static BridgeSpringSlider GetSpringSliderUnderMouse()
	{
		if ((bool)Utils.GetClosestRaycastHit(GameInput.GetMousePosition(), Utils.JOINT_SELECTOR_LAYER_MASK))
		{
			return null;
		}
		return GetSpringSliderUnderMouseSkipJointSelectorCheck();
	}

	public static BridgeSpringSlider GetSpringSliderUnderMouseSkipJointSelectorCheck()
	{
		int rayIntersectionNonAlloc = Physics2D.GetRayIntersectionNonAlloc(Cameras.MainCamera().ScreenPointToRay(GameInput.GetMousePosition()), m_RaycastHitsBuffer, float.PositiveInfinity, Utils.SPRING_LAYER_MASK);
		for (int i = 0; i < rayIntersectionNonAlloc; i++)
		{
			RaycastHit2D raycastHit2D = m_RaycastHitsBuffer[i];
			if (raycastHit2D.collider != null)
			{
				BridgeSpringSlider component = raycastHit2D.collider.GetComponent<BridgeSpringSlider>();
				if ((bool)component && component.m_Handle.gameObject.activeInHierarchy)
				{
					return component;
				}
			}
		}
		return null;
	}

	public static void ForceStopSliderFollowingMouse()
	{
		m_SliderFollowingMouse = null;
		if (m_SpringUILoop != null)
		{
			m_SpringUILoop.FadeOutNowAndStop();
			m_SpringUILoop = null;
		}
	}

	public static void ForceSliderRelease()
	{
		if ((bool)m_SliderFollowingMouse)
		{
			float num = m_SliderFollowingMouse.GetNormalizedValue() - m_NormalizedSliderValueWhenStartMoving;
			if (!Mathf.Approximately(num, 0f))
			{
				BridgeActions.StartRecording();
				BridgeActions.TranslateSpringSlider(m_SliderFollowingMouse.m_BridgeSpring, num);
				BridgeActions.FlushRecording();
				m_SliderFollowingMouse.m_BridgeSpring.RefreshVisualization();
			}
			ForceStopSliderFollowingMouse();
		}
	}

	public static void RemoveAllAdjustmentsForUnlocked()
	{
		foreach (BridgeSpring bridgeSpring in m_BridgeSprings)
		{
			if (bridgeSpring.gameObject.activeInHierarchy && !bridgeSpring.m_ParentEdge.IsLocked())
			{
				bridgeSpring.m_FreeLengthOverrideMultiplier = 1f;
				bridgeSpring.m_Slider.SetNormalizedValue(DEFAULT_NORMALIZED_VALUE);
				bridgeSpring.RefreshVisualization();
			}
		}
	}

	public static bool SpringsHaveBeenAdjusted()
	{
		foreach (BridgeSpring bridgeSpring in m_BridgeSprings)
		{
			if (!Mathf.Approximately(bridgeSpring.m_Slider.GetNormalizedValue(), DEFAULT_NORMALIZED_VALUE))
			{
				return true;
			}
		}
		return false;
	}
}
