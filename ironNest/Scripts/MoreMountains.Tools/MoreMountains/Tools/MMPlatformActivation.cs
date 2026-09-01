using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Activation/MMPlatformActivation")]
	public class MMPlatformActivation : MonoBehaviour
	{
		public enum ExecutionTimes
		{
			Awake = 0,
			Start = 1,
			OnEnable = 2
		}

		public enum PlatformActions
		{
			DoNothing = 0,
			Disable = 1
		}

		[Header("Settings")]
		public ExecutionTimes ExecutionTime;

		public bool DebugToTheConsole;

		[Header("Desktop")]
		public PlatformActions UNITY_STANDALONE_WIN;

		public PlatformActions UNITY_STANDALONE_OSX;

		public PlatformActions UNITY_STANDALONE_LINUX;

		public PlatformActions UNITY_STANDALONE;

		[Header("Mobile")]
		public PlatformActions UNITY_IOS;

		public PlatformActions UNITY_IPHONE;

		public PlatformActions UNITY_ANDROID;

		public PlatformActions UNITY_TIZEN;

		[Header("Console")]
		public PlatformActions UNITY_WII;

		public PlatformActions UNITY_PS4;

		public PlatformActions UNITY_XBOXONE;

		[Header("Others")]
		public PlatformActions UNITY_WEBGL;

		public PlatformActions UNITY_LUMIN;

		public PlatformActions UNITY_TVOS;

		public PlatformActions UNITY_WSA;

		public PlatformActions UNITY_FACEBOOK;

		public PlatformActions UNITY_ADS;

		public PlatformActions UNITY_ANALYTICS;

		[Header("Active in Editor")]
		public PlatformActions UNITY_EDITOR;

		public PlatformActions UNITY_EDITOR_WIN;

		public PlatformActions UNITY_EDITOR_OSX;

		public PlatformActions UNITY_EDITOR_LINUX;

		protected virtual void OnEnable()
		{
		}

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void Process()
		{
		}

		protected virtual void DisableIfNeeded(PlatformActions platform, string platformName)
		{
		}
	}
}
