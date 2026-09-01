using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public sealed class SwingLightFlicker : MonoBehaviour
{
	private sealed class _003CFlickerBurstRoutine_003Ed__54 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SwingLightFlicker _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CFlickerBurstRoutine_003Ed__54(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_033c: Expected I4, but got I8
			//IL_04ee: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_0082: Expected I4, but got I8
			//IL_03e9: Invalid comparison between F4 and I4
			//IL_0412: Expected O, but got I4
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_0490: Expected F4, but got I
			//IL_0490: Expected F4, but got O
			//IL_006e: Expected I4, but got I8
			//IL_04a2: Invalid comparison between I4 and F4
			//IL_04b4: Expected F4, but got I4
			SwingLightFlicker swingLightFlicker = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (flag || (nint)obj2 == 1)
					{
						_003C_003E1__state = -1;
						goto IL_01c1;
					}
				}
				else
				{
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this == null)
					{
						goto IL_04e0;
					}
					if (!swingLightFlicker.standaloneMode)
					{
						bool flag2 = !swingLightFlicker.isEmergencyLight;
						bool flag3 = swingLightFlicker._masterPowerOn;
						if (!flag2 && !swingLightFlicker._emergencyPlayerOverride)
						{
							bool flag4 = !swingLightFlicker._masterPowerOn;
							flag3 = flag4;
						}
						if (!flag3)
						{
							goto IL_02f3;
						}
					}
					if (!swingLightFlicker.allowManualOverride || !swingLightFlicker._manualOverrideOff)
					{
						_003C_003E4__this.SetLightActive(active: true);
						float value = UnityEngine.Random.value;
						if (!(swingLightFlicker.recoveryFlickerChance > value))
						{
							goto IL_01c1;
						}
						IEnumerator enumerator = _003C_003E4__this.QuickFlickerRoutine(shorterRecovery: true);
						_003C_003E2__current = enumerator;
						_003C_003E1__state = 2;
						return true;
					}
				}
				goto IL_02f3;
			}
			_003C_003E1__state = -1;
			if ((object)_003C_003E4__this != null)
			{
				bool flag5 = !swingLightFlicker.allowManualOverride;
				swingLightFlicker._isBurstRunning = true;
				if (flag5 || !swingLightFlicker._manualOverrideOff)
				{
					float value2 = UnityEngine.Random.value;
					bool flag6 = swingLightFlicker.brownoutChance < value2;
					float num = swingLightFlicker.brownoutChance - value2;
					bool flag7 = num == 0f;
					bool flag8 = !flag6;
					bool flag9 = !flag7;
					object obj3 = flag9 & flag8;
					if (obj3 == null)
					{
						IEnumerator enumerator2 = _003C_003E4__this.QuickFlickerRoutine(shorterRecovery: false);
						_003C_003E2__current = enumerator2;
						_003C_003E1__state = 3;
						return true;
					}
					_003C_003E4__this.SetLightActive(active: false);
					Vector2 brownoutDurationMinMaxSeconds = swingLightFlicker.brownoutDurationMinMaxSeconds;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rbx_v1 (SwingLightFlicker)+84]");
					float num2 = UnityEngine.Random.Range((float)brownoutDurationMinMaxSeconds, 0f);
					bool flag10 = !(0f < num2);
					float seconds = 0f;
					if (!flag10)
					{
						seconds = num2;
					}
					WaitForSeconds waitForSeconds = new WaitForSeconds(seconds);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_02d6;
			}
			goto IL_04e0;
			IL_02f3:
			return false;
			IL_02d6:
			swingLightFlicker._isBurstRunning = false;
			swingLightFlicker._runningRoutine = null;
			goto IL_02f3;
			IL_04e0:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_01c1:
			if ((object)_003C_003E4__this != null)
			{
				if (!swingLightFlicker.standaloneMode)
				{
					bool flag11 = !swingLightFlicker.isEmergencyLight;
					bool flag12 = swingLightFlicker._masterPowerOn;
					if (!flag11 && !swingLightFlicker._emergencyPlayerOverride)
					{
						bool flag13 = !swingLightFlicker._masterPowerOn;
						flag12 = flag13;
					}
					if (!flag12)
					{
						goto IL_02d6;
					}
				}
				if (!swingLightFlicker.allowManualOverride || !swingLightFlicker._manualOverrideOff)
				{
					_003C_003E4__this.SetLightActive(active: true);
				}
				goto IL_02d6;
			}
			goto IL_04e0;
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

	private sealed class _003CPassiveFlickerLoop_003Ed__52 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SwingLightFlicker _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CPassiveFlickerLoop_003Ed__52(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0561: Expected I4, but got I8
			//IL_0767: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_01d6: Expected I4, but got I8
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Expected O, but got Unknown
			//IL_06c9: Expected F4, but got I4
			//IL_0095: Expected I4, but got I8
			//IL_0882: Invalid comparison between F4 and I
			//IL_08bb: Invalid comparison between F4 and I4
			//IL_06e4: Expected F4, but got O
			//IL_06f9: Expected F4, but got I
			//IL_03b5: Invalid comparison between I4 and F4
			//IL_0400: Expected F4, but got I4
			//IL_041c: Invalid comparison between I4 and F4
			//IL_042e: Expected F4, but got I4
			//IL_081b: Invalid comparison between I4 and F4
			SwingLightFlicker swingLightFlicker = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						object obj3 = obj2 - 1;
						if (!flag)
						{
							if ((nint)obj3 != 1)
							{
								return false;
							}
							_003C_003E1__state = -1;
							if ((object)_003C_003E4__this != null)
							{
								if (!swingLightFlicker.standaloneMode)
								{
									bool flag2 = !swingLightFlicker.isEmergencyLight;
									bool flag3 = swingLightFlicker._masterPowerOn;
									if (!flag2 && !swingLightFlicker._emergencyPlayerOverride)
									{
										bool flag4 = !swingLightFlicker._masterPowerOn;
										flag3 = flag4;
									}
									if (!flag3)
									{
										goto IL_01aa;
									}
								}
								if (!swingLightFlicker.allowManualOverride || !swingLightFlicker._manualOverrideOff)
								{
									_003C_003E4__this.SetLightActive(active: true);
								}
								goto IL_01aa;
							}
							goto IL_0759;
						}
					}
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this != null)
					{
						if (swingLightFlicker.enablePassiveFlicker)
						{
							if (!swingLightFlicker.standaloneMode)
							{
								bool flag5 = !swingLightFlicker.isEmergencyLight;
								bool flag6 = swingLightFlicker._masterPowerOn;
								if (!flag5 && !swingLightFlicker._emergencyPlayerOverride)
								{
									bool flag7 = !swingLightFlicker._masterPowerOn;
									flag6 = flag7;
								}
								if (!flag6)
								{
									goto IL_0580;
								}
							}
							if (!swingLightFlicker.allowManualOverride || !swingLightFlicker._manualOverrideOff)
							{
								bool flag8 = swingLightFlicker.lightObjectToToggle == null;
								if (!flag8 && swingLightFlicker._isBurstRunning == flag8 && swingLightFlicker._runningRoutine == null)
								{
									float time = Time.time;
									if (!(swingLightFlicker._nextAllowedPassiveBurstTime > time))
									{
										float value = UnityEngine.Random.value;
										float num = swingLightFlicker.passiveAttemptFlickerChance;
										if (!(0f > swingLightFlicker.passiveAttemptFlickerChance))
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
										if (!(value > num))
										{
											float time2 = Time.time;
											bool flag9 = !(0f < swingLightFlicker.minTimeBetweenPassiveBursts);
											float num2 = 0f;
											if (!flag9)
											{
												num2 = swingLightFlicker.minTimeBetweenPassiveBursts;
											}
											swingLightFlicker._isBurstRunning = true;
											float nextAllowedPassiveBurstTime = num2 + time2;
											swingLightFlicker._nextAllowedPassiveBurstTime = nextAllowedPassiveBurstTime;
											if (0f == swingLightFlicker.minTimeBetweenPassiveBursts)
											{
												bool flag10 = !swingLightFlicker.isEmergencyLight;
												bool flag11 = swingLightFlicker._masterPowerOn;
												if (!flag10 && !swingLightFlicker._emergencyPlayerOverride)
												{
													bool flag12 = !swingLightFlicker._masterPowerOn;
													flag11 = flag12;
												}
												if (!flag11)
												{
													goto IL_051f;
												}
											}
											if (!swingLightFlicker.allowManualOverride || !swingLightFlicker._manualOverrideOff)
											{
												_003C_003E4__this.SetLightActive(active: true);
											}
											goto IL_051f;
										}
									}
								}
							}
							goto IL_0580;
						}
						goto IL_0742;
					}
					goto IL_0759;
				}
			}
			_003C_003E1__state = -1;
			if ((object)_003C_003E4__this != null)
			{
				goto IL_0580;
			}
			goto IL_0759;
			IL_0759:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_051f:
			IEnumerator enumerator = _003C_003E4__this.QuickFlickerRoutine(shorterRecovery: true);
			_003C_003E2__current = enumerator;
			_003C_003E1__state = 4;
			goto IL_084b;
			IL_0742:
			_003C_003E2__current = null;
			_003C_003E1__state = 1;
			goto IL_084b;
			IL_0580:
			if (swingLightFlicker.enablePassiveFlicker)
			{
				if (!swingLightFlicker.standaloneMode)
				{
					bool flag13 = !swingLightFlicker.isEmergencyLight;
					bool flag14 = swingLightFlicker._masterPowerOn;
					if (!flag13 && !swingLightFlicker._emergencyPlayerOverride)
					{
						bool flag15 = !swingLightFlicker._masterPowerOn;
						flag14 = flag15;
					}
					if (!flag14)
					{
						goto IL_0742;
					}
				}
				if ((!swingLightFlicker.allowManualOverride || !swingLightFlicker._manualOverrideOff) && swingLightFlicker.lightObjectToToggle != null)
				{
					bool flag16 = 0 >= (nint)swingLightFlicker.passiveAttemptIntervalMinMaxSeconds;
					float num3 = 0f;
					if (!flag16)
					{
						num3 = (float)swingLightFlicker.passiveAttemptIntervalMinMaxSeconds;
					}
					float num4 = num3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rbx_v1 (SwingLightFlicker)+48]");
					bool flag17 = !(num4 < 0f);
					float maxInclusive = num3;
					if (!flag17)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rbx_v1 (SwingLightFlicker)+48]");
						maxInclusive = 0f;
					}
					float num5 = UnityEngine.Random.Range(num3, maxInclusive);
					if (!(num5 > 0f))
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 3;
					}
					else
					{
						WaitForSeconds waitForSeconds = new WaitForSeconds(num5);
						_003C_003E2__current = waitForSeconds;
						_003C_003E1__state = 2;
					}
					goto IL_084b;
				}
			}
			goto IL_0742;
			IL_01aa:
			swingLightFlicker._isBurstRunning = false;
			swingLightFlicker._runningRoutine = null;
			goto IL_0580;
			IL_084b:
			return true;
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

	private sealed class _003CPowerRestoreRoutine_003Ed__53 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SwingLightFlicker _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CPowerRestoreRoutine_003Ed__53(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_01e5: Expected I4, but got I8
			//IL_0577: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_0232: Expected F4, but got I4
			//IL_01d1: Expected I4, but got I8
			//IL_05ce: Invalid comparison between F4 and I
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_0676: Invalid comparison between F4 and I4
			//IL_024d: Expected F4, but got O
			//IL_01bd: Expected I4, but got I8
			//IL_0262: Expected F4, but got I
			//IL_0397: Expected F4, but got I4
			//IL_006e: Expected I4, but got I8
			//IL_0616: Invalid comparison between F4 and I
			//IL_06a4: Invalid comparison between F4 and I4
			//IL_03b2: Expected F4, but got O
			//IL_03c7: Expected F4, but got I
			SwingLightFlicker swingLightFlicker = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (flag)
				{
					_003C_003E1__state = -1;
					goto IL_0267;
				}
				object obj2 = obj - 1;
				if (flag)
				{
					_003C_003E1__state = -1;
					goto IL_03cc;
				}
				if ((nint)obj2 != 1)
				{
					goto IL_01a0;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					if (!swingLightFlicker.standaloneMode)
					{
						bool flag2 = !swingLightFlicker.isEmergencyLight;
						bool flag3 = swingLightFlicker._masterPowerOn;
						if (!flag2 && !swingLightFlicker._emergencyPlayerOverride)
						{
							bool flag4 = !swingLightFlicker._masterPowerOn;
							flag3 = flag4;
						}
						if (!flag3)
						{
							goto IL_0183;
						}
					}
					if (!swingLightFlicker.allowManualOverride || !swingLightFlicker._manualOverrideOff)
					{
						_003C_003E4__this.SetLightActive(active: true);
					}
					goto IL_0183;
				}
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					swingLightFlicker._isBurstRunning = true;
					bool flag5 = 0 >= (nint)swingLightFlicker.powerRestoreStartDelayMinMaxSeconds;
					float num = 0f;
					if (!flag5)
					{
						num = (float)swingLightFlicker.powerRestoreStartDelayMinMaxSeconds;
					}
					float num2 = num;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (SwingLightFlicker)+94]");
					bool flag6 = !(num2 < 0f);
					float maxInclusive = num;
					if (!flag6)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (SwingLightFlicker)+94]");
						maxInclusive = 0f;
					}
					float num3 = UnityEngine.Random.Range(num, maxInclusive);
					if (num3 > 0f)
					{
						WaitForSeconds waitForSeconds = new WaitForSeconds(num3);
						_003C_003E2__current = waitForSeconds;
						_003C_003E1__state = 1;
						goto IL_064e;
					}
					goto IL_0267;
				}
			}
			goto IL_0569;
			IL_0183:
			swingLightFlicker._isBurstRunning = false;
			swingLightFlicker._runningRoutine = null;
			goto IL_01a0;
			IL_0569:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0267:
			if ((object)_003C_003E4__this == null)
			{
				goto IL_0569;
			}
			if (!swingLightFlicker.standaloneMode)
			{
				bool flag7 = !swingLightFlicker.isEmergencyLight;
				bool flag8 = swingLightFlicker._masterPowerOn;
				if (!flag7 && !swingLightFlicker._emergencyPlayerOverride)
				{
					bool flag9 = !swingLightFlicker._masterPowerOn;
					flag8 = flag9;
				}
				if (!flag8)
				{
					goto IL_01a0;
				}
			}
			if (swingLightFlicker.allowManualOverride && swingLightFlicker._manualOverrideOff)
			{
				goto IL_01a0;
			}
			_003C_003E4__this.SetLightActive(active: false);
			bool flag10 = 0 >= (nint)swingLightFlicker.powerRestoreOffDurationMinMaxSeconds;
			float num4 = 0f;
			if (!flag10)
			{
				num4 = (float)swingLightFlicker.powerRestoreOffDurationMinMaxSeconds;
			}
			float num5 = num4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (SwingLightFlicker)+9C]");
			bool flag11 = !(num5 < 0f);
			float maxInclusive2 = num4;
			if (!flag11)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (SwingLightFlicker)+9C]");
				maxInclusive2 = 0f;
			}
			float num6 = UnityEngine.Random.Range(num4, maxInclusive2);
			if (num6 > 0f)
			{
				WaitForSeconds waitForSeconds2 = new WaitForSeconds(num6);
				_003C_003E2__current = waitForSeconds2;
				_003C_003E1__state = 2;
				goto IL_064e;
			}
			goto IL_03cc;
			IL_01a0:
			return false;
			IL_03cc:
			if ((object)_003C_003E4__this == null)
			{
				goto IL_0569;
			}
			if (!swingLightFlicker.standaloneMode)
			{
				bool flag12 = !swingLightFlicker.isEmergencyLight;
				bool flag13 = swingLightFlicker._masterPowerOn;
				if (!flag12 && !swingLightFlicker._emergencyPlayerOverride)
				{
					bool flag14 = !swingLightFlicker._masterPowerOn;
					flag13 = flag14;
				}
				if (!flag13)
				{
					goto IL_01a0;
				}
			}
			if (swingLightFlicker.allowManualOverride && swingLightFlicker._manualOverrideOff)
			{
				goto IL_01a0;
			}
			_003C_003E4__this.SetLightActive(active: true);
			IEnumerator enumerator = _003C_003E4__this.QuickFlickerRoutine(shorterRecovery: true);
			_003C_003E2__current = enumerator;
			_003C_003E1__state = 3;
			goto IL_064e;
			IL_064e:
			return true;
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

	private sealed class _003CQuickFlickerRoutine_003Ed__55 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SwingLightFlicker _003C_003E4__this;

		public bool shorterRecovery;

		private int _003Ctoggles_003E5__2;

		private float _003CminI_003E5__3;

		private float _003CmaxI_003E5__4;

		private int _003Ci_003E5__5;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CQuickFlickerRoutine_003Ed__55(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0080: Expected I4, but got I8
			//IL_02b5: Expected I4, but got O
			//IL_0015: Expected O, but got I4
			//IL_006c: Expected I4, but got I8
			//IL_00f4: Expected I4, but got O
			//IL_0381: Expected O, but got I4
			//IL_03b8: Expected F4, but got O
			//IL_0419: Expected F4, but got O
			//IL_016c: Expected O, but got I
			//IL_0249: Invalid comparison between F4 and I4
			SwingLightFlicker swingLightFlicker = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag && (nint)obj != 1)
				{
					goto IL_02a1;
				}
				int num = _003Ci_003E5__5 + 1;
				_003Ci_003E5__5 = num;
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_02a7;
				}
				if (_003C_003E4__this.IsManuallyOff())
				{
					goto IL_02a1;
				}
				bool flag2 = (nint)swingLightFlicker.flickerToggleCountMinMax < 0;
				int num2 = 0;
				if (!flag2)
				{
					num2 = (int)swingLightFlicker.flickerToggleCountMinMax;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (SwingLightFlicker)+70]");
				int num3 = 0;
				int num4 = num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (SwingLightFlicker)+70]");
				if ((nint)num4 > (nint)0)
				{
					num3 = num2;
				}
				if (shorterRecovery)
				{
					int num5 = num2 + 2;
					if (num3 >= num5)
					{
						num3 = num5;
					}
					if (num2 > num3)
					{
						num3 = num2;
					}
				}
				int maxExclusive = num3 + 1;
				int num6 = UnityEngine.Random.Range(num2, maxExclusive);
				_003Ctoggles_003E5__2 = num6;
				bool flag3 = 0 >= (nint)swingLightFlicker.toggleIntervalMinMaxSeconds;
				Vector2 vector = (Vector2)0;
				if (!flag3)
				{
					vector = swingLightFlicker.toggleIntervalMinMaxSeconds;
				}
				_003CminI_003E5__3 = (float)vector;
				Vector2 vector2 = vector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (SwingLightFlicker)+78]");
				if ((nint)vector2 < 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (SwingLightFlicker)+78]");
					vector = (Vector2)0;
				}
				_003Ci_003E5__5 = 0;
				_003CmaxI_003E5__4 = (float)vector;
			}
			if (_003Ci_003E5__5 >= _003Ctoggles_003E5__2)
			{
				goto IL_02a1;
			}
			if ((object)_003C_003E4__this != null)
			{
				if (!_003C_003E4__this.IsEffectivelyPowered() || _003C_003E4__this.IsManuallyOff())
				{
					goto IL_02a1;
				}
				if ((object)swingLightFlicker.lightObjectToToggle != null)
				{
					bool activeSelf = swingLightFlicker.lightObjectToToggle.activeSelf;
					bool lightActive = (byte)((activeSelf ? 1u : 0u) ^ 1u) != 0;
					_003C_003E4__this.SetLightActive(lightActive);
					float num7 = UnityEngine.Random.Range(_003CminI_003E5__3, _003CmaxI_003E5__4);
					if (!(num7 > 0f))
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 2;
					}
					else
					{
						WaitForSeconds waitForSeconds = new WaitForSeconds(num7);
						_003C_003E2__current = waitForSeconds;
						_003C_003E1__state = 1;
					}
					return true;
				}
			}
			goto IL_02a7;
			IL_02a7:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_02a1:
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

	private bool standaloneMode;

	private SwingReceiver receiver;

	private GameObject lightObjectToToggle;

	private UnityEvent<bool> onLightToggled;

	private bool startManuallyOffWhenEnabled;

	private bool playRestoreSequenceOnEnable;

	private bool allowManualOverride;

	private bool enablePassiveFlicker;

	private Vector2 passiveAttemptIntervalMinMaxSeconds;

	private float passiveAttemptFlickerChance;

	private float minTimeBetweenPassiveBursts;

	private float movingThresholdDegPerSec;

	private float spikeThresholdDegPerSec2;

	private float baseBurstChancePerSecond;

	private float spikeBurstChancePerSecond;

	private float spikeExponent;

	private float minTimeBetweenBursts;

	private Vector2Int flickerToggleCountMinMax;

	private Vector2 toggleIntervalMinMaxSeconds;

	private float brownoutChance;

	private Vector2 brownoutDurationMinMaxSeconds;

	private float recoveryFlickerChance;

	private bool isEmergencyLight;

	private bool playRestoreSequenceWhenMasterPoweredOn;

	private Vector2 powerRestoreStartDelayMinMaxSeconds;

	private Vector2 powerRestoreOffDurationMinMaxSeconds;

	private bool _isBurstRunning;

	private float _nextAllowedBurstTime;

	private bool _masterPowerOn;

	private bool _lastMasterPowerOn;

	private bool _manualOverrideOff;

	private bool _emergencyPlayerOverride;

	private Coroutine _runningRoutine;

	private float _nextAllowedPassiveBurstTime;

	private Coroutine _passiveRoutine;

	private void Awake()
	{
		if (receiver == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			SwingReceiver swingReceiver = default(SwingReceiver);
			receiver = swingReceiver;
			if (receiver == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
				receiver = swingReceiver;
			}
		}
		if (lightObjectToToggle == null)
		{
			GameObject gameObject = base.gameObject;
			lightObjectToToggle = gameObject;
		}
		ApplyDesiredLightState();
	}

	private void OnEnable()
	{
		if (!standaloneMode)
		{
			SwingLightFlickerController.Register(this);
		}
		if (startManuallyOffWhenEnabled && allowManualOverride)
		{
			SetManualOverride(manualOff: true);
			return;
		}
		ApplyDesiredLightState();
		if (playRestoreSequenceOnEnable)
		{
			if (!standaloneMode)
			{
				bool flag = !isEmergencyLight;
				bool flag2 = _masterPowerOn;
				if (!flag && !_emergencyPlayerOverride)
				{
					bool flag3 = !_masterPowerOn;
					flag2 = flag3;
				}
				if (!flag2)
				{
					goto IL_0174;
				}
			}
			if (!allowManualOverride || !_manualOverrideOff)
			{
				StopAllLocalRoutines();
				IEnumerator routine = PowerRestoreRoutine();
				StartLocalRoutine(routine);
			}
		}
		goto IL_0174;
		IL_0174:
		EnsurePassiveRoutineState();
	}

	private void OnDisable()
	{
		if (!standaloneMode)
		{
			SwingLightFlickerController.Unregister(this);
		}
		if (_passiveRoutine != null)
		{
			StopCoroutine(_passiveRoutine);
			_passiveRoutine = null;
		}
		bool flag = _runningRoutine == null;
		_isBurstRunning = false;
		if (!flag)
		{
			StopCoroutine(_runningRoutine);
			_runningRoutine = null;
		}
		SetLightActive(active: true);
	}

	private void Update()
	{
		//IL_01cf: Invalid comparison between I4 and F4
		//IL_0200: Invalid comparison between I4 and F4
		//IL_0212: Expected F4, but got I4
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Expected O, but got Unknown
		//IL_02ce: Invalid comparison between I4 and F4
		//IL_02e0: Expected F4, but got I4
		//IL_03cc: Invalid comparison between I4 and F4
		//IL_03de: Expected F4, but got I4
		if (!Application.isPlaying)
		{
			return;
		}
		if (!standaloneMode)
		{
			bool flag = !isEmergencyLight;
			bool flag2 = _masterPowerOn;
			if (!flag && !_emergencyPlayerOverride)
			{
				bool flag3 = !_masterPowerOn;
				flag2 = flag3;
			}
			if (!flag2)
			{
				return;
			}
		}
		if ((allowManualOverride && _manualOverrideOff) || !(receiver != null) || !(lightObjectToToggle != null))
		{
			return;
		}
		SwingReceiver swingReceiver = receiver;
		bool flag4 = swingReceiver._motionMagnitude < movingThresholdDegPerSec;
		if (flag4 || _isBurstRunning != flag4)
		{
			return;
		}
		float time = Time.time;
		float nextAllowedBurstTime = _nextAllowedBurstTime;
		if (_nextAllowedBurstTime > time)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		if (!(0f < deltaTime))
		{
			return;
		}
		SwingReceiver swingReceiver2 = receiver;
		bool flag5 = !(0f < baseBurstChancePerSecond);
		float num = 0f;
		if (!flag5)
		{
			num = baseBurstChancePerSecond;
		}
		if (swingReceiver2._motionSpikePerSecond > spikeThresholdDegPerSec2)
		{
			nextAllowedBurstTime = spikeThresholdDegPerSec2;
			if (spikeThresholdDegPerSec2 > 0.0001f)
			{
				float num2 = swingReceiver2._motionSpikePerSecond / spikeThresholdDegPerSec2;
				bool flag6 = !(0.01f < spikeExponent);
				float num3 = 0.01f;
				if (!flag6)
				{
					num3 = spikeExponent;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
				bool flag7 = !(0f < spikeBurstChancePerSecond);
				nextAllowedBurstTime = 0f;
				if (!flag7)
				{
					nextAllowedBurstTime = spikeBurstChancePerSecond;
				}
				float num4 = num2 * nextAllowedBurstTime;
				float num5 = num4 + num;
				num = num5;
			}
		}
		float value = UnityEngine.Random.value;
		float num6 = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj = num6 ^ 0;
		float num7 = (float)obj * deltaTime;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F3D0");
		float num8 = 1f - num7;
		if (num8 > value)
		{
			float time2 = Time.time;
			bool flag8 = !(0f < minTimeBetweenBursts);
			float num9 = 0f;
			if (!flag8)
			{
				num9 = minTimeBetweenBursts;
			}
			float nextAllowedBurstTime2 = num9 + time2;
			_nextAllowedBurstTime = nextAllowedBurstTime2;
			_003CFlickerBurstRoutine_003Ed__54 obj2 = new _003CFlickerBurstRoutine_003Ed__54(0);
			obj2._003C_003E1__state = 0;
			obj2._003C_003E4__this = this;
			StartLocalRoutine(obj2);
		}
	}

	public void ToggleManualOverride()
	{
		if (!allowManualOverride)
		{
			return;
		}
		if (!isEmergencyLight)
		{
			bool manualOverride = !_manualOverrideOff;
			SetManualOverride(manualOverride);
			return;
		}
		bool flag = !_emergencyPlayerOverride;
		if (_emergencyPlayerOverride != flag)
		{
			_emergencyPlayerOverride = flag;
			ApplyDesiredLightState();
			EnsurePassiveRoutineState();
		}
	}

	public void SetManualOverride(bool manualOff)
	{
		bool flag = !allowManualOverride;
		bool flag2 = false;
		if (!flag)
		{
			flag2 = manualOff;
		}
		if (_manualOverrideOff == flag2)
		{
			return;
		}
		_manualOverrideOff = flag2;
		if (flag2)
		{
			bool flag3 = _runningRoutine == null;
			_isBurstRunning = false;
			if (!flag3)
			{
				StopCoroutine(_runningRoutine);
				_runningRoutine = null;
			}
		}
		ApplyDesiredLightState();
		EnsurePassiveRoutineState();
	}

	private void SetEmergencyManualOn(bool overrideActive)
	{
		if (_emergencyPlayerOverride != overrideActive)
		{
			_emergencyPlayerOverride = overrideActive;
			ApplyDesiredLightState();
			EnsurePassiveRoutineState();
		}
	}

	public void SetMasterPowerState(bool powerOn, bool playRestoreSequence)
	{
		//IL_02e8: Expected O, but got I4
		//IL_0302: Expected O, but got I4
		//IL_01ef: Expected O, but got I4
		if (standaloneMode)
		{
			return;
		}
		bool flag = !isEmergencyLight;
		_lastMasterPowerOn = _masterPowerOn;
		_masterPowerOn = powerOn;
		if (!flag)
		{
			_emergencyPlayerOverride = false;
		}
		bool flag3;
		bool lightActive;
		if (!standaloneMode)
		{
			bool flag2 = !isEmergencyLight;
			flag3 = powerOn;
			if (!flag2)
			{
				bool flag4 = _emergencyPlayerOverride;
				flag3 = powerOn;
				if (!flag4)
				{
					bool flag5 = !powerOn;
					flag3 = flag5;
				}
			}
			if (!flag3)
			{
				bool flag6 = _runningRoutine == null;
				_isBurstRunning = flag3;
				if (!flag6)
				{
					StopCoroutine(_runningRoutine);
					_runningRoutine = null;
				}
				lightActive = false;
				goto IL_02cc;
			}
		}
		else
		{
			flag3 = true;
		}
		bool flag8;
		if (isEmergencyLight)
		{
			bool flag7 = !_lastMasterPowerOn;
			flag8 = flag7;
		}
		else
		{
			flag8 = _lastMasterPowerOn;
		}
		object obj = flag8 & flag3;
		bool flag9 = obj == null;
		object obj2 = !flag9;
		if (obj2 == null)
		{
			bool flag10 = _runningRoutine == null;
			_isBurstRunning = false;
			if (!flag10)
			{
				StopCoroutine(_runningRoutine);
				_runningRoutine = null;
			}
			if (!isEmergencyLight)
			{
				_manualOverrideOff = false;
				if (!playRestoreSequence)
				{
					goto IL_0282;
				}
			}
			object obj3 = 141;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v310 @ rax_v6+this @ rcx (SwingLightFlicker)]");
			if ((nint)0 == 0)
			{
				goto IL_0282;
			}
			IEnumerator routine = PowerRestoreRoutine();
			bool flag11 = _runningRoutine == null;
			_isBurstRunning = false;
			if (!flag11)
			{
				StopCoroutine(_runningRoutine);
				_runningRoutine = null;
			}
			Coroutine runningRoutine = StartCoroutine(routine);
			_runningRoutine = runningRoutine;
		}
		goto IL_0290;
		IL_0290:
		EnsurePassiveRoutineState();
		return;
		IL_02cc:
		SetLightActive(lightActive);
		goto IL_0290;
		IL_0282:
		lightActive = true;
		goto IL_02cc;
	}

	public void FlickerTurnOn()
	{
		if (lightObjectToToggle != null && base.isActiveAndEnabled)
		{
			if (_manualOverrideOff)
			{
				_manualOverrideOff = false;
				EnsurePassiveRoutineState();
			}
			if (!standaloneMode)
			{
				_lastMasterPowerOn = _masterPowerOn;
				_masterPowerOn = true;
			}
			bool flag = _runningRoutine == null;
			_isBurstRunning = false;
			if (!flag)
			{
				StopCoroutine(_runningRoutine);
				_runningRoutine = null;
			}
			SetLightActive(active: false);
			IEnumerator routine = PowerRestoreRoutine();
			bool flag2 = _runningRoutine == null;
			_isBurstRunning = false;
			if (!flag2)
			{
				StopCoroutine(_runningRoutine);
				_runningRoutine = null;
			}
			Coroutine runningRoutine = StartCoroutine(routine);
			_runningRoutine = runningRoutine;
		}
	}

	private bool IsManuallyOff()
	{
		if (!allowManualOverride)
		{
			return false;
		}
		return _manualOverrideOff;
	}

	private bool IsEffectivelyPowered()
	{
		bool result;
		if (!standaloneMode)
		{
			bool flag = !isEmergencyLight;
			result = _masterPowerOn;
			if (!flag && !_emergencyPlayerOverride)
			{
				return !_masterPowerOn;
			}
		}
		else
		{
			result = true;
		}
		return result;
	}

	private void ApplyDesiredLightState()
	{
		bool flag = lightObjectToToggle == null;
		if (flag)
		{
			return;
		}
		if (standaloneMode == flag)
		{
			bool flag2 = !isEmergencyLight;
			bool flag3 = _masterPowerOn;
			if (!flag2 && !_emergencyPlayerOverride)
			{
				bool flag4 = !_masterPowerOn;
				flag3 = flag4;
			}
			if (!flag3)
			{
				goto IL_00eb;
			}
		}
		if (allowManualOverride && _manualOverrideOff)
		{
			goto IL_00eb;
		}
		bool lightActive = true;
		goto IL_0126;
		IL_0126:
		SetLightActive(lightActive);
		return;
		IL_00eb:
		lightActive = false;
		goto IL_0126;
	}

	private void StartLocalRoutine(IEnumerator routine)
	{
		bool flag = _runningRoutine == null;
		_isBurstRunning = false;
		if (!flag)
		{
			StopCoroutine(_runningRoutine);
			_runningRoutine = null;
		}
		Coroutine runningRoutine = StartCoroutine(routine);
		_runningRoutine = runningRoutine;
	}

	private void StopAllLocalRoutines()
	{
		bool flag = _runningRoutine == null;
		_isBurstRunning = false;
		if (!flag)
		{
			StopCoroutine(_runningRoutine);
			_runningRoutine = null;
		}
	}

	private void EnsurePassiveRoutineState()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		Coroutine passiveRoutine;
		if (enablePassiveFlicker)
		{
			if (!standaloneMode)
			{
				bool flag = !isEmergencyLight;
				bool flag2 = _masterPowerOn;
				if (!flag && !_emergencyPlayerOverride)
				{
					bool flag3 = !_masterPowerOn;
					flag2 = flag3;
				}
				if (!flag2)
				{
					goto IL_01b1;
				}
			}
			if ((!allowManualOverride || !_manualOverrideOff) && lightObjectToToggle != null)
			{
				bool flag4 = base.isActiveAndEnabled;
				passiveRoutine = _passiveRoutine;
				if (flag4)
				{
					if (_passiveRoutine == null)
					{
						_003CPassiveFlickerLoop_003Ed__52 obj = new _003CPassiveFlickerLoop_003Ed__52(0);
						obj._003C_003E1__state = 0;
						obj._003C_003E4__this = this;
						Coroutine passiveRoutine2 = StartCoroutine(obj);
						_passiveRoutine = passiveRoutine2;
					}
					return;
				}
				goto IL_01f7;
			}
		}
		goto IL_01b1;
		IL_01b1:
		passiveRoutine = _passiveRoutine;
		goto IL_01f7;
		IL_01f7:
		if (passiveRoutine != null)
		{
			StopCoroutine(_passiveRoutine);
			_passiveRoutine = null;
		}
	}

	private void StartPassiveRoutineIfNeeded()
	{
		if (_passiveRoutine == null)
		{
			_003CPassiveFlickerLoop_003Ed__52 obj = new _003CPassiveFlickerLoop_003Ed__52(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine passiveRoutine = StartCoroutine(obj);
			_passiveRoutine = passiveRoutine;
		}
	}

	private void StopPassiveRoutine()
	{
		if (_passiveRoutine != null)
		{
			StopCoroutine(_passiveRoutine);
			_passiveRoutine = null;
		}
	}

	private IEnumerator PassiveFlickerLoop()
	{
		_003CPassiveFlickerLoop_003Ed__52 obj = new _003CPassiveFlickerLoop_003Ed__52(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator PowerRestoreRoutine()
	{
		_003CPowerRestoreRoutine_003Ed__53 obj = new _003CPowerRestoreRoutine_003Ed__53(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator FlickerBurstRoutine()
	{
		_003CFlickerBurstRoutine_003Ed__54 obj = new _003CFlickerBurstRoutine_003Ed__54(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator QuickFlickerRoutine(bool shorterRecovery)
	{
		_003CQuickFlickerRoutine_003Ed__55 obj = new _003CQuickFlickerRoutine_003Ed__55(0);
		if (obj != null)
		{
			obj._003C_003E4__this = this;
			obj.shorterRecovery = shorterRecovery;
			return obj;
		}
		return (IEnumerator)new NullReferenceException();
	}

	private unsafe void SetLightActive(bool active)
	{
		if (lightObjectToToggle != null)
		{
			bool activeSelf = lightObjectToToggle.activeSelf;
			if (activeSelf != active)
			{
				lightObjectToToggle.SetActive(active);
				object obj = default(object);
				onLightToggled.Invoke((byte)(&obj) != 0);
			}
		}
	}

	public SwingLightFlicker()
	{
		//IL_0010: Expected O, but got I4
		//IL_0026: Expected O, but got I4
		//IL_008f: Expected O, but got I4
		//IL_00ab: Expected O, but got I4
		//IL_00d2: Expected O, but got I4
		//IL_00e3: Expected O, but got I4
		UnityEvent<bool> unityEvent = new UnityEvent<bool>();
		onLightToggled = unityEvent;
		flickerToggleCountMinMax = (Vector2Int)2;
		allowManualOverride = true;
		passiveAttemptIntervalMinMaxSeconds = (Vector2)1086324736;
		_ = 1099956224;
		passiveAttemptFlickerChance = 0.18f;
		minTimeBetweenPassiveBursts = 3.5f;
		movingThresholdDegPerSec = 12f;
		spikeThresholdDegPerSec2 = 140f;
		baseBurstChancePerSecond = 0.04f;
		spikeBurstChancePerSecond = 0.9f;
		spikeExponent = 2f;
		minTimeBetweenBursts = 0.6f;
		toggleIntervalMinMaxSeconds = (Vector2)1022739087;
		_ = 1035489772;
		brownoutChance = 0.18f;
		brownoutDurationMinMaxSeconds = (Vector2)1053609165;
		_ = 1075838976;
		recoveryFlickerChance = 0.6f;
		playRestoreSequenceWhenMasterPoweredOn = true;
		powerRestoreStartDelayMinMaxSeconds = (Vector2)0;
		_ = 1061997773;
		powerRestoreOffDurationMinMaxSeconds = (Vector2)1045220557;
		_ = 1065353216;
		_masterPowerOn = true;
		base._002Ector();
	}
}
