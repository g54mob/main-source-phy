using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class SettingsButtonActions : MonoBehaviour
	{
		[Tooltip("(Optional) Usually it's fine to leave this empty.\nIf set the this settings provider will be used. Otherwise the last used provider (or the configured provider, depending on the flag below) will be used instead.")]
		public SettingsProvider SettingsProvider;

		[Tooltip("If enabled then the configured global provider will be used if the SettingsProvider on this component is NULL, otherwise the last used provider will be used as fallback.")]
		public bool FallBackOnConfiguredProvider;

		protected SettingsProvider getProvider()
		{
			return null;
		}

		public void SettingsSave()
		{
		}

		public void SettingsReset()
		{
		}

		public void SettingsResetGroup(string group)
		{
		}

		public void SettingsResetWithoutGroup()
		{
		}

		public void SettingsResetControls()
		{
		}

		public void SettingsApply()
		{
		}

		public void SettingsApply(bool changedOnly)
		{
		}

		public void SettingsResetToUnapplied()
		{
		}

		public void SettingsResetToLastSaved()
		{
		}
	}
}
