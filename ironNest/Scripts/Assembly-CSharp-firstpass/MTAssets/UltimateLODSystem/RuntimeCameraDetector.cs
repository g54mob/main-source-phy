using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MTAssets.UltimateLODSystem
{
	[AddComponentMenu(null)]
	public class RuntimeCameraDetector : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CArrayOfCamerasDelayedUpdater_003Ed__4 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public RuntimeCameraDetector _003C_003E4__this;

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
			public _003CArrayOfCamerasDelayedUpdater_003Ed__4(int _003C_003E1__state)
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
		private sealed class _003CCurrentCameraOnScreenDetector_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public RuntimeCameraDetector _003C_003E4__this;

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
			public _003CCurrentCameraOnScreenDetector_003Ed__5(int _003C_003E1__state)
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

		private WaitForSecondsRealtime DELAY_BETWEEN_ARRAY_OF_CAMERAS_UPDATE;

		private WaitForSecondsRealtime DELAY_BETWEEN_CURRENT_CAMERA_DETECTOR;

		private Camera[] currentArrayOfCameras;

		public void Awake()
		{
		}

		[IteratorStateMachine(typeof(_003CArrayOfCamerasDelayedUpdater_003Ed__4))]
		private IEnumerator ArrayOfCamerasDelayedUpdater()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CCurrentCameraOnScreenDetector_003Ed__5))]
		private IEnumerator CurrentCameraOnScreenDetector()
		{
			return null;
		}
	}
}
