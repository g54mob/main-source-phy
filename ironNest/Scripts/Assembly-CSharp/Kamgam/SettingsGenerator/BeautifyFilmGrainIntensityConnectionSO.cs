using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "BeautifyFilmGrainIntensityConnection", menuName = "SettingsGenerator/Connection/Beautify/Film Grain Intensity", order = 53)]
	public class BeautifyFilmGrainIntensityConnectionSO : FloatConnectionSO
	{
		[Tooltip("UI slider range. Typically 0..100.")]
		public Vector2 InputRange;

		[Tooltip("Beautify filmGrainIntensity range. Beautify clamps this to 0..1.\nInputRange.x maps to OutputRange.x, InputRange.y maps to OutputRange.y.")]
		public Vector2 OutputRange;

		[Tooltip("If true, automatically sets filmGrainEnabled=false when intensity reaches 0 and true otherwise.\nDisable if you manage the film grain on/off toggle with a separate Bool connection.")]
		public bool AutoToggleEnabled;

		[Tooltip("Re-search for the Beautify volume every Set() call. Enable if your volume can be spawned at runtime.")]
		public bool ResolveEveryAccess;

		[Tooltip("Log warnings to the Console when the Beautify volume cannot be found.")]
		public bool LogWarnings;

		private BeautifyFilmGrainIntensityConnection _connection;

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
