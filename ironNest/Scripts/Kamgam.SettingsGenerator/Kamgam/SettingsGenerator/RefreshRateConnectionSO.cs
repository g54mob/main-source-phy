using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "RefreshRateConnection", menuName = "SettingsGenerator/Connection/RefreshRateConnection", order = 4)]
	public class RefreshRateConnectionSO : OptionConnectionSO
	{
		[Tooltip("Disable if the resolutions change very often.")]
		public bool CacheRefreshRates;

		[Tooltip("Refresh rate in Hz. Refresh rates below this are ignored. Equal rates are still included.")]
		public int MinRate;

		[Tooltip("Refresh rate in Hz. Refresh rates above this are ignored. Equal rates are still included.")]
		public int MaxRate;

		[Tooltip("If enabled then only those refresh rates which are usable with the current resolution are listed. That list may be much shorter than the full list (often just one).")]
		public bool LimitToCurrentResolution;

		protected RefreshRateConnection _connection;

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
