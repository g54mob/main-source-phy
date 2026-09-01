using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class TextConsoleSimulator : MonoBehaviour
{
	private sealed class _003CRevealCharacters_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TMP_Text textComponent;

		public TextConsoleSimulator _003C_003E4__this;

		private TMP_TextInfo _003CtextInfo_003E5__2;

		private int _003CtotalVisibleCharacters_003E5__3;

		private int _003CvisibleCount_003E5__4;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CRevealCharacters_003Ed__7(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0088: Expected I4, but got I8
			//IL_0252: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_0069: Expected I4, but got I8
			//IL_0055: Expected I4, but got I8
			TextConsoleSimulator textConsoleSimulator = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj != 1)
					{
						return false;
					}
					_003C_003E1__state = -1;
					goto IL_027b;
				}
				_003C_003E1__state = -1;
				_003CvisibleCount_003E5__4 = 0;
				goto IL_01ae;
			}
			_003C_003E1__state = -1;
			if ((object)textComponent != null)
			{
				textComponent.ForceMeshUpdate();
				if ((object)textComponent != null)
				{
					TMP_TextInfo textInfo = textComponent.textInfo;
					_003CtextInfo_003E5__2 = textInfo;
					TMP_TextInfo tMP_TextInfo = _003CtextInfo_003E5__2;
					if (_003CtextInfo_003E5__2 != null)
					{
						_003CtotalVisibleCharacters_003E5__3 = tMP_TextInfo.characterCount;
						_003CvisibleCount_003E5__4 = 0;
						goto IL_027b;
					}
				}
			}
			goto IL_0244;
			IL_0244:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_027b:
			if ((object)_003C_003E4__this != null)
			{
				if (textConsoleSimulator.hasTextChanged)
				{
					TMP_TextInfo tMP_TextInfo2 = _003CtextInfo_003E5__2;
					if (_003CtextInfo_003E5__2 == null)
					{
						goto IL_0244;
					}
					_003CtotalVisibleCharacters_003E5__3 = tMP_TextInfo2.characterCount;
					textConsoleSimulator.hasTextChanged = false;
				}
				if (_003CvisibleCount_003E5__4 > _003CtotalVisibleCharacters_003E5__3)
				{
					WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_01ae;
			}
			goto IL_0244;
			IL_01ae:
			if ((object)textComponent != null)
			{
				textComponent.maxVisibleCharacters = _003CvisibleCount_003E5__4;
				int num = _003CvisibleCount_003E5__4 + 1;
				_003CvisibleCount_003E5__4 = num;
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
			goto IL_0244;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private sealed class _003CRevealWords_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TMP_Text textComponent;

		private int _003CtotalWordCount_003E5__2;

		private int _003CtotalVisibleCharacters_003E5__3;

		private int _003Ccounter_003E5__4;

		private int _003CvisibleCount_003E5__5;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CRevealWords_003Ed__8(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_007d: Expected I4, but got I8
			//IL_0015: Expected O, but got I4
			//IL_0069: Expected I4, but got I8
			//IL_0055: Expected I4, but got I8
			//IL_02c7: Expected O, but got I4
			//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d6: Expected I4, but got Unknown
			//IL_017b: Expected O, but got I4
			//IL_0301: Expected I4, but got O
			//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01aa: Expected O, but got Unknown
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (flag)
				{
					_003C_003E1__state = -1;
					goto IL_0220;
				}
				if ((nint)obj != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
				textComponent.ForceMeshUpdate();
				TMP_TextInfo textInfo = textComponent.textInfo;
				_003CtotalWordCount_003E5__2 = textInfo.wordCount;
				TMP_TextInfo textInfo2 = textComponent.textInfo;
				_003CtotalVisibleCharacters_003E5__3 = textInfo2.characterCount;
				_003Ccounter_003E5__4 = 0;
			}
			object obj2 = _003CtotalWordCount_003E5__2 + 1;
			int num = _003Ccounter_003E5__4 % obj2;
			if (num != 0)
			{
				if (num >= _003CtotalWordCount_003E5__2)
				{
					if (num == _003CtotalWordCount_003E5__2)
					{
						_003CvisibleCount_003E5__5 = _003CtotalVisibleCharacters_003E5__3;
					}
				}
				else
				{
					TMP_TextInfo textInfo3 = textComponent.textInfo;
					TMP_WordInfo[] wordInfo = textInfo3.wordInfo;
					object obj3 = num - 1;
					if ((nint)obj3 >= wordInfo.Length)
					{
						IndexOutOfRangeException ex = new IndexOutOfRangeException();
						return (byte)(int)ex != 0;
					}
					object obj4 = obj3 * 2;
					object obj5 = obj3 + obj4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rcx_v13 (TMPro.TMP_WordInfo[])+2C+v341 @ rax_v16*8]");
					int num2 = (int)((nint)0 + (nint)1);
					_003CvisibleCount_003E5__5 = num2;
				}
			}
			else
			{
				_003CvisibleCount_003E5__5 = 0;
			}
			textComponent.maxVisibleCharacters = _003CvisibleCount_003E5__5;
			if (_003CvisibleCount_003E5__5 < _003CtotalVisibleCharacters_003E5__3)
			{
				goto IL_0220;
			}
			WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
			_003C_003E2__current = waitForSeconds;
			_003C_003E1__state = 1;
			return true;
			IL_0220:
			int num3 = _003Ccounter_003E5__4 + 1;
			_003Ccounter_003E5__4 = num3;
			WaitForSeconds waitForSeconds2 = new WaitForSeconds(0.1f);
			_003C_003E2__current = waitForSeconds2;
			_003C_003E1__state = 2;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private TMP_Text m_TextComponent;

	private bool hasTextChanged;

	private void Awake()
	{
		GameObject gameObject = base.gameObject;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		TMP_Text textComponent = default(TMP_Text);
		m_TextComponent = textComponent;
	}

	private void Start()
	{
		_003CRevealCharacters_003Ed__7 obj = new _003CRevealCharacters_003Ed__7(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.textComponent = m_TextComponent;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private void OnEnable()
	{
		Action<UnityEngine.Object> rhs = ON_TEXT_CHANGED;
		TMPro_EventManager.TEXT_CHANGED_EVENT.Add(rhs);
	}

	private void OnDisable()
	{
		Action<UnityEngine.Object> rhs = ON_TEXT_CHANGED;
		TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(rhs);
	}

	private void ON_TEXT_CHANGED(UnityEngine.Object obj)
	{
		hasTextChanged = true;
	}

	private IEnumerator RevealCharacters(TMP_Text textComponent)
	{
		_003CRevealCharacters_003Ed__7 obj = new _003CRevealCharacters_003Ed__7(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.textComponent = textComponent;
		return obj;
	}

	private IEnumerator RevealWords(TMP_Text textComponent)
	{
		_003CRevealWords_003Ed__8 obj = new _003CRevealWords_003Ed__8(0);
		obj._003C_003E1__state = 0;
		obj.textComponent = textComponent;
		return obj;
	}
}
