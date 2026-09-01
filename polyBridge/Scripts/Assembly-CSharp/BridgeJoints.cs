using System.Collections.Generic;
using Poly;
using Poly.Collide;
using UnityEngine;

public class BridgeJoints
{
	public static readonly float MAX_EDGES_PER_JOINT = 32f;

	public static List<BridgeJoint> m_Joints = new List<BridgeJoint>();

	public static List<BridgeJointFlash> m_FlashingJoints = new List<BridgeJointFlash>();

	public static Dictionary<string, BridgeJoint> m_JointDictionary = new Dictionary<string, BridgeJoint>();

	private static GameObject m_JointsContainer;

	private static float BOUNDS_SCALE_FOR_ANCHOR_OVERLAP = 1.2f;

	private static HashSet<BridgeJoint> m_TempHashSet = new HashSet<BridgeJoint>();

	public static void UpdateManualOutsideSim()
	{
		if (Bridge.m_BuildMaterialType != BridgeMaterialType.INVALID)
		{
			float maxEdgeLength = BridgeMaterials.GetMaxEdgeLength(Bridge.m_BuildMaterialType);
			if ((bool)BridgeJointMovement.m_SelectedJoint)
			{
				HightlightJointsInRange(BridgeJointMovement.m_SelectedJoint.transform.position, maxEdgeLength);
			}
			else if ((bool)BridgeJointPlacement.m_SelectedJoint)
			{
				if (BridgeJointPlacement.AllowPlacement(BridgeJointPlacement.m_SelectedJoint, BridgeJointPlacement.GetPlacementPos()) == PlacementReturnValue.SUCCESS)
				{
					HightlightJointsInRange(BridgeJointPlacement.GetPlacementPos(), maxEdgeLength);
				}
			}
			else
			{
				SetDefaultHighlightColor();
			}
		}
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy)
			{
				joint.UpdateManualOutsideSim();
			}
		}
	}

	public static Dictionary<string, BridgeJoint> GetJointDictionary()
	{
		return m_JointDictionary;
	}

	public static void UpdateFlashingJoints()
	{
		for (int num = m_FlashingJoints.Count - 1; num >= 0; num--)
		{
			m_FlashingJoints[num].UpdateManual();
		}
	}

	public static void ForceStopFlashingOfJoints()
	{
		for (int num = m_FlashingJoints.Count - 1; num >= 0; num--)
		{
			m_FlashingJoints[num].StopFlashing();
		}
	}

	public static void DisableOutlines()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor)
			{
				joint.DisableOutline();
			}
		}
	}

	public static void UpdateOutlines()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor)
			{
				joint.UpdateOutline();
			}
		}
	}

	public static void DestroyAllAnchors()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.m_IsAnchor)
			{
				joint.Destroy();
			}
		}
	}

	public static void DestroyAllExceptAnchors()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (!joint.m_IsAnchor)
			{
				joint.Destroy();
			}
		}
	}

	public static void DestroyAllExceptLayoutAnchors()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (!joint.m_IsAnchor || BridgePillars.GetActiveOrInactiveBridgePillarWithAnchor(joint.m_Guid) != null)
			{
				joint.Destroy();
			}
		}
	}

	public static void DestroyAll()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			joint.Destroy();
		}
		m_Joints.Clear();
		m_JointDictionary.Clear();
	}

	public static List<BridgeJointProxy> SerializeAnchorsForSandboxLayout()
	{
		List<BridgeJointProxy> list = new List<BridgeJointProxy>();
		foreach (BridgeJoint joint in m_Joints)
		{
			if ((joint.gameObject.activeInHierarchy || (bool)joint.GetComponentInParent<CustomShapeAnchor>()) && joint.m_IsAnchor && !BridgePillars.IsBridgePillarAnchor(joint.m_Guid))
			{
				BridgeJointProxy bridgeJointProxy = new BridgeJointProxy(joint);
				bridgeJointProxy.m_IsSplit = false;
				list.Add(bridgeJointProxy);
			}
		}
		return list;
	}

	public static List<BridgeJointProxy> SerializeAnchorsForBridgeSave()
	{
		List<BridgeJointProxy> list = new List<BridgeJointProxy>();
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor)
			{
				BridgeJointProxy item = new BridgeJointProxy(joint);
				list.Add(item);
			}
		}
		return list;
	}

	public static List<BridgeJointProxy> SerializeNoAnchors()
	{
		List<BridgeJointProxy> list = new List<BridgeJointProxy>();
		foreach (BridgeJoint joint in m_Joints)
		{
			if ((joint.gameObject.activeInHierarchy || (bool)joint.GetComponentInParent<CustomShapeAnchor>()) && !joint.m_IsAnchor)
			{
				list.Add(new BridgeJointProxy(joint));
			}
		}
		return list;
	}

	public static void Deserialize(List<BridgeJointProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (BridgeJointProxy proxy in proxies)
		{
			CreateJointFromProxy(proxy);
		}
	}

	public static void DeserializeAnchors(List<BridgeJointProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (BridgeJointProxy proxy in proxies)
		{
			if (proxy.m_IsAnchor)
			{
				CreateJointFromProxy(proxy);
			}
		}
	}

	public static BridgeJoint CreateJointFromProxy(BridgeJointProxy proxy)
	{
		BridgeJoint bridgeJoint = null;
		bridgeJoint = ((!proxy.m_IsAnchor) ? CreateJoint(proxy.m_Pos, proxy.m_Guid) : CreateAnchor(proxy.m_Pos, proxy.m_Guid));
		if (!bridgeJoint)
		{
			return null;
		}
		if (proxy.m_IsSplit)
		{
			bridgeJoint.Split();
			HydraulicsController.AddSplitJointToAllPhasesAcceptingNewAdditions(bridgeJoint);
		}
		bridgeJoint.m_NoBuild = proxy.m_NoBuild;
		bridgeJoint.m_FX.SetActive(GameStateManager.GetState() != GameState.SIM);
		bridgeJoint.m_Cap.SetActive(GameStateManager.GetState() == GameState.SIM);
		return bridgeJoint;
	}

	public static BridgeJoint FindByGuid(string guid)
	{
		if (string.IsNullOrEmpty(guid))
		{
			return null;
		}
		if (!m_JointDictionary.ContainsKey(guid))
		{
			return null;
		}
		return m_JointDictionary[guid];
	}

	public static void RemoveFromDictionary(BridgeJoint joint)
	{
		if (joint.m_Guid != null && m_JointDictionary.ContainsKey(joint.m_Guid) && m_JointDictionary[joint.m_Guid] == joint)
		{
			m_JointDictionary.Remove(joint.m_Guid);
		}
	}

	public static HashSet<BridgeJoint> CollectJointsInRect(Rect rect)
	{
		HashSet<BridgeJoint> hashSet = new HashSet<BridgeJoint>();
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && Utils.RectOverlapsCircle2D(rect.center - new Vector2(rect.width / 2f, rect.height / 2f), rect.width, rect.height, joint.transform.position, GameSettings.NodeRadius()))
			{
				hashSet.Add(joint);
			}
		}
		return hashSet;
	}

	public static void DestroyOrphanedJoints()
	{
		foreach (BridgeJoint orphanedJoint in GetOrphanedJoints())
		{
			orphanedJoint.Destroy();
		}
	}

	public static bool SphereOverlapsJoint(Vector3 pos, float radius)
	{
		int jOINT_LAYER_MASK = Utils.JOINT_LAYER_MASK;
		if (Physics.OverlapSphere(pos, radius, jOINT_LAYER_MASK).Length == 0)
		{
			return true;
		}
		return false;
	}

	public static bool CanCreateJointAtPosition(Vector3 dest, Vector3 src, BridgeMaterialType materialType)
	{
		if (BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions)
		{
			return true;
		}
		if (JointOverlapsPosition(dest, GameSettings.NodeDiameter()))
		{
			return false;
		}
		if (NodeLocationOverlapsBlockingPolygonShape(dest))
		{
			return false;
		}
		if (!BuildZones.ContainsEdge(dest, src))
		{
			return false;
		}
		if (dest != src)
		{
			BridgeMaterial bridgeMaterial = BridgeMaterials.GetBridgeMaterial(materialType);
			if (bridgeMaterial != null && BridgeEdges.EdgeLocationOverlapsBlockingPolygonShape(src, dest, materialType, bridgeMaterial.m_EdgeMaterial.collisionRadius))
			{
				return false;
			}
		}
		return true;
	}

	public static BridgeJoint GetJointAtPoint(Vector2 point)
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && Utils.ApproximatelyEquals(point, Utils.V3toV2(joint.transform.position)))
			{
				return joint;
			}
		}
		return null;
	}

	public static BridgeJoint FindClosestJoint(Vector2 pos)
	{
		BridgeJoint result = null;
		float num = float.MaxValue;
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy)
			{
				float num2 = Vector2.Distance(pos, joint.transform.position);
				if (num2 < num)
				{
					result = joint;
					num = num2;
				}
			}
		}
		return result;
	}

	public static BridgeJoint FindClosestJointEx(Vector2 pos, BridgeJoint excludeJoint)
	{
		BridgeJoint result = null;
		float num = float.MaxValue;
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint != excludeJoint)
			{
				float num2 = Vector2.Distance(pos, joint.transform.position);
				if (num2 < num)
				{
					result = joint;
					num = num2;
				}
			}
		}
		return result;
	}

	public static bool JointOverlapsPosition(Vector3 pos, float threshold)
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && Vector2.Distance(joint.transform.position, pos) < threshold)
			{
				return true;
			}
		}
		return false;
	}

	public static bool AnchorOverlapsPosition(Vector3 pos, BridgeJoint exclude, float threshold)
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor && joint != exclude && Vector2.Distance(joint.transform.position, pos) < threshold)
			{
				return true;
			}
		}
		return false;
	}

	public static bool JointOverlapsOtherJoints(BridgeJoint jointCompare, float mergeThreshold, float threshold)
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint != jointCompare)
			{
				float num = Vector2.Distance(joint.transform.position, jointCompare.transform.position);
				if (num >= mergeThreshold && num < threshold)
				{
					return true;
				}
			}
		}
		return false;
	}

	public static bool SphereOverlapsOtherJoints(Vector3 pos, float mergeThreshold, float threshold)
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy)
			{
				float num = Vector2.Distance(joint.transform.position, pos);
				if (num >= mergeThreshold && num < threshold)
				{
					return true;
				}
			}
		}
		return false;
	}

	public static bool JointOverlapsOtherJoints_Optimized(BridgeJoint jointCompare, float mergeThreshold, float threshold, Vector2[] cachedPositions)
	{
		float num = mergeThreshold * mergeThreshold;
		float num2 = threshold * threshold;
		Vector2 vector = jointCompare.transform.position;
		int count = m_Joints.Count;
		for (int i = 0; i < count; i++)
		{
			BridgeJoint bridgeJoint = m_Joints[i];
			ref Vector2 reference = ref cachedPositions[i];
			float num3 = reference.x - vector.x;
			float num4 = reference.y - vector.y;
			float num5 = num3 * num3 + num4 * num4;
			if (num5 < num2 && num5 >= num && (object)bridgeJoint != jointCompare && bridgeJoint.isActiveAndEnabled)
			{
				return true;
			}
		}
		return false;
	}

	public static bool JointOverlapsOtherAnchors(BridgeJoint jointCompare, float threshold)
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor && joint != jointCompare && Vector2.Distance(joint.transform.position, jointCompare.transform.position) < threshold)
			{
				return true;
			}
		}
		return false;
	}

	public static bool IsOrphanedJoint(BridgeJoint joint)
	{
		List<BridgeEdge> edgesConnectedToJoint = BridgeEdges.GetEdgesConnectedToJoint(joint);
		if (joint.gameObject.activeInHierarchy && edgesConnectedToJoint.Count == 0)
		{
			return true;
		}
		return false;
	}

	public static List<BridgeJoint> GetOrphanedJoints()
	{
		List<BridgeJoint> list = new List<BridgeJoint>();
		foreach (BridgeJoint joint in m_Joints)
		{
			if (!joint.m_IsAnchor && IsOrphanedJoint(joint))
			{
				list.Add(joint);
			}
		}
		return list;
	}

	public static void DeleteOrphanedJoints()
	{
		foreach (BridgeJoint orphanedJoint in GetOrphanedJoints())
		{
			orphanedJoint.gameObject.SetActive(value: false);
			if (BridgeActions.IsRecording())
			{
				BridgeActions.Delete(orphanedJoint);
			}
		}
	}

	public static BridgeJoint CreateAnchor(Vector3 pos, string guid)
	{
		BridgeJoint bridgeJoint = CreateJoint(pos, guid);
		if (bridgeJoint != null)
		{
			bridgeJoint.MakeAnchor();
		}
		return bridgeJoint;
	}

	public static BridgeJoint CreateDebris(Vector3 pos)
	{
		BridgeJoint bridgeJoint = CreateJoint(pos, string.Empty);
		if (bridgeJoint != null)
		{
			bridgeJoint.m_FX.gameObject.SetActive(value: false);
			bridgeJoint.m_SnapToFX.gameObject.SetActive(value: false);
			bridgeJoint.m_Cap.gameObject.SetActive(value: false);
			bridgeJoint.name += " (Debris)";
		}
		return bridgeJoint;
	}

	public static BridgeJoint CreateJoint(Vector3 pos, string guid)
	{
		if (!string.IsNullOrEmpty(guid) && m_JointDictionary.ContainsKey(guid))
		{
			Debug.LogWarning("Trying to create joint with guid " + guid + " that already is registered");
			return null;
		}
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_Joint, pos, Quaternion.identity);
		if (!gameObject)
		{
			return null;
		}
		gameObject.transform.parent = GetJointsContainerTransform();
		BridgeJoint component = gameObject.GetComponent<BridgeJoint>();
		if (!component)
		{
			return null;
		}
		if (!string.IsNullOrEmpty(guid))
		{
			component.m_Guid = guid;
			m_JointDictionary.Add(guid, component);
		}
		component.m_BuildPos = pos;
		component.m_HoverFX.SetActive(value: false);
		component.m_SelectedInnerFX.SetActive(value: false);
		component.m_InSelectionSetFX.SetActive(value: false);
		component.m_Cap.SetActive(value: false);
		if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			component.MakeGreyScale();
		}
		else
		{
			component.MakeDefaultColor();
		}
		m_Joints.Add(component);
		return component;
	}

	public static void AddToSimulation()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy)
			{
				BridgePhysics.AddNode(joint);
			}
		}
	}

	public static void DestroyInactiveJoints()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (!joint.gameObject.activeInHierarchy)
			{
				joint.Destroy();
			}
		}
	}

	public static void ApplySimulationResults_AndCacheSmoothNodePos()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.isActiveAndEnabled)
			{
				Transform transform = joint.m_Transform;
				Vec2 vec = (joint.m_PhysicsNode.cachedSmoothPos = joint.m_PhysicsNode.smoothPos);
				transform.position = vec;
			}
		}
	}

	public static void HideAllUI()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy)
			{
				joint.m_FX.SetActive(value: false);
			}
		}
	}

	public static void HideSplitUI()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			joint.HideThreeWaySplitUI();
			joint.m_Split2.SetActive(value: false);
			joint.m_StaticIconRight.gameObject.SetActive(value: true);
			joint.m_StaticIconRightSplit.gameObject.SetActive(value: false);
		}
	}

	public static void ShowThreeWaySplitUI()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && (joint.IsThreeWaySplitJoint() || joint.TwoWayShouldFunctionAsThreeWay()))
			{
				joint.ShowThreeWaySplitUI();
			}
		}
	}

	public static void HideThreeWaySplitUI()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			joint.HideThreeWaySplitUI();
		}
	}

	public static void HideHydraulicControllerTwoWaySplitUI()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			joint.HideHydraulicControllerTwoWaySplitUI();
		}
	}

	public static void HideHoverUI()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			joint.m_HoverFX.gameObject.SetActive(value: false);
		}
	}

	public static void UnHideAllUI()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy)
			{
				joint.m_FX.SetActive(value: true);
			}
		}
	}

	public static void UnHideSplitUI()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.m_IsSplit)
			{
				joint.m_Split2.SetActive(value: true);
				joint.m_StaticIconRight.gameObject.SetActive(value: false);
				joint.m_StaticIconRightSplit.gameObject.SetActive(value: true);
			}
		}
	}

	public static void MakeGreyScale()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			joint.MakeGreyScale();
		}
	}

	public static void MakeDefaultColor()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (GameStateManager.GetState() == GameState.SANDBOX)
			{
				joint.MakeGreyScale();
			}
			else
			{
				joint.MakeDefaultColor();
			}
		}
	}

	public static void SetDefaultHighlightColor()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsHighlighted)
			{
				joint.SetOutlineColor(joint.m_IsAnchor ? GameUI.StaticJointOutlineColor() : GameUI.JointOutlineColor());
				joint.SetColor(joint.m_IsAnchor ? joint.GetAnchorColor() : joint.GetJointColor(), joint.GetSplitJointColor());
				joint.m_IsHighlighted = false;
			}
		}
	}

	public static void HightlightJointsInRange(Vector3 pos, float range)
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (!joint.gameObject.activeInHierarchy || joint == BridgeJointPlacement.m_SelectedJoint)
			{
				continue;
			}
			if (BridgeEdges.IsValidEdgeLength(Vector2.Distance(pos, joint.transform.position), GameSettings.NodeDiameter(), range) && !joint.m_NoBuild)
			{
				if (!joint.m_IsHighlighted)
				{
					joint.SetOutlineColor(joint.m_IsAnchor ? GameUI.StaticJointOutlineHighlightColor() : GameUI.JointOutlineHightlightColor());
					joint.SetColor(joint.GetJointHighlightColor(), joint.GetSplitJointHighlightColor());
					joint.m_IsHighlighted = true;
				}
			}
			else if (joint.m_IsHighlighted)
			{
				joint.SetOutlineColor(joint.m_IsAnchor ? GameUI.StaticJointOutlineColor() : GameUI.JointOutlineColor());
				joint.SetColor(joint.m_IsAnchor ? joint.GetAnchorColor() : joint.GetJointColor(), joint.GetSplitJointColor());
				joint.m_IsHighlighted = false;
			}
		}
	}

	public static bool AtSameLocation(BridgeJoint A, BridgeJoint B)
	{
		if (Mathf.Approximately(A.transform.position.x, B.transform.position.x))
		{
			return Mathf.Approximately(A.transform.position.y, B.transform.position.y);
		}
		return false;
	}

	public static void RefreshThreeWaySplitJointNumberVisibility()
	{
		bool threeWaySplitJointNumberVisibility = GameStateManager.GetState() == GameState.BUILD && Cameras.GetOrthographicSize() < GameSettings.MaxOrthographicSizeToShowSplitJointNumbers() && GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy;
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.m_IsSplit)
			{
				joint.SetThreeWaySplitJointNumberVisibility(threeWaySplitJointNumberVisibility);
			}
		}
	}

	public static void HideThreeWaySplitJointNumbers()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.m_IsSplit)
			{
				joint.SetThreeWaySplitJointNumberVisibility(visible: false);
			}
		}
	}

	public static void DestroyAnchors(List<BridgeJoint> jointsToDelete)
	{
		if (jointsToDelete.Count == 0)
		{
			return;
		}
		foreach (BridgeJoint item in jointsToDelete)
		{
			HydraulicsController.RemoveSplitJointFromAllPhases(item);
			item.Destroy();
		}
		BridgeEdges.UpdateManual();
		List<BridgeJoint> orphanedJoints = GetOrphanedJoints();
		foreach (BridgeJoint item2 in orphanedJoints)
		{
			item2.Destroy();
		}
		jointsToDelete.AddRange(orphanedJoints);
		foreach (BridgeJoint item3 in jointsToDelete)
		{
			BridgeEdges.DestroyEdgesConnectedToJoint(item3);
		}
		BridgeUndo.Reset();
		BridgeRedo.Reset();
	}

	public static int GetNumActiveAnchorJoints()
	{
		int num = 0;
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor)
			{
				num++;
			}
		}
		return num;
	}

	public static int GetNumActiveNonAnchorJoints()
	{
		int num = 0;
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && !joint.m_IsAnchor)
			{
				num++;
			}
		}
		return num;
	}

	public static int GetNumSplitJoints()
	{
		int num = 0;
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsSplit)
			{
				num++;
			}
		}
		return num;
	}

	public static int GetNumThreeWaySplitJoints()
	{
		int num = 0;
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.IsThreeWaySplitJoint())
			{
				num++;
			}
		}
		return num;
	}

	public static void UnSplitAllJoints()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsSplit)
			{
				joint.UnSplit();
			}
		}
	}

	public static List<BridgeJoint> GetSplitjoints()
	{
		List<BridgeJoint> list = new List<BridgeJoint>();
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsSplit)
			{
				list.Add(joint);
			}
		}
		return list;
	}

	public static bool NodeLocationOverlapsBlockingPolygonShape(Vector3 pos)
	{
		float radius = GameSettings.NodeRadius() - 0.001f;
		PolygonShape polygonShape = PolygonShape.FromCircle((Vec2)pos, radius);
		if (TerrainIslands.OverlapsPolygonShape(polygonShape))
		{
			return true;
		}
		if (Rocks.OverlapsPolygonShape(polygonShape))
		{
			return true;
		}
		if (FlyingObjects.OverlapsPolygonShape(polygonShape))
		{
			return true;
		}
		if (CustomShapes.OverlapsPolygonShapeBlockingNodes(polygonShape))
		{
			return true;
		}
		if ((bool)BridgePillars.OverlapsPolygonShape(polygonShape))
		{
			return true;
		}
		return false;
	}

	public static BridgeJoint NodeExistsAtPosition(Vector3 pos)
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && Mathf.Approximately(Vector2.Distance(joint.transform.position, pos), 0f))
			{
				return joint;
			}
		}
		return null;
	}

	public static void SetHydraulicControllerSortOrder()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			_ = joint.m_IsSplit;
		}
	}

	public static void SetDefaultSortOrder()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.m_IsSplit)
			{
				joint.SetSplitJointSortOrder(1);
			}
		}
	}

	public static void EnableAnchorJointCaps()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.m_IsAnchor)
			{
				joint.m_Cap.SetActive(value: true);
			}
		}
	}

	public static void EnableSplitAnchorJointCaps()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.m_IsAnchor && joint.m_IsSplit)
			{
				joint.m_Cap.SetActive(value: true);
			}
		}
	}

	public static void DisableJointCaps()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			joint.m_Cap.SetActive(value: false);
		}
	}

	public static void RefreshCaps()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			joint.RefreshCap();
		}
	}

	public static bool JointsCanAddEdgeWithoutExceedingEdgeLimit(BridgeJoint A, BridgeJoint B)
	{
		if (BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions)
		{
			return true;
		}
		if (!A.HasMaxEdges())
		{
			return !B.HasMaxEdges();
		}
		return false;
	}

	public static int DeleteInvalidAnchorEdges(BridgeJoint anchor)
	{
		int num = 0;
		foreach (BridgeEdge item in BridgeEdges.GetEdgesConnectedToJoint(anchor))
		{
			if (!item.IsValidLength())
			{
				item.ForceDisable();
				num++;
			}
		}
		if (num > 0)
		{
			BridgeEdges.UpdateManual();
			DeleteOrphanedJoints();
			BridgeUndo.Reset();
			BridgeRedo.Reset();
		}
		return num;
	}

	public static void MergeIntoAnchor(BridgeJoint anchor, BridgeJoint joint)
	{
		foreach (BridgeEdge item in BridgeEdges.GetEdgesConnectedToJoint(joint))
		{
			if (item.m_JointA.m_Guid == joint.m_Guid)
			{
				item.m_JointA = anchor;
				anchor.RegisterEdgeInCache(item);
				if ((bool)item.m_JointSelectorA)
				{
					item.m_JointSelectorA.RefreshNumber();
				}
				if (item.IsPiston())
				{
					item.RefreshPistonJointRefs();
				}
				item.UpdateTransform();
				joint.Destroy();
			}
			else if (item.m_JointB.m_Guid == joint.m_Guid)
			{
				item.m_JointB = anchor;
				anchor.RegisterEdgeInCache(item);
				if ((bool)item.m_JointSelectorB)
				{
					item.m_JointSelectorB.RefreshNumber();
				}
				if (item.IsPiston())
				{
					item.RefreshPistonJointRefs();
				}
				joint.Destroy();
				item.UpdateTransform();
			}
		}
	}

	public static float Mass()
	{
		float num = 0f;
		foreach (BridgeJoint joint in m_Joints)
		{
			if (!joint.m_IsAnchor && joint.gameObject.activeInHierarchy)
			{
				num += BridgePhysics.NODE_MASS_KG;
			}
		}
		return num;
	}

	public static float Mass(HashSet<BridgeJoint> joints)
	{
		float num = 0f;
		foreach (BridgeJoint joint in joints)
		{
			if (!joint.m_IsAnchor && joint.gameObject.activeInHierarchy)
			{
				num += BridgePhysics.NODE_MASS_KG;
			}
		}
		return num;
	}

	public static void SelectAll()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy)
			{
				BridgeSelectionSet.SelectJointAndConnectedEdges(joint);
			}
		}
	}

	public static void SetUnconnectedAnchorsYPos(float y)
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor && !BridgePillars.IsBridgePillarAnchor(joint.m_Guid))
			{
				joint.transform.position = new Vector3(joint.transform.position.x, y, joint.transform.position.z);
			}
		}
	}

	public static bool AnchorOverlapsAnchor(BridgeJoint anchor)
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor && joint != anchor)
			{
				float bOUNDS_SCALE_FOR_ANCHOR_OVERLAP = BOUNDS_SCALE_FOR_ANCHOR_OVERLAP;
				Bounds a = new Bounds(anchor.m_SandboxItem.m_Colliders[0].bounds.center, anchor.m_SandboxItem.m_Colliders[0].bounds.size * bOUNDS_SCALE_FOR_ANCHOR_OVERLAP);
				Bounds b = new Bounds(joint.m_SandboxItem.m_Colliders[0].bounds.center, joint.m_SandboxItem.m_Colliders[0].bounds.size * bOUNDS_SCALE_FOR_ANCHOR_OVERLAP);
				if (Utils.BoundsIntersect2D(a, b))
				{
					return true;
				}
			}
		}
		return false;
	}

	public static bool AnchorOverlapsBounds(Bounds bounds)
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor)
			{
				float bOUNDS_SCALE_FOR_ANCHOR_OVERLAP = BOUNDS_SCALE_FOR_ANCHOR_OVERLAP;
				Bounds a = new Bounds(bounds.center, bounds.size * bOUNDS_SCALE_FOR_ANCHOR_OVERLAP);
				Bounds b = new Bounds(joint.m_SandboxItem.m_Colliders[0].bounds.center, joint.m_SandboxItem.m_Colliders[0].bounds.size * bOUNDS_SCALE_FOR_ANCHOR_OVERLAP);
				if (Utils.BoundsIntersect2D(a, b))
				{
					return true;
				}
			}
		}
		return false;
	}

	public static void ResolveOverlappingAnchors(Vector3 resolveDir)
	{
		m_TempHashSet.Clear();
		foreach (SandboxItem item in SandboxSelectionSet.m_Items)
		{
			if (item.m_Type == SandboxItemType.ANCHOR)
			{
				BridgeJoint component = item.GetComponent<BridgeJoint>();
				if (!component.IsDynamicAnchor())
				{
					m_TempHashSet.Add(component);
				}
			}
		}
		ResolveOverlappingAnchors(m_TempHashSet, resolveDir);
		m_TempHashSet.Clear();
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor && !joint.IsDynamicAnchor())
			{
				m_TempHashSet.Add(joint);
			}
		}
		ResolveOverlappingAnchors(m_TempHashSet, resolveDir);
	}

	public static void ChangeAnchorsLayer(int oldLayer, int newLayer)
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor)
			{
				Utils.ReplaceLayerRecursively(joint.gameObject, oldLayer, newLayer);
			}
		}
	}

	public static void DisableAnchorsCollision(bool disable)
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor)
			{
				joint.m_Collider.enabled = !disable;
				joint.m_HotspotCollider.enabled = !disable;
			}
		}
	}

	public static void OverrideAnchorFX_Z(float z)
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor)
			{
				joint.m_RestoreZ = joint.m_FX.transform.position.z;
				joint.m_FX.transform.position = new Vector3(joint.m_FX.transform.position.x, joint.m_FX.transform.position.y, z);
				joint.m_FX.gameObject.SetActive(value: true);
			}
		}
	}

	public static void RestoreAnchorFX_Z()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor)
			{
				joint.m_FX.transform.position = new Vector3(joint.transform.position.x, joint.transform.position.y, joint.m_RestoreZ);
			}
		}
	}

	private static void ResolveOverlappingAnchors(HashSet<BridgeJoint> anchors, Vector3 resolveDir)
	{
		foreach (BridgeJoint anchor in anchors)
		{
			while (AnchorOverlapsAnchor(anchor))
			{
				anchor.transform.position += resolveDir * GameGrid.m_Spacing;
				anchor.m_SandboxItem.SetOutlineDirty(dirty: true);
				GameUI.m_Instance.m_SandboxEditAnchor.RefreshPosition(anchor);
			}
		}
	}

	private static Transform GetJointsContainerTransform()
	{
		if (!m_JointsContainer)
		{
			m_JointsContainer = new GameObject("CreatedJoints");
		}
		return m_JointsContainer.transform;
	}
}
