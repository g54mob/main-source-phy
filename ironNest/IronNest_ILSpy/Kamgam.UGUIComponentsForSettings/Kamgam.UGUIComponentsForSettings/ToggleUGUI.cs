using System;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings;

public class ToggleUGUI : MonoBehaviour
{
	public delegate void ValueChangedDelegate(bool value);

	public TextMeshProUGUI TextTf;

	public Toggle Toggle;

	public Toggle.ToggleEvent OnValueChangedEvent;

	public ValueChangedDelegate OnValueChanged;

	public bool Value
	{
		get
		{
			//IL_0041: Expected I4, but got O
			Toggle toggle = Toggle;
			if ((object)Toggle != null)
			{
				return toggle.m_IsOn;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		set
		{
			Toggle.isOn = value;
		}
	}

	public string Text
	{
		get
		{
			//IL_0031: Expected I, but got O
			TextMeshProUGUI textTf = TextTf;
			if ((object)TextTf != null)
			{
				nint num = (nint)textTf;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v12 @ rdx_v1 (Il2CppClass<TMPro.TextMeshProUGUI>)+548] (should have been resolved before IL gen)");
			}
			return (string)(object)new NullReferenceException();
		}
		set
		{
			string text = TextTf.text;
			if (value != text)
			{
				TextTf.text = value;
			}
		}
	}

	public void Start()
	{
		Toggle toggle = Toggle;
		UnityAction<bool> call = onValueChanged;
		toggle.onValueChanged.AddListener(call);
	}

	private unsafe void onValueChanged(bool isOn)
	{
		if (OnValueChangedEvent != null)
		{
			object obj = default(object);
			OnValueChangedEvent.Invoke((byte)(&obj) != 0);
		}
		ValueChangedDelegate valueChangedDelegate = OnValueChanged;
		if (OnValueChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v50.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
	}
}
