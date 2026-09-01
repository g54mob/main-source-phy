using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public static class RenderUtils
	{
		public enum RenderPipe
		{
			BuiltIn = 0,
			URP = 1,
			HDRP = 2
		}

		private static Camera[] _tmpAllCameras;

		public static RenderPipe GetCurrentRenderPipeline()
		{
			return default(RenderPipe);
		}

		public static int GetAllCameras(out Camera[] cameras)
		{
			cameras = null;
			return 0;
		}

		public static Camera GetCurrentRenderingCamera(bool checkForMarker)
		{
			return null;
		}
	}
}
