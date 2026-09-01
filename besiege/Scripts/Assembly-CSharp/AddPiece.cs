using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BesiegeDlc;
using Mono.CSharp;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[AddComponentMenu("Core/Add Piece")]
public class AddPiece : SingleInstanceFindOnly<AddPiece>
{
	protected enum ReopenMode
	{
		None = 0,
		Overview = 1,
		BlockMapper = 2
	}

	public static class PlacementOffset
	{
		public static Vector3 position = Vector3.zero;

		public static Quaternion rotation = Quaternion.identity;

		public static void Set(Vector3 pos, Quaternion rot)
		{
			position = pos;
			rotation = rot;
		}

		public static void Reset()
		{
			position = Vector3.zero;
			rotation = Quaternion.identity;
		}
	}

	public class DirAnglePair
	{
		public Vector3 dir;

		public float angle;
	}

	public Action<bool, Transform> OnGhostChanged;

	public static bool usingCopiedBlock;

	public static XDataHolder copiedBlockData;

	public static bool disableBlockPlacement;

	public static bool disableBlockHighlight;

	public static bool additionalBracePlacementByClick = true;

	public static Vector3 mouseHitPos;

	public static Vector3 mouseHitNormal;

	public static Quaternion blockPlacedRotation;

	public static Vector3 hammerPos;

	public static Vector3 hammerFwd;

	public static bool individualOutOfBounds;

	public static bool isEditingLevel;

	public static bool isEditingMachine;

	public static bool blockGhostEnabled;

	public static bool canSimulate = true;

	public BlockSelectionTool selectionController;

	[HideInInspector]
	public bool canAdd;

	[HideInInspector]
	public bool hudOccluding;

	[HideInInspector]
	public bool GizmoOccluding;

	[HideInInspector]
	public float floorHeight;

	public RaycastHit mouseHit;

	[HideInInspector]
	public bool mouseHasHit;

	[HideInInspector]
	public bool validHitThisFrame;

	public RaycastHit hudHit;

	public bool checkVirtualBlocks;

	public Camera mainCam;

	public Camera hudCam;

	public Transform ghostUnknown;

	public LayerMask layerMasky;

	public LayerMask layerMaskyHud;

	public AudioSource clickSound;

	public bool npcBlock;

	public int solverIterationCounty = 12;

	public Transform middleOfObject;

	[HideInInspector]
	public float rotationAmount;

	protected float secondaryRotation;

	[NonSerialized]
	[Obsolete("Use OutOfBounds instead.")]
	public bool outOfBounds;

	[HideInInspector]
	public Transform physicsGoalObject;

	public BoundingBoxController boundVisCode;

	public RandomSoundController deleteSound;

	public SymmetryController symmetryController;

	public TimeSlider timeSlider;

	protected HammerAndNailAnim hammerAndNail;

	protected List<Machine> machinesToToggleSim;

	protected bool yieldOnMachineStart = true;

	protected Vector3 tempGrav;

	protected bool isRespawning;

	protected bool autoStartLevel = true;

	protected bool switchingSimulationMode;

	private int gizmoLayer = 23;

	private BlockType _currentBlockType;

	public Ray ray;

	private Ray rayHud;

	private bool setBarEarly;

	private BlockBehaviour _hoveredBlock;

	private Transform activeGhost;

	private BlockBehaviour lastBlock;

	private bool createdBlock;

	private Transform _currentGhost;

	private bool _currentGhostUpdate;

	private GhostMaterialController _currentGhostController;

	private GhostTrigger _currentGhostTrigger;

	private Transform _currentHammerObj;

	private BlockPrefab _currentGhostPrefab;

	private bool _currentGhostFlipped;

	private Transform _currentGhostArrow;

	private LayerMask overlayBlockMask;

	private LayerMask GroundLayerMask;

	private Vector3 UP = Vector3.forward;

	protected float deleteMouseHeld;

	private Rigidbody _currentGhostRigidbody;

	protected SaveableDataHolder lastBMTarget;

	protected ReopenMode reopenMode;

	private int clickStart;

	private bool hasAeroHit;

	private bool wasUpright;

	private float lastRot;

	public static List<BlockBehaviour> SelectedBlocks
	{
		get
		{
			return SingleInstanceFindOnly<AddPiece>.Instance.selectionController.MachineSelection;
		}
	}

	public static bool IsSelecting
	{
		get
		{
			return SingleInstanceFindOnly<AddPiece>.Instance.selectionController.CanSelect();
		}
	}

	public static bool UsingTool
	{
		get
		{
			return StatMaster.Mode.selectedTool != StatMaster.Tool.None;
		}
	}

	public bool OutOfBounds { get; protected set; }

	public Transform ActiveGhost
	{
		get
		{
			return activeGhost;
		}
	}

	public Rigidbody CurrentGhostRigidbody
	{
		get
		{
			return _currentGhostRigidbody;
		}
	}

	public BlockBehaviour HoveredBlock
	{
		get
		{
			return _hoveredBlock;
		}
		set
		{
			BlockHoverOver(value);
		}
	}

	public override string Name
	{
		get
		{
			return "_BUILDER";
		}
	}

	public BlockType CurrentType
	{
		get
		{
			return _currentBlockType;
		}
	}

	public Transform CurrentGhost
	{
		get
		{
			return _currentGhost;
		}
	}

	public BlockBehaviour LastBlock
	{
		get
		{
			return lastBlock;
		}
	}

	public bool CreatedBlock
	{
		get
		{
			return createdBlock;
		}
	}

	public Transform CurrentGhostArrow
	{
		get
		{
			return _currentGhostArrow;
		}
	}

	public Transform PhysicsGoalObject
	{
		get
		{
			return physicsGoalObject;
		}
		set
		{
			if (!(physicsGoalObject == value))
			{
				physicsGoalObject = value;
			}
		}
	}

	public GhostMaterialController CurrentGhostController
	{
		get
		{
			return _currentGhostController;
		}
	}

	public void SetOutOfBounds(bool oot)
	{
		OutOfBounds = oot;
		outOfBounds = oot;
	}

	public static bool IsMenuScene(string menuName)
	{
		menuName = menuName.ToUpper();
		return menuName.Equals("TITLE SCREEN") || menuName.StartsWith("LEVELSELECT");
	}

	protected override void Awake()
	{
		timeSlider = new TimeSlider(OptionsMaster.defaultTimeScale);
		WaterController.ResetShaderTime();
		StatMaster.isMainMenu = false;
		base.Awake();
		machinesToToggleSim = new List<Machine>();
		Physics.defaultSolverIterations = solverIterationCounty;
		GameObject gameObject = GameObject.Find("HAMMER");
		if (gameObject != null)
		{
			hammerAndNail = gameObject.GetComponent<HammerAndNailAnim>();
		}
		physicsGoalObject = GameObject.Find("PHYSICS GOAL").transform;
		StatMaster.levelSimulating = false;
		StatMaster.wasSimulating = false;
		disableBlockPlacement = false;
		individualOutOfBounds = false;
		blockGhostEnabled = false;
		overlayBlockMask = CreateLayerMask(new int[1] { 27 });
		SceneManager.sceneLoaded += OnSceneLoad;
	}

	private IEnumerator IEAutoLoadMachine(string path, bool disableBounds = false, bool autoPlay = false)
	{
		yield return new WaitForSecondsRealtime(2f);
		Machine machine = Machine.Active();
		if (machine != null)
		{
			if (disableBounds)
			{
				machine.boundingBoxController.DisableBounds(machine);
			}
			MachineInfo machineInfo = XmlLoader.LoadFromFullPath(path, string.Empty);
			machine.LoadMachineInfo(machineInfo);
			if (autoPlay)
			{
				yield return new WaitForSecondsRealtime(0.5f);
				ToggleSimulate();
			}
		}
	}

	protected virtual void OnSceneLoad(Scene scene, LoadSceneMode m)
	{
		if (IsMenuScene(scene.name))
		{
			StatMaster.isMainMenu = true;
			SceneManager.sceneLoaded -= OnSceneLoad;
		}
	}

	protected virtual void OnDestroy()
	{
		StatMaster.ResetStateSettings();
	}

	public override void SetUp()
	{
		if (CurrentType == BlockType.StartingBlock)
		{
			SetBlockType(BlockType.DoubleWoodenBlock);
		}
		KeyCodeConverter.Setup();
	}

	public void ViewBlockMapper(BlockBehaviour block)
	{
		HoveredBlock = block;
		BlockSelect(block);
		OpenBlockMapper(block);
	}

	protected virtual void OpenBlockMapper(BlockBehaviour block)
	{
		BlockMapper blockMapper = BlockMapper.Open(block);
		if (blockMapper != null)
		{
			BlockMapper.AudioSource.Play();
		}
	}

	protected virtual void Update()
	{
		timeSlider.Update();
		bool flag = InputManager.LeftMouseButton();
		bool flag2 = InputManager.LeftMouseButtonReleased();
		if (flag)
		{
			clickStart = Time.frameCount;
		}
		Vector2 vector = InputManager.CursorPosition();
		if ((StatMaster.levelSimulating && flag) || InputManager.RotateCameraKey())
		{
			CheckHudOcclusion(vector);
		}
		if (SelectionTool.BatchChange || (StatMaster.isMP && PlayerData.localPlayer != null && PlayerData.localPlayer.isSpectator) || Machine.Active().spawningMachine)
		{
			return;
		}
		if (!StatMaster.inMenu && InputManager.ToggleSimulationKey())
		{
			ToggleSimulate();
		}
		if (InputManager.SimulateOneFrame())
		{
			StartCoroutine(SimulateOneFrame((!InputManager.LeftHotShiftKey()) ? 0.1f : 0.01f));
		}
		RaycastHit hitInfo2;
		if (StatMaster.levelSimulating)
		{
			if (StatMaster.GodTools.PyroMode)
			{
				CheckHudOcclusion(vector);
				RaycastHit hitInfo;
				if (!StatMaster.hudOccluding && flag && Physics.Raycast(mainCam.ScreenPointToRay(vector), out hitInfo))
				{
					FireTag componentInParent = hitInfo.collider.GetComponentInParent<FireTag>();
					if (componentInParent != null)
					{
						componentInParent.OnPyro();
					}
				}
			}
		}
		else if (!StatMaster.hudOccluding && InputManager.PickBlockKey() && StatMaster.Mode.selectedTool == StatMaster.Tool.None && Physics.Raycast(mainCam.ScreenPointToRay(vector), out hitInfo2, 500f, layerMasky, QueryTriggerInteraction.Ignore))
		{
			BlockBehaviour componentInParent2 = hitInfo2.collider.GetComponentInParent<BlockBehaviour>();
			if (componentInParent2 != null && componentInParent2.Prefab.ghost != null && componentInParent2.BlockID != 0 && DlcManager.Instance.GetBlockDLCStatus(componentInParent2.Prefab.Type))
			{
				UnityEngine.Object.FindObjectOfType<BlockTabController>().SelectBlock(componentInParent2.BlockID);
			}
		}
		Machine machine = Machine.Active();
		bool flag3 = machine != null;
		if (flag3 && machine.isSimulating)
		{
			SetMiddle(machine.CalculateMiddle());
			deleteMouseHeld = 0f;
			return;
		}
		CheckHudOcclusion(vector);
		if (!flag3 || !machine.CanModify || Time.timeScale == 0f)
		{
			BlockHoverOut();
			ClearGhost();
			deleteMouseHeld = 0f;
			return;
		}
		bool flag4 = false;
		if (!hudOccluding)
		{
			flag4 = InputManager.DeleteKey() || InputManager.DeleteKeyHeld();
		}
		if (flag3 && machine.IsDraggingBlocks)
		{
			int num = Mathf.Abs(Time.frameCount - clickStart);
			float num2 = 1f / Time.unscaledDeltaTime;
			if ((float)num > num2 * 0.25f || !additionalBracePlacementByClick)
			{
				if (flag2 || flag4)
				{
					machine.FinishDraggedBlocks(flag4);
					flag = false;
					flag4 = false;
				}
			}
			else if (flag || flag4)
			{
				machine.FinishDraggedBlocks(flag4);
				flag = false;
				flag4 = false;
			}
		}
		bool flag5 = BlockMapper.CurrentInstance != null && BlockMapper.CurrentInstance.IsBlock;
		if (!disableBlockPlacement && !isEditingLevel)
		{
			if (IsSelecting)
			{
				if (!hudOccluding && !GizmoOccluding && !SkinPaintTool.PaintingSelection && flag)
				{
					if (StatMaster.Mode.displayDrag && hasAeroHit)
					{
						if (!AeroDynamicDisplay.IsSelected)
						{
							AeroDynamicDisplay.Select(true);
						}
						else
						{
							AeroDynamicDisplay.Select(false);
						}
					}
					else
					{
						if (HoveredBlock != null)
						{
							BlockSelect(HoveredBlock);
							if (StatMaster.Mode.selectedTool == StatMaster.Tool.Modify && selectionController.Count > 0)
							{
								OpenBlockMapper((!HoveredBlock.IsSelected) ? selectionController.LastBlock : HoveredBlock);
							}
						}
						else if (!InputManager.AdvancedBuilding.LeftShiftKey())
						{
							BlockDeselect();
						}
						if (AeroDynamicDisplay.IsSelected)
						{
							AeroDynamicDisplay.Select(false);
						}
					}
				}
				bool flag6 = StatMaster.Mode.selectedTool == StatMaster.Tool.Modify && selectionController.Count > 0;
				if (flag5 && !flag6)
				{
					BlockMapper.Close();
				}
			}
			else
			{
				if (StatMaster.Mode.selectSymmetryPivot)
				{
					if (!StatMaster.advancedBuilding)
					{
						BlockDeselect();
					}
					if (flag5)
					{
						BlockMapper.Close();
					}
				}
				else
				{
					if (!StatMaster.advancedBuilding || ((StatMaster.Mode.selectedTool != StatMaster.Tool.Erase || StatMaster.Mode.selectedTool != StatMaster.Tool.Paint) && !selectionController.IsDragging))
					{
						BlockDeselect();
					}
					if (flag5)
					{
						BlockMapper.Close();
					}
				}
				if (AeroDynamicDisplay.IsSelected)
				{
					AeroDynamicDisplay.Select(false);
				}
			}
		}
		else
		{
			BlockDeselect();
			if (flag5)
			{
				BlockMapper.Close();
			}
			if (AeroDynamicDisplay.IsSelected)
			{
				AeroDynamicDisplay.Select(false);
			}
		}
		if (!StatMaster.Mode.isTranslating && !isEditingLevel)
		{
			if (InputManager.RedoKeys())
			{
				machine.UndoSystem.Redo();
			}
			if (InputManager.UndoKeys())
			{
				machine.UndoSystem.Undo();
			}
		}
		if (StatMaster.inMenu || SingleInstanceFindOnly<WinScreen>.Instance.Visible)
		{
			deleteMouseHeld = 0f;
			validHitThisFrame = false;
			return;
		}
		canAdd = false;
		ray = mainCam.ScreenPointToRay(new Vector3(vector.x, vector.y, 0f));
		if (!hudOccluding && !GizmoOccluding)
		{
			bool flag7 = IsSelecting || StatMaster.Mode.selectedTool == StatMaster.Tool.Erase || StatMaster.Mode.selectedTool == StatMaster.Tool.Modify || StatMaster.Mode.selectedTool == StatMaster.Tool.Paint || StatMaster.Mode.selectSymmetryPivot;
			float maxDistance = 300f;
			LayerMask layerMask = layerMasky;
			if (flag7)
			{
				layerMask = CreateLayerMaskRemove(layerMasky, 21);
			}
			else if (!flag4 && !flag7)
			{
				switch (_currentBlockType)
				{
				case BlockType.Brace:
				case BlockType.Spring:
				case BlockType.RopeWinch:
				case BlockType.RopeMeasure:
				case BlockType.Sail:
					layerMask = CreateLayerMaskRemove(layerMask, 16);
					break;
				}
			}
			mouseHasHit = Physics.Raycast(ray, out mouseHit, maxDistance, layerMask);
			RaycastHit hitInfo3;
			if ((flag7 || disableBlockPlacement || flag4) && checkVirtualBlocks && Physics.Raycast(ray, out hitInfo3, maxDistance, overlayBlockMask) && hitInfo3.collider.GetComponentInParent<BlockBehaviour>() != null)
			{
				mouseHit = hitInfo3;
				mouseHasHit = true;
			}
		}
		else
		{
			mouseHasHit = false;
		}
		if (!hudOccluding)
		{
			BlockBehaviour block;
			if (IsSelecting && machine.OverrideHover(ray, mouseHasHit, mouseHit, _hoveredBlock, out block))
			{
				canAdd = false;
				UpdateHover(block);
			}
			else if (mouseHasHit)
			{
				GameObject gameObject = mouseHit.collider.gameObject;
				int layer = gameObject.layer;
				if (CurrentType == (BlockType)5000)
				{
					canAdd = layer == 14;
				}
				else if (layer == 12 || layer == 14)
				{
					canAdd = true;
				}
				block = gameObject.GetComponentInParent<BlockBehaviour>();
				UpdateHover(block);
			}
			else
			{
				BlockHoverOut();
			}
		}
		else
		{
			BlockHoverOut();
		}
		if (!hudOccluding && !IsSelecting && StatMaster.Mode.selectedTool != StatMaster.Tool.Modify && !StatMaster.Mode.selectSymmetryPivot && !disableBlockPlacement && !isEditingLevel)
		{
			if (StatMaster.Mode.selectedTool == StatMaster.Tool.Erase)
			{
				if (flag || InputManager.DeleteKeyHeld())
				{
					RemoveBlock();
				}
			}
			else if (StatMaster.Mode.selectedTool == StatMaster.Tool.Paint)
			{
				if (flag && mouseHasHit)
				{
					BlockSkinLoader.SetBlocksToPack(SkinPaintTool.Skin.pack, Machine.Active(), new List<BlockBehaviour> { _hoveredBlock });
				}
			}
			else if (flag && AddBlockType(activeGhost, true))
			{
				symmetryController.AddSymBlocks();
			}
		}
		deleteMouseHeld = ((!InputManager.LeftMouseButtonHeld()) ? 0f : (deleteMouseHeld + Time.deltaTime));
		if (!StatMaster.stopHotkeys && !isEditingLevel)
		{
			if (flag4 && (!IsSelecting || SelectedBlocks.Count == 0))
			{
				RemoveBlock();
			}
			if (InputManager.ReverseKey() && (bool)mouseHit.collider)
			{
				BlockBehaviour componentInParent3 = mouseHit.collider.gameObject.GetComponentInParent<BlockBehaviour>();
				if (componentInParent3 != null)
				{
					Machine componentInParent4 = componentInParent3.GetComponentInParent<Machine>();
					List<Tuple<BlockBehaviour, int>> mirroredBlocks = componentInParent4.GetMirroredBlocks(componentInParent3);
					if (componentInParent4 == machine && componentInParent4.ReverseBlock(componentInParent3, true, false))
					{
						List<UndoAction> list = new List<UndoAction>();
						list.Add(new UndoActionFlip(componentInParent4, componentInParent3));
						List<UndoAction> list2 = list;
						for (int i = 0; i < mirroredBlocks.Count; i++)
						{
							componentInParent3 = mirroredBlocks[i].Item1;
							if (componentInParent4.ReverseBlock(componentInParent3, false, false))
							{
								list2.Add(new UndoActionFlip(componentInParent4, componentInParent3));
							}
						}
						if (list2.Count > 0)
						{
							componentInParent4.UndoSystem.AddActions(list2);
						}
					}
				}
			}
			if (InputManager.RotateKey())
			{
				bool flag8 = true;
				if (!blockGhostEnabled && (bool)mouseHit.collider)
				{
					BlockBehaviour componentInParent5 = mouseHit.collider.gameObject.GetComponentInParent<BlockBehaviour>();
					if (componentInParent5 != null && componentInParent5.Prefab.Type == BlockType.BuildSurface)
					{
						Machine componentInParent6 = componentInParent5.GetComponentInParent<Machine>();
						if (componentInParent6 == machine)
						{
							List<Tuple<BlockBehaviour, int>> mirroredBlocks2 = componentInParent6.GetMirroredBlocks(componentInParent5);
							if (componentInParent6.SpinBlock(componentInParent5, true, true))
							{
								List<UndoAction> list = new List<UndoAction>();
								list.Add(new UndoActionSpin(componentInParent6, componentInParent5));
								List<UndoAction> list3 = list;
								for (int j = 0; j < mirroredBlocks2.Count; j++)
								{
									componentInParent5 = mirroredBlocks2[j].Item1;
									if (componentInParent6.SpinBlock(componentInParent5, false, true))
									{
										list3.Add(new UndoActionSpin(componentInParent6, componentInParent5));
									}
								}
								if (list3.Count > 0)
								{
									componentInParent6.UndoSystem.AddActions(list3);
								}
								flag8 = false;
							}
						}
					}
				}
				if (flag8)
				{
					rotationAmount += 90f;
					secondaryRotation += 90f;
					if (ReferenceMaster.onGhostTransformed != null)
					{
						ReferenceMaster.onGhostTransformed();
					}
				}
			}
		}
		hasAeroHit = mouseHasHit && mouseHit.collider != null && mouseHit.collider.tag == "AeroSelect";
		UpdateGhost();
	}

	public void CheckHudOcclusion(Vector3 pos)
	{
		rayHud = hudCam.ScreenPointToRay(new Vector3(pos.x, pos.y, 0f));
		hudOccluding = Physics.Raycast(rayHud, out hudHit, 1000f, layerMaskyHud) || EventSystem.current.IsPointerOverGameObject();
		Transform toolTransform = AdvancedBlockEditor.Instance.ToolTransform;
		GameObject rotateGizmo = AeroDynamicDisplay.instance.rotateGizmo;
		if (toolTransform.gameObject.activeSelf || rotateGizmo.activeInHierarchy)
		{
			Transform transform = SingleInstanceFindOnly<MouseOrbit>.Instance.cam.transform;
			float maxDistance = (transform.position - toolTransform.position).magnitude + 10f;
			Vector2 vector = InputManager.CursorPosition();
			rayHud = mainCam.ScreenPointToRay(new Vector3(vector.x, vector.y, 0f));
			GizmoOccluding = Physics.Raycast(rayHud, out mouseHit, maxDistance, CreateLayerMask(new int[1] { gizmoLayer }));
		}
		else
		{
			GizmoOccluding = false;
		}
		StatMaster.hudOccluding = hudOccluding;
		StatMaster.gizmoOccluding = GizmoOccluding;
	}

	protected virtual void UpdateHover(BlockBehaviour block)
	{
		Machine machine = Machine.Active();
		if (!(machine == null))
		{
			if (!machine.isSimulating && block != null)
			{
				BlockHoverOver(block);
			}
			else
			{
				BlockHoverOut();
			}
		}
	}

	public void SetMiddle(Vector3 pos)
	{
		middleOfObject.position = pos;
	}

	public static LayerMask CreateLayerMask(int[] layers)
	{
		LayerMask result = 0;
		for (int i = 0; i < layers.Length; i++)
		{
			result = result.value + (1 << layers[i]);
		}
		return result;
	}

	public static LayerMask CreateLayerMask(LayerMask mask, params int[] layers)
	{
		LayerMask result = mask.value;
		for (int i = 0; i < layers.Length; i++)
		{
			result = result.value + (1 << layers[i]);
		}
		return result;
	}

	public static LayerMask CreateLayerMaskRemove(LayerMask mask, params int[] layers)
	{
		LayerMask result = mask.value;
		for (int i = 0; i < layers.Length; i++)
		{
			result = result.value & ~(1 << layers[i]);
		}
		return result;
	}

	private void BlockHoverOver(BlockBehaviour block)
	{
		if (SelectedBlocks.Contains(block) || selectionController.IsDragging || StatMaster.Mode.isRotating || StatMaster.Mode.isTranslating)
		{
			if (HoveredBlock != null)
			{
				BlockHoverOut();
			}
			if (IsSelecting || (StatMaster.advancedBuilding && StatMaster.Mode.selectSymmetryPivot))
			{
				_hoveredBlock = block;
			}
			else
			{
				_hoveredBlock = null;
			}
		}
		else
		{
			if (HoveredBlock == block)
			{
				return;
			}
			if (HoveredBlock != null)
			{
				BlockHoverOut();
			}
			_hoveredBlock = block;
			if (disableBlockHighlight || StatMaster.Mode.selectSymmetryPivot || _hoveredBlock.VisualController.freezeOutline)
			{
				return;
			}
			if (block != null && (IsSelecting || StatMaster.Mode.selectedTool == StatMaster.Tool.Erase || StatMaster.Mode.selectedTool == StatMaster.Tool.Paint || StatMaster.Mode.selectedTool == StatMaster.Tool.Modify || disableBlockPlacement))
			{
				BlockVisualController visualController = block.VisualController;
				if (visualController != null && !visualController.Highlighted)
				{
					visualController.SetHighlighted();
				}
				Machine parentMachine = block.ParentMachine;
				List<Tuple<BlockBehaviour, int>> list = (from x in parentMachine.GetMirroredBlocks(_hoveredBlock)
					where !SelectedBlocks.Contains(x.Item1)
					select x).ToList();
				list.ForEach(delegate(Tuple<BlockBehaviour, int> x)
				{
					if (x.Item1.Prefab.hasBVC && !x.Item1.VisualController.Highlighted)
					{
						x.Item1.VisualController.SetHighlighted();
					}
				});
			}
			if (ReferenceMaster.onBlockHover != null)
			{
				ReferenceMaster.onBlockHover(_hoveredBlock != null);
			}
		}
	}

	protected void BlockHoverOut()
	{
		if (SelectedBlocks.Contains(_hoveredBlock) || _hoveredBlock == null)
		{
			_hoveredBlock = null;
		}
		else
		{
			if (_hoveredBlock.VisualController.freezeOutline)
			{
				return;
			}
			if (IsSelecting || StatMaster.Mode.selectedTool == StatMaster.Tool.Erase || StatMaster.Mode.selectedTool == StatMaster.Tool.Paint || StatMaster.Mode.selectedTool == StatMaster.Tool.Modify || StatMaster.Mode.selectSymmetryPivot || disableBlockPlacement)
			{
				BlockVisualController visualController = _hoveredBlock.VisualController;
				if (visualController != null && visualController.Highlighted)
				{
					visualController.SetNoOutline();
				}
				List<Tuple<BlockBehaviour, int>> list = (from x in _hoveredBlock.ParentMachine.GetMirroredBlocks(_hoveredBlock)
					where !SelectedBlocks.Contains(x.Item1)
					select x).ToList();
				list.ForEach(delegate(Tuple<BlockBehaviour, int> x)
				{
					if (!x.Item1.IsSelected && x.Item1.Prefab.hasBVC && x.Item1.VisualController.Highlighted)
					{
						x.Item1.VisualController.SetNoOutline();
					}
				});
			}
			_hoveredBlock = null;
		}
	}

	private void BlockSelect(BlockBehaviour block)
	{
		if (block == null)
		{
			return;
		}
		bool addToUndo = !UndoSystem.processing;
		if (IsSelecting)
		{
			bool flag = InputManager.AdvancedBuilding.LeftShiftKey();
			Machine machine = Machine.Active();
			bool flag2 = false;
			if (!flag && SingleInstanceFindOnly<AddPiece>.Instance.selectionController.Count > 0)
			{
				BlockBehaviour firstBlock = SingleInstanceFindOnly<AddPiece>.Instance.selectionController.FirstBlock;
				List<Tuple<float, BlockBehaviour>> list = machine.OverlapSurfaceBlocks(ray, mouseHasHit, mouseHit, block);
				list.RemoveAll((Tuple<float, BlockBehaviour> x) => x.Item1 > 12.5f);
				if (list.Count > 0)
				{
					int num = list.FindIndex((Tuple<float, BlockBehaviour> x) => x.Item2 == firstBlock);
					if (num != -1)
					{
						BlockBehaviour item = list[(num < list.Count - 1) ? (num + 1) : 0].Item2;
						selectionController.Deselect(firstBlock, addToUndo, true);
						block = item;
						flag2 = true;
					}
				}
			}
			if (!flag2 && block.IsSelected && !block.IsSelectedExtra && (SelectedBlocks.Count < 2 || flag))
			{
				selectionController.Deselect(block, addToUndo, true);
				return;
			}
		}
		else
		{
			BlockDeselect();
		}
		selectionController.Select(block, InputManager.AdvancedBuilding.LeftShiftKey(), addToUndo, true);
	}

	private void BlockDeselect()
	{
		bool addToUndo = !UndoSystem.processing;
		selectionController.DeselectAll(addToUndo);
	}

	public virtual void SetBlockType(BlockType type)
	{
		BlockBehaviour block;
		if (_currentBlockType == type || !PrefabMaster.GetBlock(type, out block))
		{
			return;
		}
		if (block.SurfaceType)
		{
			type = BlockType.BuildNode;
			if (!PrefabMaster.GetBlock(type, out block))
			{
				return;
			}
		}
		isEditingLevel = false;
		_currentGhostPrefab = block.Prefab;
		if (_currentGhost != null)
		{
			if ((bool)_currentGhostArrow)
			{
				_currentGhostArrow.localScale = new Vector3(Mathf.Abs(_currentGhostArrow.localScale.x), _currentGhostArrow.localScale.y, _currentGhostArrow.localScale.z);
			}
			_currentGhost.gameObject.SetActive(false);
		}
		rotationAmount = 0f;
		secondaryRotation = 0f;
		_currentGhostFlipped = false;
		if (_currentGhostPrefab.ghost != null)
		{
			_currentGhost = _currentGhostPrefab.ghost.transform;
			_currentGhostController = _currentGhost.GetComponent<GhostMaterialController>();
			_currentGhostTrigger = _currentGhost.GetComponentInChildren<GhostTrigger>();
			_currentGhostRigidbody = _currentGhost.GetComponent<Rigidbody>();
		}
		else
		{
			_currentGhost = ghostUnknown;
			_currentGhostTrigger = null;
		}
		_currentGhostUpdate = true;
		_currentGhostArrow = _currentGhost.FindChild("DirectionArrow");
		_currentHammerObj = _currentGhost.FindChild("HammerPos") ?? _currentGhost;
		_currentBlockType = type;
		StatMaster.ChangeSelectedBlock(_currentBlockType);
	}

	public void UpdateBlockButtons()
	{
		if (!(SingleInstanceFindOnly<BlockMenuItemsInitiator>.Instance == null))
		{
			BlockMenuControl[] menus = SingleInstanceFindOnly<BlockMenuItemsInitiator>.Instance.Menus;
			foreach (BlockMenuControl blockMenuControl in menus)
			{
				blockMenuControl.UpdateButtons();
			}
		}
	}

	public void ClearGhost()
	{
		validHitThisFrame = false;
		if (!(activeGhost == null))
		{
			if (OnGhostChanged != null)
			{
				OnGhostChanged(false, activeGhost);
			}
			activeGhost.gameObject.SetActive(false);
			if (symmetryController != null)
			{
				symmetryController.DisableSymGameObjects();
			}
			_currentGhostUpdate = true;
			activeGhost = null;
			SailBlock.SidePlacing = false;
		}
	}

	private void UpdateGhost()
	{
		blockGhostEnabled = false;
		if (hudOccluding || isEditingLevel || UsingTool || StatMaster.Mode.selectSymmetryPivot || disableBlockPlacement)
		{
			ClearGhost();
			return;
		}
		SailBlock.SidePlacing = false;
		activeGhost = null;
		Machine machine = Machine.Active();
		if (canAdd && _currentGhost != null && machine != null)
		{
			if (!_currentGhostPrefab.endBlock && (bool)mouseHit.collider && mouseHit.collider.CompareTag("AddPointArmor"))
			{
				validHitThisFrame = false;
				GameObject gameObject = _currentGhost.gameObject;
				if (gameObject.activeInHierarchy)
				{
					_currentGhostUpdate = true;
					if (OnGhostChanged != null)
					{
						OnGhostChanged(false, _currentGhost);
					}
					gameObject.SetActive(false);
					symmetryController.DisableSymGameObjects();
				}
				return;
			}
			blockGhostEnabled = true;
			activeGhost = _currentGhost;
			GameObject gameObject2 = _currentGhost.gameObject;
			if (!gameObject2.activeInHierarchy)
			{
				gameObject2.SetActive(true);
				symmetryController.UpdateSymmetryTransforms();
			}
			Vector3 position = _currentGhost.parent.position;
			if (mouseHit.collider != null)
			{
				Vector3 position2 = mouseHit.collider.transform.position;
				bool flag = mouseHit.collider.CompareTag("AddPointUseCenter");
				bool flag2 = _currentGhostPrefab.Type == BlockType.BuildNode;
				bool flag3 = _hoveredBlock != null && _hoveredBlock.Prefab.Type == BlockType.BuildSurface;
				if (_currentGhostPrefab.placeMode == PlaceMode.Center || (flag2 && flag3) || (flag2 && flag))
				{
					mouseHitPos = position2;
				}
				else if (flag)
				{
					mouseHitNormal = mouseHit.normal;
					mouseHitPos = position2;
				}
				else
				{
					mouseHitNormal = mouseHit.normal;
					mouseHitPos = position2 + mouseHitNormal / 2f;
				}
				validHitThisFrame = true;
			}
			else
			{
				validHitThisFrame = false;
			}
			Vector3 vector = mouseHitPos;
			Transform transform = ((!(mouseHit.rigidbody != null)) ? mouseHit.transform : mouseHit.rigidbody.transform);
			bool flag4 = false;
			float num = Mathf.Cos((float)Math.PI / 4f);
			Transform buildingMachine = machine.BuildingMachine;
			if (transform != null)
			{
				BasicInfo componentInParent = transform.GetComponentInParent<BasicInfo>();
				if (componentInParent != null && componentInParent.isSimulating)
				{
					return;
				}
				bool flag5 = false;
				if (componentInParent is BlockBehaviour)
				{
					switch ((BlockType)(componentInParent as BlockBehaviour).BlockID)
					{
					case BlockType.SteeringBlock:
					case BlockType.Swivel:
					case BlockType.SpinningBlock:
					case BlockType.BallJoint:
					case BlockType.LargeWheel:
					case BlockType.LargeWheelUnpowered:
						UP = transform.forward;
						if (!(Mathf.Abs(Vector3.Dot(mouseHitNormal, UP)) > num))
						{
							flag5 = true;
						}
						break;
					case BlockType.Sail:
						flag4 = true;
						break;
					}
				}
				if (!flag5)
				{
					UP = transform.forward;
					UP = GetLocalDirClosestTo(transform, buildingMachine.up);
					if (Mathf.Abs(Vector3.Dot(mouseHitNormal, UP)) > num)
					{
						UP = GetLocalDirClosestTo(transform, buildingMachine.forward);
						if (Vector3.Angle(mouseHitNormal, buildingMachine.up) <= 45f)
						{
							UP *= -1f;
						}
					}
				}
			}
			float num2 = Vector3.Dot(mouseHitNormal, buildingMachine.up);
			bool flag6 = num2 > num;
			bool flag7 = Mathf.Abs(num2) > num;
			Vector3 vector2 = Vector3.zero;
			float num3 = rotationAmount + _currentGhostPrefab.rotationOffset;
			switch (_currentGhostPrefab.placeMode)
			{
			case PlaceMode.Normal:
				blockPlacedRotation = Quaternion.LookRotation(mouseHitNormal, UP) * Quaternion.Euler(new Vector3(0f, 0f, num3));
				break;
			case PlaceMode.DialBlock:
				if (flag7)
				{
					blockPlacedRotation = Quaternion.LookRotation(mouseHitNormal, UP) * Quaternion.Euler(new Vector3(0f, 0f, num3 + 180f));
				}
				else
				{
					blockPlacedRotation = Quaternion.LookRotation(mouseHitNormal, UP) * Quaternion.Euler(new Vector3(0f, 0f, num3));
				}
				break;
			case PlaceMode.Center:
				if (mouseHit.transform != null)
				{
					blockPlacedRotation = Quaternion.Euler(0f, mouseHit.transform.eulerAngles.y + num3, 0f);
				}
				break;
			case PlaceMode.Camera:
				if (flag7)
				{
					blockPlacedRotation = Quaternion.LookRotation(mouseHitNormal, UP) * Quaternion.Euler(new Vector3(0f, 0f, num3));
				}
				else
				{
					blockPlacedRotation = Quaternion.LookRotation(mouseHitNormal, UP);
				}
				break;
			case PlaceMode.Rocket:
				if (flag7)
				{
					blockPlacedRotation = Quaternion.LookRotation(mouseHitNormal, UP) * Quaternion.Euler(new Vector3(0f, 0f, 180f + num3));
				}
				else
				{
					blockPlacedRotation = Quaternion.LookRotation(mouseHitNormal, UP) * Quaternion.Euler(new Vector3(0f, 0f, num3));
				}
				break;
			case PlaceMode.Rudder:
				if (flag7)
				{
					if (flag6)
					{
						blockPlacedRotation = Quaternion.LookRotation(mouseHitNormal, UP) * Quaternion.Euler(new Vector3(0f, 0f, num3));
					}
					else
					{
						blockPlacedRotation = Quaternion.LookRotation(mouseHitNormal, UP) * Quaternion.Euler(new Vector3(0f, 0f, num3 + 180f));
					}
				}
				else if (Vector3.Dot(mouseHitNormal, Vector3.right) > num)
				{
					blockPlacedRotation = Quaternion.LookRotation(mouseHitNormal, UP) * Quaternion.Euler(new Vector3(0f, 0f, num3 - 90f));
				}
				else
				{
					blockPlacedRotation = Quaternion.LookRotation(mouseHitNormal, UP) * Quaternion.Euler(new Vector3(0f, 0f, num3 + 90f));
				}
				break;
			case PlaceMode.Jaw:
			{
				Collider[] array = Physics.OverlapSphere(mouseHitPos + mouseHitNormal * 0.5f, 0.25f, layerMasky);
				for (int i = 0; i < array.Length; i++)
				{
					Rigidbody attachedRigidbody = array[i].attachedRigidbody;
					if ((bool)attachedRigidbody && attachedRigidbody.gameObject.tag == "MechanicalTag")
					{
						SpringReleaseBlock component = attachedRigidbody.GetComponent<SpringReleaseBlock>();
						if ((bool)component)
						{
							UP = -component.transform.up;
							num3 = 0f;
							break;
						}
					}
				}
				blockPlacedRotation = Quaternion.LookRotation(mouseHitNormal, UP) * Quaternion.Euler(new Vector3(0f, 0f, num3));
				break;
			}
			case PlaceMode.Sail:
			{
				if (flag7 || transform == null)
				{
					flag7 = true;
					if (wasUpright != flag7)
					{
						secondaryRotation = 0f;
					}
					if (flag4)
					{
						blockPlacedRotation = Quaternion.LookRotation(mouseHitNormal, transform.up) * Quaternion.Euler(new Vector3(0f, 0f, secondaryRotation));
						break;
					}
					secondaryRotation = 0f;
					blockPlacedRotation = Quaternion.LookRotation(mouseHitNormal, UP) * Quaternion.Euler(new Vector3(0f, 0f, rotationAmount));
					break;
				}
				if (wasUpright != flag7)
				{
					secondaryRotation = 0f;
				}
				Quaternion quaternion = Quaternion.Euler(new Vector3(0f - secondaryRotation, 0f, 0f));
				Vector3 vector3 = Vector3.Cross(UP, mouseHitNormal);
				Vector3 rhs = ((!flag4) ? buildingMachine.forward : (-transform.up));
				if (Vector3.Dot(-vector3, rhs) < num)
				{
					vector3 = -vector3;
				}
				blockPlacedRotation = Quaternion.LookRotation(UP, vector3) * quaternion;
				Quaternion quaternion2 = blockPlacedRotation * PlacementOffset.rotation;
				Vector3 vector4 = quaternion2 * Vector3.up;
				if (!flag4 && Mathf.Abs(Vector3.Dot(vector4, buildingMachine.up)) > num)
				{
					SailBlock.SidePlacing = false;
					blockPlacedRotation = Quaternion.LookRotation(mouseHitNormal, vector4);
					break;
				}
				SailBlock.SidePlacing = true;
				Vector3 vector5 = quaternion2 * Vector3.forward;
				Vector3 vector6 = ((!flag4) ? buildingMachine.up : transform.forward);
				if (Vector3.Dot(vector5, -vector6) > num)
				{
					quaternion2 = Quaternion.AngleAxis(180f, vector4) * quaternion2;
					vector5 = quaternion2 * Vector3.forward;
				}
				else if (flag4 && Vector3.Dot(vector5, -buildingMachine.up) > num)
				{
					quaternion2 = Quaternion.AngleAxis(180f, vector4) * quaternion2;
					vector5 = quaternion2 * Vector3.forward;
				}
				vector2 = mouseHitNormal * 1.5f - vector5 * 1.5f;
				blockPlacedRotation = quaternion2;
				break;
			}
			default:
				blockPlacedRotation = Quaternion.LookRotation(machine.BuildingMachine.forward, machine.BuildingMachine.up) * Quaternion.Euler(new Vector3(0f, num3, 0f));
				break;
			}
			vector += vector2;
			bool flag8 = _currentGhostUpdate || position != vector || lastRot != rotationAmount;
			lastRot = rotationAmount;
			wasUpright = flag7;
			if (flag8)
			{
				if ((bool)_currentGhostTrigger)
				{
					_currentGhostTrigger.touchingCount = 0;
				}
				Transform parent = _currentGhost.parent;
				parent.rotation = blockPlacedRotation * PlacementOffset.rotation;
				parent.position = mouseHitPos + vector2;
				_currentGhost.localPosition = PlacementOffset.position + _currentGhostPrefab.placementOffset;
				hammerPos = _currentHammerObj.position;
				hammerFwd = _currentHammerObj.forward;
				if ((bool)_currentGhostArrow)
				{
					bool flag9 = false;
					switch (CurrentType)
					{
					case BlockType.Rudder:
						flag9 = Vector3.Dot(_currentGhost.forward, Vector3.left) > num || Vector3.Dot(_currentGhost.forward, Vector3.down) > num;
						break;
					default:
						flag9 = Vector3.Dot(_currentGhost.forward, Vector3.left) > num;
						break;
					case BlockType.SteeringBlock:
					case BlockType.SteeringHinge:
						break;
					}
					if (flag9 != _currentGhostFlipped)
					{
						if (_currentGhostFlipped)
						{
							_currentGhostArrow.localScale = new Vector3(Mathf.Abs(_currentGhostArrow.localScale.x) * 1f, _currentGhostArrow.localScale.y, _currentGhostArrow.localScale.z);
						}
						else
						{
							_currentGhostArrow.localScale = new Vector3(Mathf.Abs(_currentGhostArrow.localScale.x) * -1f, _currentGhostArrow.localScale.y, _currentGhostArrow.localScale.z);
						}
						_currentGhostFlipped = !_currentGhostFlipped;
					}
				}
				_currentGhostUpdate = false;
				if (OnGhostChanged != null)
				{
					OnGhostChanged(true, _currentGhost);
				}
				symmetryController.UpdateSymmetryTransforms();
			}
			individualOutOfBounds = ((!(_currentGhostRigidbody != null)) ? OutOfBounds : (GhostTrigger.isTouching || OutOfBounds));
		}
		else
		{
			if (!(_currentGhost != null))
			{
				return;
			}
			validHitThisFrame = false;
			GameObject gameObject3 = _currentGhost.gameObject;
			if (gameObject3.activeInHierarchy)
			{
				_currentGhostUpdate = true;
				if (OnGhostChanged != null)
				{
					OnGhostChanged(false, _currentGhost);
				}
				gameObject3.SetActive(false);
				symmetryController.DisableSymGameObjects();
			}
		}
	}

	public static DirAnglePair CompareDirAndAxis(Vector3 dir, Vector3 axis)
	{
		DirAnglePair dirAnglePair = new DirAnglePair();
		dirAnglePair.angle = Vector3.Angle(dir, axis);
		dirAnglePair.dir = dir;
		return dirAnglePair;
	}

	public static Vector3 GetLocalDirClosestTo(Transform t, Vector3 axis)
	{
		DirAnglePair dirAnglePair = CompareDirAndAxis(t.forward, axis);
		DirAnglePair dirAnglePair2 = CompareDirAndAxis(t.up, axis);
		DirAnglePair dirAnglePair3 = CompareDirAndAxis(t.right, axis);
		DirAnglePair dirAnglePair4 = CompareDirAndAxis(-t.forward, axis);
		DirAnglePair dirAnglePair5 = CompareDirAndAxis(-t.up, axis);
		DirAnglePair dirAnglePair6 = CompareDirAndAxis(-t.right, axis);
		DirAnglePair[] values = new DirAnglePair[6] { dirAnglePair, dirAnglePair2, dirAnglePair3, dirAnglePair4, dirAnglePair5, dirAnglePair6 };
		return DirWithSmallestAngle(values, Vector3.one);
	}

	public static Vector3 DirWithSmallestAngle(DirAnglePair[] values, Vector3 exclude)
	{
		float num = 360f;
		Vector3 result = Vector3.one;
		foreach (DirAnglePair dirAnglePair in values)
		{
			if (dirAnglePair.angle < num && dirAnglePair.dir != exclude)
			{
				num = dirAnglePair.angle;
				result = dirAnglePair.dir;
			}
		}
		return result;
	}

	public void UpdateMiddleOfObject()
	{
		UpdateMiddleOfObject(false);
	}

	public void UpdateMiddleOfObject(bool resetWasd)
	{
		if (!StatMaster.isHeadless)
		{
			Machine machine = Machine.Active();
			MouseOrbit mouseOrbit = SingleInstanceFindOnly<MouseOrbit>.Instance;
			if (resetWasd && mouseOrbit.target == mouseOrbit.machineTarget)
			{
				SingleInstanceFindOnly<MouseOrbit>.Instance.wasdPosOffset = Vector3.zero;
			}
			SetMiddle(machine.CalculateMiddle());
			if (ReferenceMaster.onCalculateMiddle != null)
			{
				ReferenceMaster.onCalculateMiddle(machine);
			}
		}
	}

	public virtual IEnumerator StartMachines(List<Machine> machines)
	{
		while (!canSimulate || !AllMachinesReady(machines))
		{
			yield return new WaitForFixedUpdate();
		}
		canSimulate = false;
		if (!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim)
		{
			if (ReferenceMaster.onPreSimulateMachine != null)
			{
				foreach (Machine machine in machines)
				{
					if (!(machine == null))
					{
						ReferenceMaster.onPreSimulateMachine(machine);
					}
				}
			}
			yield return null;
		}
		if (autoStartLevel && !StatMaster.levelSimulating)
		{
			SimStateChange(true);
		}
		if (yieldOnMachineStart)
		{
			ToggleSimulationStartup();
			PreStartSim(ref machines);
		}
		Machine activeMachine = Machine.Active();
		setBarEarly = timeSlider.delegateTimeScale == 0f;
		foreach (Machine machine2 in machines)
		{
			if (!(machine2 == null))
			{
				machine2.StartSimulation();
				if (!isRespawning && setBarEarly && machine2.isLocalMachine)
				{
					SingleInstanceFindOnly<BarPositionController>.Instance.Set();
					AdvancedUIController.Instance.ToggleAdvanced(false);
				}
			}
		}
		blockGhostEnabled = false;
		if (yieldOnMachineStart)
		{
			yield return StartCoroutine(InitSimTime());
		}
		foreach (Machine machine3 in machines)
		{
			if (machine3 != null && machine3.isSimulating)
			{
				machine3.StartPhysics();
			}
		}
		if (!isRespawning && !setBarEarly && (bool)activeMachine && machines.Contains(activeMachine) && !StatMaster.isHeadless)
		{
			SingleInstanceFindOnly<BarPositionController>.Instance.Set();
			AdvancedUIController advancedUI = AdvancedUIController.Instance;
			if (advancedUI != null)
			{
				AdvancedUIController.Instance.ToggleAdvanced(false);
			}
		}
		if (yieldOnMachineStart)
		{
			yield return StartCoroutine(PostStartSim(machines));
			FinishSimulationStartup();
		}
		canSimulate = true;
	}

	private void ToggleSimulationStartup()
	{
		if (OptionsMaster.GetFPSLock() != 60)
		{
			StatMaster.SimulationStartInProgress = true;
			Application.targetFrameRate = 60;
		}
	}

	public static Vector3 GetLocalDirClosestToExclude(Transform t, Vector3 axis, Vector3 exclude)
	{
		DirAnglePair dirAnglePair = CompareDirAndAxis(t.forward, axis);
		DirAnglePair dirAnglePair2 = CompareDirAndAxis(t.up, axis);
		DirAnglePair dirAnglePair3 = CompareDirAndAxis(t.right, axis);
		DirAnglePair dirAnglePair4 = CompareDirAndAxis(-t.forward, axis);
		DirAnglePair dirAnglePair5 = CompareDirAndAxis(-t.up, axis);
		DirAnglePair dirAnglePair6 = CompareDirAndAxis(-t.right, axis);
		DirAnglePair[] values = new DirAnglePair[6] { dirAnglePair, dirAnglePair2, dirAnglePair3, dirAnglePair4, dirAnglePair5, dirAnglePair6 };
		return DirWithSmallestAngle(values, exclude);
	}

	private void FinishSimulationStartup()
	{
		if (StatMaster.SimulationStartInProgress)
		{
			StatMaster.SimulationStartInProgress = false;
			CapFPS.SetTargetFrameRate(OptionsMaster.GetFPSLock(), true);
		}
	}

	protected void PreStartSim(ref List<Machine> machines)
	{
		TimeSlider timeSlider = TimeSlider.Instance;
		timeSlider.startingSimulation = true;
		Time.timeScale = 0f;
		if (StatMaster.isMP)
		{
			foreach (Machine machine in machines)
			{
				if (machine == null)
				{
					continue;
				}
				foreach (BlockBehaviour buildingBlock in machine.BuildingBlocks)
				{
					if (!buildingBlock.noRigidbody)
					{
						buildingBlock.Rigidbody.useGravity = false;
					}
				}
			}
		}
		else
		{
			tempGrav = Physics.gravity;
			Physics.gravity = Vector3.zero;
		}
		StatMaster.startingMachines = true;
	}

	protected IEnumerator InitSimTime()
	{
		yield return null;
		int NumOfPhysFrames = 4;
		TimeSlider timeSlider = TimeSlider.Instance;
		timeSlider.startingSimulation = false;
		timeSlider.wasSimulating = false;
		Time.timeScale = 1f;
		for (int pf = 0; pf < NumOfPhysFrames; pf++)
		{
			yield return new WaitForFixedUpdate();
		}
		timeSlider.startingSimulation = true;
		Time.timeScale = 0f;
		TimeSlider.Instance.ResetRollingDelta();
	}

	protected IEnumerator PostStartSim(List<Machine> machines)
	{
		yield return null;
		timeSlider.startingSimulation = false;
		timeSlider.wasSimulating = false;
		Time.timeScale = Mathf.Max(0.01f, (!OptionsMaster.BesiegeConfig.AutoTimeScale) ? timeSlider.delegateTimeScale : ((OptionsMaster.BesiegeConfig.MinTimeScale + OptionsMaster.BesiegeConfig.MaxTimeScale) * 0.005f));
		if (OptionsMaster.BesiegeConfig.AutoTimeScale)
		{
			timeSlider.delegateTimeScale = Time.timeScale;
		}
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		StatMaster.startingMachines = false;
		if (StatMaster.isMP)
		{
			foreach (Machine machine in machines)
			{
				if (!(machine == null))
				{
					foreach (BlockBehaviour block in machine.BuildingBlocks)
					{
						if (!block.noRigidbody && !SkipSettingGravity(block))
						{
							block.Rigidbody.useGravity = true;
						}
					}
					foreach (BlockBehaviour block2 in machine.SimulationBlocks)
					{
						if (!block2.noRigidbody && !SkipSettingGravity(block2))
						{
							block2.Rigidbody.useGravity = true;
						}
					}
				}
			}
			yield break;
		}
		Physics.gravity = tempGrav;
	}

	private bool SkipSettingGravity(BlockBehaviour block)
	{
		return block.Prefab.Type == BlockType.Pin || block.Prefab.Type == BlockType.CameraBlock || block.isZeroG;
	}

	public void CreatePhysicsGoal()
	{
		WinCondition.timeTaken = 0f;
		ReferenceMaster.physicsGoalInstance = (UnityEngine.Object.Instantiate(physicsGoalObject.gameObject, physicsGoalObject.position, physicsGoalObject.rotation) as GameObject).transform;
		ReferenceMaster.physicsGoalInstance.name = "PHYSICS GOAL";
		physicsGoalObject.gameObject.SetActive(false);
	}

	public void DestroyPhysicsGoal()
	{
		if (ReferenceMaster.physicsGoalInstance != null)
		{
			if (ReferenceMaster.onDestroyPhysicsGoal != null)
			{
				ReferenceMaster.onDestroyPhysicsGoal();
			}
			UnityEngine.Object.Destroy(ReferenceMaster.physicsGoalInstance.gameObject);
		}
		WinCondition.timeTaken = 0f;
		physicsGoalObject.gameObject.SetActive(true);
	}

	private void FixedUpdate()
	{
		if (StatMaster.levelSimulating && !WinCondition.hasWon)
		{
			WinCondition.timeTaken += Time.fixedDeltaTime;
		}
	}

	protected virtual bool IsSimulating()
	{
		return StatMaster.levelSimulating;
	}

	public virtual void SimStateChange(bool toggle)
	{
		StatMaster.levelSimulating = toggle;
		if (toggle)
		{
			CreatePhysicsGoal();
		}
		else
		{
			DestroyPhysicsGoal();
		}
		if (ReferenceMaster.onLevelSimulation != null)
		{
			ReferenceMaster.onLevelSimulation(toggle);
		}
		if (!toggle)
		{
			Resources.UnloadUnusedAssets();
		}
	}

	public void ToggleSimulateNoSound()
	{
		boundVisCode.playFadeAudio = false;
		ToggleSimulate();
	}

	public virtual void ToggleSimulate()
	{
		Machine machine = Machine.Active();
		if (StatMaster.isLoadingLevels)
		{
			Debug.Log("Couldn't enter sim: switching scenes");
		}
		else if (machine == null)
		{
			Debug.Log("Couldn't enter sim: No active machine!");
		}
		else if (StatMaster.inMenu)
		{
			if (BesiegeLogFilter.logDev)
			{
				Debug.Log("Couldn't enter sim: In menu!");
			}
		}
		else if (!machine.ReadyForSim)
		{
			Debug.Log("Couldn't enter sim: Machine not ready for sim!" + machine.isReady + " : " + !machine.analyzing);
		}
		else if (machine.nodeController.IsBuilding)
		{
			Debug.Log("Couldn't enter sim: NodeController is being used!");
			machine.nodeController.ResetPlacement();
		}
		else if (!machine.isSimulating && StatMaster.waitingForSim)
		{
			Debug.Log("Couldn't enter sim: Waiting for sim!");
		}
		else if (StatMaster.SwitchingStates)
		{
			Debug.Log("Couldn't enter sim: Switching states..");
		}
		else if (!machine.isSimulating && OutOfBounds)
		{
			if (StatMaster.Bounding.Enabled || !StatMaster.Bounding.inGround)
			{
				OutOfBoundsWarning.current.OutOfBounds();
			}
			else
			{
				OutOfBoundsWarning.current.InFloor();
			}
		}
		else
		{
			StartCoroutine(IEToggleSimulate());
		}
	}

	public void ResetMapperTargets()
	{
		reopenMode = ReopenMode.None;
		lastBMTarget = null;
	}

	public virtual IEnumerator IEToggleSimulate()
	{
		if (machinesToToggleSim.Count == 0)
		{
			Machine activeMachine = Machine.Active();
			if (activeMachine == null)
			{
				yield break;
			}
			machinesToToggleSim.Add(activeMachine);
		}
		StatMaster.waitingForSim = true;
		while (!canSimulate || !AllMachinesReady(machinesToToggleSim))
		{
			StatMaster.SetSimulationState(SimulationState.WaitingOnMachineReady);
			yield return new WaitForFixedUpdate();
		}
		bool enterSimulation = !IsSimulating();
		if (enterSimulation && (StatMaster.GodTools.PyroMode || StatMaster.GodTools.DragMode || StatMaster.GodTools.UnbreakableMode || StatMaster.GodTools.InfiniteAmmoMode || StatMaster.GodTools.GravityDisabled))
		{
			GodToolsWarning.current.CheatsEnabled();
		}
		symmetryController.ClearSymGameObjects();
		if (enterSimulation)
		{
			StatMaster.SetSimulationState(SimulationState.SwitchingToGlobalSimulation);
			yield return StartCoroutine(StartMachines(machinesToToggleSim));
			DisableBlockGhosts();
		}
		else
		{
			StatMaster.SetSimulationState(SimulationState.SwitchingToLocalSimulation);
			yield return StartCoroutine(StopMachines(machinesToToggleSim));
		}
		if (enterSimulation)
		{
			StatMaster.SetSimulationState(SimulationState.GlobalSimulation);
		}
		else
		{
			StatMaster.SetSimulationState(SimulationState.BuildMode);
		}
	}

	public bool AllMachinesReady(List<Machine> machines)
	{
		for (int i = 0; i < machines.Count; i++)
		{
			Machine machine = machines[i];
			if (machine != null && !machine.ReadyForSim)
			{
				return false;
			}
		}
		return true;
	}

	public virtual IEnumerator StopMachines(List<Machine> machines)
	{
		while (!canSimulate)
		{
			yield return new WaitForFixedUpdate();
		}
		canSimulate = false;
		foreach (Machine machine in machines)
		{
			if (machine == null)
			{
				continue;
			}
			machine.EndSimulation();
			if (machine.isLocalMachine)
			{
				WinScreen winScreen = SingleInstanceFindOnly<WinScreen>.Instance;
				if (winScreen != null)
				{
					winScreen.Disable();
				}
				SingleInstanceFindOnly<BarPositionController>.Instance.Set();
				SetMiddle(machine.MiddlePosition);
				AdvancedUIController advancedUI = AdvancedUIController.Instance;
				if (advancedUI != null)
				{
					advancedUI.ToggleAdvanced(StatMaster.advancedBuilding);
				}
			}
		}
		SimStateChange(false);
		canSimulate = true;
	}

	public void ReopenMappers(Machine machine)
	{
		if (reopenMode == ReopenMode.Overview)
		{
			OverviewBlockMapper.Open(machine);
			reopenMode = ReopenMode.None;
		}
		else if (reopenMode == ReopenMode.BlockMapper && lastBMTarget != null && lastBMTarget.infoType == BasicInfo.BasicInfoType.Block)
		{
			ReopenBlockMapper(true);
		}
	}

	public void CloseMappers()
	{
		BlockMapper currentInstance = BlockMapper.CurrentInstance;
		OverviewBlockMapper currentInstance2 = OverviewBlockMapper.CurrentInstance;
		if (currentInstance2 != null)
		{
			reopenMode = ReopenMode.Overview;
			OverviewBlockMapper.Close();
		}
		else if (currentInstance != null && currentInstance.Current is BlockBehaviour)
		{
			reopenMode = ReopenMode.BlockMapper;
			lastBMTarget = currentInstance.Current;
			BlockMapper.Close();
		}
	}

	public virtual void ReopenBlockMapper(bool isMachine)
	{
		if (reopenMode == ReopenMode.BlockMapper && !(lastBMTarget == null) && (!isMachine || lastBMTarget.infoType == BasicInfo.BasicInfoType.Block) && (isMachine || lastBMTarget.infoType == BasicInfo.BasicInfoType.Entity))
		{
			BlockMapper.Open(lastBMTarget);
			if (isMachine)
			{
				(lastBMTarget as BlockBehaviour).VisualController.SetSelected();
			}
			reopenMode = ReopenMode.None;
			lastBMTarget = null;
		}
	}

	protected IEnumerator SimulateOneFrame(float simulationSpeed)
	{
		Time.timeScale = simulationSpeed;
		yield return null;
		Time.timeScale = 0f;
	}

	private bool AddBlockType(Transform block, bool ignoreUndo)
	{
		if (AddBlockTypeNoSound(block, ignoreUndo))
		{
			SingleHammerAnimate();
			return true;
		}
		return false;
	}

	public bool AddBlockTypeNoSound(Transform block, bool ignoreUndo)
	{
		return AddBlockTypeNoSound(block, CurrentType, ignoreUndo);
	}

	public bool AddBlockTypeNoSound(Transform block, BlockType t, bool ignoreUndo, bool force = false)
	{
		if (!force && ((individualOutOfBounds && !StatMaster.Mode.allowIntersection) || activeGhost == null || t == BlockType.StartingBlock))
		{
			createdBlock = false;
			return false;
		}
		if (!DlcManager.Instance.GetBlockDLCStatus(t))
		{
			createdBlock = false;
			return false;
		}
		List<UndoAction> list = new List<UndoAction>();
		Machine machine = Machine.Active();
		createdBlock = true;
		bool flag = false;
		switch (t)
		{
		case BlockType.Brace:
			AddBrace(block, t);
			StatMaster.Mode.placingBlock = true;
			flag = false;
			break;
		case BlockType.Unused:
			Debug.Log("Tried loading block id 8, unused block known to cause issues, and interfere with modding, refrained from loading block.");
			flag = false;
			break;
		case BlockType.BuildNode:
		{
			float num = 0.01f;
			bool flag2 = false;
			for (int i = 0; i < machine.BlockCount; i++)
			{
				BlockBehaviour block2;
				if (machine.GetBlockFromIndex(i, out block2) && block2.Prefab.Type == BlockType.BuildNode)
				{
					float sqrMagnitude = (block2.transform.position - block.position).sqrMagnitude;
					if (sqrMagnitude < num)
					{
						lastBlock = block2;
						flag2 = true;
						createdBlock = false;
						break;
					}
				}
			}
			if (!flag2)
			{
				AddBlock(block, t);
				flag = true;
			}
			break;
		}
		default:
			if (Machine.IsDraggedBlock(t))
			{
				AddSpring(block, t);
				StatMaster.Mode.placingBlock = true;
				flag = false;
			}
			else
			{
				AddBlock(block, t);
				UpdateMiddleOfObject();
				flag = true;
			}
			break;
		}
		if (!ignoreUndo && machine != null)
		{
			if (flag)
			{
				list.Insert(0, new UndoActionAdd(machine, BlockInfo.FromBlockBehaviour(lastBlock)));
			}
			if (list.Count > 0)
			{
				if (ReferenceMaster.onMachineModified != null)
				{
					ReferenceMaster.onMachineModified(machine);
				}
				machine.UndoSystem.AddActions(list);
			}
		}
		_currentGhostController.SetNormal();
		return true;
	}

	protected void SingleHammerAnimate()
	{
		SingleHammerAnimate(mouseHitPos, hammerPos, hammerFwd);
	}

	public void SingleHammerAnimate(Vector3 hit, Vector3 pos, Vector3 fwd)
	{
		hammerAndNail.Animate(hit, pos, fwd);
	}

	protected void AddBlock(Transform block, BlockType t)
	{
		AddBlock(block.position, block.rotation, block, t);
	}

	protected void AddBlock(Vector3 pos, Quaternion rot, Transform block, BlockType t)
	{
		bool isFlipped = false;
		if (_currentGhostFlipped && block == activeGhost)
		{
			isFlipped = _currentGhostFlipped;
		}
		else if ((bool)_currentGhostArrow && t != BlockType.SteeringBlock && t != BlockType.SteeringHinge)
		{
			bool flag = CompareVectors(block.forward, -Vector3.right, 45f);
			isFlipped = ((flag == _currentGhostFlipped) ? _currentGhostFlipped : (!_currentGhostFlipped));
		}
		Machine.Active().AddBlockGlobal(pos, rot, t, isFlipped, out lastBlock);
	}

	protected void AddBrace(Transform block, BlockType t)
	{
		Machine machine = Machine.Active();
		if (!StatMaster.isMP)
		{
			machine.AddBlockGlobal(block.position, block.rotation, t, false, out lastBlock);
			return;
		}
		ServerMachine serverMachine = machine as ServerMachine;
		serverMachine.RemoteAddBlockGlobal(block.position, block.rotation, t, false, out lastBlock);
	}

	protected void AddSpring(Transform block, BlockType t)
	{
		Machine.Active().AddBlockGlobal(block.position, block.rotation, t, false, out lastBlock);
	}

	protected void RemoveBlock()
	{
		RemoveBlock(_hoveredBlock);
		BlockHoverOut();
		_hoveredBlock = null;
	}

	public void RemoveBlock(BlockBehaviour block, bool playSound = true)
	{
		if (!block || (block.Prefab.Type == BlockType.StartingBlock && Machine.Active().GetBlocks(BlockType.StartingBlock).Count <= 1))
		{
			return;
		}
		Machine machine = Machine.Active();
		Machine componentInParent = block.GetComponentInParent<Machine>();
		if (!componentInParent || machine != componentInParent || componentInParent.isLoadingInfo)
		{
			Debug.LogWarning("Invalid machine!");
			return;
		}
		List<UndoAction> list = new List<UndoAction>();
		if (block.IsSelected)
		{
			list.Add(new UndoActionDeselect(machine, block.Guid, block.IsSelectedExtra, block.SymmetryIndex, block.TransformMultiplier));
		}
		List<BlockBehaviour> allBlocks = new List<BlockBehaviour> { block };
		if (StatMaster.Mode.Symmetry.eraser)
		{
			machine.GetMirroredBlocks(block).ForEach(delegate(Tuple<BlockBehaviour, int> x)
			{
				if (!allBlocks.Contains(x.Item1))
				{
					allBlocks.Add(x.Item1);
				}
			});
		}
		else
		{
			List<Tuple<BlockBehaviour, int>> list2 = (from x in _hoveredBlock.ParentMachine.GetMirroredBlocks(_hoveredBlock)
				where !SelectedBlocks.Contains(x.Item1)
				select x).ToList();
			list2.ForEach(delegate(Tuple<BlockBehaviour, int> x)
			{
				if (!x.Item1.IsSelected && x.Item1.Prefab.hasBVC && x.Item1.VisualController.Highlighted)
				{
					x.Item1.VisualController.SetNoOutline();
				}
			});
		}
		bool flag = false;
		if (block.SurfaceType)
		{
			flag = componentInParent.nodeController.IsBuilding;
			componentInParent.nodeController.AddDependencies(allBlocks);
		}
		for (int num = allBlocks.Count - 1; num >= 0; num--)
		{
			BlockBehaviour blockBehaviour = allBlocks[num];
			if (!flag || !blockBehaviour.SurfaceType || !componentInParent.nodeController.IsUsingBlock(blockBehaviour))
			{
				BlockInfo blockInfo = BlockInfo.FromBlockBehaviour(blockBehaviour);
				componentInParent.RemoveBlock(blockBehaviour);
				list.Add(new UndoActionRemove(machine, blockInfo));
			}
		}
		componentInParent.UndoSystem.AddActions(list);
		PostRemoveBlock(componentInParent, playSound);
		if (ReferenceMaster.onBlockRemoved != null)
		{
			ReferenceMaster.onBlockRemoved(block.BlockID);
		}
	}

	public void PostRemoveBlock(Machine machine, bool playSound = true)
	{
		UpdateMiddleOfObject();
		if (ReferenceMaster.onMachineModified != null)
		{
			ReferenceMaster.onMachineModified(machine);
		}
		if (playSound && deleteSound != null)
		{
			deleteSound.Stop();
			deleteSound.Play();
		}
		machine.CheckBounds();
	}

	protected void DisableBlockGhosts()
	{
		for (int i = 0; i < PrefabMaster.BlockGhosts.Count; i++)
		{
			PrefabMaster.BlockGhosts[i].gameObject.SetActive(false);
		}
		blockGhostEnabled = false;
	}

	protected bool CompareVectors(Vector3 a, Vector3 b, float angleError)
	{
		if (!Mathf.Approximately(a.magnitude, b.magnitude))
		{
			return false;
		}
		float num = Mathf.Cos(angleError * ((float)Math.PI / 180f));
		float num2 = Vector3.Dot(a.normalized, b.normalized);
		return num2 >= num;
	}

	protected void OnDisable()
	{
		if (_currentGhost != null)
		{
			if ((bool)_currentGhostArrow)
			{
				_currentGhostArrow.localScale = new Vector3(Mathf.Abs(_currentGhostArrow.localScale.x), _currentGhostArrow.localScale.y, _currentGhostArrow.localScale.z);
			}
			_currentGhost.gameObject.SetActive(false);
		}
		ClearGhost();
	}
}
