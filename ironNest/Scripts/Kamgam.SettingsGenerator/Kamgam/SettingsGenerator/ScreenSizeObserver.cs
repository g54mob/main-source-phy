using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class ScreenSizeObserver : MonoBehaviour
	{
		public delegate void OnScreenSizeChangedDelegate(Resolution resolution);

		private static ScreenSizeObserver _instance;

		public OnScreenSizeChangedDelegate OnScreenSizeChanged;

		private int _lastScreenWidth;

		private int _lastScreenHeight;

		public static ScreenSizeObserver Instance => null;

		public void OnEnable()
		{
		}

		public void Update()
		{
		}
	}
}
