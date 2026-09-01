using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using TMPro;
using TMPSelection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NotepadSection : MonoBehaviour
{
	public enum WriteMode
	{
		Add,
		Replace
	}

	public enum AddPosition
	{
		Top,
		Bottom
	}

	public enum TextRevealMode
	{
		Instant,
		Typewriter
	}

	private struct PendingWrite
	{
		public string content;

		public WriteMode mode;

		public AddPosition addPos;

		public float delay;

		public TextRevealMode revealMode;

		public float typewriterSecondsPerCharacter;
	}

	private sealed class _003CApplyWriteTypewriterRoutine_003Ed__47 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NotepadSection _003C_003E4__this;

		public PendingWrite pw;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CApplyWriteTypewriterRoutine_003Ed__47(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_00a1: Expected I4, but got I8
			//IL_0487: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_006e: Expected I4, but got I8
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_012a: Expected O, but got I
			//IL_01b2: Invalid comparison between F4 and I4
			//IL_0162: Expected O, but got I
			//IL_044d: Expected O, but got Ref
			//IL_017f: Expected O, but got I
			//IL_03c3: Expected O, but got I
			//IL_03d4: Expected O, but got I
			//IL_0312: Expected O, but got I
			//IL_0322: Expected O, but got I
			//IL_040d: Expected F4, but got I
			//IL_0364: Expected F4, but got I
			//IL_02a8: Expected F4, but got I
			NotepadSection notepadSection = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag && (nint)obj2 != 1)
					{
						goto IL_046b;
					}
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_0452;
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					if (!(notepadSection.targetText != null))
					{
						goto IL_046b;
					}
					if ((object)notepadSection.targetText != null)
					{
						string text = notepadSection.targetText.text;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
						object obj3 = 0;
						bool flag2 = text != null;
						string text2 = text;
						if (!flag2)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v129 @ rcx_v11+B8]");
							object obj4 = 0;
							text2 = (string)obj4;
						}
						PendingWrite pendingWrite = pw;
						if ((object)pw == null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v129 @ rcx_v11+B8]");
							object obj5 = 0;
							pendingWrite = (PendingWrite)obj5;
							if (obj5 == null)
							{
								goto IL_0479;
							}
						}
						if (pendingWrite.delay != 0f)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (NotepadSection+<ApplyWriteTypewriterRoutine>d__47)+40]");
							if ((nint)0 < (nint)0)
							{
								bool flag3 = string.IsNullOrEmpty(text2);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (NotepadSection+<ApplyWriteTypewriterRoutine>d__47)+30]");
								if ((nint)0 != 1)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (NotepadSection+<ApplyWriteTypewriterRoutine>d__47)+34]");
									if ((nint)0 != 0)
									{
										if (!flag3)
										{
											string text3 = text2 + "\n";
											text2 = text3;
										}
										_003CRevealTypewriterAddBottomScaled_003Ed__50 obj6 = new _003CRevealTypewriterAddBottomScaled_003Ed__50(0);
										obj6._003C_003E1__state = 0;
										obj6._003C_003E4__this = _003C_003E4__this;
										obj6.newEntry = (string)pendingWrite;
										obj6.existingPrefix = text2;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (NotepadSection+<ApplyWriteTypewriterRoutine>d__47)+40]");
										obj6.secondsPerChar = 0f;
										obj6.maxCharsPerFrame = notepadSection.maxTypewriterCharactersPerFrame;
										_003C_003E2__current = obj6;
										_003C_003E1__state = 3;
									}
									else
									{
										string existingSuffix;
										if (!flag3)
										{
											string text4 = "\n" + text2;
											existingSuffix = text4;
										}
										else
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
											object obj7 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v491 @ rax_v39+B8]");
											object obj8 = 0;
											existingSuffix = (string)obj8;
										}
										_003CRevealTypewriterAddTopScaled_003Ed__49 obj9 = new _003CRevealTypewriterAddTopScaled_003Ed__49(0);
										obj9._003C_003E1__state = 0;
										obj9._003C_003E4__this = _003C_003E4__this;
										obj9.newEntry = (string)pendingWrite;
										obj9.existingSuffix = existingSuffix;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (NotepadSection+<ApplyWriteTypewriterRoutine>d__47)+40]");
										obj9.secondsPerChar = 0f;
										obj9.maxCharsPerFrame = notepadSection.maxTypewriterCharactersPerFrame;
										_003C_003E2__current = obj9;
										_003C_003E1__state = 2;
									}
								}
								else
								{
									if ((object)notepadSection.targetText == null)
									{
										goto IL_0479;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
									object obj10 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v444 @ rax_v19+B8]");
									object text5 = 0;
									notepadSection.targetText.text = (string)text5;
									_003CRevealTypewriterReplaceScaled_003Ed__48 obj11 = new _003CRevealTypewriterReplaceScaled_003Ed__48(0);
									obj11._003C_003E1__state = 0;
									obj11._003C_003E4__this = _003C_003E4__this;
									obj11.fullContent = (string)pendingWrite;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (NotepadSection+<ApplyWriteTypewriterRoutine>d__47)+40]");
									obj11.secondsPerChar = 0f;
									obj11.maxCharsPerFrame = notepadSection.maxTypewriterCharactersPerFrame;
									_003C_003E2__current = obj11;
									_003C_003E1__state = 1;
								}
								return true;
							}
						}
						object obj12 = default(object);
						_003C_003E4__this.ApplyWriteInstant((PendingWrite)(&obj12));
						goto IL_0452;
					}
				}
			}
			goto IL_0479;
			IL_046b:
			return false;
			IL_0479:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0452:
			_003C_003E4__this.SafeInvoke(notepadSection.onWriteCompleted);
			goto IL_046b;
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

	private sealed class _003CProcessQueue_003Ed__45 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NotepadSection _003C_003E4__this;

		private PendingWrite _003Cnext_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CProcessQueue_003Ed__45(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_008b: Expected I4, but got I8
			//IL_02b8: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_0077: Expected I4, but got I8
			//IL_0052: Expected I4, but got I8
			//IL_005d: Expected O, but got I4
			//IL_01c9: Expected O, but got Ref
			//IL_01ed: Expected O, but got I4
			//IL_0252: Expected F4, but got I
			NotepadSection notepadSection = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (flag)
				{
					_003C_003E1__state = -1;
					goto IL_015f;
				}
				if ((nint)obj != 1)
				{
					goto IL_029c;
				}
				_003C_003E1__state = -1;
				_003Cnext_003E5__2 = (PendingWrite)0;
				_ = 0;
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_02aa;
				}
				notepadSection._isProcessing = true;
			}
			goto IL_02e1;
			IL_02aa:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_015f:
			if ((object)_003C_003E4__this == null)
			{
				goto IL_02aa;
			}
			_003C_003E4__this.SafeInvoke(notepadSection.onWriteStarted);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (NotepadSection+<ProcessQueue>d__45)+3C]");
			PendingWrite pendingWrite = default(PendingWrite);
			if ((nint)0 == 0)
			{
				_003C_003E4__this.ApplyWriteInstant((PendingWrite)(&pendingWrite));
				_003C_003E4__this.SafeInvoke(notepadSection.onWriteCompleted);
				_003Cnext_003E5__2 = (PendingWrite)0;
				_ = 0;
				goto IL_02e1;
			}
			_003CApplyWriteTypewriterRoutine_003Ed__47 obj2 = new _003CApplyWriteTypewriterRoutine_003Ed__47(0);
			obj2._003C_003E1__state = 0;
			obj2._003C_003E4__this = _003C_003E4__this;
			obj2.pw = _003Cnext_003E5__2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (NotepadSection+<ProcessQueue>d__45)+38]");
			_ = 0;
			_003C_003E2__current = obj2;
			_003C_003E1__state = 2;
			return true;
			IL_02e1:
			if ((object)_003C_003E4__this != null)
			{
				Queue<PendingWrite> pendingWrites = notepadSection._pendingWrites;
				if (notepadSection._pendingWrites != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v93 @ rax_v4 (System.Collections.Generic.Queue`1<NotepadSection+PendingWrite>)+20]");
					if ((nint)0 > (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808DFE00");
						_003Cnext_003E5__2 = pendingWrite;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (NotepadSection+<ProcessQueue>d__45)+38]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (NotepadSection+<ProcessQueue>d__45)+38]");
						if ((nint)0 <= (nint)0)
						{
							goto IL_015f;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (NotepadSection+<ProcessQueue>d__45)+38]");
						WaitForSeconds waitForSeconds = new WaitForSeconds(0f);
						_003C_003E2__current = waitForSeconds;
						_003C_003E1__state = 1;
						return true;
					}
					notepadSection._isProcessing = false;
					notepadSection._processCoroutine = null;
					goto IL_029c;
				}
			}
			goto IL_02aa;
			IL_029c:
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

	private sealed class _003CRevealTypewriterAddBottomScaled_003Ed__50 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public string newEntry;

		public float secondsPerChar;

		public NotepadSection _003C_003E4__this;

		public string existingPrefix;

		public int maxCharsPerFrame;

		private int _003Clength_003E5__2;

		private string _003CstaticPart_003E5__3;

		private float _003Celapsed_003E5__4;

		private int _003Cshown_003E5__5;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CRevealTypewriterAddBottomScaled_003Ed__50(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0045: Expected I4, but got I8
			//IL_01ec: Expected I4, but got I8
			//IL_036a: Expected I4, but got O
			//IL_007e: Invalid comparison between I4 and F4
			//IL_00f6: Expected O, but got I
			//IL_0106: Expected O, but got I
			NotepadSection notepadSection = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				string text = newEntry;
				_003C_003E1__state = -1;
				if (newEntry != null)
				{
					_003Clength_003E5__2 = text._stringLength;
					if (0f < secondsPerChar && text._stringLength > 0)
					{
						string text2 = existingPrefix;
						if (existingPrefix == null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
							object obj = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v319 @ rax_v23+B8]");
							object obj2 = 0;
							text2 = (string)obj2;
						}
						_003CstaticPart_003E5__3 = text2;
						_003Celapsed_003E5__4 = 0f;
						if ((object)_003C_003E4__this != null && (object)notepadSection.targetText != null)
						{
							notepadSection.targetText.text = _003CstaticPart_003E5__3;
							goto IL_039e;
						}
					}
					else if ((object)_003C_003E4__this != null)
					{
						string text3 = existingPrefix + newEntry;
						if ((object)notepadSection.targetText != null)
						{
							notepadSection.targetText.text = text3;
							goto IL_03c1;
						}
					}
				}
				goto IL_035c;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				goto IL_039e;
			}
			goto IL_03c1;
			IL_035c:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_042e:
			_003C_003E2__current = null;
			_003C_003E1__state = 1;
			return true;
			IL_039e:
			if (_003Cshown_003E5__5 >= _003Clength_003E5__2)
			{
				goto IL_03c1;
			}
			float deltaTime = Time.deltaTime;
			float num = deltaTime + _003Celapsed_003E5__4;
			_003Celapsed_003E5__4 = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si ecx,xmm0\"");
			int num2;
			if (0 >= 0)
			{
				bool flag = 0 <= _003Clength_003E5__2;
				num2 = 0;
				if (!flag)
				{
					num2 = _003Clength_003E5__2;
				}
			}
			else
			{
				num2 = 0;
			}
			int num3 = num2 - _003Cshown_003E5__5;
			if (num3 > 0)
			{
				if (maxCharsPerFrame > 0 && num3 >= maxCharsPerFrame)
				{
					num3 = maxCharsPerFrame;
				}
				int length = (_003Cshown_003E5__5 += num3);
				if ((object)_003C_003E4__this != null && newEntry != null)
				{
					string text4 = newEntry.Substring(0, length);
					string text5 = _003CstaticPart_003E5__3 + text4;
					if ((object)notepadSection.targetText != null)
					{
						notepadSection.targetText.text = text5;
						goto IL_042e;
					}
				}
				goto IL_035c;
			}
			goto IL_042e;
			IL_03c1:
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

	private sealed class _003CRevealTypewriterAddTopScaled_003Ed__49 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public string newEntry;

		public float secondsPerChar;

		public NotepadSection _003C_003E4__this;

		public string existingSuffix;

		public int maxCharsPerFrame;

		private int _003Clength_003E5__2;

		private float _003Celapsed_003E5__3;

		private int _003Cshown_003E5__4;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CRevealTypewriterAddTopScaled_003Ed__49(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0045: Expected I4, but got I8
			//IL_01bd: Expected I4, but got I8
			//IL_033b: Expected I4, but got O
			//IL_007e: Invalid comparison between I4 and F4
			NotepadSection notepadSection = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				string text = newEntry;
				_003C_003E1__state = -1;
				if (newEntry != null)
				{
					_003Clength_003E5__2 = text._stringLength;
					if (0f < secondsPerChar && text._stringLength > 0)
					{
						_003Celapsed_003E5__3 = 0f;
						if ((object)_003C_003E4__this != null && (object)notepadSection.targetText != null)
						{
							notepadSection.targetText.text = existingSuffix;
							goto IL_033b;
						}
					}
					else if ((object)_003C_003E4__this != null)
					{
						string text2 = newEntry + existingSuffix;
						if ((object)notepadSection.targetText != null)
						{
							notepadSection.targetText.text = text2;
							goto IL_035e;
						}
					}
				}
				goto IL_032d;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				goto IL_033b;
			}
			goto IL_035e;
			IL_032d:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_03cb:
			_003C_003E2__current = null;
			_003C_003E1__state = 1;
			return true;
			IL_033b:
			if (_003Cshown_003E5__4 >= _003Clength_003E5__2)
			{
				goto IL_035e;
			}
			float deltaTime = Time.deltaTime;
			float num = deltaTime + _003Celapsed_003E5__3;
			_003Celapsed_003E5__3 = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si ecx,xmm0\"");
			int num2;
			if (0 >= 0)
			{
				bool flag = 0 <= _003Clength_003E5__2;
				num2 = 0;
				if (!flag)
				{
					num2 = _003Clength_003E5__2;
				}
			}
			else
			{
				num2 = 0;
			}
			int num3 = num2 - _003Cshown_003E5__4;
			if (num3 > 0)
			{
				if (maxCharsPerFrame > 0 && num3 >= maxCharsPerFrame)
				{
					num3 = maxCharsPerFrame;
				}
				int length = (_003Cshown_003E5__4 += num3);
				if ((object)_003C_003E4__this != null && newEntry != null)
				{
					string text3 = newEntry.Substring(0, length);
					string text4 = text3 + existingSuffix;
					if ((object)notepadSection.targetText != null)
					{
						notepadSection.targetText.text = text4;
						goto IL_03cb;
					}
				}
				goto IL_032d;
			}
			goto IL_03cb;
			IL_035e:
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

	private sealed class _003CRevealTypewriterReplaceScaled_003Ed__48 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public string fullContent;

		public float secondsPerChar;

		public NotepadSection _003C_003E4__this;

		public int maxCharsPerFrame;

		private int _003Clength_003E5__2;

		private float _003Celapsed_003E5__3;

		private int _003Cshown_003E5__4;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CRevealTypewriterReplaceScaled_003Ed__48(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0045: Expected I4, but got I8
			//IL_01c8: Expected I4, but got I8
			//IL_032e: Expected I4, but got O
			//IL_007e: Invalid comparison between I4 and F4
			//IL_0116: Expected O, but got I
			//IL_0126: Expected O, but got I
			NotepadSection notepadSection = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				string text = fullContent;
				_003C_003E1__state = -1;
				if (fullContent != null)
				{
					_003Clength_003E5__2 = text._stringLength;
					if (0f < secondsPerChar && text._stringLength > 0)
					{
						_003Celapsed_003E5__3 = 0f;
						if ((object)_003C_003E4__this != null && (object)notepadSection.targetText != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
							object obj = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v300 @ rax_v18+B8]");
							object text2 = 0;
							notepadSection.targetText.text = (string)text2;
							goto IL_032e;
						}
					}
					else if ((object)_003C_003E4__this != null && (object)notepadSection.targetText != null)
					{
						notepadSection.targetText.text = fullContent;
						goto IL_0351;
					}
				}
				goto IL_0320;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				goto IL_032e;
			}
			goto IL_0351;
			IL_0320:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_03be:
			_003C_003E2__current = null;
			_003C_003E1__state = 1;
			return true;
			IL_032e:
			if (_003Cshown_003E5__4 >= _003Clength_003E5__2)
			{
				goto IL_0351;
			}
			float deltaTime = Time.deltaTime;
			float num = deltaTime + _003Celapsed_003E5__3;
			_003Celapsed_003E5__3 = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si ecx,xmm0\"");
			int num2;
			if (0 >= 0)
			{
				bool flag = 0 <= _003Clength_003E5__2;
				num2 = 0;
				if (!flag)
				{
					num2 = _003Clength_003E5__2;
				}
			}
			else
			{
				num2 = 0;
			}
			int num3 = num2 - _003Cshown_003E5__4;
			if (num3 > 0)
			{
				if (maxCharsPerFrame > 0 && num3 >= maxCharsPerFrame)
				{
					num3 = maxCharsPerFrame;
				}
				int length = (_003Cshown_003E5__4 += num3);
				if ((object)_003C_003E4__this != null && fullContent != null)
				{
					string text3 = fullContent.Substring(0, length);
					if ((object)notepadSection.targetText != null)
					{
						notepadSection.targetText.text = text3;
						goto IL_03be;
					}
				}
				goto IL_0320;
			}
			goto IL_03be;
			IL_0351:
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

	private TMP_Text targetText;

	private WriteMode defaultWriteMode;

	private AddPosition defaultAddPosition;

	private float writeDelaySeconds;

	private bool defaultDelayOnlyFirstNote;

	private bool cancelPendingWritesOnClear = true;

	private TextRevealMode defaultRevealMode;

	private float defaultTypewriterSecondsPerCharacter = 0.06f;

	private int maxTypewriterCharactersPerFrame;

	private bool preserveVisualSeparationWhenDeletingLayoutLines = true;

	private bool stripTags;

	public UnityEvent onFirstNoteAdded;

	public UnityEvent onAnyNoteAdded;

	public UnityEvent onWriteStarted;

	public UnityEvent onWriteCompleted;

	public UnityEvent onCleared;

	private static readonly List<NotepadSection> s_AllSections;

	private bool _hasAddedFirstNote;

	private readonly Queue<PendingWrite> _pendingWrites;

	private Coroutine _processCoroutine;

	private bool _isProcessing;

	public TMP_Text TargetText => targetText;

	public string UnityTag
	{
		get
		{
			GameObject gameObject = base.gameObject;
			if ((object)gameObject != null)
			{
				return gameObject.tag;
			}
			return (string)(object)new NullReferenceException();
		}
	}

	public bool HasAddedFirstNote => _hasAddedFirstNote;

	public bool HasPendingWrites
	{
		get
		{
			//IL_00ad: Expected I4, but got O
			//IL_0022: Expected O, but got I
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			Queue<PendingWrite> pendingWrites = _pendingWrites;
			if (_pendingWrites != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (System.Collections.Generic.Queue`1<NotepadSection+PendingWrite>)+20]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (System.Collections.Generic.Queue`1<NotepadSection+PendingWrite>)+20]");
				object obj = num ^ 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (System.Collections.Generic.Queue`1<NotepadSection+PendingWrite>)+20]");
				object obj2 = 0 & obj;
				bool flag = (nint)obj2 < 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (System.Collections.Generic.Queue`1<NotepadSection+PendingWrite>)+20]");
				bool flag2 = (nint)0 < (nint)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (System.Collections.Generic.Queue`1<NotepadSection+PendingWrite>)+20]");
				bool flag3 = (nint)0 == 0;
				bool flag4 = flag2 == flag;
				bool flag5 = !flag3;
				return flag5 & flag4;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private void Awake()
	{
		//IL_00b5: Invalid comparison between I4 and F4
		//IL_010c: Invalid comparison between I4 and F4
		if (targetText == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			TMP_Text tMP_Text = default(TMP_Text);
			targetText = tMP_Text;
		}
		if (targetText == null)
		{
			string text = base.name;
			string message = text + ": NotepadSection has no TMP_Text target; writes will be ignored.";
			Debug.LogWarning(message, this);
		}
		if (0f > writeDelaySeconds)
		{
			writeDelaySeconds = 0f;
		}
		if (0f > defaultTypewriterSecondsPerCharacter)
		{
			defaultTypewriterSecondsPerCharacter = 0f;
		}
		if (maxTypewriterCharactersPerFrame < 0)
		{
			maxTypewriterCharactersPerFrame = 0;
		}
	}

	private void OnEnable()
	{
		if (!s_AllSections.Contains(this))
		{
			s_AllSections.Add(this);
		}
		UnityAction<Scene, LoadSceneMode> value = OnSceneLoaded;
		SceneManager.sceneLoaded += value;
	}

	private void OnDisable()
	{
		bool flag = s_AllSections.Remove(this);
		UnityAction<Scene, LoadSceneMode> value = OnSceneLoaded;
		SceneManager.sceneLoaded -= value;
		if (_processCoroutine != null)
		{
			StopCoroutine(_processCoroutine);
		}
		_pendingWrites.Clear();
		_isProcessing = false;
		_processCoroutine = null;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
	}

	public void Write(string content)
	{
		float delaySeconds = default(float);
		TextRevealMode revealMode = default(TextRevealMode);
		float typewriterSecondsPerCharacter = default(float);
		Write(content, defaultWriteMode, defaultAddPosition, delaySeconds, revealMode, typewriterSecondsPerCharacter);
	}

	public void Write(string content, WriteMode mode, AddPosition addPosition)
	{
		float delaySeconds = default(float);
		TextRevealMode revealMode = default(TextRevealMode);
		float typewriterSecondsPerCharacter = default(float);
		Write(content, mode, addPosition, delaySeconds, revealMode, typewriterSecondsPerCharacter);
	}

	public void Write(string content, WriteMode mode, AddPosition addPosition, float delaySeconds)
	{
		float delaySeconds2 = default(float);
		TextRevealMode revealMode = default(TextRevealMode);
		float typewriterSecondsPerCharacter = default(float);
		Write(content, mode, addPosition, delaySeconds2, revealMode, typewriterSecondsPerCharacter);
	}

	public void Write(string content, WriteMode mode, AddPosition addPosition, float delaySeconds, TextRevealMode revealMode)
	{
		float delaySeconds2 = default(float);
		TextRevealMode revealMode2 = default(TextRevealMode);
		float typewriterSecondsPerCharacter = default(float);
		Write(content, mode, addPosition, delaySeconds2, revealMode2, typewriterSecondsPerCharacter);
	}

	public unsafe void Write(string content, WriteMode mode, AddPosition addPosition, float delaySeconds, TextRevealMode revealMode, float typewriterSecondsPerCharacter)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0367: Expected O, but got I4
		//IL_0481: Expected O, but got I4
		//IL_00b8: Expected O, but got I
		//IL_00cd: Expected O, but got I
		//IL_02c6: Expected O, but got Ref
		//IL_0182: Expected O, but got I4
		//IL_022d: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		if (!(targetText != null) || content == null)
		{
			return;
		}
		bool flag = string.IsNullOrEmpty(content);
		if (flag)
		{
			return;
		}
		bool flag2 = stripTags == flag;
		string text = content;
		if (!flag2)
		{
			string text2 = StripTags(content);
			text = text2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+67]");
		bool flag3 = (nint)0 >= (nint)0;
		object obj3 = 0;
		if (!flag3)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+67]");
			obj3 = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+77]");
		bool flag4 = (nint)0 >= (nint)0;
		object obj4 = 0;
		if (!flag4)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+77]");
			obj4 = 0;
		}
		string value = (((object)targetText == null) ? null : targetText.text);
		bool flag5 = string.IsNullOrEmpty(value);
		bool flag6 = _hasAddedFirstNote;
		bool flag7 = false;
		if (!flag6)
		{
			flag7 = flag5;
		}
		if (flag7)
		{
			_hasAddedFirstNote = true;
			SafeInvoke(onFirstNoteAdded);
		}
		SafeInvoke(onAnyNoteAdded);
		if (defaultDelayOnlyFirstNote && !flag7)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj5 = default(object);
			if (obj5 != null)
			{
				obj3 = 0;
			}
		}
		_ = 0;
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+6F]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp near ptr 000000018044B17Ch\"");
		object obj7 = default(object);
		if (obj3 == null)
		{
			object obj6 = obj7 >> 32;
			if (obj6 == null && !_isProcessing)
			{
				Queue<PendingWrite> pendingWrites = _pendingWrites;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v495 @ rax_v34 (System.Collections.Generic.Queue`1<NotepadSection+PendingWrite>)+20]");
				if ((nint)0 == 0)
				{
					SafeInvoke(onWriteStarted);
					PendingWrite pw = (PendingWrite)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
					ApplyWriteInstant(pw);
					SafeInvoke(onWriteCompleted);
					return;
				}
			}
		}
		object obj8 = obj7 >> 32;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm9,8\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm7,8\"");
		object obj9 = (object)text >> 32;
		object obj10 = obj7 >> 32;
		PendingWrite item = (PendingWrite)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-69]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-59]");
		_ = 0;
		_pendingWrites.Enqueue(item);
		if (!_isProcessing)
		{
			_003CProcessQueue_003Ed__45 obj11 = new _003CProcessQueue_003Ed__45(0);
			obj11._003C_003E1__state = 0;
			obj11._003C_003E4__this = this;
			Coroutine processCoroutine = StartCoroutine(obj11);
			_processCoroutine = processCoroutine;
		}
	}

	public void Clear()
	{
		//IL_003e: Expected O, but got I
		//IL_004e: Expected O, but got I
		if (targetText != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v96 @ rax_v9+B8]");
			object text = 0;
			targetText.text = (string)text;
		}
		bool flag = !cancelPendingWritesOnClear;
		_hasAddedFirstNote = false;
		if (!flag)
		{
			CancelPendingWritesInternal();
		}
		SafeInvoke(onCleared);
	}

	public void CancelPendingWrites()
	{
		CancelPendingWritesInternal();
	}

	private void EnsureProcessorRunning()
	{
		if (!_isProcessing)
		{
			_003CProcessQueue_003Ed__45 obj = new _003CProcessQueue_003Ed__45(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine processCoroutine = StartCoroutine(obj);
			_processCoroutine = processCoroutine;
		}
	}

	private IEnumerator ProcessQueue()
	{
		_003CProcessQueue_003Ed__45 obj = new _003CProcessQueue_003Ed__45(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void ApplyWriteInstant(PendingWrite pw)
	{
		//IL_006a: Expected O, but got I
		//IL_007a: Expected O, but got I
		//IL_017a: Expected I, but got O
		//IL_018a: Expected O, but got I
		//IL_019a: Expected O, but got I
		//IL_00a7: Expected O, but got I4
		string text2;
		while (true)
		{
			if (!(targetText != null))
			{
				return;
			}
			string text = targetText.text;
			bool flag = text != null;
			text2 = text;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v192 @ rax_v17+B8]");
				object obj2 = 0;
				text2 = (string)obj2;
			}
			if (pw.mode == WriteMode.Replace)
			{
				TMP_Text tMP_Text = targetText;
				string content = pw.content;
				nint num = (nint)tMP_Text;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ r8_v4 (Il2CppClass<TMPro.TMP_Text>)+558]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ r8_v4 (Il2CppClass<TMPro.TMP_Text>)+560]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v175 @ rax_v10 (should have been resolved before IL gen)");
				continue;
			}
			break;
		}
		bool flag2 = string.IsNullOrEmpty(text2);
		object obj5 = (int)pw.mode >> 32;
		string text4;
		if (obj5 != null)
		{
			if (flag2)
			{
				goto IL_00fa;
			}
			string text3 = text2 + "\n" + pw.content;
			text4 = text3;
		}
		else
		{
			if (flag2)
			{
				goto IL_00fa;
			}
			string text5 = pw.content + "\n" + text2;
			text4 = text5;
		}
		goto IL_010c;
		IL_00fa:
		text4 = pw.content;
		goto IL_010c;
		IL_010c:
		targetText.text = text4;
	}

	private IEnumerator ApplyWriteTypewriterRoutine(PendingWrite pw)
	{
		_003CApplyWriteTypewriterRoutine_003Ed__47 obj = new _003CApplyWriteTypewriterRoutine_003Ed__47(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.pw = (PendingWrite)pw.content;
		_ = pw.delay;
		return obj;
	}

	private IEnumerator RevealTypewriterReplaceScaled(string fullContent, float secondsPerChar, int maxCharsPerFrame)
	{
		_003CRevealTypewriterReplaceScaled_003Ed__48 obj = new _003CRevealTypewriterReplaceScaled_003Ed__48(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.fullContent = fullContent;
		obj.secondsPerChar = secondsPerChar;
		obj.maxCharsPerFrame = maxCharsPerFrame;
		return obj;
	}

	private IEnumerator RevealTypewriterAddTopScaled(string newEntry, string existingSuffix, float secondsPerChar, int maxCharsPerFrame)
	{
		_003CRevealTypewriterAddTopScaled_003Ed__49 obj = new _003CRevealTypewriterAddTopScaled_003Ed__49(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.newEntry = newEntry;
		obj.existingSuffix = existingSuffix;
		int maxCharsPerFrame2 = default(int);
		obj.maxCharsPerFrame = maxCharsPerFrame2;
		obj.secondsPerChar = secondsPerChar;
		return obj;
	}

	private IEnumerator RevealTypewriterAddBottomScaled(string newEntry, string existingPrefix, float secondsPerChar, int maxCharsPerFrame)
	{
		_003CRevealTypewriterAddBottomScaled_003Ed__50 obj = new _003CRevealTypewriterAddBottomScaled_003Ed__50(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.newEntry = newEntry;
		obj.existingPrefix = existingPrefix;
		int maxCharsPerFrame2 = default(int);
		obj.maxCharsPerFrame = maxCharsPerFrame2;
		obj.secondsPerChar = secondsPerChar;
		return obj;
	}

	private void CancelPendingWritesInternal()
	{
		if (_processCoroutine != null)
		{
			StopCoroutine(_processCoroutine);
		}
		_pendingWrites.Clear();
		_isProcessing = false;
		_processCoroutine = null;
	}

	private void SafeInvoke(UnityEvent evt)
	{
		evt?.Invoke();
	}

	public unsafe bool RemoveLayoutLineRange(int minLayoutLineIndex, int maxLayoutLineIndex, TMP_Text tmpForLayout = null)
	{
		//IL_0008: Expected O, but got Ref
		//IL_05c7: Expected I4, but got O
		//IL_010b: Expected O, but got I
		//IL_011b: Expected O, but got I
		//IL_02c8: Expected O, but got I4
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Expected O, but got Unknown
		//IL_0301: Expected I4, but got I8
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Expected O, but got Unknown
		//IL_043b: Expected O, but got I
		//IL_049b: Expected O, but got I
		//IL_051f: Expected O, but got I
		//IL_04fa: Expected O, but got I
		//IL_04c8: Expected O, but got I
		//IL_04d8: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		int num6;
		int num8;
		if (targetText != null)
		{
			bool flag = tmpForLayout == null;
			TMP_Text tMP_Text = tmpForLayout;
			if (flag)
			{
				tMP_Text = targetText;
			}
			if (tMP_Text != null)
			{
				if (tMP_Text == targetText)
				{
					if ((object)targetText == null)
					{
						goto IL_05b9;
					}
					string text = targetText.text;
					bool flag2 = text != null;
					string value = text;
					if (!flag2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
						object obj3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v523 @ rax_v53+B8]");
						object obj4 = 0;
						value = (string)obj4;
					}
					if (!string.IsNullOrEmpty(value))
					{
						if ((object)tMP_Text == null)
						{
							goto IL_05b9;
						}
						tMP_Text.ForceMeshUpdate();
						TMP_TextInfo textInfo = tMP_Text.textInfo;
						if (textInfo != null && textInfo.lineCount > 0 && textInfo.characterCount > 0)
						{
							int num2;
							if (minLayoutLineIndex >= 0)
							{
								int num = textInfo.lineCount - 1;
								bool flag3 = minLayoutLineIndex <= num;
								num2 = minLayoutLineIndex;
								if (!flag3)
								{
									num2 = num;
								}
							}
							else
							{
								num2 = 0;
							}
							int num4;
							if (maxLayoutLineIndex >= 0)
							{
								int num3 = textInfo.lineCount - 1;
								bool flag4 = maxLayoutLineIndex <= num3;
								num4 = maxLayoutLineIndex;
								if (!flag4)
								{
									num4 = num3;
								}
							}
							else
							{
								num4 = 0;
							}
							bool flag5 = num4 < num2;
							int num5 = num2;
							if (!flag5)
							{
								num5 = num4;
							}
							if (num4 >= num2)
							{
								num4 = num2;
							}
							if (num4 <= num5)
							{
								TMP_LineInfo[] lineInfo = textInfo.lineInfo;
								object obj5 = num4 * 2;
								object obj6 = num4 + obj5;
								object obj7 = obj6 << 5;
								num6 = 2147483647;
								int num7 = num4;
								num8 = -2147483648;
								while (textInfo.lineInfo != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v455 @ rdx_v18+38+v122 @ r8_v12 (TMPro.TMP_LineInfo[])]");
									if ((nint)0 >= (nint)0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v455 @ rdx_v18+40+v122 @ r8_v12 (TMPro.TMP_LineInfo[])]");
										if ((nint)0 >= (nint)0)
										{
											int num9 = num6;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v455 @ rdx_v18+38+v122 @ r8_v12 (TMPro.TMP_LineInfo[])]");
											if ((nint)num9 >= (nint)0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v455 @ rdx_v18+38+v122 @ r8_v12 (TMPro.TMP_LineInfo[])]");
												num6 = 0;
											}
											int num10 = num8;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v455 @ rdx_v18+40+v122 @ r8_v12 (TMPro.TMP_LineInfo[])]");
											if ((nint)num10 <= (nint)0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v455 @ rdx_v18+40+v122 @ r8_v12 (TMPro.TMP_LineInfo[])]");
												num8 = 0;
											}
										}
									}
									num4++;
									num7++;
									obj7 += 96;
									if (num7 <= num5)
									{
										continue;
									}
									goto IL_039e;
								}
								goto IL_05b9;
							}
						}
					}
				}
				else
				{
					string text2 = base.name;
					string message = text2 + ": RemoveLayoutLineRange was called with a TMP_Text that is not this section's targetText.\nThis is not supported because TMP layout indices won't match the displayed content.";
					Debug.LogWarning(message, this);
				}
			}
		}
		goto IL_059b;
		IL_05b9:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_059b:
		return false;
		IL_039e:
		bool preserveVisualSeparation = default(bool);
		if (num6 == 2147483647 || num8 == 2147483648L || !TMP_RichTextSelectionUtility.RemoveRichTextByPlainRange(ref System.Runtime.CompilerServices.Unsafe.As<object, string>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 95)), num6, num8, trimOuterNewlines: true, preserveVisualSeparation))
		{
			goto IL_059b;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+5F]");
		string text3 = (string)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+5F]");
		if ((nint)0 != 0)
		{
			if (text3._stringLength < 10)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+5F]");
				if (((string)0).Contains("\n"))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v754 @ rax_v45+B8]");
					object obj9 = 0;
					text3 = (string)obj9;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+5F]");
					text3 = (string)0;
				}
			}
			if ((object)targetText != null)
			{
				targetText.text = text3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+5F]");
				if (string.IsNullOrEmpty((string)0))
				{
					_hasAddedFirstNote = false;
					SafeInvoke(onCleared);
				}
				return true;
			}
		}
		goto IL_05b9;
	}

	public static NotepadSection ResolveByTag(string unityTag)
	{
		//IL_001d: Expected O, but got I4
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		if (!string.IsNullOrWhiteSpace(unityTag))
		{
			List<NotepadSection> list = s_AllSections;
			bool flag = (nint)s_AllSections < 0;
			if (s_AllSections != null)
			{
				object obj = list._size - 1;
				if (flag)
				{
					goto IL_011c;
				}
				UnityEngine.Object obj2 = default(UnityEngine.Object);
				while (s_AllSections != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					bool flag2 = obj2 == null;
					bool flag3 = (flag2 ? 1 : 0) < (false ? 1 : 0);
					if (!flag2)
					{
						if ((object)obj2 == null)
						{
							break;
						}
						GameObject gameObject = ((Component)obj2).gameObject;
						if ((object)gameObject == null)
						{
							break;
						}
						string a = gameObject.tag;
						bool flag4 = string.Equals(a, unityTag, StringComparison.Ordinal);
						flag3 = (flag4 ? 1 : 0) < (false ? 1 : 0);
						if (flag4)
						{
							return (NotepadSection)obj2;
						}
					}
					obj--;
					if (!flag3)
					{
						continue;
					}
					goto IL_011c;
				}
			}
			return (NotepadSection)(object)new NullReferenceException();
		}
		goto IL_011c;
		IL_011c:
		return null;
	}

	private static string StripTags(string s)
	{
		//IL_003c: Expected O, but got I4
		//IL_0059: Expected O, but got I
		//IL_0069: Expected O, but got I
		//IL_00ac: Expected O, but got I
		//IL_00bc: Expected O, but got I
		//IL_00ff: Expected O, but got I
		//IL_010f: Expected O, but got I
		//IL_0152: Expected O, but got I
		//IL_0162: Expected O, but got I
		//IL_01a5: Expected O, but got I
		//IL_01b5: Expected O, but got I
		//IL_01f8: Expected O, but got I
		//IL_0208: Expected O, but got I
		//IL_024b: Expected O, but got I
		//IL_025b: Expected O, but got I
		//IL_029e: Expected O, but got I
		//IL_02ae: Expected O, but got I
		//IL_02f1: Expected O, but got I
		//IL_0301: Expected O, but got I
		//IL_0344: Expected O, but got I
		//IL_0354: Expected O, but got I
		//IL_0397: Expected O, but got I
		//IL_03a7: Expected O, but got I
		//IL_03ea: Expected O, but got I
		//IL_03fa: Expected O, but got I
		//IL_043d: Expected O, but got I
		//IL_044d: Expected O, but got I
		//IL_0490: Expected O, but got I
		//IL_04a0: Expected O, but got I
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A273]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (!string.IsNullOrEmpty(s))
		{
			object obj = 0;
			string text = s;
			while (text != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v159 @ rax_v7+B8]");
				object newValue = 0;
				string text2 = text.Replace("<b>", (string)newValue);
				if (text2 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v361 @ rcx_v6+B8]");
				object newValue2 = 0;
				string text3 = text2.Replace("<i>", (string)newValue2);
				if (text3 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v367 @ rcx_v8+B8]");
				object newValue3 = 0;
				string text4 = text3.Replace("<u>", (string)newValue3);
				if (text4 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v373 @ rcx_v10+B8]");
				object newValue4 = 0;
				string text5 = text4.Replace("<B>", (string)newValue4);
				if (text5 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ rcx_v12+B8]");
				object newValue5 = 0;
				string text6 = text5.Replace("<I>", (string)newValue5);
				if (text6 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ rcx_v14+B8]");
				object newValue6 = 0;
				string text7 = text6.Replace("<U>", (string)newValue6);
				if (text7 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v383 @ rcx_v16+B8]");
				object newValue7 = 0;
				string text8 = text7.Replace("<smallcaps>", (string)newValue7);
				if (text8 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v385 @ rcx_v18+B8]");
				object newValue8 = 0;
				string text9 = text8.Replace("</b>", (string)newValue8);
				if (text9 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v387 @ rcx_v20+B8]");
				object newValue9 = 0;
				string text10 = text9.Replace("</i>", (string)newValue9);
				if (text10 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj11 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v389 @ rcx_v22+B8]");
				object newValue10 = 0;
				string text11 = text10.Replace("</u>", (string)newValue10);
				if (text11 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj12 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v391 @ rcx_v24+B8]");
				object newValue11 = 0;
				string text12 = text11.Replace("</B>", (string)newValue11);
				if (text12 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj13 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v393 @ rcx_v26+B8]");
				object newValue12 = 0;
				string text13 = text12.Replace("</I>", (string)newValue12);
				if (text13 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj14 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v395 @ rcx_v28+B8]");
				object newValue13 = 0;
				string text14 = text13.Replace("</U>", (string)newValue13);
				if (text14 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj15 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ rcx_v30+B8]");
				object newValue14 = 0;
				string text15 = text14.Replace("</smallcaps>", (string)newValue14);
				if (!string.Equals(text15, text, StringComparison.Ordinal))
				{
					obj++;
					bool flag = (nint)obj < 16;
					text = text15;
					if (flag)
					{
						continue;
					}
				}
				return text15;
			}
			return (string)(object)new NullReferenceException();
		}
		return s;
	}

	public NotepadSection()
	{
		Queue<PendingWrite> pendingWrites = new Queue<PendingWrite>();
		_pendingWrites = pendingWrites;
		base._002Ector();
	}

	static NotepadSection()
	{
		List<NotepadSection> list = new List<NotepadSection>();
		s_AllSections = list;
	}
}
