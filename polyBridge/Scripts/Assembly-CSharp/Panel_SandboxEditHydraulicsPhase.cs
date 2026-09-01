using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_SandboxEditHydraulicsPhase : MonoBehaviour
{
	[Header("Input Fields")]
	public SandboxInputField m_InputFieldTimeDelay;

	[Header("Buttons")]
	public Button m_ButtonDelete;

	private HydraulicsPhase m_LastRefreshedHydraulicsPhase;

	private void Start()
	{
		m_ButtonDelete.onClick.AddListener(OnDelete);
	}

	private void Update()
	{
		HydraulicsPhase selectedHydraulicsPhase = SandboxSelectionSet.GetSelectedHydraulicsPhase();
		if ((bool)selectedHydraulicsPhase && selectedHydraulicsPhase != m_LastRefreshedHydraulicsPhase)
		{
			RefreshProperties(selectedHydraulicsPhase);
		}
		ProcessInput();
	}

	private void OnEnable()
	{
		HydraulicsPhase selectedHydraulicsPhase = SandboxSelectionSet.GetSelectedHydraulicsPhase();
		if ((bool)selectedHydraulicsPhase)
		{
			RefreshProperties(selectedHydraulicsPhase);
		}
	}

	private void OnDisable()
	{
		m_LastRefreshedHydraulicsPhase = null;
	}

	public void ForceRefresh()
	{
		m_LastRefreshedHydraulicsPhase = null;
	}

	public void OnDelete()
	{
		if ((bool)SandboxSelectionSet.GetSelectedHydraulicsPhase())
		{
			InterfaceAudio.Play("ui_build_delete");
			SandboxSelectionSet.Delete();
		}
	}

	public void RefreshProperties(HydraulicsPhase phase)
	{
		if ((bool)phase)
		{
			m_InputFieldTimeDelay.m_InputField.text = Utils.FormatSeconds(phase.m_TimeDelaySeconds);
			m_LastRefreshedHydraulicsPhase = phase;
		}
	}

	private void ProcessInput()
	{
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
		{
			ExecuteEvents.Execute(m_ButtonDelete.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}
}
