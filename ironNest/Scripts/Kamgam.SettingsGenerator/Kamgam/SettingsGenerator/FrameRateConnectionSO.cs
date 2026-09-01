using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "FrameRateConnection", menuName = "SettingsGenerator/Connection/FrameRateConnection", order = 4)]
	public class FrameRateConnectionSO : OptionConnectionSO
	{
		public bool RemoveUnlimited;

		[Tooltip("If specified then these frame rates will be used. Common frame rates are: 30, 60, 120, 144, 165, 200, 240. HINT: Use -1 for unlimited frame rate.")]
		public List<int> CustomFrameRates;

		protected FrameRateConnection _connection;

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
