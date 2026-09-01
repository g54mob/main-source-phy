using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class SettingsLocalizationKeys : ScriptableObject, IEnumerable<SettingLocalization>, IEnumerable
{
	private sealed class _003C_003Ec__DisplayClass1_0
	{
		public string settingId;

		internal bool _003CTryGetLangKey_003Eb__0(SettingLocalization x)
		{
			//IL_0048: Expected I4, but got O
			if (x != null)
			{
				return x._id == settingId;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003CGetEnumerator_003Ed__2 : IEnumerator<SettingLocalization>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private SettingLocalization _003C_003E2__current;

		public SettingsLocalizationKeys _003C_003E4__this;

		private List<SettingLocalization>.Enumerator _003C_003E7__wrap1;

		SettingLocalization IEnumerator<SettingLocalization>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CGetEnumerator_003Ed__2(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		unsafe void IDisposable.Dispose()
		{
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			if (_003C_003E1__state == -3 || _003C_003E1__state == 1)
			{
				_ = 4294967295L;
				object obj = default(object);
				List<SettingLocalization>.Enumerator enumerator = (List<SettingLocalization>.Enumerator)(obj + 40);
				((List<SettingLocalization>.Enumerator*)enumerator)->Dispose();
			}
		}

		private unsafe bool MoveNext()
		{
			//IL_011b: Expected O, but got I
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Expected O, but got Unknown
			//IL_0170: Unknown result type (might be due to invalid IL or missing references)
			//IL_0175: Expected O, but got Unknown
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Expected O, but got Unknown
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+20]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
			if ((nint)0 == 0)
			{
				_ = 4294967295L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+20]");
				if ((nint)0 == 0)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
				if ((nint)0 != 1)
				{
					return false;
				}
			}
			_ = 4294967293L;
			object obj2 = default(object);
			List<SettingLocalization>.Enumerator enumerator = (List<SettingLocalization>.Enumerator)(obj2 + 40);
			if (((List<SettingLocalization>.Enumerator*)enumerator)->MoveNext())
			{
				object obj3 = obj2 + 40;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				_ = 1;
				return true;
			}
			_ = 4294967295L;
			List<SettingLocalization>.Enumerator enumerator2 = (List<SettingLocalization>.Enumerator)(obj2 + 40);
			((List<SettingLocalization>.Enumerator*)enumerator2)->Dispose();
			_ = 0;
			_ = 0;
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private unsafe void _003C_003Em__Finally1()
		{
			//IL_0014: Expected I4, but got I8
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Expected O, but got Unknown
			_003C_003E1__state = -1;
			List<SettingLocalization>.Enumerator enumerator = (List<SettingLocalization>.Enumerator)(this + 40);
			((List<SettingLocalization>.Enumerator*)enumerator)->Dispose();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private List<SettingLocalization> _settingsLocalizationKeys;

	public unsafe bool TryGetLangKey(string settingId, out string langKey)
	{
		//IL_00d7: Expected I4, but got O
		//IL_00c4: Expected O, but got I4
		//IL_0081: Expected O, but got I
		//IL_00b6: Expected O, but got I
		_003C_003Ec__DisplayClass1_0 CS_0024_003C_003E8__locals3 = new _003C_003Ec__DisplayClass1_0();
		if (CS_0024_003C_003E8__locals3 != null)
		{
			CS_0024_003C_003E8__locals3.settingId = settingId;
			Predicate<SettingLocalization> predicate = delegate(SettingLocalization x)
			{
				//IL_0048: Expected I4, but got O
				if (x == null)
				{
					NullReferenceException ex2 = new NullReferenceException();
					return (byte)(int)ex2 != 0;
				}
				return x._id == CS_0024_003C_003E8__locals3.settingId;
			};
			if (_settingsLocalizationKeys != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A4610");
				object obj = default(object);
				object obj3;
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ stack_10+18]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ stack_10+18]");
					if ((nint)0 == 0)
					{
						goto IL_00c9;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rdx_v8+10]");
					obj3 = 0;
				}
				else
				{
					obj3 = 0;
				}
				ref string reference = ref *(string*)obj3;
				bool flag = obj == null;
				return !flag;
			}
		}
		goto IL_00c9;
		IL_00c9:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public IEnumerator<SettingLocalization> GetEnumerator()
	{
		_003CGetEnumerator_003Ed__2 obj = new _003CGetEnumerator_003Ed__2(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		_003CGetEnumerator_003Ed__2 obj = new _003CGetEnumerator_003Ed__2(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}
}
