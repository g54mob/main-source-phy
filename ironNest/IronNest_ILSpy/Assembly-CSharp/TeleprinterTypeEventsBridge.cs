using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class TeleprinterTypeEventsBridge : MonoBehaviour
{
	private Teleprinter teleprinter;

	private Teleprinter.Teleprinters teleprinterType;

	private bool excludeLineBreaksFromCharEvents = true;

	private bool treatTabAndNbspAsSpace;

	public UnityEvent<char> onNonSpaceCharacterTyped;

	public UnityEvent<char> onSpaceTyped;

	public UnityEvent<int, int> onLineTransition;

	private int? _lastSeenLineNumber;

	private bool _subscribed;

	private void OnEnable()
	{
		if (this.teleprinter == null)
		{
			Teleprinter teleprinter = Teleprinter.GetTeleprinter(teleprinterType);
			this.teleprinter = teleprinter;
		}
		if (!_subscribed && this.teleprinter != null)
		{
			Teleprinter teleprinter2 = this.teleprinter;
			UnityAction<char, int, int> call = HandleCharacterPrintedDetailed;
			teleprinter2.onCharacterPrintedDetailed.AddListener(call);
			Teleprinter teleprinter3 = this.teleprinter;
			UnityAction<int, int> call2 = HandleLineTransition;
			teleprinter3.onLineTransition.AddListener(call2);
			_subscribed = true;
		}
	}

	private void OnDisable()
	{
		if (_subscribed)
		{
			if (this.teleprinter != null)
			{
				Teleprinter teleprinter = this.teleprinter;
				UnityAction<char, int, int> call = HandleCharacterPrintedDetailed;
				teleprinter.onCharacterPrintedDetailed.RemoveListener(call);
				Teleprinter teleprinter2 = this.teleprinter;
				UnityAction<int, int> call2 = HandleLineTransition;
				teleprinter2.onLineTransition.RemoveListener(call2);
			}
			_subscribed = false;
		}
	}

	private void ResolveTeleprinter()
	{
		if (this.teleprinter == null)
		{
			Teleprinter teleprinter = Teleprinter.GetTeleprinter(teleprinterType);
			this.teleprinter = teleprinter;
		}
	}

	private void Subscribe()
	{
		if (!_subscribed && this.teleprinter != null)
		{
			Teleprinter teleprinter = this.teleprinter;
			UnityAction<char, int, int> call = HandleCharacterPrintedDetailed;
			teleprinter.onCharacterPrintedDetailed.AddListener(call);
			Teleprinter teleprinter2 = this.teleprinter;
			UnityAction<int, int> call2 = HandleLineTransition;
			teleprinter2.onLineTransition.AddListener(call2);
			_subscribed = true;
		}
	}

	private void Unsubscribe()
	{
		if (_subscribed)
		{
			if (this.teleprinter != null)
			{
				Teleprinter teleprinter = this.teleprinter;
				UnityAction<char, int, int> call = HandleCharacterPrintedDetailed;
				teleprinter.onCharacterPrintedDetailed.RemoveListener(call);
				Teleprinter teleprinter2 = this.teleprinter;
				UnityAction<int, int> call2 = HandleLineTransition;
				teleprinter2.onLineTransition.RemoveListener(call2);
			}
			_subscribed = false;
		}
	}

	private unsafe void HandleCharacterPrintedDetailed(char character, int charIndex, int lineNumber)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		//IL_0058: Expected O, but got I4
		object obj = this + 72;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		int num3 = default(int);
		if (obj2 == null)
		{
			int? num2 = (int)(&num3);
			_lastSeenLineNumber = (int?)(object)0;
			num3 = lineNumber;
		}
		if (!excludeLineBreaksFromCharEvents || (character != '\n' && character != '\r'))
		{
			((character == ' ' || (treatTabAndNbspAsSpace && (character == '\t' || character == '\u00a0'))) ? onSpaceTyped : onNonSpaceCharacterTyped)?.Invoke((char)(ushort)(&num3));
		}
	}

	private unsafe void HandleLineTransition(int fromLine, int toLine)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		//IL_0086: Expected O, but got I4
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		object obj = this + 72;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		int num2 = default(int);
		if (obj2 != null)
		{
			object obj3 = this + 72;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
			if (num2 == toLine)
			{
				return;
			}
		}
		int? num3 = (int)(&num2);
		_lastSeenLineNumber = (int?)(object)0;
		if (onLineTransition != null)
		{
			object obj4 = default(object);
			onLineTransition.Invoke((int)(&obj4), (int)(&num2));
		}
	}

	private bool IsSpaceLike(char c)
	{
		//IL_0067: Expected O, but got I4
		if (c != ' ')
		{
			if (!treatTabAndNbspAsSpace)
			{
				return false;
			}
			if (c != '\t')
			{
				object obj = c - 160;
				return obj == null;
			}
		}
		return true;
	}
}
