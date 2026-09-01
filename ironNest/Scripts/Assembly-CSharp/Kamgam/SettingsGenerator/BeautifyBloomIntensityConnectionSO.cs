using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "BeautifyBloomIntensityConnection", menuName = "SettingsGenerator/Connection/Beautify/Bloom Intensity", order = 50)]
	public class BeautifyBloomIntensityConnectionSO : FloatConnectionSO
	{
		[Tooltip("UI slider range. Typically 0..100.")]
		public Vector2 InputRange;

		[Tooltip("Beautify bloomIntensity range.\nbloomIntensity is unbounded; typical values are 0..3 or 0..10 for heavy bloom.\nInputRange.x maps to OutputRange.x, InputRange.y maps to OutputRange.y.")]
		public Vector2 OutputRange;

		[Tooltip("Re-search for the Beautify volume every Set() call. Enable if your volume can be spawned at runtime.")]
		public bool ResolveEveryAccess;

		[Tooltip("Log warnings to the Console when the Beautify volume cannot be found.")]
		public bool LogWarnings;

		private BeautifyBloomIntensityConnection _connection;

		public override IConnection<float> GetConnection()
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
