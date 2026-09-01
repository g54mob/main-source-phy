using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class RecordPlayerController : MonoBehaviour
{
	private enum NewspaperPlaybackState
	{
		Idle,
		FadingRecordOut,
		WaitingForCue,
		DelayingCue,
		FadingCueIn,
		CuePlaying,
		FadingCueOut,
		RestoringRecord
	}

	private sealed class _003CButtonActivationRoutine_003Ed__79 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CButtonActivationRoutine_003Ed__79(int _003C_003E1__state)
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
			//IL_00e7: Expected I4, but got I8
			//IL_01a1: Expected I4, but got O
			//IL_01de: Expected F4, but got I
			//IL_011d: Expected O, but got I
			UnityEngine.Object obj = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+98]");
					if ((nint)0 != _003C_003E1__state)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg = default(object);
						string text = $"{arg:F2}s (realtime).";
						string message = "[RecordPlayerController] Button activates in " + text;
						Debug.Log(message, _003C_003E4__this);
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+48]");
					WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(0f);
					_003C_003E2__current = waitForSecondsRealtime;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_0193;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_0193;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+A0]");
				bool flag = (UnityEngine.Object)0 == null;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+98]");
					if ((nint)0 != (flag ? 1 : 0))
					{
						Debug.Log("[RecordPlayerController] Button activation delay complete.", _003C_003E4__this);
					}
					_003C_003E4__this.SetButtonActive(active: true);
				}
				_ = 0;
			}
			return false;
			IL_0193:
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

	private sealed class _003CCrossfadeRoutine_003Ed__100 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		public AudioSource incoming;

		public AudioClip incomingClip;

		public AudioSource outgoing;

		private float _003Celapsed_003E5__2;

		private float _003Cduration_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCrossfadeRoutine_003Ed__100(int _003C_003E1__state)
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
			//IL_0197: Expected I4, but got I8
			//IL_0076: Invalid comparison between F4 and I
			//IL_038c: Expected F4, but got I
			//IL_01da: Invalid comparison between I4 and F4
			//IL_00a6: Expected F4, but got I
			//IL_0225: Expected F4, but got I4
			//IL_03cd: Expected O, but got I
			//IL_051d: Invalid comparison between I4 and F4
			//IL_0261: Expected F4, but got I4
			//IL_05d6: Expected I4, but got O
			//IL_042e: Expected O, but got I
			//IL_0282: Expected O, but got I
			UnityEngine.Object context = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D5A4C0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v1 (UnityEngine.Object)+70]");
				float volume = 0f * 0f;
				incoming.volume = volume;
				incoming.Play();
				_003Celapsed_003E5__2 = 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v1 (UnityEngine.Object)+58]");
				bool flag = !(0.0001f < 0f);
				float num = 0.0001f;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v1 (UnityEngine.Object)+58]");
					num = 0f;
				}
				_003Cduration_003E5__3 = num;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v1 (UnityEngine.Object)+98]");
				if ((nint)0 != 0)
				{
					AudioClip clip = outgoing.clip;
					string text;
					if ((object)clip != null)
					{
						string name = clip.name;
						text = name;
					}
					else
					{
						text = null;
					}
					if ((object)incomingClip == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					string name2 = incomingClip.name;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					string text2 = $"'{name2}' over {arg:F2}s.";
					string message = "[RecordPlayerController] Crossfade: '" + text + "' → " + text2;
					Debug.Log(message, context);
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_0469;
				}
				_003C_003E1__state = -1;
			}
			if (_003Cduration_003E5__3 > _003Celapsed_003E5__2)
			{
				float deltaTime = Time.deltaTime;
				float num2 = (_003Celapsed_003E5__2 = deltaTime + _003Celapsed_003E5__2) / _003Cduration_003E5__3;
				if (!(0f > num2))
				{
					if (num2 > 1f)
					{
						num2 = 1f;
					}
				}
				else
				{
					num2 = 0f;
				}
				if (!(0f > num2))
				{
					if (num2 > 1f)
					{
						num2 = 1f;
					}
				}
				else
				{
					num2 = 0f;
				}
				float num3 = num2 + num2;
				float num4 = num2 * num2;
				float num5 = 3f - num3;
				float num6 = num5 * num4;
				AudioSource audioSource = outgoing;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v1 (UnityEngine.Object)+28]");
				bool flag2 = audioSource == (UnityEngine.Object)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v1 (UnityEngine.Object)+B1]");
				if ((nint)0 != 0)
				{
					float num7 = 1f - num6;
				}
				else
				{
					float num7 = num6;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v1 (UnityEngine.Object)+B1]");
				if ((nint)0 != 0)
				{
					float num8 = num6;
				}
				else
				{
					float num8 = 1f - num6;
				}
				float num9 = 1f - num6;
				float num10 = num9;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v1 (UnityEngine.Object)+70]");
				float volume2 = num10 * 0f;
				outgoing.volume = volume2;
				float num11 = num6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v1 (UnityEngine.Object)+70]");
				float volume3 = num11 * 0f;
				incoming.volume = volume3;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			AudioSource audioSource2 = incoming;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v1 (UnityEngine.Object)+70]");
			audioSource2.volume = 0f;
			outgoing.volume = 0f;
			outgoing.Stop();
			AudioSource audioSource3 = incoming;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v1 (UnityEngine.Object)+28]");
			bool flag3 = audioSource3 == (UnityEngine.Object)0;
			_ = 1065353216;
			_ = 0;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v1 (UnityEngine.Object)+98]");
			if ((nint)0 != 0)
			{
				AudioSource audioSource4 = incoming;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v1 (UnityEngine.Object)+28]");
				bool flag4 = audioSource4 != (UnityEngine.Object)0;
				string text3 = "A";
				if (flag4)
				{
					text3 = "B";
				}
				string message2 = "[RecordPlayerController] Crossfade complete. Active: " + text3 + ".";
				Debug.Log(message2, context);
			}
			goto IL_0469;
			IL_0469:
			return false;
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

	private sealed class _003CFadeNewspaperCueIn_003Ed__90 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		private float _003Celapsed_003E5__2;

		private float _003Cduration_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CFadeNewspaperCueIn_003Ed__90(int _003C_003E1__state)
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
			//IL_020e: Expected I4, but got I8
			//IL_040a: Expected I4, but got O
			//IL_045c: Invalid comparison between I4 and F4
			//IL_031a: Expected F4, but got I4
			RecordPlayerController recordPlayerController = _003C_003E4__this;
			float num;
			float num2;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					if (!recordPlayerController.newspaperAudioSource || !recordPlayerController.newspaperClip)
					{
						recordPlayerController.newspaperPlaybackState = NewspaperPlaybackState.WaitingForCue;
						goto IL_0436;
					}
					if ((object)recordPlayerController.newspaperAudioSource != null)
					{
						recordPlayerController.newspaperAudioSource.Stop();
						if ((object)recordPlayerController.newspaperAudioSource != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D5A4C0");
							if ((object)recordPlayerController.newspaperAudioSource != null)
							{
								recordPlayerController.newspaperAudioSource.loop = true;
								recordPlayerController.newspaperWeight = 0f;
								if ((object)recordPlayerController.newspaperAudioSource != null)
								{
									recordPlayerController.newspaperAudioSource.volume = 0f;
									if ((object)recordPlayerController.newspaperAudioSource != null)
									{
										recordPlayerController.newspaperAudioSource.Play();
										_003Celapsed_003E5__2 = 0f;
										num = _003Celapsed_003E5__2;
										_003Cduration_003E5__3 = recordPlayerController.newspaperMusicFadeInSeconds;
										num2 = _003Cduration_003E5__3;
										goto IL_0241;
									}
								}
							}
						}
					}
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_0436;
				}
				_003C_003E1__state = -1;
				num = _003Celapsed_003E5__2;
				num2 = _003Cduration_003E5__3;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_0241;
				}
			}
			goto IL_03fc;
			IL_03e9:
			recordPlayerController.newspaperPlaybackState = NewspaperPlaybackState.FadingCueOut;
			goto IL_0436;
			IL_0436:
			return false;
			IL_03fc:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0241:
			if (num2 > num)
			{
				if (recordPlayerController.newspaperDismissRequested)
				{
					goto IL_03e9;
				}
				float unscaledDeltaTime = Time.unscaledDeltaTime;
				float num3 = (_003Celapsed_003E5__2 = unscaledDeltaTime + _003Celapsed_003E5__2);
				bool flag = !(0.0001f < _003Cduration_003E5__3);
				float num4 = 0.0001f;
				if (!flag)
				{
					num4 = _003Cduration_003E5__3;
				}
				float num5 = num3 / num4;
				if (!(0f > num5))
				{
					if (num5 > 1f)
					{
						num5 = 1f;
					}
				}
				else
				{
					num5 = 0f;
				}
				float num6 = num5 + num5;
				float num7 = num5 * num5;
				float num8 = 3f - num6;
				float num9 = (recordPlayerController.newspaperWeight = num8 * num7);
				if ((object)recordPlayerController.newspaperAudioSource != null)
				{
					float volume = num9 * recordPlayerController.masterVolume;
					recordPlayerController.newspaperAudioSource.volume = volume;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
			}
			else
			{
				if (recordPlayerController.newspaperDismissRequested)
				{
					goto IL_03e9;
				}
				recordPlayerController.newspaperWeight = 1f;
				if ((object)recordPlayerController.newspaperAudioSource != null)
				{
					recordPlayerController.newspaperAudioSource.volume = recordPlayerController.masterVolume;
					recordPlayerController.newspaperPlaybackState = NewspaperPlaybackState.CuePlaying;
					goto IL_0436;
				}
			}
			goto IL_03fc;
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

	private sealed class _003CFadeNewspaperCueOut_003Ed__91 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		private float _003CstartWeight_003E5__2;

		private float _003Celapsed_003E5__3;

		private float _003Cduration_003E5__4;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CFadeNewspaperCueOut_003Ed__91(int _003C_003E1__state)
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
			//IL_0092: Expected I4, but got I8
			//IL_035d: Expected I4, but got O
			//IL_03ca: Invalid comparison between I4 and F4
			//IL_012c: Expected F4, but got I4
			//IL_0325: Expected O, but got I4
			//IL_0338: Unknown result type (might be due to invalid IL or missing references)
			//IL_033d: Expected I4, but got Unknown
			//IL_0424: Invalid comparison between I4 and F4
			//IL_0168: Expected F4, but got I4
			RecordPlayerController recordPlayerController = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_034f;
				}
				_003CstartWeight_003E5__2 = recordPlayerController.newspaperWeight;
				_003Celapsed_003E5__3 = 0f;
				_003Cduration_003E5__4 = recordPlayerController.newspaperMusicFadeOutSeconds;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_03ac;
				}
				_003C_003E1__state = -1;
			}
			if (_003Cduration_003E5__4 > _003Celapsed_003E5__3)
			{
				float unscaledDeltaTime = Time.unscaledDeltaTime;
				float num = (_003Celapsed_003E5__3 = unscaledDeltaTime + _003Celapsed_003E5__3);
				bool flag = !(0.0001f < _003Cduration_003E5__4);
				float num2 = 0.0001f;
				if (!flag)
				{
					num2 = _003Cduration_003E5__4;
				}
				float num3 = num / num2;
				if (!(0f > num3))
				{
					if (num3 > 1f)
					{
						num3 = 1f;
					}
				}
				else
				{
					num3 = 0f;
				}
				float num4 = num3 + num3;
				float num5 = num3 * num3;
				float num6 = 3f - num4;
				float num7 = num6 * num5;
				if (!(0f > num7))
				{
					if (num7 > 1f)
					{
						num7 = 1f;
					}
				}
				else
				{
					num7 = 0f;
				}
				if ((object)_003C_003E4__this != null)
				{
					float num8 = 0f - _003CstartWeight_003E5__2;
					float num9 = num8 * num7;
					float newspaperWeight = num9 + _003CstartWeight_003E5__2;
					recordPlayerController.newspaperWeight = newspaperWeight;
					if ((bool)recordPlayerController.newspaperAudioSource)
					{
						if ((object)recordPlayerController.newspaperAudioSource == null)
						{
							goto IL_034f;
						}
						float volume = recordPlayerController.newspaperWeight * recordPlayerController.masterVolume;
						recordPlayerController.newspaperAudioSource.volume = volume;
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
			}
			else if ((object)_003C_003E4__this != null)
			{
				recordPlayerController.newspaperWeight = 0f;
				if (!recordPlayerController.newspaperAudioSource)
				{
					goto IL_030d;
				}
				if ((object)recordPlayerController.newspaperAudioSource != null)
				{
					recordPlayerController.newspaperAudioSource.Stop();
					if ((object)recordPlayerController.newspaperAudioSource != null)
					{
						recordPlayerController.newspaperAudioSource.volume = 0f;
						goto IL_030d;
					}
				}
			}
			goto IL_034f;
			IL_030d:
			bool flag2 = _003C_003E4__this.CanRestoreNewspaperRecord();
			object obj = 0 - (flag2 ? 1 : 0);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb eax,eax\"");
			NewspaperPlaybackState newspaperPlaybackState = (NewspaperPlaybackState)(obj & 7);
			recordPlayerController.newspaperPlaybackState = newspaperPlaybackState;
			goto IL_03ac;
			IL_034f:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_03ac:
			return false;
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

	private sealed class _003CFadeRecordOutForNewspaper_003Ed__88 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		private float _003Celapsed_003E5__2;

		private float _003Cduration_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CFadeRecordOutForNewspaper_003Ed__88(int _003C_003E1__state)
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
			//IL_0083: Expected I4, but got I8
			//IL_055d: Expected I4, but got O
			//IL_05d2: Invalid comparison between I4 and F4
			//IL_011d: Expected F4, but got I4
			//IL_02bf: Expected F4, but got I4
			//IL_0343: Expected F4, but got I4
			//IL_053d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0542: Expected I4, but got Unknown
			//IL_04d1: Invalid comparison between F4 and I4
			//IL_04e5: Invalid comparison between F4 and I4
			//IL_04f8: Expected O, but got I4
			//IL_0501: Unknown result type (might be due to invalid IL or missing references)
			//IL_0506: Expected I4, but got Unknown
			RecordPlayerController recordPlayerController = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003Celapsed_003E5__2 = 0f;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_054f;
				}
				_003Cduration_003E5__3 = recordPlayerController.previousRecordFadeOutSeconds;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_05ac;
				}
				_003C_003E1__state = -1;
			}
			if (_003Cduration_003E5__3 > _003Celapsed_003E5__2)
			{
				float unscaledDeltaTime = Time.unscaledDeltaTime;
				float num = (_003Celapsed_003E5__2 = unscaledDeltaTime + _003Celapsed_003E5__2);
				bool flag = !(0.0001f < _003Cduration_003E5__3);
				float num2 = 0.0001f;
				if (!flag)
				{
					num2 = _003Cduration_003E5__3;
				}
				float num3 = num / num2;
				if (!(0f > num3))
				{
					if (num3 > 1f)
					{
						num3 = 1f;
					}
				}
				else
				{
					num3 = 0f;
				}
				float num4 = num3 + num3;
				float num5 = num3 * num3;
				float num6 = 3f - num4;
				float num7 = num6 * num5;
				float num8 = 1f - num7;
				if ((object)_003C_003E4__this != null && (object)recordPlayerController.audioSourceA != null)
				{
					float num9 = num8 * recordPlayerController.newspaperSavedAWeight;
					float volume = num9 * recordPlayerController.masterVolume;
					recordPlayerController.audioSourceA.volume = volume;
					if ((object)recordPlayerController.audioSourceB != null)
					{
						float num10 = num8 * recordPlayerController.newspaperSavedBWeight;
						float volume2 = num10 * recordPlayerController.masterVolume;
						recordPlayerController.audioSourceB.volume = volume2;
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
				}
			}
			else if ((object)_003C_003E4__this != null && (object)recordPlayerController.audioSourceA != null)
			{
				AudioClip clip = recordPlayerController.audioSourceA.clip;
				float newspaperSavedATime;
				if ((bool)clip)
				{
					if ((object)recordPlayerController.audioSourceA == null)
					{
						goto IL_054f;
					}
					newspaperSavedATime = recordPlayerController.audioSourceA.time;
				}
				else
				{
					newspaperSavedATime = 0f;
				}
				recordPlayerController.newspaperSavedATime = newspaperSavedATime;
				if ((object)recordPlayerController.audioSourceB != null)
				{
					AudioClip clip2 = recordPlayerController.audioSourceB.clip;
					float newspaperSavedBTime;
					if ((bool)clip2)
					{
						if ((object)recordPlayerController.audioSourceB == null)
						{
							goto IL_054f;
						}
						newspaperSavedBTime = recordPlayerController.audioSourceB.time;
					}
					else
					{
						newspaperSavedBTime = 0f;
					}
					recordPlayerController.newspaperSavedBTime = newspaperSavedBTime;
					if (recordPlayerController.newspaperSavedAWasPlaying)
					{
						if ((object)recordPlayerController.audioSourceA == null)
						{
							goto IL_054f;
						}
						recordPlayerController.audioSourceA.Pause();
					}
					if (recordPlayerController.newspaperSavedBWasPlaying)
					{
						if ((object)recordPlayerController.audioSourceB == null)
						{
							goto IL_054f;
						}
						recordPlayerController.audioSourceB.Pause();
					}
					if ((object)recordPlayerController.audioSourceA != null)
					{
						recordPlayerController.audioSourceA.volume = 0f;
						if ((object)recordPlayerController.audioSourceB != null)
						{
							recordPlayerController.audioSourceB.volume = 0f;
							NewspaperPlaybackState newspaperPlaybackState;
							if (!recordPlayerController.newspaperDismissRequested)
							{
								if ((bool)recordPlayerController.newspaperClip && (bool)recordPlayerController.newspaperAudioSource)
								{
									bool flag2 = recordPlayerController.newspaperStartDelaySeconds < 0f;
									bool flag3 = recordPlayerController.newspaperStartDelaySeconds == 0f;
									object obj = flag2 | flag3;
									newspaperPlaybackState = (NewspaperPlaybackState)(obj + 3);
								}
								else
								{
									newspaperPlaybackState = NewspaperPlaybackState.WaitingForCue;
								}
							}
							else
							{
								bool flag4 = _003C_003E4__this.CanRestoreNewspaperRecord();
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb ecx,ecx\"");
								NewspaperPlaybackState newspaperPlaybackState2 = (NewspaperPlaybackState)(_003C_003E4__this & 7);
								newspaperPlaybackState = newspaperPlaybackState2;
							}
							recordPlayerController.newspaperPlaybackState = newspaperPlaybackState;
							goto IL_05ac;
						}
					}
				}
			}
			goto IL_054f;
			IL_054f:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_05ac:
			return false;
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

	private sealed class _003CNewspaperStartDelayRoutine_003Ed__89 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		private float _003Celapsed_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CNewspaperStartDelayRoutine_003Ed__89(int _003C_003E1__state)
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
			//IL_00d6: Expected I4, but got I8
			//IL_02db: Expected I4, but got O
			//IL_0107: Invalid comparison between I and F4
			//IL_02aa: Expected O, but got I4
			//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c2: Expected O, but got Unknown
			//IL_0250: Expected O, but got I4
			//IL_0263: Unknown result type (might be due to invalid IL or missing references)
			//IL_0268: Expected O, but got Unknown
			UnityEngine.Object obj = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+98]");
					if ((nint)0 != _003C_003E1__state)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg = default(object);
						string text = $"{arg:F2}s (realtime) before fading in.";
						string message = "[RecordPlayerController] Newspaper cue delayed by " + text;
						Debug.Log(message, _003C_003E4__this);
					}
					_003Celapsed_003E5__2 = 0f;
					goto IL_00f5;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_0317;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_00f5;
				}
			}
			goto IL_02cd;
			IL_0317:
			return false;
			IL_02cd:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_00f5:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+60]");
			if (0f > _003Celapsed_003E5__2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+111]");
				if ((nint)0 == 0)
				{
					float unscaledDeltaTime = Time.unscaledDeltaTime;
					float num = unscaledDeltaTime + _003Celapsed_003E5__2;
					_003C_003E2__current = null;
					_003Celapsed_003E5__2 = num;
					_003C_003E1__state = 1;
					return true;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+111]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+98]");
				if ((nint)0 != 0)
				{
					Debug.Log("[RecordPlayerController] Newspaper start delay complete — fading cue in.", _003C_003E4__this);
				}
				_ = 4;
				goto IL_0317;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+98]");
			if ((nint)0 != 0)
			{
				Debug.Log("[RecordPlayerController] Newspaper start delay interrupted by dismiss.", _003C_003E4__this);
				bool flag = _003C_003E4__this.CanRestoreNewspaperRecord();
				object obj2 = 0 - (flag ? 1 : 0);
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb eax,eax\"");
				object obj3 = obj2 & 7;
				return false;
			}
			bool flag2 = _003C_003E4__this.CanRestoreNewspaperRecord();
			if ((object)_003C_003E4__this != null)
			{
				object obj4 = 0 - (flag2 ? 1 : 0);
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb eax,eax\"");
				object obj5 = obj4 & 7;
				return false;
			}
			goto IL_02cd;
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

	private sealed class _003CRestoreRecordAfterNewspaper_003Ed__92 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		private bool _003CrestoreAPlaying_003E5__2;

		private bool _003CrestoreBPlaying_003E5__3;

		private float _003Celapsed_003E5__4;

		private float _003Cduration_003E5__5;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CRestoreRecordAfterNewspaper_003Ed__92(int _003C_003E1__state)
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
			//IL_0327: Expected I4, but got I8
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			//IL_05ff: Expected F4, but got O
			//IL_0608: Unknown result type (might be due to invalid IL or missing references)
			//IL_060d: Expected O, but got Unknown
			//IL_04f3: Expected O, but got I4
			//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0502: Expected O, but got Unknown
			//IL_069b: Invalid comparison between I4 and F4
			//IL_0708: Expected F4, but got O
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Expected O, but got Unknown
			//IL_03c1: Expected F4, but got I4
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Expected O, but got Unknown
			//IL_0141: Expected O, but got I4
			//IL_0158: Expected O, but got I4
			//IL_0652: Expected I4, but got O
			//IL_01c2: Expected O, but got I
			//IL_01dd: Expected O, but got I
			//IL_01ee: Expected O, but got I
			//IL_0212: Expected O, but got I
			//IL_023a: Expected O, but got I
			RecordPlayerController recordPlayerController = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if (recordPlayerController.CanRestoreNewspaperRecord())
				{
					recordPlayerController._useAAsActive = recordPlayerController.newspaperSavedUseAAsActive;
					object obj = recordPlayerController + 292;
					if (!recordPlayerController.newspaperSavedUseAAsActive)
					{
						obj = recordPlayerController + 296;
					}
					recordPlayerController._activeWeight = (float)obj;
					object obj2 = recordPlayerController + 296;
					if (!recordPlayerController._useAAsActive)
					{
						obj2 = recordPlayerController + 292;
					}
					recordPlayerController._inactiveWeight = (float)obj2;
					_003CrestoreAPlaying_003E5__2 = recordPlayerController.newspaperSavedAWasPlaying;
					_003CrestoreBPlaying_003E5__3 = recordPlayerController.newspaperSavedBWasPlaying;
					if (recordPlayerController.newspaperSavedStartDelay)
					{
						RecordItem currentRecord = recordPlayerController._currentRecord;
						if (currentRecord.tracks != null && recordPlayerController._trackIndex >= 0)
						{
							AudioClip[] tracks = currentRecord.tracks;
							if (recordPlayerController._trackIndex < tracks.Length)
							{
								bool flag = recordPlayerController._useAAsActive;
								object obj3 = 40;
								if (!flag)
								{
									obj3 = 48;
								}
								RecordItem currentRecord2 = recordPlayerController._currentRecord;
								AudioClip[] tracks2 = currentRecord2.tracks;
								int trackIndex = recordPlayerController._trackIndex;
								if (recordPlayerController._trackIndex < tracks2.Length)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D5A4C0");
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v687 @ rax_v48+v35 @ rbp_v1 (RecordPlayerController)]");
									((AudioSource)0).time = 0f;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v687 @ rax_v48+v35 @ rbp_v1 (RecordPlayerController)]");
									((AudioSource)0).volume = 0f;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v687 @ rax_v48+v35 @ rbp_v1 (RecordPlayerController)]");
									((AudioSource)0).Play();
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v687 @ rax_v48+v35 @ rbp_v1 (RecordPlayerController)]");
									bool flag2 = (UnityEngine.Object)0 == recordPlayerController.audioSourceA;
									_003CrestoreAPlaying_003E5__2 = flag2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v687 @ rax_v48+v35 @ rbp_v1 (RecordPlayerController)]");
									bool flag3 = (UnityEngine.Object)0 == recordPlayerController.audioSourceB;
									_003CrestoreBPlaying_003E5__3 = flag3;
									goto IL_0299;
								}
								IndexOutOfRangeException ex = new IndexOutOfRangeException();
								return (byte)(int)ex != 0;
							}
						}
					}
					recordPlayerController.RestoreNewspaperSource(recordPlayerController.audioSourceA, recordPlayerController.newspaperSavedATime, _003CrestoreAPlaying_003E5__2);
					recordPlayerController.RestoreNewspaperSource(recordPlayerController.audioSourceB, recordPlayerController.newspaperSavedBTime, _003CrestoreBPlaying_003E5__3);
					goto IL_0299;
				}
				recordPlayerController.newspaperPlaybackState = NewspaperPlaybackState.Idle;
			}
			else if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				goto IL_0652;
			}
			goto IL_0675;
			IL_0299:
			recordPlayerController.audioSourceA.volume = 0f;
			recordPlayerController.audioSourceB.volume = 0f;
			_003Celapsed_003E5__4 = 0f;
			_003Cduration_003E5__5 = recordPlayerController.previousRecordFadeBackInSeconds;
			goto IL_0652;
			IL_0675:
			return false;
			IL_0652:
			if (_003Cduration_003E5__5 > _003Celapsed_003E5__4)
			{
				float unscaledDeltaTime = Time.unscaledDeltaTime;
				float num = (_003Celapsed_003E5__4 = unscaledDeltaTime + _003Celapsed_003E5__4);
				bool flag4 = !(0.0001f < _003Cduration_003E5__5);
				float num2 = 0.0001f;
				if (!flag4)
				{
					num2 = _003Cduration_003E5__5;
				}
				float num3 = num / num2;
				if (!(0f > num3))
				{
					if (num3 > 1f)
					{
						num3 = 1f;
					}
				}
				else
				{
					num3 = 0f;
				}
				float num4 = num3 + num3;
				float num5 = num3 * num3;
				float num6 = 3f - num4;
				float num7 = num6 * num5;
				float num8 = num7 * recordPlayerController.newspaperSavedAWeight;
				float volume = num8 * recordPlayerController.masterVolume;
				recordPlayerController.audioSourceA.volume = volume;
				float num9 = num7 * recordPlayerController.newspaperSavedBWeight;
				float volume2 = num9 * recordPlayerController.masterVolume;
				recordPlayerController.audioSourceB.volume = volume2;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			float volume3 = recordPlayerController.newspaperSavedAWeight * recordPlayerController.masterVolume;
			recordPlayerController.audioSourceA.volume = volume3;
			float volume4 = recordPlayerController.newspaperSavedBWeight * recordPlayerController.masterVolume;
			recordPlayerController.audioSourceB.volume = volume4;
			recordPlayerController._crossfadePending = recordPlayerController.newspaperSavedCrossfade;
			recordPlayerController.FinishNewspaperOverride();
			object obj4 = recordPlayerController.newspaperSavedCrossfade & _003CrestoreBPlaying_003E5__3;
			object obj5 = _003CrestoreAPlaying_003E5__2 & obj4;
			if (obj5 != null)
			{
				AudioSource outgoing;
				AudioSource incoming;
				if (recordPlayerController.newspaperSavedUseAAsActive)
				{
					outgoing = recordPlayerController.audioSourceA;
					incoming = recordPlayerController.audioSourceB;
				}
				else
				{
					outgoing = recordPlayerController.audioSourceB;
					incoming = recordPlayerController.audioSourceA;
				}
				_003CResumeNewspaperCrossfade_003Ed__94 obj6 = new _003CResumeNewspaperCrossfade_003Ed__94(0);
				obj6._003C_003E1__state = 0;
				obj6._003C_003E4__this = recordPlayerController;
				obj6.outgoing = outgoing;
				obj6.incoming = incoming;
				Coroutine crossfadeRoutine = recordPlayerController.StartCoroutine(obj6);
				recordPlayerController._crossfadeRoutine = crossfadeRoutine;
			}
			goto IL_0675;
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

	private sealed class _003CResumeNewspaperCrossfade_003Ed__94 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		public AudioSource outgoing;

		public AudioSource incoming;

		private float _003CstartOutgoingWeight_003E5__2;

		private float _003CstartIncomingWeight_003E5__3;

		private float _003Celapsed_003E5__4;

		private float _003Cduration_003E5__5;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CResumeNewspaperCrossfade_003Ed__94(int _003C_003E1__state)
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
			//IL_0107: Expected I4, but got I8
			//IL_03f9: Expected I4, but got O
			//IL_0053: Expected O, but got I4
			//IL_0437: Expected F4, but got I
			//IL_0442: Unknown result type (might be due to invalid IL or missing references)
			//IL_0447: Expected O, but got Unknown
			//IL_05f5: Expected F4, but got O
			//IL_0615: Invalid comparison between I4 and F4
			//IL_006a: Expected O, but got I4
			//IL_04d8: Invalid comparison between I4 and F4
			//IL_00d1: Expected F4, but got I4
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Expected O, but got Unknown
			//IL_01a1: Expected F4, but got I4
			//IL_0532: Invalid comparison between I4 and F4
			//IL_01e7: Expected F4, but got I4
			//IL_0581: Invalid comparison between I4 and F4
			//IL_0223: Expected F4, but got I4
			RecordPlayerController recordPlayerController = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_03eb;
				}
				bool flag = recordPlayerController.newspaperSavedUseAAsActive;
				object obj = 292;
				if (!flag)
				{
					obj = 296;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v244 @ rax_v21+v37 @ rdi_v1 (RecordPlayerController)]");
				_003CstartOutgoingWeight_003E5__2 = 0f;
				object obj2 = _003C_003E4__this + 296;
				if (!recordPlayerController.newspaperSavedUseAAsActive)
				{
					obj2 = _003C_003E4__this + 292;
				}
				_003CstartIncomingWeight_003E5__3 = (float)obj2;
				float num = _003CstartOutgoingWeight_003E5__2;
				_003Celapsed_003E5__4 = 0f;
				if (!(0f > _003CstartOutgoingWeight_003E5__2))
				{
					if (num > 1f)
					{
						float num2 = 1f * recordPlayerController.fadeDuration;
						_003Cduration_003E5__5 = num2;
						goto IL_048f;
					}
				}
				else
				{
					num = 0f;
				}
				float num3 = num * recordPlayerController.fadeDuration;
				_003Cduration_003E5__5 = num3;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_04b2;
				}
				_003C_003E1__state = -1;
			}
			goto IL_048f;
			IL_048f:
			if (_003Cduration_003E5__5 > _003Celapsed_003E5__4)
			{
				float deltaTime = Time.deltaTime;
				float num4 = (_003Celapsed_003E5__4 = deltaTime + _003Celapsed_003E5__4);
				bool flag2 = !(0.0001f < _003Cduration_003E5__5);
				float num5 = 0.0001f;
				if (!flag2)
				{
					num5 = _003Cduration_003E5__5;
				}
				float num6 = num4 / num5;
				if (!(0f > num6))
				{
					if (num6 > 1f)
					{
						num6 = 1f;
					}
				}
				else
				{
					num6 = 0f;
				}
				float num7 = num6 + num6;
				float num8 = num6 * num6;
				float num9 = 3f - num7;
				float num10 = num9 * num8;
				float num11 = ((0f > num10) ? 0f : ((num10 > 1f) ? 1f : num10));
				float num12 = 0f - _003CstartOutgoingWeight_003E5__2;
				float num13 = num12 * num11;
				float num14 = num13 + _003CstartOutgoingWeight_003E5__2;
				if (!(0f > num10))
				{
					if (num10 > 1f)
					{
						num10 = 1f;
					}
				}
				else
				{
					num10 = 0f;
				}
				float num15 = 1f - _003CstartIncomingWeight_003E5__3;
				float num16 = num15 * num10;
				float num17 = num16 + _003CstartIncomingWeight_003E5__3;
				if ((object)_003C_003E4__this != null && (object)outgoing != null)
				{
					float volume = num14 * recordPlayerController.masterVolume;
					outgoing.volume = volume;
					if ((object)incoming != null)
					{
						float volume2 = num17 * recordPlayerController.masterVolume;
						incoming.volume = volume2;
						recordPlayerController._activeWeight = num14;
						recordPlayerController._inactiveWeight = num17;
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
				}
			}
			else if ((object)outgoing != null)
			{
				outgoing.Stop();
				if ((object)outgoing != null)
				{
					outgoing.volume = 0f;
					if ((object)_003C_003E4__this != null && (object)incoming != null)
					{
						incoming.volume = recordPlayerController.masterVolume;
						bool useAAsActive = incoming == recordPlayerController.audioSourceA;
						recordPlayerController._useAAsActive = useAAsActive;
						recordPlayerController._activeWeight = 1f;
						recordPlayerController._crossfadePending = false;
						recordPlayerController._crossfadeRoutine = null;
						goto IL_04b2;
					}
				}
			}
			goto IL_03eb;
			IL_04b2:
			return false;
			IL_03eb:
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

	private sealed class _003CRunNewspaperTransition_003Ed__87 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CRunNewspaperTransition_003Ed__87(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 20 Invalid \"Jump target not found in method: 0x180461B77\"");
			return (byte)_003C_003E1__state != 0;
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

	private sealed class _003CStartDelayRoutine_003Ed__80 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CStartDelayRoutine_003Ed__80(int _003C_003E1__state)
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
			//IL_00cb: Expected I4, but got I8
			//IL_0329: Expected F4, but got I
			//IL_010c: Expected O, but got I
			//IL_0229: Expected O, but got I4
			//IL_0377: Expected O, but got I
			//IL_0152: Expected O, but got I
			//IL_0255: Expected O, but got I
			//IL_0240: Expected O, but got I4
			//IL_0167: Expected O, but got I
			//IL_026a: Expected O, but got I
			//IL_017c: Expected O, but got I
			//IL_034b: Expected I4, but got O
			//IL_02c0: Expected F4, but got I
			//IL_02c0: Expected O, but got I
			//IL_02d6: Expected O, but got I
			//IL_01e1: Expected O, but got I
			UnityEngine.Object context = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+98]");
				if ((nint)0 != _003C_003E1__state)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					string text = $"{arg:F2}s (realtime).";
					string message = "[RecordPlayerController] Audio starts in " + text;
					Debug.Log(message, context);
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+4C]");
				WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(0f);
				_003C_003E2__current = waitForSecondsRealtime;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+A8]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+A0]");
					bool flag = (UnityEngine.Object)0 == null;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+98]");
						if ((nint)0 != (flag ? 1 : 0))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+A0]");
							object obj = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rcx_v15+20]");
							object obj2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+AC]");
							object obj3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+AC]");
							nint num = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rcx_v16+18]");
							if (num >= 0)
							{
								goto IL_033d;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rcx_v16+20+v334 @ rax_v22*8]");
							string text2;
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rcx_v16+20+v334 @ rax_v22*8]");
								text2 = ((UnityEngine.Object)0).name;
							}
							else
							{
								text2 = null;
							}
							string message2 = "[RecordPlayerController] Start delay complete — playing track 0: '" + text2 + "'.";
							Debug.Log(message2, context);
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+B1]");
						bool flag2 = (nint)0 != 0;
						object obj4 = 40;
						if (!flag2)
						{
							obj4 = 48;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+A0]");
						object obj5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v92 @ rax_v16+20]");
						object obj6 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+AC]");
						object obj7 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+AC]");
						nint num2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ rax_v17+18]");
						if (num2 >= 0)
						{
							goto IL_033d;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D5A4C0");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v452 @ rax_v15+v32 @ rbx_v1 (UnityEngine.Object)]");
						nint num3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rbx_v1 (UnityEngine.Object)+70]");
						((AudioSource)num3).volume = 0f;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v452 @ rax_v15+v32 @ rbx_v1 (UnityEngine.Object)]");
						((AudioSource)0).Play();
					}
				}
				_ = 0;
			}
			return false;
			IL_033d:
			IndexOutOfRangeException ex = new IndexOutOfRangeException();
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

	private sealed class _003CStopDelayRoutine_003Ed__81 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RecordPlayerController _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CStopDelayRoutine_003Ed__81(int _003C_003E1__state)
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
			//IL_00e4: Expected I4, but got I8
			//IL_016d: Expected I4, but got O
			RecordPlayerController recordPlayerController = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					if ((recordPlayerController.debugLogs ? 1 : 0) != _003C_003E1__state)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg = default(object);
						string text = $"{arg:F2}s (realtime).";
						string message = "[RecordPlayerController] Audio halts in " + text;
						Debug.Log(message, _003C_003E4__this);
					}
					WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(recordPlayerController.playbackStopDelaySeconds);
					_003C_003E2__current = waitForSecondsRealtime;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_015f;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_015f;
				}
				if (recordPlayerController.debugLogs)
				{
					Debug.Log("[RecordPlayerController] Stop delay complete.", _003C_003E4__this);
				}
				_003C_003E4__this.HaltAudioImmediate();
				recordPlayerController._stopDelayRoutine = null;
			}
			return false;
			IL_015f:
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

	private ItemSlot slot;

	private AudioSource audioSourceA;

	private AudioSource audioSourceB;

	private AudioSource newspaperAudioSource;

	private LookAtTarget playButton;

	private float buttonActivationDelaySeconds;

	private float playbackStartDelaySeconds;

	private float playbackStopDelaySeconds;

	private float overlapSeconds = 3f;

	private float fadeDuration = 2f;

	private float previousRecordFadeOutSeconds = 1f;

	private float newspaperStartDelaySeconds;

	private float newspaperMusicFadeInSeconds = 1f;

	private float newspaperMusicFadeOutSeconds = 1f;

	private float previousRecordFadeBackInSeconds = 1f;

	private float masterVolume = 1f;

	public UnityEvent OnPlaybackStarted;

	public UnityEvent OnPlaybackStopped;

	public UnityEvent OnRecordRemoved;

	public UnityEvent<int> OnTrackChanged;

	private bool debugLogs;

	private RecordItem _currentRecord;

	private bool _isPlaying;

	private int _trackIndex;

	private bool _isLastTrack;

	private bool _useAAsActive = true;

	private float _activeWeight = 1f;

	private float _inactiveWeight;

	private Coroutine _crossfadeRoutine;

	private bool _crossfadePending;

	private Coroutine _buttonActivationRoutine;

	private Coroutine _startDelayRoutine;

	private Coroutine _stopDelayRoutine;

	private float _savedActiveTime;

	private float _savedInactiveTime;

	private bool _wasPausedByTimeScale;

	private NewspaperPlaybackState newspaperPlaybackState;

	private Coroutine newspaperTransitionRoutine;

	private AudioClip newspaperClip;

	private RecordItem newspaperSavedRecord;

	private bool newspaperOverrideActive;

	private bool newspaperDismissRequested;

	private bool newspaperRestorePreviousMusic;

	private bool newspaperSavedWasPlaying;

	private bool newspaperSavedAWasPlaying;

	private bool newspaperSavedBWasPlaying;

	private bool newspaperSavedStartDelay;

	private bool newspaperSavedCrossfade;

	private bool newspaperSavedUseAAsActive;

	private float newspaperSavedATime;

	private float newspaperSavedBTime;

	private float newspaperSavedAWeight;

	private float newspaperSavedBWeight;

	private float newspaperWeight;

	public float MasterVolume => masterVolume;

	private AudioSource ActiveSource
	{
		get
		{
			if (_useAAsActive)
			{
				return audioSourceA;
			}
			return audioSourceB;
		}
	}

	private AudioSource InactiveSource
	{
		get
		{
			if (_useAAsActive)
			{
				return audioSourceB;
			}
			return audioSourceA;
		}
	}

	public void SetMasterVolume(float volume)
	{
		//IL_0009: Invalid comparison between I4 and F4
		float num = default(float);
		if (!(0f > num) && num > 1f)
		{
			masterVolume = 1f;
			ApplyMasterVolumeToSources();
		}
		else
		{
			masterVolume = 0f;
			ApplyMasterVolumeToSources();
		}
	}

	public void FadeOutForNewspaper()
	{
		if (newspaperPlaybackState == NewspaperPlaybackState.Idle)
		{
			BeginNewspaperOverride();
			newspaperPlaybackState = NewspaperPlaybackState.FadingRecordOut;
			StartNewspaperTransition();
		}
	}

	public void PlayNewspaperMusic(AudioClip clip, bool restorePreviousMusic)
	{
		//IL_0092: Invalid comparison between F4 and I4
		//IL_00a3: Invalid comparison between F4 and I4
		//IL_00b6: Expected O, but got I4
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected I4, but got Unknown
		if (this.newspaperPlaybackState != NewspaperPlaybackState.Idle)
		{
			if (this.newspaperPlaybackState != NewspaperPlaybackState.FadingRecordOut && this.newspaperPlaybackState != NewspaperPlaybackState.WaitingForCue)
			{
				return;
			}
		}
		else
		{
			BeginNewspaperOverride();
			this.newspaperPlaybackState = NewspaperPlaybackState.FadingRecordOut;
		}
		newspaperClip = clip;
		newspaperRestorePreviousMusic = restorePreviousMusic;
		if (this.newspaperPlaybackState == NewspaperPlaybackState.WaitingForCue && (bool)newspaperClip)
		{
			bool flag = newspaperStartDelaySeconds < 0f;
			bool flag2 = newspaperStartDelaySeconds == 0f;
			object obj = flag | flag2;
			NewspaperPlaybackState newspaperPlaybackState = (NewspaperPlaybackState)(obj + 3);
			this.newspaperPlaybackState = newspaperPlaybackState;
		}
		StartNewspaperTransition();
	}

	public void DismissNewspaperMusic()
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected I4, but got Unknown
		if (this.newspaperPlaybackState == NewspaperPlaybackState.Idle)
		{
			return;
		}
		bool flag = this.newspaperPlaybackState == NewspaperPlaybackState.WaitingForCue;
		newspaperDismissRequested = true;
		if (!flag)
		{
			if (this.newspaperPlaybackState == NewspaperPlaybackState.CuePlaying)
			{
				this.newspaperPlaybackState = NewspaperPlaybackState.FadingCueOut;
				StartNewspaperTransition();
				return;
			}
		}
		else
		{
			bool flag2 = CanRestoreNewspaperRecord();
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb ecx,ecx\"");
			NewspaperPlaybackState newspaperPlaybackState = (NewspaperPlaybackState)(this & 7);
			this.newspaperPlaybackState = newspaperPlaybackState;
		}
		StartNewspaperTransition();
	}

	private void Awake()
	{
		//IL_001d: Expected O, but got I4
		//IL_0069: Expected O, but got I4
		//IL_00c9: Expected O, but got I4
		//IL_0129: Expected O, but got I4
		bool flag = slot;
		object obj = 1;
		if (!flag)
		{
			string text = base.name;
			string message = "[RecordPlayerController] '" + text + "': 'slot' is not assigned. Controller will disable itself.";
			Debug.LogError(message, this);
			obj = 0;
		}
		if (!audioSourceA)
		{
			string text2 = base.name;
			string message2 = "[RecordPlayerController] '" + text2 + "': 'audioSourceA' is not assigned. Controller will disable itself.";
			Debug.LogError(message2, this);
			obj = 0;
		}
		if (!audioSourceB)
		{
			string text3 = base.name;
			string message3 = "[RecordPlayerController] '" + text3 + "': 'audioSourceB' is not assigned. Controller will disable itself.";
			Debug.LogError(message3, this);
			obj = 0;
		}
		if (!newspaperAudioSource)
		{
			GameObject gameObject = base.gameObject;
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();
			newspaperAudioSource = audioSource;
		}
		if (obj != null)
		{
			if (!playButton)
			{
				string text4 = base.name;
				string message4 = "[RecordPlayerController] '" + text4 + "': 'playButton' is not assigned. Play/stop button will not function.";
				Debug.LogWarning(message4, this);
			}
			audioSourceA.loop = false;
			audioSourceB.loop = false;
			if ((bool)newspaperAudioSource)
			{
				CopyNewspaperAudioSourceSettings();
				newspaperAudioSource.playOnAwake = false;
				newspaperAudioSource.loop = true;
				newspaperAudioSource.volume = 0f;
			}
			SetButtonActive(active: false);
			ApplyMasterVolumeToSources();
		}
		else
		{
			base.enabled = false;
		}
	}

	private void OnEnable()
	{
		if ((bool)slot)
		{
			ItemSlot itemSlot = slot;
			UnityAction call = HandleSlotFilled;
			itemSlot.onSlotFilled.AddListener(call);
			ItemSlot itemSlot2 = slot;
			UnityAction call2 = HandleSlotCleared;
			itemSlot2.onSlotCleared.AddListener(call2);
		}
		if ((bool)playButton)
		{
			LookAtTarget lookAtTarget = playButton;
			UnityAction call3 = TogglePlayStop;
			lookAtTarget.onClickDown.AddListener(call3);
		}
	}

	private void OnDisable()
	{
		if ((bool)slot)
		{
			ItemSlot itemSlot = slot;
			UnityAction call = HandleSlotFilled;
			itemSlot.onSlotFilled.RemoveListener(call);
			ItemSlot itemSlot2 = slot;
			UnityAction call2 = HandleSlotCleared;
			itemSlot2.onSlotCleared.RemoveListener(call2);
		}
		if ((bool)playButton)
		{
			LookAtTarget lookAtTarget = playButton;
			UnityAction call3 = TogglePlayStop;
			lookAtTarget.onClickDown.RemoveListener(call3);
		}
		if (newspaperTransitionRoutine != null)
		{
			StopCoroutine(newspaperTransitionRoutine);
			newspaperTransitionRoutine = null;
		}
		if ((bool)newspaperAudioSource)
		{
			newspaperAudioSource.Stop();
		}
	}

	private void Update()
	{
		//IL_0021: Invalid comparison between F4 and I4
		//IL_01f6: Expected O, but got I4
		//IL_0223: Expected O, but got I
		//IL_020d: Expected O, but got I4
		//IL_02aa: Invalid comparison between F4 and I4
		//IL_0325: Expected O, but got I4
		//IL_059e: Expected F4, but got I4
		//IL_0352: Expected O, but got I
		//IL_033c: Expected O, but got I4
		//IL_05b7: Expected F4, but got I4
		//IL_044d: Expected F4, but got I4
		//IL_039f: Expected O, but got I4
		//IL_03cc: Expected O, but got I
		//IL_045b: Invalid comparison between F4 and I4
		//IL_03ff: Expected O, but got I4
		//IL_03b6: Expected O, but got I4
		//IL_042c: Expected O, but got I
		//IL_0416: Expected O, but got I4
		if (newspaperOverrideActive)
		{
			return;
		}
		float timeScale = Time.timeScale;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018045A09Ch\"");
		if (timeScale == 0f)
		{
			if (_isPlaying && !_wasPausedByTimeScale)
			{
				_wasPausedByTimeScale = true;
				SavePlaybackPositions();
				audioSourceA.Pause();
				audioSourceB.Pause();
			}
			return;
		}
		if (_wasPausedByTimeScale)
		{
			bool flag = !_isPlaying;
			_wasPausedByTimeScale = false;
			if (flag)
			{
				return;
			}
			if (debugLogs)
			{
				Debug.Log("[RecordPlayerController] Unpaused — resuming from saved position.", this);
			}
			audioSourceA.UnPause();
			audioSourceB.UnPause();
		}
		if (!_isPlaying || _startDelayRoutine != null || _crossfadePending || _crossfadeRoutine != null || _stopDelayRoutine != null)
		{
			return;
		}
		bool flag2 = _useAAsActive;
		object obj = 40;
		if (!flag2)
		{
			obj = 48;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v503 @ rax_v8+this @ rcx (RecordPlayerController)]");
		bool crossfade;
		if (((AudioSource)0).isPlaying)
		{
			if (!(_currentRecord != null))
			{
				return;
			}
			RecordItem currentRecord = _currentRecord;
			if (!currentRecord.useCrossfade || !(overlapSeconds > 0f) || (!currentRecord.loop && _isLastTrack))
			{
				return;
			}
			bool flag3 = _useAAsActive;
			object obj2 = 40;
			if (!flag3)
			{
				obj2 = 48;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v504 @ rax_v31+this @ rcx (RecordPlayerController)]");
			AudioClip clip = ((AudioSource)0).clip;
			float num;
			if (clip != null)
			{
				bool flag4 = _useAAsActive;
				object obj3 = 40;
				if (!flag4)
				{
					obj3 = 48;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v505 @ rax_v37+this @ rcx (RecordPlayerController)]");
				AudioClip clip2 = ((AudioSource)0).clip;
				float length = clip2.length;
				bool flag5 = _useAAsActive;
				object obj4 = 40;
				if (!flag5)
				{
					obj4 = 48;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v481 @ rdi_v12+this @ rcx (RecordPlayerController)]");
				float time = ((AudioSource)0).time;
				num = length - time;
			}
			else
			{
				num = 0f;
			}
			if (overlapSeconds < num || !(num > 0f))
			{
				return;
			}
			_crossfadePending = true;
			crossfade = true;
		}
		else
		{
			if (_isLastTrack && _currentRecord != null)
			{
				RecordItem currentRecord2 = _currentRecord;
				if (!currentRecord2.loop)
				{
					if (debugLogs)
					{
						Debug.Log("[RecordPlayerController] Final track ended (non-looping). Stopping.", this);
					}
					_isPlaying = false;
					_crossfadePending = false;
					StopCrossfadeRoutine();
					audioSourceA.Stop();
					float volume = ((~(_useAAsActive ? 1u : 0u) != 0) ? 0f : masterVolume);
					audioSourceA.volume = volume;
					audioSourceB.Stop();
					float volume2 = 0f;
					if (!_useAAsActive)
					{
						volume2 = masterVolume;
					}
					audioSourceB.volume = volume2;
					if (OnPlaybackStopped != null)
					{
						OnPlaybackStopped.Invoke();
					}
					return;
				}
			}
			crossfade = false;
		}
		AdvanceTrack(crossfade);
	}

	private void SavePlaybackPositions()
	{
		//IL_008e: Expected F4, but got I4
		AudioSource audioSource;
		float savedActiveTime;
		if (audioSourceA.isPlaying)
		{
			audioSource = audioSourceA;
		}
		else
		{
			if (!audioSourceB.isPlaying)
			{
				savedActiveTime = 0f;
				goto IL_00c8;
			}
			audioSource = audioSourceB;
		}
		savedActiveTime = audioSource.time;
		goto IL_00c8;
		IL_00c8:
		_savedActiveTime = savedActiveTime;
		bool flag = !debugLogs;
		_savedInactiveTime = 0f;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string message = $"[RecordPlayerController] Paused — saved position: {arg:F3}s.";
			Debug.Log(message, this);
		}
	}

	private void RestorePlaybackPositions()
	{
		if (debugLogs)
		{
			Debug.Log("[RecordPlayerController] Unpaused — resuming from saved position.", this);
		}
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (newspaperOverrideActive || !_isPlaying)
		{
			return;
		}
		if (!pauseStatus)
		{
			audioSourceA.UnPause();
			audioSourceB.UnPause();
			if (debugLogs != pauseStatus)
			{
				Debug.Log("[RecordPlayerController] OnApplicationPause(false) — resumed audio.", this);
			}
			return;
		}
		SavePlaybackPositions();
		audioSourceA.Pause();
		audioSourceB.Pause();
		if (debugLogs)
		{
			Debug.Log("[RecordPlayerController] OnApplicationPause(true) — paused audio.", this);
		}
	}

	private unsafe void HandleSlotFilled()
	{
		//IL_0366: Invalid comparison between F4 and I4
		//IL_025a: Expected O, but got I4
		//IL_0267: Expected O, but got I4
		//IL_02cb: Expected O, but got I4
		//IL_02d8: Expected O, but got I4
		ItemSlot itemSlot = slot;
		if ((bool)itemSlot.CurrentItem)
		{
			ItemSlot itemSlot2 = slot;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			RecordItem recordItem = default(RecordItem);
			_currentRecord = recordItem;
			if (!_currentRecord)
			{
				ItemSlot itemSlot3 = slot;
				string text = itemSlot3.CurrentItem.name;
				string message = "[RecordPlayerController] '" + text + "' has no RecordItem. No audio will play.";
				Debug.LogWarning(message, this);
			}
			bool flag = !debugLogs;
			int num = 0;
			if (!flag)
			{
				string[] array = new string[8] { "[RecordPlayerController] Record placed: '", null, null, null, null, null, null, null };
				ItemSlot itemSlot4 = slot;
				string text2 = itemSlot4.CurrentItem.name;
				array[1] = text2;
				array[2] = "' — ";
				string text3;
				if ((bool)_currentRecord)
				{
					RecordItem currentRecord = _currentRecord;
					AudioClip[] tracks = currentRecord.tracks;
					text3 = num.ToString();
					num = tracks.Length;
				}
				else
				{
					num = 0;
					text3 = "0";
				}
				array[3] = text3;
				array[4] = " tracks, ";
				RecordItem currentRecord2 = _currentRecord;
				if ((object)_currentRecord != null)
				{
					bool? flag2 = (byte)(&recordItem) != 0;
					bool? flag3 = (bool?)(object)0;
					recordItem = (RecordItem)currentRecord2.loop;
				}
				else
				{
					bool? flag2 = default(bool?);
					bool? flag3 = flag2;
				}
				object obj = default(object);
				object arg = (bool?)obj;
				string text4 = $"loop={arg}, ";
				array[5] = text4;
				RecordItem currentRecord3 = _currentRecord;
				if ((object)_currentRecord != null)
				{
					bool? flag3 = (byte)(&recordItem) != 0;
					flag3 = (bool?)(object)0;
					recordItem = (RecordItem)currentRecord3.useCrossfade;
				}
				object arg2 = (bool?)recordItem;
				string text5 = $"crossfade={arg2}, ";
				array[6] = text5;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg3 = default(object);
				string text6 = $"buttonDelay={arg3:F2}s.";
				array[7] = text6;
				string message2 = string.Concat(array);
				Debug.Log(message2, this);
			}
			CancelButtonActivation();
			if (!(buttonActivationDelaySeconds > 0f))
			{
				SetButtonActive(active: true);
				return;
			}
			_003CButtonActivationRoutine_003Ed__79 obj2 = new _003CButtonActivationRoutine_003Ed__79(0);
			obj2._003C_003E1__state = 0;
			obj2._003C_003E4__this = this;
			Coroutine buttonActivationRoutine = StartCoroutine(obj2);
			_buttonActivationRoutine = buttonActivationRoutine;
		}
		else if (debugLogs)
		{
			Debug.Log("[RecordPlayerController] SlotFilled — CurrentItem is null.", this);
		}
	}

	private void HandleSlotCleared()
	{
		newspaperRestorePreviousMusic = false;
		ForceStop();
		if (!_isPlaying)
		{
			if (~(debugLogs ? 1u : 0u) == 0)
			{
				Debug.Log("[RecordPlayerController] Record removed (not playing).", this);
			}
		}
		else
		{
			if (~(debugLogs ? 1u : 0u) == 0)
			{
				Debug.Log("[RecordPlayerController] Record removed while playing — OnRecordRemoved.", this);
			}
			if (OnRecordRemoved != null)
			{
				OnRecordRemoved.Invoke();
			}
		}
		_currentRecord = null;
		SetButtonActive(active: false);
	}

	public void TogglePlayStop()
	{
		//IL_010c: Invalid comparison between F4 and I4
		if (newspaperOverrideActive)
		{
			return;
		}
		if (!_isPlaying)
		{
			StartPlayback();
		}
		else if (_isPlaying)
		{
			CancelStartDelay();
			_isPlaying = false;
			_wasPausedByTimeScale = false;
			if (OnPlaybackStopped != null)
			{
				OnPlaybackStopped.Invoke();
			}
			if (debugLogs)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string text = $"Stop delay: {arg:F2}s.";
				string message = "[RecordPlayerController] Stop pressed. " + text;
				Debug.Log(message, this);
			}
			if (!(playbackStopDelaySeconds > 0f))
			{
				HaltAudioImmediate();
				return;
			}
			_003CStopDelayRoutine_003Ed__81 obj = new _003CStopDelayRoutine_003Ed__81(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine stopDelayRoutine = StartCoroutine(obj);
			_stopDelayRoutine = stopDelayRoutine;
		}
	}

	private void StartPlayback()
	{
		//IL_02a8: Invalid comparison between F4 and I4
		//IL_02d9: Expected O, but got I4
		//IL_02f0: Expected O, but got I4
		//IL_0337: Expected O, but got I
		//IL_034d: Expected O, but got I
		if (_isPlaying)
		{
			return;
		}
		if (_currentRecord != null)
		{
			RecordItem currentRecord = _currentRecord;
			if (currentRecord.tracks != null)
			{
				AudioClip[] tracks = currentRecord.tracks;
				if (tracks.Length != 0)
				{
					CancelStopDelay();
					_isPlaying = true;
					_trackIndex = 0;
					_useAAsActive = true;
					_crossfadePending = false;
					_wasPausedByTimeScale = false;
					_activeWeight = 1f;
					audioSourceA.Stop();
					audioSourceA.volume = masterVolume;
					audioSourceB.Stop();
					audioSourceB.volume = 0f;
					UpdateIsLastTrack();
					if (OnPlaybackStarted != null)
					{
						OnPlaybackStarted.Invoke();
					}
					if (debugLogs)
					{
						string[] array = new string[5] { "[RecordPlayerController] Play pressed. ", null, null, null, null };
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg = default(object);
						string text = $"Start delay: {arg:F2}s. ";
						array[1] = text;
						array[2] = "First track: '";
						RecordItem currentRecord2 = _currentRecord;
						AudioClip[] tracks2 = currentRecord2.tracks;
						string text2 = (((object)tracks2[0] == null) ? null : tracks2[0].name);
						array[3] = text2;
						array[4] = "'.";
						string message = string.Concat(array);
						Debug.Log(message, this);
					}
					if (!(playbackStartDelaySeconds > 0f))
					{
						bool flag = _useAAsActive;
						object obj = 40;
						if (!flag)
						{
							obj = 48;
						}
						RecordItem currentRecord3 = _currentRecord;
						AudioClip[] tracks3 = currentRecord3.tracks;
						int trackIndex = _trackIndex;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D5A4C0");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v583 @ rax_v38+this @ rcx (RecordPlayerController)]");
						((AudioSource)0).volume = masterVolume;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v583 @ rax_v38+this @ rcx (RecordPlayerController)]");
						((AudioSource)0).Play();
					}
					else
					{
						_003CStartDelayRoutine_003Ed__80 obj2 = new _003CStartDelayRoutine_003Ed__80(0);
						obj2._003C_003E1__state = 0;
						obj2._003C_003E4__this = this;
						Coroutine startDelayRoutine = StartCoroutine(obj2);
						_startDelayRoutine = startDelayRoutine;
					}
					return;
				}
			}
			if (debugLogs)
			{
				Debug.Log("[RecordPlayerController] StartPlayback — record has no tracks.", this);
			}
		}
		else if (debugLogs)
		{
			Debug.Log("[RecordPlayerController] StartPlayback — no record in slot.", this);
		}
	}

	private void StopPlayback()
	{
		//IL_00c1: Invalid comparison between F4 and I4
		if (_isPlaying)
		{
			CancelStartDelay();
			_isPlaying = false;
			_wasPausedByTimeScale = false;
			if (OnPlaybackStopped != null)
			{
				OnPlaybackStopped.Invoke();
			}
			if (debugLogs)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string text = $"Stop delay: {arg:F2}s.";
				string message = "[RecordPlayerController] Stop pressed. " + text;
				Debug.Log(message, this);
			}
			if (!(playbackStopDelaySeconds > 0f))
			{
				HaltAudioImmediate();
				return;
			}
			_003CStopDelayRoutine_003Ed__81 obj = new _003CStopDelayRoutine_003Ed__81(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine stopDelayRoutine = StartCoroutine(obj);
			_stopDelayRoutine = stopDelayRoutine;
		}
	}

	public void ForceStop()
	{
		_isPlaying = false;
		_trackIndex = 0;
		_isLastTrack = false;
		_crossfadePending = false;
		_wasPausedByTimeScale = false;
		_activeWeight = 1f;
		CancelButtonActivation();
		CancelStartDelay();
		CancelStopDelay();
		if (_crossfadeRoutine != null)
		{
			StopCoroutine(_crossfadeRoutine);
			_crossfadeRoutine = null;
		}
		if ((bool)audioSourceA)
		{
			audioSourceA.Stop();
			audioSourceA.volume = masterVolume;
		}
		if ((bool)audioSourceB)
		{
			audioSourceB.Stop();
			audioSourceB.volume = 0f;
		}
		if (OnPlaybackStopped != null)
		{
			OnPlaybackStopped.Invoke();
		}
	}

	private void HaltAudioImmediate()
	{
		//IL_008e: Expected F4, but got I4
		//IL_00a7: Expected F4, but got I4
		if (_crossfadeRoutine != null)
		{
			StopCoroutine(_crossfadeRoutine);
			_crossfadeRoutine = null;
		}
		_crossfadePending = false;
		_activeWeight = 1f;
		audioSourceA.Stop();
		float volume = ((~(_useAAsActive ? 1u : 0u) != 0) ? 0f : masterVolume);
		audioSourceA.volume = volume;
		audioSourceB.Stop();
		float volume2 = 0f;
		if (!_useAAsActive)
		{
			volume2 = masterVolume;
		}
		audioSourceB.volume = volume2;
	}

	private IEnumerator ButtonActivationRoutine()
	{
		_003CButtonActivationRoutine_003Ed__79 obj = new _003CButtonActivationRoutine_003Ed__79(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator StartDelayRoutine()
	{
		_003CStartDelayRoutine_003Ed__80 obj = new _003CStartDelayRoutine_003Ed__80(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator StopDelayRoutine()
	{
		_003CStopDelayRoutine_003Ed__81 obj = new _003CStopDelayRoutine_003Ed__81(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void CancelButtonActivation()
	{
		if (_buttonActivationRoutine != null)
		{
			StopCoroutine(_buttonActivationRoutine);
			_buttonActivationRoutine = null;
			if (debugLogs)
			{
				Debug.Log("[RecordPlayerController] Button activation cancelled.", this);
			}
		}
	}

	private void CancelStartDelay()
	{
		if (_startDelayRoutine != null)
		{
			StopCoroutine(_startDelayRoutine);
			_startDelayRoutine = null;
			if (debugLogs)
			{
				Debug.Log("[RecordPlayerController] Start delay cancelled.", this);
			}
		}
	}

	private void CancelStopDelay()
	{
		if (_stopDelayRoutine != null)
		{
			StopCoroutine(_stopDelayRoutine);
			_stopDelayRoutine = null;
			if (debugLogs)
			{
				Debug.Log("[RecordPlayerController] Stop delay cancelled.", this);
			}
		}
	}

	private void BeginNewspaperOverride()
	{
		//IL_04af: Invalid comparison between F4 and I4
		//IL_00ac: Invalid comparison between F4 and I4
		//IL_00bb: Invalid comparison between F4 and I4
		//IL_0201: Invalid comparison between I4 and F4
		//IL_04db: Invalid comparison between F4 and I4
		//IL_024c: Expected F4, but got I4
		//IL_02b7: Invalid comparison between I4 and F4
		//IL_0302: Expected F4, but got I4
		//IL_018d: Invalid comparison between F4 and I4
		//IL_019c: Invalid comparison between F4 and I4
		newspaperOverrideActive = true;
		newspaperRestorePreviousMusic = false;
		newspaperClip = null;
		newspaperSavedRecord = _currentRecord;
		newspaperWeight = 0f;
		bool flag = (nint)_startDelayRoutine < 0;
		bool flag2 = _startDelayRoutine == null;
		newspaperSavedWasPlaying = _isPlaying;
		bool flag3 = !flag;
		bool flag4 = !flag2;
		bool flag5 = flag4 & flag3;
		bool flag6 = (nint)_crossfadeRoutine < 0;
		bool flag7 = _crossfadeRoutine == null;
		newspaperSavedStartDelay = flag5;
		bool flag8 = !flag6;
		bool flag9 = !flag7;
		bool flag10 = flag9 & flag8;
		newspaperSavedCrossfade = flag10;
		newspaperSavedUseAAsActive = _useAAsActive;
		bool flag11;
		if (audioSourceA.isPlaying)
		{
			flag11 = true;
		}
		else
		{
			if (_wasPausedByTimeScale)
			{
				AudioClip clip = audioSourceA.clip;
				if ((bool)clip)
				{
					float volume = audioSourceA.volume;
					bool flag12 = volume < 0f;
					bool flag13 = volume == 0f;
					bool flag14 = !flag12;
					bool flag15 = !flag13;
					flag11 = flag15 & flag14;
					goto IL_048b;
				}
			}
			flag11 = false;
		}
		goto IL_048b;
		IL_049a:
		bool flag16;
		newspaperSavedBWasPlaying = flag16;
		float num;
		if (!(masterVolume > 0f))
		{
			num = ((!_useAAsActive) ? _inactiveWeight : _activeWeight);
		}
		else
		{
			float volume2 = audioSourceA.volume;
			num = volume2 / masterVolume;
			if (!(0f > num))
			{
				if (num > 1f)
				{
					num = 1f;
				}
			}
			else
			{
				num = 0f;
			}
		}
		newspaperSavedAWeight = num;
		float num2;
		if (!(masterVolume > 0f))
		{
			num2 = ((!_useAAsActive) ? _activeWeight : _inactiveWeight);
		}
		else
		{
			float volume3 = audioSourceB.volume;
			num2 = volume3 / masterVolume;
			if (!(0f > num2))
			{
				if (num2 > 1f)
				{
					num2 = 1f;
				}
			}
			else
			{
				num2 = 0f;
			}
		}
		newspaperSavedBWeight = num2;
		CancelStartDelay();
		CancelStopDelay();
		if (_crossfadeRoutine != null)
		{
			StopCoroutine(_crossfadeRoutine);
			_crossfadeRoutine = null;
		}
		bool flag17 = !debugLogs;
		_crossfadePending = false;
		if (!flag17)
		{
			Debug.Log("[RecordPlayerController] Newspaper override started.", this);
		}
		return;
		IL_048b:
		newspaperSavedAWasPlaying = flag11;
		bool isPlaying = audioSourceB.isPlaying;
		flag16 = true;
		if (!isPlaying)
		{
			if (_wasPausedByTimeScale != isPlaying)
			{
				AudioClip clip2 = audioSourceB.clip;
				if ((bool)clip2)
				{
					float volume4 = audioSourceB.volume;
					bool flag18 = volume4 < 0f;
					bool flag19 = volume4 == 0f;
					bool flag20 = !flag18;
					bool flag21 = !flag19;
					flag16 = flag21 & flag20;
					goto IL_049a;
				}
			}
			flag16 = false;
		}
		goto IL_049a;
	}

	private void StartNewspaperTransition()
	{
		if (newspaperTransitionRoutine == null)
		{
			_003CRunNewspaperTransition_003Ed__87 obj = new _003CRunNewspaperTransition_003Ed__87(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine coroutine = StartCoroutine(obj);
			newspaperTransitionRoutine = coroutine;
		}
	}

	private IEnumerator RunNewspaperTransition()
	{
		_003CRunNewspaperTransition_003Ed__87 obj = new _003CRunNewspaperTransition_003Ed__87(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator FadeRecordOutForNewspaper()
	{
		_003CFadeRecordOutForNewspaper_003Ed__88 obj = new _003CFadeRecordOutForNewspaper_003Ed__88(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator NewspaperStartDelayRoutine()
	{
		_003CNewspaperStartDelayRoutine_003Ed__89 obj = new _003CNewspaperStartDelayRoutine_003Ed__89(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator FadeNewspaperCueIn()
	{
		_003CFadeNewspaperCueIn_003Ed__90 obj = new _003CFadeNewspaperCueIn_003Ed__90(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator FadeNewspaperCueOut()
	{
		_003CFadeNewspaperCueOut_003Ed__91 obj = new _003CFadeNewspaperCueOut_003Ed__91(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator RestoreRecordAfterNewspaper()
	{
		_003CRestoreRecordAfterNewspaper_003Ed__92 obj = new _003CRestoreRecordAfterNewspaper_003Ed__92(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void RestoreNewspaperSource(AudioSource source, float savedTime, bool shouldPlay)
	{
		//IL_0075: Invalid comparison between I4 and F4
		//IL_0087: Expected F4, but got I4
		//IL_013e: Invalid comparison between I4 and F4
		//IL_00df: Expected F4, but got I4
		if (!shouldPlay)
		{
			return;
		}
		AudioClip clip = source.clip;
		if (!clip)
		{
			return;
		}
		AudioClip clip2 = source.clip;
		float length = clip2.length;
		float num = length - 0.01f;
		bool flag = !(0f < num);
		float num2 = 0f;
		if (!flag)
		{
			num2 = num;
		}
		float time;
		if (!(0f > savedTime))
		{
			bool flag2 = !(savedTime > num2);
			time = savedTime;
			if (!flag2)
			{
				time = num2;
			}
		}
		else
		{
			time = 0f;
		}
		source.time = time;
		if (!_wasPausedByTimeScale)
		{
			source.UnPause();
			if (!source.isPlaying)
			{
				source.Play();
			}
		}
	}

	private IEnumerator ResumeNewspaperCrossfade(AudioSource outgoing, AudioSource incoming)
	{
		_003CResumeNewspaperCrossfade_003Ed__94 obj = new _003CResumeNewspaperCrossfade_003Ed__94(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.outgoing = outgoing;
		obj.incoming = incoming;
		return obj;
	}

	private bool CanRestoreNewspaperRecord()
	{
		if (newspaperRestorePreviousMusic && newspaperSavedWasPlaying && (bool)newspaperSavedRecord)
		{
			return newspaperSavedRecord == _currentRecord;
		}
		return false;
	}

	private void DiscardSavedRecordPlayback()
	{
		audioSourceA.Stop();
		audioSourceB.Stop();
		_isPlaying = false;
		_crossfadePending = false;
		_wasPausedByTimeScale = false;
		_activeWeight = 1f;
		if (~(_isPlaying ? 1u : 0u) == 0 && OnPlaybackStopped != null)
		{
			OnPlaybackStopped.Invoke();
		}
	}

	private void FinishNewspaperOverride()
	{
		if ((bool)newspaperAudioSource)
		{
			newspaperAudioSource.Stop();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D5A4C0");
			newspaperAudioSource.volume = 0f;
		}
		newspaperPlaybackState = NewspaperPlaybackState.Idle;
		newspaperOverrideActive = false;
		newspaperRestorePreviousMusic = false;
		newspaperSavedRecord = null;
		newspaperClip = null;
		newspaperWeight = 0f;
		if (debugLogs)
		{
			Debug.Log("[RecordPlayerController] Newspaper override finished.", this);
		}
	}

	private void CopyNewspaperAudioSourceSettings()
	{
		AudioMixerGroup outputAudioMixerGroup = audioSourceA.outputAudioMixerGroup;
		newspaperAudioSource.outputAudioMixerGroup = outputAudioMixerGroup;
		bool mute = audioSourceA.mute;
		newspaperAudioSource.mute = mute;
		bool bypassEffects = audioSourceA.bypassEffects;
		newspaperAudioSource.bypassEffects = bypassEffects;
		bool bypassListenerEffects = audioSourceA.bypassListenerEffects;
		newspaperAudioSource.bypassListenerEffects = bypassListenerEffects;
		bool bypassReverbZones = audioSourceA.bypassReverbZones;
		newspaperAudioSource.bypassReverbZones = bypassReverbZones;
		int priority = audioSourceA.priority;
		newspaperAudioSource.priority = priority;
		float pitch = audioSourceA.pitch;
		newspaperAudioSource.pitch = pitch;
		float panStereo = audioSourceA.panStereo;
		newspaperAudioSource.panStereo = panStereo;
		float spatialBlend = audioSourceA.spatialBlend;
		newspaperAudioSource.spatialBlend = spatialBlend;
		float reverbZoneMix = audioSourceA.reverbZoneMix;
		newspaperAudioSource.reverbZoneMix = reverbZoneMix;
		float dopplerLevel = audioSourceA.dopplerLevel;
		newspaperAudioSource.dopplerLevel = dopplerLevel;
		float spread = audioSourceA.spread;
		newspaperAudioSource.spread = spread;
		AudioRolloffMode rolloffMode = audioSourceA.rolloffMode;
		newspaperAudioSource.rolloffMode = rolloffMode;
		float minDistance = audioSourceA.minDistance;
		newspaperAudioSource.minDistance = minDistance;
		float maxDistance = audioSourceA.maxDistance;
		newspaperAudioSource.maxDistance = maxDistance;
		bool spatialize = audioSourceA.spatialize;
		newspaperAudioSource.spatialize = spatialize;
		bool spatializePostEffects = audioSourceA.spatializePostEffects;
		newspaperAudioSource.spatializePostEffects = spatializePostEffects;
	}

	private unsafe void AdvanceTrack(bool crossfade)
	{
		//IL_0090: Expected O, but got I4
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected I4, but got Unknown
		//IL_00b5: Expected I, but got O
		//IL_0145: Expected I, but got O
		//IL_0315: Expected O, but got I4
		//IL_0342: Expected O, but got I
		//IL_037c: Expected O, but got I4
		//IL_022d: Invalid comparison between F4 and I4
		//IL_03a9: Expected O, but got I
		//IL_03c6: Expected O, but got I4
		//IL_032c: Expected O, but got I4
		//IL_03f8: Expected O, but got I
		//IL_0415: Expected O, but got I4
		//IL_0393: Expected O, but got I4
		//IL_0267: Expected O, but got I4
		//IL_0452: Expected O, but got I
		//IL_03dd: Expected O, but got I4
		//IL_050a: Expected O, but got I4
		//IL_0468: Expected O, but got I
		//IL_042c: Expected O, but got I4
		//IL_027e: Expected O, but got I4
		//IL_028c: Expected O, but got I4
		//IL_02bc: Expected O, but got I
		//IL_02d6: Expected O, but got I
		if (_currentRecord != null)
		{
			RecordItem currentRecord = _currentRecord;
			if (currentRecord.tracks != null)
			{
				AudioClip[] tracks = currentRecord.tracks;
				if (tracks.Length != 0)
				{
					object obj = _trackIndex + 1;
					int trackIndex = obj % tracks.Length;
					bool flag = !debugLogs;
					nint num = unchecked((nint)null);
					bool flag2 = default(bool);
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg = default(object);
						object arg2 = default(object);
						string text = $"[RecordPlayerController] Track {arg} → {arg2} ";
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg3 = default(object);
						string text2 = $"(crossfade={arg3}).";
						string message = text + text2;
						Debug.Log(message, this);
						flag2 = crossfade;
						num = unchecked((nint)null);
					}
					_trackIndex = trackIndex;
					UpdateIsLastTrack();
					bool flag3 = OnTrackChanged == null;
					int num2 = (crossfade ? 1 : 0);
					if (!flag3)
					{
						OnTrackChanged.Invoke((int)(&flag2));
						num2 = _trackIndex;
						num = 0;
					}
					RecordItem currentRecord2 = _currentRecord;
					AudioClip[] tracks2 = currentRecord2.tracks;
					int trackIndex2 = _trackIndex;
					if ((bool)tracks2[trackIndex2])
					{
						if (crossfade && fadeDuration > 0f)
						{
							StopCrossfadeRoutine();
							bool flag4 = _useAAsActive;
							object obj2 = 40;
							if (!flag4)
							{
								obj2 = 48;
							}
							bool flag5 = _useAAsActive;
							object obj3 = 48;
							if (!flag5)
							{
								obj3 = 40;
							}
							_003CCrossfadeRoutine_003Ed__100 obj4 = new _003CCrossfadeRoutine_003Ed__100(0);
							obj4._003C_003E1__state = 0;
							obj4._003C_003E4__this = this;
							obj4.incomingClip = tracks2[trackIndex2];
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v592 @ rax_v45+this @ rcx (RecordPlayerController)]");
							obj4.outgoing = (AudioSource)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v620 @ rbx_v11+this @ rcx (RecordPlayerController)]");
							obj4.incoming = (AudioSource)0;
							Coroutine crossfadeRoutine = StartCoroutine(obj4);
							_crossfadeRoutine = crossfadeRoutine;
							return;
						}
						bool flag6 = _useAAsActive;
						object obj5 = 40;
						if (!flag6)
						{
							obj5 = 48;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v168 @ rax_v28+this @ rcx (RecordPlayerController)]");
						((AudioSource)0).Stop();
						bool useAAsActive = !_useAAsActive;
						_activeWeight = 1f;
						_useAAsActive = useAAsActive;
						bool flag7 = (byte)(~(_useAAsActive ? 1u : 0u)) != 0;
						object obj6 = 48;
						if (!flag7)
						{
							obj6 = 40;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ rax_v32+this @ rcx (RecordPlayerController)]");
						((AudioSource)0).Stop();
						bool flag8 = _useAAsActive;
						object obj7 = 48;
						if (!flag8)
						{
							obj7 = 40;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v170 @ rax_v35+this @ rcx (RecordPlayerController)]");
						((AudioSource)0).volume = 0f;
						bool flag9 = _useAAsActive;
						object obj8 = 40;
						if (!flag9)
						{
							obj8 = 48;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D5A4C0");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v150 @ rsi_v6+this @ rcx (RecordPlayerController)]");
						((AudioSource)0).volume = masterVolume;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v150 @ rsi_v6+this @ rcx (RecordPlayerController)]");
						((AudioSource)0).Play();
						_crossfadePending = false;
					}
					else
					{
						if (debugLogs)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object arg4 = default(object);
							string message2 = $"[RecordPlayerController] Track {arg4} is null — skipping.";
							Debug.LogWarning(message2, this);
						}
						_crossfadePending = false;
					}
					return;
				}
			}
		}
		ForceStop();
	}

	private IEnumerator CrossfadeRoutine(AudioClip incomingClip, AudioSource outgoing, AudioSource incoming)
	{
		_003CCrossfadeRoutine_003Ed__100 obj = new _003CCrossfadeRoutine_003Ed__100(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.incomingClip = incomingClip;
		obj.outgoing = outgoing;
		obj.incoming = incoming;
		return obj;
	}

	private void StopCrossfadeRoutine()
	{
		if (_crossfadeRoutine != null)
		{
			StopCoroutine(_crossfadeRoutine);
			_crossfadeRoutine = null;
		}
	}

	private void ApplyMasterVolumeToSources()
	{
		//IL_004f: Expected O, but got I4
		//IL_00ec: Expected O, but got I4
		//IL_0066: Expected O, but got I4
		//IL_0118: Expected F4, but got I
		//IL_0103: Expected O, but got I4
		AudioSource audioSource;
		float num;
		if (!newspaperOverrideActive)
		{
			if ((bool)audioSourceA)
			{
				bool flag = _useAAsActive;
				object obj = 180;
				if (!flag)
				{
					obj = 184;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ rax_v17+this @ rcx (RecordPlayerController)]");
				float volume = 0f * masterVolume;
				audioSourceA.volume = volume;
			}
			if (!audioSourceB)
			{
				return;
			}
			audioSource = audioSourceB;
			bool flag2 = _useAAsActive;
			object obj2 = 184;
			if (!flag2)
			{
				obj2 = 180;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v236 @ rdi_v8+this @ rcx (RecordPlayerController)]");
			num = 0f;
		}
		else
		{
			if (!newspaperAudioSource)
			{
				return;
			}
			audioSource = newspaperAudioSource;
			num = newspaperWeight;
		}
		float volume2 = num * masterVolume;
		audioSource.volume = volume2;
	}

	private void UpdateIsLastTrack()
	{
		//IL_007c: Expected O, but got I4
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		if (_currentRecord != null)
		{
			RecordItem currentRecord = _currentRecord;
			if (currentRecord.tracks != null)
			{
				AudioClip[] tracks = currentRecord.tracks;
				object obj = tracks.Length - 1;
				object obj2 = _trackIndex - obj;
				bool isLastTrack = obj2 == null;
				_isLastTrack = isLastTrack;
				return;
			}
		}
		_isLastTrack = false;
	}

	private void PlayClipOnSource(AudioSource source, AudioClip clip, float weight)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D5A4C0");
		float volume = weight * masterVolume;
		source.volume = volume;
		source.Play();
	}

	private void StopSourceImmediate(AudioSource source, bool isActive)
	{
		//IL_003a: Expected F4, but got I4
		source.Stop();
		float volume = ((!isActive) ? 0f : masterVolume);
		source.volume = volume;
	}

	private void SetButtonActive(bool active)
	{
		if ((bool)playButton)
		{
			playButton.SetActive(active);
		}
	}

	private static float SmoothStep01(float t)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_005c: Expected F4, but got I4
		float num;
		if (!(0f > t))
		{
			bool flag = !(t > 1f);
			num = t;
			if (!flag)
			{
				num = 1f;
			}
		}
		else
		{
			num = 0f;
		}
		float num2 = num + num;
		float num3 = num * num;
		float num4 = 3f - num2;
		return num4 * num3;
	}
}
