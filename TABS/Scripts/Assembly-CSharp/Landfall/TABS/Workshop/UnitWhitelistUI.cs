using System;
using System.Collections;
using System.Collections.Generic;
using DM;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Landfall.TABS.Workshop
{
	public class UnitWhitelistUI : MonoBehaviour, IPlacementUI
	{
		public Transform units;

		public UILayoutGroup unitLayoutGroup;

		private UnitButton selectedUnitButton;

		private FactionButton selectedFactionButton;

		[SerializeField]
		private SimpleStateAnimation[] m_SimpleStateAnimations;

		[SerializeField]
		private GameObject m_ComingSoon;

		[SerializeField]
		private PlacementFactionBar factionBar;

		public ScenarioEditorFade fadeBG;

		private float mouseScrollDelta;

		private EventSystem eventSystem;

		private Faction[] m_UnitFactions;

		private List<DatabaseID> m_unitIDs;

		private DatabaseID m_lastSelectedUnit;

		private int m_lastSelectedUnitIndex;

		private DatabaseID m_lastSelectedFaction;

		private int m_numberOfFactions;

		private FactionCreatorUI m_FactionCreator;

		private BattleCreatorSaveUI m_SaveUI;

		private float m_FactionsXMin;

		private float m_FactionsXMax;

		private InputService m_InputService;

		public bool canCycleUnits;

		[SerializeField]
		private GameObject canCycleGlyphs;

		[SerializeField]
		private GameObject cantCycleGlyphs;

		private void Awake()
		{
			eventSystem = EventSystem.current;
			m_FactionCreator = UnityEngine.Object.FindObjectOfType<FactionCreatorUI>();
			m_InputService = ServiceLocator.GetService<InputService>();
		}

		public void Setup(Faction[] allowedFactions)
		{
			if (allowedFactions == null || allowedFactions.Length == 0)
			{
				m_UnitFactions = new Faction[0];
			}
			else
			{
				m_UnitFactions = allowedFactions;
			}
			m_unitIDs = new List<DatabaseID>();
			RedrawFactions();
		}

		private void OnEnable()
		{
			if (m_InputService != null)
			{
				m_InputService.InputChanged += InputChanged;
			}
		}

		private void OnDisable()
		{
			if (m_InputService != null)
			{
				m_InputService.InputChanged -= InputChanged;
			}
		}

		private void InputChanged(InputType inputType)
		{
			if (inputType - 1 <= InputType.Keyboard)
			{
				DeselectCurrentFactionButton();
			}
		}

		private void DeselectCurrentFactionButton()
		{
		}

		private void SetFirstUnit()
		{
			if (m_unitIDs.Count > 0)
			{
				m_lastSelectedUnit = m_unitIDs[0];
			}
			m_lastSelectedUnitIndex = 0;
		}

		public void ProcessInput(PlayerActions playerActions)
		{
			UnitButton unitButton = unitLayoutGroup.GetUnitButton(m_lastSelectedUnit);
			if (playerActions.m_cycleUnits.WasPressed && !playerActions.m_placementZoomActivate.IsPressed && m_unitIDs.Count > 0 && canCycleUnits)
			{
				if (unitButton != null)
				{
					unitButton.Deselect();
				}
				int num = Math.Sign(playerActions.m_cycleUnits.Value);
				m_lastSelectedUnitIndex = (m_lastSelectedUnitIndex + num) % m_unitIDs.Count;
				if (m_lastSelectedUnitIndex < 0)
				{
					m_lastSelectedUnitIndex = m_unitIDs.Count - 1;
				}
				m_lastSelectedUnit = m_unitIDs[m_lastSelectedUnitIndex];
				unitButton = unitLayoutGroup.GetUnitButton(m_lastSelectedUnit);
				if (unitButton == null)
				{
					if (num > 0)
					{
						int numMoveSpots = ((m_lastSelectedUnitIndex != 0) ? 1 : m_unitIDs.Count);
						bool increment = m_lastSelectedUnitIndex != 0;
						MoveUnitList(increment, numMoveSpots);
					}
					else
					{
						int num2 = m_unitIDs.Count - 1;
						int numMoveSpots2 = ((m_lastSelectedUnitIndex != num2) ? 1 : m_unitIDs.Count);
						bool increment2 = m_lastSelectedUnitIndex == num2;
						MoveUnitList(increment2, numMoveSpots2);
					}
					unitButton = unitLayoutGroup.GetUnitButton(m_lastSelectedUnit);
				}
				if (unitButton != null)
				{
					unitButton.ShowSelection();
				}
			}
			if (playerActions.m_toggleUnit.WasPressed && unitButton != null)
			{
				unitButton.ToggleWhitelist();
				m_SaveUI.UpdateUnitFilter();
			}
			if (playerActions.m_toggleUnitOrPageChange.WasPressed)
			{
				EnableUnitCycling(!canCycleUnits);
			}
			if (EventSystem.current.IsPointerOverGameObject())
			{
				mouseScrollDelta += Input.mouseScrollDelta.y;
				if (mouseScrollDelta > 1f)
				{
					unitLayoutGroup.MoveList();
					mouseScrollDelta -= 1f;
				}
				else if (mouseScrollDelta < -1f)
				{
					unitLayoutGroup.MoveList(increament: false);
					mouseScrollDelta += 1f;
				}
			}
		}

		private void EnableUnitCycling(bool enable)
		{
			canCycleUnits = enable;
			canCycleGlyphs.SetActive(enable);
			cantCycleGlyphs.SetActive(!enable);
		}

		private IEnumerator SelectFirst()
		{
			yield return null;
			yield return null;
			SelectFaction();
		}

		public void SetBattleCreatorSaveUI(BattleCreatorSaveUI saveUI)
		{
			m_SaveUI = saveUI;
		}

		public void RedrawFactions()
		{
			factionBar.Clear();
			factionBar.Spawn(m_UnitFactions);
		}

		public void MoveUnitList(bool increment, int numMoveSpots)
		{
			for (int i = 0; i < numMoveSpots; i++)
			{
				unitLayoutGroup.MoveList(increment);
			}
		}

		public void RedrawFactionUnits(int index, Faction[] unitFactions)
		{
			unitLayoutGroup.ClearElements();
			Faction faction = unitFactions[index];
			for (int i = 0; i < faction.Units.Length; i++)
			{
				UnitBlueprint unitBlueprint = faction.Units[i];
				if (!(unitBlueprint == null))
				{
					UnitBlueprint unitBlueprint2 = unitBlueprint;
					m_unitIDs.Add(unitBlueprint2.Entity.GUID);
					SpawnUnit(unitFactions[index].Units[i]);
				}
			}
		}

		private void SpawnUnit(UnitBlueprint unit, bool unlocked = true)
		{
			unitLayoutGroup.AddElement(new UnitButton.UnitButtonData(unit, this, unlocked));
		}

		public void Open()
		{
			UIBackIn();
			fadeBG.SetOn();
			EnableUnitCycling(enable: true);
		}

		public void Close()
		{
			RemoveUI();
			fadeBG.SetOff();
			EnableUnitCycling(enable: false);
			canCycleGlyphs.SetActive(value: false);
		}

		private void UIBackIn()
		{
			for (int i = 0; i < m_SimpleStateAnimations.Length; i++)
			{
				m_SimpleStateAnimations[i].SetState(SimpleStateAnimation.State.State02);
			}
		}

		private void RemoveUI()
		{
			for (int i = 0; i < m_SimpleStateAnimations.Length; i++)
			{
				m_SimpleStateAnimations[i].SetState(SimpleStateAnimation.State.State01);
			}
		}

		private bool SelectFaction()
		{
			return true;
		}

		public void RedrawFactionUnits(Faction faction)
		{
			m_unitIDs.Clear();
			unitLayoutGroup.ClearElements();
			if (faction.Units.Length == 0)
			{
				if ((bool)m_ComingSoon)
				{
					m_ComingSoon.SetActive(value: true);
				}
				return;
			}
			if ((bool)m_ComingSoon)
			{
				m_ComingSoon.SetActive(value: false);
			}
			UnitBlueprint[] array = faction.Units;
			foreach (UnitBlueprint unitBlueprint in array)
			{
				if (!(unitBlueprint == null) && ServiceLocator.GetService<ISaveLoaderService>().HasUnlockedSecret(unitBlueprint.Entity.UnlockKey))
				{
					UnitBlueprint unitBlueprint2 = unitBlueprint;
					m_unitIDs.Add(unitBlueprint2.Entity.GUID);
					DrawFactionUnit(unitBlueprint2);
				}
			}
		}

		public void DrawFactionUnit(UnitBlueprint unit)
		{
			bool unlocked = true;
			unitLayoutGroup.AddElement(new UnitButton.UnitButtonData(unit, this, unlocked));
		}

		public bool SelectUnit(DatabaseID id, UnitButton button)
		{
			if (selectedUnitButton != null)
			{
				selectedUnitButton.Deselect();
			}
			button.overrideSelection = true;
			if (m_SaveUI != null)
			{
				m_SaveUI.OnUnitClicked(id);
			}
			selectedUnitButton = button;
			m_SaveUI.UpdateUnitFilter();
			return true;
		}

		public bool SelectFaction(DatabaseID id, FactionButton button)
		{
			if ((bool)selectedFactionButton)
			{
				if (button == selectedFactionButton)
				{
					return false;
				}
				selectedFactionButton.Deselect();
			}
			button.overrideSelection = true;
			SelectFaction(id);
			selectedFactionButton = button;
			m_SaveUI.UpdateUnitFilter();
			m_lastSelectedFaction = id;
			selectedFactionButton.PopFactionTab();
			return true;
		}

		public bool WasLastSelectedFactionIndex(DatabaseID index)
		{
			return index == m_lastSelectedFaction;
		}

		private void SelectFaction(DatabaseID id)
		{
			Faction faction = ContentDatabase.Instance().GetFaction(id);
			RedrawFactionUnits(faction);
		}
	}
}
