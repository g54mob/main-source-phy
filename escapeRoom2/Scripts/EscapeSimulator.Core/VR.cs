using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Management;

public class VR
{
	public enum Type
	{
		EnableIfDetected = 0,
		ForceNone = 1,
		ForceSimulator = 2
	}

	public enum State
	{
		None = 0,
		InitedButNone = 1,
		VR = 2,
		Simulator = 3
	}

	public enum DeviceType
	{
		MetaQuest = 0,
		ValveIndex = 1,
		HtcVive = 2
	}

	public enum FadeSource
	{
		FadeInAndOut = 0,
		CameraOutOfBounds = 1
	}

	private class FadeRequest
	{
		public float fadeInDuration;

		public float fadeOutDuration;

		public Action onFadedIn;

		public Func<bool> shouldStartFadeOut;

		public Action onFadedOut;
	}

	public static VR instance;

	public static VR instanceSplitscreen;

	public const int LEFT_CONTROLLER_ID = 1;

	public const int RIGHT_CONTROLLER_ID = 2;

	public const int LEFT_POKE_CONTROLLER_ID = 3;

	public const int RIGHT_POKE_CONTROLLER_ID = 4;

	public const int LEFT_GRIP_CONTROLLER_ID = 5;

	public const int RIGHT_GRIP_CONTROLLER_ID = 6;

	public const string MENU_LAYER = "CharacterLocal";

	public const string GAME_LAYER = "Default";

	public const string RIG_ASSET_PATH = "Assets/XR/VRRig.prefab";

	public VRRig rig;

	public VRInput input;

	public DeviceType currentDeviceType;

	public VRInputModule inputModule;

	public const float DEFAULT_OCULUS_RENDER_SCALE = 1.25f;

	public static bool oculusBetaRoomsAvailable = false;

	public static readonly List<DLC> oculusPlannedDLCs = new List<DLC>();

	public static readonly List<DLC> oculusAvailableDLCs = new List<DLC>();

	public static readonly List<DLC> oculusBoughtDLCs = new List<DLC>();

	public static readonly List<DLC> oculusBoughtDLCsWhoseDownloadWasDismissed = new List<DLC>();

	public static ulong oculusId;

	public static string oculusToken;

	public static string oculusUsername;

	public static string oculusImageUrl;

	public static Texture2D oculusImage;

	public static bool oculusHasMicrophonePermission;

	public readonly Dictionary<FadeSource, float> fadeSourceToFadeAlpha = new Dictionary<FadeSource, float>
	{
		{
			FadeSource.FadeInAndOut,
			0f
		},
		{
			FadeSource.CameraOutOfBounds,
			0f
		}
	};

	private State state;

	private int currentMockActive;

	private Coroutine fadeCanvasCoroutine;

	private readonly Queue<FadeRequest> fadeRequests = new Queue<FadeRequest>();

	private List<MockTrackedPoseDriver> mockTrackedPoseDrivers = new List<MockTrackedPoseDriver>();

	private VRSimulatorController leftSimulatorController;

	private VRSimulatorController rightSimulatorController;

	private VRSimulatorControllerState previousFocusedLeftControllerState;

	private VRSimulatorControllerState previousFocusedRightControllerState;

	private Color controllerTutorialColor = Color.white;

	private GameObject popupGameObjectToTrack;

	private float popupCurrent;

	public float vrPostWeightGoal;

	public float vrPostWeightChangeSpeed = 2f;

	public bool isFadingCanvas => fadeCanvasCoroutine != null;

	public static string oculusAuthUserId => $"oculus:{oculusId}";

	public static VR initInstance(bool isPrimaryInstance)
	{
		if (isPrimaryInstance)
		{
			return instance ?? (instance = new VR());
		}
		return instanceSplitscreen ?? (instanceSplitscreen = new VR());
	}

	public void init(Type vrType)
	{
		if (state != State.None)
		{
			return;
		}
		state = State.InitedButNone;
		if (vrType != Type.ForceNone)
		{
			InputSystem.onDeviceChange -= onDeviceChange;
			InputSystem.onDeviceChange += onDeviceChange;
			vrPostWeightGoal = 1f;
			if (vrType != Type.EnableIfDetected || initDevice())
			{
				initRig();
				initSimulator();
				initInput();
				state = ((vrType == Type.EnableIfDetected) ? State.VR : State.Simulator);
				setInputSettings(state);
				Debug.Log($"VR successfully initialized (state: {state}).");
			}
		}
		static void addForeach(Dictionary<GameObject, List<Material>> materials, GameObject[] models)
		{
			foreach (GameObject gameObject in models)
			{
				Renderer component = gameObject.GetComponent<Renderer>();
				if (!(component == null))
				{
					if (materials.ContainsKey(gameObject))
					{
						materials.Remove(gameObject);
					}
					materials.Add(gameObject, new List<Material>(component.sharedMaterials));
				}
			}
		}
		static VRSimulatorController createVRSimulatorController(InternedString usage)
		{
			InputDevice inputDevice = InputSystem.AddDevice(new InputDeviceDescription
			{
				interfaceName = "VRSimulatorController",
				capabilities = new XRDeviceDescriptor
				{
					deviceName = string.Format("{0} - {1}", "VRSimulatorController", usage)
				}.ToJson()
			});
			InputSystem.SetDeviceUsage(inputDevice, usage);
			return inputDevice as VRSimulatorController;
		}
		static bool initDevice()
		{
			if (XRGeneralSettings.Instance == null)
			{
				return false;
			}
			if (XRGeneralSettings.Instance.Manager == null)
			{
				return false;
			}
			IReadOnlyList<XRLoader> activeLoaders = XRGeneralSettings.Instance.Manager.activeLoaders;
			Debug.Log(string.Format("There are {0} active XR loaders: {1}", activeLoaders.Count, string.Join(", ", activeLoaders)));
			if (activeLoaders.Count == 0)
			{
				XRGeneralSettings.Instance.Manager.DeinitializeLoader();
				return false;
			}
			XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
			if (XRGeneralSettings.Instance.Manager.activeLoader == null)
			{
				return false;
			}
			XRGeneralSettings.Instance.Manager.StartSubsystems();
			return true;
		}
		void initInput()
		{
			input = new VRInput();
			if (Is.Android)
			{
				input.LeftHand.Menu.AddBinding("<XRController>{LeftHand}/start");
				Debug.Log("Added binding for left controller menu button: <XRController>{LeftHand}/start");
				if (!Is.Editor)
				{
					InputBinding bindingOverride = input.LeftHand.RawPosition.bindings[0];
					bindingOverride.overridePath = "<OculusTouchController>{LeftHand}/{DevicePosition}";
					input.LeftHand.RawPosition.ApplyBindingOverride(0, bindingOverride);
					rig.leftController.trackedPoseDriver.positionAction = input.LeftHand.RawPosition;
					bindingOverride = input.LeftHand.RawRotation.bindings[0];
					bindingOverride.overridePath = "<OculusTouchController>{LeftHand}/{DeviceRotation}";
					input.LeftHand.RawRotation.ApplyBindingOverride(0, bindingOverride);
					rig.leftController.trackedPoseDriver.rotationAction = input.LeftHand.RawRotation;
					bindingOverride = input.RightHand.RawPosition.bindings[0];
					bindingOverride.overridePath = "<OculusTouchController>{RightHand}/{DevicePosition}";
					input.RightHand.RawPosition.ApplyBindingOverride(0, bindingOverride);
					rig.rightController.trackedPoseDriver.positionAction = input.RightHand.RawPosition;
					bindingOverride = input.RightHand.RawRotation.bindings[0];
					bindingOverride.overridePath = "<OculusTouchController>{RightHand}/{DeviceRotation}";
					input.RightHand.RawRotation.ApplyBindingOverride(0, bindingOverride);
					rig.rightController.trackedPoseDriver.rotationAction = input.RightHand.RawRotation;
				}
			}
			input.Enable();
			rig.headTrackedPoseDriver.positionAction.Enable();
			rig.headTrackedPoseDriver.rotationAction.Enable();
			rig.leftController.trackedPoseDriver.positionAction.Enable();
			rig.leftController.trackedPoseDriver.rotationAction.Enable();
			rig.rightController.trackedPoseDriver.positionAction.Enable();
			rig.rightController.trackedPoseDriver.rotationAction.Enable();
		}
		void initRig()
		{
			rig = UnityEngine.Object.Instantiate(AssetBundleLoader.getAsset<GameObject>(AssetBundleType.VR, "Assets/XR/VRRig.prefab")).GetComponent<VRRig>();
			UnityEngine.Object.DontDestroyOnLoad(rig);
			rig.smoothedCamera.gameObject.SetActive(value: false);
			if (rig.defaultSleeves == null)
			{
				rig.defaultSleeves = new Dictionary<GameObject, List<Material>>();
				addForeach(rig.defaultSleeves, new GameObject[2]
				{
					rig.leftController.handMesh.gameObject,
					rig.rightController.handMesh.gameObject
				});
				List<GameObject> list = new List<GameObject>(rig.leftController.shortSleeves);
				list.AddRange(rig.rightController.shortSleeves);
				addForeach(rig.defaultSleeves, list.ToArray());
			}
			initTutorialController(rig.leftController);
			initTutorialController(rig.rightController);
		}
		void initSimulator()
		{
			if (vrType == Type.ForceSimulator)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				List<TrackedPoseDriver> list = new List<TrackedPoseDriver>
				{
					rig.headTrackedPoseDriver,
					rig.leftController.trackedPoseDriver,
					rig.rightController.trackedPoseDriver
				};
				foreach (TrackedPoseDriver item2 in list)
				{
					MockTrackedPoseDriver item = item2.gameObject.AddComponent<MockTrackedPoseDriver>();
					mockTrackedPoseDrivers.Add(item);
					UnityEngine.Object.Destroy(item2);
				}
				MockTrackedPoseDriver component = list[1].GetComponent<MockTrackedPoseDriver>();
				component.parent = rig.headCamera.transform;
				component.parentOffset = new Vector3(-0.15f, -0.1f, 0.5f);
				MockTrackedPoseDriver component2 = list[2].GetComponent<MockTrackedPoseDriver>();
				component2.parent = rig.headCamera.transform;
				component2.parentOffset = new Vector3(0.2f, -0.1f, 0.5f);
				if (leftSimulatorController == null)
				{
					leftSimulatorController = createVRSimulatorController(CommonUsages.LeftHand);
				}
				if (rightSimulatorController == null)
				{
					rightSimulatorController = createVRSimulatorController(CommonUsages.RightHand);
				}
			}
		}
		static void initTutorialController(VRRig.Controller controller)
		{
			VRTutorialControllerButton[] componentsInChildren = controller.transform.GetComponentsInChildren<VRTutorialControllerButton>(includeInactive: true);
			foreach (VRTutorialControllerButton vRTutorialControllerButton in componentsInChildren)
			{
				vRTutorialControllerButton.line.positionCount = vRTutorialControllerButton.lines.Length;
				List<Vector3> list = new List<Vector3>();
				Transform[] lines = vRTutorialControllerButton.lines;
				foreach (Transform transform in lines)
				{
					list.Add(transform.localPosition);
					transform.gameObject.SetActive(value: false);
				}
				vRTutorialControllerButton.line.SetPositions(list.ToArray());
				if (vRTutorialControllerButton.type == VRTutorialControllerButton.ButtonType.Menu)
				{
					vRTutorialControllerButton.gameObject.SetActive(Is.Android);
				}
			}
			Localization.translateObject(controller.transform);
		}
	}

	private void onDeviceChange(InputDevice device, InputDeviceChange change)
	{
		if (change != InputDeviceChange.Added || Is.Android)
		{
			return;
		}
		if (!device.name.Contains("OpenXR"))
		{
			debugLog("Added device '" + device.name + "' is not a VR device. Skipping...");
			return;
		}
		DeviceType deviceType = currentDeviceType;
		Debug.Log("Added VR device named '" + device.name + "'.");
		if (device.name.Contains("Meta") || device.name.Contains("Quest") || device.name.Contains("Oculus"))
		{
			currentDeviceType = DeviceType.MetaQuest;
		}
		else if (device.name.Contains("Valve") || device.name.Contains("Index"))
		{
			currentDeviceType = DeviceType.ValveIndex;
		}
		else if (device.name.Contains("Htc") || device.name.Contains("Vive"))
		{
			currentDeviceType = DeviceType.HtcVive;
		}
		if (currentDeviceType != deviceType)
		{
			debugLog($"Device type changed from '{deviceType}' to '{currentDeviceType}'.");
		}
	}

	public VRInputModule initEventSystem()
	{
		if (inputModule != null)
		{
			return inputModule;
		}
		BaseInputModule[] components = EventSystem.current.gameObject.GetComponents<BaseInputModule>();
		foreach (BaseInputModule baseInputModule in components)
		{
			Debug.Log($"Destroying input module '{baseInputModule}' ({baseInputModule.GetType()}).");
			UnityEngine.Object.Destroy(baseInputModule);
		}
		inputModule = EventSystem.current.gameObject.AddComponent<VRInputModule>();
		inputModule.rightHandInputModule = createInputModule(isRightHand: true);
		inputModule.leftHandInputModule = createInputModule(isRightHand: false);
		return inputModule;
		InputSystemUIInputModule createInputModule(bool isRightHand)
		{
			InputSystemUIInputModule inputSystemUIInputModule = EventSystem.current.gameObject.AddComponent<InputSystemUIInputModule>();
			inputSystemUIInputModule.point = null;
			inputSystemUIInputModule.submit = null;
			inputSystemUIInputModule.middleClick = null;
			inputSystemUIInputModule.rightClick = null;
			InputAction action = (isRightHand ? input.RightHand.Trigger : input.LeftHand.Trigger);
			inputSystemUIInputModule.leftClick = InputActionReference.Create(action);
			InputAction action2 = (isRightHand ? input.RightHand.Scroll : input.LeftHand.Scroll);
			inputSystemUIInputModule.scrollWheel = InputActionReference.Create(action2);
			if (Is.Android && getActiveState() == State.VR)
			{
				InputAction action3 = (isRightHand ? input.RightHand.PointerPositionQuest : input.LeftHand.PointerPositionQuest);
				inputSystemUIInputModule.trackedDevicePosition = InputActionReference.Create(action3);
				InputAction action4 = (isRightHand ? input.RightHand.PointerRotationQuest : input.LeftHand.PointerRotationQuest);
				inputSystemUIInputModule.trackedDeviceOrientation = InputActionReference.Create(action4);
			}
			else
			{
				InputAction action5 = (isRightHand ? input.RightHand.PointerPosition : input.LeftHand.PointerPosition);
				inputSystemUIInputModule.trackedDevicePosition = InputActionReference.Create(action5);
				InputAction action6 = (isRightHand ? input.RightHand.PointerRotation : input.LeftHand.PointerRotation);
				inputSystemUIInputModule.trackedDeviceOrientation = InputActionReference.Create(action6);
			}
			return inputSystemUIInputModule;
		}
	}

	public void update()
	{
		updatePressIndicators();
		updateFingerAnimations();
		updateFadeCanvas();
		updateFadeRequests();
		updatePopup();
		updateControllerVisualsPose();
		updateGripInputState();
		updateTutorialControllers();
	}

	private void updatePressIndicators()
	{
		updatePressIndicator(rig.leftController, input.LeftHand.Trigger, input.LeftHand.Grip);
		updatePressIndicator(rig.rightController, input.RightHand.Trigger, input.RightHand.Grip);
		void updatePressIndicator(VRRig.Controller controller, InputAction triggerInput, InputAction gripInput)
		{
			Color color = controller.raycastPressIndicatorRenderer.color;
			color.a = Mathf.Max(triggerInput.ReadValue<float>(), gripInput.ReadValue<float>()) * rig.hitIndicatorAlpha;
			controller.raycastPressIndicatorRenderer.color = color;
		}
	}

	private void updateFingerAnimations()
	{
		setFingerAnimations(rig.leftController, input.LeftHand.Trigger, input.LeftHand.Grip);
		setFingerAnimations(rig.rightController, input.RightHand.Trigger, input.RightHand.Grip);
		static void setFingerAnimations(VRRig.Controller controller, InputAction triggerAction, InputAction gripAction)
		{
			controller.handAnimator.SetFloat("Trigger", triggerAction.ReadValue<float>());
			controller.handAnimator.SetFloat("Grip", gripAction.ReadValue<float>());
		}
	}

	private void updateFadeCanvas()
	{
		float num = 0f;
		foreach (KeyValuePair<FadeSource, float> item in fadeSourceToFadeAlpha)
		{
			item.Deconstruct(out var _, out var value);
			float b = value;
			num = Mathf.Max(num, b);
		}
		rig.fadeEffect.fadeAlpha = num;
	}

	private void updateFadeRequests()
	{
		if (!isFadingCanvas && fadeRequests.Count != 0)
		{
			FadeRequest fadeRequest = fadeRequests.Dequeue();
			fadeCanvasCoroutine = Executor.executeCoroutine(performFadeRequest(fadeRequest));
		}
	}

	private void updatePopup()
	{
		float num = ((popupGameObjectToTrack != null) ? 1 : 0);
		Vector3 vector = rig.headCamera.transform.position + rig.headCamera.transform.up * -1f;
		if (num == 0f)
		{
			popupCurrent = 0f;
			rig.popupCanvas.transform.position = vector;
		}
		else
		{
			popupCurrent = Mathf.MoveTowards(popupCurrent, num, Time.deltaTime * 3f);
			Vector3 b = rig.headCamera.transform.position + rig.headCamera.transform.forward + Vector3.up * 0.15f;
			Vector3 b2 = Vector3.Lerp(vector, b, popupCurrent);
			rig.popupCanvas.transform.position = Vector3.Lerp(rig.popupCanvas.transform.position, b2, Time.deltaTime * 10f / 5f);
		}
		rig.popupCanvas.transform.rotation = Quaternion.LookRotation(rig.popupCanvas.transform.position - rig.headCamera.transform.position);
	}

	private void updateControllerVisualsPose()
	{
		VRRig.DeviceSpecificSettings activeDeviceSettings = getActiveDeviceSettings();
		rig.leftController.trackedPoseDriverFollower.positionOffset = activeDeviceSettings.leftControllerLocalPosition;
		rig.leftController.trackedPoseDriverFollower.rotationOffset = activeDeviceSettings.leftControllerLocalRotation.eulerAngles;
		rig.rightController.trackedPoseDriverFollower.positionOffset = activeDeviceSettings.rightControllerLocalPosition;
		rig.rightController.trackedPoseDriverFollower.rotationOffset = activeDeviceSettings.rightControllerLocalRotation.eulerAngles;
	}

	private void updateGripInputState()
	{
		updateGripInputState(rig.leftController.gripState, input.LeftHand.Grip);
		updateGripInputState(rig.rightController.gripState, input.RightHand.Grip);
		void updateGripInputState(VRRig.GripState gripState, InputAction gripAction)
		{
			float num = gripAction.ReadValue<float>();
			float gripPressThreshold = PlayerSave.getSettings().gripPressThreshold;
			float num2 = ((currentDeviceType == DeviceType.ValveIndex) ? 0.01f : (gripPressThreshold - 0.15f));
			if (gripState.isPressed)
			{
				if (num < num2)
				{
					gripState.wasReleasedThisFrame = true;
					gripState.isPressed = false;
				}
				gripState.wasPressedThisFrame = false;
			}
			else
			{
				if (num >= gripPressThreshold)
				{
					gripState.wasPressedThisFrame = true;
					gripState.isPressed = true;
				}
				gripState.wasReleasedThisFrame = false;
			}
		}
	}

	private void updateTutorialControllers()
	{
		bool flag = false;
		foreach (VRTutorialController tutorialController in rig.leftController.tutorialControllers)
		{
			if (tutorialController.gameObject.activeSelf)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			updateVRTutorialController(1);
			updateVRTutorialController(2);
		}
	}

	private void updateVRTutorialController(int controllerId)
	{
		VRRig.Controller controllerForId = getControllerForId(controllerId);
		Color nonInputColor = controllerTutorialColor;
		Color hasInputColor = new Color(0.22f, 0.71f, 1f);
		foreach (VRTutorialController tutorialController in controllerForId.tutorialControllers)
		{
			if (tutorialController.type != currentDeviceType)
			{
				continue;
			}
			foreach (VRTutorialControllerButton button2 in tutorialController.buttons)
			{
				VRTutorialControllerButton button = button2;
				switch (button.type)
				{
				case VRTutorialControllerButton.ButtonType.Trigger:
					updateInputColor(isLeftControllerId(controllerId) ? input.LeftHand.Trigger : input.RightHand.Trigger);
					button.text.text = "%vrTutorialTriggerButton2%";
					Localization.translateObject(button.text.transform, forceNewText: true);
					break;
				case VRTutorialControllerButton.ButtonType.Grip:
					updateInputColor(isLeftControllerId(controllerId) ? input.LeftHand.Grip : input.RightHand.Grip);
					break;
				case VRTutorialControllerButton.ButtonType.PrimaryButton:
					updateInputColor(isLeftControllerId(controllerId) ? input.LeftHand.PrimaryButton : input.RightHand.PrimaryButton);
					break;
				case VRTutorialControllerButton.ButtonType.SecondaryButton:
					updateInputColor(isLeftControllerId(controllerId) ? input.LeftHand.SecondaryButton : input.RightHand.SecondaryButton);
					if (isLeftControllerId(controllerId) && Is.Android)
					{
						string[] array2 = button.text.text.Split('\n');
						if (array2.Length == 2)
						{
							button.text.text = array2[1];
						}
					}
					break;
				case VRTutorialControllerButton.ButtonType.Thumbstick:
				{
					InputAction inputAction = (isLeftControllerId(controllerId) ? input.LeftHand.Primary2DAxis : input.RightHand.Primary2DAxis);
					Color color = ((isLeftControllerId(controllerId) ? input.LeftHand.Primary2DAxisClick : input.RightHand.Primary2DAxisClick).IsPressed() ? hasInputColor : Color.Lerp(nonInputColor, hasInputColor, inputAction.ReadValue<Vector2>().magnitude));
					button.line.startColor = color;
					button.line.endColor = color;
					Game.MovementScheme movementScheme = (isLeftControllerId(controllerId) ? PlayerSave.getSettings().leftHandMovementScheme : PlayerSave.getSettings().rightHandMovementScheme);
					if (movementScheme == Game.MovementScheme.Off)
					{
						button.text.text = string.Empty;
						button.line.enabled = false;
					}
					else
					{
						bool flag = movementScheme != Game.MovementScheme.Teleport;
						button.text.text = ((!flag) ? (PlayerSave.getSettings().holdDownToTurnAround ? "%vrTeleportThumbstickWithTurnBack%" : "%vrTutorialJoystickButton%") : ((movementScheme == Game.MovementScheme.SmoothWithTurn) ? "%vrSmoothOnlyThumbstick%" : "%vrTutorialThumbstickButtonMovement%"));
						button.line.enabled = true;
					}
					button.text.color = color;
					Localization.translateObject(button.text.transform, forceNewText: true);
					break;
				}
				case VRTutorialControllerButton.ButtonType.Menu:
					if (isLeftControllerId(controllerId) && Is.Android)
					{
						updateInputColor(input.LeftHand.Menu);
						string[] array = button.text.text.Split('\n');
						if (array.Length == 2)
						{
							button.text.text = array[0];
						}
					}
					break;
				}
				void updateInputColor(InputAction inputAction2)
				{
					Color color2 = Color.Lerp(nonInputColor, hasInputColor, inputAction2.ReadValue<float>());
					button.line.startColor = color2;
					button.line.endColor = color2;
					button.text.color = color2;
				}
			}
		}
	}

	public VRRig.DeviceSpecificSettings getActiveDeviceSettings()
	{
		foreach (VRRig.DeviceSpecificSettings deviceSpecificSetting in rig.deviceSpecificSettings)
		{
			if (deviceSpecificSetting.deviceType == currentDeviceType)
			{
				return deviceSpecificSetting;
			}
		}
		Debug.LogError($"Could not device specific settings for current device type: {currentDeviceType}");
		return rig.deviceSpecificSettings[0];
	}

	public void updateVRSimulatorInput()
	{
		if (getActiveState() == State.Simulator)
		{
			updateVRSimulatorControllerInput(2, ref previousFocusedRightControllerState);
			updateVRSimulatorControllerInput(1, ref previousFocusedLeftControllerState);
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				currentMockActive++;
				currentMockActive %= mockTrackedPoseDrivers.Count;
			}
			for (int i = 0; i < mockTrackedPoseDrivers.Count; i++)
			{
				bool flag = i == currentMockActive;
				mockTrackedPoseDrivers[i].update(flag);
			}
			InputSystem.Update();
		}
	}

	private void updateVRSimulatorControllerInput(int controllerId, ref VRSimulatorControllerState previousFocusedControllerState)
	{
		VRSimulatorController device = ((controllerId == 2) ? rightSimulatorController : leftSimulatorController);
		VRRig.Controller controllerForId = getControllerForId(controllerId);
		Vector2 zero = Vector2.zero;
		Keyboard current = Keyboard.current;
		if ((controllerId == 1) ? current.wKey.isPressed : current.upArrowKey.isPressed)
		{
			zero.y += 1f;
		}
		if ((controllerId == 1) ? current.aKey.isPressed : current.leftArrowKey.isPressed)
		{
			zero.x -= 1f;
		}
		if ((controllerId == 1) ? current.sKey.isPressed : current.downArrowKey.isPressed)
		{
			zero.y -= 1f;
		}
		if ((controllerId == 1) ? current.dKey.isPressed : current.rightArrowKey.isPressed)
		{
			zero.x += 1f;
		}
		VRSimulatorControllerState vRSimulatorControllerState = new VRSimulatorControllerState
		{
			devicePosition = controllerForId.raycastSource.position,
			deviceRotation = controllerForId.raycastSource.rotation,
			primary2DAxis = zero
		};
		bool flag = ((controllerId == 1) ? Keyboard.current.shiftKey.isPressed : Keyboard.current.ctrlKey.isPressed);
		vRSimulatorControllerState.WithButton(VRSimulatorControllerButton.Primary2DAxisClick, flag);
		bool flag2 = ((controllerId == 1) ? Keyboard.current.nKey.isPressed : Keyboard.current.mKey.isPressed);
		vRSimulatorControllerState.WithButton(VRSimulatorControllerButton.SecondaryButton, flag2);
		if (currentMockActive == controllerId)
		{
			bool isPressed = Mouse.current.leftButton.isPressed;
			vRSimulatorControllerState.WithButton(VRSimulatorControllerButton.TriggerButton, isPressed);
			vRSimulatorControllerState.trigger = (isPressed ? 1 : 0);
			bool isPressed2 = Mouse.current.rightButton.isPressed;
			vRSimulatorControllerState.WithButton(VRSimulatorControllerButton.GripButton, isPressed2);
			vRSimulatorControllerState.grip = (isPressed2 ? 1 : 0);
			bool isPressed3 = Keyboard.current.spaceKey.isPressed;
			vRSimulatorControllerState.WithButton(VRSimulatorControllerButton.PrimaryButton, isPressed3);
			previousFocusedControllerState = vRSimulatorControllerState;
		}
		else
		{
			vRSimulatorControllerState.WithButton(VRSimulatorControllerButton.TriggerButton, previousFocusedControllerState.HasButton(VRSimulatorControllerButton.TriggerButton));
			vRSimulatorControllerState.trigger = previousFocusedControllerState.trigger;
			vRSimulatorControllerState.WithButton(VRSimulatorControllerButton.GripButton, previousFocusedControllerState.HasButton(VRSimulatorControllerButton.GripButton));
			vRSimulatorControllerState.grip = previousFocusedControllerState.grip;
			vRSimulatorControllerState.WithButton(VRSimulatorControllerButton.PrimaryButton, previousFocusedControllerState.HasButton(VRSimulatorControllerButton.PrimaryButton));
		}
		InputSystem.QueueStateEvent(device, vRSimulatorControllerState);
	}

	public bool isActive()
	{
		State state = this.state;
		return state == State.VR || state == State.Simulator;
	}

	public State getActiveState()
	{
		return state;
	}

	public void setLayerToRenderers(string layer)
	{
		int layerValue = LayerMask.NameToLayer(layer);
		changeLayer(rig.leftController.visuals.transform);
		changeLayer(rig.rightController.visuals.transform);
		void changeLayer(Transform root)
		{
			root.gameObject.layer = layerValue;
			foreach (Transform item in root)
			{
				changeLayer(item);
			}
		}
	}

	public void setCustomization(bool useLongSleeves, Color handColor, bool isTutorial, bool isMenu, bool brightControllerTutorialColor = false)
	{
		if (isActive())
		{
			setSleeves(rig.leftController.longSleeves, areLong: true);
			setSleeves(rig.leftController.shortSleeves, areLong: false);
			setSleeves(rig.rightController.longSleeves, areLong: true);
			setSleeves(rig.rightController.shortSleeves, areLong: false);
			setTutorialControllers(rig.leftController.tutorialControllers);
			setTutorialControllers(rig.rightController.tutorialControllers);
			rig.rightController.handMesh.gameObject.SetActive(!isTutorial);
			rig.leftController.handMesh.gameObject.SetActive(!isTutorial);
			rig.leftController.watch.gameObject.SetActive(!isTutorial && !isMenu);
			rig.rightController.watch.gameObject.SetActive(!isTutorial && !isMenu);
			controllerTutorialColor = (brightControllerTutorialColor ? Color.white : Color.grey);
		}
		static void setSleeves(List<GameObject> sleeves, bool areLong)
		{
			for (int i = 0; i < sleeves.Count; i++)
			{
			}
		}
		void setTutorialControllers(List<VRTutorialController> controllers)
		{
			foreach (VRTutorialController controller in controllers)
			{
				controller.gameObject.SetActive(isTutorial && currentDeviceType == controller.type);
			}
		}
	}

	public void vibrateController(int controllerId, float intensity, float duration)
	{
		if (getActiveState() == State.Simulator)
		{
			return;
		}
		float num = ((controllerId == 1) ? PlayerSave.getSettings().leftHandVibrationIntensity : PlayerSave.getSettings().rightHandVibrationIntensity);
		if (num != 0f)
		{
			XRController xRController = (isLeftControllerId(controllerId) ? XRController.leftHand : XRController.rightHand);
			if (xRController != null)
			{
				SendHapticImpulseCommand command = SendHapticImpulseCommand.Create(0, intensity * num, duration);
				xRController.ExecuteCommand(ref command);
			}
		}
	}

	public VRRig.Controller getControllerForId(int id)
	{
		if (!isLeftControllerId(id))
		{
			return rig.rightController;
		}
		return rig.leftController;
	}

	public VRRig.Controller getOppositeControllerForId(int id)
	{
		if (!isLeftControllerId(id))
		{
			return rig.leftController;
		}
		return rig.rightController;
	}

	public static bool isTriggerControllerId(int controllerId)
	{
		if (controllerId != 1)
		{
			return controllerId == 2;
		}
		return true;
	}

	public static bool isLeftControllerId(int controllerId)
	{
		if (controllerId != 1 && controllerId != 3)
		{
			return controllerId == 5;
		}
		return true;
	}

	public static bool isRaycastController(int controllerId)
	{
		if (controllerId != 1 && controllerId != 5 && controllerId != 2)
		{
			return controllerId == 6;
		}
		return true;
	}

	public static int getSiblingButtonId(int controllerId)
	{
		return controllerId switch
		{
			5 => 1, 
			1 => 5, 
			6 => 2, 
			2 => 6, 
			_ => -1, 
		};
	}

	public VRRaycaster setupCanvas(Canvas canvas)
	{
		BaseRaycaster[] components = canvas.gameObject.GetComponents<BaseRaycaster>();
		for (int i = 0; i < components.Length; i++)
		{
			UnityEngine.Object.Destroy(components[i]);
		}
		canvas.renderMode = RenderMode.WorldSpace;
		canvas.worldCamera = rig.headCamera;
		return canvas.gameObject.AddComponent<VRRaycaster>();
	}

	public void mapCanvas(Canvas canvasToEdit, Canvas templateCanvas)
	{
		RectTransform component = canvasToEdit.GetComponent<RectTransform>();
		RectTransform component2 = templateCanvas.GetComponent<RectTransform>();
		component.position = component2.position;
		component.rotation = component2.rotation;
		component.pivot = component2.pivot;
		component.SetGlobalScale(component2.lossyScale);
		component.sizeDelta = component2.sizeDelta;
	}

	public void setRigPosition(Vector3 position)
	{
		if (isActive())
		{
			Vector3 position2 = position + (rig.transform.position - rig.headCamera.transform.position);
			position2.y = position.y;
			rig.transform.position = position2;
		}
	}

	public void setRigDirection(Vector3 direction)
	{
		if (isActive())
		{
			Vector3 to = direction;
			to.y = 0f;
			Vector3 forward = rig.headCamera.transform.forward;
			forward.y = 0f;
			float angle = Vector3.SignedAngle(forward, to, Vector3.up);
			rig.transform.RotateAround(rig.headCamera.transform.position, Vector3.up, angle);
		}
	}

	public void setRigLayer(string layerName)
	{
		int targetLayer = LayerMask.NameToLayer(layerName);
		int dontRecordLayer = LayerMask.NameToLayer("es2deleted:DontRecordInVRSmoothed");
		UnityUtils.visitGameObject(rig.gameObject, delegate(GameObject child)
		{
			child.layer = ((child.layer == dontRecordLayer) ? dontRecordLayer : targetLayer);
		});
	}

	public void setBlackScreenScene(List<Canvas> canvasesToShow)
	{
		setRigLayer("UI");
		int uiLayer = LayerMask.NameToLayer("UI");
		foreach (Canvas item in canvasesToShow)
		{
			UnityUtils.visitGameObject(item.gameObject, delegate(GameObject childObject)
			{
				childObject.layer = uiLayer;
			});
		}
		rig.headCamera.cullingMask = LayerMask.GetMask("UI", "es2deleted:DontRecordInVRSmoothed");
		rig.headCamera.clearFlags = CameraClearFlags.Color;
		rig.headCamera.backgroundColor = Color.black;
		vrPostWeightGoal = 0f;
		VRRaycaster.isOcclusionEnabled = false;
		VRRaycaster.focusedCanvases = canvasesToShow;
	}

	public void clearRig()
	{
		rig.transform.position = Vector3.zero;
		rig.transform.rotation = Quaternion.identity;
		rig.wallSphere.SetActive(value: false);
		rig.headCameraOffset.localPosition = Vector3.zero;
		rig.vignette.amounts.Clear();
		rig.skyConfettiEffect.gameObject.SetActive(value: false);
		rig.headCamera.nearClipPlane = 0.01f;
		rig.headCamera.farClipPlane = 1000f;
		rig.leftController.visuals.SetActive(value: true);
		rig.leftController.lastFrameTeleportInput = Vector2.zero;
		rig.leftController.movementThumbstickState = Game.MovementThumbstickState.WaitingForInput;
		rig.leftController.rotateThumbstickState = Game.RotateThumbstickState.WaitingForInput;
		rig.leftController.teleportLineAlphaCurrent = -1f;
		rig.leftController.uiHitLastFrame = null;
		rig.leftController.uiVibratorOnLastHitChange = null;
		rig.leftController.hoveredPocketLastFrame = null;
		rig.leftController.grabData = null;
		rig.leftController.hoveredSlot = null;
		rig.leftController.touchedSwitchLastFrame = null;
		rig.leftController.raycastDestinationIndicatorProgress.fillAmount = 0f;
		rig.rightController.visuals.SetActive(value: true);
		rig.rightController.lastFrameTeleportInput = Vector2.zero;
		rig.rightController.movementThumbstickState = Game.MovementThumbstickState.WaitingForInput;
		rig.rightController.rotateThumbstickState = Game.RotateThumbstickState.WaitingForInput;
		rig.rightController.teleportLineAlphaCurrent = -1f;
		rig.rightController.uiHitLastFrame = null;
		rig.rightController.uiVibratorOnLastHitChange = null;
		rig.rightController.hoveredPocketLastFrame = null;
		rig.rightController.grabData = null;
		rig.rightController.hoveredSlot = null;
		rig.rightController.touchedSwitchLastFrame = null;
		rig.rightController.raycastDestinationIndicatorProgress.fillAmount = 0f;
		foreach (Transform item in rig.leftController.inventorySource)
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}
		foreach (Transform item2 in rig.rightController.inventorySource)
		{
			UnityEngine.Object.Destroy(item2.gameObject);
		}
		foreach (Transform item3 in rig.popupCanvas.transform)
		{
			UnityEngine.Object.Destroy(item3.gameObject);
		}
	}

	public void updateControllerRayBasedOnUI(int controllerId, VRInputModule vrInputModule)
	{
		if (!isActive())
		{
			return;
		}
		VRRig.Controller controllerForId = getControllerForId(controllerId);
		if (vrInputModule.tryGetLastRaycastResult(controllerId, out var raycastResult))
		{
			setControllerRayToTarget(controllerId, raycastResult.worldPosition, raycastResult.worldNormal);
			setControllerIndicatorState(controllerId, isShown: true);
		}
		else
		{
			setControllerRayToTarget(controllerId, controllerForId.raycastSource.position + controllerForId.raycastSource.forward * 10f, raycastResult.worldNormal);
			setControllerIndicatorState(controllerId, isShown: false);
		}
		GameObject gameObject = raycastResult.gameObject;
		if (gameObject != controllerForId.uiHitLastFrame)
		{
			GameObject gameObject2 = getUiVibratorFromUiHit(gameObject);
			if (gameObject2 != null)
			{
				setControllerRayColor(controllerId, isHovering: true);
				if (gameObject2 != controllerForId.uiVibratorOnLastHitChange)
				{
					vibrateController(controllerId, 0.1f, 0.05f);
				}
			}
			else
			{
				setControllerRayColor(controllerId, isHovering: false);
			}
			controllerForId.uiVibratorOnLastHitChange = gameObject2;
		}
		controllerForId.uiHitLastFrame = gameObject;
		static GameObject getUiVibratorFromUiHit(GameObject uiHit)
		{
			if (uiHit == null)
			{
				return null;
			}
			if (uiHit.TryGetComponentInParent<Button>(out var component) && uiHit.name != "Blocker")
			{
				return component.gameObject;
			}
			if (uiHit.TryGetComponentInParent<EventButton>(out var component2))
			{
				return component2.gameObject;
			}
			if (uiHit.TryGetComponentInParent<Toggle>(out var component3))
			{
				return component3.gameObject;
			}
			if (uiHit.TryGetComponentInParent<Slider>(out var component4))
			{
				return component4.gameObject;
			}
			if (uiHit.TryGetComponentInParent<Dropdown>(out var component5))
			{
				return component5.gameObject;
			}
			if (uiHit.TryGetComponentInParent<InputField>(out var component6))
			{
				return component6.gameObject;
			}
			if (uiHit.TryGetComponentInParent<Scrollbar>(out var component7))
			{
				return component7.gameObject;
			}
			return null;
		}
	}

	public void setControllerRayToTarget(int controllerId, Vector3 hitPoint, Vector3 hitNormal)
	{
		VRRig.Controller controllerForId = getControllerForId(controllerId);
		controllerForId.hitPoint = hitPoint;
		controllerForId.raycastLineRenderer.positionCount = 8;
		bool num = controllerForId.raycastSource.localPosition != Vector3.zero;
		Vector3 vector = controllerForId.raycastSource.TransformPoint(new Vector3(0f, 0f, 0.25f)) - controllerForId.raycastSource.position;
		Vector3 vector2 = (num ? Vector3.zero : vector);
		Vector3 vector3 = controllerForId.raycastSource.position + vector2;
		Vector3 vector4 = (hitPoint - vector3) / 7f;
		for (int i = 0; i < 8; i++)
		{
			Vector3 position = vector3 + vector4 * i;
			controllerForId.raycastLineRenderer.SetPosition(i, position);
		}
		if (hitNormal != Vector3.zero)
		{
			Vector3 vector5 = hitPoint + hitNormal.normalized * 0.01f;
			controllerForId.raycastDestinationIndicator.transform.position = vector5;
			controllerForId.raycastDestinationIndicator.transform.rotation = Quaternion.LookRotation(hitNormal);
			float distanceFromCamera = Vector3.Distance(rig.headCamera.transform.position, vector5);
			controllerForId.raycastDestinationIndicator.transform.localScale = Vector3.one * calculateHitIndicatorScale(distanceFromCamera);
		}
		static float calculateHitIndicatorScale(float num2)
		{
			return 0.015f * num2 + 0.015f;
		}
	}

	public void setControllerRayColor(int controllerId, bool isHovering)
	{
		VRRig.Controller controllerForId = getControllerForId(controllerId);
		controllerForId.raycastLineRenderer.colorGradient = (isHovering ? rig.hoveringRayGradient : rig.defaultRayGradient);
		Color color = (isHovering ? rig.hoveringHitIndicatorColor : rig.defaultHitIndicatorColor);
		color.a = rig.hitIndicatorAlpha;
		controllerForId.raycastDestinationIndicatorRenderer.color = color;
		controllerForId.raycastDestinationIndicatorProgress.color = color;
		Color color2 = (isHovering ? rig.hoveringHitIndicatorColor : rig.defaultHitIndicatorColor);
		color2.a = controllerForId.raycastPressIndicatorRenderer.color.a;
		controllerForId.raycastPressIndicatorRenderer.color = color2;
	}

	public void setControllerIndicatorState(int controllerId, bool isShown)
	{
		VRRig.Controller controllerForId = getControllerForId(controllerId);
		controllerForId.raycastPressIndicatorRenderer.enabled = isShown;
		controllerForId.raycastDestinationIndicatorRenderer.enabled = isShown;
	}

	public void fadeInAndOut(float fadeDuration, Action onFadedIn = null, Func<bool> shouldStartFadeOut = null, Action onFadedOut = null)
	{
		fadeInAndOut(fadeDuration, fadeDuration, onFadedIn, shouldStartFadeOut, onFadedOut);
	}

	public void fadeInAndOut(float fadeInDuration, float fadeOutDuration, Action onFadedIn = null, Func<bool> shouldStartFadeOut = null, Action onFadedOut = null)
	{
		if (fadeInDuration < 0f)
		{
			Debug.LogError($"VR canvas fade-in duration must be a non-negative value (was: {fadeInDuration}).");
			return;
		}
		if (fadeOutDuration < 0f)
		{
			Debug.LogError($"VR canvas fade-out duration must be a non-negative value (was: {fadeOutDuration}).");
			return;
		}
		fadeRequests.Enqueue(new FadeRequest
		{
			fadeInDuration = fadeInDuration,
			fadeOutDuration = fadeOutDuration,
			onFadedIn = onFadedIn,
			shouldStartFadeOut = shouldStartFadeOut,
			onFadedOut = onFadedOut
		});
	}

	public void startPopupAnimationEnter(GameObject gameObjectToTrack)
	{
		popupGameObjectToTrack = gameObjectToTrack;
	}

	private IEnumerator performFadeRequest(FadeRequest fadeRequest)
	{
		if (fadeRequest.fadeInDuration > 0f)
		{
			float fadeInStep = 1f / fadeRequest.fadeInDuration;
			while (fadeSourceToFadeAlpha[FadeSource.FadeInAndOut] < 1f)
			{
				fadeSourceToFadeAlpha[FadeSource.FadeInAndOut] = Mathf.MoveTowards(fadeSourceToFadeAlpha[FadeSource.FadeInAndOut], 1f, Time.deltaTime * fadeInStep);
				yield return null;
			}
		}
		else
		{
			fadeSourceToFadeAlpha[FadeSource.FadeInAndOut] = 1f;
		}
		fadeRequest.onFadedIn?.Invoke();
		while (fadeRequest.shouldStartFadeOut != null && !fadeRequest.shouldStartFadeOut())
		{
			yield return null;
		}
		if (fadeRequest.fadeOutDuration > 0f)
		{
			float fadeInStep = 1f / fadeRequest.fadeOutDuration;
			while (fadeSourceToFadeAlpha[FadeSource.FadeInAndOut] > 0f)
			{
				fadeSourceToFadeAlpha[FadeSource.FadeInAndOut] = Mathf.MoveTowards(fadeSourceToFadeAlpha[FadeSource.FadeInAndOut], 0f, Time.deltaTime * fadeInStep);
				yield return null;
			}
		}
		else
		{
			fadeSourceToFadeAlpha[FadeSource.FadeInAndOut] = 0f;
		}
		fadeRequest.onFadedOut?.Invoke();
		fadeCanvasCoroutine = null;
	}

	public static void debugLog(string message, UnityEngine.Object context = null)
	{
		DateTime now = DateTime.Now;
		string name = SceneManager.GetActiveScene().name;
		Debug.Log($"[{now}] [VR] [{name}] {message}", context);
	}

	public void handleApplicationFocusChange(bool hasFocus)
	{
		if (isActive() && Is.Android && !Is.Editor)
		{
			rig.leftController.transform.gameObject.SetActive(hasFocus);
			rig.rightController.transform.gameObject.SetActive(hasFocus);
			Debug.Log($"Application focus changed. Has focus: {hasFocus}");
		}
	}

	public void destroy()
	{
		if (getActiveState() == State.Simulator)
		{
			InputSystem.RemoveDevice(leftSimulatorController);
			InputSystem.RemoveDevice(rightSimulatorController);
			setInputSettings(State.VR);
		}
		else if (getActiveState() == State.VR)
		{
			XRGeneralSettings.Instance.Manager.StopSubsystems();
			XRGeneralSettings.Instance.Manager.DeinitializeLoader();
		}
	}

	private void setInputSettings(State targetState)
	{
		switch (targetState)
		{
		case State.VR:
			InputSystem.settings.backgroundBehavior = InputSettings.BackgroundBehavior.ResetAndDisableNonBackgroundDevices;
			InputSystem.settings.editorInputBehaviorInPlayMode = InputSettings.EditorInputBehaviorInPlayMode.PointersAndKeyboardsRespectGameViewFocus;
			break;
		case State.Simulator:
			InputSystem.settings.backgroundBehavior = InputSettings.BackgroundBehavior.IgnoreFocus;
			InputSystem.settings.editorInputBehaviorInPlayMode = InputSettings.EditorInputBehaviorInPlayMode.AllDevicesRespectGameViewFocus;
			break;
		}
	}
}
