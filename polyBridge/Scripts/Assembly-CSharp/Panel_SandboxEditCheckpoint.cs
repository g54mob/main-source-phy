using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_SandboxEditCheckpoint : MonoBehaviour
{
	public Image m_Icon;

	public RectTransform m_VerticalLayoutGroupInner;

	public RectTransform m_VerticalLayoutGroupOuter;

	public SandboxPanelResizer m_ResizerInner;

	[Header("Nudge")]
	public Panel_SandboxNudge m_SandboxNudge;

	[Header("Input Fields")]
	public SandboxInputField m_InputFieldPosX;

	public SandboxInputField m_InputFieldPosY;

	[Header("Toggles")]
	public Toggle m_TriggerTimelineToggle;

	public Toggle m_StopToggle;

	public Toggle m_ReverseOnRestartToggle;

	public Toggle m_InvisibleInSimToggle;

	[Header("Buttons")]
	public Button m_DeleteButton;

	private PointerEvents m_TriggerTimelinePointerEvents;

	private PointerEvents m_StopPointerEvents;

	private PointerEvents m_ReverseOnRestartPointerEvents;

	private PointerEvents m_InvisibleInSimPointerEvents;

	private Checkpoint m_LastRefreshedCheckpoint;

	private void Awake()
	{
		m_TriggerTimelinePointerEvents = m_TriggerTimelineToggle.GetComponent<PointerEvents>();
		m_TriggerTimelinePointerEvents.RegisterOnClickedDelegate(OnTriggerTimelineToggle);
		m_StopPointerEvents = m_StopToggle.GetComponent<PointerEvents>();
		m_StopPointerEvents.RegisterOnClickedDelegate(OnStopToggle);
		m_ReverseOnRestartPointerEvents = m_ReverseOnRestartToggle.GetComponent<PointerEvents>();
		m_ReverseOnRestartPointerEvents.RegisterOnClickedDelegate(OnReverseOnRestartToggle);
		m_InvisibleInSimPointerEvents = m_InvisibleInSimToggle.GetComponent<PointerEvents>();
		m_InvisibleInSimPointerEvents.RegisterOnClickedDelegate(OnInvisibleInSimToggle);
	}

	private void Update()
	{
		Checkpoint selectedCheckpoint = GetSelectedCheckpoint();
		if ((bool)selectedCheckpoint && selectedCheckpoint != m_LastRefreshedCheckpoint)
		{
			RefreshProperties(selectedCheckpoint);
		}
		UpdateIcon(selectedCheckpoint);
		ProcessInput();
	}

	private void OnEnable()
	{
		Checkpoint selectedCheckpoint = GetSelectedCheckpoint();
		if ((bool)selectedCheckpoint)
		{
			RefreshProperties(selectedCheckpoint);
			UpdateIcon(selectedCheckpoint);
		}
		if ((bool)m_DeleteButton)
		{
			m_DeleteButton.onClick.AddListener(OnDelete);
		}
		RefreshPanelLayout();
	}

	private void OnDisable()
	{
		m_LastRefreshedCheckpoint = null;
		if ((bool)m_DeleteButton)
		{
			m_DeleteButton.onClick.RemoveAllListeners();
		}
	}

	public void UpdateForCurrentDevice()
	{
		m_SandboxNudge.UpdateForCurrentDevice();
	}

	public void ForceRefresh()
	{
		m_LastRefreshedCheckpoint = null;
	}

	public void RefreshPosition(Checkpoint checkpoint)
	{
		m_InputFieldPosX.m_InputField.text = Utils.FormatThreeDecimalPlaces(checkpoint.transform.position.x);
		m_InputFieldPosY.m_InputField.text = Utils.FormatThreeDecimalPlaces(checkpoint.transform.position.y);
	}

	public void RefreshToggles(Checkpoint checkpoint)
	{
		m_TriggerTimelineToggle.isOn = checkpoint.m_TriggerTimeline;
		m_StopToggle.isOn = checkpoint.m_StopVehicle;
		m_ReverseOnRestartToggle.isOn = checkpoint.m_ReverseVehicleOnRestart;
		m_InvisibleInSimToggle.isOn = checkpoint.m_InvisibleInSim;
	}

	public void RefreshProperties(Checkpoint checkpoint)
	{
		if ((bool)checkpoint)
		{
			RefreshPosition(checkpoint);
			RefreshToggles(checkpoint);
			m_LastRefreshedCheckpoint = checkpoint;
		}
	}

	private void UpdateReverseAfterRestartVisibility()
	{
		m_ReverseOnRestartToggle.transform.parent.gameObject.SetActive(m_StopToggle.isOn);
	}

	public void HideProperties(bool hide)
	{
		m_TriggerTimelineToggle.transform.parent.gameObject.SetActive(!hide);
		m_StopToggle.transform.parent.gameObject.SetActive(!hide);
		m_ReverseOnRestartToggle.transform.parent.gameObject.SetActive(!hide);
		m_InvisibleInSimToggle.transform.parent.gameObject.SetActive(!hide);
	}

	private void OnTriggerTimelineToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Checkpoint selectedCheckpoint = GetSelectedCheckpoint();
		if ((bool)selectedCheckpoint)
		{
			selectedCheckpoint.m_TriggerTimeline = m_TriggerTimelineToggle.isOn;
			if (selectedCheckpoint.m_TriggerTimeline && selectedCheckpoint.m_Timeline == null)
			{
				selectedCheckpoint.m_Timeline = EventTimelines.CreateTimeline();
				selectedCheckpoint.m_Timeline.m_Header.text = selectedCheckpoint.GetTextMeshString();
				selectedCheckpoint.m_Timeline.m_Checkpoint = selectedCheckpoint;
				selectedCheckpoint.m_Timeline.SetCheckpointSprite();
			}
			if (!selectedCheckpoint.m_TriggerTimeline && selectedCheckpoint.m_Timeline != null)
			{
				EventTimelines.DestroyCheckpointTimeline(selectedCheckpoint);
			}
			SandboxUndo.SnapShot();
		}
	}

	private void OnStopToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Checkpoint selectedCheckpoint = GetSelectedCheckpoint();
		if ((bool)selectedCheckpoint)
		{
			selectedCheckpoint.m_StopVehicle = m_StopToggle.isOn;
			selectedCheckpoint.RefreshMesh();
			selectedCheckpoint.InstantiatePickupFX();
			if (selectedCheckpoint.m_StopVehicle)
			{
				CreateRestartVehiclePhase(selectedCheckpoint);
			}
			else
			{
				DeleteRestartVehiclePhase(selectedCheckpoint);
			}
			if (selectedCheckpoint.m_Timeline != null)
			{
				selectedCheckpoint.m_Timeline.SetCheckpointSprite();
			}
			RefreshPanelLayout();
			SandboxUndo.SnapShot();
		}
	}

	private void RefreshPanelLayout()
	{
		if ((bool)m_VerticalLayoutGroupOuter && (bool)m_VerticalLayoutGroupInner && (bool)m_ResizerInner)
		{
			UpdateReverseAfterRestartVisibility();
			LayoutRebuilder.ForceRebuildLayoutImmediate(m_VerticalLayoutGroupInner);
			m_ResizerInner.ForceUpdate();
			LayoutRebuilder.ForceRebuildLayoutImmediate(m_VerticalLayoutGroupOuter);
		}
	}

	private void CreateRestartVehiclePhase(Checkpoint checkpoint)
	{
		if ((bool)Vehicles.FindByGuid(checkpoint.m_VehicleGuid))
		{
			VehicleRestartPhase vehicleRestartPhase = VehicleRestartPhases.CreatePhase(Vector3.zero, Utils.GenerateUniqueId(), checkpoint.m_VehicleGuid);
			if ((bool)vehicleRestartPhase)
			{
				checkpoint.m_VehicleRestartPhaseGuid = vehicleRestartPhase.m_Guid;
				vehicleRestartPhase.AddToEventEdtior();
			}
		}
	}

	private void DeleteRestartVehiclePhase(Checkpoint checkpoint)
	{
		VehicleRestartPhase vehicleRestartPhase = VehicleRestartPhases.FindByGuid(checkpoint.m_VehicleRestartPhaseGuid);
		if ((bool)vehicleRestartPhase)
		{
			VehicleRestartPhases.DestroyPhase(vehicleRestartPhase);
		}
	}

	private void OnReverseOnRestartToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Checkpoint selectedCheckpoint = GetSelectedCheckpoint();
		if ((bool)selectedCheckpoint)
		{
			selectedCheckpoint.m_ReverseVehicleOnRestart = m_ReverseOnRestartToggle.isOn;
			selectedCheckpoint.RefreshMesh();
			selectedCheckpoint.InstantiatePickupFX();
			if (selectedCheckpoint.m_Timeline != null)
			{
				selectedCheckpoint.m_Timeline.SetCheckpointSprite();
			}
			SandboxUndo.SnapShot();
		}
	}

	private void OnInvisibleInSimToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		Checkpoint selectedCheckpoint = GetSelectedCheckpoint();
		if ((bool)selectedCheckpoint)
		{
			selectedCheckpoint.m_InvisibleInSim = m_InvisibleInSimToggle.isOn;
			selectedCheckpoint.m_SandboxItem.m_Label.m_InvisibleIcon.gameObject.SetActive(m_InvisibleInSimToggle.isOn);
			SandboxUndo.SnapShot();
		}
	}

	private void OnDelete()
	{
		InterfaceAudio.Play("ui_build_delete");
		Checkpoint selectedCheckpoint = GetSelectedCheckpoint();
		if ((bool)selectedCheckpoint)
		{
			SandboxSelectionSet.CancelSelection();
			Checkpoints.DestroyCheckpoint(selectedCheckpoint);
			SandboxUndo.SnapShot();
			GameUI.m_Instance.m_SandboxEditCheckpoint.gameObject.SetActive(value: false);
		}
	}

	private Checkpoint GetSelectedCheckpoint()
	{
		SandboxTapeCheckpoint component = base.gameObject.GetComponent<SandboxTapeCheckpoint>();
		if (!(component != null))
		{
			return SandboxSelectionSet.GetSelectedCheckpoint();
		}
		return component.m_Checkpoint;
	}

	private void UpdateIcon(Checkpoint checkpoint)
	{
		if (checkpoint != null && m_Icon != null)
		{
			m_Icon.sprite = checkpoint.GetCheckpointSprite();
			m_Icon.color = checkpoint.m_Color;
		}
	}

	private void ProcessInput()
	{
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
		{
			ExecuteEvents.Execute(m_DeleteButton.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}
}
