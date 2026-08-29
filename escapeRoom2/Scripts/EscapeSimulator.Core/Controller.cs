using System;
using System.Collections.Generic;
using System.IO;
using Steamworks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class Controller
{
	private static bool inited;

	public static bool isControllerMode;

	public static bool controllerModeChanged;

	public static bool controllerInputSetChanged;

	private static InputActionSetHandle_t modeUI;

	private static InputActionSetHandle_t modeGame;

	private static InputHandle_t[] inputHandlesShared = new InputHandle_t[16];

	private static int inputHandlesSharedCount = 0;

	public static InputState touchStateController;

	public static Vector2 touchPosition;

	private static InputHandle_t emptyControllerHandle = new InputHandle_t(0uL);

	private static InputHandle_t currentController = new InputHandle_t(0uL);

	private static Dictionary<ControllerButtonActionType, ControllerButtonState> buttonStates = new Dictionary<ControllerButtonActionType, ControllerButtonState>();

	private static Dictionary<ControllerAxisActionType, ControllerAxisState> axisStates = new Dictionary<ControllerAxisActionType, ControllerAxisState>();

	private static Dictionary<ControllerButtonActionType, string> buttonNameCache = new Dictionary<ControllerButtonActionType, string>();

	private static Dictionary<ControllerAxisActionType, string> axisNameCache = new Dictionary<ControllerAxisActionType, string>();

	private static Dictionary<string, Sprite> glyphCache = new Dictionary<string, Sprite>();

	private static ControllerButtonActionType[] allButtons;

	private static ControllerAxisActionType[] allAxis;

	private static bool controllerIsPrimaryMode = false;

	private static GameObject lastSelectedUI = null;

	private static List<string> glyphPathCandidates = new List<string>(4);

	private static EInputActionOrigin[] originsOutShared = new EInputActionOrigin[8];

	public static Transform currentSelectableGroupParent;

	private static List<Selectable> currentSelectableGroup;

	private static Selectable lastSelectedSelectable;

	public static void init()
	{
		if (inited)
		{
			return;
		}
		controllerIsPrimaryMode = SteamUtils.IsSteamRunningOnSteamDeck() || SteamUtils.IsSteamInBigPictureMode();
		isControllerMode = controllerIsPrimaryMode;
		allButtons = (ControllerButtonActionType[])Enum.GetValues(typeof(ControllerButtonActionType));
		allAxis = (ControllerAxisActionType[])Enum.GetValues(typeof(ControllerAxisActionType));
		if (SteamManager.Initialized)
		{
			Debug.Log("SteamInput Init Result: " + SteamInput.Init(bExplicitlyCallRunFrame: true));
			SteamInput.RunFrame();
			tryToObtainModes();
			inputHandlesSharedCount = SteamInput.GetConnectedControllers(inputHandlesShared);
			if (inputHandlesSharedCount > 0 && inputHandlesShared[0] != currentController && isControllerMode)
			{
				currentController = inputHandlesShared[0];
				Debug.Log("Setting startup new controller [1/" + inputHandlesSharedCount + "]: " + SteamInput.GetInputTypeForHandle(currentController));
			}
		}
		inited = true;
	}

	private static void tryToObtainModes()
	{
		if (modeGame.m_InputActionSetHandle == 0L)
		{
			modeGame = SteamInput.GetActionSetHandle("Game");
			modeUI = SteamInput.GetActionSetHandle("UI");
			for (int i = 0; i < allButtons.Length; i++)
			{
				ControllerButtonActionType key = allButtons[i];
				buttonNameCache[key] = key.ToString();
				buttonStates[key] = new ControllerButtonState
				{
					state = InputState.None,
					steamHandle = SteamInput.GetDigitalActionHandle(key.ToString())
				};
			}
			for (int j = 0; j < allAxis.Length; j++)
			{
				ControllerAxisActionType key2 = allAxis[j];
				axisNameCache[key2] = key2.ToString();
				axisStates[key2] = new ControllerAxisState
				{
					value = Vector2.zero,
					steamHandle = SteamInput.GetAnalogActionHandle(key2.ToString())
				};
			}
		}
	}

	private static void initGenericInputData()
	{
		for (int i = 0; i < allButtons.Length; i++)
		{
			ControllerButtonActionType key = (ControllerButtonActionType)allButtons.GetValue(i);
			buttonStates[key] = new ControllerButtonState
			{
				state = InputState.None
			};
			buttonNameCache[key] = key.ToString();
		}
		for (int j = 0; j < allAxis.Length; j++)
		{
			ControllerAxisActionType key2 = allAxis[j];
			axisStates[key2] = new ControllerAxisState
			{
				value = Vector2.zero
			};
			axisNameCache[key2] = key2.ToString();
		}
	}

	public static void destroy()
	{
		SteamInput.Shutdown();
	}

	public static bool isActive()
	{
		bool flag = currentController != emptyControllerHandle;
		return isControllerMode && flag;
	}

	public static void updateController(MenuOptions options, EscapeSimulatorSteamInputActionSets set)
	{
		controllerModeChanged = false;
		if (isActive() && isControllerMode)
		{
			bool flag = false;
			for (int i = 1; i < 30; i++)
			{
				if (options.getActionKey((KeyBindingAction)i))
				{
					flag = true;
					break;
				}
			}
			bool flag2 = Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) || Mathf.Abs(Input.GetAxis("Mouse X")) > 0.1f || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.1f;
			if (SteamUtils.IsSteamRunningOnSteamDeck())
			{
				flag2 = false;
			}
			if (flag || flag2)
			{
				controllerModeChanged = true;
				isControllerMode = false;
				Debug.Log("Activating Keyboard/Mouse, casued by input from: " + (flag2 ? "Mouse" : "Keyboard"));
				currentController = emptyControllerHandle;
			}
		}
		if (isActive() && isControllerMode && EventSystem.current != null)
		{
			if (EventSystem.current.currentSelectedGameObject != null)
			{
				lastSelectedUI = EventSystem.current.currentSelectedGameObject;
			}
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
			{
				if (lastSelectedUI == null)
				{
					lastSelectedUI = EventSystem.current.firstSelectedGameObject;
				}
				EventSystem.current.SetSelectedGameObject(lastSelectedUI);
			}
		}
		if (SteamManager.Initialized)
		{
			SteamInput.RunFrame();
			inputHandlesSharedCount = SteamInput.GetConnectedControllers(inputHandlesShared);
			if (inputHandlesSharedCount > 0)
			{
				tryToObtainModes();
			}
			for (int j = 0; j < inputHandlesSharedCount; j++)
			{
				InputHandle_t inputHandle_t = inputHandlesShared[j];
				if (inputHandle_t == currentController)
				{
					continue;
				}
				ControllerButtonActionType[] array = allButtons;
				for (int k = 0; k < array.Length; k++)
				{
					ControllerButtonActionType key = array[k];
					InputDigitalActionData_t digitalActionData = SteamInput.GetDigitalActionData(inputHandle_t, buttonStates[key].steamHandle);
					if (digitalActionData.bActive != 0 && digitalActionData.bState != 0)
					{
						currentController = inputHandle_t;
						Debug.Log("Activating Controller [" + (j + 1) + "/" + inputHandlesSharedCount + "]: " + SteamInput.GetInputTypeForHandle(currentController).ToString() + " with: " + key);
						buttonStates[key].state = InputState.None;
						buttonStates[key].used = true;
						controllerModeChanged = true;
						isControllerMode = true;
						break;
					}
				}
			}
			bool flag3 = false;
			for (int l = 0; l < inputHandlesSharedCount; l++)
			{
				flag3 = flag3 || inputHandlesShared[l] == currentController;
			}
			if (isControllerMode && !flag3)
			{
				if (controllerIsPrimaryMode && inputHandlesSharedCount > 0)
				{
					currentController = inputHandlesShared[0];
					controllerModeChanged = true;
					isControllerMode = true;
					Debug.Log("Activating Controller [1/" + inputHandlesSharedCount + "] due to other being disconnected.");
				}
				else
				{
					currentController = emptyControllerHandle;
					controllerModeChanged = true;
					isControllerMode = false;
					Debug.Log("Activating Keyboard/Mouse");
				}
			}
		}
		updateSteamInput(set);
		validateSelectingGroup();
	}

	private static void updateSwitchInput()
	{
	}

	public static void selectSelectable(Selectable selectable, EventSystem eventSystem = null)
	{
		if (eventSystem == null)
		{
			eventSystem = EventSystem.current;
		}
		GameObject currentSelectedGameObject = eventSystem.currentSelectedGameObject;
		if (currentSelectedGameObject != null && currentSelectedGameObject.TryGetComponent<Selectable>(out var component))
		{
			component.OnDeselect(null);
		}
		if (selectable != null && isActive())
		{
			eventSystem.SetSelectedGameObject(selectable.gameObject);
			selectable.Select();
			selectable.OnSelect(null);
		}
		else
		{
			eventSystem.SetSelectedGameObject(null);
		}
	}

	private static void updateSteamInput(EscapeSimulatorSteamInputActionSets setToActivate)
	{
		if (currentController == emptyControllerHandle)
		{
			return;
		}
		for (int i = 0; i < inputHandlesSharedCount; i++)
		{
			InputHandle_t inputHandle = inputHandlesShared[i];
			InputActionSetHandle_t currentActionSet = SteamInput.GetCurrentActionSet(inputHandle);
			SteamInput.ActivateActionSet(inputHandle, setToActivate switch
			{
				EscapeSimulatorSteamInputActionSets.Game => modeGame, 
				EscapeSimulatorSteamInputActionSets.UI => modeUI, 
				_ => modeGame, 
			});
			controllerInputSetChanged = SteamInput.GetCurrentActionSet(inputHandle) != currentActionSet;
		}
		foreach (KeyValuePair<ControllerButtonActionType, ControllerButtonState> buttonState in buttonStates)
		{
			handleButton(buttonState.Value);
		}
		foreach (KeyValuePair<ControllerAxisActionType, ControllerAxisState> axisState in axisStates)
		{
			handleAxis(axisState.Value);
		}
		static void handleAxis(ControllerAxisState state)
		{
			InputAnalogActionData_t analogActionData = SteamInput.GetAnalogActionData(currentController, state.steamHandle);
			if (analogActionData.bActive == 1)
			{
				state.value.x = analogActionData.x;
				state.value.y = analogActionData.y;
			}
			else
			{
				state.value = Vector2.zero;
			}
		}
		static void handleButton(ControllerButtonState state)
		{
			InputDigitalActionData_t digitalActionData = SteamInput.GetDigitalActionData(currentController, state.steamHandle);
			if (digitalActionData.bActive == 0)
			{
				state.state = InputState.None;
			}
			else if (digitalActionData.bState == 1)
			{
				if (state.state == InputState.None)
				{
					state.state = InputState.Down;
				}
				else
				{
					state.state = InputState.Hold;
				}
			}
			else
			{
				InputState state2 = state.state;
				if (state2 == InputState.Hold || state2 == InputState.Down)
				{
					state.state = InputState.Up;
				}
				else
				{
					if (state.state == InputState.Up)
					{
						state.used = false;
					}
					state.state = InputState.None;
				}
			}
		}
	}

	public static bool getUIConfirmDown()
	{
		return getButtonDown(ControllerButtonActionType.UIConfirmPrimary, ControllerButtonActionType.UIConfirmSecondary);
	}

	public static bool getUIConfirmUp()
	{
		return getButtonUp(ControllerButtonActionType.UIConfirmPrimary, ControllerButtonActionType.UIConfirmSecondary);
	}

	public static bool getButtonDown(params ControllerButtonActionType[] types)
	{
		if (Is.Android)
		{
			return false;
		}
		bool result = false;
		foreach (ControllerButtonActionType key in types)
		{
			ControllerButtonState controllerButtonState = buttonStates[key];
			if (controllerButtonState.state == InputState.Down && !controllerButtonState.used)
			{
				result = true;
			}
		}
		return result;
	}

	public static bool getButton(params ControllerButtonActionType[] types)
	{
		if (Is.Android)
		{
			return false;
		}
		bool result = false;
		foreach (ControllerButtonActionType key in types)
		{
			ControllerButtonState controllerButtonState = buttonStates[key];
			if (controllerButtonState.state != InputState.None && !controllerButtonState.used)
			{
				result = true;
			}
		}
		return result;
	}

	public static bool getButtonHold(params ControllerButtonActionType[] types)
	{
		if (Is.Android)
		{
			return false;
		}
		bool result = false;
		foreach (ControllerButtonActionType key in types)
		{
			ControllerButtonState controllerButtonState = buttonStates[key];
			if (controllerButtonState.state == InputState.Hold && !controllerButtonState.used)
			{
				result = true;
			}
		}
		return result;
	}

	public static bool getButtonUp(params ControllerButtonActionType[] types)
	{
		if (Is.Android)
		{
			return false;
		}
		bool result = false;
		foreach (ControllerButtonActionType key in types)
		{
			ControllerButtonState controllerButtonState = buttonStates[key];
			if (controllerButtonState.state == InputState.Up && !controllerButtonState.used)
			{
				result = true;
			}
		}
		return result;
	}

	public static Vector2 getAxis(ControllerAxisActionType type)
	{
		return axisStates[type].value;
	}

	public static float getUIAxisForNavigation()
	{
		return axisStates[ControllerAxisActionType.UIMoveAndRotate].value.y + axisStates[ControllerAxisActionType.UIMoveAndCursor].value.y;
	}

	public static float getUIAxisForChangeValue()
	{
		return axisStates[ControllerAxisActionType.UIMoveAndRotate].value.x + axisStates[ControllerAxisActionType.UIMoveAndCursor].value.x;
	}

	public static void drawDebug()
	{
		foreach (KeyValuePair<ControllerButtonActionType, ControllerButtonState> buttonState in buttonStates)
		{
			GUILayout.Label(buttonState.Key.ToString() + " " + buttonState.Value.state);
		}
	}

	private static void addPathForSteamOrigin(List<string> pathCandidates, EInputActionOrigin[] origins, int originsCount)
	{
		if (originsCount > 0)
		{
			string fileName = Path.GetFileName(SteamInput.GetGlyphPNGForActionOrigin(origins[0], ESteamInputGlyphSize.k_ESteamInputGlyphSize_Medium, 0u).Replace('\\', '/'));
			pathCandidates.Add(Path.Combine(Application.streamingAssetsPath, "Icons/SteamDark/" + fileName));
		}
	}

	private static void enqueueCommonGlyphPaths(List<string> pathCandidates, string actionString)
	{
		if (isActive())
		{
			glyphPathCandidates.Add(Path.Combine(Application.streamingAssetsPath, "Icons/Controller/" + actionString + ".png"));
		}
		else
		{
			glyphPathCandidates.Add(Path.Combine(Application.streamingAssetsPath, "Icons/PC/" + actionString + ".png"));
		}
	}

	public static string getGlyphString(ControllerButtonActionType type)
	{
		string result = "";
		if (isActive() && SteamInput.GetDigitalActionOrigins(currentController, SteamInput.GetCurrentActionSet(currentController), buttonStates[type].steamHandle, originsOutShared) > 0)
		{
			result = originsOutShared[0].ToString();
		}
		return result;
	}

	public static string getGlyphString(ControllerAxisActionType type)
	{
		string result = "";
		if (isActive() && SteamInput.GetAnalogActionOrigins(currentController, SteamInput.GetCurrentActionSet(currentController), axisStates[type].steamHandle, originsOutShared) > 0)
		{
			result = originsOutShared[0].ToString();
		}
		return result;
	}

	public static Sprite getGlyph(ControllerButtonActionType type)
	{
		glyphPathCandidates.Clear();
		if (isActive())
		{
			processOrigins(modeGame);
			processOrigins(modeUI);
		}
		enqueueCommonGlyphPaths(glyphPathCandidates, buttonNameCache[type]);
		return loadGlyph(glyphPathCandidates);
		void processOrigins(InputActionSetHandle_t actionSet)
		{
			int digitalActionOrigins = SteamInput.GetDigitalActionOrigins(currentController, actionSet, buttonStates[type].steamHandle, originsOutShared);
			if (digitalActionOrigins > 0 && SteamInput.GetInputTypeForHandle(currentController) == ESteamInputType.k_ESteamInputType_SwitchProController)
			{
				if (originsOutShared[0] == EInputActionOrigin.k_EInputActionOrigin_XBox360_X)
				{
					originsOutShared[0] = EInputActionOrigin.k_EInputActionOrigin_XBox360_Y;
				}
				else if (originsOutShared[0] == EInputActionOrigin.k_EInputActionOrigin_XBox360_Y)
				{
					originsOutShared[0] = EInputActionOrigin.k_EInputActionOrigin_XBox360_X;
				}
				else if (originsOutShared[0] == EInputActionOrigin.k_EInputActionOrigin_XBox360_A)
				{
					originsOutShared[0] = EInputActionOrigin.k_EInputActionOrigin_XBox360_B;
				}
				else if (originsOutShared[0] == EInputActionOrigin.k_EInputActionOrigin_XBox360_B)
				{
					originsOutShared[0] = EInputActionOrigin.k_EInputActionOrigin_XBox360_A;
				}
			}
			addPathForSteamOrigin(glyphPathCandidates, originsOutShared, digitalActionOrigins);
		}
	}

	public static Sprite getGlyph(ControllerAxisActionType type)
	{
		glyphPathCandidates.Clear();
		if (isActive())
		{
			int analogActionOrigins = SteamInput.GetAnalogActionOrigins(currentController, SteamInput.GetCurrentActionSet(currentController), axisStates[type].steamHandle, originsOutShared);
			addPathForSteamOrigin(glyphPathCandidates, originsOutShared, analogActionOrigins);
		}
		enqueueCommonGlyphPaths(glyphPathCandidates, axisNameCache[type]);
		return loadGlyph(glyphPathCandidates);
	}

	private static Sprite loadGlyph(List<string> pathCandidates)
	{
		Sprite result = null;
		foreach (string pathCandidate in pathCandidates)
		{
			if (pathCandidate == null || pathCandidate == "")
			{
				Debug.LogWarning("No path for glyph: " + pathCandidate);
				continue;
			}
			if (glyphCache.ContainsKey(pathCandidate))
			{
				result = glyphCache[pathCandidate];
				break;
			}
			if (!File.Exists(pathCandidate))
			{
				continue;
			}
			byte[] data = File.ReadAllBytes(pathCandidate);
			Texture2D texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, mipChain: true);
			texture2D.LoadImage(data);
			Sprite value = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
			texture2D.wrapMode = TextureWrapMode.Clamp;
			texture2D.filterMode = FilterMode.Trilinear;
			glyphCache[pathCandidate] = value;
			result = glyphCache[pathCandidate];
			break;
		}
		return result;
	}

	public static bool canSelect(Selectable selectable)
	{
		if (currentSelectableGroup == null)
		{
			return true;
		}
		return currentSelectableGroup.Contains(selectable);
	}

	public static void setSelectingGroup(Transform parent, Selectable firstSelectable = null)
	{
		if (currentSelectableGroupParent == parent)
		{
			return;
		}
		Debug.Log("set selecting group " + parent, parent);
		if (parent == null)
		{
			currentSelectableGroup = null;
			lastSelectedSelectable = null;
			currentSelectableGroupParent = null;
			return;
		}
		currentSelectableGroupParent = parent;
		currentSelectableGroup = new List<Selectable>(parent.GetComponentsInChildren<Selectable>(includeInactive: true));
		currentSelectableGroup.RemoveAll((Selectable x) => x.navigation.mode == Navigation.Mode.None);
		if (firstSelectable == null)
		{
			firstSelectable = currentSelectableGroup.Find((Selectable x) => x.TryGetComponent<SelectableGroupHint>(out var component) && component.hints.Contains(SelectableGroupHint.Hint.FirstSelectable) && x.gameObject.activeInHierarchy && x.IsInteractable());
		}
		if (firstSelectable == null)
		{
			firstSelectable = currentSelectableGroup.Find((Selectable x) => x.gameObject.activeInHierarchy && x.gameObject.activeInHierarchy && x.IsInteractable());
		}
		selectSelectable(firstSelectable);
		lastSelectedSelectable = firstSelectable;
	}

	public static void validateSelectingGroup()
	{
		if (!isActive() || currentSelectableGroup == null || currentSelectableGroup.Count <= 0)
		{
			return;
		}
		EventSystem current = EventSystem.current;
		if (lastSelectedSelectable != current.currentSelectedGameObject)
		{
			Selectable item = ((current.currentSelectedGameObject != null) ? current.currentSelectedGameObject.GetComponent<Selectable>() : null);
			if (!currentSelectableGroup.Contains(item))
			{
				selectSelectable(lastSelectedSelectable);
			}
			else
			{
				lastSelectedSelectable = item;
			}
		}
	}

	private static T setupNavigation<T>(IList<T> items, Func<T, Selectable> getSelectable) where T : class
	{
		T val = null;
		T val2 = null;
		foreach (T item in items)
		{
			Selectable selectable = getSelectable(item);
			if (selectable != null && selectable.gameObject.activeSelf && selectable.interactable)
			{
				if (val == null)
				{
					val = item;
				}
				Navigation navigation = selectable.navigation;
				navigation.mode = Navigation.Mode.Explicit;
				navigation.selectOnDown = null;
				if (val2 != null)
				{
					Selectable selectable2 = getSelectable(val2);
					Navigation navigation2 = selectable2.navigation;
					navigation.selectOnUp = selectable2;
					navigation2.selectOnDown = selectable;
					selectable2.navigation = navigation2;
				}
				else
				{
					navigation.selectOnUp = null;
				}
				selectable.navigation = navigation;
				val2 = item;
			}
		}
		return val;
	}

	public static OptionsPrefab_Base setupVerticalNavigation(List<SelectableOption> options)
	{
		List<OptionsPrefab_Base> list = new List<OptionsPrefab_Base>();
		foreach (SelectableOption option in options)
		{
			list.Add(option.baseObject);
		}
		return setupNavigation(list, (OptionsPrefab_Base item) => item.selectable);
	}

	public static void setupVerticalNavigationAndSelectFirst(List<SelectableOption> options)
	{
		selectSelectable(setupVerticalNavigation(options).selectable);
	}

	public static Button setupVerticalNavigation(Transform transform, Button ignoreButton = null)
	{
		List<Button> list = new List<Button>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Button component = transform.GetChild(i).GetComponent<Button>();
			if (component != null && component != ignoreButton)
			{
				list.Add(component);
			}
		}
		return setupNavigation(list, (Button button) => button);
	}

	public static void setupVerticalNavigationAndSelectFirst(Transform transform)
	{
		selectSelectable(setupVerticalNavigation(transform));
	}

	public static Button setupVerticalNavigation(List<Button> buttons)
	{
		return setupNavigation(buttons, (Button button) => button);
	}

	public static void setupVerticalNavigationAndSelectFirst(List<Button> buttons)
	{
		selectSelectable(setupVerticalNavigation(buttons));
	}

	public static void useButton(ControllerButtonActionType buttonActionType)
	{
		if (buttonStates[buttonActionType].state != InputState.None)
		{
			buttonStates[buttonActionType].used = true;
		}
	}
}
