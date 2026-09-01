using Poly.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Poly.Timers
{
	public class PerformanceTimer : FrameStartEndListener
	{
		public int maxTimerDepth = 6;

		public bool timeGraphics = true;

		public bool showMax = true;

		public bool showExactFrameTime = true;

		public int resetMaxAfterNumFrames = 50;

		public bool _RESET_MAX;

		private bool pruneAgain;

		private int numFixedFramesPlayed;

		private bool isVisible;

		public Transform _temp_Canvas;

		public Image _temp_Image;

		public bool fadeOutTimerPanelWhenMouseInactive;

		private Color originalPanelColor;

		private Color originalTextColor;

		private int frameCount;

		private Timer timer = Singleton<Timer, int>.instance;
	}
}
