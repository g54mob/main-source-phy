using System;
using System.Collections.Generic;
using InControl;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.Events;

namespace LevelCreator
{
	public class InputManager : MonoBehaviour
	{
		public enum Modifiers
		{
			None = 0,
			ShiftUp = 1,
			ShiftDown = 2,
			AltUp = 4,
			AltDown = 8,
			CtrlUp = 0x10,
			CtrlDown = 0x20
		}

		private static bool debugMode = false;

		private static bool recieveInputs = true;

		private static List<InputState> inputStates = new List<InputState>();

		private HashSet<PlayerAction> m_pressedPlayerActions = new HashSet<PlayerAction>();

		private HashSet<BindingSource> m_pressedBindings = new HashSet<BindingSource>();

		private HashSet<string> m_pressedPositiveAxes = new HashSet<string>(StringComparer.Ordinal);

		private HashSet<string> m_pressedNegativeAxes = new HashSet<string>(StringComparer.Ordinal);

		private static Dictionary<PlayerAction, DateTime> m_actionPressedDurations = new Dictionary<PlayerAction, DateTime>();

		public static UnityEvent onInputStateChanged = new UnityEvent();

		public static UnityEvent onInputStateRemoved = new UnityEvent();

		public static bool ShiftIsPressed { get; private set; }

		public static bool AltIsPressed { get; private set; }

		public static bool CtrlIsPressed { get; private set; }

		public static float ScrollSensitivity
		{
			get
			{
				if (PlayerActions.Instance.InputType == InputType.Controller)
				{
					return 3f;
				}
				return 18f;
			}
		}

		public void OnGUI()
		{
			if (!recieveInputs)
			{
				return;
			}
			AltIsPressed = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) || PlayerActions.Instance.m_altAlternate.IsPressed;
			CtrlIsPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
			ShiftIsPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || PlayerActions.Instance.m_shiftAlternate.IsPressed;
			InputState inputState = null;
			int num;
			for (num = inputStates.Count - 1; num >= 0; num--)
			{
				num = Mathf.Min(num, inputStates.Count - 1);
				inputState = inputStates[num];
				if (inputState != null)
				{
					foreach (InputKey key in inputState.Keys)
					{
						if (CheckActionDown(key.playerAction))
						{
							key.onKeyDown.Invoke();
						}
						else if (CheckActionUp(key.playerAction))
						{
							key.onKeyUp.Invoke();
						}
					}
					if (DMEditor.Instance.currentInputMode == DMEditor.InputMode.UIOnly || DMEditor.Instance.HasMouseCursor())
					{
						break;
					}
				}
			}
		}

		private bool CheckActionDown(PlayerAction playerAction)
		{
			if (playerAction != null && playerAction.IsPressed && !m_pressedPlayerActions.Contains(playerAction))
			{
				m_pressedPlayerActions.Add(playerAction);
				m_actionPressedDurations[playerAction] = DateTime.Now;
				foreach (BindingSource binding in playerAction.Bindings)
				{
					if (m_pressedBindings.Contains(binding))
					{
						return false;
					}
					m_pressedBindings.Add(binding);
				}
				return true;
			}
			return false;
		}

		private bool CheckActionUp(PlayerAction playerAction)
		{
			if (playerAction != null && !playerAction.IsPressed && m_pressedPlayerActions.Contains(playerAction))
			{
				m_pressedPlayerActions.Remove(playerAction);
				m_actionPressedDurations.Remove(playerAction);
				m_pressedBindings.Clear();
				return true;
			}
			return false;
		}

		public static bool ShouldPollInvokePlayerAction(PlayerAction playerAction)
		{
			return GetPlayerActionPressedDuration(playerAction) > 0.5f;
		}

		public static float GetPlayerActionPressedDuration(PlayerAction playerAction)
		{
			if (!playerAction)
			{
				return 0f;
			}
			if (m_actionPressedDurations.TryGetValue(playerAction, out var value))
			{
				return (float)(DateTime.Now - value).TotalMilliseconds / 1000f;
			}
			return 0f;
		}

		public static void DisableInputPolling()
		{
			recieveInputs = false;
		}

		public static void EnableInputPolling()
		{
			recieveInputs = true;
		}

		private void Awake()
		{
			EnableInputPolling();
		}

		public static void PushState(InputState inputState)
		{
			if (debugMode)
			{
				Debug.Log("Pushed State: " + inputState.Name);
			}
			PeekState()?.OnLoseFocus();
			inputState.OnReceiveFocus();
			inputStates.Add(inputState);
			onInputStateChanged.Invoke();
			if (debugMode)
			{
				PrintStack();
			}
		}

		public static void RemoveState(InputState inputState)
		{
			if (inputStates.Count != 0)
			{
				if (debugMode)
				{
					Debug.Log("Popped State " + PeekState().Name);
				}
				if (inputState == PeekState())
				{
					inputState.OnLoseFocus();
				}
				int num = inputStates.IndexOf(inputState);
				if (num != -1)
				{
					inputState.OnStateRemoved();
					inputStates.RemoveAt(num);
					onInputStateChanged.Invoke();
					onInputStateRemoved.Invoke();
				}
				if (debugMode)
				{
					PrintStack();
				}
			}
		}

		public static InputState PeekState()
		{
			if (inputStates.Count == 0)
			{
				return null;
			}
			return inputStates[inputStates.Count - 1];
		}

		public static void ClearInputStates()
		{
			inputStates.Clear();
		}

		public static void PrintStack()
		{
			InputState[] array = inputStates.ToArray();
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				if (i != 0)
				{
					text += "  ||  ";
				}
				text += array[array.Length - 1 - i].Name;
			}
			Debug.Log("<b>Current Stack: </b>" + text);
		}

		private static Modifiers GetCurrentModifiers()
		{
			return (Modifiers)(0 | ((!ShiftIsPressed) ? 1 : 2) | (AltIsPressed ? 8 : 4) | (CtrlIsPressed ? 32 : 16));
		}
	}
}
