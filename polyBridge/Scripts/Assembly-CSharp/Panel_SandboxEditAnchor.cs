using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_SandboxEditAnchor : MonoBehaviour
{
	[Header("Input Fields")]
	public SandboxInputField m_InputFieldPosX;

	public SandboxInputField m_InputFieldPosY;

	[Header("Nudge")]
	public Panel_SandboxNudge m_SandboxNudge;

	[Header("Toggles")]
	public Toggle m_NoBuildToggle;

	[Header("Buttons")]
	public Button m_ButtonConvertToNode;

	public Button m_ButtonDuplicate;

	public Button m_ButtonDelete;

	private PointerEvents m_NoBuildTogglePointerEvents;

	private BridgeJoint m_LastRefreshedAnchor;

	private readonly Vector3 DUPLICATE_OFFSET = new Vector3(0.5f, 0f, 0f);

	private void Awake()
	{
		m_NoBuildTogglePointerEvents = m_NoBuildToggle.GetComponent<PointerEvents>();
		m_NoBuildTogglePointerEvents.RegisterOnClickedDelegate(OnNoBuildToggle);
		m_ButtonConvertToNode.onClick.AddListener(OnConvertToNode);
		m_ButtonDelete.onClick.AddListener(OnDelete);
		m_ButtonDuplicate.onClick.AddListener(OnDuplicate);
	}

	private void Update()
	{
		BridgeJoint selectedAnchor = SandboxSelectionSet.GetSelectedAnchor();
		if ((bool)selectedAnchor && selectedAnchor != m_LastRefreshedAnchor)
		{
			RefreshProperties(selectedAnchor);
		}
		ProcessInput(selectedAnchor);
	}

	private void OnEnable()
	{
		BridgeJoint selectedAnchor = SandboxSelectionSet.GetSelectedAnchor();
		if ((bool)selectedAnchor)
		{
			RefreshProperties(selectedAnchor);
		}
	}

	private void OnDisable()
	{
		m_LastRefreshedAnchor = null;
	}

	public void UpdateForCurrentDevice()
	{
		m_SandboxNudge.UpdateForCurrentDevice();
	}

	public void ForceRefresh()
	{
		m_LastRefreshedAnchor = null;
	}

	public void RefreshPosition(BridgeJoint anchor)
	{
		m_InputFieldPosX.m_InputField.text = Utils.FormatThreeDecimalPlaces(anchor.transform.position.x);
		m_InputFieldPosY.m_InputField.text = Utils.FormatThreeDecimalPlaces(anchor.transform.position.y);
	}

	private void RefreshToggles(BridgeJoint anchor)
	{
		m_NoBuildToggle.isOn = anchor.m_NoBuild;
	}

	public void RefreshProperties(BridgeJoint anchor)
	{
		if ((bool)anchor)
		{
			bool flag = anchor.isCustomShapeAnchor() || BridgePillars.IsBridgePillarAnchor(anchor.m_Guid);
			m_InputFieldPosX.gameObject.SetActive(!flag);
			m_InputFieldPosY.gameObject.SetActive(!flag);
			RefreshPosition(anchor);
			RefreshToggles(anchor);
			m_LastRefreshedAnchor = anchor;
		}
	}

	private void OnConvertToNode()
	{
		BridgeJoint selectedAnchor = SandboxSelectionSet.GetSelectedAnchor();
		if (!selectedAnchor)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		selectedAnchor.RevertAnchor();
		selectedAnchor.m_FX.gameObject.SetActive(value: true);
		SandboxUndo.SnapShot();
		SandboxSelectionSet.CancelSelection();
	}

	private void OnDelete()
	{
		if ((bool)SandboxSelectionSet.GetSelectedAnchor())
		{
			InterfaceAudio.Play("ui_build_delete");
			SandboxSelectionSet.Delete();
		}
	}

	private void OnDuplicate()
	{
		InterfaceAudio.Play("ui_build_generic_place");
		BridgeJoint selectedAnchor = SandboxSelectionSet.GetSelectedAnchor();
		if ((bool)selectedAnchor)
		{
			BridgeJoint bridgeJoint = BridgeJoints.CreateAnchor(selectedAnchor.transform.position + DUPLICATE_OFFSET, Utils.GenerateUniqueId());
			if (bridgeJoint != null)
			{
				SandboxSelectionSet.ForceSelection(bridgeJoint.m_SandboxItem);
				SandboxUndo.SnapShot();
			}
		}
	}

	private void OnNoBuildToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		BridgeJoint selectedAnchor = SandboxSelectionSet.GetSelectedAnchor();
		if ((bool)selectedAnchor)
		{
			selectedAnchor.m_NoBuild = m_NoBuildToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void ProcessInput(BridgeJoint anchor)
	{
		if ((bool)anchor && !GameStateCommonInput.IgnoreKeyboardInput())
		{
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
			{
				ExecuteEvents.Execute(m_ButtonDelete.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
			}
			else if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
			{
				ExecuteEvents.Execute(m_ButtonDuplicate.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
			}
		}
	}
}
