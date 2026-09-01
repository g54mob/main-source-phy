using UnityEngine;

public class uConsoleInput
{
	public static bool m_ForceSubmit;

	public static bool m_ForceAutoComplete;

	public static bool m_ForceRecallUp;

	public static bool m_ForceRecallDown;

	public static bool m_ForceScrollLogUp;

	public static bool m_ForceScrollLogDown;

	public static char[] m_DelimterChars = new char[2] { ' ', '\t' };

	private static int m_LastHistoryIndexRecalled;

	private static float m_TimeRecallUpPressed;

	private static float m_TimeRecallDownPressed;

	private static float m_TimeScrollUpPressed;

	private static float m_TimeScrollDownPressed;

	private static float m_CommandRepeatTimeSeconds;

	private static bool m_ControlTipDisplayed;

	private static int m_InputLength;

	public static void Initialize()
	{
		m_LastHistoryIndexRecalled = -1;
	}

	public static void DoFrame()
	{
		if (ModApi.IsConsoleActivationAllowed())
		{
			ProcessActivationInput();
		}
		if (uConsole.IsOn())
		{
			ProcessSubmitInput();
			ProcessAutoCompleteInput();
			ProcessHistoryInput();
			ProcessLogInput();
		}
	}

	private static void SubmitCommand()
	{
		string text = uConsole.m_GUI.InputFieldGetText();
		if (text != null)
		{
			uConsole.RunCommand(text);
			uConsole.m_GUI.InputFieldSetFocus();
			uConsole.m_GUI.InputFieldClearText();
			m_LastHistoryIndexRecalled = -1;
		}
	}

	private static void AutoComplete()
	{
		string text = uConsole.m_GUI.InputFieldGetText();
		if (text == null || text == "")
		{
			return;
		}
		string bestCompletion = uConsoleAutoComplete.GetBestCompletion(text);
		if (bestCompletion != null)
		{
			if (uConsole.CommandIsUnabmiguousAutoComplete(bestCompletion))
			{
				uConsole.m_GUI.InputFieldSetText(bestCompletion + " ");
			}
			else
			{
				uConsole.m_GUI.InputFieldSetText(bestCompletion);
				uConsoleAutoComplete.DisplayPossibleMatches(bestCompletion);
			}
			uConsole.m_GUI.InputFieldMoveCaretToEnd();
		}
	}

	private static void RecallCommandUp()
	{
		if (uConsoleHistory.GetNumLines() != 0)
		{
			if (m_LastHistoryIndexRecalled >= uConsoleHistory.GetNumLines() - 1)
			{
				m_LastHistoryIndexRecalled = -1;
			}
			m_LastHistoryIndexRecalled++;
			string line = uConsoleHistory.GetLine(m_LastHistoryIndexRecalled);
			uConsole.m_GUI.InputFieldSetText(line);
			uConsole.m_GUI.InputFieldMoveCaretToEnd();
		}
	}

	private static void RecallCommandDown()
	{
		if (uConsoleHistory.GetNumLines() != 0)
		{
			if (m_LastHistoryIndexRecalled < 1)
			{
				m_LastHistoryIndexRecalled = uConsoleHistory.GetNumLines();
			}
			m_LastHistoryIndexRecalled--;
			string line = uConsoleHistory.GetLine(m_LastHistoryIndexRecalled);
			uConsole.m_GUI.InputFieldSetText(line);
			uConsole.m_GUI.InputFieldMoveCaretToEnd();
		}
	}

	private static void ProcessActivationInput()
	{
		if (Input.GetKeyDown(uConsole.m_Instance.m_Activate))
		{
			if (!uConsole.IsOn())
			{
				uConsole.TurnOn();
				uConsole.m_GUI.InputFieldMoveCaretToEnd();
				uConsole.m_GUI.InputFieldSetFocus();
				if (!m_ControlTipDisplayed)
				{
					Debug.Log("Press \"" + uConsole.m_Instance.m_Activate.ToString() + "\" again to close console");
					m_ControlTipDisplayed = true;
				}
			}
			else
			{
				uConsole.TurnOff();
				uConsole.m_GUI.InputFieldDeactivate();
			}
			string text = uConsole.m_GUI.InputFieldGetText();
			if (text.Length > m_InputLength)
			{
				uConsole.m_GUI.InputFieldSetText(text.Substring(0, m_InputLength));
			}
		}
		else
		{
			m_InputLength = uConsole.m_GUI.InputFieldGetText().Length;
		}
	}

	private static void ProcessSubmitInput()
	{
		if (Input.GetKeyUp(uConsole.m_Instance.m_Submit) || Input.GetKeyUp(uConsole.m_Instance.m_SubmitAlt) || m_ForceSubmit)
		{
			SubmitCommand();
			m_ForceSubmit = false;
		}
	}

	private static void ProcessAutoCompleteInput()
	{
		if (Input.GetKeyDown(uConsole.m_Instance.m_AutoComplete) || m_ForceAutoComplete)
		{
			AutoComplete();
			m_ForceAutoComplete = false;
		}
	}

	private static void ProcessHistoryInput()
	{
		if (Input.GetKeyDown(uConsole.m_Instance.m_HistoryUp) || m_ForceRecallUp)
		{
			RecallCommandUp();
			m_TimeRecallUpPressed = Time.realtimeSinceStartup;
			m_CommandRepeatTimeSeconds = 0.5f;
			m_ForceRecallUp = false;
		}
		if (Input.GetKeyDown(uConsole.m_Instance.m_HistoryDown) || m_ForceRecallDown)
		{
			RecallCommandDown();
			m_TimeRecallDownPressed = Time.realtimeSinceStartup;
			m_CommandRepeatTimeSeconds = 0.5f;
			m_ForceRecallDown = false;
		}
		if (Input.GetKey(uConsole.m_Instance.m_HistoryUp) && Time.realtimeSinceStartup - m_TimeRecallUpPressed > m_CommandRepeatTimeSeconds)
		{
			RecallCommandUp();
			m_TimeRecallUpPressed = Time.realtimeSinceStartup;
			m_CommandRepeatTimeSeconds = 0.1f;
		}
		if (Input.GetKey(uConsole.m_Instance.m_HistoryDown) && Time.realtimeSinceStartup - m_TimeRecallDownPressed > m_CommandRepeatTimeSeconds)
		{
			RecallCommandDown();
			m_TimeRecallDownPressed = Time.realtimeSinceStartup;
			m_CommandRepeatTimeSeconds = 0.1f;
		}
	}

	private static void ProcessLogInput()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			uConsole.m_GUI.ScrollLogUp();
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
			uConsole.m_GUI.ScrollLogDown();
		}
		if (Input.GetKeyDown(uConsole.m_Instance.m_ScrollLogUp) || m_ForceScrollLogUp)
		{
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				uConsole.m_GUI.ScrollLogUpMax();
			}
			else
			{
				uConsole.m_GUI.ScrollLogUp();
				m_TimeScrollUpPressed = Time.realtimeSinceStartup;
				m_CommandRepeatTimeSeconds = 0.5f;
			}
			m_ForceScrollLogUp = false;
		}
		if (Input.GetKeyDown(uConsole.m_Instance.m_ScrollLogDown) || m_ForceScrollLogDown)
		{
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				uConsole.m_GUI.ScrollLogDownMax();
			}
			else
			{
				uConsole.m_GUI.ScrollLogDown();
				m_TimeScrollDownPressed = Time.realtimeSinceStartup;
				m_CommandRepeatTimeSeconds = 0.5f;
			}
			m_ForceScrollLogDown = false;
		}
		if (Input.GetKey(uConsole.m_Instance.m_ScrollLogUp) && Time.realtimeSinceStartup - m_TimeScrollUpPressed > m_CommandRepeatTimeSeconds)
		{
			uConsole.m_GUI.ScrollLogUp();
			m_TimeScrollUpPressed = Time.realtimeSinceStartup;
			m_CommandRepeatTimeSeconds = 0.1f;
		}
		if (Input.GetKey(uConsole.m_Instance.m_ScrollLogDown) && Time.realtimeSinceStartup - m_TimeScrollDownPressed > m_CommandRepeatTimeSeconds)
		{
			uConsole.m_GUI.ScrollLogDown();
			m_TimeScrollDownPressed = Time.realtimeSinceStartup;
			m_CommandRepeatTimeSeconds = 0.1f;
		}
	}
}
