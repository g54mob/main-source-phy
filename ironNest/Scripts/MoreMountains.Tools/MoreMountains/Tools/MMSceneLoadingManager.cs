using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	public class MMSceneLoadingManager : MonoBehaviour
	{
		public enum LoadingStatus
		{
			LoadStarted = 0,
			BeforeEntryFade = 1,
			EntryFade = 2,
			AfterEntryFade = 3,
			UnloadOriginScene = 4,
			LoadDestinationScene = 5,
			LoadProgressComplete = 6,
			InterpolatedLoadProgressComplete = 7,
			BeforeExitFade = 8,
			ExitFade = 9,
			DestinationSceneActivation = 10,
			UnloadSceneLoader = 11,
			LoadTransitionComplete = 12
		}

		public struct LoadingSceneEvent
		{
			public LoadingStatus Status;

			public string SceneName;

			private static LoadingSceneEvent e;

			public LoadingSceneEvent(string sceneName, LoadingStatus status)
			{
				Status = default(LoadingStatus);
				SceneName = null;
			}

			public static void Trigger(string sceneName, LoadingStatus status)
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CLoadAsynchronously_003Ed__22 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMSceneLoadingManager _003C_003E4__this;

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
			public _003CLoadAsynchronously_003Ed__22(int _003C_003E1__state)
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

		[Header("Binding")]
		public static string LoadingScreenSceneName;

		[Header("GameObjects")]
		public Text LoadingText;

		public CanvasGroup LoadingProgressBar;

		public CanvasGroup LoadingAnimation;

		public CanvasGroup LoadingCompleteAnimation;

		[Header("Time")]
		public float StartFadeDuration;

		public float ProgressBarSpeed;

		public float ExitFadeDuration;

		public float LoadCompleteDelay;

		protected AsyncOperation _asyncOperation;

		protected static string _sceneToLoad;

		protected float _fadeDuration;

		protected float _fillTarget;

		protected string _loadingTextValue;

		protected Image _progressBarImage;

		protected static MMTweenType _tween;

		public static void LoadScene(string sceneToLoad)
		{
		}

		public static void LoadScene(string sceneToLoad, string loadingSceneName)
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void Update()
		{
		}

		[IteratorStateMachine(typeof(_003CLoadAsynchronously_003Ed__22))]
		protected virtual IEnumerator LoadAsynchronously()
		{
			return null;
		}

		protected virtual void LoadingSetup()
		{
		}

		protected virtual void LoadingComplete()
		{
		}
	}
}
