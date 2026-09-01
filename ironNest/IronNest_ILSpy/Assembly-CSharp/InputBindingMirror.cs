using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;

[Serializable]
public class InputBindingMirror
{
	private sealed class _003Cget_BindingsToMirror_003Ed__5 : IEnumerable<InputBindingReference>, IEnumerable, IEnumerator<InputBindingReference>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private InputBindingReference _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		public InputBindingMirror _003C_003E4__this;

		private InputBindingReference[] _003C_003E7__wrap1;

		private int _003C_003E7__wrap2;

		InputBindingReference IEnumerator<InputBindingReference>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003Cget_BindingsToMirror_003Ed__5(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			_003C_003El__initialThreadId = num;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_003b: Expected I4, but got I8
			//IL_00aa: Expected I4, but got I8
			//IL_015c: Expected I4, but got O
			if (_003C_003E1__state == 0)
			{
				InputBindingMirror inputBindingMirror = _003C_003E4__this;
				_003C_003E1__state = -1;
				_003C_003E7__wrap1 = inputBindingMirror._bindingsToMirror;
				_003C_003E7__wrap2 = 0;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_0139;
				}
				int num = _003C_003E7__wrap2 + 1;
				_003C_003E7__wrap2 = num;
				_003C_003E1__state = -1;
			}
			InputBindingReference[] array = _003C_003E7__wrap1;
			if (_003C_003E7__wrap2 < array.Length)
			{
				InputBindingReference[] array2 = _003C_003E7__wrap1;
				int num2 = _003C_003E7__wrap2;
				if (_003C_003E7__wrap2 < array2.Length)
				{
					_003C_003E2__current = array2[num2];
					_003C_003E1__state = 1;
					return true;
				}
				IndexOutOfRangeException ex = new IndexOutOfRangeException();
				return (byte)(int)ex != 0;
			}
			_003C_003E7__wrap1 = null;
			goto IL_0139;
			IL_0139:
			return false;
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

		IEnumerator<InputBindingReference> IEnumerable<InputBindingReference>.GetEnumerator()
		{
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					return this;
				}
			}
			_003Cget_BindingsToMirror_003Ed__5 obj2 = new _003Cget_BindingsToMirror_003Ed__5(0);
			obj2._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj2._003C_003El__initialThreadId = num;
			obj2._003C_003E4__this = _003C_003E4__this;
			return obj2;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					return this;
				}
			}
			_003Cget_BindingsToMirror_003Ed__5 obj2 = new _003Cget_BindingsToMirror_003Ed__5(0);
			obj2._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj2._003C_003El__initialThreadId = num;
			obj2._003C_003E4__this = _003C_003E4__this;
			return obj2;
		}
	}

	private string _settingsId;

	private InputBindingReference[] _bindingsToMirror;

	public string SettingsId => _settingsId;

	public IEnumerable<InputBindingReference> BindingsToMirror
	{
		get
		{
			//IL_0042: Expected I4, but got I8
			_003Cget_BindingsToMirror_003Ed__5 obj = new _003Cget_BindingsToMirror_003Ed__5(0);
			obj._003C_003E1__state = -2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj._003C_003El__initialThreadId = num;
			obj._003C_003E4__this = this;
			return obj;
		}
	}
}
