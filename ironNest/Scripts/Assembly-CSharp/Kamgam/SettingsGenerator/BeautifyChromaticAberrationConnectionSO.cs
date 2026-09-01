using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "BeautifyChromaticAberrationConnection", menuName = "SettingsGenerator/Connection/Beautify/Chromatic Aberration Intensity", order = 52)]
	public class BeautifyChromaticAberrationConnectionSO : FloatConnectionSO
	{
		[Tooltip("UI slider range. Typically 0..100.")]
		public Vector2 InputRange;

		[Tooltip("Beautify chromaticAberrationIntensity range.\nBeautify hard-clamps this to 0..0.1 internally — values above 0.1 have no additional effect.\nInputRange.x maps to OutputRange.x, InputRange.y maps to OutputRange.y.")]
		public Vector2 OutputRange;

		[Tooltip("Re-search for the Beautify volume every Set() call. Enable if your volume can be spawned at runtime.")]
		public bool ResolveEveryAccess;

		[Tooltip("Log warnings to the Console when the Beautify volume cannot be found.")]
		public bool LogWarnings;

		private BeautifyChromaticAberrationConnection _connection;

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
