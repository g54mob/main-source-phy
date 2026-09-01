using System;
using System.Collections.Generic;
using BesiegeDlc;
using UnityEngine;

[AddComponentMenu("UI/Advanced UI Controller")]
public class AdvancedUIController : MonoBehaviour
{
	[Serializable]
	public class TooltipSwitch
	{
		public Tooltip tooltip;

		public Transform parent;

		public Transform advancedParent;

		[HideInInspector]
		public Vector3 defaultDir = Vector3.zero;
	}

	public static AdvancedUIController Instance;

	public GameObject translateTool;

	public MachineRotation rotateTool;

	public GameObject groundTool;

	public GameObject miniGroundTool;

	public GameObject mirrorTool;

	public GameObject symmetryTool;

	public GameObject eraserTool;

	public GameObject modifyTool;

	public GameObject paintTool;

	public GameObject intersectionButton;

	public GameObject duplicateButton;

	public GameObject binButton;

	public GameObject keyButton;

	public GameObject keyDropButton;

	public GameObject aeroButton;

	public GameObject dropBG;

	public GameObject comButton;

	public AlignUIBetween infoBG;

	public SoundOnClick rotateSound;

	public BlockButtonControl surfaceButton;

	public GameObject infoButton;

	public AlignUI blockCounterAlign;

	public GameObject boundsButton;

	public Transform tabBG;

	public Transform blockButtons;

	private Vector3 binPos;

	private Vector3 infoPos;

	private Vector3 initialPos;

	private Vector3 modifyPos;

	private Vector3 symmetryPos;

	private Vector3 lastButtonPos;

	public bool newUI;

	public TooltipSwitch[] tooltips;

	private void Awake()
	{
		BlockSkinLoader.SkinModified += ToggleSkins;
		DlcManager instance = DlcManager.Instance;
		instance.DlcSettingsChanged = (Action)Delegate.Combine(instance.DlcSettingsChanged, new Action(OnDlcChanged));
		Instance = this;
		binPos = binButton.transform.localPosition;
		if ((bool)infoButton)
		{
			infoPos = infoButton.transform.localPosition;
		}
		initialPos = base.transform.localPosition;
		symmetryPos = symmetryTool.transform.localPosition;
		modifyPos = modifyTool.transform.localPosition;
	}

	private void Start()
	{
		Toggle(StatMaster.advancedBuilding);
		if (!StatMaster.isMP)
		{
			OnDlcChanged();
		}
	}

	private void OnDestroy()
	{
		BlockSkinLoader.SkinModified -= ToggleSkins;
		if (DlcManager.Instance != null)
		{
			DlcManager instance = DlcManager.Instance;
			instance.DlcSettingsChanged = (Action)Delegate.Remove(instance.DlcSettingsChanged, new Action(OnDlcChanged));
		}
	}

	public void ToggleAdvanced(bool toggle)
	{
		TranslateButton.Instance.ToggleAdvanced(toggle);
		MachineRotation.Instance.ToggleAdvanced(toggle);
		SymmetryButton.Instance.ToggleAdvanced(toggle);
		MirrorButton.Instance.ToggleAdvanced(toggle);
		PaintButton.Instance.ToggleAdvanced(toggle);
	}

	public void OnDlcChanged()
	{
		if (DlcManager.Instance == null)
		{
			return;
		}
		DlcManager.DlcStatusType dlcStatus = DlcManager.Instance.GetDlcStatus(DlcManager.DlcType.Water);
		if ((bool)tabBG && (bool)blockButtons)
		{
			DlcManager.DlcStatusType dlcStatusType = dlcStatus;
			if (dlcStatusType == DlcManager.DlcStatusType.MissingDlc)
			{
				Vector3 localPosition = tabBG.localPosition;
				localPosition.x = 1.05339f;
				tabBG.localPosition = localPosition;
				localPosition = blockButtons.localPosition;
				localPosition.x = -0.74f;
				blockButtons.localPosition = localPosition;
			}
			else
			{
				Vector3 localPosition = tabBG.localPosition;
				localPosition.x = 1.79339f;
				tabBG.localPosition = localPosition;
				localPosition = blockButtons.localPosition;
				localPosition.x = 0f;
				blockButtons.localPosition = localPosition;
			}
		}
	}

	public void Toggle(bool toggle)
	{
		ToggleAdvanced(toggle);
		if (!newUI)
		{
			base.transform.localPosition = initialPos + new Vector3(toggle ? 0.066f : 0.36227846f, 0f, 0f);
		}
		if (StatMaster.Mode.selectedTool != StatMaster.Tool.Modify)
		{
			BlockSelectionTool selectionController = AdvancedBlockEditor.Instance.selectionController;
			selectionController.DeselectAll(false);
			Machine machine = Machine.Active();
			if (toggle && machine != null)
			{
				List<BlockBehaviour> selectedBlocks = machine.UndoSystem.GetSelectedBlocks();
				if (selectedBlocks.Count > 0)
				{
					selectionController.Select(selectedBlocks, false, false);
				}
			}
		}
		if ((bool)duplicateButton)
		{
			duplicateButton.SetActive(false);
		}
		if (!newUI && (bool)keyDropButton)
		{
			keyDropButton.SetActive(false);
		}
		groundTool.SetActive(!toggle);
		miniGroundTool.SetActive(toggle);
		surfaceButton.gameObject.SetActive(toggle);
		int siblingIndex = surfaceButton.transform.GetSiblingIndex();
		Transform parent = surfaceButton.transform.parent;
		if (toggle)
		{
			if (lastButtonPos != Vector3.zero)
			{
				Vector3 localPosition = lastButtonPos;
				for (int num = parent.childCount - 1; num > siblingIndex; num--)
				{
					Transform child = parent.GetChild(num);
					Vector3 localPosition2 = child.localPosition;
					child.localPosition = localPosition;
					localPosition = localPosition2;
				}
			}
		}
		else
		{
			siblingIndex++;
			Vector3 localPosition3 = surfaceButton.transform.localPosition;
			for (int i = siblingIndex; i < parent.childCount; i++)
			{
				Transform child2 = parent.GetChild(i);
				Vector3 localPosition4 = child2.localPosition;
				child2.localPosition = localPosition3;
				localPosition3 = (lastButtonPos = localPosition4);
			}
		}
		mirrorTool.SetActive(toggle);
		intersectionButton.SetActive(toggle);
		paintTool.SetActive(toggle && OptionsMaster.skinsEnabled);
		eraserTool.SetActive(!paintTool.activeSelf);
		if (!newUI)
		{
			rotateSound.enabled = toggle;
			binButton.transform.localPosition = ((!toggle) ? intersectionButton.transform.localPosition : binPos);
			modifyTool.transform.localPosition = ((!toggle) ? modifyPos : symmetryPos);
			symmetryTool.transform.localPosition = ((!toggle) ? symmetryPos : modifyPos);
		}
		else
		{
			if ((bool)infoBG)
			{
				aeroButton.SetActive(toggle);
				infoBG.quad2 = ((!toggle) ? comButton.transform.GetChild(0) : aeroButton.transform.GetChild(0));
				infoBG.updateHook[0] = ((!toggle) ? comButton : aeroButton).GetComponent<AlignUI>();
				symmetryTool.SetActive(toggle);
				infoBG.ScheduleAlign();
			}
			if ((bool)boundsButton)
			{
				boundsButton.SetActive(!toggle);
				binButton.transform.localPosition = ((!toggle) ? (boundsButton.transform.localPosition + Vector3.left * 0.75f) : binPos);
				if ((bool)infoButton)
				{
					infoButton.transform.localPosition = ((!toggle) ? (boundsButton.transform.localPosition + Vector3.right * 0.73f) : infoPos);
					if ((bool)blockCounterAlign)
					{
						blockCounterAlign.Align();
					}
				}
			}
			else
			{
				binButton.transform.localPosition = ((!toggle) ? symmetryTool.transform.localPosition : binPos);
				if ((bool)infoButton)
				{
					infoButton.transform.localPosition = ((!toggle) ? intersectionButton.transform.localPosition : infoPos);
					if ((bool)blockCounterAlign)
					{
						blockCounterAlign.Align();
					}
				}
			}
		}
		if (!newUI)
		{
			dropBG.transform.localScale = new Vector3(dropBG.transform.lossyScale.x, 4.33f, dropBG.transform.lossyScale.z);
		}
		for (int j = 0; j < tooltips.Length; j++)
		{
			TooltipSwitch tooltipSwitch = tooltips[j];
			tooltipSwitch.tooltip.tooltipParent = ((!toggle) ? tooltipSwitch.parent : tooltipSwitch.advancedParent);
			(toggle ? tooltipSwitch.parent : tooltipSwitch.advancedParent).gameObject.SetActive(false);
			((!toggle) ? tooltipSwitch.parent : tooltipSwitch.advancedParent).gameObject.SetActive(true);
			if (!newUI)
			{
				if (tooltipSwitch.defaultDir == Vector3.zero)
				{
					tooltipSwitch.defaultDir = tooltipSwitch.tooltip.lerpPosDirection;
				}
				tooltipSwitch.tooltip.lerpPosDirection = ((!toggle) ? tooltipSwitch.defaultDir : new Vector3(-0.5f, 0f, 0f));
			}
			tooltipSwitch.tooltip.Reset();
		}
		if (!toggle)
		{
			rotateTool.Reset();
		}
		AdvancedBlockEditor.Instance.UpdateGizmo();
	}

	public void ToggleSkins(BlockSkinLoader.SModifier m)
	{
		if (m != null && m == BlockSkinLoader.UpdateAll)
		{
			ToggleSkins();
		}
	}

	public void ToggleSkins()
	{
		if (StatMaster.Mode.selectedTool == StatMaster.Tool.Erase || StatMaster.Mode.selectedTool == StatMaster.Tool.Paint)
		{
			MachineToolController.Instance.DisableAll();
		}
		paintTool.SetActive(StatMaster.advancedBuilding && OptionsMaster.skinsEnabled);
		eraserTool.SetActive(!paintTool.activeSelf);
	}
}
