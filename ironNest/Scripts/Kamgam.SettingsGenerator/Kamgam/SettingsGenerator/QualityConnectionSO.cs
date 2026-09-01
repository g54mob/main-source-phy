using System;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "QualityConnection", menuName = "SettingsGenerator/Connection/QualityConnection", order = 4)]
	public class QualityConnectionSO : OptionConnectionSO
	{
		[NonSerialized]
		[Obsolete("BUGFIX: The settings are now handed over automatically via the IConnectionWithSettingsAccess.SetSettings(Settings settings) method. This is no longer used and has no effect.")]
		[HideInInspector]
		public SettingsProvider SettingsProvider;

		protected QualityConnection _connection;

		public override IConnectionWithOptions<string> GetConnection()
		{
			return null;
		}

		public void Create()
		{
		}

		public override void DestroyConnection()
		{
		}
	}
}
