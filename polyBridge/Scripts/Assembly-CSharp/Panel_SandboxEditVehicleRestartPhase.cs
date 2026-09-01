using UnityEngine;

public class Panel_SandboxEditVehicleRestartPhase : MonoBehaviour
{
	[Header("Input Fields")]
	public SandboxInputField m_InputFieldTimeDelay;

	private VehicleRestartPhase m_LastRefreshedVehicleRestartPhase;

	private void Update()
	{
		VehicleRestartPhase selectedVehicleRestartPhase = SandboxSelectionSet.GetSelectedVehicleRestartPhase();
		if ((bool)selectedVehicleRestartPhase && selectedVehicleRestartPhase != m_LastRefreshedVehicleRestartPhase)
		{
			RefreshProperties(selectedVehicleRestartPhase);
		}
	}

	private void OnEnable()
	{
		VehicleRestartPhase selectedVehicleRestartPhase = SandboxSelectionSet.GetSelectedVehicleRestartPhase();
		if ((bool)selectedVehicleRestartPhase)
		{
			RefreshProperties(selectedVehicleRestartPhase);
		}
	}

	private void OnDisable()
	{
		m_LastRefreshedVehicleRestartPhase = null;
	}

	public void ForceRefresh()
	{
		m_LastRefreshedVehicleRestartPhase = null;
	}

	public void RefreshProperties(VehicleRestartPhase phase)
	{
		if ((bool)phase)
		{
			m_InputFieldTimeDelay.m_InputField.text = Utils.FormatSeconds(phase.m_TimeDelaySeconds);
			m_LastRefreshedVehicleRestartPhase = phase;
		}
	}
}
