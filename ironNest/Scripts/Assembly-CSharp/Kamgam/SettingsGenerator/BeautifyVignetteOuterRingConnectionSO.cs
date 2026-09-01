using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "BeautifyVignetteOuterRingConnection", menuName = "SettingsGenerator/Connection/Beautify/Vignette Outer Ring", order = 54)]
	public class BeautifyVignetteOuterRingConnectionSO : FloatConnectionSO
	{
		[Tooltip("UI slider range. Typically 0..100.")]
		public Vector2 InputRange;

		[Tooltip("Beautify vignettingOuterRing range. Beautify clamps this to -2..1. Positive values darken the edges; negative values brighten them. Default (0, 1) gives a standard darkening vignette slider. InputRange.x maps to OutputRange.x, InputRange.y maps to OutputRange.y.")]
		public Vector2 OutputRange;

		[Tooltip("Re-search for the Beautify volume every Set() call. Enable if your volume can be spawned at runtime.")]
		public bool ResolveEveryAccess;

		[Tooltip("Log warnings to the Console when the Beautify volume cannot be found.")]
		public bool LogWarnings;

		private BeautifyVignetteOuterRingConnection _connection;

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
