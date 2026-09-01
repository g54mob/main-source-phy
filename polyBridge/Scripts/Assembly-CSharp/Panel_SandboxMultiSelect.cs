using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_SandboxMultiSelect : MonoBehaviour
{
	[Header("Buttons")]
	public Button m_DuplicateButton;

	public Button m_DeleteButton;

	public Button m_ExportCustomShape;

	[Header("Nudge")]
	public Panel_SandboxNudge m_SandboxNudge;

	[Header("Toggle")]
	public GameObject m_TogglesPanel;

	public SandboxToggle m_ShowInBuildSandboxToggle;

	[Header("Lock Movement Help")]
	public GameObject m_HelpPanel;

	public TextMeshProUGUI m_LockMovementHelpText;

	private PointerEvents m_ShowInBuildTogglePointerEvents;

	private List<SandboxItem> m_DuplicatedItems = new List<SandboxItem>();

	private void Start()
	{
		m_ShowInBuildTogglePointerEvents = m_ShowInBuildSandboxToggle.m_Toggle.GetComponent<PointerEvents>();
		m_ShowInBuildTogglePointerEvents.RegisterOnClickedDelegate(OnShowInBuildToggle);
		m_DuplicateButton.onClick.AddListener(OnDuplicate);
		m_DeleteButton.onClick.AddListener(OnDelete);
		m_ExportCustomShape.onClick.AddListener(OnExportCustomShape);
	}

	private void OnEnable()
	{
		RefreshProperties();
	}

	private void Update()
	{
		if (GameUI.m_Instance.m_SandboxMenu.GetEditMenuForSelection() != GameUI.m_Instance.m_SandboxMultiSelect.gameObject)
		{
			GameUI.m_Instance.m_SandboxMenu.ActivateSandboxSubMenu(GameUI.m_Instance.m_SandboxMenu.GetEditMenuForSelection());
			return;
		}
		UpdateExportCustomShapeVisibility();
		UpdateNudgeZ();
		RefreshLockMovementHelpText();
		ProcessInput();
	}

	public void RefreshProperties()
	{
		RefreshLockMovementHelpText();
		UpdateExportCustomShapeVisibility();
		UpdateNudgeZ();
		UpdateShowInBuildToggle();
	}

	public void UpdateForCurrentDevice()
	{
		m_HelpPanel.SetActive(GameInput.GetActiveGameDevice() != GameDevice.Gamepad);
		m_SandboxNudge.UpdateForCurrentDevice();
	}

	private void UpdateExportCustomShapeVisibility()
	{
		bool active = true;
		foreach (SandboxItem item in SandboxSelectionSet.m_Items)
		{
			if (item.m_Type != SandboxItemType.CUSTOM_SHAPE)
			{
				active = false;
				break;
			}
			if (item.m_Type == SandboxItemType.CUSTOM_SHAPE && item.GetComponent<CustomShape>().IsDynamicProp())
			{
				active = false;
				break;
			}
		}
		m_ExportCustomShape.gameObject.SetActive(active);
	}

	private void UpdateNudgeZ()
	{
		m_SandboxNudge.EnableNudgeZ(SandboxSelectionSet.AllItemsAreDecorOrCustomShapes());
	}

	private void OnDuplicate()
	{
		if (SandboxSelectionSet.m_Items.Count == 0)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		Bounds bounds = new Bounds(SandboxSelectionSet.m_Items[0].transform.position, new Vector3(2f, 1f, 1f));
		foreach (SandboxItem item in SandboxSelectionSet.m_Items)
		{
			if ((item.m_Type == SandboxItemType.TERRAIN && item.GetComponent<TerrainIsland>().m_TerrainIslandType == TerrainIslandType.Bookend) || item.m_Colliders == null)
			{
				continue;
			}
			Collider[] colliders = item.m_Colliders;
			foreach (Collider collider in colliders)
			{
				if (collider != null)
				{
					bounds.Encapsulate(collider.bounds);
				}
			}
		}
		Vector3 offset = new Vector3(bounds.size.x + 2f, 0f, 0f);
		m_DuplicatedItems.Clear();
		foreach (SandboxItem item2 in SandboxSelectionSet.m_Items)
		{
			SandboxItem sandboxItem = item2.TryDuplicate(offset);
			if ((bool)sandboxItem)
			{
				m_DuplicatedItems.Add(sandboxItem);
			}
		}
		if (m_DuplicatedItems.Count == 0)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		BridgeJoints.ResolveOverlappingAnchors(Vector3.right);
		InterfaceAudio.Play("ui_build_generic_place");
		SandboxSelectionSet.CancelSelection();
		foreach (SandboxItem duplicatedItem in m_DuplicatedItems)
		{
			SandboxSelectionSet.SelectItem(duplicatedItem);
		}
		SandboxUndo.SnapShot();
	}

	private void OnDelete()
	{
		if (!SandboxSelectionSet.IsEmpty())
		{
			InterfaceAudio.Play("ui_build_delete");
			SandboxSelectionSet.Delete();
		}
	}

	private void OnExportCustomShape()
	{
		PopupInputField.Display(Localize.Get("UI_CUSTOM_SHAPE_EXPORT_NAME"), string.Empty, isFilename: false, isDirectory: false, SandboxSelectionSet.ExportSelectedCustomShapes);
	}

	private void RefreshLockMovementHelpText()
	{
		string tooltipBindingString = Bindings.m_Bindings[BindingType.HORIZONTAL_CONSTRAINT_UNIVERSAL].GetTooltipBindingString();
		string tooltipBindingString2 = Bindings.m_Bindings[BindingType.VERTICAL_CONSTRAINT_UNIVERSAL].GetTooltipBindingString();
		if (string.IsNullOrEmpty(tooltipBindingString) || string.IsNullOrEmpty(tooltipBindingString2))
		{
			m_LockMovementHelpText.text = string.Empty;
			return;
		}
		m_LockMovementHelpText.text = string.Format(Localize.Get("UI_SANDBOX_LOCK_MOVEMENT_HELP"), tooltipBindingString, tooltipBindingString2);
		if (Game.InDecorModeTopView())
		{
			m_LockMovementHelpText.text = m_LockMovementHelpText.text.Replace("Y", "Z");
		}
	}

	private void ProcessInput()
	{
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
		{
			ExecuteEvents.Execute(m_DeleteButton.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
		{
			ExecuteEvents.Execute(m_DuplicateButton.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	private void OnShowInBuildToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		bool flag = false;
		if (m_ShowInBuildSandboxToggle.m_Toggle.isOn)
		{
			foreach (SandboxItem item in SandboxSelectionSet.m_Items)
			{
				if (item.m_Type == SandboxItemType.DECOR)
				{
					item.GetComponent<Decor>().m_ShowInBuildMode = true;
					flag = true;
				}
			}
		}
		else
		{
			foreach (SandboxItem item2 in SandboxSelectionSet.m_Items)
			{
				if (item2.m_Type == SandboxItemType.DECOR)
				{
					item2.GetComponent<Decor>().m_ShowInBuildMode = false;
					flag = true;
				}
			}
		}
		if (flag)
		{
			SandboxUndo.SnapShot();
			m_ShowInBuildSandboxToggle.EnableMixedImage(on: false);
		}
	}

	private void UpdateShowInBuildToggle()
	{
		int num = 0;
		int num2 = 0;
		bool flag = false;
		foreach (SandboxItem item in SandboxSelectionSet.m_Items)
		{
			if (item.m_Type != SandboxItemType.DECOR)
			{
				flag = true;
				break;
			}
			if (item.GetComponent<Decor>().m_ShowInBuildMode)
			{
				num++;
			}
			else
			{
				num2++;
			}
		}
		m_TogglesPanel.gameObject.SetActive(!flag);
		if (num > 0 && num2 > 0)
		{
			m_ShowInBuildSandboxToggle.m_Toggle.isOn = false;
			m_ShowInBuildSandboxToggle.EnableMixedImage(on: true);
		}
		else if (num > 0)
		{
			m_ShowInBuildSandboxToggle.m_Toggle.isOn = true;
			m_ShowInBuildSandboxToggle.EnableMixedImage(on: false);
		}
		else if (num2 > 0)
		{
			m_ShowInBuildSandboxToggle.m_Toggle.isOn = false;
			m_ShowInBuildSandboxToggle.EnableMixedImage(on: false);
		}
	}
}
