using System;
using System.Collections.Generic;
using Kamgam.SettingsGenerator;
using UnityEngine;
using UnityEngine.Events;

public class SettingsExitHandler : MonoBehaviour
{
	private SettingsProvider _settingsProvider;

	private UnityEvent _onExit;

	private UnityEvent<List<ISetting>> _onUnappliedSettings;

	public void TryExitSettings()
	{
		if (_settingsProvider != null && _settingsProvider.HasSettings())
		{
			List<ISetting> list = new List<ISetting>();
			Settings settings = _settingsProvider.Settings;
			List<ISetting> list2 = default(List<ISetting>);
			List<ISetting> unappliedSettings = settings.GetUnappliedSettings(list2);
			if (list2 != null && list2._size > 0)
			{
				if (_onUnappliedSettings != null)
				{
					_onUnappliedSettings.Invoke(list2);
				}
				return;
			}
		}
		if (_onExit != null)
		{
			_onExit.Invoke();
		}
	}

	private unsafe bool HasUnappliedSettings(out List<ISetting> unappliedSettings)
	{
		//IL_01a4: Expected I4, but got O
		ref List<ISetting> reference = ref *(List<ISetting>*)null;
		if (!(_settingsProvider != null))
		{
			goto IL_0190;
		}
		if ((object)_settingsProvider != null)
		{
			if (!_settingsProvider.HasSettings())
			{
				goto IL_0190;
			}
			List<ISetting> list = new List<ISetting>();
			reference = ref *(List<ISetting>*)list;
			if ((object)_settingsProvider != null)
			{
				Settings settings = _settingsProvider.Settings;
				if ((object)settings != null)
				{
					List<ISetting> unappliedSettings2 = settings.GetUnappliedSettings(unappliedSettings);
					List<ISetting> list2 = unappliedSettings;
					if (unappliedSettings != null)
					{
						int num = list2._size ^ list2._size;
						int num2 = list2._size & num;
						bool flag = num2 < 0;
						bool flag2 = list2._size < 0;
						bool flag3 = list2._size == 0;
						bool flag4 = flag2 == flag;
						bool flag5 = !flag3;
						return flag5 & flag4;
					}
					goto IL_0190;
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_0190:
		return false;
	}
}
