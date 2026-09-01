using System;
using Landfall.TABS.UnitEditor;
using UnityEngine;
using UnityEngine.UI;

public class UnitEditorWeaponsPage : UnitEditorSubMenu
{
	[SerializeField]
	private Toggle oneHandedToggle;

	[SerializeField]
	private Toggle twoHandedToggle;

	private const string Toggle = "BUTTON_TOGGLE";

	private const string Flip = "LABEL_WEAPONFLIP";

	private UnitEditorManager unitEditorManager;

	public void SetManager(UnitEditorManager manager)
	{
		unitEditorManager = manager;
	}

	public override void Open()
	{
		base.Open();
		UpdateGlyphs();
		SetInitialWeaponHandedToggle();
	}

	public override void OnGainedFocus()
	{
		base.OnGainedFocus();
		UpdateGlyphs();
	}

	protected override void Update()
	{
		if (!(unitEditorManager == null))
		{
			base.Update();
		}
	}

	protected override void UpdateGamepads()
	{
		base.UpdateGamepads();
		if (!UnitEditorManager.isTestingUnit)
		{
			if (playerActions.m_flipWeaponSlots.WasPressed)
			{
				unitEditorManager.FlipWeapons();
			}
			if (playerActions.m_toggleOneTwoHandedWeapons.WasPressed)
			{
				ToggleWeaponHandMode();
			}
		}
	}

	private void SetInitialWeaponHandedToggle()
	{
		if (!(unitEditorManager == null))
		{
			switch (unitEditorManager.WeaponHandMode)
			{
			case UnitEditorManager.WeaponMode.OneHanded:
				oneHandedToggle.isOn = true;
				break;
			case UnitEditorManager.WeaponMode.TwoHanded:
				twoHandedToggle.isOn = true;
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}

	private void ToggleWeaponHandMode()
	{
		if (!(unitEditorManager == null))
		{
			switch (unitEditorManager.WeaponHandMode)
			{
			case UnitEditorManager.WeaponMode.OneHanded:
				twoHandedToggle.isOn = true;
				break;
			case UnitEditorManager.WeaponMode.TwoHanded:
				oneHandedToggle.isOn = true;
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}

	private void UpdateGlyphs()
	{
		if (stateManager is UnitEditorUIManager unitEditorUIManager)
		{
			UnitEditorGamepadGlyphs gamepadGlyphs = unitEditorUIManager.GamepadGlyphs;
			if (!(gamepadGlyphs == null))
			{
				gamepadGlyphs.UpdateActionNames("Back", "BUTTON_EXIT", UnitEditorGamepadGlyphs.Position.Left);
				gamepadGlyphs.UpdateActionNames("Toggle One/Two Handed", "BUTTON_TOGGLE", UnitEditorGamepadGlyphs.Position.Middle);
				gamepadGlyphs.UpdateActionNames("Flip Weapons", "LABEL_WEAPONFLIP", UnitEditorGamepadGlyphs.Position.Right);
			}
		}
	}
}
