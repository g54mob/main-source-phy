using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InControl;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace LevelCreator
{
	public class DMEditor : MonoBehaviour
	{
		public enum StartState
		{
			New = 0,
			Edit = 1,
			Return = 2
		}

		public enum InputMode
		{
			Game = 0,
			UIOnly = 1
		}

		public enum ControllerMode
		{
			mouseCursor = 0,
			firstPersonMovement = 1
		}

		public enum VisualTargetMode
		{
			None = 0,
			Sphere = 1,
			Dot = 2,
			Crosshair = 3,
			Hand = 4,
			HandClosed = 5
		}

		private enum SnapObjectsMode
		{
			relaxed = 0,
			forced = 1
		}

		private const int ThumbnailJpgQuality = 85;

		[Header("General Settings")]
		[SerializeField]
		private string m_defaultToolGuid = string.Empty;

		private GameObject m_defaultTool;

		private GameObject m_currentTool;

		public float rayDistance = 100f;

		public PostProcessVolume postProcessVolume;

		[SerializeField]
		private MapAsset m_levelMap;

		private static string m_levelToLoadOnStart = "";

		public UnityEvent OnLevelLoaded = new UnityEvent();

		[Header("Editor Level")]
		[SerializeField]
		private Volume m_volumePrefab;

		public ContextInfoMenu contextInfoMenu;

		public Camera playerCamera;

		public ControllerManager playerController;

		public GameObject water;

		[Header("Sun and Lighting")]
		public Transform m_sun;

		public Light m_directionalLight;

		[ColorUsage(false, true)]
		[SerializeField]
		private Color m_daySkyColor = Color.black;

		[ColorUsage(false, true)]
		[SerializeField]
		private Color m_dayEquatorColor = Color.black;

		[ColorUsage(false, true)]
		[SerializeField]
		private Color m_dayGroundColor = Color.black;

		[Space]
		[ColorUsage(false, true)]
		[SerializeField]
		private Color m_nightSkyColor = Color.black;

		[ColorUsage(false, true)]
		[SerializeField]
		private Color m_nightEquatorColor = Color.black;

		[ColorUsage(false, true)]
		[SerializeField]
		private Color m_nightGroundColor = Color.black;

		[Header("Tables")]
		public ToolTable toolTable;

		public VolumeBrushTable brushTable;

		public DMEditorObjectTable editorObjectTable;

		[SerializeField]
		private DMEditorObjectTable m_editorObjectTableTriggerables;

		[SerializeField]
		private DMEditorObjectTable m_editorObjectTableEffects;

		public SeedCollectionTable seedTable;

		[Header("UI")]
		public CanvasRenderer playerCanvasRenderer;

		public Transform gridCanvas;

		[SerializeField]
		private LevelSettings m_levelSettingsMenu;

		[SerializeField]
		private LevelPresetData m_defaultPreset;

		[SerializeField]
		private PopUp m_popUpPrefab;

		[SerializeField]
		private Canvas m_popUpCanvasPrefab;

		public ToolBar toolBar;

		public ToolControlsBuilder toolControlsBuilder;

		public GlyphService glyphService;

		[Header("Escape Menu")]
		[SerializeField]
		private GameObject m_escapeCanvas;

		[SerializeField]
		private GameObject m_escapeParent;

		[SerializeField]
		private Button m_escapeSettingsButton;

		[Space]
		private bool m_showCursor = true;

		private bool m_currentShowCursor = true;

		private Level.Settings m_levelSettings = new Level.Settings();

		private Level.Scene m_levelScene = new Level.Scene();

		private Level.Volume m_levelVolume = new Level.Volume();

		private static DMEditorState m_editorState = new DMEditorState();

		public static bool LevelWasDirtyWhenEnteredPlayMode;

		private Level.Scene m_newScene = new Level.Scene();

		private Level.Volume m_newVolume = new Level.Volume();

		[HideInInspector]
		public UnityEvent undo = new UnityEvent();

		public static InputState inputState = new InputState("DMEditorInputState");

		private static DMEditor internalInstance;

		private bool takeLevelSnapshot;

		private bool blocksGridMenu;

		private IApplicationStateTracker stateTracker;

		private static PlayerAction saveAction;

		private static PlayerAction loadAction;

		private Dictionary<Guid, DMEditorComponent> existingEditorObjects = new Dictionary<Guid, DMEditorComponent>();

		private GameObject m_activeVisualTarget;

		[Header("Visual Targets")]
		[SerializeField]
		private GameObject m_sphere;

		[SerializeField]
		private GameObject m_dot;

		[SerializeField]
		private GameObject m_crosshair;

		[SerializeField]
		private GameObject m_hand;

		[SerializeField]
		private GameObject m_handClosed;

		private Dictionary<DMEditorComponent, DateTime> objectsToSnap = new Dictionary<DMEditorComponent, DateTime>();

		private List<DMEditorComponent> objectsWithNewCustomData = new List<DMEditorComponent>();

		private readonly List<Component> cachedEditorObjectComponents = new List<Component>();

		public static LevelPresetData CurrentPreset { get; private set; }

		public static bool BlockEscapeMenu { get; private set; }

		public GameObject LevelRootObject { get; private set; }

		public Volume VolumeRootObject { get; private set; }

		public GameObject Actions { get; private set; }

		public GameObject Preview { get; private set; }

		public LevelSettings LevelSettingsMenu
		{
			get
			{
				return m_levelSettingsMenu;
			}
			set
			{
				m_levelSettingsMenu = value;
			}
		}

		public Level.Settings LevelSettings
		{
			get
			{
				return m_levelSettings;
			}
			set
			{
				m_levelSettings = value;
			}
		}

		public static DMEditor Instance => internalInstance;

		public static StartState startState { get; private set; }

		public InputMode currentInputMode { get; private set; }

		public event Action<int, string> SettingMusicToPlay;

		public event System.Action OnEnterMenu;

		private void AssertionCheck()
		{
		}

		private void Awake()
		{
			AssertionCheck();
			internalInstance = this;
			LeanTween.init(2000);
			CampaignPlayerDataHolder.StartedPlayingSandbox();
			InputManager.ClearInputStates();
			AssignInputState();
			HideCursor();
			UpdateSkybox();
			PlayerActions.Instance.OnLastInputTypeChanged += OnLastInputTypeChanged;
			PopUp.InitPopupSystem(m_popUpPrefab, m_popUpCanvasPrefab);
			if (startState == StartState.New)
			{
				Utility.DelayAction(this, delegate
				{
					DMUIManager.Instance.OpenPanel(DMUIManager.UIPanels.StartMenu);
				});
			}
			stateTracker = ServiceLocator.GetService<IApplicationStateTracker>();
		}

		private void OnEnable()
		{
			if (stateTracker != null)
			{
				stateTracker.OnApplicationSuspended += EnterEscapeMenu;
			}
		}

		private void OnDisable()
		{
			if (stateTracker != null)
			{
				stateTracker.OnApplicationSuspended -= EnterEscapeMenu;
			}
		}

		private void OnApplicationFocus(bool focus)
		{
			Utility.DelayAction(this, delegate
			{
				UpdateInputMode();
			}, 5);
		}

		private void OnLastInputTypeChanged(BindingSourceType obj)
		{
			if (PlayerActions.Instance.InputType == InputType.Controller)
			{
				HideCursor();
			}
			else if (currentInputMode == InputMode.UIOnly)
			{
				ShowCursor();
			}
		}

		private void OnDestroy()
		{
			PlayerActions.Instance.OnLastInputTypeChanged -= OnLastInputTypeChanged;
			BlockEscapeMenu = false;
			LeanTween.cancelAll();
			InputService service = ServiceLocator.GetService<InputService>();
			if ((bool)service)
			{
				service.ResetStateCounter();
			}
		}

		private void AssignInputState()
		{
			PlayerActions instance = PlayerActions.Instance;
			inputState.ClearAllEvents();
			inputState.AddOnKeyDownListener(instance.m_editorUndo, delegate
			{
				Undo();
			});
			inputState.AddOnKeyDownListener(instance.m_editorRedo, delegate
			{
				Redo();
			});
			inputState.AddOnKeyDownListener(instance.m_playmode, delegate
			{
				LoadLevelScene();
			});
			inputState.AddOnKeyDownListener(instance.m_quickSave, delegate
			{
				QuickSave();
			});
			inputState.AddOnKeyDownListener(instance.m_openGrid, delegate
			{
				if (CanOpenGridMenu())
				{
					toolBar.SwitchHotbar(0);
					Utility.DelayAction(this, delegate
					{
						if (!(this == null) && !(m_currentTool == null))
						{
							PlacementTool component = m_currentTool.GetComponent<PlacementTool>();
							if (!(component == null) && component.currentState != null)
							{
								component.currentState.OnRadialMenu();
							}
						}
					});
				}
			});
			inputState.AddOnKeyDownListener(instance.m_enterExitBattle, delegate
			{
				DMUIManager.Instance.OpenPanel(DMUIManager.UIPanels.TopBar);
			});
			inputState.AddOnKeyDownListener(instance.m_menu, delegate
			{
				EnterEscapeMenu();
			});
			m_escapeSettingsButton.onClick.AddListener(delegate
			{
				m_escapeCanvas.GetComponentInChildren<CanvasToggle>(includeInactive: true).SetCanvasToggle(turnCanvasOn: true);
			});
			InputManager.PushState(inputState);
		}

		private bool CanOpenGridMenu()
		{
			return !blocksGridMenu;
		}

		private void Start()
		{
			LevelRootObject = new GameObject("Level");
			LevelRootObject.transform.parent = base.transform;
			VolumeRootObject = UnityEngine.Object.Instantiate(m_volumePrefab, base.transform);
			Actions = new GameObject("Actions");
			Actions.transform.parent = base.transform;
			Preview = new GameObject("Preview");
			Preview.transform.parent = base.transform;
			Vector3Int noOfVoxels = Level.VoxelChunk.noOfCells * Level.chunkCount;
			VolumeData volumeData = new VolumeData();
			volumeData.Init(noOfVoxels);
			for (int i = 0; i < Level.chunkCount.z; i++)
			{
				for (int j = 0; j < Level.chunkCount.y; j++)
				{
					for (int k = 0; k < Level.chunkCount.x; k++)
					{
						Level.VoxelChunk voxelChunk = new Level.VoxelChunk
						{
							densities = new float[Level.VoxelChunk.noOfCells.x + 1, Level.VoxelChunk.noOfCells.y + 1, Level.VoxelChunk.noOfCells.z + 1],
							version = 0
						};
						Vector3Int key = new Vector3Int(k * Level.VoxelChunk.noOfCells.x, j * Level.VoxelChunk.noOfCells.y, i * Level.VoxelChunk.noOfCells.z);
						for (int l = 0; l <= Level.VoxelChunk.noOfCells.z; l++)
						{
							for (int m = 0; m <= Level.VoxelChunk.noOfCells.y; m++)
							{
								for (int n = 0; n <= Level.VoxelChunk.noOfCells.x; n++)
								{
									int num = n + key.x;
									int num2 = m + key.y;
									int num3 = l + key.z;
									voxelChunk.densities[l, m, n] = ((num >= 0 && num2 >= 0 && num3 >= 0 && num < volumeData.voxels.GetLength(2) - 1 && num2 < volumeData.voxels.GetLength(1) - 1 && num3 < volumeData.voxels.GetLength(0) - 1) ? volumeData.voxels[num3, num2, num] : 0f);
								}
							}
						}
						Level.MaterialChunk materialChunk = new Level.MaterialChunk
						{
							densities = new float[Level.MaterialChunk.noOfCells.z + 1, Level.MaterialChunk.noOfCells.y + 1, Level.MaterialChunk.noOfCells.x + 1],
							version = 0
						};
						for (int num4 = 0; num4 <= Level.MaterialChunk.noOfCells.z; num4++)
						{
							for (int num5 = 0; num5 <= Level.MaterialChunk.noOfCells.y; num5++)
							{
								for (int num6 = 0; num6 <= Level.MaterialChunk.noOfCells.x; num6++)
								{
									materialChunk.densities[num4, num5, num6] = 0f;
								}
							}
						}
						Level.FoliageChunk foliageChunk = new Level.FoliageChunk
						{
							densities = new float[Level.FoliageChunk.noOfCells.z + 1, Level.FoliageChunk.noOfCells.y + 1, Level.FoliageChunk.noOfCells.x + 1],
							version = 0
						};
						for (int num7 = 0; num7 <= Level.FoliageChunk.noOfCells.z; num7++)
						{
							for (int num8 = 0; num8 <= Level.FoliageChunk.noOfCells.y; num8++)
							{
								for (int num9 = 0; num9 <= Level.FoliageChunk.noOfCells.x; num9++)
								{
									foliageChunk.densities[num7, num8, num9] = 0f;
								}
							}
						}
						m_levelVolume.volumeChunks.Add(key, new Level.VolumeChunk
						{
							voxelChunk = voxelChunk,
							materialChunk = materialChunk,
							foliageChunk = foliageChunk
						});
					}
				}
			}
			toolBar.BuildCategoryHotbar();
			toolBar.BuildSubHotbars();
			MergeObjectTables();
			SwitchToDefaultTool();
			UpdateLevel();
			SetPreset(m_defaultPreset);
			UIScreenInputBlocker.DoBlockInput(open: false);
			if (startState == StartState.Edit || startState == StartState.Return)
			{
				LoadLevel(startState, m_levelToLoadOnStart);
				SetLevelToLoadOnStart(string.Empty);
			}
			Time.timeScale = 1f;
		}

		public static void SetStartState(StartState startState)
		{
			DMEditor.startState = startState;
		}

		public static void SetLevelToLoadOnStart(string levelPath)
		{
			m_levelToLoadOnStart = levelPath;
		}

		private static void Swap<T>(ref T a, ref T b)
		{
			T val = a;
			a = b;
			b = val;
		}

		private void MergeObjectTables()
		{
			DMEditorObjectTable mergedTable = ScriptableObject.CreateInstance<DMEditorObjectTable>();
			editorObjectTable.ForEachRow(delegate(string key, DMEditorObjectRow row)
			{
				mergedTable.AddRow(key, row);
			});
			m_editorObjectTableTriggerables.ForEachRow(delegate(string key, DMEditorObjectRow row)
			{
				mergedTable.AddRow(key, row);
			});
			m_editorObjectTableEffects.ForEachRow(delegate(string key, DMEditorObjectRow row)
			{
				mergedTable.AddRow(key, row);
			});
			editorObjectTable = mergedTable;
		}

		public void ScheduleTakeLevelSnapshot()
		{
			Utility.DelayAction(this, delegate
			{
				TakeLevelSnapshot();
			});
		}

		private void TakeLevelSnapshot()
		{
			SnapObjects(SnapObjectsMode.forced);
			m_newScene.flatEntities.Clear();
			m_newVolume.volumeChunks.Clear();
			VolumeRootObject.ForEachChunk(delegate(Vector3Int chunkPosition, VolumeMeshChunk volumeMeshChunk)
			{
				m_newVolume.volumeChunks.Add(chunkPosition, volumeMeshChunk.CloneVolumeChunk());
			});
			bool flag = m_newVolume.volumeChunks.Count == m_levelVolume.volumeChunks.Count;
			if (flag)
			{
				foreach (KeyValuePair<Vector3Int, Level.VolumeChunk> volumeChunk in m_levelVolume.volumeChunks)
				{
					flag &= m_newVolume.volumeChunks.TryGetValue(volumeChunk.Key, out var value) && volumeChunk.Value.HasSameVersions(value);
				}
			}
			LevelUtil.AddChildEntities(m_newScene.flatEntities, Guid.Empty, LevelRootObject);
			m_newScene.flatEntities.Sort((Level.FlatEntity a, Level.FlatEntity b) => a.entity.guid.CompareTo(b.entity.guid));
			List<Level.FlatEntity> list = new List<Level.FlatEntity>();
			List<Level.FlatEntity> list2 = new List<Level.FlatEntity>();
			int num = 0;
			int num2 = 0;
			while (num < m_levelScene.flatEntities.Count || num2 < m_newScene.flatEntities.Count)
			{
				int num3 = ((num == m_levelScene.flatEntities.Count) ? 1 : ((num2 == m_newScene.flatEntities.Count) ? (-1) : m_levelScene.flatEntities[num].entity.guid.CompareTo(m_newScene.flatEntities[num2].entity.guid)));
				if (num3 == 0)
				{
					if (!m_levelScene.flatEntities[num].Equals(m_newScene.flatEntities[num2]))
					{
						list2.Add(m_levelScene.flatEntities[num]);
						list.Add(m_newScene.flatEntities[num2]);
					}
					num++;
					num2++;
				}
				else if (num3 < 0)
				{
					list2.Add(m_levelScene.flatEntities[num++]);
				}
				else
				{
					list.Add(m_newScene.flatEntities[num2++]);
				}
			}
			if (m_editorState.CurrentHistoryEntry + 1 < m_editorState.HistoryDeltaModel.Count)
			{
				m_editorState.HistoryDeltaModel.RemoveRange(m_editorState.CurrentHistoryEntry + 1, m_editorState.HistoryDeltaModel.Count - (m_editorState.CurrentHistoryEntry + 1));
			}
			if (list.Count == 0 && list2.Count == 0 && flag)
			{
				Debug.Log("No changes detected, skipping snapshot.");
				return;
			}
			m_editorState.HistoryDeltaModel.Add(new DeltaModel
			{
				HistoryId = ++m_editorState.NextHistoryId,
				previousVolumeChunks = m_levelVolume.volumeChunks,
				nextVolumeChunks = (flag ? m_levelVolume.volumeChunks : m_newVolume.volumeChunks),
				NewEntities = list,
				OldEntities = list2
			});
			if (m_editorState.HistoryDeltaModel.Count <= 100)
			{
				m_editorState.CurrentHistoryEntry++;
			}
			else
			{
				m_editorState.HistoryDeltaModel.RemoveAt(0);
			}
			Swap(ref m_levelScene.flatEntities, ref m_newScene.flatEntities);
			if (!flag)
			{
				Swap(ref m_levelVolume.volumeChunks, ref m_newVolume.volumeChunks);
				m_newVolume.volumeChunks = new Dictionary<Vector3Int, Level.VolumeChunk>();
			}
			AnalyzeLevelResult analyzeLevelResult = LevelUtil.AnalyzeLevel(LevelRootObject);
			if (analyzeLevelResult != AnalyzeLevelResult.Approved)
			{
				MessageDisplay.DisplayMessage(LevelUtil.LevelResultToErrorMessage(analyzeLevelResult));
				Undo();
				if (m_editorState.HistoryDeltaModel.Count > 0)
				{
					m_editorState.HistoryDeltaModel.RemoveAt(m_editorState.HistoryDeltaModel.Count - 1);
				}
			}
			else
			{
				m_editorState.MapIsDirty = true;
			}
		}

		private void BuildLevelSettings()
		{
			SetWaterLevel(m_levelSettings.waterLevel);
			SetPreset(m_levelSettings.presetName, ignoreMusic: true);
			SetWeather(m_levelSettings.weatherIndex);
			SetMusic(m_levelSettings.musicIndex);
			SetTimeOfDay(m_levelSettings.timeOfDay);
			SetSunColor(m_levelSettings.sunColor);
			SetSunIntensity(m_levelSettings.sunIntensity);
		}

		private void AddChildrenToExistingEditorObjects(GameObject gameObject)
		{
			foreach (Transform item in gameObject.transform)
			{
				DMEditorComponent component = item.gameObject.GetComponent<DMEditorComponent>();
				if (component != null)
				{
					AddChildrenToExistingEditorObjects(component.gameObject);
					existingEditorObjects.Add(component.entity.guid, component);
				}
			}
		}

		private void ClearExistingEditorObjects()
		{
			foreach (KeyValuePair<Guid, DMEditorComponent> existingEditorObject in existingEditorObjects)
			{
				UnityEngine.Object.Destroy(existingEditorObject.Value.gameObject);
			}
			existingEditorObjects.Clear();
		}

		private void UpdateLevel()
		{
			VolumeRootObject.SetChunks(m_levelVolume.volumeChunks);
			AddChildrenToExistingEditorObjects(LevelRootObject);
			foreach (EntityTreeNode item in LevelUtil.BuildEntityTrees(m_levelScene.flatEntities))
			{
				DMEditorComponent editorObject = InstantiateEditorObjectUsingExistingEditorObjects(item, LevelRootObject);
				MarkObjectForSnapping(editorObject);
			}
			foreach (KeyValuePair<Guid, DMEditorComponent> existingEditorObject in existingEditorObjects)
			{
				UnityEngine.Object.Destroy(existingEditorObject.Value.gameObject);
			}
			existingEditorObjects.Clear();
			InitiateEditorObjects();
		}

		public void SwitchAction(ToolTableRow newAction)
		{
			if (playerCamera == null || ((bool)m_currentTool && newAction.toolPrefab.name == m_currentTool.name))
			{
				return;
			}
			if ((bool)m_currentTool)
			{
				UnityEngine.Object.DestroyImmediate(m_currentTool);
			}
			GameObject original = ((newAction.toolPrefab != null) ? newAction.toolPrefab : m_defaultTool);
			m_currentTool = UnityEngine.Object.Instantiate(original, playerCamera.transform);
			m_currentTool.name = newAction.toolPrefab.name;
			BlockEscapeMenu = newAction.blockEscapeMenu;
			blocksGridMenu = newAction.blocksGridMenu;
			GameObject ct = m_currentTool;
			LeanTween.delayedCall(1.5f, (System.Action)delegate
			{
				if (ct != null)
				{
					TutorialPopUps.TooltipPopUp(newAction, m_currentTool);
				}
			});
		}

		public void SwitchToDefaultTool()
		{
			ToolTableRow rowValue = toolTable.GetRowValue(m_defaultToolGuid);
			m_defaultTool = rowValue.toolPrefab;
			SwitchAction(rowValue);
		}

		private static void ApplyEntityUpdates(List<Level.FlatEntity> entities, List<Level.FlatEntity> entitiesToAdd, List<Level.FlatEntity> entitiesToRemove)
		{
			Dictionary<Guid, Level.FlatEntity> dictionary = new Dictionary<Guid, Level.FlatEntity>();
			foreach (Level.FlatEntity entity in entities)
			{
				dictionary.Add(entity.entity.guid, entity);
			}
			foreach (Level.FlatEntity item in entitiesToAdd)
			{
				dictionary.Remove(item.entity.guid);
			}
			foreach (Level.FlatEntity item2 in entitiesToRemove)
			{
				dictionary.Add(item2.entity.guid, item2);
			}
			entities.Clear();
			foreach (KeyValuePair<Guid, Level.FlatEntity> item3 in dictionary)
			{
				entities.Add(item3.Value);
			}
		}

		public void Undo()
		{
			if (m_editorState.CurrentHistoryEntry >= 0)
			{
				ScreenDistortion.RadialDistort(1.5f);
				ScreenShake.Instance.AddForce(UnityEngine.Random.insideUnitSphere.normalized * 0.5f, playerCamera.transform.position + Vector3.forward);
				ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect("UI/Swosh", 1.5f, playerCamera.transform.position, SoundEffectVariations.MaterialType.Default, null, 0.75f);
				undo.Invoke();
				m_levelVolume.volumeChunks = m_editorState.HistoryDeltaModel[m_editorState.CurrentHistoryEntry].previousVolumeChunks;
				ApplyEntityUpdates(m_levelScene.flatEntities, m_editorState.HistoryDeltaModel[m_editorState.CurrentHistoryEntry].NewEntities, m_editorState.HistoryDeltaModel[m_editorState.CurrentHistoryEntry].OldEntities);
				m_editorState.CurrentHistoryEntry--;
				UpdateLevel();
			}
		}

		public void Redo()
		{
			if (m_editorState.CurrentHistoryEntry + 1 < m_editorState.HistoryDeltaModel.Count)
			{
				ScreenDistortion.RadialDistort(1.5f);
				ScreenShake.Instance.AddForce(UnityEngine.Random.insideUnitSphere.normalized * 0.5f, playerCamera.transform.position + Vector3.forward);
				ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect("UI/Swosh", 1.5f, playerCamera.transform.position, SoundEffectVariations.MaterialType.Default, null, 0.75f);
				undo.Invoke();
				m_editorState.CurrentHistoryEntry++;
				m_levelVolume.volumeChunks = m_editorState.HistoryDeltaModel[m_editorState.CurrentHistoryEntry].nextVolumeChunks;
				ApplyEntityUpdates(m_levelScene.flatEntities, m_editorState.HistoryDeltaModel[m_editorState.CurrentHistoryEntry].OldEntities, m_editorState.HistoryDeltaModel[m_editorState.CurrentHistoryEntry].NewEntities);
				UpdateLevel();
			}
		}

		private void ClearUndoHistory()
		{
			m_editorState.CurrentHistoryEntry = -1;
			m_editorState.HistoryDeltaModel.Clear();
			m_editorState.NextHistoryId = 0;
			m_editorState.HistoryId = 0;
		}

		public void SetShowWater(bool showWater)
		{
			if (showWater != water.activeSelf)
			{
				m_levelSettings.showWater = showWater;
				if (showWater)
				{
					water.SetActive(value: true);
				}
				float y = (showWater ? 1 : 0);
				float to = ((!showWater) ? 600 : 0);
				LeanTween.delayedCall(0.3f, (System.Action)delegate
				{
					water.SetActive(m_levelSettings.showWater);
				});
				water.transform.LeanScale(new Vector3(1f, y, 1f), 0.3f).setEaseOutExpo();
				water.transform.GetChild(0).LeanRotateY(to, 0.5f).setEaseOutExpo();
			}
		}

		public void SetWaterLevel(float waterLevel)
		{
			m_levelSettings.waterLevel = waterLevel;
			water.transform.LeanMoveY(waterLevel, 0.05f);
			bool showWater = waterLevel > -24f;
			SetShowWater(showWater);
		}

		public bool IsPointUnderWater(Vector3 point)
		{
			return m_levelSettings.waterLevel > point.y;
		}

		public void SetWaterMaterial(Material waterMaterial)
		{
			water.GetComponentInChildren<MeshRenderer>().material = waterMaterial;
		}

		public void UpdateMaterials(LevelPresetData levelPreset)
		{
			VolumeRootObject.UpdateMaterials(levelPreset);
		}

		public void SetWeather(int index)
		{
			m_levelSettingsMenu.m_weatherSelector.SetIndexWithoutNotify(index);
			m_levelSettings.weatherIndex = index;
			m_levelSettingsMenu.SetWeather(index);
		}

		public void SetMusic(string musicName)
		{
			for (int i = 0; i < AudioManager.MusicClips.Length; i++)
			{
				string text = AudioManager.MusicClips[i];
				if (musicName == text)
				{
					this.SettingMusicToPlay?.Invoke(i, musicName);
					SetMusic(i);
					break;
				}
			}
		}

		public void SetMusic(int index)
		{
			m_levelSettingsMenu.m_musicSelector.SetIndexWithoutNotify(index);
			m_levelSettings.musicIndex = index;
			AudioManager.SetClip(index);
		}

		public void SetPreset(string presetName, bool ignoreMusic = false)
		{
			LevelPresetData levelPresetData = (from x in Resources.FindObjectsOfTypeAll<LevelPresetData>()
				where x.name == presetName
				select x).FirstOrDefault();
			if (levelPresetData == null)
			{
				levelPresetData = (from x in Resources.LoadAll<LevelPresetData>("LevelMenuData")
					where x.name == presetName
					select x).FirstOrDefault();
			}
			SetPreset(levelPresetData, ignoreMusic);
		}

		public void SetPreset(LevelPresetData preset, bool ignoreMusic = false)
		{
			if (!(preset == null))
			{
				m_levelSettings.presetName = preset.name;
				CurrentPreset = preset;
				postProcessVolume.sharedProfile = preset.PostProcessProfile;
				ApplyPostSettings component = postProcessVolume.gameObject.GetComponent<ApplyPostSettings>();
				if (component != null)
				{
					component.AssignSettingsToProfile(postProcessVolume.sharedProfile);
				}
				RenderSettings.skybox = preset.Skybox;
				UpdateSkybox();
				UpdateMaterials(preset);
				if (preset.WaterMaterial != null)
				{
					SetWaterMaterial(preset.WaterMaterial);
				}
				SeedCollectionRow rowValue = seedTable.GetRowValue(preset.SeedCollectionKey);
				if (rowValue != null)
				{
					VolumeRootObject.UpdateFoliage(rowValue.seeds);
				}
				else
				{
					Debug.LogError("Missing seed collection from " + preset.name + ": \"" + preset.SeedCollectionKey + "\"");
					VolumeRootObject.UpdateFoliage(null);
				}
				if (!ignoreMusic)
				{
					SetMusic(preset.Music);
				}
				playerCamera.GetComponent<ScreenSpaceShaderRenderer>().SetMaterial(preset.ScreenSpaceMaterial);
			}
		}

		public float GetSunHeight()
		{
			MeshRenderer componentInChildren = m_sun.GetComponentInChildren<MeshRenderer>();
			return Vector3.Dot(Vector3.up, componentInChildren.transform.position.normalized);
		}

		private void UpdateLightValues()
		{
			bool num = GetSunHeight() <= 0f;
			Color color = (num ? new Color(0.4f, 0.4f, 0.5f) : Color.white);
			float num2 = (num ? 1f : 1f);
			m_directionalLight.color = m_levelSettings.sunColor * color;
			m_directionalLight.intensity = m_levelSettings.sunIntensity * num2;
		}

		public void SetSunColor(Color color)
		{
			m_levelSettings.sunColor = color;
			UpdateLightValues();
		}

		public void SetSunIntensity(float intensity)
		{
			m_levelSettings.sunIntensity = intensity;
			m_sun.transform.GetChild(0).GetChild(0).transform.localScale = Vector3.one * Mathf.Lerp(100f, 300f, intensity / SunManipulator.m_maxIntensity);
			UpdateLightValues();
		}

		public void SetTimeOfDay(Quaternion timeOfDay)
		{
			m_levelSettings.timeOfDay = timeOfDay;
			m_sun.transform.rotation = timeOfDay;
			m_directionalLight.transform.localRotation = ((GetSunHeight() <= 0f) ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.identity);
			UpdateSkybox();
			UpdateLightValues();
		}

		private void UpdateSkybox()
		{
			MeshRenderer[] componentsInChildren = m_sun.GetComponentsInChildren<MeshRenderer>();
			float num = Mathf.Clamp01(Vector3.Dot(Vector3.up, componentsInChildren[0].transform.position.normalized));
			float num2 = Mathf.Lerp(1f, 0f, num * 0.75f);
			float num3 = Mathf.Lerp(1f, 0f, num * 3f);
			RenderSettings.skybox.SetFloat("_Blend1_2", num2);
			RenderSettings.skybox.SetFloat("_Blend1_3", num3);
			m_levelSettings.skyboxDayBlend = num2;
			m_levelSettings.skyboxNightBlend = num3;
			float a = Mathf.Lerp(0f, 1f, Mathf.Pow(num, 0.25f));
			float a2 = Mathf.Lerp(1f, 0f, Mathf.Pow(num, 0.025f));
			componentsInChildren[0].material.SetColor("_Color", new Color(1f, 1f, 1f, a));
			componentsInChildren[1].material.SetColor("_Color", new Color(1f, 1f, 1f, a2));
			RenderSettings.ambientGroundColor = Color.Lerp(m_dayGroundColor, m_nightGroundColor, num3);
			RenderSettings.ambientEquatorColor = Color.Lerp(m_dayEquatorColor, m_nightEquatorColor, num3);
			RenderSettings.ambientSkyColor = Color.Lerp(m_daySkyColor, m_nightSkyColor, num3);
			m_levelSettings.ambientGroundColor = RenderSettings.ambientGroundColor;
			m_levelSettings.ambientEquatorColor = RenderSettings.ambientEquatorColor;
			m_levelSettings.ambientSkyColor = RenderSettings.ambientSkyColor;
		}

		public bool GetShowTeamEdge()
		{
			SettingsInstance settingsInstance = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_LEVELCREATOR_TEAMEDGE");
			return settingsInstance.currentValue == settingsInstance.defaultValue;
		}

		public void LoadLevel(StartState startState, string filePath)
		{
			FileHandlingFileType fileType;
			if (filePath.Contains(Paths.PlayerLevelDirectoryName) || filePath.Contains(Paths.TestMapName))
			{
				fileType = FileHandlingFileType.CustomContentOrLocalStorageFile;
			}
			else
			{
				fileType = FileHandlingFileType.StreamingAssetsOrReadOnlyFile;
			}
			DMIOWrapper.File.Exists(filePath, fileType, delegate(bool exists)
			{
				if (exists)
				{
					switch (startState)
					{
					case StartState.New:
						m_editorState.SetCurrentFilePath(string.Empty);
						m_editorState.MapIsDirty = false;
						break;
					case StartState.Edit:
						if (filePath.Contains(Paths.PlayerLevelDirectoryName))
						{
							m_editorState.SetCurrentFilePath(filePath);
						}
						break;
					}
					Debug.Log("Loading " + filePath);
					DMIOWrapper.File.ReadAllBytes(filePath, fileType, delegate(byte[] bytes, Exception e)
					{
						Level level = LevelSerializer.Deserialize(Utility.Unzip(bytes));
						if (filePath != Paths.TestMapPath)
						{
							ClearUndoHistory();
						}
						m_levelSettings = level.settings;
						BuildLevelSettings();
						m_levelScene = level.scene;
						m_levelVolume = level.volume;
						VolumeRootObject.InvalidateAllChunks();
						UpdateLevel();
						playerController.ResetView();
						LevelUtility.AddRecentLevel(filePath);
						ScreenshotTool.Screenshots.Clear();
						StartMenu.SetBackButtonState(StartMenu.StartMenuBackState.ToEditor);
						LevelWasDirtyWhenEnteredPlayMode = false;
					});
				}
			});
		}

		private void EnsureLevelSaveDirExists(string filePath, Action<string, DatabaseID> callback)
		{
			DatabaseID levelID = DatabaseID.NewID();
			string folderPath = Path.Combine(Paths.PlayerLevelDirectory, levelID.ToString());
			DMIOWrapper.Directory.EnsureDirectoryExists(folderPath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate
			{
				callback?.Invoke(folderPath, levelID);
			});
		}

		public void QuickSave()
		{
			if (HasSaveableLevelPath())
			{
				WriteLevel(m_editorState.CurrentFilePath + CustomContentFilePaths.FileEndingCustomLevel);
				LeanTween.delayedCall(0.2f, (System.Action)delegate
				{
					PopUp.CreatePopUp(Vector3.zero, "LC_MAP_SAVED_POPUP", demandFocus: false, 1f).Show();
				});
			}
			else
			{
				DMUIManager.Instance.OpenPanel(DMUIManager.UIPanels.SaveMenu);
			}
		}

		public void WriteLevel(string filePath)
		{
			DMIOWrapper.File.WriteAllBytes(filePath, Utility.Zip(LevelSerializer.Serialize(new Level
			{
				settings = m_levelSettings,
				scene = m_levelScene,
				volume = m_levelVolume
			})), FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception e)
			{
				Debug.Log("Saved level to " + filePath);
				if (e == null && filePath != Paths.TestMapPath)
				{
					m_editorState.MapIsDirty = false;
				}
				m_editorState.HistoryId = GetCurrentHistoryId();
				LevelUtility.AddRecentLevel(filePath);
			});
		}

		public void SaveLevel(string filePath, string levelName, Texture2D thumbnail)
		{
			EnsureLevelSaveDirExists(filePath, delegate(string folderPath, DatabaseID levelID)
			{
				filePath = Path.Combine(folderPath, levelID.ToString());
				m_editorState.SetCurrentFilePath(filePath);
				SaveThumbnail(filePath, thumbnail);
				WriteLevel(filePath + CustomContentFilePaths.FileEndingCustomLevel);
				SaveMetadata(filePath, levelName, levelID, null);
			});
		}

		private void SaveThumbnail(string filePath, Texture2D thumbnail)
		{
			byte[] bytes = thumbnail.EncodeToJPG(85);
			DMIOWrapper.File.WriteAllBytes(filePath + ".jpg", bytes, FileHandlingFileType.CustomContentOrLocalStorageFile, OnDoneWritingThumbnail);
		}

		private void OnDoneWritingThumbnail(Exception e)
		{
			if (e != null)
			{
				Debug.LogError("Writing thumbnail failed.\n" + e.Message);
			}
		}

		public static void SaveMetadata(string levelPath, string levelName, DatabaseID id, Action<bool> doneCallback)
		{
			FileIOWrapper service = ServiceLocator.GetService<FileIOWrapper>();
			string iconPath = levelPath + ".jpg";
			string text = levelPath;
			if (!text.Contains(CustomContentFilePaths.FileEndingCustomMap))
			{
				text += CustomContentFilePaths.FileEndingCustomMap;
			}
			string contents = JsonUtility.ToJson(CustomMap.Serialize(new CustomMap(levelPath, iconPath, text, levelName, id)));
			service.WriteAllText(text, contents, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(Exception exception)
			{
				if (exception == null)
				{
					ServiceLocator.GetService<CustomContentLoaderModIO>().QuickRefresh(WorkshopContentType.Map, delegate
					{
						doneCallback?.Invoke(obj: true);
					});
				}
				else
				{
					Debug.LogFormat("Failed to save level metadata: {0}\n{1}", levelPath, exception);
					doneCallback?.Invoke(obj: false);
				}
			});
		}

		private void Update()
		{
			if (m_currentShowCursor != m_showCursor || (PlayerActions.Instance.InputType == InputType.Keyboard && !Cursor.visible))
			{
				UpdateCursor();
			}
			SnapObjects(SnapObjectsMode.relaxed);
			if ((!PlayerActions.Instance.m_menu.WasPressed && !PlayerActions.Instance.m_back.WasPressed) || !m_escapeCanvas.activeSelf)
			{
				return;
			}
			LeanTween.delayedCall(0.2f, (System.Action)delegate
			{
				if (!m_escapeParent.activeSelf)
				{
					ExitEscapeMenu();
				}
			});
		}

		public void LoadLevelScene()
		{
			LevelWasDirtyWhenEnteredPlayMode = HasDirtyLevelData();
			WriteLevel(Paths.TestMapPath);
			SpawnLevel.levelToSpawn = Paths.TestMapPath;
			SpawnLevel.IsCustomLevelTestRun = true;
			ServiceLocator.GetService<GameModeService>().SetGameMode<SandboxGameMode>();
			TABSSceneManager.LoadMap(m_levelMap);
		}

		public void SetInputMode(InputMode inputMode)
		{
			switch (inputMode)
			{
			case InputMode.Game:
				SetControllerMode(ControllerMode.firstPersonMovement);
				m_escapeCanvas.SetActive(value: true);
				break;
			case InputMode.UIOnly:
				SetControllerMode(ControllerMode.mouseCursor);
				if (!m_escapeParent.activeSelf)
				{
					m_escapeCanvas.SetActive(value: false);
				}
				break;
			}
			currentInputMode = inputMode;
			UpdateCursor();
		}

		public void UpdateInputMode()
		{
			if (m_escapeCanvas.activeSelf)
			{
				SetInputMode(InputMode.UIOnly);
			}
			else
			{
				SetInputMode(DMUIManager.Instance.IsOpen ? InputMode.UIOnly : InputMode.Game);
			}
		}

		public void UpdateCursor()
		{
			m_currentShowCursor = m_showCursor;
			if (m_showCursor)
			{
				if (PlayerActions.Instance.InputType != InputType.Controller)
				{
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
				}
			}
			else
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}

		public void HideCursor()
		{
			m_showCursor = false;
		}

		public void ShowCursor()
		{
			m_showCursor = true;
		}

		public void SetControllerMode(ControllerMode controllerMode)
		{
			switch (controllerMode)
			{
			case ControllerMode.mouseCursor:
				ShowCursor();
				playerController.SetRotationLock(locked: true);
				playerController.SetMovementLock(locked: true);
				break;
			case ControllerMode.firstPersonMovement:
				HideCursor();
				playerController.SetRotationLock(locked: false);
				playerController.SetMovementLock(locked: false);
				break;
			}
		}

		public bool HasMouseCursor()
		{
			return m_showCursor;
		}

		private void EnterEscapeMenu()
		{
			if (!DMUIManager.Instance.IsOpen)
			{
				this.OnEnterMenu?.Invoke();
				SetInputMode(InputMode.UIOnly);
				InputManager.DisableInputPolling();
			}
		}

		public void ExitEscapeMenu()
		{
			SetInputMode(InputMode.Game);
			InputManager.EnableInputPolling();
		}

		public void SetVisualTargetMode(VisualTargetMode visualTargetMode)
		{
			switch (visualTargetMode)
			{
			case VisualTargetMode.Sphere:
				EnableVisualTarget(m_sphere);
				break;
			case VisualTargetMode.Dot:
				EnableVisualTarget(m_dot);
				break;
			case VisualTargetMode.Crosshair:
				EnableVisualTarget(m_crosshair);
				break;
			case VisualTargetMode.Hand:
				EnableVisualTarget(m_hand);
				break;
			case VisualTargetMode.HandClosed:
				EnableVisualTarget(m_handClosed);
				break;
			case VisualTargetMode.None:
				EnableVisualTarget(null);
				break;
			}
			void EnableVisualTarget(GameObject target)
			{
				if (m_activeVisualTarget != null)
				{
					m_activeVisualTarget.SetActive(value: false);
				}
				m_activeVisualTarget = target;
				if (m_activeVisualTarget != null)
				{
					m_activeVisualTarget.SetActive(value: true);
				}
			}
		}

		public void SetVisualObjectSphereRadius(float radius)
		{
			m_sphere.transform.localScale = Vector3.one * radius * 2f;
		}

		public void EnableSphereEmission(bool enabled)
		{
			m_sphere.GetComponent<Renderer>().material.SetInt("_EmissionToggle", enabled ? 1 : 0);
		}

		public void MoveToLevel(GameObject gameObject)
		{
			gameObject.transform.parent = LevelRootObject.transform;
			Utility.SetLayerRecursively(gameObject, LayerMask.NameToLayer("Default"));
		}

		public void MoveToLevel(DMEditorComponent editorObject)
		{
			MoveToLevel(editorObject.gameObject);
		}

		public void MoveToPreview(DMEditorComponent editorObject)
		{
			if (!objectsToSnap.ContainsKey(editorObject))
			{
				objectsToSnap.Remove(editorObject);
			}
			if ((bool)editorObject.transform.parent.GetComponent<DMEditorComponent>())
			{
				EntityTransformation globalEntityTransformWithoutSlope = editorObject.GetGlobalEntityTransformWithoutSlope();
				editorObject.Position = globalEntityTransformWithoutSlope.position;
				editorObject.AdditionalRotation = globalEntityTransformWithoutSlope.rotation;
				editorObject.Scale = globalEntityTransformWithoutSlope.scale;
			}
			editorObject.gameObject.transform.parent = Preview.transform;
			Utility.SetLayerRecursively(editorObject.gameObject, LayerMask.NameToLayer("Ignore Raycast"));
		}

		public void MarkObjectForSnapping(DMEditorComponent editorObject)
		{
			if (!editorObject)
			{
				Debug.LogError("Called MarkObjectForSnapping with null object.");
			}
			else if (!objectsToSnap.ContainsKey(editorObject))
			{
				objectsToSnap.Add(editorObject, DateTime.Now);
			}
		}

		public void MarkObjectsForSnapping(Bounds bounds)
		{
			DateTime now = DateTime.Now;
			foreach (Transform item in LevelRootObject.transform)
			{
				DMEditorComponent component = item.gameObject.GetComponent<DMEditorComponent>();
				if (component != null && bounds.Contains(component.Position) && !objectsToSnap.ContainsKey(component))
				{
					objectsToSnap.Add(component, now);
				}
			}
		}

		private void SnapObjects(SnapObjectsMode snapObjectsMode)
		{
			try
			{
				if (objectsToSnap == null || objectsToSnap.Count == 0)
				{
					return;
				}
			}
			catch
			{
				return;
			}
			bool flag = false;
			if (snapObjectsMode == SnapObjectsMode.forced)
			{
				VolumeRootObject.BuildAllChunks();
				flag = true;
			}
			List<DMEditorComponent> list = new List<DMEditorComponent>();
			DateTime now = DateTime.Now;
			foreach (KeyValuePair<DMEditorComponent, DateTime> item in objectsToSnap)
			{
				if ((bool)item.Key)
				{
					Utility.SnapDistance snapDistance = Utility.SnapDistance.Short;
					if (!flag && VolumeRootObject.HasChunksUnderConstruction(item.Key.Position, Utility.GetSnapDistance(snapDistance)))
					{
						if ((now - item.Value).TotalMilliseconds < 200.0)
						{
							continue;
						}
						VolumeRootObject.BuildAllChunks();
						flag = true;
					}
					Utility.SnapTransform? snapTransform = Utility.GetSnapTransform(item.Key.Position, snapDistance);
					if (snapTransform.HasValue)
					{
						item.Key.Position = snapTransform.Value.position;
						item.Key.Slope = snapTransform.Value.slope;
					}
					if (!snapTransform.HasValue)
					{
						UnityEngine.Object.Destroy(item.Key.gameObject);
					}
				}
				list.Add(item.Key);
			}
			foreach (DMEditorComponent item2 in list)
			{
				objectsToSnap.Remove(item2);
			}
		}

		public Quaternion GetSlope(GameObject gameObject)
		{
			if (gameObject == null)
			{
				return Quaternion.identity;
			}
			DMEditorComponent componentInParent = gameObject.GetComponentInParent<DMEditorComponent>();
			if (componentInParent == null)
			{
				return Quaternion.identity;
			}
			return componentInParent.CalculateLocalRotation();
		}

		public EntityTransformation GetTransformation(GameObject gameObject)
		{
			if (gameObject == null)
			{
				return EntityTransformation.Id;
			}
			DMEditorComponent componentInParent = gameObject.GetComponentInParent<DMEditorComponent>();
			if (componentInParent == null)
			{
				return EntityTransformation.Id;
			}
			return componentInParent.GetGlobalEntityTransform();
		}

		public void SetParent(DMEditorComponent editorObject, GameObject parentGameObject)
		{
			if (!(parentGameObject == null))
			{
				DMEditorComponent componentInParent = parentGameObject.GetComponentInParent<DMEditorComponent>();
				if (!(componentInParent == null))
				{
					editorObject.transform.parent = componentInParent.transform;
					EntityTransformation entityTransformation = new EntityTransformation
					{
						position = editorObject.entity.position,
						rotation = editorObject.entity.rotation,
						scale = editorObject.entity.scale
					};
					EntityTransformation entityTransformation2 = componentInParent.GetGlobalEntityTransform().Inverse() * entityTransformation;
					editorObject.entity.position = entityTransformation2.position;
					editorObject.entity.rotation = entityTransformation2.rotation;
					editorObject.entity.scale = entityTransformation2.scale;
				}
			}
		}

		private void SetCustomData(DMEditorComponent editorObject, Dictionary<string, string> customData)
		{
			if (editorObject.entity.customData != customData)
			{
				if (editorObject.entity.customData != null && customData != null && editorObject.entity.customData.Count == customData.Count && !editorObject.entity.customData.Except(customData).Any())
				{
					Debug.LogWarning("equal");
					return;
				}
				editorObject.entity.customData = customData;
				objectsWithNewCustomData.Add(editorObject);
			}
		}

		public void InitiateEditorObjects()
		{
			foreach (DMEditorComponent objectsWithNewCustomDatum in objectsWithNewCustomData)
			{
				if (objectsWithNewCustomDatum == null)
				{
					continue;
				}
				objectsWithNewCustomDatum.GetComponents(cachedEditorObjectComponents);
				foreach (Component cachedEditorObjectComponent in cachedEditorObjectComponents)
				{
					if (!(cachedEditorObjectComponent is TriggerBox triggerBox) || objectsWithNewCustomDatum.entity.customData == null || !objectsWithNewCustomDatum.entity.customData.TryGetValue("triggerBox", out var value))
					{
						continue;
					}
					foreach (Guid item in value.Split(',').Select(Guid.Parse).ToList())
					{
						bool flag = false;
						foreach (Transform item2 in LevelRootObject.transform)
						{
							DMEditorComponent[] componentsInChildren = item2.GetComponentsInChildren<DMEditorComponent>();
							if (componentsInChildren == null)
							{
								continue;
							}
							DMEditorComponent[] array = componentsInChildren;
							foreach (DMEditorComponent dMEditorComponent in array)
							{
								if (dMEditorComponent != null && dMEditorComponent.entity.guid == item)
								{
									flag = true;
									triggerBox.AddConnection(dMEditorComponent);
								}
							}
						}
						if (!flag)
						{
							Debug.LogError("Could not find id!");
						}
					}
				}
			}
			objectsWithNewCustomData.Clear();
		}

		public DMEditorComponent InstantiateEditorObjectUsingExistingEditorObjects(Level.Entity entity, GameObject parent)
		{
			if (existingEditorObjects.TryGetValue(entity.guid, out var value))
			{
				value.EndPhysicsSimulation(snapInPlace: false);
				value.SetTransform(entity.position, entity.slope, entity.rotation, entity.scale);
				value.transform.parent = parent.transform;
				value.Teleport(DMEditorComponent.TeleportMode.TeleportAll);
				SetCustomData(value, entity.customData);
				existingEditorObjects.Remove(entity.guid);
				return value;
			}
			return InstantiateEditorObject_Impl(entity.guid, entity.objectTypeId, entity.position, entity.slope, entity.rotation, entity.scale, entity.customData, entity.heightOffset, parent, animatedSpawn: false);
		}

		public DMEditorComponent InstantiateEditorObjectUsingExistingEditorObjects(EntityTreeNode entityTreeNode, GameObject parent)
		{
			DMEditorComponent dMEditorComponent = InstantiateEditorObjectUsingExistingEditorObjects(entityTreeNode.entity, parent);
			if (entityTreeNode.childs != null)
			{
				foreach (EntityTreeNode child in entityTreeNode.childs)
				{
					InstantiateEditorObjectUsingExistingEditorObjects(child, dMEditorComponent.gameObject);
				}
			}
			return dMEditorComponent;
		}

		public DMEditorComponent InstantiateEditorObject(EntityTreeNode entityTreeNode, GameObject parent, bool animatedSpawn, Vector3? maybePosition, Quaternion? maybeSlope)
		{
			DMEditorComponent dMEditorComponent = InstantiateEditorObject_Impl(Guid.NewGuid(), entityTreeNode.entity.objectTypeId, maybePosition.HasValue ? maybePosition.Value : entityTreeNode.entity.position, maybeSlope.HasValue ? maybeSlope.Value : entityTreeNode.entity.slope, entityTreeNode.entity.rotation, entityTreeNode.entity.scale, entityTreeNode.entity.customData, entityTreeNode.entity.heightOffset, parent, animatedSpawn);
			if (entityTreeNode.childs != null)
			{
				foreach (EntityTreeNode child in entityTreeNode.childs)
				{
					InstantiateEditorObject(child, dMEditorComponent.gameObject, animatedSpawn, null, null);
				}
			}
			return dMEditorComponent;
		}

		private DMEditorComponent InstantiateEditorObject_Impl(Guid guid, string id, Vector3 position, Quaternion slope, Quaternion additionalRotation, Vector3 scale, Dictionary<string, string> customData, float heightOffset, GameObject parent, bool animatedSpawn)
		{
			DMEditorObjectRow rowValue = editorObjectTable.GetRowValue(id);
			if (rowValue != null && rowValue.EditorObject != null)
			{
				if (scale == Vector3.zero)
				{
					scale = Vector3.one * rowValue.InitialScale;
				}
				Quaternion rotation = additionalRotation;
				if (additionalRotation == Quaternion.identity)
				{
					rotation = additionalRotation * Quaternion.Euler(rowValue.InitialRotation);
				}
				GameObject gameObject = ((!(parent != null)) ? UnityEngine.Object.Instantiate(rowValue.EditorObject) : UnityEngine.Object.Instantiate(rowValue.EditorObject, parent.transform));
				DMEditorComponent dMEditorComponent = gameObject.AddComponent<DMEditorComponent>();
				dMEditorComponent.Init(guid, id, rowValue.defaultSlopeAngle, 0.5f);
				dMEditorComponent.SetTransform(position, slope, rotation, scale);
				dMEditorComponent.Teleport(DMEditorComponent.TeleportMode.TeleportAll);
				dMEditorComponent.pivotOffset = rowValue.PivotOffset;
				dMEditorComponent.HeightOffset = heightOffset;
				MeshCollider componentInChildren = dMEditorComponent.GetComponentInChildren<MeshCollider>();
				if ((bool)componentInChildren)
				{
					componentInChildren.convex = true;
				}
				dMEditorComponent.CanSimulatePhysics = rowValue.CanSimulatePhysics;
				if (!dMEditorComponent.GetComponentInChildren<Collider>())
				{
					dMEditorComponent.gameObject.AddComponent<SphereCollider>().radius = 1f;
				}
				if (rowValue.IsEffect)
				{
					ParticleSystem[] componentsInChildren = dMEditorComponent.gameObject.GetComponentsInChildren<ParticleSystem>();
					ParticleSystem[] array = componentsInChildren;
					foreach (ParticleSystem obj in array)
					{
						ParticleSystem.MainModule main = obj.main;
						main.playOnAwake = false;
						main.loop = false;
						obj.Stop();
					}
					Component[] componentsInChildren2 = gameObject.GetComponentsInChildren<Component>();
					foreach (Component component in componentsInChildren2)
					{
						if (!(component is Light) && !(component is Transform) && !(component is MeshFilter) && !(component is DelayEvent) && !(component is MeshRenderer) && !(component is TriggerEffect) && !(component is SphereCollider) && !(component is ParticleSystem) && !(component is PlaySoundEffect) && !(component is DMEditorComponent) && !(component is SphereRadiusChange) && !(component is ParticleSystemRenderer) && !(component is ParticleSystemForceField))
						{
							UnityEngine.Object.DestroyImmediate(component);
						}
					}
					if (componentsInChildren != null && componentsInChildren.Length != 0)
					{
						GameObject obj2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
						obj2.transform.SetParent(gameObject.transform);
						obj2.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
						obj2.transform.localPosition = Vector3.zero;
						obj2.transform.localRotation = Quaternion.identity;
						Material material = obj2.GetComponent<Renderer>().material;
						material.EnableKeyword("_EMISSION");
						material.SetColor("_EmissionColor", new Color(1.5f, 1.3f, 1.3f));
						material.SetFloat("_Glossiness", 0.75f);
						dMEditorComponent.gameObject.AddComponent<TriggerEffect>();
					}
				}
				gameObject.transform.localScale = scale * (animatedSpawn ? 0.0001f : 1f);
				Utility.SetLayerRecursively(gameObject, LayerMask.NameToLayer("Default"));
				SetCustomData(dMEditorComponent, customData);
				return dMEditorComponent;
			}
			return null;
		}

		public DMEditorComponent InstantiateEditorObject(string id, Vector3 position, Quaternion slope, Quaternion additionalRotation, Vector3 scale, GameObject parent, bool animatedSpawn)
		{
			return InstantiateEditorObject_Impl(Guid.NewGuid(), id, position, slope, additionalRotation, scale, null, 0f, parent, animatedSpawn);
		}

		public DMEditorComponent InstantiateEditorObject(string id, DMEditorComponent prototype, GameObject parent, bool animatedSpawn)
		{
			return InstantiateEditorObject_Impl(Guid.NewGuid(), id, prototype.Position, prototype.Slope, prototype.AdditionalRotation, prototype.Scale, prototype.entity.customData, prototype.HeightOffset, parent, animatedSpawn);
		}

		public DMEditorComponent InstantiateEditorObject(string id, Vector3 position, Quaternion slope, Quaternion additionalRotation, GameObject parent, bool animatedSpawn)
		{
			return InstantiateEditorObject_Impl(Guid.NewGuid(), id, position, slope, additionalRotation, Vector3.zero, null, 0f, parent, animatedSpawn);
		}

		public GameObject GetPrefab(string id)
		{
			if (editorObjectTable.GetRowValue(id) != null)
			{
				return editorObjectTable.GetRowValue(id).EditorObject;
			}
			return null;
		}

		public DMEditorComponent GetAnyObjectOrChildInSphere(Vector3 targetPosition, float radius)
		{
			Collider[] array = Physics.OverlapSphere(targetPosition, radius);
			foreach (Collider collider in array)
			{
				if (collider.gameObject != null)
				{
					return collider.gameObject.GetComponent<DMEditorComponent>();
				}
			}
			return null;
		}

		public List<DMEditorComponent> GetRootObjectsInSphere(Vector3 targetPosition, float radius)
		{
			return (from obj in GetObjectsInSphere(targetPosition, radius)
				where obj.transform.parent.GetComponent<DMEditorComponent>() == null
				select obj).ToList();
		}

		public List<DMEditorComponent> GetObjectsInSphere(Vector3 targetPosition, float radius)
		{
			List<DMEditorComponent> list = new List<DMEditorComponent>();
			Collider[] array = Physics.OverlapSphere(targetPosition, radius);
			foreach (Collider collider in array)
			{
				if (collider.gameObject != null && collider.gameObject.GetComponentInParent<DMEditorComponent>() != null)
				{
					list.Add(collider.gameObject.GetComponentInParent<DMEditorComponent>());
				}
			}
			return list;
		}

		public void EnableFirstPersonMovement()
		{
			playerController.SetMovementLock(locked: false);
		}

		public void DisableFirstPersonMovement()
		{
			playerController.SetMovementLock(locked: true);
		}

		public static bool HasSaveableLevelPath()
		{
			return !string.IsNullOrEmpty(m_editorState.CurrentFilePath);
		}

		private int GetCurrentHistoryId()
		{
			if (m_editorState.CurrentHistoryEntry < 0)
			{
				return 0;
			}
			return m_editorState.HistoryDeltaModel[m_editorState.CurrentHistoryEntry].HistoryId;
		}

		public bool HasDirtyLevelData()
		{
			if (GetCurrentHistoryId() == m_editorState.HistoryId)
			{
				return m_editorState.MapIsDirty;
			}
			return true;
		}

		public void ClearLevel()
		{
			m_levelSettings = new Level.Settings();
			m_levelScene = new Level.Scene();
			m_levelVolume = new Level.Volume();
			m_editorState.MapIsDirty = false;
			ClearUndoHistory();
		}

		public static UpdateTimestamps? GetUpdateTimestamps()
		{
			if (Instance == null || Instance.VolumeRootObject == null)
			{
				return null;
			}
			return Instance.VolumeRootObject.GetUpdateTimestamps();
		}
	}
}
