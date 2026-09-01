using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Kamgam.SettingsGenerator.Examples
{
	public class PlayMusicThroughAudioMixer : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CplayAndStopAfterNSeconds_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public PlayMusicThroughAudioMixer _003C_003E4__this;

			public float seconds;

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
			public _003CplayAndStopAfterNSeconds_003Ed__11(int _003C_003E1__state)
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

		public SettingsProvider SettingsProvider;

		public string musicSettingId;

		public float Duration;

		public AudioSource SourceManagedByMixer;

		protected SettingFloat _musicVolumeSetting;

		protected bool _isPlaying;

		protected float _musicVolume;

		public SettingFloat MusicVolumeSetting => null;

		public void Toggle()
		{
		}

		private void play()
		{
		}

		[IteratorStateMachine(typeof(_003CplayAndStopAfterNSeconds_003Ed__11))]
		private IEnumerator playAndStopAfterNSeconds(float seconds)
		{
			return null;
		}

		private void stop()
		{
		}
	}
}
