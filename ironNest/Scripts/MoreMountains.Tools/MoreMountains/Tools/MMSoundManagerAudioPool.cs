using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	public class MMSoundManagerAudioPool
	{
		[CompilerGenerated]
		private sealed class _003CAutoDisableAudioSource_003Ed__2 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public AudioSource source;

			public float playbackDuration;

			public float duration;

			public AudioClip clip;

			public bool doNotAutoRecycleIfNotDonePlaying;

			public float playbackTime;

			private float _003CmaxTime_003E5__2;

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
			public _003CAutoDisableAudioSource_003Ed__2(int _003C_003E1__state)
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

		protected List<AudioSource> _pool;

		public virtual void FillAudioSourcePool(int poolSize, Transform parent)
		{
		}

		[IteratorStateMachine(typeof(_003CAutoDisableAudioSource_003Ed__2))]
		public virtual IEnumerator AutoDisableAudioSource(float duration, AudioSource source, AudioClip clip, bool doNotAutoRecycleIfNotDonePlaying, float playbackTime, float playbackDuration)
		{
			return null;
		}

		public virtual AudioSource GetAvailableAudioSource(bool poolCanExpand, Transform parent)
		{
			return null;
		}

		public virtual bool FreeSound(AudioSource sourceToStop)
		{
			return false;
		}
	}
}
