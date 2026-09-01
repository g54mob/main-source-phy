using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BlockMapperInternal;
using InternalModding.Mapper;
using Modding.Mapper;
using Mono.CSharp;
using Selectors;
using UnityEngine;

[AddComponentMenu("UI/BlockMapper")]
public class BlockMapper : MonoBehaviour, IWidgetContainer
{
	public const float COMPONENT_Z = 0.1f;

	private const float CONTAINER_OFFSET_Y = 0.5f;

	private const float TRUE_CONTAINER_OFFSET_Y = 0.5555561f;

	private const float ENTITY_CUSTOMIZE_OFFSET_Y = -50f;

	private static MToggle logicToggle = new MToggle("Logic toggle", "logic-toggle", false);

	private static UIButton addLogicButton;

	private static Transform hudTransform;

	private static Vector3? previousPosition;

	private static bool isClosed = true;

	private static bool isClosing;

	private static AudioSource _audioSource;

	[HideInInspector]
	public static XDataHolder clipboard;

	private static BlockBehaviour blockClipboard;

	[HideInInspector]
	public static GenericEntity entityClipboard;

	public static Action onMapperClose;

	public static Action onMapperOpen;

	private bool _isDirty;

	[SerializeField]
	private DynamicText blockNameText;

	[SerializeField]
	protected float maxBlockNameWidth;

	[SerializeField]
	private AudioClip buttonClickSound;

	[SerializeField]
	private UIDrag dragWindow;

	[SerializeField]
	private Transform background;

	[SerializeField]
	private MeshRenderer voidspace;

	[SerializeField]
	private Transform cannotCustomizeText;

	[SerializeField]
	private UIButton copyButton;

	[SerializeField]
	private UIButton pasteButton;

	[SerializeField]
	private UIButton closeButton;

	[SerializeField]
	private UIButton resetButton;

	[SerializeField]
	private ContainerDetails container;

	[SerializeField]
	private UIScrollbar scrollbar;

	private KeyController keyController;

	private KeyController emulatedKeyController;

	private GenericController<MMenu> menuController;

	private GenericController<MMenu> footerMenuController;

	private GenericController<MToggle> toggleController;

	private GenericController<MValue> valueController;

	private GenericController<MSlider> sliderController;

	private GenericController<MColourSlider> colourSliderController;

	private GenericController<MToggle> limitToggleController;

	private GenericController<MLimits> limitController;

	private GenericController<MVisual> visualController;

	private GenericController<MText> textController;

	private GenericController<MTeam> teamController;

	private GenericController<MLogic> logicController;

	private CustomController customController;

	private WidgetController addLogicController;

	private WidgetController visualControllerCollapsed;

	private GenericController<MToggle> entityHeader;

	private WidgetController transformController;

	private WidgetController settingsHeaderController;

	private WidgetController entityNameController;

	private Vector3 closeStartPos;

	private Vector3 resetStartPos;

	private Vector3 nameStartPos;

	private bool lastScrollbarActive;

	private bool isRebuilding;

	private float nameYOffset;

	private readonly HashSet<string> _allVariables = new HashSet<string>();

	public static Action<MapperType> OnParameterChange;

	public static Action<UndoAction> OnParameterUndo;

	public static Transform lowerRight;

	public static Transform upperLeft;

	protected static int framesSinceButton = 0;

	private Camera hudCamera;

	public static BlockMapper CurrentInstance { get; private set; }

	public static bool IsOpen
	{
		get
		{
			return !isClosing && !isClosed;
		}
	}

	public static AudioSource AudioSource
	{
		get
		{
			if (_audioSource != null)
			{
				return _audioSource;
			}
			_audioSource = new GameObject("BlockMapperSound").AddComponent<AudioSource>();
			_audioSource.outputAudioMixerGroup = ReferenceMaster.GetMixer("UI");
			_audioSource.gameObject.hideFlags = HideFlags.HideInHierarchy;
			_audioSource.volume = 0.45f;
			_audioSource.pitch = 1.8f;
			_audioSource.clip = CurrentInstance.buttonClickSound;
			return _audioSource;
		}
	}

	public SaveableDataHolder Current { get; private set; }

	public GenericEntity Entity
	{
		get
		{
			return Current as GenericEntity;
		}
	}

	public BlockBehaviour Block
	{
		get
		{
			return Current as BlockBehaviour;
		}
	}

	public GenericDataHolder Holder
	{
		get
		{
			return Current as GenericDataHolder;
		}
	}

	public ParameterWidget PickTarget { get; set; }

	public bool PickSupportsBlocks { get; set; }

	public bool IsBlock
	{
		get
		{
			return Current is BlockBehaviour;
		}
	}

	public bool IsEntity
	{
		get
		{
			return Current is GenericEntity;
		}
	}

	public bool IsLogic
	{
		get
		{
			return !IsBlock && logicToggle.IsActive;
		}
	}

	public bool IsGenericHolder
	{
		get
		{
			return Current is GenericDataHolder;
		}
	}

	public KeyCode LastPressedKey { get; private set; }

	public bool IsDirty
	{
		get
		{
			return _isDirty;
		}
		set
		{
			_isDirty = value;
		}
	}

	public IWidgetContainer Container
	{
		get
		{
			object result;
			if (container != null)
			{
				IWidgetContainer widgetContainer = container;
				result = widgetContainer;
			}
			else
			{
				result = this;
			}
			return (IWidgetContainer)result;
		}
	}

	public UIButton CopyButton
	{
		get
		{
			return copyButton;
		}
	}

	public UIButton PasteButton
	{
		get
		{
			return pasteButton;
		}
	}

	public UIButton CloseButton
	{
		get
		{
			return closeButton;
		}
	}

	public UIButton ResetButton
	{
		get
		{
			return resetButton;
		}
	}

	public HashSet<string> AllVariables
	{
		get
		{
			return _allVariables;
		}
	}

	public float TopValue()
	{
		return base.transform.position.y;
	}

	public float ZValue()
	{
		return base.transform.position.z - 0.1f;
	}

	public void RefreshPickFields()
	{
		for (int i = 0; i < logicController.ContainerCount; i++)
		{
			LogicSelector logicSelector = logicController.containers[i].selector as LogicSelector;
			logicSelector.RefreshPickFields();
		}
	}

	private void Awake()
	{
		if (!lowerRight)
		{
			lowerRight = GameObject.FindWithTag("lowerRight").transform;
		}
		if (!upperLeft)
		{
			upperLeft = GameObject.FindWithTag("upperLeft").transform;
		}
		if (hudTransform == null)
		{
			hudTransform = GameObject.Find("HUD").transform;
		}
		base.transform.SetParent(hudTransform, false);
		Vector3? vector = previousPosition;
		if (vector.HasValue)
		{
			Transform obj = base.transform;
			Vector3? vector2 = previousPosition;
			obj.position = vector2.Value;
		}
		closeStartPos = closeButton.transform.localPosition;
		resetStartPos = resetButton.transform.localPosition;
		nameStartPos = blockNameText.transform.localPosition;
		lastScrollbarActive = false;
		keyController = base.gameObject.AddComponent<KeyController>();
		emulatedKeyController = base.gameObject.AddComponent<KeyController>();
		string text = "Prefabs/BlockMapper/";
		menuController = new GenericController<MMenu>(text + "MenuContainer");
		footerMenuController = new GenericController<MMenu>(text + "FooterMenuContainer");
		toggleController = new GenericController<MToggle>(text + "ToggleContainer");
		valueController = new GenericController<MValue>(text + "ValueContainer");
		sliderController = new GenericController<MSlider>(text + "SliderContainer");
		colourSliderController = new GenericController<MColourSlider>(text + "ColourSliderContainer");
		limitToggleController = new GenericController<MToggle>(text + "ToggleContainer");
		limitController = new GenericController<MLimits>(text + "LimitsContainer");
		visualController = new GenericController<MVisual>(text + "VisualContainer");
		visualControllerCollapsed = new WidgetController(text + "VisualContainerCollapsed");
		teamController = new GenericController<MTeam>(text + "TeamContainer");
		textController = new GenericController<MText>(text + "TextContainer");
		customController = new CustomController(text + "CustomContainer");
		string text2 = text + "LevelEditor/";
		entityHeader = new GenericController<MToggle>(text2 + "LogicToggleContainer");
		entityNameController = new WidgetController(text2 + "EntityNameContainer");
		transformController = new WidgetController(text2 + "TransformContainer");
		settingsHeaderController = new WidgetController(text2 + "SettingsHeader");
		logicController = new GenericController<MLogic>(text2 + "LogicContainer");
		addLogicController = new WidgetController(text2 + "AddLogicContainer");
		dragWindow.DragEnded += UpdateBackground;
		closeButton.Click += Close;
		copyButton.Click += Copy;
		pasteButton.Click += Paste;
		resetButton.Click += Reset;
	}

	private void ShowLogic()
	{
		logicController.Display(Container, entityHeader.EndPosition);
		addLogicController.Display(Container, logicController.EndPosition);
		UIButton componentInChildren = addLogicController.Widget.GetComponentInChildren<UIButton>();
		if (addLogicButton != componentInChildren)
		{
			UpdateAddButton(componentInChildren);
		}
	}

	private void UpdateAddButton(UIButton addButton)
	{
		addLogicButton = addButton;
		addLogicButton.ResetDelegates();
		addButton.Click += OnAddLogic;
	}

	private void ShowMapper()
	{
		menuController.Display(Container, keyController.EndPosition);
		toggleController.Display(Container, menuController.EndPosition);
		valueController.Display(Container, toggleController.EndPosition);
		sliderController.Display(Container, valueController.EndPosition);
		colourSliderController.Display(Container, sliderController.EndPosition);
		emulatedKeyController.Display(Container, colourSliderController.EndPosition);
		limitToggleController.Display(Container, emulatedKeyController.EndPosition);
		limitController.Display(Container, limitToggleController.EndPosition);
		visualController.Display(Container, limitController.EndPosition);
		visualControllerCollapsed.Display(Container, visualController.EndPosition);
		textController.Display(Container, visualControllerCollapsed.EndPosition);
		teamController.Display(Container, textController.EndPosition);
		customController.Display(Container, teamController.EndPosition);
		footerMenuController.Display(Container, customController.EndPosition);
	}

	public void OnAddLogic()
	{
		EditLogicHandler instance = EditLogicHandler.Instance;
		if ((bool)instance)
		{
			instance.OnAddLogic();
		}
	}

	public void ClearWidgets()
	{
		keyController.Clear();
		entityHeader.Clear();
		menuController.Clear();
		toggleController.Clear();
		valueController.Clear();
		sliderController.Clear();
		colourSliderController.Clear();
		limitToggleController.Clear();
		limitController.Clear();
		visualController.Clear();
		visualControllerCollapsed.Clear();
		textController.Clear();
		teamController.Clear();
		emulatedKeyController.Clear();
		customController.Clear();
		footerMenuController.Clear();
		logicController.Clear();
		entityNameController.Clear();
		transformController.Clear();
		settingsHeaderController.Clear();
		addLogicController.Clear();
	}

	public void ToggleLogic(bool toggle)
	{
		logicToggle.IsActive = toggle;
	}

	private void OnToggleLogic(bool toggle)
	{
		IsDirty = true;
	}

	private void LateUpdate()
	{
		framesSinceButton++;
		if (Current == null)
		{
			Close();
			return;
		}
		if (!StatMaster.stopHotkeys)
		{
			if (InputManager.CopyKeys())
			{
				Copy();
			}
			if (InputManager.PasteKeys())
			{
				Paste();
			}
		}
		if (IsDirty && !isRebuilding)
		{
			Rebuild();
		}
	}

	private void UpdateBackground()
	{
		float num = 0.2f;
		float num2 = num * base.transform.localScale.x * 0.75f;
		float num3 = base.transform.localScale.x * background.localScale.x / 2f;
		Vector3 position = base.transform.position;
		bool flag = false;
		if (position.x + 0.01f >= lowerRight.position.x - num3 - num2 && position.y > Mathf.Lerp(lowerRight.position.y, upperLeft.position.y, 0.7f))
		{
			flag = true;
		}
		if (flag)
		{
			float num4 = upperLeft.position.y - lowerRight.position.y;
			num4 = Mathf.Abs(num4 / base.transform.localScale.y);
			SetScrollHeight(num4 - 0.5555561f);
			background.localScale = new Vector3(background.localScale.x, num4, background.localScale.z);
			background.localPosition = new Vector3(background.localPosition.x, (0f - background.localScale.y) * 0.5f, background.localPosition.z);
			position = base.transform.position;
			position.y = upperLeft.position.y;
			base.transform.position = position;
			scrollbar.UpdateBounds();
			position = base.transform.position;
			position.x = lowerRight.position.x - num3 - ((!scrollbar.active) ? 0f : num2);
			base.transform.position = position;
			bool flag2 = false;
			WidgetController widgetController = null;
			WidgetController widgetController2 = null;
			if (IsBlock)
			{
				if (!scrollbar.active)
				{
					float num5 = num4 * base.transform.localScale.y - 0.5f;
					visualController.UpdateDisplay(Container, num5 - visualController.Height);
					visualControllerCollapsed.UpdateDisplay(Container, num5 - visualControllerCollapsed.Height);
					flag2 = true;
					widgetController = limitController;
					widgetController2 = ((!(visualController.Height > 0f)) ? visualControllerCollapsed : visualController);
				}
				else
				{
					visualController.Display(Container, limitController.EndPosition);
					visualControllerCollapsed.Display(Container, visualController.EndPosition);
					flag2 = false;
				}
			}
			else if (!scrollbar.active)
			{
				flag2 = true;
				widgetController = ((!IsLogic) ? footerMenuController : addLogicController);
				widgetController2 = visualController;
			}
			else
			{
				flag2 = false;
			}
			if (flag2)
			{
				voidspace.gameObject.SetActive(true);
				float start = TopValue() - widgetController.EndPosition - 0.5555561f * base.transform.localScale.y;
				float end = TopValue() - num4 * base.transform.localScale.y;
				if (widgetController2.Height > 0f)
				{
					end = TopValue() - widgetController2.EndPosition + widgetController2.Height - 0.5555561f * base.transform.localScale.y;
				}
				SetVoid(start, end);
			}
			else
			{
				voidspace.gameObject.SetActive(false);
			}
		}
		else
		{
			SetScrollHeight(8f);
			if (IsBlock)
			{
				visualController.Display(Container, limitController.EndPosition);
				visualControllerCollapsed.Display(Container, visualController.EndPosition);
			}
			voidspace.gameObject.SetActive(false);
			if (cannotCustomizeText == null)
			{
				float num6 = ((IsLogic ? addLogicController.EndPosition : footerMenuController.EndPosition) + 0.5f) / base.transform.localScale.y;
				float num7 = scrollbar.contentMask.localScale.y + 0.5f / base.transform.localScale.y;
				if (num6 > num7)
				{
					num6 = num7;
				}
				background.localScale = new Vector3(background.localScale.x, num6, background.localScale.z);
				background.localPosition = new Vector3(background.localPosition.x, (0f - background.localScale.y) * 0.5f, background.localPosition.z);
			}
			scrollbar.UpdateBounds();
		}
		SetLayerDepth(flag);
		bool active = scrollbar.active;
		if (active != lastScrollbarActive)
		{
			closeButton.transform.localPosition = closeStartPos + ((!active) ? 0f : num) * Vector3.right;
			resetButton.transform.localPosition = resetStartPos + ((!active) ? 0f : num) * Vector3.right;
		}
		UpdateTitleTextPos(active);
	}

	private void SetScrollHeight(float height)
	{
		scrollbar.contentMask.localPosition = new Vector3(scrollbar.contentMask.localPosition.x, (0f - height) / 2f, scrollbar.contentMask.localPosition.z);
		scrollbar.contentMask.localScale = new Vector3(scrollbar.contentMask.localScale.x, height, scrollbar.contentMask.localScale.z);
		scrollbar.transform.localPosition = new Vector3(scrollbar.transform.localPosition.x, (0f - height) / 2f, scrollbar.transform.localPosition.z);
		scrollbar.scrollBG.localScale = new Vector3(scrollbar.scrollBG.localScale.x, height, scrollbar.scrollBG.localScale.z);
		BoxCollider component = scrollbar.GetComponent<BoxCollider>();
		component.size = new Vector3(component.size.x, height, component.size.z);
		Transform child = scrollbar.transform.GetChild(0);
		child.localPosition = new Vector3(child.localPosition.x, (height + child.localScale.y) / 2f, child.localPosition.z);
	}

	private void SetVoid(float start, float end)
	{
		Transform transform = voidspace.transform;
		float num = Mathf.Abs(end - start) / base.transform.localScale.y;
		transform.position = new Vector3(transform.position.x, (start + end) / 2f, transform.position.z);
		transform.localScale = new Vector3(transform.localScale.x, num, transform.localScale.z);
		num /= transform.localScale.x;
		voidspace.sharedMaterial.mainTextureScale = voidspace.sharedMaterial.mainTextureScale.x * new Vector2(1f, num);
	}

	private void SetLayerDepth(bool snap)
	{
		AssignBlurTest component = GetComponent<AssignBlurTest>();
		component.camera.depth = ((!snap) ? 1f : 0.5f);
		EnableCam component2 = GetComponent<EnableCam>();
		component2.target.depth = ((!snap) ? 1.05f : 0.55f);
	}

	private void UpdateTitleTextPos(bool barActive)
	{
		float num = ((!(cannotCustomizeText == null) || nameYOffset == 0f) ? nameYOffset : (nameYOffset - 0.02f));
		Vector3 vector = new Vector3(nameStartPos.x, nameStartPos.y + num, nameStartPos.z);
		blockNameText.transform.localPosition = vector + ((!barActive) ? 0f : 0.1f) * Vector3.right;
		lastScrollbarActive = barActive;
	}

	public void RefreshLists()
	{
		if (!IsBlock && !IsGenericHolder)
		{
			entityHeader.RegisterToggle(logicToggle);
			int num = 0;
			if (Entity.DisplayNameWidget())
			{
				entityNameController.RegisterToggle(Entity);
				num = 2;
			}
			transformController.RegisterToggle(Current);
			if (Current.MapperTypes.Count > num)
			{
				settingsHeaderController.RegisterToggle(Current);
			}
			DestroyCannotCustomizeText();
		}
		if (!IsLogic)
		{
			foreach (MapperType mapperType in Current.MapperTypes)
			{
				mapperType.DisplayStateChanged += delegate
				{
					IsDirty = true;
				};
				mapperType.NameChanged += delegate
				{
					IsDirty = true;
				};
				MKey mKey = mapperType as MKey;
				if (mKey != null)
				{
					DestroyCannotCustomizeText();
					if (!mKey.isEmulator)
					{
						keyController.RegisterKey(mKey);
					}
					else
					{
						emulatedKeyController.RegisterKey(mKey);
					}
					continue;
				}
				MMenu mMenu = mapperType as MMenu;
				if (mMenu != null)
				{
					DestroyCannotCustomizeText();
					if (!mMenu.isFooterMenu)
					{
						menuController.RegisterToggle(mMenu);
					}
					else
					{
						footerMenuController.RegisterToggle(mMenu);
					}
					continue;
				}
				MToggle mToggle = mapperType as MToggle;
				if (mToggle != null)
				{
					DestroyCannotCustomizeText();
					((!(mToggle.Key == "uselimits")) ? toggleController : limitToggleController).RegisterToggle(mToggle);
					continue;
				}
				MValue mValue = mapperType as MValue;
				if (mValue != null)
				{
					DestroyCannotCustomizeText();
					valueController.RegisterToggle(mValue);
					continue;
				}
				MSlider mSlider = mapperType as MSlider;
				if (mSlider != null)
				{
					DestroyCannotCustomizeText();
					sliderController.RegisterToggle(mSlider);
					continue;
				}
				MColourSlider mColourSlider = mapperType as MColourSlider;
				if (mColourSlider != null)
				{
					DestroyCannotCustomizeText();
					colourSliderController.RegisterToggle(mColourSlider);
					continue;
				}
				MLimits mLimits = mapperType as MLimits;
				if (mLimits != null)
				{
					DestroyCannotCustomizeText();
					limitController.RegisterToggle(mLimits);
					continue;
				}
				MText mText = mapperType as MText;
				if (mText != null)
				{
					DestroyCannotCustomizeText();
					textController.RegisterToggle(mText);
					continue;
				}
				MTeam mTeam = mapperType as MTeam;
				if (mTeam != null)
				{
					DestroyCannotCustomizeText();
					teamController.RegisterToggle(mTeam);
				}
				else if (CustomMapperTypes.IsCustomMapperType(mapperType) && (OneSelected() || CustomMapperTypes.IsSupportsMultiple(mapperType)))
				{
					DestroyCannotCustomizeText();
					customController.RegisterToggle(mapperType);
				}
			}
			if (!OptionsMaster.skinsEnabled || !IsBlock || !OneSelected())
			{
				return;
			}
			if (StatMaster.collapseSkinMapper)
			{
				DestroyCannotCustomizeText();
				visualControllerCollapsed.RegisterToggle(Current);
				return;
			}
			BlockVisualController blockVisualController = Block.VisualController;
			if (Block.Prefab.hasBVC && blockVisualController.Prefab.CanGetNewVisuals && blockVisualController.Options.Count > 1)
			{
				MVisual mVisual = Block.Visual;
				if (mVisual == null)
				{
					mVisual = (Block.Visual = new MVisual(blockVisualController, blockVisualController.Options.IndexOf(blockVisualController.selectedSkin), blockVisualController.Options, "_CurrentSkin"));
				}
				else
				{
					mVisual.Items = blockVisualController.Options;
					int num2 = blockVisualController.Options.IndexOf(blockVisualController.selectedSkin);
					mVisual.Value = ((num2 != -1) ? num2 : 0);
					mVisual.DisplayName = mVisual.Selection.pack.name.ToUpper();
				}
				DestroyCannotCustomizeText();
				visualController.RegisterToggle(mVisual);
			}
			return;
		}
		for (int num3 = 0; num3 < Entity.logicData.Count; num3++)
		{
			EntityLogic entityLogic = Entity.logicData[num3];
			if (!entityLogic.hasMLogic)
			{
				entityLogic.mLogic = new MLogic(string.Empty, "logic" + entityLogic.ID, entityLogic);
				entityLogic.hasMLogic = true;
			}
			logicController.RegisterToggle(entityLogic.mLogic);
		}
		if (Entity.TriggerTypeCount() > 0)
		{
			addLogicController.RegisterToggle();
		}
	}

	public void Copy()
	{
		EditFieldHandler instance = EditFieldHandler.Instance;
		if (!instance)
		{
			clipboard = new XDataHolder();
			Current.OnSave(clipboard);
			if (IsBlock)
			{
				clipboard = clipboard.Clone();
				clipboard.EraseCustomBlockData();
			}
		}
		if (IsBlock)
		{
			blockClipboard = Block;
		}
		else if (!IsGenericHolder)
		{
			entityClipboard = Entity;
		}
		if (framesSinceButton > 2)
		{
			AudioSource.Play();
		}
		framesSinceButton = 0;
	}

	public void Paste()
	{
		EditFieldHandler instance = EditFieldHandler.Instance;
		if ((bool)instance)
		{
			if (IsBlock)
			{
				if (blockClipboard != null)
				{
					instance.OnPaste(blockClipboard, CopyMode.All);
					ParameterChange(blockClipboard);
				}
			}
			else if (!IsGenericHolder && entityClipboard != null)
			{
				instance.OnPaste(entityClipboard, (!IsLogic) ? CopyMode.Settings : CopyMode.Logic);
			}
		}
		else if (clipboard != null)
		{
			if (IsBlock)
			{
				List<BlockBehaviour> machineSelection = AdvancedBlockEditor.Instance.selectionController.MachineSelection;
				if (!machineSelection.Contains(Block))
				{
					machineSelection.Add(Block);
				}
				List<UndoAction> list = new List<UndoAction>();
				Machine parentMachine = Block.ParentMachine;
				for (int i = 0; i < machineSelection.Count; i++)
				{
					BlockBehaviour blockBehaviour = machineSelection[i];
					int blockID = blockBehaviour.BlockID;
					if (blockID == 71 || blockID == 72)
					{
						continue;
					}
					BlockInfo prevInfo = BlockInfo.FromBlockBehaviour(blockBehaviour);
					StatMaster.isPaste = true;
					blockBehaviour.isBMAction = true;
					if (blockClipboard.Prefab.Type != blockBehaviour.Prefab.Type)
					{
						XData.Clamp = true;
					}
					XDataHolder data = clipboard.Clone();
					blockBehaviour.OnLoad(data);
					XDataHolder data2 = new XDataHolder();
					blockBehaviour.OnSave(data2);
					XData.Clamp = false;
					if (OptionsMaster.skinsEnabled && !StatMaster.collapseSkinMapper && blockClipboard.Prefab.CanGetNewVisuals)
					{
						BlockSkinLoader.SkinPack pack = blockClipboard.VisualController.selectedSkin.pack;
						BlockSkinLoader.SkinPack.Skin selectedSkin = blockBehaviour.VisualController.selectedSkin;
						if (selectedSkin.pack != pack)
						{
							BlockSkinLoader.SkinPack.Skin skin = blockBehaviour.VisualController.FindVisualOptionFor(pack);
							if (skin != null && skin != selectedSkin)
							{
								blockBehaviour.VisualController.ReplaceSkin(skin);
							}
						}
					}
					blockBehaviour.isBMAction = false;
					list.Add(new UndoActionEdit(parentMachine, BlockInfo.FromBlockBehaviour(blockBehaviour), prevInfo));
					ParameterChange(blockBehaviour);
				}
				StatMaster.isPaste = false;
				parentMachine.UndoSystem.AddActionsWithTool(list);
			}
			else
			{
				StatMaster.isPaste = true;
				Current.isBMAction = true;
				Current.OnLoad(clipboard);
				StatMaster.isPaste = false;
				Current.isBMAction = false;
			}
			Refresh();
		}
		if (framesSinceButton > 2)
		{
			AudioSource.Play();
		}
		framesSinceButton = 0;
	}

	private void ParameterChange(BlockBehaviour block)
	{
		if (OnParameterChange != null)
		{
			MapperType mapperType = block.GetMapperType("bmt-mass");
			if (mapperType == null)
			{
				mapperType = block.GetMapperType("bmt-buoyancy");
			}
			if (mapperType == null)
			{
				mapperType = block.GetMapperType("bmt-inertia");
			}
			if (mapperType == null)
			{
				mapperType = block.GetMapperType("bmt-hascolliders");
			}
			if (mapperType == null)
			{
				mapperType = block.GetMapperType("bmt-opt-collider");
			}
			if (mapperType == null)
			{
				mapperType = block.GetMapperType("bmt-opt-surfmat");
			}
			if (mapperType != null)
			{
				OnParameterChange(mapperType);
			}
		}
	}

	public void Refresh()
	{
		Machine machine = Machine.Active();
		if (!(machine != null) || !machine.IsLoadingMachine)
		{
			Open(Current);
		}
	}

	public void Reset()
	{
		EditFieldHandler instance = EditFieldHandler.Instance;
		if ((bool)instance)
		{
			instance.OnReset();
		}
		else if (IsBlock)
		{
			List<BlockBehaviour> machineSelection = AdvancedBlockEditor.Instance.selectionController.MachineSelection;
			if (!machineSelection.Contains(Block))
			{
				machineSelection.Add(Block);
			}
			Machine parentMachine = Block.ParentMachine;
			List<UndoAction> list = new List<UndoAction>();
			for (int i = 0; i < machineSelection.Count; i++)
			{
				BlockBehaviour blockBehaviour = machineSelection[i];
				BlockInfo prevInfo = BlockInfo.FromBlockBehaviour(blockBehaviour);
				blockBehaviour.isBMAction = true;
				XDataHolder xDataHolder = blockBehaviour.InitialState.Clone();
				xDataHolder.EraseCustomBlockData();
				blockBehaviour.OnLoad(Current.InitialState);
				blockBehaviour.OnReset();
				blockBehaviour.VisualController.UpdateVisFromPack(BlockSkinLoader.defaultPack);
				blockBehaviour.isBMAction = false;
				list.Add(new UndoActionEdit(parentMachine, BlockInfo.FromBlockBehaviour(blockBehaviour), prevInfo));
				ParameterChange(blockBehaviour);
			}
			Refresh();
			parentMachine.UndoSystem.AddActionsWithTool(list);
		}
		else
		{
			Current.isBMAction = true;
			XDataHolder xDataHolder2 = Current.InitialState.Clone();
			xDataHolder2.EraseCustomBlockData();
			Current.OnLoad(Current.InitialState);
			Current.OnReset();
			Current.isBMAction = false;
			Refresh();
		}
		if (framesSinceButton > 2)
		{
			AudioSource.Play();
		}
		framesSinceButton = 0;
	}

	public void Rebuild()
	{
		isRebuilding = true;
		StatMaster.Mode.pickMode = StatMaster.Mode.PickMode.None;
		ClearWidgets();
		RefreshLists();
		if (!IsBlock)
		{
			entityHeader.Display(Container, 0f);
		}
		if (!IsLogic)
		{
			entityNameController.Display(Container, entityHeader.EndPosition);
			transformController.Display(Container, entityNameController.EndPosition);
			settingsHeaderController.Display(Container, transformController.EndPosition);
			keyController.Display(Container, (!IsBlock) ? settingsHeaderController.EndPosition : 0f);
			ShowMapper();
		}
		else
		{
			ShowLogic();
		}
		UpdateDynamicTextImmediately();
		UpdateBackground();
		isRebuilding = false;
		IsDirty = false;
	}

	private void UpdateDynamicTextImmediately(bool setCam = false)
	{
		DynamicText[] componentsInChildren = GetComponentsInChildren<DynamicText>();
		if (hudCamera == null)
		{
			hudCamera = GameObject.Find("HUD Cam").GetComponent<Camera>();
		}
		DynamicText[] array = componentsInChildren;
		foreach (DynamicText dynamicText in array)
		{
			dynamicText.GenerateMesh();
			if (dynamicText.cam != hudCamera)
			{
				dynamicText.cam = hudCamera;
			}
		}
	}

	private void DestroyCannotCustomizeText()
	{
		if (cannotCustomizeText != null)
		{
			UnityEngine.Object.Destroy(cannotCustomizeText.gameObject);
		}
		cannotCustomizeText = null;
	}

	public void SetBlockName(string name)
	{
		string[] array = Regex.Split(name, "\r\n|\r|\n");
		nameYOffset = ((array.Length <= 1) ? 0f : 0.115f);
		string text = name.ToUpper();
		string text2 = blockNameText.GetText();
		if (!text2.Equals(text))
		{
			blockNameText.transform.localScale = Vector3.one;
			blockNameText.SetText(text);
			Renderer component = blockNameText.GetComponent<Renderer>();
			float x = component.bounds.size.x;
			if (x > maxBlockNameWidth)
			{
				float num = maxBlockNameWidth / x;
				blockNameText.transform.localScale = new Vector3(num, num, num);
			}
			UpdateTitleTextPos(scrollbar.active);
		}
	}

	public static void SkinToggled(BlockSkinLoader.SModifier m)
	{
		if (!(CurrentInstance == null) && m != null && !(m is BlockSkinLoader.SkinPack.Skin) && !(m is BlockSkinLoader.SkinPack))
		{
			CurrentInstance.Refresh();
		}
	}

	public static BlockMapper Open(SaveableDataHolder obj)
	{
		if (CurrentInstance != null)
		{
			Close(true);
		}
		OverviewBlockMapper.Close();
		if (obj == null)
		{
			Debug.LogWarning("Trying to open BlockMapper, but target is null!");
			return null;
		}
		if (StatMaster.isLoadingLevels || BesiegeEntryPoint.transitioning || FadeScreen.faded)
		{
			return null;
		}
		obj.OnMapperOpen();
		if (!obj.IsModifying)
		{
			return null;
		}
		if (obj.infoType == BasicInfo.BasicInfoType.Block)
		{
			Machine machine = Machine.Active();
			foreach (Tuple<BlockBehaviour, int> mirroredBlock in machine.GetMirroredBlocks(obj as BlockBehaviour))
			{
				if (!(mirroredBlock.Item1 == obj))
				{
					mirroredBlock.Item1.OnMapperOpen();
				}
			}
		}
		CurrentInstance = UnityEngine.Object.Instantiate(Resources.Load<BlockMapper>("Prefabs/BlockMapper/BlockMapper"));
		if (CurrentInstance == null)
		{
			Debug.LogWarning("Couldn't instantiate BlockMapper, CurrentInstance is null!");
			return null;
		}
		CurrentInstance.name = "BlockMapper - " + obj.name;
		CurrentInstance.Current = obj;
		if (CurrentInstance.IsBlock)
		{
			BlockSkinLoader.SkinModified += SkinToggled;
		}
		else if (!CurrentInstance.IsGenericHolder)
		{
			logicToggle.Toggled += CurrentInstance.OnToggleLogic;
		}
		isClosed = false;
		CurrentInstance.FindAllVariables();
		CurrentInstance.Rebuild();
		CurrentInstance.UpdateDynamicTextImmediately(true);
		CurrentInstance.UpdateBackground();
		if (onMapperOpen != null)
		{
			onMapperOpen();
		}
		return CurrentInstance;
	}

	public static void Pick(GameObject pickedObject)
	{
		if (!(CurrentInstance == null))
		{
			CurrentInstance.PickTarget.Pick(pickedObject);
		}
	}

	public static void Close()
	{
		Close(false);
	}

	public static void Close(bool isRefresh)
	{
		if (CurrentInstance == null)
		{
			return;
		}
		isClosing = true;
		if (!isRefresh && onMapperClose != null)
		{
			onMapperClose();
		}
		if (CurrentInstance.IsBlock)
		{
			BlockSkinLoader.SkinModified -= SkinToggled;
		}
		else if (!CurrentInstance.IsGenericHolder)
		{
			logicToggle.Toggled -= CurrentInstance.OnToggleLogic;
			EditLogicHandler instance = EditLogicHandler.Instance;
			if ((bool)instance)
			{
				instance.OnCloseMapper();
			}
			StatMaster.Mode.pickMode = StatMaster.Mode.PickMode.None;
		}
		EditFieldHandler instance2 = EditFieldHandler.Instance;
		if ((bool)instance2)
		{
			instance2.OnCloseMapper();
		}
		CurrentInstance.Current.OnMapperClose();
		if (CurrentInstance.IsBlock)
		{
			Machine machine = Machine.Active();
			if ((bool)machine)
			{
				foreach (Tuple<BlockBehaviour, int> mirroredBlock in machine.GetMirroredBlocks(CurrentInstance.Block))
				{
					if (!(mirroredBlock.Item1 == CurrentInstance.Current))
					{
						mirroredBlock.Item1.OnMapperClose();
					}
				}
			}
			if (!isRefresh && !Machine.Active().isSimulating && StatMaster.Mode.selectedTool == StatMaster.Tool.Modify)
			{
				AdvancedBlockEditor.Instance.selectionController.DeselectAll(true);
			}
		}
		CurrentInstance.ClearWidgets();
		previousPosition = CurrentInstance.transform.position;
		if (framesSinceButton > 2)
		{
			AudioSource.Play();
		}
		framesSinceButton = 0;
		CurrentInstance.SetLayerDepth(false);
		UnityEngine.Object.Destroy(CurrentInstance.gameObject);
		CurrentInstance = null;
		isClosing = false;
		isClosed = true;
	}

	private static bool IsOwner(ushort playerId)
	{
		return playerId == BesiegeNetworkManager.Instance.PlayerID;
	}

	public static void OnEditField(SaveableDataHolder dataHolder, MapperType mapperType)
	{
		EditFieldHandler instance = EditFieldHandler.Instance;
		if ((bool)instance)
		{
			instance.OnEditField(dataHolder, mapperType);
		}
		else if (dataHolder.infoType == BasicInfo.BasicInfoType.Block)
		{
			BlockBehaviour blockBehaviour = null;
			BlockBehaviour blockBehaviour2 = dataHolder as BlockBehaviour;
			Machine parentMachine = blockBehaviour2.ParentMachine;
			List<BlockBehaviour> machineSelection = AdvancedBlockEditor.Instance.selectionController.MachineSelection;
			if (!machineSelection.Contains(blockBehaviour2))
			{
				machineSelection.Add(blockBehaviour2);
			}
			Dictionary<BlockBehaviour, Tuple<XData, XData>> dictionary = new Dictionary<BlockBehaviour, Tuple<XData, XData>>();
			XData xData = mapperType.Serialize();
			for (int i = 0; i < machineSelection.Count; i++)
			{
				BlockBehaviour blockBehaviour3 = machineSelection[i];
				BlockType type = blockBehaviour3.Prefab.Type;
				if (type == BlockType.BuildNode || type == BlockType.BuildEdge)
				{
					continue;
				}
				if (dictionary.ContainsKey(machineSelection[i]))
				{
					Debug.LogError("[BlockMapper]: OnEditField already added an undo state for " + machineSelection[i]);
					continue;
				}
				dictionary.Add(machineSelection[i], new Tuple<XData, XData>(machineSelection[i].GetLoadData(xData.Key), xData));
				MapperType mapperType2 = machineSelection[i].GetMapperType(mapperType.Key);
				if (mapperType2 != null)
				{
					mapperType2.DeSerialize(xData);
				}
			}
			mapperType.ApplyValue();
			for (int j = 0; j < machineSelection.Count; j++)
			{
				blockBehaviour = machineSelection[j];
				XData.Clamp = true;
				blockBehaviour.Load(xData);
				XDataHolder data = new XDataHolder();
				blockBehaviour.OnSave(data);
				XData.Clamp = false;
			}
			parentMachine.UndoSystem.AddAction(new UndoActionField(parentMachine, dictionary));
		}
		else
		{
			mapperType.ApplyValue();
		}
		if (OnParameterChange != null)
		{
			OnParameterChange(mapperType);
		}
	}

	public static void EditField(List<BlockBehaviour> blockList, SaveableDataHolder dataHolder, MapperType mapperType)
	{
		BlockBehaviour blockBehaviour = null;
		Machine parentMachine = (dataHolder as BlockBehaviour).ParentMachine;
		Dictionary<BlockBehaviour, Tuple<XData, XData>> dictionary = new Dictionary<BlockBehaviour, Tuple<XData, XData>>();
		XData xData = mapperType.Serialize();
		if (!blockList.Contains(dataHolder as BlockBehaviour))
		{
			blockList.Add(dataHolder as BlockBehaviour);
		}
		for (int i = 0; i < blockList.Count; i++)
		{
			dictionary.Add(blockList[i], new Tuple<XData, XData>(blockList[i].GetLoadData(xData.Key), xData));
			MapperType mapperType2 = blockList[i].GetMapperType(mapperType.Key);
			if (mapperType2 != null)
			{
				mapperType2.DeSerialize(xData);
			}
		}
		mapperType.ApplyValue();
		for (int j = 0; j < blockList.Count; j++)
		{
			blockBehaviour = blockList[j];
			XData.Clamp = true;
			blockBehaviour.Load(xData);
			XDataHolder data = new XDataHolder();
			blockBehaviour.OnSave(data);
			XData.Clamp = false;
		}
		parentMachine.UndoSystem.AddAction(new UndoActionField(parentMachine, dictionary));
		if (OnParameterChange != null)
		{
			OnParameterChange(mapperType);
		}
	}

	public static void UpdateBlockData(ushort playerId, byte[] inData)
	{
		byte[] array = CLZF2.Decompress(inData);
		int num = 0;
		if (StatMaster.isClient)
		{
			playerId = NetworkCompression.ReadUInt16(array, 0);
			byte[] array2 = new byte[array.Length - 2];
			Buffer.BlockCopy(array, 2, array2, 0, array2.Length);
			array = array2;
		}
		Dictionary<BlockBehaviour, Tuple<XData, XData>> dictionary = new Dictionary<BlockBehaviour, Tuple<XData, XData>>();
		int num2 = array[num++];
		bool flag = (num2 & 1) != 0;
		bool flag2 = (num2 & 2) != 0;
		int count;
		num += NetworkCompression.UnpackUInt(array, num, false, out count);
		BlockBehaviour blockBehaviour = null;
		ServerMachine machine;
		if (!NetworkScene.Instance.GetMachine(playerId, out machine))
		{
			return;
		}
		XData xData;
		for (int i = 0; i < count; i++)
		{
			int num3 = (int)NetworkCompression.ReadUInt(false, array, num);
			num += 4;
			BlockBehaviour block;
			if (machine.GetBlockFromIndex(num3, out block))
			{
				if (i == count - 1)
				{
					blockBehaviour = block;
				}
				if (!flag)
				{
					num += XDataHolder.DecodeXData(array, num, out xData);
					dictionary.Add(block, new Tuple<XData, XData>(block.GetLoadData(xData.Key), xData));
				}
				else
				{
					dictionary.Add(block, null);
				}
				continue;
			}
			Debug.LogError("Couldn't find block " + num3 + " on machine " + machine.name);
			return;
		}
		if (flag)
		{
			num += XDataHolder.DecodeXData(array, num, out xData);
			List<BlockBehaviour> list = new List<BlockBehaviour>(dictionary.Keys);
			for (int j = 0; j < list.Count; j++)
			{
				BlockBehaviour blockBehaviour2 = list[j];
				dictionary[blockBehaviour2] = new Tuple<XData, XData>(blockBehaviour2.GetLoadData(xData.Key), xData);
			}
		}
		foreach (KeyValuePair<BlockBehaviour, Tuple<XData, XData>> item in dictionary)
		{
			BlockBehaviour key = item.Key;
			xData = item.Value.Item2;
			XData.Clamp = !flag2;
			if (key.Load(xData))
			{
				XDataHolder data = new XDataHolder();
				key.OnSave(data);
			}
			XData.Clamp = false;
		}
		bool flag3 = IsOwner(playerId);
		bool flag4 = flag3 && !flag2;
		if (flag3)
		{
			OverviewBlockMapper currentInstance = OverviewBlockMapper.CurrentInstance;
			BlockMapper currentInstance2 = CurrentInstance;
			KeyValuePair<BlockBehaviour, Tuple<XData, XData>> keyValuePair = dictionary.First();
			if (currentInstance != null)
			{
				MKey mKey = blockBehaviour.GetMapperType(keyValuePair.Value.Item2.Key) as MKey;
				if (mKey != null)
				{
					currentInstance.OnEditBlockKey(blockBehaviour, mKey);
				}
			}
			else if (!currentInstance2 || currentInstance2.Current != blockBehaviour)
			{
				AdvancedBlockEditor.Instance.SetActiveTool(StatMaster.Tool.Modify, false);
				Open(blockBehaviour);
			}
		}
		if (flag4)
		{
			machine.UndoSystem.AddAction(new UndoActionField(machine, dictionary));
		}
		if (StatMaster.isHosting)
		{
			byte[] array2 = new byte[2 + array.Length];
			NetworkCompression.WriteUInt16(playerId, array2, 0);
			Buffer.BlockCopy(array, 0, array2, 2, array.Length);
			NetworkAuxAddPiece.Instance.SendFragmentedNetworkMessage(RPCMessageType.EditBlock, CLZF2.Compress(array2));
		}
	}

	public static void UpdateEntityData(ushort playerId, byte[] data, int offset)
	{
		if (!LevelEditor.Instance.isActive)
		{
			return;
		}
		if (StatMaster.isClient)
		{
			playerId = NetworkCompression.ReadUInt16(data, offset);
			byte[] array = new byte[data.Length - 2];
			Buffer.BlockCopy(data, 2, array, 0, array.Length);
			data = array;
		}
		List<SaveableDataHolder> list = new List<SaveableDataHolder>();
		long id = BitConverter.ToInt64(data, 0);
		offset += LevelEntity.ID_LENGTH;
		LevelEntity entity;
		if (!LevelEditor.Instance.Get(id, out entity))
		{
			return;
		}
		list.Add(entity.behaviour);
		bool flag = data[offset++] == 1;
		XData prevData = null;
		XData xData;
		offset += XDataHolder.DecodeXData(data, offset, out xData);
		bool flag2 = IsOwner(playerId);
		bool flag3 = flag2 && !flag;
		for (int i = 0; i < list.Count; i++)
		{
			SaveableDataHolder saveableDataHolder = list[i];
			if (flag3)
			{
				prevData = saveableDataHolder.GetLoadData(xData.Key);
			}
			if (!saveableDataHolder.Load(xData))
			{
				return;
			}
			if (flag3)
			{
				LevelUndoSystem.Add(new LUAEditEntityField(entity, prevData));
			}
			if (flag2)
			{
				BlockMapper blockMapper = CurrentInstance;
				if (!blockMapper || blockMapper.Current != saveableDataHolder)
				{
					blockMapper = Open(saveableDataHolder);
				}
				LevelEditor.Instance.SetActiveTool(StatMaster.Tool.Modify);
				if (blockMapper != null && blockMapper.IsLogic)
				{
					blockMapper.ToggleLogic(false);
				}
			}
			XDataHolder data2 = new XDataHolder();
			saveableDataHolder.OnSave(data2);
		}
		if (StatMaster.isHosting)
		{
			byte[] array = new byte[2 + data.Length];
			NetworkCompression.WriteUInt16(playerId, array, 0);
			Buffer.BlockCopy(data, 0, array, 2, data.Length);
			NetworkAuxAddPiece.Instance.SendNetworkMessage(RPCMessageType.EditEntity, array);
		}
	}

	public static void PasteBlock(ushort playerId, byte[] inData)
	{
		byte[] array = CLZF2.Decompress(inData);
		if (StatMaster.isClient)
		{
			playerId = NetworkCompression.ReadUInt16(array, 0);
			byte[] array2 = new byte[array.Length - 2];
			Buffer.BlockCopy(array, 2, array2, 0, array2.Length);
			array = array2;
		}
		int num = 0;
		bool flag = IsOwner(playerId);
		ServerMachine machine;
		if (!NetworkScene.Instance.GetMachine(playerId, out machine))
		{
			return;
		}
		List<UndoAction> list = new List<UndoAction>();
		BlockBehaviour block2;
		if (StatMaster.isHosting)
		{
			CopyMode copyMode = (CopyMode)array[num++];
			bool flag2 = array[num++] == 1;
			int blockIndex = (int)NetworkCompression.ReadUInt(false, array, num);
			num += 4;
			BlockBehaviour block;
			if (!machine.GetBlockFromIndex(blockIndex, out block))
			{
				return;
			}
			StatMaster.isPaste = true;
			XDataHolder xDataHolder = new XDataHolder();
			block.OnSave(xDataHolder, copyMode);
			XDataHolder xDataHolder2 = xDataHolder.Clone();
			xDataHolder2.EraseCustomBlockData();
			StatMaster.isPaste = false;
			int count;
			num += NetworkCompression.UnpackUInt(array, num, false, out count);
			List<byte[]> list2 = new List<byte[]>();
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				int num3 = (int)NetworkCompression.ReadUInt(false, array, num);
				num += 4;
				if (!machine.GetBlockFromIndex(num3, out block2))
				{
					return;
				}
				BlockInfo prevInfo = BlockInfo.FromBlockBehaviour(block2);
				StatMaster.isPaste = true;
				block2.isBMAction = true;
				if (block.Prefab.Type != block2.Prefab.Type)
				{
					XData.Clamp = true;
				}
				XDataHolder data = xDataHolder2.Clone();
				block2.OnLoad(data, copyMode);
				block2.isBMAction = false;
				XDataHolder xDataHolder3 = new XDataHolder();
				block2.OnSave(xDataHolder3, CopyMode.All);
				XData.Clamp = false;
				StatMaster.isPaste = false;
				byte[] outData;
				bool flag3 = xDataHolder3.Encode(out outData);
				byte[] array3 = null;
				bool flag4 = false;
				if (flag2)
				{
					BlockSkinLoader.SkinPack.Skin selectedSkin = block.VisualController.selectedSkin;
					if (block2.Prefab.hasBVC && block2.Prefab.SkinCanBeChanged && block2.VisualController.selectedSkin.pack != selectedSkin.pack)
					{
						block2.VisualController.ReplaceSkin(selectedSkin);
						block2.OnUpdateSkin();
						array3 = selectedSkin.pack.Encode();
						flag4 = true;
					}
				}
				byte[] array4 = new byte[5 + (flag3 ? outData.Length : 0) + (flag4 ? array3.Length : 0)];
				int num4 = 0;
				NetworkCompression.WriteUInt((uint)num3, false, array4, num4);
				num4 += 4;
				array4[num4++] = (byte)((flag3 ? 1 : 0) | (flag2 ? (flag4 ? 2 : 0) : 0));
				if (flag3)
				{
					Buffer.BlockCopy(outData, 0, array4, num4, outData.Length);
					num4 += outData.Length;
				}
				if (flag2 && flag4)
				{
					Buffer.BlockCopy(array3, 0, array4, num4, array3.Length);
				}
				if (flag)
				{
					BlockInfo newInfo = BlockInfo.FromBlockBehaviour(block2);
					list.Add(new UndoActionEdit(machine, newInfo, prevInfo));
				}
				list2.Add(array4);
				num2 += array4.Length;
			}
			int num5 = NetworkCompression.PackedUIntLength(list2.Count, false);
			byte[] array5 = new byte[3 + num5 + num2];
			int num6 = 0;
			NetworkCompression.WriteUInt16(playerId, array5, num6);
			num6 += 2;
			array5[num6++] = (byte)copyMode;
			NetworkCompression.PackUInt(list2.Count, array5, num6, false, num5);
			num6 += num5;
			NetworkCompression.WriteArray(list2, array5, num6);
			byte[] messageData = CLZF2.Compress(array5);
			NetworkAuxAddPiece.Instance.SendFragmentedNetworkMessage(RPCMessageType.MapperPasteBlock, messageData);
		}
		else
		{
			CopyMode copyMode = (CopyMode)array[num++];
			int count2;
			num += NetworkCompression.UnpackUInt(array, num, false, out count2);
			for (int j = 0; j < count2; j++)
			{
				int blockIndex2 = (int)NetworkCompression.ReadUInt(false, array, num);
				num += 4;
				if (!machine.GetBlockFromIndex(blockIndex2, out block2))
				{
					return;
				}
				BlockInfo prevInfo2 = BlockInfo.FromBlockBehaviour(block2);
				int num7 = array[num++];
				bool flag5 = (num7 & 1) != 0;
				XDataHolder xDataHolder3 = new XDataHolder();
				if (flag5)
				{
					num += xDataHolder3.Decode(array, num);
				}
				block2.isBMAction = true;
				if ((num7 & 2) != 0)
				{
					BlockSkinLoader.SkinPack.Skin skin;
					num += BlockSkinLoader.SkinPack.Skin.Decode(array, num, out skin);
					BlockBehaviour blockBehaviour = block2;
					if (blockBehaviour.VisualController.selectedSkin != skin)
					{
						blockBehaviour.VisualController.ReplaceSkin(skin);
						blockBehaviour.OnUpdateSkin();
					}
				}
				block2.OnLoad(xDataHolder3, copyMode);
				block2.isBMAction = false;
				if (flag)
				{
					list.Add(new UndoActionEdit(machine, BlockInfo.FromBlockBehaviour(block2), prevInfo2));
				}
			}
		}
		StatMaster.isPaste = false;
		if (flag)
		{
			machine.UndoSystem.AddActionsWithTool(list);
		}
	}

	public static void PasteEntity(ushort playerId, byte[] data)
	{
		if (!StatMaster.Mode.levelEdit)
		{
			return;
		}
		if (StatMaster.isClient)
		{
			playerId = NetworkCompression.ReadUInt16(data, 0);
			byte[] array = new byte[data.Length - 2];
			Buffer.BlockCopy(data, 2, array, 0, array.Length);
			data = array;
		}
		int num = 0;
		long id = BitConverter.ToInt64(data, num);
		LevelEntity entity;
		if (!LevelEditor.Instance.Get(id, out entity))
		{
			return;
		}
		SaveableDataHolder behaviour = entity.behaviour;
		num += LevelEntity.ID_LENGTH;
		XDataHolder xDataHolder = new XDataHolder();
		XDataHolder xDataHolder2 = null;
		bool flag = IsOwner(playerId);
		if (flag)
		{
			xDataHolder2 = new XDataHolder();
		}
		CopyMode copyMode;
		if (StatMaster.isHosting)
		{
			long id2 = BitConverter.ToInt64(data, num);
			LevelEntity entity2;
			if (!LevelEditor.Instance.Get(id2, out entity2))
			{
				return;
			}
			SaveableDataHolder behaviour2 = entity2.behaviour;
			num += LevelEntity.ID_LENGTH;
			StatMaster.isPaste = true;
			copyMode = (CopyMode)data[num++];
			if (flag)
			{
				behaviour.OnSave(xDataHolder2, copyMode);
			}
			XDataHolder data2 = new XDataHolder();
			behaviour2.OnSave(data2, copyMode);
			behaviour.isBMAction = true;
			behaviour.OnLoad(data2, copyMode);
			entity.ReplaceEntityReference(entity2.identifier, entity.identifier);
			entity.RemoveIncompatibleTriggers();
			behaviour.isBMAction = false;
			behaviour.OnSave(xDataHolder, CopyMode.All);
			StatMaster.isPaste = false;
			byte[] outData;
			bool flag2 = xDataHolder.Encode(out outData);
			num = 0;
			int iD_LENGTH = LevelEntity.ID_LENGTH;
			byte[] array2 = new byte[2 + iD_LENGTH + 2 + (flag2 ? outData.Length : 0)];
			NetworkCompression.WriteUInt16(playerId, array2, num);
			num += 2;
			Buffer.BlockCopy(data, 0, array2, num, iD_LENGTH);
			num += iD_LENGTH;
			array2[num] = (byte)copyMode;
			num++;
			array2[num] = (byte)(flag2 ? 1u : 0u);
			num++;
			if (flag2)
			{
				Buffer.BlockCopy(outData, 0, array2, num, outData.Length);
			}
			num += outData.Length;
			NetworkAuxAddPiece.Instance.SendNetworkMessage(RPCMessageType.MapperPasteEntity, array2);
		}
		else
		{
			num = LevelEntity.ID_LENGTH;
			copyMode = (CopyMode)data[num];
			num++;
			int num2 = data[num];
			num++;
			if ((num2 & 1) != 0)
			{
				num += xDataHolder.Decode(data, num);
			}
			behaviour.isBMAction = true;
			if (flag)
			{
				behaviour.OnSave(xDataHolder2, copyMode);
			}
			behaviour.OnLoad(xDataHolder, copyMode);
			behaviour.isBMAction = false;
		}
		if (flag)
		{
			LevelUndoSystem.Add(new LUAChangeEntityData(entity, xDataHolder2, copyMode));
		}
		BlockMapper currentInstance = CurrentInstance;
		if ((bool)currentInstance && currentInstance.IsLogic && currentInstance.Current == behaviour)
		{
			currentInstance.Refresh();
		}
	}

	public static void ResetBlock(ushort playerId, byte[] inData)
	{
		byte[] array = CLZF2.Decompress(inData);
		int num = 0;
		if (StatMaster.isClient)
		{
			playerId = NetworkCompression.ReadUInt16(array, num);
			num += 2;
		}
		ServerMachine machine;
		if (!NetworkScene.Instance.GetMachine(playerId, out machine))
		{
			return;
		}
		BlockInfo prevInfo = null;
		bool flag = IsOwner(playerId);
		int num2 = array[num++];
		bool flag2 = (num2 & 1) != 0;
		List<UndoAction> list = new List<UndoAction>();
		BlockBehaviour block;
		if (StatMaster.isHosting)
		{
			int count;
			num += NetworkCompression.UnpackUInt(array, num, false, out count);
			List<byte[]> list2 = new List<byte[]>();
			int num3 = 0;
			for (int i = 0; i < count; i++)
			{
				int num4 = (int)NetworkCompression.ReadUInt(false, array, num);
				num += 4;
				if (!machine.GetBlockFromIndex(num4, out block))
				{
					return;
				}
				if (flag)
				{
					prevInfo = BlockInfo.FromBlockBehaviour(block);
				}
				block.isBMAction = true;
				block.ResetHolder();
				block.isBMAction = false;
				XDataHolder xDataHolder = new XDataHolder();
				block.OnSave(xDataHolder);
				byte[] outData;
				bool flag3 = xDataHolder.Encode(out outData);
				bool flag4 = false;
				if (flag2 && block.Prefab.SkinCanBeChanged && !block.VisualController.selectedSkin.isDefault)
				{
					block.VisualController.ReplaceSkin(block.Prefab.DefaultSkin);
					block.OnUpdateSkin();
					flag4 = true;
				}
				int num5 = 0;
				int num6 = NetworkCompression.PackedUIntLength(block.BuildIndex, false);
				byte[] array2 = new byte[num6 + 1 + (flag3 ? outData.Length : 0)];
				NetworkCompression.PackUInt(num4, array2, num5, false, num6);
				num5 += num6;
				array2[num5++] = (byte)((flag3 ? 1 : 0) | (flag4 ? 2 : 0));
				if (flag3)
				{
					Buffer.BlockCopy(outData, 0, array2, num5, outData.Length);
					num5 += outData.Length;
				}
				if (flag)
				{
					list.Add(new UndoActionEdit(machine, BlockInfo.FromBlockBehaviour(block), prevInfo));
				}
				list2.Add(array2);
				num3 += array2.Length;
			}
			num = 0;
			int num7 = NetworkCompression.PackedUIntLength(list2.Count, false);
			byte[] array3 = new byte[3 + num7 + num3];
			NetworkCompression.WriteUInt16(playerId, array3, num);
			num += 2;
			array3[num++] = (byte)(flag2 ? 1u : 0u);
			NetworkCompression.PackUInt(list2.Count, array3, num, false, num7);
			num += num7;
			NetworkCompression.WriteArray(list2, array3, num);
			byte[] messageData = CLZF2.Compress(array3);
			NetworkAuxAddPiece.Instance.SendFragmentedNetworkMessage(RPCMessageType.MapperResetBlock, messageData);
		}
		else
		{
			int count2;
			num += NetworkCompression.UnpackUInt(array, num, false, out count2);
			for (int j = 0; j < count2; j++)
			{
				int count3;
				num += NetworkCompression.UnpackUInt(array, num, false, out count3);
				if (!machine.GetBlockFromIndex(count3, out block))
				{
					return;
				}
				if (flag)
				{
					prevInfo = BlockInfo.FromBlockBehaviour(block);
				}
				int num8 = array[num++];
				bool flag3 = (num8 & 1) != 0;
				bool flag4 = (num8 & 2) != 0;
				XDataHolder xDataHolder2 = new XDataHolder();
				if (flag3)
				{
					num += xDataHolder2.Decode(array, num);
				}
				if (flag4)
				{
					BlockBehaviour blockBehaviour = block;
					blockBehaviour.VisualController.ReplaceSkin(blockBehaviour.Prefab.DefaultSkin);
					blockBehaviour.OnUpdateSkin();
				}
				block.isBMAction = true;
				block.OnLoad(xDataHolder2);
				block.OnReset();
				block.isBMAction = false;
				if (flag)
				{
					list.Add(new UndoActionEdit(machine, BlockInfo.FromBlockBehaviour(block), prevInfo));
				}
			}
		}
		if (flag)
		{
			machine.UndoSystem.AddActionsWithTool(list);
		}
	}

	public static void ResetEntity(ushort playerId, byte[] data)
	{
		if (!LevelEditor.Instance.isActive)
		{
			return;
		}
		int num = 0;
		if (StatMaster.isClient)
		{
			playerId = NetworkCompression.ReadUInt16(data, num);
			num += 2;
		}
		long id = BitConverter.ToInt64(data, num);
		num += LevelEntity.ID_LENGTH;
		LevelEntity entity;
		if (!LevelEditor.Instance.Get(id, out entity))
		{
			return;
		}
		SaveableDataHolder behaviour = entity.behaviour;
		XDataHolder xDataHolder = null;
		bool flag = IsOwner(playerId);
		if (flag)
		{
			xDataHolder = new XDataHolder();
			behaviour.OnSave(xDataHolder);
		}
		if (StatMaster.isHosting)
		{
			behaviour.isBMAction = true;
			behaviour.ResetHolder();
			GenericEntity genericEntity = behaviour as GenericEntity;
			genericEntity.SetupDefault();
			behaviour.isBMAction = false;
			XDataHolder xDataHolder2 = new XDataHolder();
			behaviour.OnSave(xDataHolder2);
			byte[] outData;
			bool flag2 = xDataHolder2.Encode(out outData);
			byte[] array = new byte[2 + LevelEntity.ID_LENGTH + 1 + (flag2 ? outData.Length : 0)];
			num = 0;
			NetworkCompression.WriteUInt16(playerId, array, num);
			num += 2;
			Buffer.BlockCopy(data, 0, array, num, LevelEntity.ID_LENGTH);
			num += LevelEntity.ID_LENGTH;
			array[num] = (byte)(flag2 ? 1u : 0u);
			num++;
			if (flag2)
			{
				Buffer.BlockCopy(outData, 0, array, num, outData.Length);
				num += outData.Length;
			}
			NetworkAuxAddPiece.Instance.SendNetworkMessage(RPCMessageType.MapperResetEntity, array);
		}
		else
		{
			int num2 = data[num];
			num++;
			XDataHolder xDataHolder3 = new XDataHolder();
			bool flag2 = (num2 & 1) != 0;
			bool flag3 = (num2 & 2) != 0;
			if (flag2)
			{
				num += xDataHolder3.Decode(data, num);
			}
			if (flag3)
			{
				BlockBehaviour blockBehaviour = behaviour as BlockBehaviour;
				blockBehaviour.VisualController.ReplaceSkin(blockBehaviour.Prefab.DefaultSkin);
				blockBehaviour.OnUpdateSkin();
			}
			behaviour.isBMAction = true;
			behaviour.OnLoad(xDataHolder3);
			behaviour.OnReset();
			behaviour.isBMAction = false;
		}
		if (flag)
		{
			LevelUndoSystem.Add(new LUAChangeEntityData(entity, xDataHolder, CopyMode.All));
		}
		BlockMapper currentInstance = CurrentInstance;
		if ((bool)currentInstance && currentInstance.IsLogic && currentInstance.Current == behaviour)
		{
			currentInstance.Refresh();
		}
	}

	public void FindAllVariables()
	{
		_allVariables.Clear();
		if (Machine.Active() == null)
		{
			return;
		}
		foreach (BlockBehaviour buildingBlock in Machine.Active().BuildingBlocks)
		{
			foreach (MKey key in buildingBlock.KeyList)
			{
				if (key.useMessage)
				{
					_allVariables.UnionWith(key.message.Where((string x) => !string.IsNullOrEmpty(x)));
				}
			}
		}
	}

	public bool OneSelected()
	{
		if (IsBlock)
		{
			List<BlockBehaviour> machineSelection = AdvancedBlockEditor.Instance.selectionController.MachineSelection;
			return AdvancedBlockEditor.Instance.SelectionCount == 1 || (Block is BuildSurface && machineSelection.Count((BlockBehaviour x) => x.Prefab.Type != BlockType.BuildEdge && x.Prefab.Type != BlockType.BuildNode) == 1) || (Block is BuildNodeBlock && machineSelection.Count((BlockBehaviour x) => x.Prefab.Type != BlockType.BuildEdge) == 1);
		}
		if (IsEntity)
		{
			return LevelEditor.Instance.SelectionCount <= 1;
		}
		Debug.LogError("Selected something that isn't a block or entity with the block mapper!");
		return true;
	}
}
