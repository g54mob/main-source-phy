using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(Text))]
	[AddComponentMenu("More Mountains/Tools/Performance/MMFPSCounter")]
	public class MMFPSCounter : MonoBehaviour
	{
		public enum Modes
		{
			Instant = 0,
			MovingAverage = 1,
			InstantAndMovingAverage = 2
		}

		public float UpdateInterval;

		public Modes Mode;

		protected float _framesAccumulated;

		protected float _framesDrawnInTheInterval;

		protected float _timeLeft;

		protected Text _text;

		protected int _currentFPS;

		protected int _totalFrames;

		protected int _average;

		private static string[] _stringsFrom00To300;

		protected virtual void Start()
		{
		}

		protected virtual void Update()
		{
		}
	}
}
