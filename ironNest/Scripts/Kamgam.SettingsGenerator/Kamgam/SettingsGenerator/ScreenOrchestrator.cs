using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class ScreenOrchestrator : MonoBehaviour
	{
		public delegate void OnCompleteDelegate(Resolution? resolution, bool? fullScreen, FullScreenMode? fullScreenMode);

		[CompilerGenerated]
		private sealed class _003CapplyStaggered_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ScreenOrchestrator _003C_003E4__this;

			private bool? _003CtRequestedFullScreen_003E5__2;

			private FullScreenMode? _003CtRequestedFullScreenMode_003E5__3;

			private Resolution? _003CtRequestedResolution_003E5__4;

			private RefreshRate? _003CtRequestedRefreshRate_003E5__5;

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
			public _003CapplyStaggered_003Ed__16(int _003C_003E1__state)
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

		private static ScreenOrchestrator _instance;

		public OnCompleteDelegate OnComplete;

		protected Resolution? requestedResolution;

		protected RefreshRate? requestedRefreshRate;

		protected bool? requestedFullScreen;

		protected FullScreenMode? requestedFullScreenMode;

		protected Coroutine _applyCoroutine;

		public static ScreenOrchestrator Instance => null;

		public void RequestResolution(Resolution resolution)
		{
		}

		public void RequestRefreshRate(RefreshRate refreshRate)
		{
		}

		public void RequestFullScreen(bool fullScreen)
		{
		}

		public void RequestFullScreenMode(FullScreenMode fullScreenMode)
		{
		}

		public void LateUpdate()
		{
		}

		protected void apply()
		{
		}

		[IteratorStateMachine(typeof(_003CapplyStaggered_003Ed__16))]
		protected IEnumerator applyStaggered()
		{
			return null;
		}

		public static Resolution GetCurrentResolution()
		{
			return default(Resolution);
		}

		public void Destroy()
		{
		}
	}
}
