using System;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChatController : MonoBehaviour
{
	public TMP_InputField ChatInputField;

	public TMP_Text ChatDisplayOutput;

	public Scrollbar ChatScrollbar;

	private void OnEnable()
	{
		TMP_InputField chatInputField = ChatInputField;
		UnityAction<string> call = AddToChatOutput;
		chatInputField.m_OnSubmit.AddListener(call);
	}

	private void OnDisable()
	{
		TMP_InputField chatInputField = ChatInputField;
		UnityAction<string> call = AddToChatOutput;
		chatInputField.m_OnSubmit.RemoveListener(call);
	}

	private void AddToChatOutput(string newText)
	{
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		//IL_01c3: Expected O, but got I
		//IL_01d3: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rax_v7+B8]");
		object text = 0;
		ChatInputField.text = (string)text;
		DateTime now = DateTime.Now;
		DateTime dateTime = default(DateTime);
		int hour = dateTime.Hour;
		int num = default(int);
		string text2 = num.ToString("d2");
		int minute = dateTime.Minute;
		string text3 = num.ToString("d2");
		int second = dateTime.Second;
		string text4 = num.ToString("d2");
		string text5 = "[<#FFFF80>" + text2 + ":" + text3 + ":" + text4 + "</color>] " + newText;
		if (ChatDisplayOutput != null)
		{
			string text6 = ChatDisplayOutput.text;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v438 @ rcx_v29+B8]");
			object obj3 = 0;
			if (text6 != (string)obj3)
			{
				string text7 = ChatDisplayOutput.text;
				string text8 = text7 + "\n" + text5;
				ChatDisplayOutput.text = text8;
			}
			else
			{
				ChatDisplayOutput.text = text5;
			}
		}
		ChatInputField.ActivateInputField();
		ChatScrollbar.value = 0f;
	}
}
