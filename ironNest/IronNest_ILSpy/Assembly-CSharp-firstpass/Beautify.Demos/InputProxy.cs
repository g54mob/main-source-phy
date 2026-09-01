using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Beautify.Demos;

public static class InputProxy
{
	public static bool GetKeyDown(KeyCode keyCode)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 32 Invalid \"Jump target not found in method: 0x1803D515A\"");
		if (keyCode > KeyCode.F)
		{
			switch (keyCode)
			{
			case KeyCode.J:
			{
				KeyControl jKey = Keyboard._003Ccurrent_003Ek__BackingField.jKey;
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 72 Invalid \"Jump target not found in method: 0x1803D5162\"");
				return jKey.wasPressedThisFrame;
			}
			case KeyCode.N:
			{
				KeyControl nKey = Keyboard._003Ccurrent_003Ek__BackingField.nKey;
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 105 Invalid \"Jump target not found in method: 0x1803D5162\"");
				return nKey.wasPressedThisFrame;
			}
			default:
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 124 Invalid \"Jump target not found in method: 0x1803D515A\"");
				KeyControl tKey = Keyboard._003Ccurrent_003Ek__BackingField.tKey;
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 138 Invalid \"Jump target not found in method: 0x1803D5162\"");
				return tKey.wasPressedThisFrame;
			}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 160 Invalid \"Jump target not found in method: 0x1803D50E5\"");
		return (byte)(keyCode - 48) != 0;
	}

	public static bool GetMouseButtonDown(int button)
	{
		//IL_0030: Expected O, but got I4
		//IL_00ba: Expected I4, but got O
		Mouse mouse = Mouse._003Ccurrent_003Ek__BackingField;
		if (Mouse._003Ccurrent_003Ek__BackingField != null)
		{
			bool flag = button == 0;
			ButtonControl buttonControl;
			if (!flag)
			{
				object obj = button - 1;
				if (!flag)
				{
					if ((nint)obj != 1)
					{
						goto IL_00a6;
					}
					buttonControl = mouse._003CmiddleButton_003Ek__BackingField;
				}
				else
				{
					buttonControl = mouse._003CrightButton_003Ek__BackingField;
				}
			}
			else
			{
				buttonControl = mouse._003CleftButton_003Ek__BackingField;
			}
			if (buttonControl != null)
			{
				return buttonControl.wasPressedThisFrame;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		goto IL_00a6;
		IL_00a6:
		return false;
	}
}
