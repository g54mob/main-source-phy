using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Vectrosity;

public class CampaignTutorial
{
	public static bool m_Completed;

	public static bool m_ResumeWhenEnteringBuildMode;

	public static CampaignTutorialStage m_CurrentStage;

	private static string m_CurrentActiveButtonName;

	private static List<Button> m_AllButtons = new List<Button>();

	private static bool m_CancelPlacementOnLateUpdate = false;

	private static bool m_GridOnAtStart;

	private static bool m_AutoTriangulateEnabledAtStart;

	private static bool m_AutoDrawEnabledAtStart;

	private static CampaignTutorialType m_TutorialType;

	private static bool m_InProgress;

	private static TweenPosition m_MoveHydroSliderTween;

	private static Vector3 HYDRO_START_POS_1 = new Vector3(19f, 2.75f, 0f);

	private static Vector3 HYDRO_START_POS_2 = new Vector3(25f, 6.75f, 0f);

	private static readonly float LEFT_PISTON_X_THRESHOLD = 20f;

	private static readonly Vector3 SELECT_BOX_START = new Vector3(2.5f, 7.5f, 0f);

	private static readonly Vector3 SELECT_BOX_END = new Vector3(9.5f, 4f, 0f);

	private static VectorLine m_SelectionBox;

	private static TweenPosition m_SelectionBoxTween;

	public static void Init()
	{
	}

	public static void OnDisable()
	{
		ReenableButtons();
		HideTutorialArrows();
		DestroyWorldspaceElements();
	}

	public static void Start(CampaignTutorialType type)
	{
		if (type != CampaignTutorialType.None)
		{
			m_TutorialType = type;
			CreateWorldspaceElements();
			if (m_AllButtons.Count == 0)
			{
				CreateAllButtonsList();
			}
			ResetTutorial();
			BridgeSimSpeed.SetSimulationSpeedAbsolute(1f);
			m_GridOnAtStart = Profiles.m_ActiveProfile.m_GridEnabled;
			m_AutoTriangulateEnabledAtStart = Profiles.m_ActiveProfile.m_AutoTriangulateEnabled;
			m_AutoDrawEnabledAtStart = Profiles.m_ActiveProfile.m_AutoDrawEnabled;
			ForceGridOn();
			ForceAutoTriangulateOff();
			ForceAutoDrawOff();
			if (type == CampaignTutorialType.UI)
			{
				Cameras.SetOrthographicSize(7.691361f);
			}
			m_InProgress = true;
		}
	}

	public static void End()
	{
		if (m_InProgress)
		{
			BridgeShadow.Clear();
			RestoreSettings();
			m_InProgress = false;
			m_Completed = true;
			m_ResumeWhenEnteringBuildMode = false;
			GameUI.m_Instance.m_HydraulicsController.m_Locked = false;
		}
	}

	public static void UpdateManual()
	{
		GameUI.m_Instance.m_CampaignTutorial.gameObject.SetActive(IsRunning());
		if (IsRunning())
		{
			UpdateObjectives();
			UpdateWorldspaceElements();
			UpdateActiveButtons();
		}
	}

	public static void LateUpdateManual()
	{
		if (IsRunning())
		{
			if (m_CurrentStage != CampaignTutorialStage.UI_DRAW_ROAD && m_CurrentStage != CampaignTutorialStage.UI_DRAW_WOOD && m_CurrentStage != CampaignTutorialStage.HYDRO_DRAW)
			{
				m_CancelPlacementOnLateUpdate = true;
			}
			if (m_CancelPlacementOnLateUpdate)
			{
				BridgeJointPlacement.CancelSelection();
				m_CancelPlacementOnLateUpdate = false;
			}
		}
	}

	public static CampaignTutorialType GetTutorialType()
	{
		return m_TutorialType;
	}

	public static bool IsRunning()
	{
		return m_InProgress;
	}

	public static bool BindingIsBlocked(Binding binding)
	{
		if (!IsRunning())
		{
			return false;
		}
		if (GameInput.IsMouseButton(binding.m_KeyCode))
		{
			return false;
		}
		if (binding.m_BindingType == BindingType.SCREENSHOT)
		{
			return false;
		}
		if (binding.m_BindingType == BindingType.INCREASE_SIM_SPEED)
		{
			return false;
		}
		if (binding.m_BindingType == BindingType.DECREASE_SIM_SPEED)
		{
			return false;
		}
		if (binding.m_BindingType == BindingType.STRESS_VIS)
		{
			return false;
		}
		if (binding.m_BindingType == BindingType.LEVEL_INFO && m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_SHOW_LEVEL_INFO)
		{
			return false;
		}
		if (binding.m_BindingType == BindingType.COPY_SELECTION && m_CurrentStage == CampaignTutorialStage.UI_COPY_BRIDGE)
		{
			return false;
		}
		if (binding.m_BindingType == BindingType.SELECT_ROAD && m_CurrentStage == CampaignTutorialStage.UI_SELECT_ROAD)
		{
			return false;
		}
		if (binding.m_BindingType == BindingType.SELECT_WOOD && m_CurrentStage == CampaignTutorialStage.UI_SELECT_WOOD)
		{
			return false;
		}
		if (GameStateManager.GetState() != GameState.SIM || GameUI.m_Instance.m_LevelComplete.gameObject.activeInHierarchy || GameUI.m_Instance.m_LevelFailed.gameObject.activeInHierarchy)
		{
			if (binding.m_BindingType == BindingType.START_SIM && m_CurrentStage == CampaignTutorialStage.UI_SIMULATE)
			{
				return false;
			}
			if (binding.m_BindingType == BindingType.START_SIM && m_CurrentStage == CampaignTutorialStage.HYDRO_SIMULATE)
			{
				return false;
			}
			if (binding.m_BindingType == BindingType.START_SIM && m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_FIRST_SIM)
			{
				return false;
			}
			if (binding.m_BindingType == BindingType.START_SIM && m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_SECOND_SIM)
			{
				return false;
			}
			if (binding.m_BindingType == BindingType.START_SIM && m_CurrentStage == CampaignTutorialStage.HYDRO_SIMULATE)
			{
				return false;
			}
		}
		return true;
	}

	public static bool BlockMoveAction()
	{
		return IsRunning();
	}

	public static bool BlockMoveJoint(BridgeJoint joint)
	{
		return IsRunning();
	}

	public static bool BlockJointEdgeSelection()
	{
		if (!IsRunning())
		{
			return false;
		}
		return true;
	}

	public static bool BlockGroupSelect()
	{
		if (IsRunning())
		{
			return m_CurrentStage != CampaignTutorialStage.UI_SELECT_BRIDGE;
		}
		return false;
	}

	public static bool BlockCopy()
	{
		if (IsRunning())
		{
			return m_CurrentStage != CampaignTutorialStage.UI_COPY_BRIDGE;
		}
		return false;
	}

	public static bool BlockPaste()
	{
		if (!IsRunning())
		{
			return false;
		}
		if (m_CurrentStage == CampaignTutorialStage.UI_PASTE_BRIDGE)
		{
			int num = 0;
			foreach (BridgeJoint joint in BridgeJoints.m_Joints)
			{
				if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor && joint.GetNumConnectedEdges() == 0 && ClipboardManager.WillMergeWithJointGuid(joint.m_Guid))
				{
					num++;
				}
			}
			if (num == 2)
			{
				return false;
			}
		}
		return true;
	}

	public static bool CanPlaceJoint(BridgeJoint startJoint, Vector3 placementPos)
	{
		if (!BridgeShadow.PositionsMatchEdge(startJoint.transform.position, placementPos))
		{
			return false;
		}
		BridgeJoint bridgeJoint = BridgeJoints.NodeExistsAtPosition(placementPos);
		if ((bool)bridgeJoint)
		{
			BridgeEdge bridgeEdge = BridgeEdges.EdgeExistsWithNodePositions(startJoint.transform.position, bridgeJoint.transform.position);
			if ((bool)bridgeEdge && bridgeEdge.m_Material.m_MaterialType != Bridge.m_BuildMaterialType)
			{
				return false;
			}
		}
		return true;
	}

	public static void CreatedNewEdge(BridgeJoint jointA, BridgeJoint jointB, BridgeMaterialType materialType)
	{
		if (BridgeShadow.IsBuiltOver())
		{
			LoadNextStage();
		}
	}

	public static bool CanShowPistonSlider(PistonSlider slider)
	{
		if (!IsRunning())
		{
			return true;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRO_DRAG && slider.transform.position.x < 20f)
		{
			return true;
		}
		return false;
	}

	public static bool ForceShowPistonSlider(PistonSlider slider)
	{
		if (IsRunning() && m_CurrentStage == CampaignTutorialStage.HYDRO_DRAG)
		{
			return slider.transform.position.x < 20f;
		}
		return false;
	}

	public static bool CanSplitJoint(BridgeJoint joint)
	{
		if (!IsRunning())
		{
			return false;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRO_MAKE_SPLIT && Mathf.Approximately(joint.transform.position.x, 22f))
		{
			return true;
		}
		return false;
	}

	public static void ReenableButtons()
	{
		for (int i = 0; i < m_AllButtons.Count; i++)
		{
			m_AllButtons[i].interactable = true;
		}
	}

	public static void HideTutorialArrows()
	{
		GameUI.m_Instance.m_BottomBar.m_RoadMaterialButton.ShowTutorialArrow(show: false);
		GameUI.m_Instance.m_BottomBar.m_WoodMaterialButton.ShowTutorialArrow(show: false);
		GameUI.m_Instance.m_BottomBar.m_HydraulicControllerHelpArrow.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_LevelInfoTutorialArrow.SetActive(value: false);
		GameUI.m_Instance.m_TopBar.m_LevelInfoTutorialArrow.SetActive(value: false);
	}

	public static void RestoreSettings()
	{
		RestoreGridSetting();
		RestoreAutoTriangulateSetting();
		RestoreAutoDrawSetting();
	}

	public static void PauseTweens()
	{
		iTween.Pause(m_SelectionBoxTween.gameObject);
		iTween.Pause(m_MoveHydroSliderTween.gameObject);
	}

	public static void ResumeTweens()
	{
		iTween.Resume(m_MoveHydroSliderTween.gameObject);
		iTween.SetDelay(m_MoveHydroSliderTween.gameObject, m_MoveHydroSliderTween.m_Delay);
		iTween.Resume(m_SelectionBoxTween.gameObject);
		iTween.SetDelay(m_SelectionBoxTween.gameObject, m_SelectionBoxTween.m_Delay);
	}

	public static bool IsFirstStage(CampaignTutorialStage stage)
	{
		if (stage != CampaignTutorialStage.UI_INTRO && stage != CampaignTutorialStage.HYDRO_INTRO)
		{
			return stage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_INTRO;
		}
		return true;
	}

	private static void UpdateObjectives()
	{
		switch (m_TutorialType)
		{
		case CampaignTutorialType.Hydraulics:
			UpdateObjectivesHydraulics();
			break;
		case CampaignTutorialType.HydraulicController:
			UpdateObjectivesHydraulicsController();
			break;
		case CampaignTutorialType.UI:
			UpdateObjectivesUI();
			break;
		default:
			Debug.Log($"Unexpepcted tutorial type: {m_TutorialType}");
			break;
		}
	}

	private static void ResetTutorial()
	{
		m_MoveHydroSliderTween.gameObject.SetActive(value: false);
		switch (m_TutorialType)
		{
		case CampaignTutorialType.UI:
			m_CurrentStage = CampaignTutorialStage.INVALID;
			break;
		case CampaignTutorialType.Hydraulics:
			m_CurrentStage = CampaignTutorialStage.UI_END;
			break;
		case CampaignTutorialType.HydraulicController:
			m_CurrentStage = CampaignTutorialStage.HYDRO_END;
			break;
		default:
			m_CurrentStage = CampaignTutorialStage.INVALID;
			Debug.LogWarning("Unexpect tutorial type: " + GetTutorialType());
			break;
		}
		LoadNextStage();
	}

	private static void LoadNextStage()
	{
		switch (m_TutorialType)
		{
		case CampaignTutorialType.Hydraulics:
			LoadNextStageHydraulics();
			break;
		case CampaignTutorialType.HydraulicController:
			LoadNextStageHydraulicsController();
			break;
		case CampaignTutorialType.UI:
			LoadNextStageUI();
			break;
		default:
			Debug.Log($"Unexpepcted tutorial type: {m_TutorialType}");
			break;
		}
	}

	private static void CreateAllButtonsList()
	{
		m_AllButtons.AddRange(GameUI.m_Instance.m_BuildToolBar.GetComponentsInChildren<Button>(includeInactive: true));
		m_AllButtons.AddRange(GameUI.m_Instance.m_Selection.GetComponentsInChildren<Button>(includeInactive: true));
		m_AllButtons.AddRange(GameUI.m_Instance.m_Clipboard.GetComponentsInChildren<Button>(includeInactive: true));
		Button[] componentsInChildren = GameUI.m_Instance.m_TopBar.GetComponentsInChildren<Button>(includeInactive: true);
		foreach (Button button in componentsInChildren)
		{
			if (button.name != "Button_Menu" && button.name != "Button_LevelNav_Next" && button.name != "Button_LevelNav_Prev")
			{
				m_AllButtons.Add(button);
			}
		}
		componentsInChildren = GameUI.m_Instance.m_LevelFailed.GetComponentsInChildren<Button>(includeInactive: true);
		foreach (Button button2 in componentsInChildren)
		{
			if (button2.name == "Button_Share")
			{
				m_AllButtons.Add(button2);
			}
		}
	}

	private static void UpdateActiveButtons()
	{
		for (int i = 0; i < m_AllButtons.Count; i++)
		{
			if (m_AllButtons[i].name == m_CurrentActiveButtonName)
			{
				m_AllButtons[i].interactable = true;
			}
			else
			{
				m_AllButtons[i].interactable = false;
			}
		}
	}

	private static void CreateWorldspaceElements()
	{
		m_SelectionBox = new VectorLine("Selection", new List<Vector3>(8), GameUI.m_Instance.m_PlacementLineTexture, 8f);
		m_SelectionBox.rectTransform.gameObject.hideFlags = HideFlags.HideInHierarchy;
		m_SelectionBox.Draw3DAuto();
		m_SelectionBox.MakeRect(SELECT_BOX_START, SELECT_BOX_START);
		m_SelectionBox.layer = Utils.DEFAULT_LAYER;
		m_SelectionBox.textureScale = 1f;
		m_SelectionBox.color = Color.white;
		m_SelectionBox.AddNormals();
		m_SelectionBox.SetWidth(GameUI.m_Instance.m_PlacementLineWidth / Cameras.MainCamera().orthographicSize);
		GameObject gameObject = new GameObject();
		gameObject.transform.position = SELECT_BOX_START;
		m_SelectionBoxTween = gameObject.AddComponent<TweenPosition>();
		m_SelectionBoxTween.m_MoveToPos = Utils.V3toV2(SELECT_BOX_END);
		m_SelectionBoxTween.m_Delay = 0.5f;
		m_SelectionBoxTween.m_Time = 0.75f;
		m_SelectionBoxTween.m_EaseType = iTween.EaseType.linear;
		m_SelectionBoxTween.m_LoopType = iTween.LoopType.loop;
		GameObject gameObject2 = new GameObject();
		gameObject2.transform.position = new Vector3(19f, 4.75f, -10f);
		SpriteRenderer spriteRenderer = gameObject2.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = GameUI.m_Instance.m_CampaignTutorial.m_SliderArrowSprite;
		spriteRenderer.color = new Color(1f / 3f, 0.18431373f, 44f / 51f, 0.5f);
		spriteRenderer.sortingOrder = 10;
		m_MoveHydroSliderTween = gameObject2.AddComponent<TweenPosition>();
		m_MoveHydroSliderTween.m_MoveToPos = new Vector2(19f, 8.75f);
		m_MoveHydroSliderTween.m_Delay = 0.75f;
		m_MoveHydroSliderTween.m_Time = 0.75f;
		m_MoveHydroSliderTween.m_EaseType = iTween.EaseType.easeInOutQuad;
		m_MoveHydroSliderTween.m_LoopType = iTween.LoopType.loop;
	}

	private static void UpdateWorldspaceElements()
	{
		if (m_SelectionBox != null && m_SelectionBoxTween != null && GameUI.m_Instance != null)
		{
			m_SelectionBox.textureOffset = (0f - GameUI.m_Instance.m_PlacementLineAnimSpeed) * Time.unscaledTime % 1f;
			if (m_SelectionBox.active)
			{
				m_SelectionBox.MakeRect(SELECT_BOX_START, m_SelectionBoxTween.transform.position);
			}
		}
	}

	private static void DestroyWorldspaceElements()
	{
		if (m_SelectionBox != null)
		{
			VectorLine.Destroy(ref m_SelectionBox);
		}
		if (m_SelectionBoxTween != null)
		{
			Object.Destroy(m_SelectionBoxTween.gameObject);
		}
		if (m_MoveHydroSliderTween != null)
		{
			Object.Destroy(m_MoveHydroSliderTween.gameObject);
		}
	}

	private static void ForceGridOn()
	{
		Profiles.m_ActiveProfile.m_GridEnabled = true;
		GameGrid.m_Grid.SetActive(value: true);
		GameUI.m_Instance.m_BuildToolBar.m_GridButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_BuildToolBar.m_GridSelectedButton.gameObject.SetActive(value: true);
	}

	private static void ForceStressOff()
	{
		Profiles.m_ActiveProfile.m_StressViewEnabled = false;
		GameUI.m_Instance.m_SimToolBar.m_StressButton.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_SimToolBar.m_StressSelectedButton.gameObject.SetActive(value: false);
		BridgeEdges.SetDefaultColors();
	}

	private static void ForceAutoTriangulateOff()
	{
		Profiles.m_ActiveProfile.m_AutoTriangulateEnabled = false;
		GameUI.m_Instance.m_BuildToolBar.m_AutoTriangulateButton.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_BuildToolBar.m_AutoTriangulateSelectedButton.gameObject.SetActive(value: false);
	}

	private static void ForceAutoDrawOff()
	{
		Profiles.m_ActiveProfile.m_AutoDrawEnabled = false;
		GameUI.m_Instance.m_BuildToolBar.m_AutoDrawButton.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_BuildToolBar.m_AutoDrawSelectedButton.gameObject.SetActive(value: false);
	}

	private static void RestoreGridSetting()
	{
		if (m_GridOnAtStart)
		{
			GameUI.m_Instance.m_BuildToolBar.OnGridSilent();
		}
		else
		{
			GameUI.m_Instance.m_BuildToolBar.OnGridSelectedSilent();
		}
	}

	private static void RestoreAutoTriangulateSetting()
	{
		if (m_AutoTriangulateEnabledAtStart)
		{
			GameUI.m_Instance.m_BuildToolBar.OnAutoTriangulateSilent();
		}
		else
		{
			GameUI.m_Instance.m_BuildToolBar.OnAutoTriangulateSelectedSilent();
		}
	}

	private static void RestoreAutoDrawSetting()
	{
		if (m_AutoDrawEnabledAtStart)
		{
			GameUI.m_Instance.m_BuildToolBar.OnAutoDrawSilent();
		}
		else
		{
			GameUI.m_Instance.m_BuildToolBar.OnAutoDrawSelectedSilent();
		}
	}

	private static BridgeSaveData LoadShadowBridgeData(string slotFilename)
	{
		BridgeSaveSlotData bridgeSaveSlotData = BridgeSaveSlots.Load(slotFilename);
		if (bridgeSaveSlotData == null)
		{
			Debug.LogWarning("Failed to load bridge slot: " + slotFilename);
			return null;
		}
		BridgeSaveData bridgeSaveData = new BridgeSaveData();
		int offset = 0;
		bridgeSaveData.DeserializeBinary(bridgeSaveSlotData.m_Bridge, ref offset);
		return bridgeSaveData;
	}

	private static string GetShadowBridgeFilename(string filename)
	{
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
		return Path.Combine(Application.streamingAssetsPath, "Tutorials", fileNameWithoutExtension) + ".slot";
	}

	private static bool LoadNextChangeOnClick()
	{
		if (GameInput.AnyKeyDown() && !Input.GetKeyDown(KeyCode.Escape) && ActivePanels.m_Panels.Count == 0)
		{
			LoadNextStage();
			return true;
		}
		return false;
	}

	private static void LoadNextStageHydraulics()
	{
		m_CurrentStage++;
		m_CurrentActiveButtonName = "";
		m_MoveHydroSliderTween.gameObject.SetActive(value: false);
		if (m_CurrentStage == CampaignTutorialStage.HYDRO_INTRO)
		{
			GameUI.m_Instance.m_BottomBar.SelectMaterial(BridgeMaterialType.HYDRAULICS, animateTransition: false);
		}
		else if (m_CurrentStage == CampaignTutorialStage.HYDRO_SELECT_HYDRAULICS)
		{
			m_CurrentActiveButtonName = "Button_Hydraulics";
		}
		else if (m_CurrentStage == CampaignTutorialStage.HYDRO_DRAW)
		{
			LoadHydraulicsShadowBridge();
			m_CurrentActiveButtonName = "Button_Hydraulics";
			GameUI.m_Instance.m_CampaignTutorial.m_IndicateStartHydro1.SetActive(value: true);
			GameUI.m_Instance.m_CampaignTutorial.m_IndicateStartHydro2.SetActive(value: true);
			PositionStartHydroIndicators();
		}
		else if (m_CurrentStage == CampaignTutorialStage.HYDRO_DRAG)
		{
			GameUI.m_Instance.m_CampaignTutorial.m_IndicateStartHydro1.SetActive(value: false);
			GameUI.m_Instance.m_CampaignTutorial.m_IndicateStartHydro2.SetActive(value: false);
			foreach (BridgeEdge edge in BridgeEdges.m_Edges)
			{
				if (edge.IsPiston() && edge.transform.position.x < LEFT_PISTON_X_THRESHOLD)
				{
					m_MoveHydroSliderTween.transform.rotation = edge.transform.rotation;
					if (edge.m_JointA.transform.position.y > edge.m_JointB.transform.position.y)
					{
						m_MoveHydroSliderTween.m_MoveToPos = new Vector2(19f, 0.75f);
					}
					else
					{
						m_MoveHydroSliderTween.m_MoveToPos = new Vector2(19f, 8.75f);
					}
					break;
				}
			}
			m_MoveHydroSliderTween.gameObject.SetActive(value: true);
			m_MoveHydroSliderTween.Play();
		}
		else if (m_CurrentStage == CampaignTutorialStage.HYDRO_MAKE_SPLIT)
		{
			GameUI.m_Instance.m_CampaignTutorial.m_SplitJoint.SetActive(value: true);
			Vector3 vector = Cameras.MainCamera().WorldToScreenPoint(new Vector3(22f, 5f, 0f));
			GameUI.m_Instance.m_CampaignTutorial.m_SplitJointRectTransform.position = Utils.V2toV3(vector);
		}
		else if (m_CurrentStage == CampaignTutorialStage.HYDRO_SIMULATE)
		{
			GameUI.m_Instance.m_CampaignTutorial.m_SelectSim.SetActive(value: true);
			m_CurrentActiveButtonName = "Button_StartSim";
		}
		UpdateActiveButtons();
		m_CancelPlacementOnLateUpdate = true;
		GameUI.m_Instance.m_CampaignTutorial.UpdateBasicActiveStage(m_CurrentStage);
	}

	private static void UpdateObjectivesHydraulics()
	{
		if (m_CurrentStage == CampaignTutorialStage.HYDRO_INTRO)
		{
			if (GameInput.AnyKeyDown() && !Input.GetKeyDown(KeyCode.Escape) && ActivePanels.m_Panels.Count == 0)
			{
				LoadNextStage();
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRO_SELECT_HYDRAULICS)
		{
			if (Bridge.m_BuildMaterialType == BridgeMaterialType.HYDRAULICS)
			{
				LoadNextStage();
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRO_DRAW)
		{
			MaybeHideStartHydroIndicators();
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRO_DRAG)
		{
			if ((bool)Pistons.m_SliderFollowingMouse && Pistons.m_SliderFollowingMouse.m_Piston.m_Edge.transform.position.x < LEFT_PISTON_X_THRESHOLD && Mathf.Approximately(Pistons.m_SliderFollowingMouse.GetNormalizedValue(), 1f))
			{
				Pistons.ForceStopSliderFollowingMouse();
				LoadNextStage();
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRO_MAKE_SPLIT)
		{
			foreach (BridgeJoint joint in BridgeJoints.m_Joints)
			{
				if (joint.m_IsSplit)
				{
					GameUI.m_Instance.m_CampaignTutorial.m_SplitJoint.SetActive(value: false);
					LoadNextStage();
					break;
				}
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRO_SIMULATE)
		{
			if (GameStateManager.GetState() == GameState.SIM)
			{
				GameUI.m_Instance.m_CampaignTutorial.m_SelectSim.SetActive(value: false);
				LoadNextStage();
			}
		}
		else if (m_CurrentStage == CampaignTutorialStage.HYDRO_END && LevelCompleteScreenActive())
		{
			End();
		}
	}

	private static void LoadHydraulicsShadowBridge()
	{
		BridgeSaveData bridgeSaveData = LoadShadowBridgeData(GetShadowBridgeFilename("024"));
		if (bridgeSaveData != null)
		{
			BridgeShadow.Show(bridgeSaveData);
		}
	}

	private static void PositionStartHydroIndicators()
	{
		Vector2 v = Cameras.MainCamera().WorldToScreenPoint(HYDRO_START_POS_1);
		GameUI.m_Instance.m_CampaignTutorial.m_IndicateStartHydro1.transform.position = Utils.V2toV3(v);
		Vector2 v2 = Cameras.MainCamera().WorldToScreenPoint(HYDRO_START_POS_2);
		GameUI.m_Instance.m_CampaignTutorial.m_IndicateStartHydro2.transform.position = Utils.V2toV3(v2);
	}

	private static void MaybeHideStartHydroIndicators()
	{
		foreach (Piston piston in Pistons.m_Pistons)
		{
			if (piston.m_Edge.transform.position.x < LEFT_PISTON_X_THRESHOLD)
			{
				GameUI.m_Instance.m_CampaignTutorial.m_IndicateStartHydro1.gameObject.SetActive(value: false);
			}
			else
			{
				GameUI.m_Instance.m_CampaignTutorial.m_IndicateStartHydro2.gameObject.SetActive(value: false);
			}
		}
	}

	private static bool LevelCompleteScreenActive()
	{
		return GameUI.m_Instance.m_LevelComplete.gameObject.activeInHierarchy;
	}

	private static void LoadNextStageHydraulicsController()
	{
		m_CurrentStage++;
		m_CurrentActiveButtonName = "";
		if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_INTRO)
		{
			GameUI.m_Instance.m_HydraulicsController.m_Locked = true;
		}
		else if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_FIRST_SIM)
		{
			GameUI.m_Instance.m_CampaignTutorial.m_SelectSim.SetActive(value: true);
			m_CurrentActiveButtonName = "Button_StartSim";
		}
		else if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_SHOW_LEVEL_INFO)
		{
			GameUI.m_Instance.m_TopBar.m_LevelInfoTutorialArrow.SetActive(value: true);
			m_CurrentActiveButtonName = "Button_LevelInfo";
		}
		else if (m_CurrentStage != CampaignTutorialStage.HYDRAULICS_CONTROLLER_PHASES && m_CurrentStage != CampaignTutorialStage.HYDRAULICS_CONTROLLER_PHASED && m_CurrentStage != CampaignTutorialStage.HYDRAULICS_CONTROLLER_WAIT_FOR_FAILURE)
		{
			if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_FAILED)
			{
				m_ResumeWhenEnteringBuildMode = true;
			}
			else if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_SELECT_CONTROLLER)
			{
				GameUI.m_Instance.m_BottomBar.m_HydraulicControllerHelpArrow.SetActive(value: true);
				m_CurrentActiveButtonName = "Button_HydraulicController";
			}
			else if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_CLICK_D)
			{
				GameUI.m_Instance.m_BottomBar.m_HydraulicControllerHelpArrow.SetActive(value: false);
				GameUI.m_Instance.m_CampaignTutorial.m_SelectPhaseD.SetActive(value: true);
			}
			else if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_DISABLE_HYDRO)
			{
				GameUI.m_Instance.m_CampaignTutorial.m_SelectPhaseD.SetActive(value: false);
				GameUI.m_Instance.m_CampaignTutorial.m_ClickHydraulic.SetActive(value: true);
				PositionClickHydraulicIndicator();
			}
			else if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_SECOND_SIM)
			{
				GameUI.m_Instance.m_CampaignTutorial.m_ClickHydraulic.SetActive(value: false);
				GameUI.m_Instance.m_CampaignTutorial.m_SelectSim.SetActive(value: true);
				m_CurrentActiveButtonName = "Button_StartSim";
			}
			else if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_WAIT_FOR_NOTICE)
			{
				GameUI.m_Instance.m_CampaignTutorial.m_SelectSim.SetActive(value: false);
			}
			else
			{
				_ = m_CurrentStage;
				_ = 32;
			}
		}
		UpdateActiveButtons();
		m_CancelPlacementOnLateUpdate = true;
		GameUI.m_Instance.m_CampaignTutorial.UpdateBasicActiveStage(m_CurrentStage);
	}

	private static void PositionClickHydraulicIndicator()
	{
		BridgeEdge bridgeEdge = null;
		foreach (BridgeEdge edge in BridgeEdges.m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && edge.IsPiston())
			{
				bridgeEdge = edge;
			}
		}
		if (bridgeEdge == null)
		{
			return;
		}
		foreach (BridgeEdge edge2 in BridgeEdges.m_Edges)
		{
			if (edge2.gameObject.activeInHierarchy && edge2.IsPiston() && edge2.transform.position.x > bridgeEdge.transform.position.x)
			{
				bridgeEdge = edge2;
			}
		}
		GameUI.m_Instance.m_CampaignTutorial.PositionClickHydrulicIndicator(bridgeEdge.GetCenterPos());
	}

	private static void UpdateObjectivesHydraulicsController()
	{
		GameUI.m_Instance.m_CampaignTutorial.m_SelectPhaseD.GetComponent<RectTransform>().anchoredPosition = new Vector2(-72f, (GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? 150 : 120);
		if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_INTRO)
		{
			LoadNextChangeOnClick();
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_FIRST_SIM)
		{
			if (GameStateManager.GetState() == GameState.SIM)
			{
				GameUI.m_Instance.m_CampaignTutorial.m_SelectSim.SetActive(value: false);
				LoadNextStage();
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_WAIT_FOR_PHASEA)
		{
			if (GameStateSim.m_ElapsedSeconds > 2f)
			{
				LoadNextStage();
				GameUI.m_Instance.m_TopBar.OnPauseSim();
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_SHOW_LEVEL_INFO)
		{
			if (GameUI.m_Instance.m_LevelInfoLite.gameObject.activeInHierarchy)
			{
				GameUI.m_Instance.m_TopBar.m_LevelInfoTutorialArrow.SetActive(value: false);
				LoadNextStage();
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_PHASES)
		{
			if (GameInput.AnyKeyDown() && !Input.GetKeyDown(KeyCode.Escape))
			{
				GameUI.m_Instance.m_TopBar.OnUnPauseSim();
				LoadNextStage();
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_WAIT_FOR_PHASED)
		{
			foreach (EventTimeline timeline in EventTimelines.m_Timelines)
			{
				if ((bool)timeline.m_ActiveStage && timeline.m_ActiveStage.GetStageLabel() == "D")
				{
					LoadNextStage();
					GameUI.m_Instance.m_TopBar.OnPauseSim();
					break;
				}
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_PHASED)
		{
			if (GameInput.AnyKeyDown() && !Input.GetKeyDown(KeyCode.Escape))
			{
				GameUI.m_Instance.m_TopBar.OnUnPauseSim();
				LoadNextStage();
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_WAIT_FOR_FAILURE)
		{
			if (GameUI.m_Instance.m_LevelFailed.gameObject.activeInHierarchy)
			{
				LoadNextStage();
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_FAILED)
		{
			if (!GameUI.m_Instance.m_LevelFailed.gameObject.activeInHierarchy)
			{
				LoadNextStage();
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_SELECT_CONTROLLER)
		{
			if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
			{
				LoadNextStage();
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_CLICK_D)
		{
			EventUnit selectedPhase = GameUI.m_Instance.m_HydraulicsController.GetSelectedPhase();
			if (selectedPhase != null && (bool)selectedPhase.m_ParentStage && selectedPhase.m_ParentStage.GetStageLabel() == "D")
			{
				LoadNextStage();
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_DISABLE_HYDRO)
		{
			HydraulicsControllerPhase hydraulicsControllerPhase = HydraulicsController.FindControllerPhaseWithHydraulicsPhase(GameUI.m_Instance.m_HydraulicsController.GetSelectedHydraulicsPhase());
			if (hydraulicsControllerPhase != null && hydraulicsControllerPhase.m_Pistons.Count == 2)
			{
				LoadNextStage();
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_SECOND_SIM)
		{
			if (GameStateManager.GetState() == GameState.SIM)
			{
				GameUI.m_Instance.m_CampaignTutorial.m_SelectSim.SetActive(value: false);
				LoadNextStage();
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_WAIT_FOR_NOTICE)
		{
			foreach (EventTimeline timeline2 in EventTimelines.m_Timelines)
			{
				if ((bool)timeline2.m_ActiveStage && timeline2.m_ActiveStage.GetStageLabel() == "D")
				{
					LoadNextStage();
					break;
				}
			}
			return;
		}
		if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_NOTICE)
		{
			if (LevelCompleteScreenActive())
			{
				LoadNextStage();
			}
		}
		else if (m_CurrentStage == CampaignTutorialStage.HYDRAULICS_CONTROLLER_END && LevelCompleteScreenActive())
		{
			End();
		}
	}

	private static void LoadNextStageUI()
	{
		m_CurrentStage++;
		m_CurrentActiveButtonName = "";
		m_SelectionBox.active = false;
		m_SelectionBoxTween.gameObject.SetActive(value: false);
		if (m_CurrentStage == CampaignTutorialStage.UI_INTRO)
		{
			GameUI.m_Instance.m_BottomBar.ClearSelectedMaterial();
			Bridge.m_BuildMaterialType = BridgeMaterialType.WOOD;
			Budget.m_RoadBudget = 0;
			Budget.m_WoodBudget = 0;
		}
		else if (m_CurrentStage == CampaignTutorialStage.UI_SELECT_ROAD)
		{
			GameUI.m_Instance.m_BottomBar.m_RoadMaterialButton.ShowTutorialArrow(show: true);
			Budget.m_RoadBudget = Budget.UNLIMITED_MATERIAL_BUDGET;
			m_CurrentActiveButtonName = "Button_Road";
		}
		else if (m_CurrentStage == CampaignTutorialStage.UI_DRAW_ROAD)
		{
			m_CurrentActiveButtonName = "Button_Road";
			LoadRoadShadowBridge();
			GameUI.m_Instance.m_CampaignTutorial.m_ClickAnchor.SetActive(value: true);
			PositionClickAnchorArrow();
		}
		else if (m_CurrentStage == CampaignTutorialStage.UI_TRUSSES)
		{
			GameUI.m_Instance.m_CampaignTutorial.m_ClickAnchor.SetActive(value: false);
		}
		else if (m_CurrentStage == CampaignTutorialStage.UI_SELECT_WOOD)
		{
			Budget.m_WoodBudget = Budget.UNLIMITED_MATERIAL_BUDGET;
			GameUI.m_Instance.m_BottomBar.m_WoodMaterialButton.ShowTutorialArrow(show: true);
			m_CurrentActiveButtonName = "Button_Wood";
		}
		else if (m_CurrentStage == CampaignTutorialStage.UI_DRAW_WOOD)
		{
			m_CurrentActiveButtonName = "Button_Wood";
			LoadRoadWithTrussesShadowBridge();
		}
		else if (m_CurrentStage == CampaignTutorialStage.UI_SELECT_BRIDGE)
		{
			m_SelectionBox.active = true;
			m_SelectionBoxTween.gameObject.SetActive(value: true);
			m_SelectionBoxTween.Play();
		}
		else if (m_CurrentStage == CampaignTutorialStage.UI_COPY_BRIDGE)
		{
			GameUI.m_Instance.m_Selection.ShowCopyTutorialArrow(show: true);
			m_CurrentActiveButtonName = "Button_Copy";
		}
		else if (m_CurrentStage == CampaignTutorialStage.UI_PASTE_BRIDGE)
		{
			GameUI.m_Instance.m_CampaignTutorial.m_IndicateAnchor1.SetActive(value: true);
			GameUI.m_Instance.m_CampaignTutorial.m_IndicateAnchor2.SetActive(value: true);
			GameUI.m_Instance.m_Selection.ShowCopyTutorialArrow(show: false);
			PositionIndicateAnchors();
		}
		else if (m_CurrentStage == CampaignTutorialStage.UI_SIMULATE)
		{
			GameUI.m_Instance.m_CampaignTutorial.m_IndicateAnchor1.SetActive(value: false);
			GameUI.m_Instance.m_CampaignTutorial.m_IndicateAnchor2.SetActive(value: false);
			GameUI.m_Instance.m_CampaignTutorial.m_SelectSim.SetActive(value: true);
			m_CurrentActiveButtonName = "Button_StartSim";
		}
		UpdateActiveButtons();
		m_CancelPlacementOnLateUpdate = true;
		GameUI.m_Instance.m_CampaignTutorial.UpdateBasicActiveStage(m_CurrentStage);
	}

	private static void UpdateObjectivesUI()
	{
		if (m_CurrentStage == CampaignTutorialStage.UI_INTRO)
		{
			if (GameInput.AnyKeyDown() && !Input.GetKeyDown(KeyCode.Escape) && ActivePanels.m_Panels.Count == 0)
			{
				LoadNextStage();
			}
		}
		else if (m_CurrentStage == CampaignTutorialStage.UI_SELECT_ROAD)
		{
			if (Bridge.m_BuildMaterialType == BridgeMaterialType.ROAD)
			{
				GameUI.m_Instance.m_BottomBar.m_RoadMaterialButton.ShowTutorialArrow(show: false);
				LoadNextStage();
			}
		}
		else if (m_CurrentStage == CampaignTutorialStage.UI_DRAW_ROAD)
		{
			if (BridgeEdges.m_Edges.Count > 0)
			{
				GameUI.m_Instance.m_CampaignTutorial.m_ClickAnchor.gameObject.SetActive(value: false);
			}
		}
		else if (m_CurrentStage == CampaignTutorialStage.UI_TRUSSES)
		{
			if (GameInput.AnyKeyDown() && !Input.GetKeyDown(KeyCode.Escape) && ActivePanels.m_Panels.Count == 0)
			{
				LoadNextStage();
			}
		}
		else if (m_CurrentStage == CampaignTutorialStage.UI_SELECT_WOOD)
		{
			if (Bridge.m_BuildMaterialType == BridgeMaterialType.WOOD)
			{
				GameUI.m_Instance.m_BottomBar.m_WoodMaterialButton.ShowTutorialArrow(show: false);
				LoadNextStage();
			}
		}
		else
		{
			if (m_CurrentStage == CampaignTutorialStage.UI_DRAW_WOOD)
			{
				return;
			}
			if (m_CurrentStage == CampaignTutorialStage.UI_SELECT_BRIDGE)
			{
				if (AllJointsWithEdgesSelected())
				{
					LoadNextStage();
				}
				else if (!BridgeSelectionSet.IsEmpty())
				{
					BridgeSelectionSet.CancelSelection();
					InterfaceAudio.PlayErrorBeep();
				}
			}
			else if (m_CurrentStage == CampaignTutorialStage.UI_COPY_BRIDGE)
			{
				if (ClipboardManager.ReadyToPaste())
				{
					LoadNextStage();
				}
			}
			else if (m_CurrentStage == CampaignTutorialStage.UI_PASTE_BRIDGE)
			{
				if (AllAnchorsHaveConnections())
				{
					ClipboardManager.ClearClipboard();
					LoadNextStage();
				}
			}
			else if (m_CurrentStage == CampaignTutorialStage.UI_SIMULATE)
			{
				if (GameStateManager.GetState() == GameState.SIM)
				{
					GameUI.m_Instance.m_CampaignTutorial.m_SelectSim.SetActive(value: false);
					LoadNextStage();
				}
			}
			else if (m_CurrentStage == CampaignTutorialStage.UI_END && LevelCompleteScreenActive())
			{
				End();
			}
		}
	}

	private static void LoadRoadShadowBridge()
	{
		BridgeSaveData bridgeSaveData = LoadShadowBridgeData(GetShadowBridgeFilename("900_0"));
		if (bridgeSaveData != null)
		{
			BridgeShadow.Show(bridgeSaveData);
		}
	}

	private static void LoadRoadWithTrussesShadowBridge()
	{
		BridgeSaveData bridgeSaveData = LoadShadowBridgeData(GetShadowBridgeFilename("900_1"));
		if (bridgeSaveData != null)
		{
			BridgeShadow.Show(bridgeSaveData);
		}
	}

	private static bool AllJointsWithEdgesSelected()
	{
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && !joint.m_IsAnchor && joint.GetNumConnectedEdges() > 0 && !BridgeSelectionSet.ContainsJoint(joint))
			{
				return false;
			}
		}
		return true;
	}

	private static bool AllAnchorsHaveConnections()
	{
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor && joint.GetNumConnectedEdges() == 0)
			{
				return false;
			}
		}
		return true;
	}

	private static void PositionClickAnchorArrow()
	{
		if (BridgeJoints.m_Joints.Count == 0)
		{
			return;
		}
		BridgeJoint bridgeJoint = BridgeJoints.m_Joints[0];
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor && joint.transform.position.x < bridgeJoint.transform.position.x)
			{
				bridgeJoint = joint;
			}
		}
		if (bridgeJoint != null)
		{
			Vector2 v = Cameras.MainCamera().WorldToScreenPoint(bridgeJoint.transform.position);
			GameUI.m_Instance.m_CampaignTutorial.m_ClickAnchor.transform.position = Utils.V2toV3(v);
		}
	}

	private static void PositionIndicateAnchors()
	{
		BridgeJoint bridgeJoint = null;
		BridgeJoint bridgeJoint2 = null;
		int num = 0;
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && joint.m_IsAnchor)
			{
				num++;
				if (num == 4)
				{
					bridgeJoint = joint;
				}
				if (num == 2)
				{
					bridgeJoint2 = joint;
				}
			}
		}
		if (bridgeJoint != null)
		{
			Vector2 v = Cameras.MainCamera().WorldToScreenPoint(bridgeJoint.transform.position);
			GameUI.m_Instance.m_CampaignTutorial.m_IndicateAnchor1.transform.position = Utils.V2toV3(v);
		}
		if (bridgeJoint2 != null)
		{
			Vector2 v2 = Cameras.MainCamera().WorldToScreenPoint(bridgeJoint2.transform.position);
			GameUI.m_Instance.m_CampaignTutorial.m_IndicateAnchor2.transform.position = Utils.V2toV3(v2);
		}
	}
}
