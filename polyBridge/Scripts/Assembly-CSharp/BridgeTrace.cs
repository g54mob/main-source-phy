using System.Collections.Generic;
using UnityEngine;

public class BridgeTrace
{
	public static ArcShape m_Shape;

	public static ArcTracer m_ArcTracer;

	public static bool m_SnapToGrid;

	public static bool m_TracingFollowsMouse;

	public static bool m_JustFilled;

	private static bool m_TracingActive;

	private static bool m_TracingLocked;

	private static BridgeJoint m_StartJoint;

	private static float PLACEMENT_Z = -10f;

	private static ParticleSystem m_ErrorParticleSystem;

	private static bool m_ProcessClickOnNextButtonUp;

	private static Vector2 m_MousePosWhenButtonDown;

	private static Vector3 m_LastMousePos;

	public static List<BridgeJoint> m_FillBuffer = new List<BridgeJoint>();

	private static float m_NextFillTimer;

	private static bool m_PlayFillErrorFX;

	private static float FILL_STEP_SECONDS = 0.1f;

	private static TraceFailureReason m_FailureReason;

	private static float FAILURE_REASON_DISPLAY_SECONDS = 3f;

	private static BridgeMaterialType m_FillMaterialType;

	public static void Init(ArcShape shape, bool snapToGrid)
	{
		m_ArcTracer = InstantiateArcTracer();
		m_ErrorParticleSystem = InstantiateErrorFX();
		m_TracingActive = false;
		m_ProcessClickOnNextButtonUp = false;
		m_Shape = shape;
		m_ArcTracer.SetShapeSilent(shape);
		m_SnapToGrid = snapToGrid;
	}

	public static void UpdateManual()
	{
		UpdateFill();
		if (m_TracingFollowsMouse && (!BridgeJointPlacement.m_SelectedJoint || !m_TracingActive))
		{
			ClearTraceLine();
			return;
		}
		if (m_ArcTracer.IsDraggingHandles())
		{
			m_JustFilled = false;
		}
		if (m_TracingFollowsMouse && BridgeJointPlacement.m_SelectedJoint != m_StartJoint)
		{
			ProcessButtonDown();
			return;
		}
		if (m_TracingLocked && !m_TracingActive)
		{
			if (!m_ArcTracer.HandlesVisible() && !ShouldSuppressHandles())
			{
				m_ArcTracer.ShowHandles();
			}
			if (m_TracingFollowsMouse || m_ArcTracer.IsDraggingHandles())
			{
				m_ArcTracer.SetArcLengthInfoBoxVisibility(ActivePanels.None());
			}
			else
			{
				m_ArcTracer.SetArcLengthInfoBoxVisibility(value: false);
			}
			if (!GameStateBuild.m_CameraInTransition)
			{
				ShowShadowTrace();
			}
		}
		if (m_TracingLocked)
		{
			if (!m_ArcTracer.HandlesVisible() && !ShouldSuppressHandles())
			{
				m_ArcTracer.ShowHandles();
			}
			if (m_ArcTracer.HandlesVisible() && ShouldSuppressHandles())
			{
				m_ArcTracer.HideHandles();
			}
			m_ArcTracer.SetArcLengthInfoBoxVisibility(ActivePanels.None() && m_ArcTracer.HandlesVisible());
		}
		if (!m_TracingActive)
		{
			return;
		}
		if (m_TracingLocked)
		{
			ShowShadowTrace();
		}
		if (!m_TracingFollowsMouse && (bool)BridgeJointPlacement.m_SelectedJoint)
		{
			Vector3 vector = new Vector3(BridgeJointPlacement.m_SelectedJoint.transform.position.x, BridgeJointPlacement.m_SelectedJoint.transform.position.y, PLACEMENT_Z);
			BridgeTraceShadow.Clear();
			m_ArcTracer.SetShapeSilent(m_Shape);
			m_ArcTracer.StartDrawingFrom(vector);
			m_StartJoint = BridgeJointPlacement.m_SelectedJoint;
			m_TracingFollowsMouse = true;
			m_JustFilled = false;
			m_TracingLocked = false;
			m_LastMousePos = Vector3.zero;
			GameUI.m_Instance.m_TraceTool.m_FillSlider.value = 100f;
			DrawTraceToMousePosition();
		}
		if (m_TracingFollowsMouse)
		{
			if (m_ArcTracer.HandlesVisible())
			{
				m_ArcTracer.HideHandles();
			}
			if (GameInput.GetMousePosition() != m_LastMousePos)
			{
				DrawTraceToMousePosition();
			}
			m_LastMousePos = GameInput.GetMousePosition();
			m_ArcTracer.SetArcLengthInfoBoxVisibility(ActivePanels.None());
		}
	}

	private static bool ShouldSuppressHandles()
	{
		if (!IsTracingActive())
		{
			return true;
		}
		if (BridgeJointPlacement.IsDrawing() || (bool)BridgeJointMovement.m_SelectedJoint || BridgePillarMovement.IsMovingSelectionSet())
		{
			return true;
		}
		if (GameToolMode.GetMode() == GameToolModeType.MOVE || GameToolMode.GetMode() == GameToolModeType.ERASE)
		{
			return true;
		}
		if (ClipboardManager.ReadyToPaste() || BridgePillarPlacement.InPlacementMode())
		{
			return true;
		}
		if ((bool)Pistons.m_SliderFollowingMouse || (bool)BridgeSprings.m_SliderFollowingMouse)
		{
			return true;
		}
		return false;
	}

	private static void DrawTraceToMousePosition()
	{
		Vector3 vector = GameGrid.SnapPosToGrid(Cameras.MainCamera().ScreenToWorldPoint(GameInput.GetMousePosition()));
		m_ArcTracer.ContinueDrawingTo(vector);
	}

	public static void ProcessSoftButtonDown()
	{
		if (m_TracingFollowsMouse)
		{
			m_ProcessClickOnNextButtonUp = true;
			m_MousePosWhenButtonDown = GameInput.GetMousePosition();
		}
	}

	public static void ProcessButtonUp()
	{
		if (m_TracingFollowsMouse && m_ProcessClickOnNextButtonUp)
		{
			float num = Mathf.Abs(GameInput.GetMousePosition().x - m_MousePosWhenButtonDown.x);
			float num2 = Mathf.Abs(GameInput.GetMousePosition().y - m_MousePosWhenButtonDown.y);
			if (num < 1f && num2 < 1f)
			{
				ProcessButtonDown();
			}
		}
		m_ProcessClickOnNextButtonUp = false;
	}

	public static void ProcessButtonDown()
	{
		if (!m_TracingFollowsMouse)
		{
			return;
		}
		if (m_ArcTracer.Finish())
		{
			BridgeJointPlacement.CancelSelection();
			if (m_Shape == ArcShape.FLAT && m_ArcTracer.GetArcDistance() >= 1.499f)
			{
				m_ArcTracer.RepositionHandles();
			}
			m_ArcTracer.ShowHandles();
			m_TracingFollowsMouse = false;
			m_TracingLocked = true;
			InterfaceAudio.Play("ui_build_tracetool_shape_dragRelease");
		}
		else
		{
			m_ArcTracer.Clear();
			InterfaceAudio.PlayErrorBeep();
		}
	}

	public static void AttachToJoint(BridgeJoint joint)
	{
		if (m_TracingFollowsMouse && (bool)joint && !(joint == m_StartJoint))
		{
			Vector3 position = joint.transform.position;
			m_ArcTracer.ContinueDrawingTo(position);
			ProcessButtonDown();
		}
	}

	public static void SetShape(ArcShape shape)
	{
		m_Shape = shape;
		m_ArcTracer.SetShape(shape);
	}

	public static void ClearTraceLine()
	{
		if ((bool)m_ArcTracer)
		{
			m_ArcTracer.HideHandles();
			m_ArcTracer.SetArcLengthInfoBoxVisibility(value: false);
			m_ArcTracer.Clear();
			m_ArcTracer.LockTangents();
		}
		BridgeTraceShadow.Clear();
		m_TracingFollowsMouse = false;
		m_TracingLocked = false;
		m_JustFilled = false;
	}

	public static bool IsTracingActive()
	{
		return m_TracingActive;
	}

	public static bool IsTraceLinePlaced()
	{
		return m_TracingLocked;
	}

	public static bool TangentsLocked()
	{
		if ((bool)m_ArcTracer)
		{
			return m_ArcTracer.TangentsLocked();
		}
		return true;
	}

	public static bool TracingFollowsMouse()
	{
		return m_TracingFollowsMouse;
	}

	public static void TurnOnTracing()
	{
		m_TracingActive = true;
	}

	public static void TurnOffTracing()
	{
		m_TracingActive = false;
		BridgeTraceShadow.Clear();
		if ((bool)m_ArcTracer)
		{
			m_ArcTracer.HideHandles();
			m_ArcTracer.SetArcLengthInfoBoxVisibility(value: false);
		}
	}

	public static void OnLayoutLoaded()
	{
		if ((bool)m_ArcTracer)
		{
			m_ArcTracer.OnLayoutLoaded();
		}
	}

	public static void Hide(bool hide)
	{
		if ((bool)m_ArcTracer)
		{
			m_ArcTracer.Hide(hide);
		}
	}

	public static bool IsDraggingHandles()
	{
		if (IsVisible() && (bool)m_ArcTracer)
		{
			return m_ArcTracer.IsDraggingHandles();
		}
		return false;
	}

	public static void ClearDraggingHandles()
	{
		if ((bool)m_ArcTracer)
		{
			m_ArcTracer.ResetDraggingHandles();
		}
	}

	public static bool IsVisible()
	{
		if ((bool)m_ArcTracer && m_ArcTracer.gameObject.activeInHierarchy)
		{
			return m_ArcTracer.IsTracerVisible();
		}
		return false;
	}

	public static bool IsLongEnoughToPlace()
	{
		return m_ArcTracer.GetArcDistance() >= m_ArcTracer.MIN_ARC_DISTANCE;
	}

	private static void DisablePlacementUI()
	{
		if ((bool)m_ArcTracer && m_ArcTracer.IsTracerVisible())
		{
			m_ArcTracer.Clear();
		}
	}

	private static void UpdatePlacementLine(Vector3 lineStartPos, Vector3 lineEndPos)
	{
		if ((bool)m_ArcTracer)
		{
			m_ArcTracer.ContinueDrawingTo(lineEndPos);
		}
	}

	private static Vector3 CalculatePlacementLineEndPos(Vector3 lineStartPos)
	{
		Vector3 mousePosition = GameInput.GetMousePosition();
		Vector3 result = Cameras.MainCamera().ScreenToWorldPoint(mousePosition);
		result.z = lineStartPos.z;
		return result;
	}

	private static ArcTracer InstantiateArcTracer()
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_ArcTracer, new Vector3(0f, 0f, PLACEMENT_Z), Quaternion.identity);
		if (!gameObject)
		{
			return null;
		}
		Object.DontDestroyOnLoad(gameObject);
		return gameObject.GetComponent<ArcTracer>();
	}

	private static ParticleSystem InstantiateErrorFX()
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_FillError);
		Object.DontDestroyOnLoad(gameObject);
		ParticleSystem component = gameObject.GetComponent<ParticleSystem>();
		ParticleSystem.MainModule main = component.main;
		main.useUnscaledTime = true;
		return component;
	}

	private static void StopErrorFX()
	{
		if ((bool)m_ErrorParticleSystem)
		{
			m_ErrorParticleSystem.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmittingAndClear);
		}
	}

	private static void ShowShadowTrace()
	{
		BridgeTraceShadow.Clear();
		BridgeTraceShadow.Show(CalculateFillPositions(GameUI.m_Instance.m_BuildToolBar.m_TraceToolPanel.GetFillSegmentLength(), playErrorFX: false), Bridge.m_BuildMaterialType);
	}

	public static bool IsFilling()
	{
		return m_FillBuffer.Count >= 2;
	}

	public static void CancelFill()
	{
		m_FillBuffer.Clear();
	}

	public static void CompleteFillingInstantly()
	{
		m_PlayFillErrorFX = false;
		bool flag = false;
		while (m_FillBuffer.Count >= 2 && FillStepSilent())
		{
			flag = true;
		}
		if (flag)
		{
			BridgeEdges.UpdateManual();
		}
		m_FillBuffer.Clear();
	}

	public static void UpdateFill()
	{
		if (Time.realtimeSinceStartup < m_NextFillTimer)
		{
			return;
		}
		if (!FillStepSilent())
		{
			m_FillBuffer.Clear();
			return;
		}
		if (m_FillBuffer.Count >= 2)
		{
			BridgeAudio.PlayCreateEdge((m_FillMaterialType == BridgeMaterialType.INVALID) ? Bridge.m_BuildMaterialType : m_FillMaterialType);
		}
		m_NextFillTimer = Time.realtimeSinceStartup + FILL_STEP_SECONDS;
	}

	private static bool FillStepSilent()
	{
		if (!IsFilling())
		{
			if (m_PlayFillErrorFX)
			{
				InterfaceAudio.PlayErrorBeep();
				m_ErrorParticleSystem.Play(withChildren: true);
				DisplayMessageForFailure(m_FailureReason);
				m_PlayFillErrorFX = false;
			}
			return false;
		}
		if (m_FillBuffer[0].gameObject != null)
		{
			m_FillBuffer[0].gameObject.SetActive(value: true);
		}
		if (m_FillBuffer[1].gameObject != null)
		{
			m_FillBuffer[1].gameObject.SetActive(value: true);
		}
		m_FillBuffer.RemoveAt(0);
		return true;
	}

	public static bool Fill(float maxEdgeLength)
	{
		m_FillBuffer.Clear();
		StopErrorFX();
		m_NextFillTimer = Time.realtimeSinceStartup + FILL_STEP_SECONDS;
		m_PlayFillErrorFX = false;
		List<Vector3> list = CalculateFillPositions(maxEdgeLength, playErrorFX: true);
		if (list.Count < 2)
		{
			m_FillBuffer.Clear();
			return false;
		}
		FillUsingPositions(list);
		m_JustFilled = true;
		return true;
	}

	public static List<Vector3> CalculateFillPositions(float maxEdgeLength, bool playErrorFX)
	{
		List<Vector3> list = new List<Vector3>();
		float num = Utils.ApproximateFloat(m_ArcTracer.arcTracerSpline.Length);
		Mathf.CeilToInt(num / maxEdgeLength);
		int num2 = Mathf.CeilToInt(num / maxEdgeLength);
		int num3 = ((Budget.GetMaterialLeft(Bridge.m_BuildMaterialType) == Budget.UNLIMITED_MATERIAL_BUDGET) ? num2 : Budget.GetMaterialLeft(Bridge.m_BuildMaterialType));
		if (num3 == 0 && playErrorFX)
		{
			Vector3 vector = m_ArcTracer.arcTracerSpline.InterpolateByDistance(0f);
			PlayErrorAtEndOfTrace(new Vector3(vector.x, vector.y, 0f), TraceFailureReason.OUT_OF_MATERIAL);
			return list;
		}
		int num4 = Mathf.Min(num3, num2);
		float num5 = Mathf.Min(maxEdgeLength, m_ArcTracer.arcTracerSpline.Length / (float)num4);
		for (int i = 0; i <= num4; i++)
		{
			Vector3 vector2 = m_ArcTracer.arcTracerSpline.InterpolateByDistance((float)i * num5);
			list.Add(new Vector3(vector2.x, vector2.y, 0f));
		}
		if (num3 < num2 && list.Count > 0 && playErrorFX)
		{
			PlayErrorAtEndOfTrace(list[list.Count - 1], TraceFailureReason.OUT_OF_MATERIAL);
		}
		return list;
	}

	private static void FillUsingPositions(List<Vector3> positions)
	{
		BridgeJoint bridgeJoint = BridgeJoints.GetJointAtPoint(positions[0]);
		BridgeJoint jointAtPoint = BridgeJoints.GetJointAtPoint(positions[positions.Count - 1]);
		bool flag = false;
		m_FillMaterialType = Bridge.m_BuildMaterialType;
		if (!bridgeJoint && !BridgeJoints.CanCreateJointAtPosition(positions[0], positions[0], Bridge.m_BuildMaterialType))
		{
			m_PlayFillErrorFX = true;
			m_ErrorParticleSystem.transform.position = positions[0];
			m_FailureReason = TraceFailureReason.INVALID_LOCATION;
			return;
		}
		BridgeActions.StartRecording();
		if (!bridgeJoint)
		{
			bridgeJoint = BridgeJoints.CreateJoint(positions[0], Utils.GenerateUniqueId());
			if (bridgeJoint != null)
			{
				BridgeActions.Create(bridgeJoint);
				flag = true;
			}
		}
		if (!bridgeJoint)
		{
			m_FailureReason = TraceFailureReason.UNKNOWN;
			return;
		}
		AddJointToFillBuffer(bridgeJoint, disable: false);
		BridgeJoint bridgeJoint2 = bridgeJoint;
		int num = positions.Count - 1;
		for (int i = 1; i < positions.Count; i++)
		{
			BridgeJoint bridgeJoint3 = jointAtPoint;
			if (i != num || !bridgeJoint3)
			{
				if (!BridgeJoints.CanCreateJointAtPosition(positions[i], bridgeJoint2.transform.position, Bridge.m_BuildMaterialType) || PosOverlapsWithFillBufferJoints(positions[i], GameSettings.NodeDiameter()))
				{
					m_ErrorParticleSystem.transform.position = positions[i];
					m_PlayFillErrorFX = true;
					m_FailureReason = TraceFailureReason.INVALID_LOCATION;
					break;
				}
				bridgeJoint3 = BridgeJoints.CreateJoint(positions[i], Utils.GenerateUniqueId());
				if (bridgeJoint3 != null)
				{
					BridgeActions.Create(bridgeJoint3);
				}
			}
			if ((bool)BridgeEdges.GetEdgeFromJoints(bridgeJoint2, bridgeJoint3))
			{
				m_ErrorParticleSystem.transform.position = positions[i];
				m_PlayFillErrorFX = true;
				m_FailureReason = TraceFailureReason.INVALID_LOCATION;
				break;
			}
			if (!JointOutOfBounds(bridgeJoint3) && BridgeEdges.CanFormEdgeBetweenJoints(null, bridgeJoint2, bridgeJoint3, Bridge.m_BuildMaterialType) && BridgeJoints.JointsCanAddEdgeWithoutExceedingEdgeLimit(bridgeJoint2, bridgeJoint3))
			{
				BridgeEdge bridgeEdge = BridgeEdges.CreateEdgeWithPistonOrSpring(bridgeJoint2, bridgeJoint3, Bridge.m_BuildMaterialType);
				if ((bool)bridgeEdge)
				{
					BridgeActions.Create(bridgeEdge);
					Budget.AdjustBudgetForAddedEdge(bridgeEdge);
					AddJointToFillBuffer(bridgeJoint3, bridgeJoint3 != jointAtPoint);
				}
				bridgeJoint2 = bridgeJoint3;
				continue;
			}
			m_ErrorParticleSystem.transform.position = bridgeJoint3.transform.position;
			m_PlayFillErrorFX = true;
			float length = Vector3.Distance(bridgeJoint2.transform.position, bridgeJoint3.transform.position);
			if (JointOutOfBounds(bridgeJoint3))
			{
				m_FailureReason = TraceFailureReason.INVALID_LOCATION;
				WorldBounds.ShowBriefly();
			}
			else if (!Budget.CanAffordEdge(length, Bridge.m_BuildMaterialType))
			{
				m_FailureReason = TraceFailureReason.OUT_OF_CASH;
			}
			if (bridgeJoint3 != jointAtPoint)
			{
				bridgeJoint3.Destroy();
			}
			break;
		}
		if (flag && (bool)bridgeJoint && BridgeJoints.IsOrphanedJoint(bridgeJoint))
		{
			BridgeActions.CancelRecording();
			bridgeJoint.Destroy();
		}
		else
		{
			BridgeActions.FlushRecording();
		}
	}

	private static bool JointOutOfBounds(BridgeJoint joint)
	{
		return !WorldBounds.Contains(joint.transform.position);
	}

	private static void AddJointToFillBuffer(BridgeJoint joint, bool disable)
	{
		m_FillBuffer.Add(joint);
		joint.gameObject.SetActive(!disable);
	}

	private static void PlayErrorAtEndOfTrace(Vector3 pos, TraceFailureReason failureReason)
	{
		m_PlayFillErrorFX = true;
		m_ErrorParticleSystem.transform.position = pos;
		m_FailureReason = failureReason;
	}

	private static void DisplayMessageForFailure(TraceFailureReason failureReason)
	{
		switch (failureReason)
		{
		case TraceFailureReason.INVALID_LOCATION:
			GameUI.ShowMessage(ScreenMessageLocation.TOP_CENTER, Localize.Get("TRACE_INVALID_LOCATION"), FAILURE_REASON_DISPLAY_SECONDS);
			break;
		case TraceFailureReason.OUT_OF_CASH:
			GameUI.ShowMessage(ScreenMessageLocation.TOP_CENTER, Localize.Get("TRACE_OUT_OF_CASH"), FAILURE_REASON_DISPLAY_SECONDS);
			break;
		case TraceFailureReason.OUT_OF_MATERIAL:
			GameUI.ShowMessage(ScreenMessageLocation.TOP_CENTER, Localize.Get("TRACE_OUT_OF_MATERIAL"), FAILURE_REASON_DISPLAY_SECONDS);
			break;
		}
	}

	private static bool PosOverlapsWithFillBufferJoints(Vector3 pos, float threshold)
	{
		foreach (BridgeJoint item in m_FillBuffer)
		{
			if (Vector2.Distance(item.transform.position, pos) < threshold)
			{
				return true;
			}
		}
		return false;
	}
}
