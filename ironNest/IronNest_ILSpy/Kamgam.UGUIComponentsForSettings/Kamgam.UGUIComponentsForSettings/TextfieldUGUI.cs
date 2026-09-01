using System;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Kamgam.UGUIComponentsForSettings;

public class TextfieldUGUI : MonoBehaviour
{
	public delegate void OnTextChangedDelegate(string text);

	public TMP_InputField InputTf;

	public UnityEvent<string> OnTextChangedEvent;

	public OnTextChangedDelegate OnTextChanged;

	public string Text
	{
		get
		{
			TMP_InputField inputTf = InputTf;
			if ((object)InputTf != null)
			{
				return inputTf.m_Text;
			}
			return (string)(object)new NullReferenceException();
		}
		set
		{
			TMP_InputField inputTf = InputTf;
			if (value != inputTf.m_Text)
			{
				InputTf.text = value;
				if (OnTextChangedEvent != null)
				{
					TMP_InputField inputTf2 = InputTf;
					OnTextChangedEvent.Invoke(inputTf2.m_Text);
				}
				OnTextChangedDelegate onTextChangedDelegate = OnTextChanged;
				if (OnTextChanged != null)
				{
					TMP_InputField inputTf3 = InputTf;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v57.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
				}
			}
		}
	}

	public void Start()
	{
		TMP_InputField inputTf = InputTf;
		UnityAction<string> call = onTextChanged;
		inputTf.m_OnValueChanged.AddListener(call);
	}

	private void onTextChanged(string text)
	{
		OnTextChangedDelegate onTextChangedDelegate = OnTextChanged;
		if (OnTextChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v30.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
		if (OnTextChangedEvent != null)
		{
			OnTextChangedEvent.Invoke(text);
		}
	}
}
