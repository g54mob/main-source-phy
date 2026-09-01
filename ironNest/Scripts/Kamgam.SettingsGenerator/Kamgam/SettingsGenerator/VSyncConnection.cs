using System;

namespace Kamgam.SettingsGenerator
{
	public class VSyncConnection : Connection<bool>
	{
		[NonSerialized]
		protected bool vSyncEnabled;

		public override bool Get()
		{
			return false;
		}

		public override void Set(bool vSyncEnabled)
		{
		}

		public override void OnQualityChanged(int qualityLevel)
		{
		}
	}
}
