using UnityEngine;
using UnityEngine.UI;

public class Panel_SandboxMenu : MonoBehaviour
{
	public PointerEvents m_PointerEvents;

	[Header("Panels")]
	public RectTransform m_RootRectTransform;

	public Panel_SandboxTabs m_SandboxTabsPanel;

	[Header("Collapse")]
	public Button m_CollapseButton;

	public GameObject m_Divider;

	public static CollapseState m_CollapseState;

	private void Start()
	{
		m_CollapseButton.onClick.AddListener(OnCollapse);
		m_CollapseState = CollapseState.UNCOLLAPSED;
	}

	private void OnEnable()
	{
		CheckForMultipleSelection();
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
		if (SandboxSelectionSet.IsEmpty())
		{
			ActivateSandboxSubMenu(null);
		}
		else
		{
			CheckForMultipleSelection();
		}
	}

	public void OnSandboxEditItem()
	{
		GameObject editMenuForSelection = GetEditMenuForSelection();
		ActivateSandboxSubMenu(editMenuForSelection);
	}

	public void ActivateSandboxSubMenu(GameObject menuToActivate)
	{
		if (!(menuToActivate == null) || !GameUI.m_Instance.m_SandboxEditCustomShapeTools.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_SandboxMultiSelect.gameObject.SetActive((GameUI.m_Instance.m_SandboxMultiSelect.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditAnchor.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditAnchor.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditZedAxisVehicle.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditZedAxisVehicle.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditCheckpoint.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditCheckpoint.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditBuildZone.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditBuildZone.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditCustomShape.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditCustomShape.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditCustomShapeTools.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditCustomShapeTools.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditFlyingObject.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditFlyingObject.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditRock.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditRock.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditPillar.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditPillar.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditDecor.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditDecor.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditHydraulicsPhase.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditHydraulicsPhase.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditVehicleRestartPhase.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditVehicleRestartPhase.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditPlatform.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditPlatform.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditRamp.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditRamp.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditTerrain.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditTerrain.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditVehicle.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditVehicle.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditVehicleStopTrigger.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditVehicleStopTrigger.gameObject == menuToActivate) ? true : false);
			GameUI.m_Instance.m_SandboxEditWater.gameObject.SetActive((GameUI.m_Instance.m_SandboxEditWater.gameObject == menuToActivate) ? true : false);
		}
	}

	public GameObject GetActiveSubMenu()
	{
		if (GameUI.m_Instance.m_SandboxMultiSelect.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxMultiSelect.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditAnchor.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditAnchor.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditZedAxisVehicle.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditZedAxisVehicle.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditCheckpoint.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditCheckpoint.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditCustomShape.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditCustomShape.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditCustomShapeTools.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditCustomShapeTools.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditFlyingObject.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditFlyingObject.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditRock.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditRock.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditPillar.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditPillar.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditDecor.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditDecor.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditHydraulicsPhase.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditHydraulicsPhase.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditPlatform.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditPlatform.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditRamp.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditRamp.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditTerrain.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditTerrain.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditVehicle.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditVehicle.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditVehicleRestartPhase.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditVehicleRestartPhase.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditVehicleStopTrigger.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditVehicleStopTrigger.gameObject;
		}
		if (GameUI.m_Instance.m_SandboxEditWater.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditWater.gameObject;
		}
		return null;
	}

	public void MaybeActivateEditSubmenu()
	{
		if (SandboxSelectionSet.m_Items.Count == 1)
		{
			GameUI.m_Instance.m_SandboxMenu.ActivateSandboxSubMenu(GetEditMenuForSelection());
		}
	}

	public bool IsEditMenu(GameObject menu)
	{
		if (!(menu == GameUI.m_Instance.m_SandboxMultiSelect.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditAnchor.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditZedAxisVehicle.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditCheckpoint.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditBuildZone.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditCustomShape.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditCustomShapeTools.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditFlyingObject.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditRock.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditPillar.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditDecor.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditHydraulicsPhase.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditPlatform.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditRamp.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditTerrain.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditVehicle.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditVehicleRestartPhase.gameObject) && !(menu == GameUI.m_Instance.m_SandboxEditVehicleStopTrigger.gameObject))
		{
			return menu == GameUI.m_Instance.m_SandboxEditWater.gameObject;
		}
		return true;
	}

	public GameObject GetEditMenuForSelection()
	{
		if (SandboxSelectionSet.IsEmpty() || SandboxSelectionSet.MultipleItemsSelected())
		{
			return GameUI.m_Instance.m_SandboxMultiSelect.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedAnchor())
		{
			return GameUI.m_Instance.m_SandboxEditAnchor.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedZedAxisVehicle())
		{
			return GameUI.m_Instance.m_SandboxEditZedAxisVehicle.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedCheckpoint())
		{
			return GameUI.m_Instance.m_SandboxEditCheckpoint.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedBuildZone())
		{
			return GameUI.m_Instance.m_SandboxEditBuildZone.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedCustomShape() && !GameUI.m_Instance.m_SandboxEditCustomShapeTools.gameObject.activeInHierarchy)
		{
			return GameUI.m_Instance.m_SandboxEditCustomShape.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedFlyingObject())
		{
			return GameUI.m_Instance.m_SandboxEditFlyingObject.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedRock())
		{
			return GameUI.m_Instance.m_SandboxEditRock.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedPlatform())
		{
			return GameUI.m_Instance.m_SandboxEditPlatform.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedPillar())
		{
			return GameUI.m_Instance.m_SandboxEditPillar.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedDecor())
		{
			return GameUI.m_Instance.m_SandboxEditDecor.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedRamp())
		{
			return GameUI.m_Instance.m_SandboxEditRamp.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedTerrain())
		{
			return GameUI.m_Instance.m_SandboxEditTerrain.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedVehicle())
		{
			return GameUI.m_Instance.m_SandboxEditVehicle.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedVehicleStopTrigger())
		{
			return GameUI.m_Instance.m_SandboxEditVehicleStopTrigger.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedHydraulicsPhase())
		{
			return GameUI.m_Instance.m_SandboxEditHydraulicsPhase.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedVehicleRestartPhase())
		{
			return GameUI.m_Instance.m_SandboxEditVehicleRestartPhase.gameObject;
		}
		if ((bool)SandboxSelectionSet.GetSelectedWaterBlock())
		{
			return GameUI.m_Instance.m_SandboxEditWater.gameObject;
		}
		return null;
	}

	public bool IsCollapsed()
	{
		return m_CollapseState == CollapseState.COLLAPSED;
	}

	private void OnCollapse()
	{
		if (m_CollapseState == CollapseState.UNCOLLAPSED)
		{
			m_CollapseState = CollapseState.COLLAPSED;
			m_CollapseButton.transform.localScale = new Vector3(-1f, 1f, 1f);
			m_RootRectTransform.anchoredPosition = new Vector2(195f, -51f);
			m_Divider.SetActive(value: false);
			InterfaceAudio.Play("ui_menubar_gen_off");
		}
		else if (m_CollapseState == CollapseState.COLLAPSED)
		{
			m_CollapseState = CollapseState.UNCOLLAPSED;
			m_CollapseButton.transform.localScale = Vector3.one;
			m_RootRectTransform.anchoredPosition = new Vector2(0f, -51f);
			m_Divider.SetActive(value: true);
			InterfaceAudio.Play("ui_menubar_gen_on");
		}
	}

	private void CheckForMultipleSelection()
	{
		if (SandboxSelectionSet.MultipleItemsSelected() && !GameUI.m_Instance.m_SandboxMultiSelect.gameObject.activeInHierarchy)
		{
			ActivateSandboxSubMenu(GameUI.m_Instance.m_SandboxMultiSelect.gameObject);
		}
	}
}
