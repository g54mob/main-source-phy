using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class SettingsButtonActions : MonoBehaviour
{
	public SettingsProvider SettingsProvider;

	public bool FallBackOnConfiguredProvider;

	protected SettingsProvider getProvider()
	{
		bool flag = SettingsProvider != null;
		if (!flag)
		{
			if (FallBackOnConfiguredProvider != flag)
			{
				return SettingsGeneratorSettings.GetProvider();
			}
			return SettingsProvider.LastUsedSettingsProvider;
		}
		return SettingsProvider;
	}

	public void SettingsSave()
	{
		SettingsProvider provider = getProvider();
		if ((bool)provider)
		{
			provider.Save();
		}
	}

	public void SettingsReset()
	{
		SettingsProvider provider = getProvider();
		if ((bool)provider)
		{
			provider.Reset();
		}
	}

	public void SettingsResetGroup(string group)
	{
		SettingsProvider provider = getProvider();
		if ((bool)provider)
		{
			provider.ResetGroup(group);
		}
	}

	public void SettingsResetWithoutGroup()
	{
		SettingsProvider provider = getProvider();
		if ((bool)provider)
		{
			provider.ResetWithoutGroup();
		}
	}

	public void SettingsResetControls()
	{
		SettingsProvider provider = getProvider();
		if ((bool)provider)
		{
			provider.ResetControls();
		}
	}

	public void SettingsApply()
	{
		SettingsProvider provider = getProvider();
		if ((bool)provider)
		{
			provider.Apply();
		}
	}

	public void SettingsApply(bool changedOnly)
	{
		SettingsProvider provider = getProvider();
		if ((bool)provider)
		{
			provider.Apply(changedOnly);
		}
	}

	public void SettingsResetToUnapplied()
	{
		SettingsProvider provider = getProvider();
		if ((bool)provider)
		{
			provider.ResetToUnappliedValues();
		}
	}

	public void SettingsResetToLastSaved()
	{
		SettingsProvider provider = getProvider();
		if ((bool)provider)
		{
			provider.ResetToLastSave();
		}
	}
}
