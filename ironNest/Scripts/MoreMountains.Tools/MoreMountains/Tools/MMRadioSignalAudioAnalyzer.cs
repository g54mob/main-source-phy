using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMRadioSignalAudioAnalyzer : MMRadioSignal
	{
		[Header("Audio Analyzer")]
		public MMAudioAnalyzer TargetAnalyzer;

		public int BeatID;

		protected override void Shake()
		{
		}
	}
}
