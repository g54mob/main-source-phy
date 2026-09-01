using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "BeautifyAnamorphicFlaresIntensityConnection", menuName = "SettingsGenerator/Connection/Beautify/Anamorphic Flares Intensity", order = 55)]
	public class BeautifyAnamorphicFlaresIntensityConnectionSO : FloatConnectionSO
	{
		[Tooltip("UI slider range. Typically 0..100.")]
		public Vector2 InputRange;

		[Tooltip("Beautify anamorphicFlaresIntensity range. This is an unbounded FloatParameter; typical authored values are 0..3. InputRange.x maps to OutputRange.x, InputRange.y maps to OutputRange.y.")]
		public Vector2 OutputRange;

		[Tooltip("Re-search for the Beautify volume every Set() call. Enable if your volume can be spawned at runtime.")]
		public bool ResolveEveryAccess;

		[Tooltip("Log warnings to the Console when the Beautify volume cannot be found.")]
		public bool LogWarnings;

		private BeautifyAnamorphicFlaresIntensityConnection _connection;

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
