using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "AntiAliasingConnection", menuName = "SettingsGenerator/Connection/AntiAliasingConnection", order = 4)]
	public class AntiAliasingConnectionSO : OptionConnectionSO
	{
		[Tooltip("Please notice that this has no effect in the Built-In render pipeline since there the anti aliasing settings is set globally in the GraphicsSettings. In URP and HDRP it's set per camera.")]
		public bool LimitToMainCamera;

		[Tooltip("If enabled then the options list will include MSAA for renders that support it (URP AND HDRP).")]
		public bool IncludeMSAA;

		protected AntiAliasingConnection _connection;

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
