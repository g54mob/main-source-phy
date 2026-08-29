using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;
using UnityEngine.XR;

public class MenuOptions : MonoBehaviour
{
	private class TabData
	{
		public Transform panel;

		public OptionsPrefab_TabButton tabButton;

		public List<SelectableOption> options = new List<SelectableOption>();

		public Action onSelectedChanged;

		public bool isSelected => tabButton.selected;

		public TabData(GameObject panel, OptionsPrefab_TabButton button)
		{
			this.panel = panel.transform;
			tabButton = button;
		}
	}

	private class ValidateInfo
	{
		public GameObject objectToControl;

		public Func<bool> shouldObjectBeVisible;
	}

	public class KeyBinding
	{
		public string title;

		public KeyBindingAction action;

		public KeyCode keyCode;

		public KeyCode keyCodeSecondary;

		public List<KeyCode> modifiers;

		public List<KeyCode> modifiersSecondary;

		public Text text;

		public Text textSecondary;
	}

	public Transform optionsParent;

	public GameObject restartTutorialCanvas;

	public ScrollRect scrollView;

	public GameObject pressKeyCanvas;

	public Text pressKeyDescription;

	public Transform tabParent;

	public ES2FrameController frame;

	private bool constructed;

	private List<TabData> panels = new List<TabData>();

	public Button backgroundButton;

	public GameObject exitToMenuController;

	private Action onCancelKeyInput;

	private Action onOpenKeyInput;

	private float controllerAxisMoveTo;

	private float axisMoveCooldown;

	private float moveSliderAcc = 1f;

	private GameObject mouseSmoothnessGO;

	private GameObject mouseSmoothnessAmountGO;

	public Selectable firstSelectable;

	public Selectable lastSelectable;

	private Dictionary<KeyBindingAction, KeyBinding> keyBindings;

	public KeyBindingAction currentKeyBindingActionToSet;

	private bool isCurrentKeyBindingSecondary;

	private KeyBindingAction[][] exclusiveKeybindings;

	[NonSerialized]
	private bool isVR;

	public Image vrControlsImage;

	public Sprite vrMetaQuestControlsSprite;

	public Sprite vrValveIndexControlsSprite;

	public Sprite vrHtcViveControlsSprite;

	[NonSerialized]
	public Dropdown vrLeftHandMovementSchemeDropdown;

	[NonSerialized]
	public Dropdown vrRightHandMovementSchemeDropdown;

	[NonSerialized]
	public Dropdown vrStanceDropdown;

	[NonSerialized]
	public Dropdown languageDropdown;

	[NonSerialized]
	public GameObject menuOptionBrightness;

	[HideInInspector]
	public Dropdown controlModeDropdown;

	public int recordingDisableObjectLevel;

	private SelectableOption lastFrameSelectedOption;

	private GameObject movementSpeedGO;

	private List<ValidateInfo> objectsToValidate = new List<ValidateInfo>(16);

	public static bool isBreakableFadeOn = false;

	public static bool dragZoomItemsOnClick = false;

	public static bool useTeleportMovement = false;

	[NonSerialized]
	public int keybindSceneRevision;

	private static KeyCode[] MODIFIERS = new KeyCode[8]
	{
		KeyCode.LeftShift,
		KeyCode.RightShift,
		KeyCode.LeftControl,
		KeyCode.RightControl,
		KeyCode.LeftAlt,
		KeyCode.RightAlt,
		KeyCode.LeftMeta,
		KeyCode.RightMeta
	};

	private static string[] MODIFIERS_STRS = new string[8] { "LShift", "RShift", "LCtrl", "RCtrl", "LAlt", "RAlt", "LCmd", "RCmd" };

	private GameObject[] controllerNavigations => new GameObject[2]
	{
		frame.ui.Tabs_Parent_goLeft.gameObject,
		frame.ui.Tabs_Parent_goRight.gameObject
	};

	public void constructMenuOptions(bool isVR, bool isEditor, bool isInGame, EventSystem eventSystem, Action onCancelKeyInput = null, Action onOpenKeyInput = null, Action onChangeLanguage = null, Action onChangeTheme = null, Action onChangeCameraFov = null, Action onApplyAutoAntiMotionSickness = null, Action onVoiceChatToggle = null)
	{
		keyBindings = new Dictionary<KeyBindingAction, KeyBinding>();
		exclusiveKeybindings = new KeyBindingAction[1][] { new KeyBindingAction[2]
		{
			KeyBindingAction.Run,
			KeyBindingAction.InstantPickupModifier
		} };
		this.onCancelKeyInput = onCancelKeyInput;
		this.onOpenKeyInput = onOpenKeyInput;
		constructed = true;
		setupFrame();
		defineVRTab();
		defineRoomEditor();
		defineGraphicsTab();
		defineAudioTab();
		defineAdvancedTab();
		defineRebindsMainGame();
		clickTab(panels[0].tabButton.gameObject, fromInit: true);
		void defineAdvancedTab()
		{
			TabData tabData = defineTab("%AdvancedUpper%");
			defineHeader("%gameplay%", tabData);
			defineToggle("%ShowTimer%", PlayerSave.getSettings().showTimer, delegate(bool newValue)
			{
				PlayerSave.getSettings().showTimer = newValue;
			}, tabData);
			if (!isVR)
			{
				defineHeader("%AntiMotionSickness%", tabData);
				GameObject disableAnyLevelUIOverlayGO = null;
				defineButton("%AutoAntiMotionSickness%", "%Apply%", delegate
				{
					if (base.gameObject.activeInHierarchy)
					{
						Toggle componentInChildren = disableAnyLevelUIOverlayGO.GetComponentInChildren<Toggle>();
						if (!componentInChildren.isOn)
						{
							componentInChildren.isOn = true;
						}
						movementSpeedGO.GetComponentInChildren<Slider>().value = 0.5f;
						onApplyAutoAntiMotionSickness();
					}
				}, tabData);
				defineButton("%RevertAutoAntiMotionSickness%", "%RoomEditor_Revert%", delegate
				{
					if (base.gameObject.activeInHierarchy)
					{
						Toggle componentInChildren = disableAnyLevelUIOverlayGO.GetComponentInChildren<Toggle>();
						if (componentInChildren.isOn)
						{
							componentInChildren.isOn = false;
						}
						movementSpeedGO.GetComponentInChildren<Slider>().value = 1f;
					}
				}, tabData);
				disableAnyLevelUIOverlayGO = defineToggle("%DisableAnyLevelUIOverlay%", PlayerSave.getSettings().disableAnyLevelUIOverlay, delegate(bool newValue)
				{
					PlayerSave.getSettings().disableAnyLevelUIOverlay = newValue;
				}, tabData);
				movementSpeedGO = defineSlider("%MovementSpeed%", PlayerSave.getSettings().movementSpeed, 0.25f, 1f, delegate(float newValue)
				{
					PlayerSave.getSettings().movementSpeed = newValue;
				}, tabData, null, 0.5f);
			}
			defineSlider("%HoverUIScale%", PlayerSave.getSettings().hoverUIScale, 1f, 1.5f, delegate(float newValue)
			{
				PlayerSave.getSettings().hoverUIScale = newValue;
			}, tabData, null, 0.05f);
			defineToggle("%HideItemHints%", PlayerSave.getSettings().isHidingItemHints, delegate(bool newValue)
			{
				PlayerSave.getSettings().isHidingItemHints = newValue;
			}, tabData);
			defineToggle("%StreamerMode%", PlayerSave.getSettings().isStreamerMode, delegate(bool newValue)
			{
				PlayerSave.getSettings().isStreamerMode = newValue;
			}, tabData, () => !Controller.isActive());
			defineToggle("%reduceQualitySettingsOnLowFPS%", PlayerSave.getSettings().reduceSettingsOnLowFrameRate, delegate(bool newValue)
			{
				PlayerSave.getSettings().reduceSettingsOnLowFrameRate = newValue;
			}, tabData);
			defineToggle("%Menu_ChatProfanityFilter%", PlayerSave.getSettings().chatProfanityFilterEnabled, delegate(bool filterEnabled)
			{
				PlayerSave.getSettings().chatProfanityFilterEnabled = filterEnabled;
			}, tabData);
			string[] names = Enum.GetNames(typeof(PhotonService.RegionSelector));
			for (int num = 0; num < names.Length; num++)
			{
				names[num] = "%Menu_Region_" + names[num] + "%";
			}
			if (Is.dev())
			{
				defineHeader("", tabData);
				defineHeader("%ControlTesting%", tabData);
				defineToggle("%FadeBreakables%", initial: false, delegate(bool newValue)
				{
					isBreakableFadeOn = newValue;
				}, tabData);
				defineToggle("%DragZoomItemsOnClick%", dragZoomItemsOnClick, delegate(bool value)
				{
					dragZoomItemsOnClick = value;
				}, tabData);
				defineToggle("%DragZoomItemsOnClick%", dragZoomItemsOnClick, delegate(bool value)
				{
					dragZoomItemsOnClick = value;
				}, tabData);
				defineToggle("%UseTeleportMovement%", useTeleportMovement, delegate(bool newValue)
				{
					useTeleportMovement = newValue;
				}, tabData);
				defineToggle("%disableExamineModeEffects%", PlayerSave.getSettings().disableExamineModeEffects, delegate(bool newValue)
				{
					PlayerSave.getSettings().disableExamineModeEffects = newValue;
				}, tabData);
				defineToggle("%headBob%", PlayerSave.getSettings().useHeadBob, delegate(bool newValue)
				{
					PlayerSave.getSettings().useHeadBob = newValue;
				}, tabData);
			}
			defineFooterSpacer(tabData);
		}
		void defineAudioTab()
		{
			TabData tabData = defineTab("%AudioUpper%");
			if (!Controller.isActive())
			{
				defineHeader("%Audio%", tabData);
			}
			defineToggle("%Music%", PlayerSave.getSettings().musicEnabled, delegate(bool newValue)
			{
				PlayerSave.getSettings().musicEnabled = newValue;
				PineFmod.setVolume(PineFmod.getBus("bus:/Music"), newValue ? (PlayerSave.getSettings().musicVolume / 100f) : 0f);
			}, tabData);
			defineSlider("%MusicVolume%", PlayerSave.getSettings().musicVolume, 0f, 100f, delegate(float newValue)
			{
				PlayerSave.getSettings().musicVolume = newValue;
				if (PlayerSave.getSettings().musicEnabled)
				{
					PineFmod.setVolume(PineFmod.getBus("bus:/Music"), newValue / 100f);
				}
			}, tabData, null, 1f);
			defineToggle("%Sound%", PlayerSave.getSettings().soundEnabled, delegate(bool newValue)
			{
				PlayerSave.getSettings().soundEnabled = newValue;
				PineFmod.setVolume(PineFmod.getBus("bus:/Sound Effects"), newValue ? (PlayerSave.getSettings().soundVolume / 100f) : 0f);
			}, tabData);
			defineSlider("%SoundVolume%", PlayerSave.getSettings().soundVolume, 0f, 100f, delegate(float newValue)
			{
				PlayerSave.getSettings().soundVolume = newValue;
				if (PlayerSave.getSettings().soundEnabled)
				{
					PineFmod.setVolume(PineFmod.getBus("bus:/Sound Effects"), newValue / 100f);
				}
			}, tabData, null, 1f);
			defineHeader("", tabData);
			defineHeader("%VoiceChat%", tabData);
			defineToggle("%VoiceChatEnabled%", PlayerSave.getSettings().voiceChatEnabled, delegate(bool newValue)
			{
				PlayerSave.getSettings().voiceChatEnabled = newValue;
				PineFmod.setVolume(PineFmod.getBus("bus:/Voice"), newValue ? (PlayerSave.getSettings().voiceChatVolume / 100f) : 0f);
				onVoiceChatToggle();
			}, tabData);
			defineToggle("%Menu_VoiceChat_ShowIndicators%", PlayerSave.getSettings().showVoiceChatIndicators, delegate(bool newValue)
			{
				PlayerSave.getSettings().showVoiceChatIndicators = newValue;
			}, tabData, () => !isVR && PlayerSave.getSettings().voiceChatEnabled);
			defineSlider("%VoiceChatVolume%", PlayerSave.getSettings().voiceChatVolume, 0f, 100f, delegate(float newValue)
			{
				PlayerSave.getSettings().voiceChatVolume = newValue;
				if (PlayerSave.getSettings().voiceChatEnabled)
				{
					PineFmod.setVolume(PineFmod.getBus("bus:/Voice"), newValue / 100f);
				}
			}, tabData, null, 1f);
			defineToggle("%voiceChatAlwaysOn%", PlayerSave.getSettings().voiceChatAlwaysOn, delegate(bool newValue)
			{
				PlayerSave.getSettings().voiceChatAlwaysOn = newValue;
			}, tabData);
			defineSlider("%VoiceActivityThreshold%", PlayerSave.getSettings().voiceActivityThreshold, 0f, 0.1f, delegate(float newValue)
			{
				PlayerSave.getSettings().voiceActivityThreshold = newValue;
			}, tabData, () => false);
			defineFooterSpacer(tabData);
		}
		void defineFooterSpacer(TabData tabData)
		{
			defineHeader("", tabData);
			defineHeader("", tabData);
		}
		void defineGraphicsTab()
		{
			TabData tabData = defineTab("%GraphicsUpper%");
			defineHeader("%Screen%", tabData);
			menuOptionBrightness = defineSlider("%RoomEditor_Brightness%", PlayerSave.getSettings().hdrpPostExposure, -2f, 2f, delegate(float newValue)
			{
				PlayerSave.getSettings().hdrpPostExposure = newValue;
				Game game = UnityEngine.Object.FindObjectOfType<Game>();
				if (game != null)
				{
					game.syncPostExposure();
					if (!isInGame)
					{
						syncOptions(game.menuOptions);
					}
				}
			}, tabData, null, 0.01f);
			if (Is.Steam)
			{
				defineLanguageSelection(tabData);
			}
			if (!isVR)
			{
				defineDropdown("%FullScreenMode%", new string[4] { "%ExclusiveFullScreen%", "%FullScreenWindow%", "%MaximizedWindow%", "%Windowed%" }, (int)PlayerSave.getSettings().fullScreenMode, delegate(int newValue)
				{
					PlayerSave.getSettings().fullScreenMode = (FullScreenMode)newValue;
					Screen.fullScreenMode = PlayerSave.getSettings().fullScreenMode;
				}, tabData);
				List<string> list = new List<string>(Screen.resolutions.Length);
				List<Resolution> resolutionsResolution = new List<Resolution>(Screen.resolutions.Length);
				for (int num = Screen.resolutions.Length - 1; num >= 0; num--)
				{
					if (Screen.resolutions[num].width > Screen.resolutions[num].height)
					{
						list.Add($"{Screen.resolutions[num].width} x {Screen.resolutions[num].height} @ {Screen.resolutions[num].refreshRate}Hz");
						resolutionsResolution.Add(Screen.resolutions[num]);
					}
				}
				if (list.Count == 0)
				{
					Debug.Log("No resoultions found! probably started on a vertical monitor. Setting all possible resolutions instead only landscape ones.");
					for (int num2 = Screen.resolutions.Length - 1; num2 >= 0; num2--)
					{
						list.Add($"{Screen.resolutions[num2].width} x {Screen.resolutions[num2].height} @ {Screen.resolutions[num2].refreshRate}Hz");
						resolutionsResolution.Add(Screen.resolutions[num2]);
					}
				}
				int currentResolution = 0;
				for (int num3 = 0; num3 < resolutionsResolution.Count; num3++)
				{
					if (PlayerSave.getSettings().screenResolutionWidth == resolutionsResolution[num3].width && PlayerSave.getSettings().screenResolutionHeight == resolutionsResolution[num3].height && PlayerSave.getSettings().screenRefreshRate == resolutionsResolution[num3].refreshRate)
					{
						currentResolution = num3;
					}
				}
				List<DisplayInfo> displayLayout = new List<DisplayInfo>();
				Screen.GetDisplayLayout(displayLayout);
				if (displayLayout.Count > 1)
				{
					List<string> list2 = new List<string>();
					for (int num4 = 0; num4 < displayLayout.Count; num4++)
					{
						if (displayLayout[num4].Equals(Screen.mainWindowDisplayInfo))
						{
							list2.Add(num4 + 1 + " (%Active%)");
						}
						else
						{
							list2.Add((num4 + 1).ToString());
						}
					}
					defineDropdown("%SelectedMonitor%", list2.ToArray(), PlayerSave.getSettings().activeMonitor, delegate(int newValue)
					{
						PlayerSave.getSettings().activeMonitor = newValue;
						Vector2Int position = new Vector2Int(0, 0);
						if (Screen.fullScreenMode != FullScreenMode.Windowed)
						{
							position.x += displayLayout[newValue].width / 2;
							position.y += displayLayout[newValue].height / 2;
						}
						Screen.MoveMainWindowTo(displayLayout[newValue], position);
						setNewResolution(Screen.resolutions[Screen.resolutions.Length - 1]);
					}, tabData);
				}
				defineDropdown("%ScreenResolution%", list.ToArray(), currentResolution, delegate(int newValue)
				{
					currentResolution = newValue;
					setNewResolution(resolutionsResolution[newValue]);
					Screen.SetResolution(PlayerSave.getSettings().screenResolutionWidth, PlayerSave.getSettings().screenResolutionHeight, PlayerSave.getSettings().fullScreenMode, PlayerSave.getSettings().screenRefreshRate);
					Invoke("resetHDRPRenderTextures", 1f / 30f);
				}, tabData);
				defineToggle("%VSync%", PlayerSave.getSettings().vSyncEnabled, delegate(bool newValue)
				{
					PlayerSave.getSettings().vSyncEnabled = newValue;
					PlayerSave.setFramerate();
				}, tabData);
			}
			defineDropdown("%DynamicResolution%", new string[3] { "%dr_off%", "%dr_automatic%", "%dr_fixed%" }, (int)PlayerSave.getSettings().hdrpResolutionScaling, delegate(int newValue)
			{
				PlayerSave.getSettings().hdrpResolutionScaling = (Game.ResolutionScaling)newValue;
			}, tabData);
			defineSlider("%DynamicResolutionFixedScaling%", PlayerSave.getSettings().hdrpResolutionScalingFixed, 0.5f, 1f, delegate(float newValue)
			{
				PlayerSave.getSettings().hdrpResolutionScalingFixed = newValue;
			}, tabData, () => PlayerSave.getSettings().hdrpResolutionScaling == Game.ResolutionScaling.Fixed, 0.05f);
			if (!isVR)
			{
				defineToggle("%LimitFrameRate%", PlayerSave.getSettings().limitFrameRate, delegate(bool newValue)
				{
					PlayerSave.getSettings().limitFrameRate = newValue;
					PlayerSave.setFramerate();
				}, tabData, () => !PlayerSave.getSettings().vSyncEnabled);
				defineSlider("%TargetFrameRate%", PlayerSave.getSettings().targetFrameRate, 10f, 240f, delegate(float newValue)
				{
					PlayerSave.getSettings().targetFrameRate = (int)newValue;
					PlayerSave.setFramerate();
				}, tabData, () => PlayerSave.getSettings().limitFrameRate && !PlayerSave.getSettings().vSyncEnabled, 0.5f);
				defineSlider("%CameraFOV%", PlayerSave.getSettings().gameFov, 40f, 140f, delegate(float newValue)
				{
					PlayerSave.getSettings().gameFov = newValue;
					if (onChangeCameraFov != null)
					{
						onChangeCameraFov();
					}
				}, tabData, null, 1f);
			}
			defineHeader("", tabData);
			defineHeader("%Graphics%", tabData);
			defineToggle("%ScreenSpaceReflections%", PlayerSave.getSettings().hdrpScreenSpaceReflections, delegate(bool newValue)
			{
				PlayerSave.getSettings().hdrpScreenSpaceReflections = newValue;
			}, tabData);
			defineToggle("%AmbientOcclusion%", PlayerSave.getSettings().hdrpScreenSpaceAmbientOcclusion, delegate(bool newValue)
			{
				PlayerSave.getSettings().hdrpScreenSpaceAmbientOcclusion = newValue;
			}, tabData);
			defineToggle("%Bloom%", PlayerSave.getSettings().hdrpBloom, delegate(bool newValue)
			{
				PlayerSave.getSettings().hdrpBloom = newValue;
			}, tabData);
			defineToggle("%MotionBlur%", PlayerSave.getSettings().hdrpMotionBlur, delegate(bool newValue)
			{
				PlayerSave.getSettings().hdrpMotionBlur = newValue;
			}, tabData);
			defineToggle("%Decals%", PlayerSave.getSettings().hdrpDecals, delegate(bool newValue)
			{
				PlayerSave.getSettings().hdrpDecals = newValue;
			}, tabData);
			defineToggle("%Volumetrics%", PlayerSave.getSettings().hdrpVolumetrics, delegate(bool newValue)
			{
				PlayerSave.getSettings().hdrpVolumetrics = newValue;
			}, tabData);
			defineDropdown("%Antialiasing%", new string[4] { "%off%", "%FXAA%", "%TAA%", "SMAA%" }, (int)PlayerSave.getSettings().hdrpAntialiasingMode, delegate(int newValue)
			{
				PlayerSave.getSettings().hdrpAntialiasingMode = (HDAdditionalCameraData.AntialiasingMode)newValue;
			}, tabData);
			defineDropdown("%Geometry%", new string[2] { "%High%", "%Medium%" }, PlayerSave.getSettings().hdrpGeometry, delegate(int newValue)
			{
				PlayerSave.getSettings().hdrpGeometry = newValue;
			}, tabData);
			defineDropdown("%Textures%", new string[3] { "%Full%", "%Half%", "%Quarter%" }, (int)PlayerSave.getSettings().playerTextureLevel, delegate(int newValue)
			{
				QualitySettings.globalTextureMipmapLimit = newValue;
				PlayerSave.getSettings().playerTextureLevel = (PlayerSave.TextureLevel)newValue;
			}, tabData);
			defineFooterSpacer(tabData);
		}
		void defineLanguageSelection(TabData tab)
		{
			List<string> list = new List<string>(32);
			foreach (Localization.LocalizedLanguage allLanguage in Localization.allLanguages)
			{
				if (SteamLocalization.availableLanguages.Contains(allLanguage.systemLanguage))
				{
					list.Add(allLanguage.name);
				}
			}
			if (!SteamLocalization.availableLanguages.Contains((Language)PlayerSave.getSettings().language))
			{
				PlayerSave.getSettings().language = 0;
				PlayerSave.flush();
			}
			languageDropdown = defineDropdown("%Language%", list.ToArray(), PlayerSave.getSettings().language, delegate(int newValue)
			{
				PlayerSave.getSettings().language = (int)SteamLocalization.availableLanguages[newValue];
				onChangeLanguage();
			}, tab).GetComponentInChildren<Dropdown>();
		}
		void defineRebindsMainGame()
		{
			if (!isEditor)
			{
				if (isVR)
				{
					this.isVR = true;
					UnityEngine.Object.Destroy(backgroundButton);
				}
				else
				{
					if (vrControlsImage != null)
					{
						UnityEngine.Object.Destroy(vrControlsImage.gameObject);
					}
					TabData tabData = defineTab("%ControlsUpper%");
					defineHeader("%Mouse%", tabData, () => !Controller.isActive());
					mouseSmoothnessGO = defineToggle("%MouseSmoothness%", PlayerSave.getSettings().mouseSmoothing, delegate(bool newValue)
					{
						PlayerSave.getSettings().mouseSmoothing = newValue;
					}, tabData, () => !Controller.isActive());
					mouseSmoothnessAmountGO = defineSlider("%MouseSmoothnessAmount%", PlayerSave.getSettings().mouseSmoothnessAmount, 0f, 20f, delegate(float newValue)
					{
						PlayerSave.getSettings().mouseSmoothnessAmount = newValue;
					}, tabData, () => !Controller.isActive() && PlayerSave.getSettings().mouseSmoothing);
					defineSlider("%MouseSensitivity%", PlayerSave.getSettings().mouseSensitivity, 1f, 100f, delegate(float newValue)
					{
						PlayerSave.getSettings().mouseSensitivity = newValue;
					}, tabData, () => !Controller.isActive());
					defineToggle("%lookinvertxtitle%", PlayerSave.getSettings().invertLookX, delegate(bool newValue)
					{
						PlayerSave.getSettings().invertLookX = newValue;
					}, tabData, () => !Controller.isActive());
					defineToggle("%lookinvertytitle%", PlayerSave.getSettings().invertLookY, delegate(bool newValue)
					{
						PlayerSave.getSettings().invertLookY = newValue;
					}, tabData, () => !Controller.isActive());
					defineToggle("%invertymousescroll%", PlayerSave.getSettings().invertMouseScroll, delegate(bool newValue)
					{
						PlayerSave.getSettings().invertMouseScroll = newValue;
					}, tabData, () => !Controller.isActive());
					defineHeader("", tabData, () => !Controller.isActive());
					defineButton("%resetKeys%", "%reset%", isEditor ? new Action(keyBindingsEditorReset) : new Action(keyBindingsReset), tabData, () => !Controller.isActive());
					defineHeader("%Movement%", tabData, () => !Controller.isActive());
					defineRebindingKey("%Forward%", KeyBindingAction.Up, tabData, () => !Controller.isActive());
					defineRebindingKey("%Left%", KeyBindingAction.Left, tabData, () => !Controller.isActive());
					defineRebindingKey("%Backward%", KeyBindingAction.Down, tabData, () => !Controller.isActive());
					defineRebindingKey("%Right%", KeyBindingAction.Right, tabData, () => !Controller.isActive());
					defineRebindingKey("%Crouch%", KeyBindingAction.Crouch, tabData, () => !Controller.isActive());
					defineRebindingKey("%Run%", KeyBindingAction.Run, tabData, () => !Controller.isActive());
					defineHeader("", tabData, () => !Controller.isActive());
					defineHeader("%Actions%", tabData, () => !Controller.isActive());
					defineRebindingKey("%Examine%", KeyBindingAction.ExamineInventory, tabData, () => !Controller.isActive());
					defineRebindingKey("%Pin%", KeyBindingAction.Pin, tabData, () => !Controller.isActive());
					defineRebindingKey("%Drop%", KeyBindingAction.Drop, tabData, () => !Controller.isActive());
					defineRebindingKey("%Throw%", KeyBindingAction.Throw, tabData, () => !Controller.isActive());
					defineRebindingKey("%take%", KeyBindingAction.Take, tabData, () => !Controller.isActive());
					defineRebindingKey("%InstantPickupModifier%", KeyBindingAction.InstantPickupModifier, tabData, () => !Controller.isActive());
					defineRebindingKey("%PingAndEmote%", KeyBindingAction.MpPingModifier, tabData, () => !Controller.isActive());
					defineRebindingKey("%MultiplayerPushToTalk%", KeyBindingAction.PushToTalk, tabData, () => !Controller.isActive());
					defineRebindingKey("%HoverAction_Zoom%", KeyBindingAction.Zoom, tabData, () => !Controller.isActive());
					defineRebindingKey("%OpenChat%", KeyBindingAction.Chat, tabData, () => !Controller.isActive());
					defineRebindingKey("%openWalkthrough%", KeyBindingAction.Walkthrough, tabData, () => !Controller.isActive());
					defineRebindingKey("%emulateRightClick%", KeyBindingAction.EmulateMouseRightClick, tabData, () => !Controller.isActive());
					defineHeader("%Controller%", tabData, () => Controller.isActive());
					defineSlider("%ControllerSensitivity%", PlayerSave.getSettings().controllerSensitivityMove, 1f, 10f, delegate(float newValue)
					{
						PlayerSave.getSettings().controllerSensitivityMove = newValue;
					}, tabData, () => Controller.isActive());
					defineSlider("%ControllerSensitivityZoom%", PlayerSave.getSettings().controllerSensitivityZoom, 1f, 100f, delegate(float newValue)
					{
						PlayerSave.getSettings().controllerSensitivityZoom = newValue;
					}, tabData, () => Controller.isActive());
					defineToggle("%InvertRightXtitle%", PlayerSave.getSettings().controllerInvertRightX, delegate(bool newValue)
					{
						PlayerSave.getSettings().controllerInvertRightX = newValue;
					}, tabData, () => Controller.isActive());
					defineToggle("%InvertRightYtitle%", PlayerSave.getSettings().controllerInvertRightY, delegate(bool newValue)
					{
						PlayerSave.getSettings().controllerInvertRightY = newValue;
					}, tabData, () => Controller.isActive());
					defineToggle("%InvertLeftXtitle%", PlayerSave.getSettings().controllerInvertLeftX, delegate(bool newValue)
					{
						PlayerSave.getSettings().controllerInvertLeftX = newValue;
					}, tabData, () => Controller.isActive());
					defineToggle("%InvertLeftYtitle%", PlayerSave.getSettings().controllerInvertLeftY, delegate(bool newValue)
					{
						PlayerSave.getSettings().controllerInvertLeftY = newValue;
					}, tabData, () => Controller.isActive());
					defineFooterSpacer(tabData);
				}
			}
		}
		void defineRoomEditor()
		{
			if (isEditor)
			{
				TabData tabData = defineTab("%RoomEditorUpper%");
				PlayerSave.Settings settings = PlayerSave.getSettings();
				defineHeader("%options%", tabData);
				defineDropdown("Code Editor Theme", new string[2] { "Dark", "Light" }, (int)settings.re.textEditorTheme, delegate(int newValue)
				{
					settings.re.textEditorTheme = (PlayerSave.RoomEditorSettings.TextEditorTheme)newValue;
				}, tabData);
				defineDropdown("Open text assets with", new string[3] { "Built-in editor", "System default application", "Custom" }, (int)settings.re.textEditorType, delegate(int newValue)
				{
					settings.re.textEditorType = (PlayerSave.RoomEditorSettings.TextEditorType)newValue;
				}, tabData);
				defineInputField("Custom text editor path", settings.re.customTextEditorPath, delegate(string newValue)
				{
					settings.re.customTextEditorPath = newValue;
				}, tabData, () => settings.re.textEditorType == PlayerSave.RoomEditorSettings.TextEditorType.Custom);
				defineDropdown("Open texture assets with", new string[3] { "Paint", "System default application", "Custom" }, (int)settings.re.textureEditorType, delegate(int newValue)
				{
					settings.re.textureEditorType = (PlayerSave.RoomEditorSettings.TextureEditorType)newValue;
				}, tabData);
				defineInputField("Custom texture editor path", settings.re.customTextureEditorPath, delegate(string newValue)
				{
					settings.re.customTextureEditorPath = newValue;
				}, tabData, () => settings.re.textureEditorType == PlayerSave.RoomEditorSettings.TextureEditorType.Custom);
				defineHeader("%Movement%", tabData);
				defineRebindingKey("%Forward%", KeyBindingAction.EditorForward, tabData);
				defineRebindingKey("%Left%", KeyBindingAction.EditorLeft, tabData);
				defineRebindingKey("%Backward%", KeyBindingAction.EditorBackward, tabData);
				defineRebindingKey("%Right%", KeyBindingAction.EditorRight, tabData);
				defineRebindingKey("%Up%", KeyBindingAction.EditorUp, tabData);
				defineRebindingKey("%Down%", KeyBindingAction.EditorDown, tabData);
				defineHeader("", tabData);
				defineHeader("%Actions%", tabData);
				defineRebindingKey("%Focus%", KeyBindingAction.EditorFocus, tabData);
				defineRebindingKey("%Rotate%", KeyBindingAction.EditorRotate, tabData);
				defineRebindingKey("%Duplicate%", KeyBindingAction.EditorDuplicate, tabData);
				defineRebindingKey("%Delete%", KeyBindingAction.EditorDelete, tabData);
				defineRebindingKey("%Undo%", KeyBindingAction.EditorUndo, tabData);
				defineRebindingKey("%Redo%", KeyBindingAction.EditorRedo, tabData);
				defineRebindingKey("%GizmoObjectSpace%", KeyBindingAction.EditorGizmoObjectSpace, tabData);
				defineRebindingKey("%GizmoPosition%", KeyBindingAction.EditorGizmoPosition, tabData);
				defineRebindingKey("%GizmoRotation%", KeyBindingAction.EditorGizmoRotation, tabData);
				defineRebindingKey("%GizmoScale%", KeyBindingAction.EditorGizmoScale, tabData);
				defineRebindingKey("%RoomEditor_Save%", KeyBindingAction.EditorSave, tabData);
				defineRebindingKey("%RoomEditor_Hide%", KeyBindingAction.EditorHide, tabData);
				defineRebindingKey("%RoomEditor_HideOnlySelected%", KeyBindingAction.EditorHideOnlySelected, tabData);
				defineRebindingKey("%RoomEditor_Unhide%", KeyBindingAction.EditorUnhideAll, tabData);
				defineRebindingKey("%RoomEditor_HideFocus%", KeyBindingAction.EditorHideFocus, tabData);
				defineRebindingKey("%searchKeybinding%", KeyBindingAction.EditorOpenSearch, tabData);
				defineRebindingKey("%RoomEditor_ToggleCodeEditor%", KeyBindingAction.EditorToggleCodeEditor, tabData);
				defineRebindingKey("%RoomEditor_EnterPlayMode%", KeyBindingAction.EditorEnterPlayMode, tabData);
				runValidation();
			}
		}
		TabData defineTab(string label)
		{
			OptionsPrefab_TabButton tab = OptionsPrefabs.get().getTabButton(label, tabParent.transform);
			tab.transform.SetSiblingIndex(tabParent.transform.childCount - 3);
			tab.transform.localScale = Vector3.one;
			tab.transform.localPosition = Vector3.zero;
			tab.button.onClick.AddListener(delegate
			{
				clickTab(tab.gameObject);
			});
			TabData tabData = new TabData(UnityEngine.Object.Instantiate(optionsParent, optionsParent.parent).gameObject, tab);
			panels.Add(tabData);
			return tabData;
		}
		void defineVRTab()
		{
			if (isVR)
			{
				TabData tabData = defineTab("%MenuOptions_VR%");
				_ = tabData.panel.transform;
				UnityEngine.Object.Destroy(backgroundButton);
				if (Is.Android && !Is.Release)
				{
					List<float> renderScaleValues = new List<float>();
					List<string> list = new List<string>();
					for (float num = 1.5f; num >= 0.5f; num -= 0.05f)
					{
						renderScaleValues.Add(num);
						list.Add(num.ToString("F2"));
					}
					defineDropdown("Render Scale", list.ToArray(), renderScaleValues.FindIndex((float rs) => UnityUtils.closeEnough(rs, 1.25f)), delegate(int renderScaleIndex)
					{
						float num2 = renderScaleValues[renderScaleIndex];
						Game game = UnityEngine.Object.FindObjectOfType<Game>();
						if (game != null)
						{
							game.vrOculusRenderScale = num2;
						}
						XRSettings.eyeTextureResolutionScale = num2;
						Debug.Log($"Render scale variable: {num2}, actual: {XRSettings.eyeTextureResolutionScale}");
					}, tabData);
				}
				defineHeader("%MenuOptions_VR_Locomotion%", tabData);
				string[] choices = new string[4] { "%MenuOptions_MovementScheme_Teleport%", "%MenuOptions_MovementScheme_Smooth%", "%MenuOptions_MovementScheme_SmoothWithTurn%", "%MenuOptions_MovementScheme_Off%" };
				vrLeftHandMovementSchemeDropdown = defineDropdown("%vrMovementLeftHand%", choices, (int)PlayerSave.getSettings().leftHandMovementScheme, delegate(int newMovementSchemeIndex)
				{
					PlayerSave.getSettings().leftHandMovementScheme = (Game.MovementScheme)newMovementSchemeIndex;
				}, tabData).GetComponentInChildren<Dropdown>();
				vrRightHandMovementSchemeDropdown = defineDropdown("%vrMovementRightHand%", choices, (int)PlayerSave.getSettings().rightHandMovementScheme, delegate(int newMovementSchemeIndex)
				{
					PlayerSave.getSettings().rightHandMovementScheme = (Game.MovementScheme)newMovementSchemeIndex;
				}, tabData).GetComponentInChildren<Dropdown>();
				defineSlider("%MovementSpeed%", PlayerSave.getSettings().movementSpeed, 0.25f, 2f, delegate(float newValue)
				{
					PlayerSave.getSettings().movementSpeed = newValue;
				}, tabData, null, 0.05f);
				defineSlider("%MenuOptions_MotionComfortFilter%", PlayerSave.getSettings().comfortVignetteSize, 0f, 100f, delegate(float newValue)
				{
					PlayerSave.getSettings().comfortVignetteSize = newValue;
				}, tabData, null, 25f);
				defineToggle("%MenuOptions_TurningStyle%", PlayerSave.getSettings().useSnapTurn, delegate(bool newValue)
				{
					PlayerSave.getSettings().useSnapTurn = newValue;
				}, tabData, null, "%MenuOptions_TurningStyle_Snap%", "%MenuOptions_TurningStyle_Smooth%");
				defineSlider("%MenuOptions_SnapTurnDegreesPerTurn%", PlayerSave.getSettings().snapTurnDegreesPerTurn, 15f, 90f, delegate(float newValue)
				{
					PlayerSave.getSettings().snapTurnDegreesPerTurn = newValue;
				}, tabData, () => PlayerSave.getSettings().useSnapTurn, 15f);
				defineSlider("%MenuOptions_SmoothTurnDegreesPerSecond%", PlayerSave.getSettings().smoothTurnDegreesPerSecond, 15f, 180f, delegate(float newValue)
				{
					PlayerSave.getSettings().smoothTurnDegreesPerSecond = newValue;
				}, tabData, () => !PlayerSave.getSettings().useSnapTurn, 15f);
				vrStanceDropdown = defineDropdown("%MenuOptions_HeightType%", new string[2] { "%MenuOptions_HeightType_Standing%", "%MenuOptions_HeightType_Sitting%" }, (int)PlayerSave.getSettings().vrHeightType, delegate(int newVRHeightType)
				{
					PlayerSave.getSettings().vrHeightType = (Game.VRHeightType)newVRHeightType;
				}, tabData).GetComponentInChildren<Dropdown>();
				defineSlider("%MenuOptions_TeleportBlinkDuration%", PlayerSave.getSettings().teleportBlinkDuration, 0f, 1f, delegate(float newValue)
				{
					PlayerSave.getSettings().teleportBlinkDuration = newValue;
				}, tabData, null, 0.05f);
				defineToggle("%MenuOptions_HoldDownToTurnAround2%", PlayerSave.getSettings().holdDownToTurnAround, delegate(bool newValue)
				{
					PlayerSave.getSettings().holdDownToTurnAround = newValue;
				}, tabData);
				defineSlider("%MenuOptions_TurnAroundHoldTime%", PlayerSave.getSettings().turnAroundHoldTime, 0f, 1f, delegate(float newValue)
				{
					PlayerSave.getSettings().turnAroundHoldTime = newValue;
				}, tabData, () => PlayerSave.getSettings().holdDownToTurnAround, 0.05f);
				defineHeader("", tabData);
				defineHeader("%MenuOptions_VR_Gameplay%", tabData);
				defineDropdown("%MenuOptions_CursorType%", new string[2] { "%MenuOptions_CursorType_None%", "%MenuOptions_CursorType_Circle%" }, (int)PlayerSave.getSettings().cursorType, delegate(int newCursorType)
				{
					PlayerSave.getSettings().cursorType = (Game.CursorType)newCursorType;
				}, tabData);
				defineDropdown("%MenuOptions_RayType%", new string[2] { "%MenuOptions_RayType_None%", "%MenuOptions_RayType_Line%" }, (int)PlayerSave.getSettings().rayType, delegate(int newRayType)
				{
					PlayerSave.getSettings().rayType = (Game.RayType)newRayType;
				}, tabData);
				defineDropdown("%MenuOptions_HandSmoothingType%", new string[2] { "%MenuOptions_HandSmoothingType_None%", "%MenuOptions_HandSmoothingType_Hand%" }, (int)PlayerSave.getSettings().handSmoothingType, delegate(int newSmoothingType)
				{
					PlayerSave.getSettings().handSmoothingType = (Game.HandSmoothingType)newSmoothingType;
				}, tabData);
				defineDropdown("%MenuOptions_InventoryOpenPosition%", new string[2] { "%MenuOptions_InventoryOpenPositionInFront%", "%MenuOptions_InventoryOpenPositionInHand%" }, (int)PlayerSave.getSettings().inventoryOpenPosition, delegate(int newInventoryOpenPosition)
				{
					PlayerSave.getSettings().inventoryOpenPosition = (Game.InventoryOpenPosition)newInventoryOpenPosition;
				}, tabData);
				defineDropdown("%MenuOptions_HandVisibilityWhenHoldingItem%", new string[3] { "%MenuOptions_HandVisibilityWhenHoldingItem_Hidden%", "%MenuOptions_HandVisibilityWhenHoldingItem_Transparent%", "%MenuOptions_HandVisibilityWhenHoldingItem_Opaque%" }, (int)PlayerSave.getSettings().handVisibilityWhenHoldingItem, delegate(int newValue)
				{
					PlayerSave.getSettings().handVisibilityWhenHoldingItem = (Game.HandVisibilityWhenHoldingItem)newValue;
				}, tabData);
				defineToggle("%MenuOptions_AutoCloseInventory%", PlayerSave.getSettings().autoCloseInventory, delegate(bool newValue)
				{
					PlayerSave.getSettings().autoCloseInventory = newValue;
				}, tabData);
				defineSlider("%MenuOptions_InventoryRotationSpeed2%", PlayerSave.getSettings().inventoryRotationSpeed, -1f, 1f, delegate(float newValue)
				{
					PlayerSave.getSettings().inventoryRotationSpeed = newValue;
				}, tabData);
				defineToggle("%MenuOptions_ZoomScaling%", PlayerSave.getSettings().disableZoomScaling, delegate(bool newValue)
				{
					PlayerSave.getSettings().disableZoomScaling = newValue;
				}, tabData);
				defineToggle("%MenuOptions_ThumbstickRotatesItemInHand%", PlayerSave.getSettings().thumbstickRotatesItemInHand, delegate(bool newValue)
				{
					PlayerSave.getSettings().thumbstickRotatesItemInHand = newValue;
				}, tabData);
				defineToggle("%MenuOptions_ShowItemInfoOnHover%", PlayerSave.getSettings().showItemInfoOnHover, delegate(bool newValue)
				{
					PlayerSave.getSettings().showItemInfoOnHover = newValue;
				}, tabData);
				defineSlider("%MenuOptions_ItemInfoRequiredHoverDuration%", PlayerSave.getSettings().itemInfoRequiredHoverDuration, 0f, 2f, delegate(float newValue)
				{
					PlayerSave.getSettings().itemInfoRequiredHoverDuration = newValue;
				}, tabData, () => PlayerSave.getSettings().showItemInfoOnHover, 0.25f);
				defineHeader("", tabData);
				defineHeader("%MenuOptions_VR_Haptics%", tabData);
				defineSlider("%MenuOptions_RightHandVibrationIntensity%", PlayerSave.getSettings().rightHandVibrationIntensity, 0f, 2f, delegate(float newValue)
				{
					PlayerSave.getSettings().rightHandVibrationIntensity = newValue;
				}, tabData);
				defineSlider("%MenuOptions_LeftHandVibrationIntensity%", PlayerSave.getSettings().leftHandVibrationIntensity, 0f, 2f, delegate(float newValue)
				{
					PlayerSave.getSettings().leftHandVibrationIntensity = newValue;
				}, tabData);
				defineHeader("", tabData);
				defineHeader("%tagOther%", tabData);
				defineSlider("%MenuOptions_GripPressThreshold%", PlayerSave.getSettings().gripPressThreshold, 0.2f, 0.8f, delegate(float newValue)
				{
					PlayerSave.getSettings().gripPressThreshold = newValue;
				}, tabData, null, 0.05f);
				defineSlider("%MenuOptions_ThumbstickDeadzone%", PlayerSave.getSettings().thumbstickDeadzone, 0.05f, 0.5f, delegate(float newValue)
				{
					PlayerSave.getSettings().thumbstickDeadzone = newValue;
				}, tabData, null, 0.05f);
				defineToggle("%MenuOptions_UseNewThrowAlgorithm%", PlayerSave.getSettings().useNewThrowAlgorithm, delegate(bool newValue)
				{
					PlayerSave.getSettings().useNewThrowAlgorithm = newValue;
				}, tabData);
				defineDropdown("%MenuOptions_PictureImageFormat%", new string[2] { "JPG", "PNG" }, (int)PlayerSave.getSettings().pictureImageFormat, delegate(int newPictureImageFormat)
				{
					PlayerSave.getSettings().pictureImageFormat = (VRPictureCamera.ImageFormat)newPictureImageFormat;
				}, tabData);
				defineHeader("", tabData);
			}
		}
		void setupFrame()
		{
			VisualControl visualControl = new VisualControl("back", ControllerButtonActionType.UIBack, "%back%", VisualControl.ControlType.Button, VisualControl.Position.BotRight);
			visualControl.addEscape();
			frame.addOrUpdateControl(visualControl);
		}
		static void syncOptions(MenuOptions options)
		{
			Slider componentInChildren = options.menuOptionBrightness.GetComponentInChildren<Slider>(includeInactive: true);
			if (componentInChildren != null)
			{
				componentInChildren.value = PlayerSave.getSettings().hdrpPostExposure;
			}
			InputField componentInChildren2 = options.menuOptionBrightness.GetComponentInChildren<InputField>(includeInactive: true);
			if (componentInChildren2 != null)
			{
				componentInChildren2.text = componentInChildren.value.ToString("0.00");
			}
		}
	}

	private void clickTab(GameObject tab, bool fromInit = false)
	{
		foreach (TabData panel in panels)
		{
			bool isSelected = panel.isSelected;
			bool flag = panel.tabButton.gameObject == tab;
			panel.panel.gameObject.SetActive(flag);
			Themes.ColorsTabButton tabButtonColors = Menu.getTheme().tabButtonColors;
			panel.tabButton.Text.color = (flag ? tabButtonColors.selectedColorText : tabButtonColors.unselectedColorText);
			panel.tabButton.selectedImage.gameObject.SetActive(flag);
			panel.tabButton.selected = flag;
			if (isSelected != panel.isSelected && panel.onSelectedChanged != null)
			{
				panel.onSelectedChanged();
			}
		}
		if (!fromInit)
		{
			controllerSelectFirstVisible();
		}
		runValidation();
		if (!fromInit)
		{
			scrollView.verticalNormalizedPosition = 1f;
			PineFmod.playOneShotSound("event:/Sound Effects/00 General/Menu Sound Effects/Small/UI_Small_04");
		}
	}

	private TabData getSelectedPanel()
	{
		for (int i = 0; i < panels.Count; i++)
		{
			if (panels[i].isSelected)
			{
				return panels[i];
			}
		}
		return null;
	}

	public static void setNewResolution(Resolution resolutionToSet)
	{
		Debug.Log($"Setting resolution to: {resolutionToSet.width} {resolutionToSet.height} @ {resolutionToSet.refreshRate} hz");
		if ((float)resolutionToSet.width / (float)resolutionToSet.height >= 1.3f)
		{
			PlayerSave.getSettings().screenResolutionWidth = resolutionToSet.width;
			PlayerSave.getSettings().screenResolutionHeight = resolutionToSet.height;
			PlayerSave.getSettings().screenRefreshRate = resolutionToSet.refreshRate;
		}
		else
		{
			Debug.Log("This resolution is not valid, we need at least 4:3. Setting 720p resolution to allow player to interact with options.");
			PlayerSave.getSettings().screenResolutionWidth = 1280;
			PlayerSave.getSettings().screenResolutionHeight = 720;
			PlayerSave.getSettings().screenRefreshRate = resolutionToSet.refreshRate;
			PlayerSave.getSettings().fullScreenMode = FullScreenMode.FullScreenWindow;
		}
	}

	private void resetHDRPRenderTextures()
	{
		int width = Screen.width;
		int height = Screen.height;
		((HDRenderPipeline)RenderPipelineManager.currentPipeline).ResetRTHandleReferenceSize(width, height);
	}

	public void syncTabVisuals(bool silent = false)
	{
	}

	public void applyAutoAntiMotionSickness()
	{
		Toggle componentInChildren = mouseSmoothnessGO.GetComponentInChildren<Toggle>();
		if (!componentInChildren.isOn)
		{
			componentInChildren.isOn = true;
		}
		mouseSmoothnessAmountGO.GetComponentInChildren<Slider>().value = 2.6f;
	}

	private void OnEnable()
	{
		if (constructed)
		{
			runValidation();
			controllerSelectFirstVisible();
			scrollView.verticalNormalizedPosition = 1f;
		}
	}

	public void onCancelInput()
	{
		onCancelKeyInput();
	}

	private void runValidation()
	{
		if (!constructed)
		{
			return;
		}
		bool flag = false;
		foreach (ValidateInfo item in objectsToValidate)
		{
			if (item.objectToControl.activeSelf != item.shouldObjectBeVisible())
			{
				flag = true;
			}
			item.objectToControl.SetActive(item.shouldObjectBeVisible());
		}
		TabData selectedPanel = getSelectedPanel();
		if (selectedPanel != null)
		{
			runValidationController(selectedPanel.options, ref firstSelectable, ref lastSelectable);
		}
		if (base.gameObject.activeInHierarchy && (Controller.controllerModeChanged || flag))
		{
			StartCoroutine(refreshScrollView());
		}
		GameObject[] array = controllerNavigations;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(Controller.isActive());
		}
	}

	private IEnumerator refreshScrollView()
	{
		if (scrollView.content.gameObject.activeInHierarchy)
		{
			yield return null;
			scrollView.content.gameObject.SetActive(value: false);
			yield return null;
			scrollView.content.gameObject.SetActive(value: true);
		}
	}

	private GameObject defineHeader(string label, TabData data, Func<bool> validation = null)
	{
		SelectableOption header = OptionsPrefabs.get().getHeader(label, data.panel.transform);
		if (validation != null)
		{
			objectsToValidate.Add(new ValidateInfo
			{
				objectToControl = header.mainObject.gameObject,
				shouldObjectBeVisible = validation
			});
		}
		data.options.Add(header);
		return header.mainObject;
	}

	private GameObject defineLabelWithTitle(string label, string title, TabData tabData, Func<bool> validation = null)
	{
		SelectableOption labelWithTitle = OptionsPrefabs.get().getLabelWithTitle(label, title, tabData.panel);
		if (validation != null)
		{
			objectsToValidate.Add(new ValidateInfo
			{
				objectToControl = labelWithTitle.mainObject.gameObject,
				shouldObjectBeVisible = validation
			});
		}
		tabData.options.Add(labelWithTitle);
		return labelWithTitle.mainObject;
	}

	private GameObject defineDropdown(string label, string[] choices, int initial, Action<int> onChange, TabData tabData, Func<bool> validation = null)
	{
		SelectableOption dropdown = OptionsPrefabs.get().getDropdown(label, tabData.panel, choices, initial, delegate(int x)
		{
			onChange(x);
			runValidation();
		});
		if (validation != null)
		{
			objectsToValidate.Add(new ValidateInfo
			{
				objectToControl = dropdown.baseObject.gameObject,
				shouldObjectBeVisible = validation
			});
		}
		tabData.options.Add(dropdown);
		return dropdown.mainObject;
	}

	private GameObject defineInputField(string label, string initial, Action<string> onChange, TabData tabData, Func<bool> validation = null)
	{
		SelectableOption inputField = OptionsPrefabs.get().getInputField(label, tabData.panel, initial, delegate(string x)
		{
			onChange(x);
			runValidation();
		});
		if (validation != null)
		{
			objectsToValidate.Add(new ValidateInfo
			{
				objectToControl = inputField.baseObject.gameObject,
				shouldObjectBeVisible = validation
			});
		}
		tabData.options.Add(inputField);
		return inputField.mainObject;
	}

	private GameObject defineSlider(string label, float initial, float min, float max, Action<float> onChange, TabData tabData, Func<bool> validation = null, float interval = 0.1f)
	{
		SelectableOption slider = OptionsPrefabs.get().getSlider(label, tabData.panel, initial, min, max, delegate(float x)
		{
			onChange(x);
			runValidation();
		}, interval);
		if (validation != null)
		{
			objectsToValidate.Add(new ValidateInfo
			{
				objectToControl = slider.baseObject.gameObject,
				shouldObjectBeVisible = validation
			});
		}
		tabData.options.Add(slider);
		return slider.mainObject;
	}

	private GameObject defineToggle(string label, bool initial, Action<bool> onChange, TabData tabData, Func<bool> validation = null, string onText = null, string offText = null)
	{
		SelectableOption toggle = OptionsPrefabs.get().getToggle(label, tabData.panel, initial, delegate(bool newValue)
		{
			onChange(newValue);
			runValidation();
		}, onText, offText);
		if (validation != null)
		{
			objectsToValidate.Add(new ValidateInfo
			{
				objectToControl = toggle.baseObject.gameObject,
				shouldObjectBeVisible = validation
			});
		}
		tabData.options.Add(toggle);
		return toggle.mainObject;
	}

	private GameObject defineButton(string label, string buttonLabel, Action onPress, TabData tabData, Func<bool> validation = null)
	{
		SelectableOption button = OptionsPrefabs.get().getButton(label, tabData.panel, buttonLabel, delegate
		{
			onPress();
			runValidation();
		});
		if (validation != null)
		{
			objectsToValidate.Add(new ValidateInfo
			{
				objectToControl = button.baseObject.gameObject,
				shouldObjectBeVisible = validation
			});
		}
		tabData.options.Add(button);
		return button.mainObject;
	}

	private GameObject defineRebindingKey(string label, KeyBindingAction action, TabData tabData, Func<bool> validation = null)
	{
		GameObject gameObject = null;
		KeyBinding keybinding = null;
		(gameObject, keybinding) = OptionsPrefabs.get().getRebindingKey(label, tabData.panel, action, delegate
		{
			onButtonPressed(keybinding, isSecondary: false);
			runValidation();
		}, delegate
		{
			onButtonPressed(keybinding, isSecondary: true);
			runValidation();
		});
		keyBindings[action] = keybinding;
		if (validation != null)
		{
			objectsToValidate.Add(new ValidateInfo
			{
				objectToControl = gameObject,
				shouldObjectBeVisible = validation
			});
		}
		return gameObject;
	}

	public void onButtonPressed(KeyBinding keyBinding, bool isSecondary)
	{
		currentKeyBindingActionToSet = keyBinding.action;
		isCurrentKeyBindingSecondary = isSecondary;
		pressKeyDescription.text = Localization.lookupInDictionary("rebindingPressKeyDescription").Replace("[]", "\"" + Localization.translate(keyBinding.title) + "\"");
		onOpenKeyInput();
	}

	public void closeActions(bool resetTabs = true)
	{
		List<PlayerSave.StoredKeyBinding> list = PlayerSave.getSettings().keyBindings;
		foreach (KeyValuePair<KeyBindingAction, KeyBinding> keyBinding in keyBindings)
		{
			KeyBinding binding = keyBinding.Value;
			PlayerSave.StoredKeyBinding storedKeyBinding = list.Find((PlayerSave.StoredKeyBinding x) => x.action == binding.action);
			if (storedKeyBinding == null)
			{
				storedKeyBinding = new PlayerSave.StoredKeyBinding
				{
					action = binding.action
				};
				list.Add(storedKeyBinding);
			}
			storedKeyBinding.keyCode = binding.keyCode;
			storedKeyBinding.keyCodeSecondary = binding.keyCodeSecondary;
			storedKeyBinding.modifiers = binding.modifiers;
			storedKeyBinding.modifiersSecondary = binding.modifiersSecondary;
		}
		PlayerSave.flush();
		if (resetTabs)
		{
			clickTab(panels[0].tabButton.gameObject);
		}
	}

	public void Update()
	{
		updateController(getSelectedPanel().options, scrollView, runValidation, controllerSelectFirstVisible, ref lastFrameSelectedOption, ref controllerAxisMoveTo, ref axisMoveCooldown, ref moveSliderAcc);
		if (Controller.isActive())
		{
			if (Controller.getButtonDown(ControllerButtonActionType.UIMoveTabLeft))
			{
				moveTab(-1);
			}
			if (Controller.getButtonDown(ControllerButtonActionType.UIMoveTabRight))
			{
				moveTab(1);
			}
		}
		if (Controller.controllerModeChanged)
		{
			GameObject[] array = controllerNavigations;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(Controller.isActive());
			}
		}
		if (exitToMenuController != null)
		{
			exitToMenuController.gameObject.SetActive(Controller.isActive());
		}
		_ = isVR;
		void moveTab(int direction)
		{
			int num = panels.IndexOf(getSelectedPanel());
			num += direction;
			num = ((num >= 0) ? (num % panels.Count) : (panels.Count - 1));
			clickTab(panels[num].tabButton.gameObject);
		}
	}

	public Selectable getFirstVisibleSelectable()
	{
		return getSelectedPanel().options.Find((SelectableOption x) => canSelect(x) && x.selectable.gameObject.activeInHierarchy).selectable;
	}

	public static bool canSelect(SelectableOption option)
	{
		if (option.type != SelectableOptionType.None && option.type != SelectableOptionType.Header)
		{
			return option.type != SelectableOptionType.LabelWithText;
		}
		return false;
	}

	public static void runValidationController(List<SelectableOption> selectableOptions, ref Selectable firstSelectable, ref Selectable lastSelectable)
	{
		firstSelectable = null;
		lastSelectable = null;
		foreach (SelectableOption selectableOption in selectableOptions)
		{
			if (canSelect(selectableOption) && selectableOption.selectable.gameObject.activeSelf)
			{
				if (firstSelectable == null)
				{
					firstSelectable = selectableOption.selectable;
					Navigation navigation = firstSelectable.navigation;
					navigation.mode = Navigation.Mode.Explicit;
					firstSelectable.navigation = navigation;
				}
				else if (lastSelectable != null)
				{
					Navigation navigation2 = lastSelectable.navigation;
					navigation2.mode = Navigation.Mode.Explicit;
					navigation2.selectOnDown = selectableOption.selectable;
					lastSelectable.navigation = navigation2;
					Navigation navigation3 = selectableOption.selectable.navigation;
					navigation3.mode = Navigation.Mode.Explicit;
					navigation3.selectOnUp = lastSelectable;
					selectableOption.selectable.navigation = navigation3;
				}
				lastSelectable = selectableOption.selectable;
			}
		}
	}

	public static void updateController(List<SelectableOption> options, ScrollRect scrollView, Action runValidation, Action selectFirstVisible, ref SelectableOption lastSelectedOption, ref float joystickAxisMoveTo, ref float axisMoveCooldown, ref float moveSliderAcc)
	{
		if (Controller.controllerModeChanged)
		{
			runValidation?.Invoke();
			if (!Controller.isActive())
			{
				options.ForEach(delegate(SelectableOption x)
				{
					if (x.type == SelectableOptionType.Dropdown)
					{
						((OptionsPrefab_Dropdown)x.baseObject).arrows.ForEach(delegate(GameObject arrow)
						{
							arrow.gameObject.SetActive(value: false);
						});
					}
				});
			}
		}
		GameObject selected = EventSystem.current.currentSelectedGameObject;
		SelectableOption selectableOption = options.Find((SelectableOption x) => canSelect(x) && x.selectable.gameObject == selected);
		if (Controller.isActive())
		{
			List<SelectableOption> list = options.FindAll((SelectableOption x) => x.selectable != null && x.selectable.gameObject.activeInHierarchy);
			if (Controller.controllerModeChanged)
			{
				selectFirstVisible();
			}
			if (selectableOption != null && list.Count > 0)
			{
				int num = list.IndexOf(selectableOption);
				joystickAxisMoveTo = 1f - (float)num / ((float)list.Count - 1f);
				if (scrollView != null)
				{
					scrollView.verticalNormalizedPosition = Mathf.MoveTowards(scrollView.normalizedPosition.y, joystickAxisMoveTo, Time.deltaTime);
				}
				axisMoveCooldown -= Time.deltaTime;
				float uIAxisForNavigation = Controller.getUIAxisForNavigation();
				float uIAxisForChangeValue = Controller.getUIAxisForChangeValue();
				if (UnityUtils.closeEnough(uIAxisForNavigation, 0f, 0.2f) && UnityUtils.closeEnough(uIAxisForChangeValue, 0f, 0.2f))
				{
					axisMoveCooldown = 0f;
				}
				if (selectableOption.type == SelectableOptionType.Toggle)
				{
					if (axisMoveCooldown <= 0f && (Controller.getUIConfirmDown() || Mathf.Abs(uIAxisForChangeValue) > 0.5f))
					{
						Toggle componentInChildren = selectableOption.selectable.GetComponentInChildren<Toggle>();
						componentInChildren.isOn = !componentInChildren.isOn;
						axisMoveCooldown = 0.5f;
					}
				}
				else if (selectableOption.type == SelectableOptionType.Button)
				{
					if (Controller.getUIConfirmDown())
					{
						selectableOption.selectable.GetComponentInChildren<Button>().onClick.Invoke();
					}
				}
				else if (selectableOption.type == SelectableOptionType.Slider)
				{
					if (Mathf.Abs(uIAxisForChangeValue) < 0.5f || Mathf.Abs(uIAxisForNavigation) > 0.3f)
					{
						moveSliderAcc = 1f;
					}
					if (Mathf.Abs(uIAxisForChangeValue) > 0.5f)
					{
						moveSliderAcc += Time.deltaTime * 15f;
						moveSliderAcc = Mathf.Clamp(moveSliderAcc, 1f, 25f);
						Slider componentInChildren2 = selectableOption.selectable.GetComponentInChildren<Slider>();
						float num2 = (componentInChildren2.maxValue - componentInChildren2.minValue) * 0.01f;
						float num3 = uIAxisForChangeValue * Time.deltaTime * 5f * moveSliderAcc * num2;
						componentInChildren2.value += num3;
					}
				}
				else if (selectableOption.type == SelectableOptionType.Dropdown && axisMoveCooldown <= 0f && Mathf.Abs(uIAxisForChangeValue) > 0.5f)
				{
					Dropdown componentInChildren3 = selectableOption.selectable.GetComponentInChildren<Dropdown>();
					int value = componentInChildren3.value;
					int num4 = ((uIAxisForChangeValue > 0.5f) ? 1 : (-1)) * selectableOption.direction;
					value = (int)Mathf.Repeat(value + num4, componentInChildren3.options.Count);
					componentInChildren3.value = value;
					axisMoveCooldown = 0.5f;
				}
			}
			if (lastSelectedOption != null && lastSelectedOption.baseObject is OptionsPrefab_Dropdown optionsPrefab_Dropdown)
			{
				optionsPrefab_Dropdown.arrows.ForEach(delegate(GameObject x)
				{
					x.SetActive(value: false);
				});
			}
			if (selectableOption != null && selectableOption.baseObject is OptionsPrefab_Dropdown optionsPrefab_Dropdown2)
			{
				optionsPrefab_Dropdown2.arrows.ForEach(delegate(GameObject x)
				{
					x.SetActive(value: true);
				});
			}
			lastSelectedOption = selectableOption;
		}
		else if (selectableOption != null && lastSelectedOption != null)
		{
			lastSelectedOption = null;
		}
		bool flag = Controller.isActive();
		foreach (SelectableOption option in options)
		{
			if (option.selectable != null)
			{
				option.selectable.enabled = flag;
			}
		}
	}

	public void controllerSelectFirstVisible()
	{
		SelectableOption selectableOption = getSelectedPanel().options.Find((SelectableOption x) => x.selectable != null && x.selectable.gameObject.activeSelf);
		if (selectableOption != null)
		{
			Controller.selectSelectable(selectableOption.selectable);
		}
	}

	private bool isModifierKey(KeyCode keyCode)
	{
		return Array.IndexOf(MODIFIERS, keyCode) >= 0;
	}

	private bool isMouseKeyCode(KeyCode keyCode)
	{
		if (keyCode != KeyCode.Mouse0 && keyCode != KeyCode.Mouse1 && keyCode != KeyCode.Mouse2 && keyCode != KeyCode.Mouse3 && keyCode != KeyCode.Mouse4 && keyCode != KeyCode.Mouse5)
		{
			return keyCode == KeyCode.Mouse6;
		}
		return true;
	}

	public KeyCode getAnyKeyDown()
	{
		foreach (KeyCode value in Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown(value) && !isMouseKeyCode(value) && value != KeyCode.Escape)
			{
				KeyCode result = value;
				if (value == KeyCode.LeftControl && Input.GetKeyDown(KeyCode.RightAlt))
				{
					result = KeyCode.RightAlt;
				}
				return result;
			}
		}
		return KeyCode.None;
	}

	public (KeyCode, List<KeyCode>) getAnyKeyWithMods()
	{
		List<KeyCode> list = new List<KeyCode>();
		KeyCode keyCode = KeyCode.None;
		foreach (KeyCode value in Enum.GetValues(typeof(KeyCode)))
		{
			if (!isMouseKeyCode(value) && value != KeyCode.Escape)
			{
				if (Input.GetKey(value) && isModifierKey(value))
				{
					list.Add(value);
				}
				if (keyCode == KeyCode.None && Input.GetKeyUp(value))
				{
					keyCode = value;
				}
			}
		}
		if (keyCode != KeyCode.None)
		{
			if (isModifierKey(keyCode))
			{
				list.Clear();
			}
			return (keyCode, list);
		}
		return (KeyCode.None, null);
	}

	public void keyBindingsReset()
	{
		if (!isVR)
		{
			keyBindingsSetKey(KeyBindingAction.Up, KeyCode.W, KeyCode.UpArrow);
			keyBindingsSetKey(KeyBindingAction.Left, KeyCode.A, KeyCode.LeftArrow);
			keyBindingsSetKey(KeyBindingAction.Down, KeyCode.S, KeyCode.DownArrow);
			keyBindingsSetKey(KeyBindingAction.Right, KeyCode.D, KeyCode.RightArrow);
			keyBindingsSetKey(KeyBindingAction.Crouch, KeyCode.LeftControl, KeyCode.C);
			keyBindingsSetKey(KeyBindingAction.Run, KeyCode.LeftShift, KeyCode.RightShift);
			keyBindingsSetKey(KeyBindingAction.ExamineInventory, KeyCode.Space, KeyCode.E);
			keyBindingsSetKey(KeyBindingAction.Pin, KeyCode.Tab, KeyCode.None);
			keyBindingsSetKey(KeyBindingAction.Drop, KeyCode.Q, KeyCode.None);
			keyBindingsSetKey(KeyBindingAction.Throw, KeyCode.T, KeyCode.None);
			keyBindingsSetKey(KeyBindingAction.Take, KeyCode.F, KeyCode.None);
			keyBindingsSetKey(KeyBindingAction.InstantPickupModifier, KeyCode.LeftShift, KeyCode.None);
			keyBindingsSetKey(KeyBindingAction.MpPingModifier, KeyCode.LeftAlt, KeyCode.None);
			keyBindingsSetKey(KeyBindingAction.PushToTalk, KeyCode.G, KeyCode.V);
			keyBindingsSetKey(KeyBindingAction.Zoom, KeyCode.Z, KeyCode.None);
			keyBindingsSetKey(KeyBindingAction.Chat, KeyCode.Return, KeyCode.None);
			keyBindingsSetKey(KeyBindingAction.Walkthrough, KeyCode.H, KeyCode.None);
			keyBindingsSetKey(KeyBindingAction.EmulateMouseRightClick, KeyCode.B, KeyCode.None);
		}
	}

	public void keyBindingsEditorReset()
	{
		keyBindingsSetKey(KeyBindingAction.EditorForward, KeyCode.W, KeyCode.None);
		keyBindingsSetKey(KeyBindingAction.EditorLeft, KeyCode.A, KeyCode.None);
		keyBindingsSetKey(KeyBindingAction.EditorBackward, KeyCode.S, KeyCode.None);
		keyBindingsSetKey(KeyBindingAction.EditorRight, KeyCode.D, KeyCode.None);
		keyBindingsSetKey(KeyBindingAction.EditorUp, KeyCode.E, KeyCode.None);
		keyBindingsSetKey(KeyBindingAction.EditorDown, KeyCode.Q, KeyCode.None);
		keyBindingsSetKey(KeyBindingAction.EditorRotate, KeyCode.T, KeyCode.None);
		keyBindingsSetKey(KeyBindingAction.EditorDuplicate, KeyCode.D, false, KeyCode.LeftControl);
		keyBindingsSetKey(KeyBindingAction.EditorDuplicate, KeyCode.C, true, KeyCode.LeftControl);
		keyBindingsSetKey(KeyBindingAction.EditorFocus, KeyCode.F, KeyCode.None);
		keyBindingsSetKey(KeyBindingAction.EditorDelete, KeyCode.Delete, KeyCode.X);
		keyBindingsSetKey(KeyBindingAction.EditorUndo, KeyCode.Z, false, KeyCode.LeftControl);
		keyBindingsSetKey(KeyBindingAction.EditorUndo, KeyCode.None, true);
		keyBindingsSetKey(KeyBindingAction.EditorRedo, KeyCode.Z, false, KeyCode.LeftControl, KeyCode.LeftShift);
		keyBindingsSetKey(KeyBindingAction.EditorRedo, KeyCode.None, true);
		keyBindingsSetKey(KeyBindingAction.EditorGizmoObjectSpace, KeyCode.Q, KeyCode.None);
		keyBindingsSetKey(KeyBindingAction.EditorGizmoPosition, KeyCode.W, KeyCode.None);
		keyBindingsSetKey(KeyBindingAction.EditorGizmoRotation, KeyCode.E, KeyCode.None);
		keyBindingsSetKey(KeyBindingAction.EditorGizmoScale, KeyCode.R, KeyCode.None);
		keyBindingsSetKey(KeyBindingAction.EditorSave, KeyCode.S, false, KeyCode.LeftControl);
		keyBindingsSetKey(KeyBindingAction.EditorSave, KeyCode.None, true);
		keyBindingsSetKey(KeyBindingAction.EditorHide, KeyCode.H, false);
		keyBindingsSetKey(KeyBindingAction.EditorHide, KeyCode.None, true);
		keyBindingsSetKey(KeyBindingAction.EditorHideOnlySelected, KeyCode.H, false, KeyCode.LeftAlt);
		keyBindingsSetKey(KeyBindingAction.EditorHideOnlySelected, KeyCode.None, true);
		keyBindingsSetKey(KeyBindingAction.EditorUnhideAll, KeyCode.H, false, KeyCode.LeftControl);
		keyBindingsSetKey(KeyBindingAction.EditorUnhideAll, KeyCode.None, true);
		keyBindingsSetKey(KeyBindingAction.EditorHideFocus, KeyCode.H, false, KeyCode.LeftShift);
		keyBindingsSetKey(KeyBindingAction.EditorHideFocus, KeyCode.None, true);
		keyBindingsSetKey(KeyBindingAction.EditorOpenSearch, KeyCode.Space, false, KeyCode.LeftControl);
		keyBindingsSetKey(KeyBindingAction.EditorOpenSearch, KeyCode.None, true);
		keyBindingsSetKey(KeyBindingAction.EditorToggleCodeEditor, KeyCode.BackQuote, KeyCode.None);
		keyBindingsSetKey(KeyBindingAction.EditorEnterPlayMode, KeyCode.P, KeyCode.None);
	}

	public void resetIfKeybindingExists(KeyBindingAction action, KeyCode keyCode, bool checkModifiers = false, params KeyCode[] modifiers)
	{
		KeyBinding conflictingKeyBinding = getConflictingKeyBinding(action, keyCode, checkModifiers, modifiers);
		if (conflictingKeyBinding != null)
		{
			keyBindingsSetKey(conflictingKeyBinding.action, KeyCode.None, conflictingKeyBinding.keyCodeSecondary == keyCode);
		}
	}

	public void keyBindingsSetKey(KeyBindingAction action, KeyCode keyCode, KeyCode keyCodeSecondary)
	{
		var (flag, keyBinding) = getKeyBinding(action);
		if (flag)
		{
			keyBinding.text.text = keyCode.ToString();
			keyBinding.keyCode = keyCode;
			keyBinding.textSecondary.text = keyCodeSecondary.ToString();
			keyBinding.keyCodeSecondary = keyCodeSecondary;
			keyBinding.modifiers = new List<KeyCode>();
			keyBinding.modifiersSecondary = new List<KeyCode>();
			keybindSceneRevision++;
		}
	}

	public void keyBindingsSetKey(KeyBindingAction action, KeyCode keyCode, bool isSecondary = false, params KeyCode[] modifiers)
	{
		var (flag, keyBinding) = getKeyBinding(action);
		if (!flag)
		{
			return;
		}
		string text = "";
		for (int i = 0; i < modifiers.Length; i++)
		{
			KeyCode value = modifiers[i];
			string text2 = value.ToString();
			int num = Array.IndexOf(MODIFIERS, value);
			if (num >= 0)
			{
				text2 = MODIFIERS_STRS[num];
			}
			text = text + text2 + "-";
		}
		text += keyCode;
		if (isSecondary)
		{
			if (keyBinding.textSecondary != null)
			{
				keyBinding.textSecondary.text = text;
				keyBinding.keyCodeSecondary = keyCode;
				keyBinding.modifiersSecondary = new List<KeyCode>(modifiers);
			}
		}
		else
		{
			keyBinding.text.text = text;
			keyBinding.keyCode = keyCode;
			keyBinding.modifiers = new List<KeyCode>(modifiers);
		}
		keybindSceneRevision++;
	}

	public void keyBindingsSetKey(KeyCode keycode, bool isSecondary)
	{
		keyBindingsSetKey(currentKeyBindingActionToSet, keycode, isSecondary);
	}

	public void keyBindingsSetKey(KeyCode keycode, params KeyCode[] modifiers)
	{
		keyBindingsSetKey(currentKeyBindingActionToSet, keycode, isCurrentKeyBindingSecondary, modifiers);
	}

	public void keyBindingsLoadSave()
	{
		if (PlayerSave.getSettings().keyBindings.Count == 0)
		{
			closeActions();
			return;
		}
		foreach (KeyValuePair<KeyBindingAction, KeyBinding> keyBinding in keyBindings)
		{
			KeyBinding binding = keyBinding.Value;
			PlayerSave.StoredKeyBinding storedKeyBinding = PlayerSave.getSettings().keyBindings.Find((PlayerSave.StoredKeyBinding b) => b.action == binding.action);
			if (storedKeyBinding != null)
			{
				resetIfKeybindingExists(storedKeyBinding.action, storedKeyBinding.keyCode, checkModifiers: true, storedKeyBinding.modifiers.ToArray());
				keyBindingsSetKey(storedKeyBinding.action, storedKeyBinding.keyCode, isSecondary: false, storedKeyBinding.modifiers.ToArray());
				resetIfKeybindingExists(storedKeyBinding.action, storedKeyBinding.keyCodeSecondary, checkModifiers: true, storedKeyBinding.modifiersSecondary.ToArray());
				keyBindingsSetKey(storedKeyBinding.action, storedKeyBinding.keyCodeSecondary, isSecondary: true, storedKeyBinding.modifiersSecondary.ToArray());
			}
		}
	}

	public (bool exists, KeyBinding keyBinding) getKeyBinding(KeyBindingAction action)
	{
		KeyBinding value;
		return (exists: keyBindings.TryGetValue(action, out value), keyBinding: value);
	}

	public string getKeyBindingAsString(KeyBindingAction action, string defaultIfNotFound = "")
	{
		var (flag, keyBinding) = getKeyBinding(action);
		if (!flag)
		{
			return defaultIfNotFound;
		}
		StringBuilder stringBuilder = new StringBuilder();
		foreach (KeyCode modifier in keyBinding.modifiers)
		{
			stringBuilder.Append(modifier.ToString().SplitPascalCase() + " + ");
		}
		stringBuilder.Append(keyBinding.keyCode.ToString());
		return stringBuilder.ToString();
	}

	public Sprite getKeyBindingGlyph(KeyBindingAction action)
	{
		var (flag, keyBinding) = getKeyBinding(action);
		if (!flag)
		{
			return null;
		}
		string text = Path.Combine(Application.streamingAssetsPath, "Icons", "PC", "keycodes", keyBinding.keyCode.ToString().ToLower() + ".png");
		if (File.Exists(text))
		{
			byte[] data = File.ReadAllBytes(text);
			Texture2D texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, mipChain: true);
			texture2D.LoadImage(data);
			Sprite result = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
			texture2D.wrapMode = TextureWrapMode.Clamp;
			texture2D.filterMode = FilterMode.Trilinear;
			return result;
		}
		Debug.Log($"Keybinding exists, but no glyph found for {keyBinding.keyCode}. Looking at {text}");
		return null;
	}

	public KeyBinding getConflictingKeyBinding(KeyBindingAction action, KeyCode keyCode, bool checkModifiers = false, params KeyCode[] modifiers)
	{
		KeyBindingAction[] editorMoveActions = new KeyBindingAction[6]
		{
			KeyBindingAction.EditorForward,
			KeyBindingAction.EditorLeft,
			KeyBindingAction.EditorBackward,
			KeyBindingAction.EditorRight,
			KeyBindingAction.EditorUp,
			KeyBindingAction.EditorDown
		};
		foreach (KeyValuePair<KeyBindingAction, KeyBinding> keyBinding in keyBindings)
		{
			KeyBindingAction key = keyBinding.Key;
			KeyBinding value = keyBinding.Value;
			if (areKeyBindingActionsConflictable(key, action) && isEditorMoveAction(action) == isEditorMoveAction(value.action) && ((value.keyCode == keyCode && sameModifiers(value.modifiers)) || (value.keyCodeSecondary == keyCode && sameModifiers(value.modifiersSecondary))))
			{
				return value;
			}
		}
		return null;
		bool isEditorMoveAction(KeyBindingAction value2)
		{
			return Array.IndexOf(editorMoveActions, value2) >= 0;
		}
		bool sameModifiers(List<KeyCode> keyModifiers)
		{
			if (!checkModifiers)
			{
				return true;
			}
			bool flag = modifiers.Length == keyModifiers.Count;
			for (int i = 0; i < modifiers.Length && flag; i++)
			{
				flag = keyModifiers.Contains(modifiers[i]);
			}
			return flag;
		}
	}

	private bool areKeyBindingActionsConflictable(KeyBindingAction action1, KeyBindingAction action2)
	{
		if (Array.Exists(exclusiveKeybindings, (KeyBindingAction[] x) => Array.IndexOf(x, action1) >= 0 && Array.IndexOf(x, action2) >= 0))
		{
			return false;
		}
		return true;
	}

	public KeyCode getKey(KeyBindingAction action)
	{
		var (flag, keyBinding) = getKeyBinding(action);
		if (!flag)
		{
			return KeyCode.None;
		}
		return keyBinding.keyCode;
	}

	public KeyCode getSecondaryKey(KeyBindingAction action)
	{
		var (flag, keyBinding) = getKeyBinding(action);
		if (!flag)
		{
			return KeyCode.None;
		}
		return keyBinding.keyCodeSecondary;
	}

	public bool getActionKeyDown(KeyBindingAction action, bool checkModifiers = true)
	{
		var (flag, keyBinding) = getKeyBinding(action);
		if (flag)
		{
			if (!Input.GetKeyDown(keyBinding.keyCode) || !modifiersMatch(keyBinding.modifiers, keyBinding, checkModifiers))
			{
				if (Input.GetKeyDown(keyBinding.keyCodeSecondary))
				{
					return modifiersMatch(keyBinding.modifiersSecondary, keyBinding, checkModifiers);
				}
				return false;
			}
			return true;
		}
		return false;
	}

	public bool getActionKey(KeyBindingAction action)
	{
		var (flag, keyBinding) = getKeyBinding(action);
		if (flag)
		{
			if (!Input.GetKey(keyBinding.keyCode))
			{
				return Input.GetKey(keyBinding.keyCodeSecondary);
			}
			return true;
		}
		return false;
	}

	public bool getActionKeyUp(KeyBindingAction action, bool checkModifiers = true)
	{
		var (flag, keyBinding) = getKeyBinding(action);
		if (flag)
		{
			if (!Input.GetKeyUp(keyBinding.keyCode) || !modifiersMatch(keyBinding.modifiers, keyBinding, checkModifiers))
			{
				if (Input.GetKeyUp(keyBinding.keyCodeSecondary))
				{
					return modifiersMatch(keyBinding.modifiersSecondary, keyBinding, checkModifiers);
				}
				return false;
			}
			return true;
		}
		return false;
	}

	private bool modifiersMatch(List<KeyCode> keyModifiers, KeyBinding kb, bool checkModifiers)
	{
		bool flag = true;
		for (int i = 0; i < MODIFIERS.Length && flag && checkModifiers; i++)
		{
			KeyCode keyCode = MODIFIERS[i];
			if (kb.keyCode != keyCode && kb.keyCodeSecondary != keyCode)
			{
				flag = Input.GetKey(keyCode) == keyModifiers.Contains(keyCode);
			}
		}
		return flag;
	}
}
