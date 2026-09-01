using System;
using System.Collections;
using DM;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.WinConditions;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using LevelCreator;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS.UnitPlacement
{
	public class UnitPlacementBrush
	{
		private const float PREVENT_UNIT_PLACEMENT_TIME = 0.5f;

		private bool m_shouldUpdate;

		protected UnitBlueprint m_unitToSpawn;

		private GameStateManager m_gamestateManager;

		private BrushBehaviourBase m_brushBehaviour;

		private PlacementCursor m_cursor;

		private float m_targetSize = 1f;

		private float m_sizeVelocity;

		private float m_sizeSpring = 200f;

		private float m_sizeDampning = 0.8f;

		public float preventPlacementTimer;

		private SandboxPlacementCamera m_placementCamera;

		private GameSceneSequencer m_sequencer;

		private CursorController m_cursorController;

		private Vector3 m_lastCursorPosition;

		private Vector3 m_lastCursorCanvasPosition;

		private bool m_removedUnitThisInput;

		private int m_runPlacementRoutineCount;

		private MonoBehaviour m_parentService;

		private bool m_isRunningPlacementRoutine;

		private Team m_cursorPositionTeam;

		private GameObject m_hoveredStaticObject;

		private GameObject m_hoveredUnit;

		private INetworkQuitController m_quitController;

		private InputService m_inputService;

		private RuntimeReferenceService m_runtimeReferenceService;

		private SettingsProfileManager m_SettingsProfileManager;

		private MaxUnitsController m_MaxUnits;

		private bool wasInFreelookInPreviousUpdate;

		private bool needToUpFirst;

		private bool allowPlacement = true;

		public BrushBehaviourBase BrushBehaviour => m_brushBehaviour;

		public PlacementCursor Cursor => m_cursor;

		public bool IsRunningPlacementRoutine => m_isRunningPlacementRoutine;

		public Vector3 LastCursorCanvasPosition => m_lastCursorCanvasPosition;

		public GameModeService GameModeService { get; private set; }

		public GameObject HoveredStaticObject => m_hoveredStaticObject;

		public GameObject HoveredUnit => m_hoveredUnit;

		public UnitBlueprint UnitToSpawn => m_unitToSpawn;

		public bool CameraInFreelook
		{
			get
			{
				if (m_placementCamera != null)
				{
					return m_placementCamera.IsInFreeLook();
				}
				return false;
			}
		}

		public event Action<UnitBlueprint> SelectedUnitChanged;

		public event System.Action PlacementCursorChanged;

		public event Action<Unit> RemovedUnit;

		public event Action<PlacementCursorState> CursorStateChange;

		public UnitPlacementBrush(GameModeService parentService)
		{
			m_parentService = parentService;
			GameModeService = parentService;
			m_lastCursorCanvasPosition = Input.mousePosition;
			m_gamestateManager = ServiceLocator.GetService<GameStateManager>();
			m_cursorController = ServiceLocator.GetService<CursorController>();
		}

		public BrushBehaviourBase InitializeBrushWithType<T>() where T : BrushBehaviourBase
		{
			if (m_brushBehaviour != null)
			{
				m_brushBehaviour.OnDestroy();
			}
			T brushBehaviour = Activator.CreateInstance<T>();
			m_brushBehaviour = brushBehaviour;
			m_brushBehaviour.SetPlacementBrush(this);
			m_brushBehaviour.Initialize();
			m_shouldUpdate = true;
			m_inputService = ServiceLocator.GetService<InputService>();
			m_runtimeReferenceService = ServiceLocator.GetService<RuntimeReferenceService>();
			m_SettingsProfileManager = ServiceLocator.GetService<SettingsProfileManager>();
			m_MaxUnits = ServiceLocator.GetService<MaxUnitsController>();
			m_quitController = ServiceLocator.GetService<INetworkQuitController>();
			return m_brushBehaviour;
		}

		public BrushBehaviourBase InitializeBrushWithType(Type brushType)
		{
			if (!brushType.IsSubclassOf(typeof(BrushBehaviourBase)))
			{
				Debug.LogError("Type " + brushType.Name + " needs to be a subclass of BrushBehaviourBase.");
				return null;
			}
			if (m_brushBehaviour != null)
			{
				m_brushBehaviour.OnDestroy();
			}
			m_brushBehaviour = (BrushBehaviourBase)Activator.CreateInstance(brushType);
			m_brushBehaviour.SetPlacementBrush(this);
			m_brushBehaviour.Initialize();
			m_shouldUpdate = true;
			return m_brushBehaviour;
		}

		public void SetBrushUnit(DatabaseID id)
		{
			UnitBlueprint unitBlueprint = ContentDatabase.Instance().GetUnitBlueprint(id);
			if (m_cursor == null)
			{
				Debug.LogError("Brush Cursor was null at this point.");
			}
			if (unitBlueprint != null && m_cursor != null)
			{
				m_unitToSpawn = unitBlueprint;
				this.SelectedUnitChanged?.Invoke(m_unitToSpawn);
				m_cursor.ChangedUnitBrush(m_unitToSpawn);
			}
		}

		public void ClearBrushUnit()
		{
			m_unitToSpawn = null;
		}

		public void CursorStateChanged()
		{
			if (!(m_cursor == null))
			{
				this.CursorStateChange?.Invoke(m_cursor.State);
			}
		}

		public void PreventPlacementBuffer(float seconds = 0.5f)
		{
			preventPlacementTimer = seconds;
			allowPlacement = false;
		}

		public void BlockPlacement()
		{
			allowPlacement = false;
		}

		public void UnblockPlacement()
		{
			allowPlacement = true;
		}

		public bool RaycastMap(out Vector3 pos)
		{
			if (Physics.Raycast(m_sequencer.GetCameraRay(), out var hitInfo, float.PositiveInfinity, m_cursor.PrimaryActionMask))
			{
				pos = hitInfo.point;
				return true;
			}
			pos = Vector3.zero;
			return false;
		}

		public void SetPlacementCamera(SandboxPlacementCamera placementCamera)
		{
			m_placementCamera = placementCamera;
		}

		public void SetGameSceneSequencer(GameSceneSequencer sequencer)
		{
			m_sequencer = sequencer;
		}

		public void OnEnterPlacementState()
		{
			m_shouldUpdate = true;
		}

		public void OnEnterBattleState()
		{
			m_shouldUpdate = false;
		}

		public void ShouldUpdate(bool update)
		{
			m_shouldUpdate = update;
			m_placementCamera.AllowMovement(update);
		}

		public void UnlockFreeLookCameraMovement()
		{
			m_placementCamera.AllowMovement(allow: true);
		}

		public void BlockBrushRemove(bool value)
		{
			allowPlacement = value;
		}

		public void Update()
		{
			if (!m_shouldUpdate || m_cursor == null)
			{
				return;
			}
			if (m_brushBehaviour == null || m_placementCamera == null)
			{
				Debug.LogError("Failed to find brush behaviour. Cannot update brush for now. Skipping");
				return;
			}
			if (m_placementCamera == null)
			{
				Debug.LogError("Placement Camera is null, cannot update brush.");
			}
			bool flag = PlayerActions.Instance.InputType == InputType.Controller;
			bool flag2 = MapSettingsUI.IsOpen || UIMapSelector.IsOpen;
			if ((flag && flag2) || m_inputService.CurrentState == InputService.InputState.UI)
			{
				return;
			}
			if (m_placementCamera.IsInFreeLook() || m_placementCamera.IsReturning || m_placementCamera.IsResettingRotation)
			{
				wasInFreelookInPreviousUpdate = true;
				return;
			}
			if (wasInFreelookInPreviousUpdate)
			{
				wasInFreelookInPreviousUpdate = false;
				if (flag)
				{
					m_cursorController.CenterCursor();
				}
				m_cursor.SetCursorState(PlacementCursorState.DenyPlacement);
			}
			if (SceneSettings.IsEditingLine)
			{
				needToUpFirst = true;
				return;
			}
			if (needToUpFirst)
			{
				if (PlayerActions.Instance.m_placeUnit.WasReleased)
				{
					needToUpFirst = false;
				}
				return;
			}
			if (preventPlacementTimer > 0f && !allowPlacement)
			{
				preventPlacementTimer -= Time.unscaledDeltaTime;
				if (preventPlacementTimer <= 0f)
				{
					allowPlacement = true;
				}
			}
			Vector3 cursorPosition = m_cursorController.CursorPosition;
			Vector3.Distance(cursorPosition, m_lastCursorCanvasPosition);
			m_lastCursorCanvasPosition = cursorPosition;
			Ray cameraRay = m_sequencer.GetCameraRay();
			Vector3 vector = new Vector3(-999f, -999f, -999f);
			m_targetSize = 0.45f;
			bool flag3 = false;
			if (Physics.Raycast(cameraRay, out var hitInfo, float.PositiveInfinity, (int)m_cursor.PrimaryActionMask | (int)m_cursor.InvalidUnitPlacementMask) && !m_cursorController.IsPointerOverGameObject())
			{
				vector = hitInfo.point;
				GameObject gameObject = hitInfo.transform.gameObject;
				float num = Vector3.Angle(hitInfo.normal, Vector3.up);
				bool flag4 = Cursor.InvalidUnitPlacementMask.Contains(gameObject.layer);
				if (PlayerActions.Instance.m_placeUnit.IsPressed && allowPlacement && !flag4 && num <= Cursor.MaxPlacementAngleEuler)
				{
					m_brushBehaviour.PrimaryActionOnObject(gameObject, vector);
					flag3 = true;
				}
				m_brushBehaviour.HoveringStaticSceneObjects(gameObject, vector, hitInfo.normal);
			}
			if (Physics.Raycast(cameraRay, out var hitInfo2, float.PositiveInfinity, m_cursor.SecondaryActionMask) && !m_cursorController.IsPointerOverGameObject())
			{
				GameObject gameObject2 = hitInfo2.transform.root.gameObject;
				if (m_hoveredUnit == null || gameObject2 != m_hoveredUnit)
				{
					m_brushBehaviour.StartHoverDynamicObjects(gameObject2, hitInfo2.point);
					if (m_hoveredUnit != null)
					{
						m_brushBehaviour.EndHoverDynamicObject(m_hoveredUnit, hitInfo2.point);
					}
					m_hoveredUnit = gameObject2;
				}
				if (PlayerActions.Instance.m_removeUnit.IsPressed && allowPlacement && !flag3)
				{
					m_removedUnitThisInput = true;
					m_brushBehaviour.SecondaryActionOnObject(gameObject2, hitInfo2.point);
				}
				m_brushBehaviour.HoveringDynamicObjects(m_hoveredUnit, hitInfo2.point);
			}
			else if (m_hoveredUnit != null)
			{
				m_brushBehaviour.EndHoverDynamicObject(m_hoveredUnit, Vector3.zero);
				m_hoveredUnit = null;
			}
			Vector3 delta = vector - m_lastCursorPosition;
			if (delta.magnitude > float.Epsilon)
			{
				m_brushBehaviour?.BrushPositionDelta(m_lastCursorPosition, vector, delta);
				m_lastCursorPosition = vector;
			}
			m_cursorPositionTeam = m_brushBehaviour.GetTeamAreaAtPosition(vector, out var _);
		}

		public Team GetTeamAtCursorPosition()
		{
			return m_cursorPositionTeam;
		}

		private void FixedUpdate()
		{
			m_sizeVelocity *= m_sizeDampning;
		}

		public void SetCursor(PlacementCursor cursor)
		{
			m_cursor = cursor;
			this.PlacementCursorChanged?.Invoke();
		}

		public void PlaceLayoutUnitInternal(TABSCampaignLevelAsset.TABSLayoutUnit unit, Team team, bool addToLayout, Quaternion unitRotation, bool costsBudget = true)
		{
			if (unit != null)
			{
				PlaceUnitInternal(unit.m_unitBlueprint, team, unit.m_position, unitRotation, addToLayout, unit.RuntimeReference, costsBudget, unit.CampaignUnit);
			}
		}

		public void PlaceUnitInternal(UnitBlueprint blueprint, Team team, Vector3 position, Quaternion rotation, bool addToLayout, RuntimeReference runtimeReference, bool costsBudget = true, bool campaignUnit = false, bool checkUserPlacementMaxUnits = false, int martianInstanceId = 0)
		{
			if (blueprint == null)
			{
				return;
			}
			bool flag = CharacterItemExtensions.ShouldCustomContentBeValidated();
			if (blueprint.HasMissingComponents && flag)
			{
				ServiceLocator.GetService<ModalPanel>().PopUp("CUSTOM_CONTENT_VALIDATION_FAILED_UNIT_ADDITIONAL_INFO", null, -1f, false);
				return;
			}
			if (checkUserPlacementMaxUnits && GameModeService.CurrentGameMode.TeamLayouts != null && m_MaxUnits != null)
			{
				int? maxUnits = m_MaxUnits.GetMaxUnits();
				int num = ((!GameModeService.IsCurrentBaseGameModeType<CampaignGameMode>()) ? (m_MaxUnits.GetTeamUnitCount(team) + blueprint.FoodCost) : (m_MaxUnits.GetTeamUnitCount(team) + 1));
				if (maxUnits.HasValue && num > maxUnits.Value)
				{
					return;
				}
			}
			bool flag2 = !costsBudget || GameModeService.CurrentGameMode.BattleBudget.CanAfford(team, (int)blueprint.GetUnitCost());
			bool flag3 = ServiceLocator.GetService<DebugService>() != null && ServiceLocator.GetService<DebugService>().HasUnlimitedUnitPoints;
			if (costsBudget && flag2 && !flag3)
			{
				GameModeService.CurrentGameMode.BattleBudget.SpendAmount(team, (int)blueprint.GetUnitCost());
			}
			if (!(flag2 || flag3))
			{
				return;
			}
			if ((m_brushBehaviour != null) & (blueprint != null))
			{
				Unit unit = m_brushBehaviour.Place(blueprint, team, position, rotation, addToLayout, campaignUnit, runtimeReference, forMartianPlayer: false, spawnRiders: true, isMartian: false, martianInstanceId);
				if (unit.RuntimeReference == null)
				{
					unit.RuntimeReference = m_runtimeReferenceService.CreateReference(unit);
				}
				GameModeService.CurrentGameMode.OnUnitSpawned(unit, team);
				RiderHolder component = unit.GetComponent<RiderHolder>();
				if (!(component != null))
				{
					return;
				}
				for (int i = 0; i < component.riders.Count; i++)
				{
					Unit component2 = component.riders[i].GetComponent<Unit>();
					if (component2 != null)
					{
						if (component2.RuntimeReference == null)
						{
							component2.RuntimeReference = m_runtimeReferenceService.CreateReference(component2);
						}
						GameModeService.CurrentGameMode.OnUnitSpawned(component2, team);
					}
					else
					{
						Debug.LogError("Rider is missing its unit component, this should not happen.", component.riders[i]);
					}
				}
			}
			else
			{
				Debug.LogError("WARNING: Brush Behaviour or blueprint is null, One possible cause of this is from trying to load a custom unit from Mod.io that can not be found!");
			}
		}

		public void RemoveUnitInternal(Unit unit, Team team)
		{
			GameModeService.CurrentGameMode.BattleBudget.ReturnAmount(team, (int)unit.unitBlueprint.GetUnitCost());
			m_brushBehaviour.Remove(unit, team);
			this.RemovedUnit?.Invoke(unit);
			GameModeService.CurrentGameMode.OnUnitRemoved(unit, team);
			RiderHolder component = unit.GetComponent<RiderHolder>();
			if (!(component != null))
			{
				return;
			}
			for (int i = 0; i < component.riders.Count; i++)
			{
				Unit component2 = component.riders[i].GetComponent<Unit>();
				if (component2 != null)
				{
					GameModeService.CurrentGameMode.OnUnitRemoved(component2, team);
				}
				else
				{
					Debug.LogError("Rider is missing its unit component, this should not happen.", component.riders[i]);
				}
			}
		}

		public Coroutine PlaceLayout(TABSCampaignLevelAsset.TABSLayoutUnit[] layout, Team team, System.Action onDone = null)
		{
			return m_parentService.StartCoroutine(PlaceLayoutRoutine(layout, team, onDone));
		}

		private IEnumerator PlaceLayoutRoutine(TABSCampaignLevelAsset.TABSLayoutUnit[] layout, Team team, System.Action onDone)
		{
			BlockPlacement();
			m_isRunningPlacementRoutine = true;
			yield return new WaitForSeconds(0.2f);
			if (SpawnLevel.IsCustomLevelScene)
			{
				yield return new WaitUntil(() => SpawnLevel.GetSpawnLevelState == SpawnLevel.SpawnLevelState.Done);
			}
			bool mustPlaceUnits = true;
			if (m_quitController != null && ServiceLocator.GetService<GameModeService>().CurrentGameMode is OnlineMultiplayerGameMode)
			{
				if (m_quitController.DidQuit || m_quitController.DidOpponentQuit)
				{
					mustPlaceUnits = false;
				}
				else
				{
					yield return new WaitForSeconds(1f);
					if (m_quitController.DidQuit || m_quitController.DidOpponentQuit)
					{
						mustPlaceUnits = false;
					}
				}
			}
			if (mustPlaceUnits)
			{
				int len = layout.Length;
				ServiceLocator.GetService<RuntimeReferenceService>();
				for (int i = 0; i < len; i++)
				{
					TABSCampaignLevelAsset.TABSLayoutUnit tABSLayoutUnit = layout[i];
					float y = (Mathf.Approximately(tABSLayoutUnit.Rotation, 0f) ? ((float)((team == Team.Red) ? 90 : 270)) : tABSLayoutUnit.Rotation);
					Quaternion unitRotation = Quaternion.Euler(0f, y, 0f);
					bool flag = CharacterItemExtensions.ShouldCustomContentBeValidated();
					if (tABSLayoutUnit.m_unitBlueprint.HasMissingComponents && flag)
					{
						ServiceLocator.GetService<ModalPanel>().PopUp("CUSTOM_CONTENT_VALIDATION_FAILED_ADDITIONAL_INFO", ReturnToCustomContentPage, -1f, false);
						yield break;
					}
					m_brushBehaviour.PlaceLayoutUnit(tABSLayoutUnit, team, unitRotation);
					yield return null;
				}
			}
			onDone?.Invoke();
			m_isRunningPlacementRoutine = false;
			UnblockPlacement();
		}

		private void ReturnToCustomContentPage()
		{
			TABSSceneManager.LoadCustomContentPage();
		}
	}
}
