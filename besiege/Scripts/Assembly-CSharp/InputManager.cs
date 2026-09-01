using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : SingleInstance<InputManager>
{
	public enum KeyInputType
	{
		GetKey = 0,
		GetKeyUp = 1,
		GetKeyDown = 2
	}

	public enum Scroll
	{
		None = 0,
		Up = 1,
		Down = 2
	}

	public class Camera
	{
		public static bool ForwardKeyHeld()
		{
			if (StatMaster.stopHotkeys || StatMaster.stopWASDcamMovement)
			{
				return false;
			}
			ControlScheme.ControlEntry controlEntry = Scheme.General[2];
			return GetControl(controlEntry.Options[0], KeyInputType.GetKey, true, controlEntry.Options);
		}

		public static bool LeftKeyHeld()
		{
			if (StatMaster.stopHotkeys || StatMaster.stopWASDcamMovement)
			{
				return false;
			}
			ControlScheme.ControlEntry controlEntry = Scheme.General[2];
			return GetControl(controlEntry.Options[1], KeyInputType.GetKey, true, controlEntry.Options);
		}

		public static bool BackwardKeyHeld()
		{
			if (StatMaster.stopHotkeys || StatMaster.stopWASDcamMovement)
			{
				return false;
			}
			ControlScheme.ControlEntry controlEntry = Scheme.General[2];
			return GetControl(controlEntry.Options[2], KeyInputType.GetKey, true, controlEntry.Options);
		}

		public static bool RightKeyHeld()
		{
			if (StatMaster.stopHotkeys || StatMaster.stopWASDcamMovement)
			{
				return false;
			}
			ControlScheme.ControlEntry controlEntry = Scheme.General[2];
			return GetControl(controlEntry.Options[3], KeyInputType.GetKey, true, controlEntry.Options);
		}
	}

	public class AdvancedBuilding
	{
		public static bool MoveToolKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.AdvancedBuilding[1];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool RotateToolKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.AdvancedBuilding[2];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool MirrorToolKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.AdvancedBuilding[3];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool ModifyToolKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.AdvancedBuilding[4];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool PaintToolKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.AdvancedBuilding[5];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool ToggleKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.AdvancedBuilding[0];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool LeftCtrlKey()
		{
			ControlScheme.ControlEntry control = Scheme.AdvancedBuilding[7];
			return GetControl(control, KeyInputType.GetKey);
		}

		public static bool LeftAltKey()
		{
			ControlScheme.ControlEntry control = Scheme.AdvancedBuilding[8];
			return GetControl(control, KeyInputType.GetKey);
		}

		public static bool LeftShiftKey()
		{
			ControlScheme.ControlEntry control = Scheme.AdvancedBuilding[6];
			return GetControl(control, KeyInputType.GetKey);
		}

		public static bool SelectAllKeys()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.AdvancedBuilding[9];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool SelectInverseKeys()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.AdvancedBuilding[10];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool DuplicateKeys()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.AdvancedBuilding[11];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool BreakSurface()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.AdvancedBuilding[12];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool ExportObj()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			return Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.E);
		}
	}

	public class LevelEditor
	{
		public static float scaleDownTime;

		public static float rotateDownTime;

		public static bool MoveToolKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[0];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool RotateToolKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[1];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool ScaleToolKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[2];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool MirrorToolKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[3];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool ModifyToolKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[4];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool BrushModeKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[8];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool ScaleKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[10];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool ScaleKeySustained()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[10];
			return ScaleKey() || GetControl(control, KeyInputType.GetKey);
		}

		public static bool ScaleKeyUp()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[10];
			return GetControl(control, KeyInputType.GetKeyUp);
		}

		public static bool ScaleKeyHeld()
		{
			return scaleDownTime > 0.2f;
		}

		public static bool RotateKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[1];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		protected static bool RotateKeySustained()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[1];
			return RotateKey() || GetControl(control, KeyInputType.GetKey);
		}

		public static bool RotateKeyUp()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[1];
			return GetControl(control, KeyInputType.GetKeyUp);
		}

		public static bool RotateKeyHeld()
		{
			return rotateDownTime > 0.2f;
		}

		public static bool LeftCtrlKey()
		{
			ControlScheme.ControlEntry control = Scheme.LevelEditor[6];
			return GetControl(control, KeyInputType.GetKey);
		}

		public static bool LeftAltKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[7];
			return GetControl(control, KeyInputType.GetKey);
		}

		public static bool LeftShiftKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[5];
			return GetControl(control, KeyInputType.GetKey);
		}

		public static bool FocusLevelSelectionKey()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[11];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool SelectAllKeys()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[12];
			return GetControl(control, KeyInputType.GetKeyDown);
		}

		public static bool DuplicateKeys()
		{
			if (StatMaster.stopHotkeys)
			{
				return false;
			}
			ControlScheme.ControlEntry control = Scheme.LevelEditor[13];
			return GetControl(control, KeyInputType.GetKeyDown);
		}
	}

	public class Joystick
	{
		public class Trigger
		{
			private static bool leftWasHeld;

			private static bool rightWasHeld;

			private static float GetAxis()
			{
				switch (Name)
				{
				case "Controller (Xbox One For Windows)":
				case "Controller (XBOX 360 For Windows)":
					return Input.GetAxisRaw("Joy0Axis2") + Input.GetAxisRaw("Joy1Axis2") + Input.GetAxisRaw("Joy2Axis2");
				default:
					return 0f;
				}
			}

			public static bool Left()
			{
				return LeftHeld() && !leftWasHeld;
			}

			public static bool Right()
			{
				return RightHeld() && !rightWasHeld;
			}

			public static bool LeftHeld()
			{
				return GetAxis() > 0.01f;
			}

			public static bool RightHeld()
			{
				return GetAxis() < -0.01f;
			}

			public static bool LeftReleased()
			{
				return !LeftHeld() && leftWasHeld;
			}

			public static bool RightReleased()
			{
				return !RightHeld() && rightWasHeld;
			}

			public static void Update()
			{
				leftWasHeld = LeftHeld();
				rightWasHeld = RightHeld();
			}

			public static bool Pressed(KeyCode key)
			{
				switch (key)
				{
				case KeyCode.Joystick8Button19:
					return Left();
				case KeyCode.Joystick8Button18:
					return Right();
				default:
					return false;
				}
			}

			public static bool Held(KeyCode key)
			{
				switch (key)
				{
				case KeyCode.Joystick8Button19:
					return LeftHeld();
				case KeyCode.Joystick8Button18:
					return RightHeld();
				default:
					return false;
				}
			}

			public static bool Released(KeyCode key)
			{
				switch (key)
				{
				case KeyCode.Joystick8Button19:
					return LeftReleased();
				case KeyCode.Joystick8Button18:
					return RightReleased();
				default:
					return false;
				}
			}
		}

		public class Face
		{
			public static KeyCode Y()
			{
				switch (Name)
				{
				case "Controller":
					return KeyCode.JoystickButton0;
				case "Controller (Xbox One For Windows)":
				case "Controller (XBOX 360 For Windows)":
					return KeyCode.JoystickButton3;
				default:
					return KeyCode.Joystick8Button0;
				}
			}

			public static KeyCode B()
			{
				switch (Name)
				{
				case "Controller":
					return KeyCode.JoystickButton1;
				case "Controller (Xbox One For Windows)":
				case "Controller (XBOX 360 For Windows)":
					return KeyCode.JoystickButton1;
				default:
					return KeyCode.Joystick8Button1;
				}
			}

			public static KeyCode A()
			{
				switch (Name)
				{
				case "Controller":
					return KeyCode.JoystickButton2;
				case "Controller (Xbox One For Windows)":
				case "Controller (XBOX 360 For Windows)":
					return KeyCode.JoystickButton0;
				default:
					return KeyCode.Joystick8Button2;
				}
			}

			public static KeyCode X()
			{
				switch (Name)
				{
				case "Controller":
					return KeyCode.JoystickButton3;
				case "Controller (Xbox One For Windows)":
				case "Controller (XBOX 360 For Windows)":
					return KeyCode.JoystickButton2;
				default:
					return KeyCode.Joystick8Button3;
				}
			}
		}

		public class Dpad
		{
			private static bool leftWasHeld;

			private static bool rightWasHeld;

			private static bool upWasHeld;

			private static bool downWasHeld;

			private static float GetXAxis()
			{
				switch (Name)
				{
				case "Controller":
					return Input.GetAxisRaw("Joy0Axis0") + Input.GetAxisRaw("Joy1Axis0") + Input.GetAxisRaw("Joy2Axis0");
				case "Controller (Xbox One For Windows)":
				case "Controller (XBOX 360 For Windows)":
					return Input.GetAxisRaw("Joy0Axis5") + Input.GetAxisRaw("Joy1Axis5") + Input.GetAxisRaw("Joy2Axis5");
				default:
					return 0f;
				}
			}

			private static float GetYAxis()
			{
				switch (Name)
				{
				case "Controller":
					return Input.GetAxisRaw("Joy0Axis1") + Input.GetAxisRaw("Joy1Axis1") + Input.GetAxisRaw("Joy2Axis1");
				case "Controller (Xbox One For Windows)":
				case "Controller (XBOX 360 For Windows)":
					return 0f - (Input.GetAxisRaw("Joy0Axis6") + Input.GetAxisRaw("Joy1Axis6") + Input.GetAxisRaw("Joy2Axis6"));
				default:
					return 0f;
				}
			}

			public static void Update()
			{
				leftWasHeld = LeftHeld();
				rightWasHeld = RightHeld();
				upWasHeld = UpHeld();
				downWasHeld = DownHeld();
			}

			public static bool Left()
			{
				return LeftHeld() && !leftWasHeld;
			}

			public static bool Right()
			{
				return RightHeld() && !rightWasHeld;
			}

			public static bool Up()
			{
				return UpHeld() && !upWasHeld;
			}

			public static bool Down()
			{
				return DownHeld() && !downWasHeld;
			}

			public static bool LeftHeld()
			{
				return GetXAxis() < -0.01f;
			}

			public static bool RightHeld()
			{
				return GetXAxis() > 0.01f;
			}

			public static bool UpHeld()
			{
				return GetYAxis() < -0.01f;
			}

			public static bool DownHeld()
			{
				return GetYAxis() > 0.01f;
			}

			public static bool LeftReleased()
			{
				return !LeftHeld() && leftWasHeld;
			}

			public static bool RightReleased()
			{
				return !RightHeld() && rightWasHeld;
			}

			public static bool UpReleased()
			{
				return !UpHeld() && upWasHeld;
			}

			public static bool DownReleased()
			{
				return !DownHeld() && downWasHeld;
			}

			public static bool Pressed(KeyCode key)
			{
				switch (key)
				{
				case KeyCode.Joystick8Button9:
					return Left();
				case KeyCode.Joystick8Button8:
					return Right();
				case KeyCode.Joystick8Button7:
					return Up();
				case KeyCode.Joystick8Button6:
					return Down();
				default:
					return false;
				}
			}

			public static bool Held(KeyCode key)
			{
				switch (key)
				{
				case KeyCode.Joystick8Button9:
					return LeftHeld();
				case KeyCode.Joystick8Button8:
					return RightHeld();
				case KeyCode.Joystick8Button7:
					return UpHeld();
				case KeyCode.Joystick8Button6:
					return DownHeld();
				default:
					return false;
				}
			}

			public static bool Released(KeyCode key)
			{
				switch (key)
				{
				case KeyCode.Joystick8Button9:
					return LeftReleased();
				case KeyCode.Joystick8Button8:
					return RightReleased();
				case KeyCode.Joystick8Button7:
					return UpReleased();
				case KeyCode.Joystick8Button6:
					return DownReleased();
				default:
					return false;
				}
			}
		}

		public static string Name;
	}

	public const KeyCode ScrollUp = KeyCode.DoubleQuote;

	public const KeyCode ScrollDown = KeyCode.Caret;

	protected static List<CallBack> SequenceOfCloseFunctions = new List<CallBack>();

	public static float scrollActivateThreshold = 0.07f;

	private static ControlScheme.ControlOption[] ignoredKeys = new ControlScheme.ControlOption[0];

	private static int scrollUpFrame = 0;

	private static int scrollDownFrame = 0;

	private static float deleteDownTime = 0f;

	public static GameObject KeyTarget = null;

	private static HashSet<KeyCode> keys = new HashSet<KeyCode>();

	public override string Name
	{
		get
		{
			return "InputManager";
		}
	}

	public static int ToCloseCount
	{
		get
		{
			return SequenceOfCloseFunctions.Count;
		}
	}

	public static ControlScheme Scheme
	{
		get
		{
			return OptionsMaster.CustomControls;
		}
	}

	public static event KeyboardKeyPressed KeyboardKeyPressed;

	public static event KeyboardKeyReleased KeyboardKeysReleased;

	public static void AddAsNextToClose(CallBack func)
	{
		if (!SequenceOfCloseFunctions.Contains(func))
		{
			SequenceOfCloseFunctions.Add(func);
		}
	}

	public static void RemoveAsNextToClose(CallBack func)
	{
		if (SequenceOfCloseFunctions.Contains(func))
		{
			SequenceOfCloseFunctions.Remove(func);
		}
	}

	private void OnSceneLoad(Scene scene, LoadSceneMode m)
	{
		SequenceOfCloseFunctions.Clear();
	}

	public void Awake()
	{
		string[] joystickNames = Input.GetJoystickNames();
		if (joystickNames.Length == 0)
		{
			Joystick.Name = "N/A";
		}
		else
		{
			Joystick.Name = joystickNames[0];
		}
		SceneManager.sceneLoaded += OnSceneLoad;
	}

	public void Update()
	{
		float deltaTime = Time.deltaTime;
		deleteDownTime = ((!DeleteKeySustained()) ? 0f : (deleteDownTime + deltaTime));
		LevelEditor.rotateDownTime = ((!RotateKeySustained()) ? 0f : (LevelEditor.rotateDownTime + deltaTime));
		LevelEditor.scaleDownTime = ((!LevelEditor.ScaleKeySustained()) ? 0f : (LevelEditor.scaleDownTime + deltaTime));
		if (CloseKey() && SequenceOfCloseFunctions.Count > 0)
		{
			int index = SequenceOfCloseFunctions.Count - 1;
			CallBack callBack = SequenceOfCloseFunctions[index];
			if (callBack != null)
			{
				callBack();
			}
			RemoveAsNextToClose(callBack);
		}
	}

	public void LateUpdate()
	{
		GetKeys();
		Joystick.Dpad.Update();
		Joystick.Trigger.Update();
	}

	public static KeyCode GetFirstControl(ControlScheme.ControlEntry[] category, int type, int index, KeyCode def)
	{
		if (category == null)
		{
			Debug.LogError("[InputManager.GetFirstControl]: ControlScheme Category is missing");
			return def;
		}
		if (type >= category.Length)
		{
			Debug.LogError("[InputManager.GetFirstControl]: Type (" + type + ") exeeds Category array " + category);
			return def;
		}
		if (index >= category[type].Options.Length)
		{
			Debug.LogError("[InputManager.GetFirstControl]: Index (" + index + ") exeeds Options in " + category[type].Name);
			return def;
		}
		if (category[type].Options[index].Keys.Length == 0)
		{
			Debug.LogError("[InputManager.GetFirstControl]: No keys in Option + " + index + " in " + category[type].Name);
			return def;
		}
		return category[type].Options[index].Keys[0];
	}

	public static bool GetControl(ControlScheme.ControlEntry control, KeyInputType InputMethod, bool checkOthers = true)
	{
		for (int i = 0; i < control.Options.Length; i++)
		{
			if (GetControl(control.Options[i], InputMethod, checkOthers, ignoredKeys))
			{
				return true;
			}
		}
		return false;
	}

	public static bool GetControl(ControlScheme.ControlOption control, KeyInputType InputMethod, bool checkOthers, params ControlScheme.ControlOption[] ignored)
	{
		bool keysDown;
		Scroll hasScroll;
		GetKeysActive(control, InputMethod, out keysDown, out hasScroll, checkOthers, ignored);
		if (keysDown)
		{
			switch (hasScroll)
			{
			case Scroll.Up:
				return ActualScrollValue() > scrollActivateThreshold;
			case Scroll.Down:
				return ActualScrollValue() < 0f - scrollActivateThreshold;
			default:
				return true;
			}
		}
		return false;
	}

	public static void GetKeysActive(ControlScheme.ControlOption control, KeyInputType InputMethod, out bool keysDown, out Scroll hasScroll, bool checkOthers, params ControlScheme.ControlOption[] ignored)
	{
		keysDown = true;
		hasScroll = Scroll.None;
		int num = control.Keys.Length;
		for (int i = 0; i < num; i++)
		{
			KeyCode keyCode = control.Keys[i];
			switch (keyCode)
			{
			case KeyCode.DoubleQuote:
				hasScroll = Scroll.Up;
				continue;
			case KeyCode.Caret:
				hasScroll = Scroll.Down;
				continue;
			}
			if (hasScroll != Scroll.None || i < num - 1)
			{
				if (!Input.GetKey(keyCode))
				{
					keysDown = false;
					return;
				}
				continue;
			}
			switch (InputMethod)
			{
			case KeyInputType.GetKey:
				if (!Input.GetKey(keyCode))
				{
					keysDown = false;
					return;
				}
				break;
			case KeyInputType.GetKeyUp:
				if (!Input.GetKeyUp(keyCode))
				{
					keysDown = false;
					return;
				}
				break;
			case KeyInputType.GetKeyDown:
				if (!Input.GetKeyDown(keyCode))
				{
					keysDown = false;
					return;
				}
				break;
			}
		}
		if (checkOthers)
		{
			keysDown = !OtherKeysHeld(control.Keys, ignored);
		}
	}

	public static bool OtherKeysHeld(KeyCode[] ignored, params ControlScheme.ControlOption[] addIgnored)
	{
		ControlScheme customControls = OptionsMaster.CustomControls;
		return OtherKeysHeld(customControls.General, ignored, addIgnored) || OtherKeysHeld(customControls.Building, ignored, addIgnored) || (StatMaster.advancedBuilding && OtherKeysHeld(customControls.AdvancedBuilding, ignored, addIgnored)) || (StatMaster.Mode.levelEdit && OtherKeysHeld(customControls.LevelEditor, ignored, addIgnored));
	}

	public static bool OtherKeysHeld(ControlScheme.ControlEntry[] entries, KeyCode[] ignored, params ControlScheme.ControlOption[] addIgnored)
	{
		bool result = false;
		for (int i = 0; i < entries.Length; i++)
		{
			if (OtherKeysHeld(entries[i], ignored, addIgnored))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public static bool OtherKeysHeld(ControlScheme.ControlEntry entry, KeyCode[] ignored, params ControlScheme.ControlOption[] addIgnored)
	{
		bool result = false;
		for (int i = 0; i < entry.Options.Length; i++)
		{
			if (OtherKeysHeld(entry.Options[i], ignored, addIgnored))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public static bool OtherKeysHeld(ControlScheme.ControlOption control, KeyCode[] ignored, params ControlScheme.ControlOption[] addIgnored)
	{
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < addIgnored.Length; i++)
		{
			if (control == addIgnored[i])
			{
				return false;
			}
		}
		for (int j = 0; j < control.Keys.Length; j++)
		{
			KeyCode key = control.Keys[j];
			if (!flag2 && Array.Exists(ignored, (KeyCode element) => element == key))
			{
				flag2 = true;
				if (flag)
				{
					break;
				}
			}
			if (!flag && Input.GetKey(key) && !Array.Exists(ignored, (KeyCode element) => element == key))
			{
				flag = true;
				if (flag2)
				{
					break;
				}
			}
		}
		return flag && flag2;
	}

	public static KeyCode GetSimToggleKey()
	{
		ControlScheme.ControlEntry controlEntry = Scheme.General[1];
		return controlEntry.Options[0].Keys[0];
	}

	public static bool IsSimToggle(KeyCode key)
	{
		ControlScheme.ControlEntry controlEntry = Scheme.General[1];
		for (int i = 0; i < controlEntry.Options.Length; i++)
		{
			ControlScheme.ControlOption controlOption = controlEntry.Options[i];
			for (int j = 0; j < controlOption.Keys.Length; j++)
			{
				if (controlOption.Keys[j] == key)
				{
					return true;
				}
			}
		}
		return false;
	}

	public static bool ConsoleKey()
	{
		ControlScheme.ControlEntry control = Scheme.General[15];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool LeftMouseButton()
	{
		ControlScheme.ControlEntry control = Scheme.General[0];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool LeftMouseButtonHeld()
	{
		ControlScheme.ControlEntry control = Scheme.General[0];
		return GetControl(control, KeyInputType.GetKey);
	}

	public static bool LeftMouseButtonReleased()
	{
		ControlScheme.ControlEntry control = Scheme.General[0];
		return GetControl(control, KeyInputType.GetKeyUp);
	}

	public static bool RightMouseButtonHeld()
	{
		return RotateCameraKeyHeld();
	}

	public static bool RotateCameraKey()
	{
		ControlScheme.ControlEntry control = Scheme.General[3];
		return GetControl(control, KeyInputType.GetKeyDown, false);
	}

	public static bool RotateCameraKeyHeld()
	{
		ControlScheme.ControlEntry control = Scheme.General[3];
		return GetControl(control, KeyInputType.GetKey, false);
	}

	public static bool RotateCameraKeyReleased()
	{
		ControlScheme.ControlEntry control = Scheme.General[3];
		return GetControl(control, KeyInputType.GetKeyUp, false);
	}

	public static bool FocusCameraKey()
	{
		ControlScheme.ControlEntry control = Scheme.General[4];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool FocusCameraKeyHeld()
	{
		ControlScheme.ControlEntry control = Scheme.General[4];
		return GetControl(control, KeyInputType.GetKey);
	}

	public static bool FocusCameraKeyReleased()
	{
		ControlScheme.ControlEntry control = Scheme.General[4];
		return GetControl(control, KeyInputType.GetKeyUp);
	}

	public static bool PickBlockKey()
	{
		ControlScheme.ControlEntry control = Scheme.Building[3];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool PanCameraKeyHeld()
	{
		ControlScheme.ControlEntry control = Scheme.General[5];
		return GetControl(control, KeyInputType.GetKey);
	}

	public static bool SnapCameraKeyHeld()
	{
		ControlScheme.ControlEntry control = Scheme.General[7];
		return GetControl(control, KeyInputType.GetKey);
	}

	public static float ZoomValue()
	{
		if (StatMaster.stopHotkeys)
		{
			return 0f;
		}
		ControlScheme.ControlEntry controlEntry = Scheme.General[6];
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < controlEntry.Options.Length; i += 2)
		{
			Scroll hasScroll;
			float controlAxis = GetControlAxis(controlEntry.Options[i], out hasScroll);
			num = ((controlAxis != 0f) ? controlAxis : num);
			if (i + 1 >= controlEntry.Options.Length)
			{
				break;
			}
			controlAxis = GetControlAxis(controlEntry.Options[i + 1], out hasScroll);
			if (hasScroll != Scroll.None)
			{
				controlAxis *= 1.25f;
			}
			num2 = ((controlAxis != 0f) ? controlAxis : num2);
		}
		if (num < 0f)
		{
			num *= -1f;
		}
		if (num2 > 0f)
		{
			num2 *= -1f;
		}
		return num + num2;
	}

	public static float ScrollFieldValue()
	{
		return ZoomValue();
	}

	public static float ActualScrollValue()
	{
		return Input.mouseScrollDelta.y / 10f;
	}

	public static bool Scrolling(KeyCode key)
	{
		switch (key)
		{
		case KeyCode.DoubleQuote:
			if (ActualScrollValue() > scrollActivateThreshold && scrollUpFrame <= Time.frameCount)
			{
				scrollUpFrame = Time.frameCount;
				return true;
			}
			break;
		case KeyCode.Caret:
			if (ActualScrollValue() < 0f - scrollActivateThreshold && scrollDownFrame <= Time.frameCount)
			{
				scrollDownFrame = Time.frameCount;
				return true;
			}
			break;
		}
		return false;
	}

	public static bool ScrollStopped(KeyCode key)
	{
		switch (key)
		{
		case KeyCode.DoubleQuote:
			if (Time.frameCount > scrollUpFrame + 2)
			{
				return true;
			}
			break;
		case KeyCode.Caret:
			if (Time.frameCount > scrollDownFrame + 2)
			{
				return true;
			}
			break;
		}
		return false;
	}

	public static float GetControlAxis(ControlScheme.ControlOption option, out Scroll hasScroll)
	{
		bool keysDown;
		GetKeysActive(option, KeyInputType.GetKeyDown, out keysDown, out hasScroll, false);
		if (keysDown)
		{
			float num = ActualScrollValue();
			if (hasScroll == Scroll.Up)
			{
				return (!(num > 0f)) ? 0f : num;
			}
			if (hasScroll == Scroll.Down)
			{
				return (!(num < 0f)) ? 0f : num;
			}
			return 0.1f;
		}
		return 0f;
	}

	public static float MouseX()
	{
		return Input.GetAxis("Mouse X");
	}

	public static float MouseY()
	{
		return Input.GetAxis("Mouse Y");
	}

	public static Vector2 CursorPosition()
	{
		return Input.mousePosition;
	}

	public static bool CursorMoved(Vector2 orgPos)
	{
		return (CursorPosition() - orgPos).sqrMagnitude > 0f;
	}

	public static bool CloseKey()
	{
		if (StatMaster.stopHotkeys)
		{
			return false;
		}
		ControlScheme.ControlEntry control = Scheme.General[9];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool ToggleSimulationKey()
	{
		if (StatMaster.stopHotkeys)
		{
			return false;
		}
		ControlScheme.ControlEntry control = Scheme.General[1];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool SimulateOneFrame()
	{
		return false;
	}

	public static bool ToggleHUDKey()
	{
		if (StatMaster.stopHotkeys)
		{
			return false;
		}
		ControlScheme.ControlEntry control = Scheme.General[8];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool LeftHotCtrlKey()
	{
		ControlScheme.ControlEntry control = Scheme.AdvancedBuilding[7];
		return GetControl(control, KeyInputType.GetKey);
	}

	public static bool LeftHotShiftKey()
	{
		ControlScheme.ControlEntry control = Scheme.AdvancedBuilding[6];
		return GetControl(control, KeyInputType.GetKey);
	}

	public static bool SelectHotAllKeys()
	{
		return AdvancedBuilding.SelectAllKeys();
	}

	public static bool SearchKeys()
	{
		if (StatMaster.stopHotkeys)
		{
			return false;
		}
		ControlScheme.ControlEntry control = Scheme.Building[8];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool ReverseKey()
	{
		ControlScheme.ControlEntry control = Scheme.Building[1];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool RotateKey()
	{
		ControlScheme.ControlEntry control = Scheme.Building[2];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	protected static bool RotateKeySustained()
	{
		ControlScheme.ControlEntry control = Scheme.Building[2];
		return RotateKey() || GetControl(control, KeyInputType.GetKey);
	}

	public static bool RotateKeyUp()
	{
		ControlScheme.ControlEntry control = Scheme.Building[2];
		return GetControl(control, KeyInputType.GetKeyUp);
	}

	public static bool DeleteKey()
	{
		if (StatMaster.stopHotkeys)
		{
			return false;
		}
		ControlScheme.ControlEntry control = Scheme.Building[0];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool EraseToolSustained()
	{
		return LeftMouseButtonHeld() && StatMaster.Mode.selectedTool == StatMaster.Tool.Erase && !StatMaster.advancedBuilding;
	}

	protected static bool DeleteKeySustained()
	{
		ControlScheme.ControlEntry control = Scheme.Building[0];
		return DeleteKey() || GetControl(control, KeyInputType.GetKey) || EraseToolSustained();
	}

	public static bool DeleteKeyHeld()
	{
		if (StatMaster.stopHotkeys)
		{
			return false;
		}
		return (deleteDownTime > 0.5f) ? true : false;
	}

	public static bool RightShiftKey()
	{
		return Input.GetKey(KeyCode.RightShift);
	}

	public static bool CopyKeys()
	{
		ControlScheme.ControlEntry control = Scheme.Building[4];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool PasteKeys()
	{
		ControlScheme.ControlEntry control = Scheme.Building[5];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool CutKeys()
	{
		return LeftHotCtrlKey() && Input.GetKeyDown(KeyCode.X);
	}

	public static bool SelectAllKeys()
	{
		ControlScheme.ControlEntry control = Scheme.AdvancedBuilding[9];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool UndoKeys()
	{
		ControlScheme.ControlEntry control = Scheme.Building[6];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool RedoKeys()
	{
		ControlScheme.ControlEntry control = Scheme.Building[7];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool TogglePlayerList()
	{
		if (StatMaster.stopHotkeys)
		{
			return false;
		}
		ControlScheme.ControlEntry control = Scheme.General[12];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool ToggleMachineInfoHUD()
	{
		if (StatMaster.stopHotkeys)
		{
			return false;
		}
		ControlScheme.ControlEntry control = Scheme.General[11];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool ToggleChat()
	{
		if (StatMaster.stopHotkeys)
		{
			return false;
		}
		ControlScheme.ControlEntry control = Scheme.General[14];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool ResetCameraButton()
	{
		if (StatMaster.stopHotkeys || StatMaster.inMenu)
		{
			return false;
		}
		ControlScheme.ControlEntry control = Scheme.General[10];
		return GetControl(control, KeyInputType.GetKeyDown);
	}

	public static bool AnyKey()
	{
		return Input.anyKey;
	}

	public static bool AnyKeyDown()
	{
		return Input.anyKeyDown;
	}

	public static void GetKeys()
	{
		if (InputManager.KeyboardKeyPressed == null || InputManager.KeyboardKeysReleased == null)
		{
			return;
		}
		if (AnyKeyDown())
		{
			KeyCode keyCode = Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().FirstOrDefault((KeyCode key) => Input.GetKeyDown(key));
			if (keyCode != KeyCode.None)
			{
				AddKey(keyCode);
			}
		}
		bool flag = false;
		if (Input.GetMouseButtonDown(0))
		{
			AddKey(KeyCode.Mouse0);
		}
		else if (Input.GetMouseButtonDown(1))
		{
			AddKey(KeyCode.Mouse1);
		}
		else if (Input.GetMouseButtonDown(2))
		{
			AddKey(KeyCode.Mouse2);
		}
		else if (ActualScrollValue() > 0f)
		{
			flag = true;
			AddKey(KeyCode.DoubleQuote);
		}
		else if (ActualScrollValue() < 0f)
		{
			flag = true;
			AddKey(KeyCode.Caret);
		}
		else if (Joystick.Dpad.Left())
		{
			flag = true;
			AddKey(KeyCode.Joystick8Button9);
		}
		else if (Joystick.Dpad.Right())
		{
			flag = true;
			AddKey(KeyCode.Joystick8Button8);
		}
		else if (Joystick.Dpad.Up())
		{
			flag = true;
			AddKey(KeyCode.Joystick8Button7);
		}
		else if (Joystick.Dpad.Down())
		{
			flag = true;
			AddKey(KeyCode.Joystick8Button6);
		}
		else if (Joystick.Trigger.Left())
		{
			flag = true;
			AddKey(KeyCode.Joystick8Button19);
		}
		else if (Joystick.Trigger.Right())
		{
			flag = true;
			AddKey(KeyCode.Joystick8Button18);
		}
		foreach (KeyCode key in keys)
		{
			if (flag || Input.GetKeyUp(key))
			{
				OnKeyboardKeyReleased();
				keys.Clear();
				break;
			}
		}
	}

	private static void AddKey(KeyCode key)
	{
		if (!keys.Contains(key))
		{
			keys.Add(key);
		}
		OnKeyboardKeyPressed(key);
	}

	protected static void OnKeyboardKeyPressed(KeyCode keycode)
	{
		if (InputManager.KeyboardKeyPressed != null)
		{
			InputManager.KeyboardKeyPressed(keycode);
		}
	}

	protected static void OnKeyboardKeyReleased()
	{
		if (InputManager.KeyboardKeysReleased != null)
		{
			InputManager.KeyboardKeysReleased(keys);
		}
	}

	public static bool GetKey(KeyCode key)
	{
		return Scrolling(key) || Joystick.Dpad.Held(key) || Joystick.Trigger.Held(key) || Input.GetKey(key);
	}

	public static bool GetKeyDown(KeyCode key)
	{
		return Scrolling(key) || Joystick.Dpad.Pressed(key) || Joystick.Trigger.Pressed(key) || Input.GetKeyDown(key);
	}

	public static bool GetKeyUp(KeyCode key)
	{
		return ScrollStopped(key) || Joystick.Dpad.Released(key) || Joystick.Trigger.Released(key) || Input.GetKeyUp(key);
	}
}
