using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	public class MMAdditiveSceneLoadingManagerSettings
	{
		public enum UnloadMethods
		{
			None = 0,
			ActiveScene = 1,
			AllScenes = 2
		}

		[Tooltip("the name of the MMSceneLoadingManager scene you want to use when in additive mode")]
		public string LoadingSceneName;

		[Tooltip("when in additive loading mode, the thread priority to apply to the loading")]
		public ThreadPriority ThreadPriority;

		[Tooltip("whether or not to make additional sanity checks (better leave this to true)")]
		public bool SecureLoad;

		[Tooltip("when in additive loading mode, whether or not to interpolate the progress bar's progress")]
		public bool InterpolateProgress;

		[Tooltip("when in additive loading mode, when in additive loading mode, the duration (in seconds) of the delay before the entry fade")]
		public float BeforeEntryFadeDelay;

		[Tooltip("when in additive loading mode, the duration (in seconds) of the entry fade")]
		public float EntryFadeDuration;

		[Tooltip("when in additive loading mode, the duration (in seconds) of the delay before the entry fade")]
		public float AfterEntryFadeDelay;

		[Tooltip("when in additive loading mode, the duration (in seconds) of the delay before the exit fade")]
		public float BeforeExitFadeDelay;

		[Tooltip("when in additive loading mode, the duration (in seconds) of the exit fade")]
		public float ExitFadeDuration;

		[Tooltip("when in additive loading mode, when in additive loading mode, the tween to use to fade on entry")]
		public MMTweenType EntryFadeTween;

		[Tooltip("when in additive loading mode, the tween to use to fade on exit")]
		public MMTweenType ExitFadeTween;

		[Tooltip("when in additive loading mode, the speed at which the loader's progress bar should move")]
		public float ProgressBarSpeed;

		[Tooltip("a list of progress intervals (values should be between 0 and 1) and their associated speeds, letting you have the bar progress less linearly")]
		public List<MMSceneLoadingSpeedInterval> SpeedIntervals;

		[Tooltip("when in additive loading mode, the selective additive fade mode")]
		public MMAdditiveSceneLoadingManager.FadeModes FadeMode;

		[Tooltip("the chosen way to unload scenes (none, only the active scene, all loaded scenes)")]
		public UnloadMethods UnloadMethod;

		[Tooltip("the name of the anti spill scene to use when loading additively.If left empty, that scene will be automatically created, but you can specify any scene to use for that. Usually you'll want your own anti spill scene to be just an empty scene, but you can customize its lighting settings for example.")]
		public string AntiSpillSceneName;
	}
}
