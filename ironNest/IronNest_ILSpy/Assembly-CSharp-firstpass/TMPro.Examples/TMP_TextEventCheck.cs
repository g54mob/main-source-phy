using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

namespace TMPro.Examples;

public class TMP_TextEventCheck : MonoBehaviour
{
	public TMP_TextEventHandler TextEventHandler;

	private TMP_Text m_TextComponent;

	private void OnEnable()
	{
		if (TextEventHandler != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			TMP_Text textComponent = default(TMP_Text);
			m_TextComponent = textComponent;
			TMP_TextEventHandler textEventHandler = TextEventHandler;
			UnityAction<char, int> call = OnCharacterSelection;
			textEventHandler.m_OnCharacterSelection.AddListener(call);
			TMP_TextEventHandler textEventHandler2 = TextEventHandler;
			UnityAction<char, int> call2 = OnSpriteSelection;
			textEventHandler2.m_OnSpriteSelection.AddListener(call2);
			TMP_TextEventHandler textEventHandler3 = TextEventHandler;
			UnityAction<string, int, int> call3 = OnWordSelection;
			textEventHandler3.m_OnWordSelection.AddListener(call3);
			TMP_TextEventHandler textEventHandler4 = TextEventHandler;
			UnityAction<string, int, int> call4 = OnLineSelection;
			textEventHandler4.m_OnLineSelection.AddListener(call4);
			TMP_TextEventHandler textEventHandler5 = TextEventHandler;
			UnityAction<string, string, int> call5 = OnLinkSelection;
			textEventHandler5.m_OnLinkSelection.AddListener(call5);
		}
	}

	private void OnDisable()
	{
		if (TextEventHandler != null)
		{
			TMP_TextEventHandler textEventHandler = TextEventHandler;
			UnityAction<char, int> call = OnCharacterSelection;
			textEventHandler.m_OnCharacterSelection.RemoveListener(call);
			TMP_TextEventHandler textEventHandler2 = TextEventHandler;
			UnityAction<char, int> call2 = OnSpriteSelection;
			textEventHandler2.m_OnSpriteSelection.RemoveListener(call2);
			TMP_TextEventHandler textEventHandler3 = TextEventHandler;
			UnityAction<string, int, int> call3 = OnWordSelection;
			textEventHandler3.m_OnWordSelection.RemoveListener(call3);
			TMP_TextEventHandler textEventHandler4 = TextEventHandler;
			UnityAction<string, int, int> call4 = OnLineSelection;
			textEventHandler4.m_OnLineSelection.RemoveListener(call4);
			TMP_TextEventHandler textEventHandler5 = TextEventHandler;
			UnityAction<string, string, int> call5 = OnLinkSelection;
			textEventHandler5.m_OnLinkSelection.RemoveListener(call5);
		}
	}

	private void OnCharacterSelection(char c, int index)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
		int num = default(int);
		string text = num.ToString();
		object obj = default(object);
		string message = "Character [" + (string)obj + "] at Index: " + text + " has been selected.";
		Debug.Log(message);
	}

	private void OnSpriteSelection(char c, int index)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
		int num = default(int);
		string text = num.ToString();
		object obj = default(object);
		string message = "Sprite [" + (string)obj + "] at Index: " + text + " has been selected.";
		Debug.Log(message);
	}

	private void OnWordSelection(string word, int firstCharacterIndex, int length)
	{
		int num = default(int);
		string text = num.ToString();
		int num2 = default(int);
		string text2 = num2.ToString();
		string message = "Word [" + word + "] with first character index of " + text + " and length of " + text2 + " has been selected.";
		Debug.Log(message);
	}

	private void OnLineSelection(string lineText, int firstCharacterIndex, int length)
	{
		int num = default(int);
		string text = num.ToString();
		int num2 = default(int);
		string text2 = num2.ToString();
		string message = "Line [" + lineText + "] with first character index of " + text + " and length of " + text2 + " has been selected.";
		Debug.Log(message);
	}

	private void OnLinkSelection(string linkID, string linkText, int linkIndex)
	{
		if (m_TextComponent != null)
		{
			TMP_TextInfo textInfo = m_TextComponent.textInfo;
		}
		int num = default(int);
		string text = num.ToString();
		string message = "Link Index: " + text + " with ID [" + linkID + "] and Text \"" + linkText + "\" has been selected.";
		Debug.Log(message);
	}
}
