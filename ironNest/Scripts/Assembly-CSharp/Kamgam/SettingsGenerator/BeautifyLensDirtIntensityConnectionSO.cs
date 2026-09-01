using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "BeautifyLensDirtIntensityConnection", menuName = "SettingsGenerator/Connection/Beautify/Lens Dirt Intensity", order = 51)]
	public class BeautifyLensDirtIntensityConnectionSO : FloatConnectionSO
	{
		[Tooltip("UI slider range. Typically 0..100.")]
		public Vector2 InputRange;

		[Tooltip("Beautify lensDirtIntensity range. Typical range is 0..1.\nInputRange.x maps to OutputRange.x, InputRange.y maps to OutputRange.y.")]
		public Vector2 OutputRange;

		[Tooltip("Re-search for the Beautify volume every Set() call. Enable if your volume can be spawned at runtime.")]
		public bool ResolveEveryAccess;

		[Tooltip("Log warnings to the Console when the Beautify volume cannot be found.")]
		public bool LogWarnings;

		private BeautifyLensDirtIntensityConnection _connection;

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
