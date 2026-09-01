using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class BridgeJointSelectors
{
	public static List<BridgeJointSelector> m_Selectors = new List<BridgeJointSelector>();

	public static Vector3 SCALE_ROAD = new Vector3(0.6f, 0.6f, 1f);

	public static Vector3 SCALE_NONROAD = new Vector3(0.5f, 0.5f, 1f);

	private static BridgeJoint m_JointUnderMouse;

	private static BridgeJoint m_JointSelected;

	private static BridgeJoint m_JointForCircle;

	private static float m_NumSecondsJointUnderMouse;

	private static float m_PersistUntilTimer;

	private static int NUM_CIRCLE_SEGMENTS = 64;

	private static float CIRCLE_RADIUS = 2f;

	private static VectorLine m_VectorLineCircle;

	public static bool m_DebugSplitJointHoverTest = true;

	public static bool m_DebugSplitJointHoverCircle;

	public static float m_DebugSplitJointHoverTime = 0f;

	public static float m_DebugSplitJointPersistTime = 0.5f;

	private static float SHOW_BRIDGE_JOINT_SELECTOR_FOR_UNDO_SECONDS = 1f;

	private static BridgeJointSelector m_BridgeJointSelectorShowForUndo;

	private static float m_ShowBridgeJointSelectorForUndoTimer;

	private static readonly int MAX_RAYCAST_HITS = 32;

	private static RaycastHit2D[] m_RaycastHitsBuffer = new RaycastHit2D[MAX_RAYCAST_HITS];

	public static void Init()
	{
	}

	public static void OnLayoutLoaded()
	{
		if (m_VectorLineCircle != null)
		{
			VectorLine.Destroy(ref m_VectorLineCircle);
			m_VectorLineCircle = null;
		}
		m_JointForCircle = null;
		m_JointUnderMouse = null;
		m_ShowBridgeJointSelectorForUndoTimer = float.MinValue;
	}

	public static BridgeJointSelector Create(BridgeEdge edge, BridgeJointSelectorSide side)
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_JointSelector);
		if (!gameObject)
		{
			return null;
		}
		BridgeJointSelector component = gameObject.GetComponent<BridgeJointSelector>();
		if ((bool)component)
		{
			component.name = Prefabs.m_Instance.m_JointSelector.name;
			component.m_Edge = edge;
			component.m_Side = side;
			component.transform.SetParent(edge.transform);
			component.transform.rotation = Quaternion.identity;
			component.gameObject.SetActive(component.IsVisible());
			m_Selectors.Add(component);
		}
		return component;
	}

	public static void UpdateManual()
	{
		if (!m_DebugSplitJointHoverTest)
		{
			UpdateHighlightState();
		}
	}

	public static void LateUpdateManual()
	{
		if (!m_DebugSplitJointHoverTest)
		{
			return;
		}
		if (!GameStateCommonInput.IgnoreKeyboardInput() && GameInput.IsDown(BindingType.SHOW_ALL_SPLIT_JOINT_NUMBERS))
		{
			EnableAllJointSelectors();
			return;
		}
		if (!GameStateCommonInput.IgnoreKeyboardInput() && GameInput.JustReleased(BindingType.SHOW_ALL_SPLIT_JOINT_NUMBERS))
		{
			DisableAllJointSelectors();
			m_BridgeJointSelectorShowForUndo = null;
		}
		m_JointUnderMouse = null;
		if (BridgeSelectionSet.m_Joints.Count == 1)
		{
			BridgeJoint bridgeJoint = null;
			using (HashSet<BridgeJoint>.Enumerator enumerator = BridgeSelectionSet.m_Joints.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					bridgeJoint = enumerator.Current;
				}
			}
			if (bridgeJoint.m_IsSplit)
			{
				m_JointUnderMouse = bridgeJoint;
			}
		}
		if (m_JointUnderMouse == null)
		{
			BridgeJoint jointUnderMouse = GetJointUnderMouse();
			if ((bool)jointUnderMouse && jointUnderMouse == m_JointUnderMouse)
			{
				m_NumSecondsJointUnderMouse += Time.unscaledDeltaTime;
			}
			else
			{
				m_NumSecondsJointUnderMouse = 0f;
			}
			m_JointUnderMouse = jointUnderMouse;
		}
		if (JointIsValidForShowingJointSelectors(m_JointUnderMouse) && m_NumSecondsJointUnderMouse >= m_DebugSplitJointHoverTime)
		{
			ShowJointSelectorsOnlyForJoint(m_JointUnderMouse);
			MakeCircle(new Vector3(m_JointUnderMouse.transform.position.x, m_JointUnderMouse.transform.position.y, -3f), CIRCLE_RADIUS);
			m_JointForCircle = m_JointUnderMouse;
		}
		else
		{
			if ((bool)m_JointForCircle && Vector2.Distance(Utils.GetWorldPointFromScreenPos(GameInput.GetMousePosition()), m_JointForCircle.transform.position) > CIRCLE_RADIUS)
			{
				if (Mathf.Approximately(m_PersistUntilTimer, float.MaxValue))
				{
					m_PersistUntilTimer = m_DebugSplitJointPersistTime;
				}
				else
				{
					m_PersistUntilTimer -= Time.unscaledDeltaTime;
				}
				if ((bool)m_JointForCircle && m_PersistUntilTimer < 0f)
				{
					m_JointForCircle = null;
				}
			}
			if (!JointIsValidForShowingJointSelectors(m_JointForCircle))
			{
				if (m_VectorLineCircle != null)
				{
					m_VectorLineCircle.active = false;
				}
				DisableAllJointSelectors();
			}
		}
		if (!m_DebugSplitJointHoverCircle && m_VectorLineCircle != null)
		{
			m_VectorLineCircle.active = false;
		}
		if ((bool)m_BridgeJointSelectorShowForUndo)
		{
			m_ShowBridgeJointSelectorForUndoTimer -= Time.unscaledDeltaTime;
			if (m_ShowBridgeJointSelectorForUndoTimer < 0f)
			{
				m_BridgeJointSelectorShowForUndo.gameObject.SetActive(value: false);
				m_BridgeJointSelectorShowForUndo = null;
			}
			else
			{
				m_BridgeJointSelectorShowForUndo.gameObject.SetActive(value: true);
			}
		}
	}

	public static void ShowBridgeJointSelectorForUndo(BridgeJointSelector selector)
	{
		m_BridgeJointSelectorShowForUndo = selector;
		m_ShowBridgeJointSelectorForUndoTimer = SHOW_BRIDGE_JOINT_SELECTOR_FOR_UNDO_SECONDS;
	}

	public static void CancelCircle()
	{
		if (m_VectorLineCircle != null)
		{
			m_VectorLineCircle.active = false;
		}
	}

	public static void DisableAllJointSelectors()
	{
		foreach (BridgeJointSelector selector in m_Selectors)
		{
			selector.gameObject.SetActive(value: false);
		}
	}

	public static void EnableAllJointSelectors()
	{
		foreach (BridgeJointSelector selector in m_Selectors)
		{
			BridgeJoint associatedJoint = selector.GetAssociatedJoint();
			if ((bool)associatedJoint && associatedJoint.m_IsSplit && associatedJoint.gameObject.activeInHierarchy)
			{
				bool activeInHierarchy = selector.gameObject.activeInHierarchy;
				selector.gameObject.SetActive(value: true);
				if (!activeInHierarchy)
				{
					selector.transform.rotation = Quaternion.identity;
					selector.ResolveOverlap();
				}
			}
		}
	}

	private static bool JointIsValidForShowingJointSelectors(BridgeJoint joint)
	{
		if (!joint || !joint.m_IsSplit || !joint.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (joint == BridgeJointPlacement.m_SelectedJoint)
		{
			return false;
		}
		if (joint == BridgeJointMovement.m_SelectedJoint)
		{
			return false;
		}
		return true;
	}

	private static void ShowJointSelectorsOnlyForJoint(BridgeJoint joint)
	{
		foreach (BridgeJointSelector selector in m_Selectors)
		{
			BridgeJoint associatedJoint = selector.GetAssociatedJoint();
			bool activeInHierarchy = selector.gameObject.activeInHierarchy;
			selector.gameObject.SetActive(associatedJoint == joint);
			if (!activeInHierarchy)
			{
				selector.transform.rotation = Quaternion.identity;
				selector.ResolveOverlap();
			}
		}
		m_PersistUntilTimer = float.MaxValue;
	}

	public static bool JointSelectorIsUnderMouse()
	{
		return Utils.GetClosestRaycastHit(GameInput.GetMousePosition(), Utils.JOINT_SELECTOR_LAYER_MASK) != null;
	}

	public static bool CycleUnderMouse(Vector3 screenPos, bool forward)
	{
		BridgeJointSelector bridgeJointSelector = null;
		RaycastHit[] array = Physics.RaycastAll(Cameras.MainCamera().ScreenPointToRay(screenPos), float.MaxValue, Utils.JOINT_SELECTOR_LAYER_MASK);
		int num = 0;
		BridgeJointSelector bridgeJointSelector2 = null;
		RaycastHit[] array2 = array;
		foreach (RaycastHit raycastHit in array2)
		{
			bridgeJointSelector = raycastHit.transform.GetComponent<BridgeJointSelector>();
			if ((bool)bridgeJointSelector && !bridgeJointSelector.Ducked())
			{
				num++;
				bridgeJointSelector2 = bridgeJointSelector;
			}
		}
		if (num == 1 && (bool)bridgeJointSelector2)
		{
			bridgeJointSelector2.Cycle(forward);
			bridgeJointSelector2.DrawOnTop();
			return true;
		}
		Collider closestRaycastHit = Utils.GetClosestRaycastHit(screenPos, Utils.JOINT_SELECTOR_LAYER_MASK);
		if ((bool)closestRaycastHit)
		{
			bridgeJointSelector = closestRaycastHit.transform.GetComponent<BridgeJointSelector>();
			if ((bool)bridgeJointSelector)
			{
				bridgeJointSelector.Cycle(forward);
				bridgeJointSelector.DrawOnTop();
				return true;
			}
		}
		return false;
	}

	public static SplitJointNumber SplitJointNumberUnderMouse(Vector2 screenPos)
	{
		if (Physics2D.GetRayIntersectionNonAlloc(Cameras.MainCamera().ScreenPointToRay(GameInput.GetMousePosition()), m_RaycastHitsBuffer, float.PositiveInfinity, Utils.SPLIT_JOINT_NUMBER_LAYER_MASK) > 0)
		{
			RaycastHit2D raycastHit2D = m_RaycastHitsBuffer[0];
			if (raycastHit2D.collider != null)
			{
				return raycastHit2D.collider.GetComponent<SplitJointNumber>();
			}
		}
		return null;
	}

	public static bool SelectorOverlapsOtherSelectors(BridgeJointSelector selectorCompare, float threshold)
	{
		foreach (BridgeJointSelector selector in m_Selectors)
		{
			if (selector.gameObject.activeInHierarchy && selector.m_Edge != selectorCompare.m_Edge && Vector2.Distance(selector.transform.position, selectorCompare.transform.position) < threshold)
			{
				return true;
			}
		}
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint != selectorCompare.m_Edge.m_JointA && joint != selectorCompare.m_Edge.m_JointB && Vector2.Distance(joint.transform.position, selectorCompare.transform.position) < threshold)
			{
				return true;
			}
		}
		return false;
	}

	public static void RefreshVisibility()
	{
		foreach (BridgeJointSelector selector in m_Selectors)
		{
			selector.RefreshVisibility();
		}
	}

	private static void UpdateHighlightState()
	{
		foreach (BridgeJointSelector selector in m_Selectors)
		{
			selector.UpdateHighlightState();
		}
	}

	private static BridgeJointSelector GetMovingSelector()
	{
		if (!BridgeJointMovement.m_SelectedJoint)
		{
			return null;
		}
		foreach (BridgeJointSelector selector in m_Selectors)
		{
			if (selector.m_Edge.m_JointA == BridgeJointMovement.m_SelectedJoint || selector.m_Edge.m_JointB == BridgeJointMovement.m_SelectedJoint)
			{
				return selector;
			}
		}
		return null;
	}

	private static BridgeJoint GetJointUnderMouse()
	{
		if (Physics.Raycast(Cameras.MainCamera().ScreenPointToRay(GameInput.GetMousePosition()), out var hitInfo, float.MaxValue, Utils.JOINT_HOTSPOT_LAYER_MASK))
		{
			return hitInfo.transform.parent.GetComponent<BridgeJoint>();
		}
		return null;
	}

	private static void MakeCircle(Vector3 origin, float radius)
	{
		if (m_VectorLineCircle == null)
		{
			m_VectorLineCircle = new VectorLine("JointNumbersCircle", new List<Vector3>(NUM_CIRCLE_SEGMENTS + 1), null, 2f, LineType.Continuous);
			m_VectorLineCircle.layer = Utils.FOREGROUND_LAYER;
			m_VectorLineCircle.color = GameUI.GroupSelectionBoxOutlineColor();
			m_VectorLineCircle.Draw3DAuto();
		}
		m_VectorLineCircle.MakeCircle(origin, radius, NUM_CIRCLE_SEGMENTS);
		m_VectorLineCircle.active = true;
	}
}
