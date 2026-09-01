using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace MoreMountains.Tools
{
	public class MMAdditiveSceneLoadingManager : MMMonoBehaviour
	{
		public enum FadeModes
		{
			FadeInThenOut = 0,
			FadeOutThenIn = 1
		}

		[CompilerGenerated]
		private sealed class _003CDestinationSceneActivation_003Ed__62 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMAdditiveSceneLoadingManager _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CDestinationSceneActivation_003Ed__62(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CEntryFade_003Ed__56 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMAdditiveSceneLoadingManager _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CEntryFade_003Ed__56(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CExitFade_003Ed__61 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMAdditiveSceneLoadingManager _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CExitFade_003Ed__61(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CLoadDestinationScene_003Ed__59 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMAdditiveSceneLoadingManager _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CLoadDestinationScene_003Ed__59(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CLoadSequence_003Ed__53 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMAdditiveSceneLoadingManager _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CLoadSequence_003Ed__53(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CProcessDelayAfterEntryFade_003Ed__57 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMAdditiveSceneLoadingManager _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CProcessDelayAfterEntryFade_003Ed__57(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CProcessDelayBeforeEntryFade_003Ed__55 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMAdditiveSceneLoadingManager _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CProcessDelayBeforeEntryFade_003Ed__55(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CProcessDelayBeforeExitFade_003Ed__60 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMAdditiveSceneLoadingManager _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CProcessDelayBeforeExitFade_003Ed__60(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CUnloadOriginScenes_003Ed__58 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMAdditiveSceneLoadingManager _003C_003E4__this;

			private Scene[] _003C_003E7__wrap1;

			private int _003C_003E7__wrap2;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CUnloadOriginScenes_003Ed__58(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CUnloadSceneLoader_003Ed__64 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMAdditiveSceneLoadingManager _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CUnloadSceneLoader_003Ed__64(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[MMInspectorGroup("Audio Listener", true, 3, false)]
		public AudioListener LoadingAudioListener;

		[MMInspectorGroup("Settings", true, 10, false)]
		[Tooltip("the ID on which to trigger a fade, has to match the ID on the fader in your scene")]
		public int FaderID;

		[Tooltip("whether or not to output debug messages to the console")]
		public bool DebugMode;

		[MMInspectorGroup("Progress Events", true, 11, false)]
		[Tooltip("an event used to update progress")]
		public ProgressEvent SetRealtimeProgressValue;

		[Tooltip("an event used to update progress with interpolation")]
		public ProgressEvent SetInterpolatedProgressValue;

		[MMInspectorGroup("State Events", true, 12, false)]
		[Tooltip("an event that will be invoked when the load starts")]
		public UnityEvent OnLoadStarted;

		[Tooltip("an event that will be invoked when the delay before the entry fade starts")]
		public UnityEvent OnBeforeEntryFade;

		[Tooltip("an event that will be invoked when the entry fade starts")]
		public UnityEvent OnEntryFade;

		[Tooltip("an event that will be invoked when the delay after the entry fade starts")]
		public UnityEvent OnAfterEntryFade;

		[Tooltip("an event that will be invoked when the origin scene gets unloaded")]
		public UnityEvent OnUnloadOriginScene;

		[Tooltip("an event that will be invoked when the destination scene starts loading")]
		public UnityEvent OnLoadDestinationScene;

		[Tooltip("an event that will be invoked when the load of the destination scene is complete")]
		public UnityEvent OnLoadProgressComplete;

		[Tooltip("an event that will be invoked when the interpolated load of the destination scene is complete")]
		public UnityEvent OnInterpolatedLoadProgressComplete;

		[Tooltip("an event that will be invoked when the delay before the exit fade starts")]
		public UnityEvent OnBeforeExitFade;

		[Tooltip("an event that will be invoked when the exit fade starts")]
		public UnityEvent OnExitFade;

		[Tooltip("an event that will be invoked when the destination scene gets activated")]
		public UnityEvent OnDestinationSceneActivation;

		[Tooltip("an event that will be invoked when the scene loader gets unloaded")]
		public UnityEvent OnUnloadSceneLoader;

		protected static bool _interpolateProgress;

		protected static float _progressInterpolationSpeed;

		protected static List<MMSceneLoadingSpeedInterval> _speedIntervals;

		protected static float _beforeEntryFadeDelay;

		protected static MMTweenType _entryFadeTween;

		protected static float _entryFadeDuration;

		protected static float _afterEntryFadeDelay;

		protected static float _beforeExitFadeDelay;

		protected static MMTweenType _exitFadeTween;

		protected static float _exitFadeDuration;

		protected static FadeModes _fadeMode;

		protected static string _sceneToLoadName;

		protected static string _loadingScreenSceneName;

		protected static List<string> _scenesInBuild;

		protected static Scene[] _initialScenes;

		protected float _loadProgress;

		protected float _interpolatedLoadProgress;

		protected static bool _loadingInProgress;

		protected AsyncOperation _unloadOriginAsyncOperation;

		protected AsyncOperation _loadDestinationAsyncOperation;

		protected AsyncOperation _unloadLoadingAsyncOperation;

		protected bool _setRealtimeProgressValueIsNull;

		protected bool _setInterpolatedProgressValueIsNull;

		protected const float _asyncProgressLimit = 0.9f;

		protected MMSceneLoadingAntiSpill _antiSpill;

		protected static string _antiSpillSceneName;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		protected static void InitializeStatics()
		{
		}

		public static void LoadScene(string sceneToLoadName, MMAdditiveSceneLoadingManagerSettings settings)
		{
		}

		public static void LoadScene(string sceneToLoadName, string loadingSceneName = "MMAdditiveLoadingScreen", ThreadPriority threadPriority = ThreadPriority.High, bool secureLoad = true, bool interpolateProgress = true, float beforeEntryFadeDelay = 0f, float entryFadeDuration = 0.25f, float afterEntryFadeDelay = 0.1f, float beforeExitFadeDelay = 0.25f, float exitFadeDuration = 0.2f, MMTweenType entryFadeTween = null, MMTweenType exitFadeTween = null, float progressBarSpeed = 5f, FadeModes fadeMode = FadeModes.FadeInThenOut, MMAdditiveSceneLoadingManagerSettings.UnloadMethods unloadMethod = MMAdditiveSceneLoadingManagerSettings.UnloadMethods.AllScenes, string antiSpillSceneName = "", List<MMSceneLoadingSpeedInterval> speedIntervals = null)
		{
		}

		private static Scene[] GetScenesToUnload(MMAdditiveSceneLoadingManagerSettings.UnloadMethods unloaded)
		{
			return null;
		}

		protected virtual void Awake()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void UpdateProgress()
		{
		}

		public static float ComputeInterpolationSpeed(float t)
		{
			return 0f;
		}

		[IteratorStateMachine(typeof(_003CLoadSequence_003Ed__53))]
		protected virtual IEnumerator LoadSequence()
		{
			return null;
		}

		protected virtual void InitiateLoad()
		{
		}

		[IteratorStateMachine(typeof(_003CProcessDelayBeforeEntryFade_003Ed__55))]
		protected virtual IEnumerator ProcessDelayBeforeEntryFade()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CEntryFade_003Ed__56))]
		protected virtual IEnumerator EntryFade()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CProcessDelayAfterEntryFade_003Ed__57))]
		protected virtual IEnumerator ProcessDelayAfterEntryFade()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CUnloadOriginScenes_003Ed__58))]
		protected virtual IEnumerator UnloadOriginScenes()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CLoadDestinationScene_003Ed__59))]
		protected virtual IEnumerator LoadDestinationScene()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CProcessDelayBeforeExitFade_003Ed__60))]
		protected virtual IEnumerator ProcessDelayBeforeExitFade()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CExitFade_003Ed__61))]
		protected virtual IEnumerator ExitFade()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CDestinationSceneActivation_003Ed__62))]
		protected virtual IEnumerator DestinationSceneActivation()
		{
			return null;
		}

		protected virtual void OnLoadOperationComplete(AsyncOperation obj)
		{
		}

		[IteratorStateMachine(typeof(_003CUnloadSceneLoader_003Ed__64))]
		protected virtual IEnumerator UnloadSceneLoader()
		{
			return null;
		}

		protected virtual void SetAudioListener(bool state)
		{
		}

		protected virtual void OnDestroy()
		{
		}

		protected virtual void MMLoadingSceneDebug(string message)
		{
		}
	}
}
