using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class ScreenSizeObserver : MonoBehaviour
{
	public delegate void OnScreenSizeChangedDelegate(Resolution resolution);

	private static ScreenSizeObserver _instance;

	public OnScreenSizeChangedDelegate OnScreenSizeChanged;

	private int _lastScreenWidth;

	private int _lastScreenHeight;

	public static ScreenSizeObserver Instance
	{
		get
		{
			if (!_instance)
			{
				GameObject gameObject = new GameObject();
				if ((object)gameObject != null)
				{
					ScreenSizeObserver instance = gameObject.AddComponent<ScreenSizeObserver>();
					_instance = instance;
					if ((object)_instance != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
						object obj = default(object);
						if (obj != null)
						{
							object obj2 = obj;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v194 @ rdx_v9+168] (should have been resolved before IL gen)");
							string text = default(string);
							_instance.name = text;
							if ((object)_instance != null)
							{
								GameObject target = _instance.gameObject;
								UnityEngine.Object.DontDestroyOnLoad(target);
								goto IL_010c;
							}
						}
					}
				}
				return (ScreenSizeObserver)(object)new NullReferenceException();
			}
			goto IL_010c;
			IL_010c:
			return _instance;
		}
	}

	public void OnEnable()
	{
		int width = Screen.width;
		_lastScreenWidth = width;
		int height = Screen.height;
		_lastScreenHeight = height;
	}

	public void Update()
	{
		int width = Screen.width;
		if (_lastScreenWidth == width)
		{
			int height = Screen.height;
			if (_lastScreenHeight == height)
			{
				return;
			}
		}
		int width2 = Screen.width;
		_lastScreenWidth = width2;
		int height2 = Screen.height;
		_lastScreenHeight = height2;
		int width3 = Screen.width;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805E11B0");
		int height3 = Screen.height;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180623550");
		Resolution currentResolution = Screen.currentResolution;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E583B0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180974FC0");
		OnScreenSizeChangedDelegate onScreenSizeChanged = OnScreenSizeChanged;
		if (OnScreenSizeChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v94.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
	}
}
