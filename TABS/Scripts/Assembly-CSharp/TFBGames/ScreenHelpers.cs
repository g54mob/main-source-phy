using UnityEngine;

namespace TFBGames
{
	public static class ScreenHelpers
	{
		public static float GetAspectRatio()
		{
			return (float)Screen.width / (float)Screen.height;
		}
	}
}
