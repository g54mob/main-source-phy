using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will request the load of a new scene, using the method of your choice")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("Scene/Load Scene")]
	public class MMF_LoadScene : MMF_Feedback
	{
		public enum LoadingModes
		{
			Direct = 0,
			MMSceneLoadingManager = 1,
			MMAdditiveSceneLoadingManager = 2,
			DirectAdditive = 3
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Scene Loading", true, 57, true, false)]
		[Tooltip("the name of the loading screen scene to use - HAS TO BE ADDED TO YOUR BUILD SETTINGS")]
		public string LoadingSceneName;

		[Tooltip("the name of the destination scene - HAS TO BE ADDED TO YOUR BUILD SETTINGS")]
		public string DestinationSceneName;

		[Header("Mode")]
		[Tooltip("the loading mode to use to load the destination scene : - direct : uses Unity's SceneManager API- MMSceneLoadingManager : the simple, original MM way of loading scenes- MMAdditiveSceneLoadingManager : a more advanced way of loading scenes, with (way) more options")]
		public LoadingModes LoadingMode;

		[Header("Loading Scene Manager")]
		[Tooltip("the priority to use when loading the new scenes")]
		public ThreadPriority Priority;

		[Tooltip("whether or not to perform extra checks to make sure the loading screen and destination scene are in the build settings")]
		public bool SecureLoad;

		[Tooltip("the chosen way to unload scenes (none, only the active scene, all loaded scenes)")]
		[MMFEnumCondition("LoadingMode", new int[] { 2 })]
		public MMAdditiveSceneLoadingManagerSettings.UnloadMethods UnloadMethod;

		[Tooltip("the name of the anti spill scene to use when loading additively.If left empty, that scene will be automatically created, but you can specify any scene to use for that. Usually you'll want your own anti spill scene to be just an empty scene, but you can customize its lighting settings for example.")]
		[MMFEnumCondition("LoadingMode", new int[] { 2 })]
		public string AntiSpillSceneName;

		[MMFInspectorGroup("Loading Scene Delays", true, 58, false, false)]
		[Tooltip("a delay (in seconds) to apply before the first fade plays")]
		public float BeforeEntryFadeDelay;

		[Tooltip("the duration (in seconds) of the entry fade")]
		public float EntryFadeDuration;

		[Tooltip("a delay (in seconds) to apply after the first fade plays")]
		public float AfterEntryFadeDelay;

		[Tooltip("a delay (in seconds) to apply before the exit fade plays")]
		public float BeforeExitFadeDelay;

		[Tooltip("the duration (in seconds) of the exit fade")]
		public float ExitFadeDuration;

		[MMFInspectorGroup("Speed", true, 59, false, false)]
		[Tooltip("whether or not to interpolate progress (slower, but usually looks better and smoother)")]
		public bool InterpolateProgress;

		[Tooltip("the speed at which the progress bar should move if interpolated")]
		public float ProgressInterpolationSpeed;

		[Tooltip("a list of progress intervals (values should be between 0 and 1) and their associated speeds, letting you have the bar progress less linearly")]
		public List<MMSceneLoadingSpeedInterval> SpeedIntervals;

		[MMFInspectorGroup("Transitions", true, 59, false, false)]
		[Tooltip("the order in which to play fades (really depends on the type of fader you have in your loading screen")]
		public MMAdditiveSceneLoadingManager.FadeModes FadeMode;

		[Tooltip("the tween to use on the entry fade")]
		public MMTweenType EntryFadeTween;

		[Tooltip("the tween to use on the exit fade")]
		public MMTweenType ExitFadeTween;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
