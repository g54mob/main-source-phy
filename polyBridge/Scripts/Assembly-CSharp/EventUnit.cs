using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventUnit : MonoBehaviour
{
	public GameObject m_BackgroundHover;

	public GameObject m_BackgroundSelected;

	public Image m_IconBackground;

	public Image m_Icon;

	public TextMeshProUGUI m_Text;

	public PointerEvents m_IconEvents;

	public Image m_Off;

	[NonSerialized]
	public EventUnitType m_Type;

	[NonSerialized]
	public GameObject m_SourceObject;

	[NonSerialized]
	public EventStage m_ParentStage;

	[NonSerialized]
	public Vector3 m_OffsetFromPointer;

	[NonSerialized]
	public Vector2 m_StartMovementPos;

	[NonSerialized]
	public RectTransform m_RectTransform;

	private float m_ElapsedSeconds;

	private bool m_StartedSimulation;

	private float NO_PROGRESS_TIMEOUT_SECONDS = 5f;

	private void Awake()
	{
		m_RectTransform = m_Icon.GetComponent<RectTransform>();
		m_Type = EventUnitType.NONE;
		m_Off.gameObject.SetActive(value: false);
	}

	private void OnDestroy()
	{
		if ((bool)m_ParentStage && m_ParentStage.m_Units.Contains(this))
		{
			m_ParentStage.m_Units.Remove(this);
		}
	}

	public void Restore()
	{
		m_StartedSimulation = false;
		m_ElapsedSeconds = 0f;
	}

	public void SetText(string text)
	{
		m_Text.gameObject.SetActive(value: true);
		m_Text.text = text;
	}

	public void SetSprite(GameObject source)
	{
		m_Icon.sprite = source.GetComponent<SandboxItem>().GetSpriteForEventViewer();
		if (m_Type == EventUnitType.VEHICLE_RESTART_PHASE)
		{
			AdjustIconForVehicleRestart();
		}
	}

	public void AdjustIconForVehicleRestart()
	{
		if (!(m_SourceObject != null))
		{
			return;
		}
		VehicleRestartPhase component = m_SourceObject.GetComponent<VehicleRestartPhase>();
		m_Icon.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
		m_Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 5.6f);
		Vehicle vehicle = Vehicles.FindByGuid(component.m_VehicleGuid);
		if (vehicle != null)
		{
			VehicleSkin currentSkin = vehicle.GetCurrentSkin();
			if (currentSkin != null)
			{
				m_Icon.color = currentSkin.GetColorForUI();
			}
		}
	}

	public void FixedUpdate_Manual()
	{
		m_ElapsedSeconds += Time.fixedDeltaTime / BridgeSimSpeed.m_SimulationSpeedMultiplier;
		if (!m_StartedSimulation)
		{
			switch (m_Type)
			{
			case EventUnitType.ZED_AXIS_VEHICLE:
				MaybeStartZedAxisVehicleSimulation();
				break;
			case EventUnitType.HYDRAULICS_PHASE:
				MaybeStartHydraulicsPhase();
				break;
			case EventUnitType.VEHICLE:
				MaybeStartVehicleSimulation();
				break;
			case EventUnitType.VEHICLE_RESTART_PHASE:
				MaybeStartVehicleRestartPhase();
				break;
			}
		}
	}

	public void StartSimulation()
	{
		m_StartedSimulation = true;
		switch (m_Type)
		{
		case EventUnitType.ZED_AXIS_VEHICLE:
			GetZedAxisVehicle().StartSimulation();
			break;
		case EventUnitType.HYDRAULICS_PHASE:
			GetHydraulicsPhase().StartSimulation();
			break;
		case EventUnitType.VEHICLE:
			GetVehicle().StartSimulation();
			break;
		case EventUnitType.VEHICLE_RESTART_PHASE:
			GetVehicleRestartPhase().StartSimulation();
			break;
		default:
			m_StartedSimulation = false;
			break;
		}
	}

	public bool IsComplete()
	{
		if (!m_StartedSimulation)
		{
			return false;
		}
		return m_Type switch
		{
			EventUnitType.HYDRAULICS_PHASE => HydraulicsPhaseIsComplete(), 
			EventUnitType.VEHICLE => VehiclePhaseIsComplete(), 
			EventUnitType.VEHICLE_RESTART_PHASE => VehicleRestartPhaseIsComplete(), 
			EventUnitType.ZED_AXIS_VEHICLE => ZedAxisVehiclePhaseIsComplete(), 
			_ => true, 
		};
	}

	public Vehicle GetHungVehicle()
	{
		if (IsComplete() || !m_SourceObject)
		{
			return null;
		}
		Vehicle vehicle = m_SourceObject.GetComponent<Vehicle>();
		if (!vehicle)
		{
			VehicleRestartPhase component = m_SourceObject.GetComponent<VehicleRestartPhase>();
			if ((bool)component)
			{
				vehicle = Vehicles.FindByGuid(component.m_VehicleGuid);
			}
		}
		if ((bool)vehicle && vehicle.m_NumSecondsNoProgressWithMotorOn > NO_PROGRESS_TIMEOUT_SECONDS)
		{
			return vehicle;
		}
		return null;
	}

	public bool IsEmpty()
	{
		return m_Type == EventUnitType.NONE;
	}

	public void Hover()
	{
		m_BackgroundHover.SetActive(value: true);
	}

	public void UnHover()
	{
		m_BackgroundHover.SetActive(value: false);
	}

	public void Select()
	{
		m_BackgroundHover.SetActive(value: false);
		m_BackgroundSelected.SetActive(value: true);
	}

	public void DeSelect()
	{
		m_BackgroundSelected.SetActive(value: false);
	}

	public ZedAxisVehicle GetZedAxisVehicle()
	{
		if (m_Type != EventUnitType.ZED_AXIS_VEHICLE || !(m_SourceObject != null))
		{
			return null;
		}
		return m_SourceObject.GetComponent<ZedAxisVehicle>();
	}

	public HydraulicsPhase GetHydraulicsPhase()
	{
		if (m_Type != EventUnitType.HYDRAULICS_PHASE || !(m_SourceObject != null))
		{
			return null;
		}
		return m_SourceObject.GetComponent<HydraulicsPhase>();
	}

	public Vehicle GetVehicle()
	{
		if (m_Type != EventUnitType.VEHICLE || !(m_SourceObject != null))
		{
			return null;
		}
		return m_SourceObject.GetComponent<Vehicle>();
	}

	public VehicleRestartPhase GetVehicleRestartPhase()
	{
		if (m_Type != EventUnitType.VEHICLE_RESTART_PHASE || !(m_SourceObject != null))
		{
			return null;
		}
		return m_SourceObject.GetComponent<VehicleRestartPhase>();
	}

	public bool HasStartedSimulation()
	{
		return m_StartedSimulation;
	}

	private void MaybeStartZedAxisVehicleSimulation()
	{
		ZedAxisVehicle zedAxisVehicle = GetZedAxisVehicle();
		if ((bool)zedAxisVehicle && m_ElapsedSeconds > zedAxisVehicle.m_TimeDelaySeconds)
		{
			StartSimulation();
		}
	}

	private void MaybeStartVehicleSimulation()
	{
		Vehicle vehicle = GetVehicle();
		if ((bool)vehicle && m_ElapsedSeconds > vehicle.m_TimeDelaySeconds)
		{
			StartSimulation();
		}
	}

	private void MaybeStartHydraulicsPhase()
	{
		HydraulicsPhase hydraulicsPhase = GetHydraulicsPhase();
		if ((bool)hydraulicsPhase && m_ElapsedSeconds > hydraulicsPhase.m_TimeDelaySeconds)
		{
			StartSimulation();
		}
	}

	private void MaybeStartVehicleRestartPhase()
	{
		VehicleRestartPhase vehicleRestartPhase = GetVehicleRestartPhase();
		if ((bool)vehicleRestartPhase && m_ElapsedSeconds > vehicleRestartPhase.m_TimeDelaySeconds)
		{
			StartSimulation();
		}
	}

	private bool HydraulicsPhaseIsComplete()
	{
		HydraulicsPhase hydraulicsPhase = GetHydraulicsPhase();
		if (!hydraulicsPhase)
		{
			return true;
		}
		return hydraulicsPhase.IsComplete();
	}

	private bool VehiclePhaseIsComplete()
	{
		Vehicle vehicle = GetVehicle();
		if (!vehicle)
		{
			return true;
		}
		if (!vehicle.m_ReachedVictoryFlag)
		{
			return vehicle.m_ReachedStopCheckpoint;
		}
		return true;
	}

	private bool VehicleRestartPhaseIsComplete()
	{
		VehicleRestartPhase vehicleRestartPhase = GetVehicleRestartPhase();
		if (!vehicleRestartPhase)
		{
			return true;
		}
		return vehicleRestartPhase.IsComplete();
	}

	private bool ZedAxisVehiclePhaseIsComplete()
	{
		ZedAxisVehicle zedAxisVehicle = GetZedAxisVehicle();
		if (!zedAxisVehicle)
		{
			return true;
		}
		return zedAxisVehicle.TravelledCompletelyOutOfWorld();
	}
}
