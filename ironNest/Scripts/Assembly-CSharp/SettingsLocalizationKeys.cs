using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "Localization/SettingsLocalizationKeys")]
public class SettingsLocalizationKeys : ScriptableObject, IEnumerable<SettingLocalization>, IEnumerable
{
	[CompilerGenerated]
	private sealed class _003CGetEnumerator_003Ed__2 : IEnumerator<SettingLocalization>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private SettingLocalization _003C_003E2__current;

		public SettingsLocalizationKeys _003C_003E4__this;

		private List<SettingLocalization>.Enumerator _003C_003E7__wrap1;

		SettingLocalization IEnumerator<SettingLocalization>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CGetEnumerator_003Ed__2(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[SerializeField]
	private List<SettingLocalization> _settingsLocalizationKeys;

	public bool TryGetLangKey(string settingId, out string langKey)
	{
		langKey = null;
		return false;
	}

	[IteratorStateMachine(typeof(_003CGetEnumerator_003Ed__2))]
	public IEnumerator<SettingLocalization> GetEnumerator()
	{
		return null;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return null;
	}
}
