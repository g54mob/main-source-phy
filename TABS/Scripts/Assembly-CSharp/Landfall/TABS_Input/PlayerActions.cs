using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BitCode.Users;
using InControl;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS_Input
{
	public class PlayerActions : PlayerActionSet
	{
		private static PlayerActions _instance;

		public PlayerAction m_anybuttonpressed;

		public PlayerAction m_toggleSlowMotion;

		public PlayerAction m_slowMotion;

		public PlayerAction m_toggleSuperSlow;

		public PlayerAction m_toggleTime;

		public PlayerAction m_moveFast;

		public PlayerAction m_openUnitRadialMenu;

		public PlayerAction m_placeUnit;

		public PlayerAction m_removeUnit;

		public PlayerAction m_clearUnits;

		public PlayerAction m_toggleFreeCam;

		public PlayerAction m_enterExitBattle;

		public PlayerAction m_moveLeft;

		public PlayerAction m_moveRight;

		public PlayerAction m_moveForward;

		public PlayerAction m_moveBackward;

		public PlayerAction m_moveAnyDirection;

		public PlayerTwoAxisAction m_move;

		public PlayerAction m_moveUp;

		public PlayerAction m_moveDown;

		public PlayerOneAxisAction m_moveVertical;

		public PlayerAction m_placementZoomIn;

		public PlayerAction m_placementZoomOut;

		public PlayerAction m_placementZoomActivate;

		public PlayerOneAxisAction m_placementZoom;

		public PlayerAction m_centerCursor;

		public PlayerAction m_centerCameraPositionAndZoom;

		public PlayerAction m_possessToggle;

		public PlayerAction m_possessAttack1;

		public PlayerAction m_possessAttack2;

		public PlayerAction m_possessAttack3;

		public PlayerAction m_possessToggleCamera;

		public PlayerAction m_aimLeft;

		public PlayerAction m_aimRight;

		public PlayerAction m_aimUp;

		public PlayerAction m_aimDown;

		public PlayerTwoAxisAction m_aim;

		public PlayerAction m_cycleUnitsUp;

		public PlayerAction m_cycleUnitsDown;

		public PlayerOneAxisAction m_cycleUnits;

		public PlayerAction m_cycleTabsUp;

		public PlayerAction m_cycleTabsDown;

		public PlayerOneAxisAction m_cycleTabs;

		public PlayerAction m_openFactionSelection;

		public PlayerAction m_resetFactions;

		public PlayerAction m_menu;

		public PlayerAction m_options;

		public PlayerAction m_utility;

		public PlayerAction m_mapSettings;

		public PlayerAction m_mapSelect;

		public PlayerAction m_moveCampaignCreatorBattle;

		public PlayerAction m_back;

		public PlayerAction m_accept;

		public PlayerAction m_ScenarioEditor;

		public PlayerAction m_cogGridButton;

		public PlayerAction m_resetSettings;

		public PlayerAction m_applySettings;

		public PlayerAction m_toggleTab;

		public PlayerAction m_upload;

		public PlayerAction m_clearFilter;

		public PlayerAction m_refresh;

		public PlayerAction m_showInput;

		public PlayerAction m_placementShowInput;

		public PlayerAction m_limitUnits;

		public PlayerAction m_acceptSwitchLeftBumper;

		public PlayerAction m_acceptSwitchRightBumper;

		public PlayerAction m_pageLeft;

		public PlayerAction m_pageRight;

		public PlayerAction m_changeUser;

		public PlayerAction m_reorderBattleUp;

		public PlayerAction m_reorderBattleDown;

		public PlayerOneAxisAction m_reorderBattles;

		public PlayerAction m_editLine;

		public PlayerAction m_swapTeamLine;

		public PlayerAction m_cycleLineType;

		public PlayerAction m_finishEditLine;

		public PlayerAction m_resetTeamLine;

		public PlayerAction m_uiUp;

		public PlayerAction m_uiDown;

		public PlayerAction m_uiLeft;

		public PlayerAction m_uiRight;

		public PlayerOneAxisAction m_uiNavigationVertical;

		public PlayerOneAxisAction m_uiNavigationHorizontal;

		public PlayerTwoAxisAction m_uiNavigation;

		public PlayerAction m_CycleAbilitiesLeft;

		public PlayerAction m_CycleAbilitiesRight;

		public PlayerAction m_CycleAbilityTypes;

		public PlayerAction m_CycleAbilityModes;

		public PlayerAction m_CanTriggerTypeChange;

		public PlayerAction m_TriggerAbility;

		public PlayerAction m_RemoveVictoryConditions;

		public PlayerAction m_EditVictoryConditions;

		public PlayerAction m_AddVictoryConditions;

		public PlayerAction m_AddVictoryConditionsFromInspector;

		public PlayerAction m_editorUndo;

		public PlayerAction m_editorRedo;

		public PlayerAction m_shiftAlternate;

		public PlayerAction m_altAlternate;

		public PlayerAction m_controlAlternate;

		public PlayerAction m_hazardSlowMotion;

		public PlayerAction m_increaseRadius;

		public PlayerAction m_decreaseRadius;

		public PlayerAction m_menuSelect;

		public PlayerAction m_playmode;

		public PlayerAction m_toolMenu;

		public PlayerAction m_itemMenu;

		public PlayerAction m_flyUp;

		public PlayerAction m_flyDown;

		public PlayerAction m_showCursor;

		public PlayerAction m_quickSave;

		public PlayerAction m_radialCycleLeft;

		public PlayerAction m_radialCycleRight;

		public PlayerAction m_radialCycleThemeLeft;

		public PlayerAction m_radialCycleThemeRight;

		public PlayerAction m_radialCycleSubSelectionLeft;

		public PlayerAction m_radialCycleSubSelectionRight;

		public PlayerAction m_openGrid;

		public PlayerAction m_closeGrid;

		public PlayerAction m_cycleGridCategoryLeft;

		public PlayerAction m_cycleGridCategoryRight;

		public PlayerAction m_scrollToNextGroup;

		public PlayerAction m_scrollToPreviousGroup;

		public PlayerAction m_invokeHotbar;

		public PlayerAction m_cycleHotbarLeft;

		public PlayerAction m_cycleHotbarRight;

		public PlayerAction m_cycleHotbarCategoryLeft;

		public PlayerAction m_cycleHotbarCategoryRight;

		public PlayerAction m_toolPrimary;

		public PlayerAction m_toolSecondary;

		public PlayerAction m_toolSpecial1;

		public PlayerAction m_toolSpecial2;

		public PlayerAction m_toolSpecial3;

		public PlayerAction m_toolSpecial4;

		public PlayerAction m_toolSpecial5;

		public PlayerAction m_toolSpecial6;

		public PlayerAction m_toolSpecial7;

		public PlayerAction m_toolSpecial8;

		public PlayerAction m_toolSpecial9;

		public PlayerAction m_toolToggleSliders;

		public PlayerAction m_toolConnectTriggers;

		public PlayerAction m_toolRotateLeft;

		public PlayerAction m_toolRotateRight;

		public PlayerAction m_toolScaleUp;

		public PlayerAction m_toolScaleDown;

		public PlayerAction m_scaleModifier;

		public PlayerAction m_addThumbnail;

		public PlayerAction m_saveCustomContent;

		public PlayerAction m_toggleTeamColours;

		public PlayerAction m_removeEquippedItem;

		public PlayerAction m_toggleOneTwoHandedWeapons;

		public PlayerAction m_editUnitName;

		public PlayerAction m_previewUnitVoice;

		public PlayerAction m_flipWeaponSlots;

		public PlayerAction m_itemSelectSearch;

		public PlayerAction m_toggleUnitCost;

		public PlayerAction m_editRider;

		public PlayerAction m_removeRider;

		public PlayerAction m_discardChanges;

		public PlayerAction m_deleteContent;

		public PlayerAction m_newContent;

		public PlayerAction m_toggleDownloadTab;

		public PlayerAction m_newLineModifier;

		public PlayerAction m_sendInvite;

		public PlayerAction m_showProfile;

		public PlayerAction m_editFactionIcon;

		public PlayerAction m_editFactionColor;

		public PlayerAction m_editRadialMenuFaction;

		public PlayerAction m_addRemoveRadialMenuFaction;

		public PlayerAction m_resetRadialMenuEditing;

		public PlayerAction m_toggleUnitOrPageChange;

		public PlayerAction m_toggleUnit;

		private InputType m_inputType;

		private IPlayerPrefsPlatform m_PlayerPrefs;

		private WaitForStorage m_WaitForStorage;

		private AccountManager m_AccountManager;

		private BindingSourceType assumedBindingTypeAtInit;

		public static PlayerActions Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = CreateWithDefaultBindings();
					_instance.LoadBindings();
				}
				return _instance;
			}
		}

		public InputType InputType
		{
			get
			{
				if (m_inputType == InputType.Any)
				{
					return GetInputType(_instance.LastInputType);
				}
				return m_inputType;
			}
		}

		public static string GetCurrentBinds(PlayerAction action)
		{
			ReadOnlyCollection<BindingSource> bindings = action.Bindings;
			List<BindingSource> list = new List<BindingSource>();
			for (int i = 0; i < bindings.Count; i++)
			{
				if (bindings[i].BindingSourceType == BindingSourceType.KeyBindingSource || bindings[i].BindingSourceType == BindingSourceType.MouseBindingSource)
				{
					list.Add(bindings[i]);
				}
			}
			string text = string.Empty;
			for (int j = 0; j < list.Count; j++)
			{
				text += list[j].Name;
				if (j < list.Count - 1)
				{
					text += " or ";
				}
			}
			return text;
		}

		public PlayerActions()
		{
			m_PlayerPrefs = ServiceLocator.GetService<IPlayerPrefsPlatform>();
			m_WaitForStorage = ServiceLocator.GetService<WaitForStorage>();
			m_AccountManager = ServiceLocator.GetService<AccountManager>();
			if (m_AccountManager != null)
			{
				m_AccountManager.ActiveAccountChanged += OnActiveAccountChanged;
			}
			m_anybuttonpressed = CreatePlayerAction("Any Button Pressed");
			m_menu = CreatePlayerAction("Menu");
			m_options = CreatePlayerAction("Options");
			m_utility = CreatePlayerAction("Utility");
			m_mapSettings = CreatePlayerAction("INPUT_MAP_SETTINGS");
			m_mapSelect = CreatePlayerAction("INPUT_MAP_SELECTION");
			m_editLine = CreatePlayerAction("Edit Line");
			m_swapTeamLine = CreatePlayerAction("Swap Team Line");
			m_cycleLineType = CreatePlayerAction("Cycle Line Type");
			m_finishEditLine = CreatePlayerAction("INPUT_FINISH_LINE_EDIT");
			m_resetTeamLine = CreatePlayerAction("Reset Line");
			m_resetSettings = CreatePlayerAction("Reset Settings");
			m_applySettings = CreatePlayerAction("Apply Settings");
			m_back = CreatePlayerAction("Back");
			m_accept = CreatePlayerAction("Accept");
			m_refresh = CreatePlayerAction("BUTTON_REFRESH");
			m_acceptSwitchLeftBumper = CreatePlayerAction("Accept Switch Left Bumper");
			m_acceptSwitchRightBumper = CreatePlayerAction("Accept Switch Right Bumper");
			m_showInput = CreatePlayerAction("Show Input");
			m_placementShowInput = CreatePlayerAction("Show Input (in game)");
			m_ScenarioEditor = CreatePlayerAction("Scenario Editor");
			m_cogGridButton = CreatePlayerAction("Cog Grid Button");
			m_toggleTab = CreatePlayerAction("Toggle Tab");
			m_limitUnits = CreatePlayerAction("Limit Units");
			m_upload = CreatePlayerAction("Upload");
			m_clearFilter = CreatePlayerAction("Clear Filter");
			m_moveCampaignCreatorBattle = CreatePlayerAction("Move Campaign Creator Battle");
			m_reorderBattleUp = CreatePlayerAction("Reorder Battle Up");
			m_reorderBattleDown = CreatePlayerAction("Reorder Battle Down");
			m_reorderBattles = CreateOneAxisPlayerAction(m_reorderBattleDown, m_reorderBattleUp);
			m_changeUser = CreatePlayerAction("Change User");
			m_uiUp = CreatePlayerAction("Up");
			m_uiDown = CreatePlayerAction("Down");
			m_uiLeft = CreatePlayerAction("Left");
			m_uiRight = CreatePlayerAction("Right");
			m_uiNavigationVertical = CreateOneAxisPlayerAction(m_uiDown, m_uiUp);
			m_uiNavigationHorizontal = CreateOneAxisPlayerAction(m_uiLeft, m_uiRight);
			m_uiNavigation = CreateTwoAxisPlayerAction(m_uiLeft, m_uiRight, m_uiDown, m_uiUp);
			m_pageLeft = CreatePlayerAction("Page Left");
			m_pageRight = CreatePlayerAction("Page Right");
			m_slowMotion = CreatePlayerAction("INPUT_SLOW");
			m_toggleSlowMotion = CreatePlayerAction("INPUT_TOGGLE_SLOW");
			m_toggleSuperSlow = CreatePlayerAction("INPUT_TOGGLE_SUPER_SLOW");
			m_toggleTime = CreatePlayerAction("INPUT_TOGGLE_PAUSE_TIME");
			m_moveFast = CreatePlayerAction("INPUT_MOVE_CAMERA_FAST");
			m_toggleFreeCam = CreatePlayerAction("INPUT_TOGGLE_FREECAM");
			m_enterExitBattle = CreatePlayerAction("INPUT_EXIT_BATTLE");
			m_placeUnit = CreatePlayerAction("INPUT_PLACE_UNIT");
			m_removeUnit = CreatePlayerAction("INPUT_REMOVE_UNIT");
			m_clearUnits = CreatePlayerAction("INPUT_CLEAR_UNITS");
			m_openUnitRadialMenu = CreatePlayerAction("INPUT_TOGGLE_RADIAL");
			m_moveLeft = CreatePlayerAction("INPUT_MOVE_LEFT");
			m_moveRight = CreatePlayerAction("INPUT_MOVE_RIGHT");
			m_moveForward = CreatePlayerAction("INPUT_MOVE_FORWARD");
			m_moveBackward = CreatePlayerAction("INPUT_MOVE_BACKWARD");
			m_moveAnyDirection = CreatePlayerAction("INPUT_MOVE_ANYDIRECTION");
			m_move = CreateTwoAxisPlayerAction(m_moveLeft, m_moveRight, m_moveBackward, m_moveForward);
			m_moveUp = CreatePlayerAction("INPUT_MOVE_UP");
			m_moveDown = CreatePlayerAction("INPUT_MOVE_DOWN");
			m_moveVertical = CreateOneAxisPlayerAction(m_moveDown, m_moveUp);
			m_placementZoomIn = CreatePlayerAction("INPUT_ZOOM_IN");
			m_placementZoomOut = CreatePlayerAction("INPUT_ZOOM_OUT");
			m_placementZoomActivate = CreatePlayerAction("Placement Zoom Activate");
			m_placementZoom = CreateOneAxisPlayerAction(m_placementZoomOut, m_placementZoomIn);
			m_centerCursor = CreatePlayerAction("Center Cursor");
			m_centerCameraPositionAndZoom = CreatePlayerAction("Center Camera Position and Zoom");
			m_possessToggle = CreatePlayerAction("INPUT_POSSESS");
			m_possessAttack1 = CreatePlayerAction("INPUT_POSSESS_ATTACK_1");
			m_possessAttack2 = CreatePlayerAction("INPUT_POSSESS_ATTACK_2");
			m_possessAttack3 = CreatePlayerAction("INPUT_POSSESS_ATTACK_3");
			m_possessToggleCamera = CreatePlayerAction("INPUT_POSSES_CAMERA");
			m_aimLeft = CreatePlayerAction("INPUT_AIM_LEFT");
			m_aimRight = CreatePlayerAction("INPUT_AIM_RIGHT");
			m_aimUp = CreatePlayerAction("INPUT_AIM_UP");
			m_aimDown = CreatePlayerAction("INPUT_AIM_DOWN");
			m_aim = CreateTwoAxisPlayerAction(m_aimLeft, m_aimRight, m_aimDown, m_aimUp);
			m_cycleUnitsUp = CreatePlayerAction("Cycle Units Up");
			m_cycleUnitsDown = CreatePlayerAction("Cycle Units Down");
			m_cycleUnits = CreateOneAxisPlayerAction(m_cycleUnitsDown, m_cycleUnitsUp);
			m_cycleTabsUp = CreatePlayerAction("Cycle Tabs Up");
			m_cycleTabsDown = CreatePlayerAction("Cycle Tabs Down");
			m_cycleTabs = CreateOneAxisPlayerAction(m_cycleTabsDown, m_cycleTabsUp);
			m_openFactionSelection = CreatePlayerAction("Open Faction Selection");
			m_resetFactions = CreatePlayerAction("INPUT_RESET_FACTIONS");
			m_CycleAbilitiesLeft = CreatePlayerAction("INPUT_BUGS_CYCLE_LEFT");
			m_CycleAbilitiesRight = CreatePlayerAction("INPUT_BUGS_CYCLE_RIGHT");
			m_CycleAbilityTypes = CreatePlayerAction("INPUT_BUGS_CYCLE_TYPE");
			m_CycleAbilityModes = CreatePlayerAction("INPUT_BUGS_CYCLE_MODE");
			m_TriggerAbility = CreatePlayerAction("INPUT_BUGS_TRIGGER");
			m_CanTriggerTypeChange = CreatePlayerAction("INPUT_BUGS_TRIGGER_MODE");
			m_RemoveVictoryConditions = CreatePlayerAction("INPUT_WINCON_REMOVE");
			m_EditVictoryConditions = CreatePlayerAction("INPUT_WINCON_EDIT");
			m_AddVictoryConditions = CreatePlayerAction("INPUT_WINCON_ADD");
			m_AddVictoryConditionsFromInspector = CreatePlayerAction("INPUT_WINCON_ADD_INSPECTOR");
			m_saveCustomContent = CreatePlayerAction("Save Custom Content");
			m_toggleTeamColours = CreatePlayerAction("Toggle Team Colours");
			m_removeEquippedItem = CreatePlayerAction("Remove Equipped");
			m_toggleOneTwoHandedWeapons = CreatePlayerAction("Toggle One/Two Handed");
			m_editUnitName = CreatePlayerAction("Edit Name");
			m_previewUnitVoice = CreatePlayerAction("Preview Unit Voice");
			m_flipWeaponSlots = CreatePlayerAction("Flip Weapons");
			m_itemSelectSearch = CreatePlayerAction("Search Items");
			m_toggleUnitCost = CreatePlayerAction("Toggle Unit Cost");
			m_editRider = CreatePlayerAction("Edit Rider");
			m_removeRider = CreatePlayerAction("Remove Rider");
			m_discardChanges = CreatePlayerAction("Discard Changes");
			m_deleteContent = CreatePlayerAction("Delete Content");
			m_newContent = CreatePlayerAction("New Content");
			m_toggleDownloadTab = CreatePlayerAction("INPUT_TOGGLE_DOWNLOAD_TAB");
			m_newLineModifier = CreatePlayerAction("New Line Modifier");
			m_sendInvite = CreatePlayerAction("INPUT_SEND_INVITE");
			m_showProfile = CreatePlayerAction("INPUT_SHOW_PROFILE");
			m_editFactionIcon = CreatePlayerAction("Edit Faction Icon");
			m_editFactionColor = CreatePlayerAction("Edit Faction Color");
			m_editRadialMenuFaction = CreatePlayerAction("Edit Radial Menu Faction");
			m_addRemoveRadialMenuFaction = CreatePlayerAction("Add Remove Radial Menu Faction");
			m_resetRadialMenuEditing = CreatePlayerAction("Reset Radial Menu");
			m_toggleUnitOrPageChange = CreatePlayerAction("INPUT_TOGGLE_UNIT_OR_PAGE_CHANGE");
			m_toggleUnit = CreatePlayerAction("INPUT_TOGGLE_UNIT");
			m_editorUndo = CreatePlayerAction("Editor Undo");
			m_editorRedo = CreatePlayerAction("Editor Redo");
			m_shiftAlternate = CreatePlayerAction("Shift Alternate");
			m_altAlternate = CreatePlayerAction("Alt Alternate");
			m_controlAlternate = CreatePlayerAction("Control Alternate");
			m_hazardSlowMotion = CreatePlayerAction("Hazard Slow Motion");
			m_increaseRadius = CreatePlayerAction("Increase Radius");
			m_decreaseRadius = CreatePlayerAction("Decrease Radius");
			m_menuSelect = CreatePlayerAction("Menu Select");
			m_playmode = CreatePlayerAction("Playmode");
			m_toolMenu = CreatePlayerAction("Tool Menu");
			m_itemMenu = CreatePlayerAction("Item Menu");
			m_flyUp = CreatePlayerAction("Fly Up");
			m_flyDown = CreatePlayerAction("Fly Down");
			m_showCursor = CreatePlayerAction("Show Cursor");
			m_quickSave = CreatePlayerAction("Quick Save");
			m_radialCycleLeft = CreatePlayerAction("Radial Cycle Left");
			m_radialCycleRight = CreatePlayerAction("Radial Cycle Right");
			m_radialCycleThemeLeft = CreatePlayerAction("Raidal Cycle Theme Left");
			m_radialCycleThemeRight = CreatePlayerAction("Raidal Cycle Theme Right");
			m_radialCycleSubSelectionLeft = CreatePlayerAction("Radial Cycle Sub Seletion Left");
			m_radialCycleSubSelectionRight = CreatePlayerAction("Radial Cycle Sub Selection Right");
			m_openGrid = CreatePlayerAction("Open Grid");
			m_closeGrid = CreatePlayerAction("Close Grid");
			m_cycleGridCategoryLeft = CreatePlayerAction("Cycle Grid Category Left");
			m_cycleGridCategoryRight = CreatePlayerAction("Cycle Grid Category Right");
			m_scrollToNextGroup = CreatePlayerAction("Scroll To Next Group");
			m_scrollToPreviousGroup = CreatePlayerAction("Scroll To Previous Action");
			m_invokeHotbar = CreatePlayerAction("Invoke Hotbar");
			m_cycleHotbarRight = CreatePlayerAction("Cycle Hotbar Right");
			m_cycleHotbarLeft = CreatePlayerAction("Cycle Hotbar Left");
			m_cycleHotbarCategoryLeft = CreatePlayerAction("Cycle Hotbar Category Left");
			m_cycleHotbarCategoryRight = CreatePlayerAction("Cycle Hotbar Category Right");
			m_toolPrimary = CreatePlayerAction("Tool Primary");
			m_toolSecondary = CreatePlayerAction("Tool Secondary");
			m_toolSpecial1 = CreatePlayerAction("Tool Special 1");
			m_toolSpecial2 = CreatePlayerAction("Tool Special 2");
			m_toolSpecial3 = CreatePlayerAction("Tool Special 3");
			m_toolSpecial4 = CreatePlayerAction("Tool Special 4");
			m_toolSpecial5 = CreatePlayerAction("Tool Special 5");
			m_toolSpecial6 = CreatePlayerAction("Tool Special 6");
			m_toolSpecial7 = CreatePlayerAction("Tool Special 7");
			m_toolSpecial8 = CreatePlayerAction("Tool Special 8");
			m_toolSpecial9 = CreatePlayerAction("Tool Special 9");
			m_toolToggleSliders = CreatePlayerAction("Tool Toggle Sliders");
			m_toolConnectTriggers = CreatePlayerAction("Tool Connect Triggers");
			m_toolRotateLeft = CreatePlayerAction("Tool Rotate Left");
			m_toolRotateRight = CreatePlayerAction("Tool Rotate Right");
			m_toolScaleUp = CreatePlayerAction("Tool Scale Up");
			m_toolScaleDown = CreatePlayerAction("Tool Scale Down");
			m_scaleModifier = CreatePlayerAction("Scale Modifier");
			m_addThumbnail = CreatePlayerAction("Add Thumbnail");
		}

		~PlayerActions()
		{
			if (m_AccountManager != null)
			{
				m_AccountManager.ActiveAccountChanged -= OnActiveAccountChanged;
			}
		}

		public static PlayerActions CreateWithDefaultBindings(bool allowKeyboardAndMouse = true)
		{
			PlayerActions playerActions = new PlayerActions();
			InputControlType control = InputControlType.Action1;
			InputControlType control2 = InputControlType.Action2;
			playerActions.m_menu.AddDefaultBinding(Key.Escape);
			DefaultMenuControllerBindings(playerActions);
			playerActions.assumedBindingTypeAtInit = ((!allowKeyboardAndMouse) ? BindingSourceType.DeviceBindingSource : BindingSourceType.KeyBindingSource);
			foreach (InputControlType value in Enum.GetValues(typeof(InputControlType)))
			{
				playerActions.m_anybuttonpressed.AddDefaultBinding(value);
			}
			playerActions.m_accept.AddDefaultBinding(control);
			playerActions.m_accept.AddDefaultBinding(Key.Return);
			playerActions.m_upload.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_clearFilter.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_cogGridButton.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_limitUnits.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_resetSettings.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_applySettings.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_toggleTab.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_showInput.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_placementShowInput.AddDefaultBinding(InputControlType.DPadUp);
			playerActions.m_moveCampaignCreatorBattle.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_reorderBattleUp.AddDefaultBinding(InputControlType.RightStickUp);
			playerActions.m_reorderBattleDown.AddDefaultBinding(InputControlType.RightStickDown);
			playerActions.m_changeUser.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_uiUp.AddDefaultBinding(InputControlType.DPadUp);
			playerActions.m_uiDown.AddDefaultBinding(InputControlType.DPadDown);
			playerActions.m_uiLeft.AddDefaultBinding(InputControlType.DPadLeft);
			playerActions.m_uiRight.AddDefaultBinding(InputControlType.DPadRight);
			playerActions.m_uiUp.AddDefaultBinding(InputControlType.LeftStickUp);
			playerActions.m_uiDown.AddDefaultBinding(InputControlType.LeftStickDown);
			playerActions.m_uiLeft.AddDefaultBinding(InputControlType.LeftStickLeft);
			playerActions.m_uiRight.AddDefaultBinding(InputControlType.LeftStickRight);
			playerActions.m_sendInvite.AddDefaultBinding(control);
			playerActions.m_showProfile.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_pageLeft.AddDefaultBinding(InputControlType.LeftTrigger);
			playerActions.m_pageRight.AddDefaultBinding(InputControlType.RightTrigger);
			playerActions.m_acceptSwitchLeftBumper.AddDefaultBinding(InputControlType.LeftBumper);
			playerActions.m_acceptSwitchRightBumper.AddDefaultBinding(InputControlType.RightBumper);
			playerActions.m_back.AddDefaultBinding(control2);
			playerActions.m_back.AddDefaultBinding(Key.Escape);
			playerActions.m_refresh.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_slowMotion.AddDefaultBinding(InputControlType.LeftBumper);
			playerActions.m_toggleSlowMotion.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_toggleSuperSlow.AddDefaultBinding(control);
			playerActions.m_toggleTime.AddDefaultBinding(control2);
			playerActions.m_moveFast.AddDefaultBinding(Key.LeftShift);
			playerActions.m_moveFast.AddDefaultBinding(InputControlType.LeftStickButton);
			playerActions.m_openUnitRadialMenu.AddDefaultBinding(InputControlType.LeftBumper);
			playerActions.m_openUnitRadialMenu.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_placeUnit.AddDefaultBinding(control);
			playerActions.m_placeUnit.AddDefaultBinding(InputControlType.RightBumper);
			playerActions.m_removeUnit.AddDefaultBinding(control2);
			playerActions.m_clearUnits.AddDefaultBinding(control2);
			playerActions.m_cycleUnitsUp.AddDefaultBinding(InputControlType.RightTrigger);
			playerActions.m_cycleUnitsDown.AddDefaultBinding(InputControlType.LeftTrigger);
			playerActions.m_cycleTabsUp.AddDefaultBinding(InputControlType.RightBumper);
			playerActions.m_cycleTabsDown.AddDefaultBinding(InputControlType.LeftBumper);
			playerActions.m_openFactionSelection.AddDefaultBinding(InputControlType.DPadDown);
			playerActions.m_resetFactions.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_toggleFreeCam.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_enterExitBattle.AddDefaultBinding(Key.Tab);
			playerActions.m_moveLeft.AddDefaultBinding(InputControlType.LeftStickLeft);
			playerActions.m_moveRight.AddDefaultBinding(InputControlType.LeftStickRight);
			playerActions.m_moveForward.AddDefaultBinding(InputControlType.LeftStickUp);
			playerActions.m_moveBackward.AddDefaultBinding(InputControlType.LeftStickDown);
			playerActions.m_moveAnyDirection.AddDefaultBinding(InputControlType.Action5);
			playerActions.m_moveUp.AddDefaultBinding(InputControlType.RightTrigger);
			playerActions.m_moveDown.AddDefaultBinding(InputControlType.LeftTrigger);
			playerActions.m_moveDown.AddDefaultBinding(Key.LeftControl);
			playerActions.m_placementZoomActivate.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_placementZoomIn.AddDefaultBinding(InputControlType.RightTrigger);
			playerActions.m_placementZoomOut.AddDefaultBinding(InputControlType.LeftTrigger);
			playerActions.m_centerCursor.AddDefaultBinding(InputControlType.RightStickButton);
			playerActions.m_centerCameraPositionAndZoom.AddDefaultBinding(InputControlType.LeftStickButton);
			playerActions.m_aimLeft.AddDefaultBinding(InputControlType.RightStickLeft);
			playerActions.m_aimRight.AddDefaultBinding(InputControlType.RightStickRight);
			playerActions.m_aimUp.AddDefaultBinding(InputControlType.RightStickUp);
			playerActions.m_aimDown.AddDefaultBinding(InputControlType.RightStickDown);
			playerActions.m_possessToggle.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_possessToggle.AddDefaultBinding(InputControlType.RightBumper);
			playerActions.m_possessAttack1.AddDefaultBinding(InputControlType.LeftTrigger);
			playerActions.m_possessAttack2.AddDefaultBinding(InputControlType.RightTrigger);
			playerActions.m_possessAttack3.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_possessToggleCamera.AddDefaultBinding(InputControlType.LeftStickButton);
			playerActions.m_options.AddDefaultBinding(InputControlType.DPadRight);
			playerActions.m_mapSettings.AddDefaultBinding(InputControlType.DPadLeft);
			playerActions.m_mapSelect.AddDefaultBinding(InputControlType.DPadRight);
			playerActions.m_ScenarioEditor.AddDefaultBinding(InputControlType.DPadLeft);
			playerActions.m_CycleAbilitiesLeft.AddDefaultBinding(Key.LessThan);
			playerActions.m_CycleAbilitiesRight.AddDefaultBinding(Key.GreaterThan);
			playerActions.m_CycleAbilityModes.AddDefaultBinding(Mouse.MiddleButton);
			playerActions.m_TriggerAbility.AddDefaultBinding(Mouse.RightButton);
			playerActions.m_CycleAbilitiesLeft.AddDefaultBinding(InputControlType.DPadLeft);
			playerActions.m_CycleAbilitiesRight.AddDefaultBinding(InputControlType.DPadRight);
			playerActions.m_CycleAbilityModes.AddDefaultBinding(InputControlType.DPadUp);
			playerActions.m_CycleAbilityTypes.AddDefaultBinding(InputControlType.DPadUp);
			playerActions.m_TriggerAbility.AddDefaultBinding(InputControlType.DPadDown);
			playerActions.m_CanTriggerTypeChange.AddDefaultBinding(InputControlType.RightStickButton);
			playerActions.m_editLine.AddDefaultBinding(control);
			playerActions.m_swapTeamLine.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_cycleLineType.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_resetTeamLine.AddDefaultBinding(InputControlType.RightStickButton);
			playerActions.ListenOptions.IncludeUnknownControllers = true;
			playerActions.ListenOptions.MaxAllowedBindings = 0u;
			playerActions.ListenOptions.AllowDuplicateBindingsPerSet = true;
			playerActions.ListenOptions.UnsetDuplicateBindingsOnSet = false;
			playerActions.ListenOptions.IncludeMouseButtons = true;
			playerActions.ListenOptions.IncludeModifiersAsFirstClassKeys = true;
			playerActions.ListenOptions.IncludeMouseScrollWheel = true;
			playerActions.m_RemoveVictoryConditions.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_EditVictoryConditions.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_AddVictoryConditions.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_AddVictoryConditionsFromInspector.AddDefaultBinding(control);
			playerActions.m_saveCustomContent.AddDefaultBinding(InputControlType.Start);
			playerActions.m_saveCustomContent.AddDefaultBinding(InputControlType.Menu);
			playerActions.m_saveCustomContent.AddDefaultBinding(InputControlType.Plus);
			playerActions.m_toggleTeamColours.AddDefaultBinding(InputControlType.RightStickButton);
			playerActions.m_removeEquippedItem.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_toggleOneTwoHandedWeapons.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_editUnitName.AddDefaultBinding(InputControlType.LeftStickButton);
			playerActions.m_previewUnitVoice.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_flipWeaponSlots.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_itemSelectSearch.AddDefaultBinding(InputControlType.LeftStickButton);
			playerActions.m_toggleUnitCost.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_editRider.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_removeRider.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_discardChanges.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_deleteContent.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_newContent.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_toggleDownloadTab.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_newLineModifier.AddDefaultBinding(Key.LeftShift);
			playerActions.m_editFactionIcon.AddDefaultBinding(InputControlType.Action3);
			playerActions.m_editFactionColor.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_editRadialMenuFaction.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_addRemoveRadialMenuFaction.AddDefaultBinding(control);
			playerActions.m_resetRadialMenuEditing.AddDefaultBinding(InputControlType.RightStickButton);
			playerActions.m_toggleUnitOrPageChange.AddDefaultBinding(InputControlType.Action4);
			playerActions.m_toggleUnit.AddDefaultBinding(InputControlType.Action3);
			if (allowKeyboardAndMouse)
			{
				playerActions.m_back.AddDefaultBinding(Key.Escape);
				playerActions.m_toggleSuperSlow.AddDefaultBinding(Key.G);
				playerActions.m_toggleTime.AddDefaultBinding(Key.T);
				playerActions.m_moveFast.AddDefaultBinding(Key.LeftShift);
				playerActions.m_toggleFreeCam.AddDefaultBinding(Key.F);
				playerActions.m_enterExitBattle.AddDefaultBinding(Key.Tab);
				playerActions.m_moveLeft.AddDefaultBinding(Key.A);
				playerActions.m_moveRight.AddDefaultBinding(Key.D);
				playerActions.m_moveForward.AddDefaultBinding(Key.W);
				playerActions.m_moveBackward.AddDefaultBinding(Key.S);
				playerActions.m_moveLeft.AddDefaultBinding(Key.LeftArrow);
				playerActions.m_moveRight.AddDefaultBinding(Key.RightArrow);
				playerActions.m_moveForward.AddDefaultBinding(Key.UpArrow);
				playerActions.m_moveBackward.AddDefaultBinding(Key.DownArrow);
				playerActions.m_moveUp.AddDefaultBinding(Key.E);
				playerActions.m_moveUp.AddDefaultBinding(Key.Space);
				playerActions.m_moveDown.AddDefaultBinding(Key.Q);
				playerActions.m_moveDown.AddDefaultBinding(Key.LeftControl);
				playerActions.m_possessToggle.AddDefaultBinding(Key.F);
				playerActions.m_possessAttack1.AddDefaultBinding(Mouse.LeftButton);
				playerActions.m_possessAttack2.AddDefaultBinding(Mouse.RightButton);
				playerActions.m_possessAttack3.AddDefaultBinding(Key.Space);
				playerActions.m_possessToggleCamera.AddDefaultBinding(Key.C);
				playerActions.m_slowMotion.AddDefaultBinding(Mouse.LeftButton);
				playerActions.m_placeUnit.AddDefaultBinding(Mouse.LeftButton);
				playerActions.m_removeUnit.AddDefaultBinding(Mouse.RightButton);
				playerActions.m_placementZoomIn.AddDefaultBinding(Mouse.PositiveScrollWheel);
				playerActions.m_placementZoomOut.AddDefaultBinding(Mouse.NegativeScrollWheel);
				playerActions.m_aimLeft.AddDefaultBinding(Mouse.NegativeX);
				playerActions.m_aimRight.AddDefaultBinding(Mouse.PositiveX);
				playerActions.m_aimUp.AddDefaultBinding(Mouse.PositiveY);
				playerActions.m_aimDown.AddDefaultBinding(Mouse.NegativeY);
				playerActions.m_possessAttack2.AddDefaultBinding(Mouse.RightButton);
				playerActions.m_possessAttack1.AddDefaultBinding(Mouse.LeftButton);
				playerActions.m_finishEditLine.AddDefaultBinding(Mouse.LeftButton);
				playerActions.m_inputType = InputType.Any;
				playerActions.m_editorUndo.AddDefaultBinding(Key.Z);
				playerActions.m_editorUndo.AddDefaultBinding(InputControlType.None);
				playerActions.m_editorRedo.AddDefaultBinding(Key.Y);
				playerActions.m_editorRedo.AddDefaultBinding(InputControlType.None);
				playerActions.m_shiftAlternate.AddDefaultBinding(Key.Shift);
				playerActions.m_shiftAlternate.AddDefaultBinding(InputControlType.LeftStickButton);
				playerActions.m_altAlternate.AddDefaultBinding(default(Key));
				playerActions.m_altAlternate.AddDefaultBinding(InputControlType.None);
				playerActions.m_controlAlternate.AddDefaultBinding(default(Key));
				playerActions.m_controlAlternate.AddDefaultBinding(InputControlType.None);
				playerActions.m_hazardSlowMotion.AddDefaultBinding(Key.T);
				playerActions.m_hazardSlowMotion.AddDefaultBinding(InputControlType.Action3);
				playerActions.m_increaseRadius.AddDefaultBinding(Mouse.PositiveScrollWheel);
				playerActions.m_increaseRadius.AddDefaultBinding(InputControlType.None);
				playerActions.m_decreaseRadius.AddDefaultBinding(Mouse.NegativeScrollWheel);
				playerActions.m_decreaseRadius.AddDefaultBinding(InputControlType.None);
				playerActions.m_menuSelect.AddDefaultBinding(Mouse.LeftButton);
				playerActions.m_menuSelect.AddDefaultBinding(control);
				playerActions.m_playmode.AddDefaultBinding(Key.P);
				playerActions.m_playmode.AddDefaultBinding(InputControlType.None);
				playerActions.m_toolMenu.AddDefaultBinding(default(Key));
				playerActions.m_toolMenu.AddDefaultBinding(InputControlType.None);
				playerActions.m_itemMenu.AddDefaultBinding(default(Key));
				playerActions.m_itemMenu.AddDefaultBinding(InputControlType.None);
				playerActions.m_flyUp.AddDefaultBinding(Key.Space);
				playerActions.m_flyUp.AddDefaultBinding(InputControlType.RightTrigger);
				playerActions.m_flyDown.AddDefaultBinding(Key.Control);
				playerActions.m_flyDown.AddDefaultBinding(InputControlType.LeftTrigger);
				playerActions.m_showCursor.AddDefaultBinding(Key.Alt);
				playerActions.m_quickSave.AddDefaultBinding(Key.F5);
				playerActions.m_radialCycleLeft.AddDefaultBinding(Key.Q);
				playerActions.m_radialCycleLeft.AddDefaultBinding(InputControlType.None);
				playerActions.m_radialCycleRight.AddDefaultBinding(Key.E);
				playerActions.m_radialCycleRight.AddDefaultBinding(InputControlType.None);
				playerActions.m_radialCycleThemeLeft.AddDefaultBinding(Key.Key1);
				playerActions.m_radialCycleThemeLeft.AddDefaultBinding(InputControlType.LeftBumper);
				playerActions.m_radialCycleThemeRight.AddDefaultBinding(Key.Key2);
				playerActions.m_radialCycleThemeRight.AddDefaultBinding(InputControlType.RightBumper);
				playerActions.m_radialCycleSubSelectionLeft.AddDefaultBinding(Key.Key3);
				playerActions.m_radialCycleSubSelectionLeft.AddDefaultBinding(InputControlType.Action3);
				playerActions.m_radialCycleSubSelectionRight.AddDefaultBinding(Key.Key4);
				playerActions.m_radialCycleSubSelectionRight.AddDefaultBinding(InputControlType.Action4);
				playerActions.m_openGrid.AddDefaultBinding(Key.B);
				playerActions.m_openGrid.AddDefaultBinding(InputControlType.DPadUp);
				playerActions.m_closeGrid.AddDefaultBinding(Key.B);
				playerActions.m_closeGrid.AddDefaultBinding(control2);
				playerActions.m_cycleGridCategoryLeft.AddDefaultBinding(Key.Q);
				playerActions.m_cycleGridCategoryLeft.AddDefaultBinding(InputControlType.LeftBumper);
				playerActions.m_cycleGridCategoryRight.AddDefaultBinding(Key.E);
				playerActions.m_cycleGridCategoryRight.AddDefaultBinding(InputControlType.RightBumper);
				playerActions.m_scrollToNextGroup.AddDefaultBinding(InputControlType.RightTrigger);
				playerActions.m_scrollToPreviousGroup.AddDefaultBinding(InputControlType.LeftTrigger);
				playerActions.m_invokeHotbar.AddDefaultBinding(Key.F);
				playerActions.m_invokeHotbar.AddDefaultBinding(InputControlType.Action3);
				playerActions.m_cycleHotbarLeft.AddDefaultBinding(Mouse.PositiveScrollWheel);
				playerActions.m_cycleHotbarLeft.AddDefaultBinding(InputControlType.DPadLeft);
				playerActions.m_cycleHotbarRight.AddDefaultBinding(Mouse.NegativeScrollWheel);
				playerActions.m_cycleHotbarRight.AddDefaultBinding(InputControlType.DPadRight);
				playerActions.m_cycleHotbarCategoryLeft.AddDefaultBinding(Key.Q);
				playerActions.m_cycleHotbarCategoryLeft.AddDefaultBinding(InputControlType.LeftBumper);
				playerActions.m_cycleHotbarCategoryRight.AddDefaultBinding(Key.E);
				playerActions.m_cycleHotbarCategoryRight.AddDefaultBinding(InputControlType.RightBumper);
				playerActions.m_toolPrimary.AddDefaultBinding(Mouse.LeftButton);
				playerActions.m_toolPrimary.AddDefaultBinding(control);
				playerActions.m_toolSecondary.AddDefaultBinding(Mouse.RightButton);
				playerActions.m_toolSecondary.AddDefaultBinding(control2);
				playerActions.m_toolSpecial1.AddDefaultBinding(Key.E);
				playerActions.m_toolSpecial1.AddDefaultBinding(InputControlType.Action3);
				playerActions.m_toolSpecial2.AddDefaultBinding(Mouse.MiddleButton);
				playerActions.m_toolSpecial2.AddDefaultBinding(InputControlType.Action4);
				playerActions.m_toolSpecial3.AddDefaultBinding(Key.V);
				playerActions.m_toolSpecial3.AddDefaultBinding(InputControlType.DPadDown);
				playerActions.m_toolToggleSliders.AddDefaultBinding(Key.R);
				playerActions.m_toolToggleSliders.AddDefaultBinding(InputControlType.RightStickButton);
				playerActions.m_toolConnectTriggers.AddDefaultBinding(Key.R);
				playerActions.m_toolConnectTriggers.AddDefaultBinding(InputControlType.RightStickButton);
				playerActions.m_toolRotateLeft.AddDefaultBinding(Key.Q);
				playerActions.m_toolRotateLeft.AddDefaultBinding(InputControlType.LeftBumper);
				playerActions.m_toolRotateRight.AddDefaultBinding(Key.E);
				playerActions.m_toolRotateRight.AddDefaultBinding(InputControlType.RightBumper);
				playerActions.m_toolScaleUp.AddDefaultBinding(Key.Q);
				playerActions.m_toolScaleUp.AddDefaultBinding(InputControlType.LeftBumper);
				playerActions.m_toolScaleDown.AddDefaultBinding(Key.E);
				playerActions.m_toolScaleDown.AddDefaultBinding(InputControlType.RightBumper);
				playerActions.m_scaleModifier.AddDefaultBinding(Key.LeftShift);
				playerActions.m_scaleModifier.AddDefaultBinding(InputControlType.Action4);
				playerActions.m_toolSpecial4.AddDefaultBinding(Key.V);
				playerActions.m_toolSpecial5.AddDefaultBinding(Key.F);
				playerActions.m_toolSpecial6.AddDefaultBinding(Key.G);
				playerActions.m_toolSpecial7.AddDefaultBinding(Key.X);
				playerActions.m_toolSpecial8.AddDefaultBinding(Key.T);
				playerActions.m_toolSpecial9.AddDefaultBinding(Key.Y);
				playerActions.m_addThumbnail.AddDefaultBinding(InputControlType.Action3);
			}
			return playerActions;
		}

		private static void DefaultMenuControllerBindings(PlayerActions playerActions)
		{
			playerActions.m_enterExitBattle.AddDefaultBinding(InputControlType.View);
			playerActions.m_utility.AddDefaultBinding(InputControlType.View);
			playerActions.m_menu.AddDefaultBinding(InputControlType.Menu);
			playerActions.m_enterExitBattle.AddDefaultBinding(InputControlType.Back);
			playerActions.m_utility.AddDefaultBinding(InputControlType.Back);
			playerActions.m_menu.AddDefaultBinding(InputControlType.Start);
			playerActions.m_menu.AddDefaultBinding(InputControlType.Options);
			playerActions.m_enterExitBattle.AddDefaultBinding(InputControlType.TouchPadButton);
			playerActions.m_utility.AddDefaultBinding(InputControlType.TouchPadButton);
			playerActions.m_menu.AddDefaultBinding(InputControlType.Select);
			playerActions.m_enterExitBattle.AddDefaultBinding(InputControlType.Minus);
			playerActions.m_utility.AddDefaultBinding(InputControlType.Minus);
			playerActions.m_menu.AddDefaultBinding(InputControlType.Plus);
		}

		public void SaveBindings(bool savePlayerPrefsImmediately)
		{
			string value = Save();
			m_PlayerPrefs.SetString("WithControllerBindings", value);
			if (savePlayerPrefsImmediately)
			{
				m_PlayerPrefs.Save();
			}
		}

		public void LoadBindings()
		{
			if (m_WaitForStorage != null)
			{
				m_WaitForStorage.FireWhenReady(OnLoadBindings);
			}
		}

		private void OnActiveAccountChanged(ILocalAccount account)
		{
			LoadBindings();
		}

		private void OnLoadBindings()
		{
			if (m_PlayerPrefs.HasKey("WithControllerBindings"))
			{
				string data = m_PlayerPrefs.GetString("WithControllerBindings");
				Load(data);
			}
			else if (m_PlayerPrefs.HasKey("Bindings"))
			{
				string data2 = m_PlayerPrefs.GetString("Bindings");
				Load(data2);
				MigrateBindingsWithControllerBindings();
			}
		}

		private void MigrateBindingsWithControllerBindings()
		{
			PlayerActions playerActions = CreateWithDefaultBindings();
			PlayerActions playerActions2 = new PlayerActions();
			int count = _instance.Actions.Count;
			for (int i = 0; i < count; i++)
			{
				PlayerAction playerAction = playerActions.Actions[i];
				PlayerAction playerAction2 = playerActions2.Actions[i];
				foreach (BindingSource binding in playerAction.Bindings)
				{
					playerAction.RemoveBinding(binding);
					playerAction2.AddDefaultBinding(binding);
				}
			}
			foreach (PlayerAction action in playerActions2.Actions)
			{
				action.ClearBindings();
			}
			for (int j = 0; j < count; j++)
			{
				PlayerAction playerAction3 = _instance.Actions[j];
				PlayerAction playerAction4 = playerActions2.Actions[j];
				foreach (BindingSource binding2 in playerAction3.Bindings)
				{
					playerAction3.RemoveBinding(binding2);
					playerAction4.AddBinding(binding2);
				}
			}
			_instance.Destroy();
			playerActions.Destroy();
			_instance = playerActions2;
			_instance.m_inputType = InputType.Any;
			DefaultMenuControllerBindings(_instance);
			string value = _instance.Save();
			m_PlayerPrefs.SetString("WithControllerBindings", value);
		}

		public InputType GetInputType(BindingSourceType bindingSource)
		{
			switch (bindingSource)
			{
			case BindingSourceType.None:
			case BindingSourceType.KeyBindingSource:
			case BindingSourceType.MouseBindingSource:
				return InputType.Keyboard;
			case BindingSourceType.DeviceBindingSource:
			case BindingSourceType.UnknownDeviceBindingSource:
				return InputType.Controller;
			default:
				return InputType.Keyboard;
			}
		}

		public BindingSourceType GetAssumedBindingSourceAtInit()
		{
			return assumedBindingTypeAtInit;
		}

		public static string ControlToKey(string controlName, Type controlType)
		{
			return "IN_CONTROL_" + controlType.Name + "_" + controlName.Replace(" ", string.Empty);
		}

		public static string ControlToKey(string controlName, BindingSourceType bindingSourceType)
		{
			Type type = null;
			switch (bindingSourceType)
			{
			case BindingSourceType.DeviceBindingSource:
				type = typeof(InputControlType);
				break;
			case BindingSourceType.KeyBindingSource:
				type = typeof(Key);
				break;
			case BindingSourceType.MouseBindingSource:
				type = typeof(Mouse);
				break;
			case BindingSourceType.UnknownDeviceBindingSource:
				type = typeof(InputControlType);
				break;
			default:
				type = typeof(InputControlType);
				break;
			case BindingSourceType.None:
				break;
			}
			if (type == null)
			{
				Debug.LogError("Unrecognized binding type: " + Enum.GetName(typeof(BindingSourceType), bindingSourceType));
				return null;
			}
			return ControlToKey(controlName, type);
		}

		public static string GetLocalizedBinding(string controlName, BindingSourceType bindingSourceType)
		{
			return Localizer.GetSinglePhrase(ControlToKey(controlName, bindingSourceType));
		}
	}
}
