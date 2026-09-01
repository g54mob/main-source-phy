using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator.Examples;

public class PlayMusicThroughAudioMixer : MonoBehaviour
{
	private sealed class _003CplayAndStopAfterNSeconds_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PlayMusicThroughAudioMixer _003C_003E4__this;

		public float seconds;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CplayAndStopAfterNSeconds_003Ed__11(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_014e: Expected I4, but got I8
			//IL_0191: Expected I4, but got O
			PlayMusicThroughAudioMixer playMusicThroughAudioMixer = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					SettingFloat musicVolumeSetting = _003C_003E4__this.MusicVolumeSetting;
					if (musicVolumeSetting != null)
					{
						float value = musicVolumeSetting.GetValue();
						float musicVolume = default(float);
						playMusicThroughAudioMixer._musicVolume = musicVolume;
						SettingFloat musicVolumeSetting2 = _003C_003E4__this.MusicVolumeSetting;
						if (musicVolumeSetting2 != null)
						{
							musicVolumeSetting2.SetValue(0f);
							if ((object)playMusicThroughAudioMixer.SourceManagedByMixer != null)
							{
								playMusicThroughAudioMixer.SourceManagedByMixer.Play();
								WaitForSeconds waitForSeconds = new WaitForSeconds(seconds);
								_003C_003E2__current = waitForSeconds;
								_003C_003E1__state = 1;
								return true;
							}
						}
					}
				}
				goto IL_0183;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_0183;
				}
				_003C_003E4__this.stop();
			}
			return false;
			IL_0183:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	public SettingsProvider SettingsProvider;

	public string musicSettingId;

	public float Duration = 5f;

	public AudioSource SourceManagedByMixer;

	protected SettingFloat _musicVolumeSetting;

	protected bool _isPlaying;

	protected float _musicVolume;

	public SettingFloat MusicVolumeSetting
	{
		get
		{
			if (_musicVolumeSetting == null && SettingsProvider != null)
			{
				if ((object)SettingsProvider != null)
				{
					if (!SettingsProvider.HasSettings())
					{
						goto IL_00e7;
					}
					if ((object)SettingsProvider != null)
					{
						Settings settings = SettingsProvider.Settings;
						if ((object)settings != null)
						{
							SettingFloat musicVolumeSetting = settings.GetFloat(musicSettingId);
							_musicVolumeSetting = musicVolumeSetting;
							goto IL_00e7;
						}
					}
				}
				return (SettingFloat)(object)new NullReferenceException();
			}
			goto IL_00e7;
			IL_00e7:
			return _musicVolumeSetting;
		}
	}

	public void Toggle()
	{
		bool isPlaying = !_isPlaying;
		_isPlaying = isPlaying;
		if (~(_isPlaying ? 1u : 0u) == 0)
		{
			StopAllCoroutines();
			SourceManagedByMixer.Stop();
			SettingFloat musicVolumeSetting = MusicVolumeSetting;
			musicVolumeSetting.SetValue(_musicVolume);
			_isPlaying = false;
		}
		else
		{
			_003CplayAndStopAfterNSeconds_003Ed__11 obj = new _003CplayAndStopAfterNSeconds_003Ed__11(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			obj.seconds = Duration;
			Coroutine coroutine = StartCoroutine(obj);
		}
	}

	private void play()
	{
		_003CplayAndStopAfterNSeconds_003Ed__11 obj = new _003CplayAndStopAfterNSeconds_003Ed__11(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.seconds = Duration;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private IEnumerator playAndStopAfterNSeconds(float seconds)
	{
		_003CplayAndStopAfterNSeconds_003Ed__11 obj = new _003CplayAndStopAfterNSeconds_003Ed__11(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.seconds = seconds;
		return obj;
	}

	private void stop()
	{
		StopAllCoroutines();
		SourceManagedByMixer.Stop();
		SettingFloat musicVolumeSetting = MusicVolumeSetting;
		musicVolumeSetting.SetValue(_musicVolume);
		_isPlaying = false;
	}
}
