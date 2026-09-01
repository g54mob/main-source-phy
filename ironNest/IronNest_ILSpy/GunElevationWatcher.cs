using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class GunElevationWatcher : MonoBehaviour
{
	[Serializable]
	public class GunControllerEvent : UnityEvent<GunController>
	{
	}

	private sealed class _003CDelayedFire_003Ed__26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public int tokenAtSchedule;

		public GunElevationWatcher _003C_003E4__this;

		public GunController gun;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CDelayedFire_003Ed__26(int _003C_003E1__state)
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
			//IL_0075: Expected I4, but got I8
			//IL_0287: Expected I4, but got O
			GunElevationWatcher gunElevationWatcher = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				WaitForSeconds waitForSeconds = new WaitForSeconds(delay);
				_003C_003E2__current = waitForSeconds;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_0279;
				}
				if (tokenAtSchedule == gunElevationWatcher._pendingToken)
				{
					if (gunElevationWatcher.cancelIfGunDropsBelowDuringDelay && gun != null)
					{
						GunController gunController = gun;
						if ((object)gun == null)
						{
							goto IL_0279;
						}
						if (!(gunElevationWatcher.triggerAboveDegrees < gunController._003CCurrentElevation_003Ek__BackingField))
						{
							if (gunElevationWatcher.logCrossings)
							{
								if ((object)gun == null)
								{
									goto IL_0279;
								}
								string message = "[GunElevationWatcher] Trigger did not fire (gun not above threshold at fire time): '" + gunController.gunName + "'";
								Debug.Log(message, gun);
							}
							int pendingToken = gunElevationWatcher._pendingToken + 1;
							gunElevationWatcher._pendingToken = pendingToken;
							if (gunElevationWatcher._pendingCoroutine != null)
							{
								_003C_003E4__this.StopCoroutine(gunElevationWatcher._pendingCoroutine);
								gunElevationWatcher._pendingCoroutine = null;
							}
							gunElevationWatcher._pendingGun = null;
							goto IL_0243;
						}
					}
					gunElevationWatcher._pendingCoroutine = null;
					gunElevationWatcher._pendingGun = null;
					_003C_003E4__this.FireEventsNow(gun);
					return false;
				}
			}
			goto IL_0243;
			IL_0279:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0243:
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

	private sealed class _003CEnumerateValidGuns_003Ed__30 : IEnumerable<GunController>, IEnumerable, IEnumerator<GunController>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private GunController _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		public GunElevationWatcher _003C_003E4__this;

		private int _003Ci_003E5__2;

		GunController IEnumerator<GunController>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CEnumerateValidGuns_003Ed__30(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			_003C_003El__initialThreadId = num;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_0056: Expected I4, but got I8
			//IL_0115: Expected I4, but got O
			GunElevationWatcher gunElevationWatcher = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003Ci_003E5__2 = _003C_003E1__state;
				goto IL_0141;
			}
			if (_003C_003E1__state != 1)
			{
				goto IL_0101;
			}
			_003C_003E1__state = -1;
			goto IL_0160;
			IL_0101:
			return false;
			IL_0160:
			int num = _003Ci_003E5__2 + 1;
			_003Ci_003E5__2 = num;
			goto IL_0141;
			IL_0141:
			if ((object)_003C_003E4__this != null)
			{
				List<GunController> guns = gunElevationWatcher.guns;
				if (gunElevationWatcher.guns != null)
				{
					if (_003Ci_003E5__2 >= guns._size)
					{
						goto IL_0101;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					UnityEngine.Object obj = default(UnityEngine.Object);
					if (obj != null)
					{
						_003C_003E2__current = (GunController)obj;
						_003C_003E1__state = 1;
						return true;
					}
					goto IL_0160;
				}
			}
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

		IEnumerator<GunController> IEnumerable<GunController>.GetEnumerator()
		{
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					return this;
				}
			}
			_003CEnumerateValidGuns_003Ed__30 obj2 = new _003CEnumerateValidGuns_003Ed__30(0);
			obj2._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj2._003C_003El__initialThreadId = num;
			obj2._003C_003E4__this = _003C_003E4__this;
			return obj2;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					return this;
				}
			}
			_003CEnumerateValidGuns_003Ed__30 obj2 = new _003CEnumerateValidGuns_003Ed__30(0);
			obj2._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj2._003C_003El__initialThreadId = num;
			obj2._003C_003E4__this = _003C_003E4__this;
			return obj2;
		}
	}

	private List<GunController> guns;

	private bool autoFindGunsInScene;

	private bool deduplicateGunReferences;

	private float triggerAboveDegrees;

	private bool triggerOnlyOnce;

	private bool armImmediatelyOnEnable;

	private float triggerDelaySeconds;

	private bool cancelIfGunDropsBelowDuringDelay;

	private bool singlePendingTrigger;

	private UnityEvent onAnyGunElevatedAboveThreshold;

	private GunControllerEvent onThresholdCrossedWithGun;

	private bool logCrossings;

	private readonly Dictionary<GunController, float> _lastElevation;

	private bool _hasTriggered;

	private Coroutine _pendingCoroutine;

	private GunController _pendingGun;

	private int _pendingToken;

	private unsafe void OnEnable()
	{
		//IL_028d: Expected O, but got Ref
		//IL_00d0: Expected O, but got Ref
		//IL_0385: Expected O, but got I4
		//IL_032a: Expected O, but got I
		//IL_0333: Expected O, but got I4
		//IL_01c3: Expected O, but got I4
		//IL_0168: Expected O, but got I
		//IL_0171: Expected O, but got I4
		//IL_039d: Expected F4, but got Ref
		//IL_03c6: Expected O, but got I
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Expected O, but got Unknown
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Expected O, but got Unknown
		//IL_01ed: Expected F4, but got Ref
		//IL_0216: Expected O, but got I
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Expected O, but got Unknown
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Expected O, but got Unknown
		if (autoFindGunsInScene)
		{
			GunController[] array = UnityEngine.Object.FindObjectsByType<GunController>(FindObjectsInactive.Include, FindObjectsSortMode.None);
			if (array != null)
			{
				guns.AddRange(array);
			}
		}
		if (deduplicateGunReferences)
		{
			DeduplicateGunsInPlace();
		}
		_lastElevation.Clear();
		float value = default(float);
		object obj2 = default(object);
		if (!armImmediatelyOnEnable)
		{
			IEnumerable<GunController> enumerable = EnumerateValidGuns();
			((Dictionary<GunController, float>)null).set_Item((GunController)(object)typeof(IEnumerable<GunController>), value);
			object obj = (object)(&obj2);
			Dictionary<GunController, float> dictionary = null;
			float num;
			GunController gunController = default(GunController);
			float num2 = default(float);
			object obj3 = default(object);
			object obj13 = default(object);
			for (; obj2 != null; Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v770 @ rdx_v34] (should have been resolved before IL gen)"), num = gunController._003CCurrentElevation_003Ek__BackingField, _lastElevation.set_Item(gunController, (float)(nint)(&num2)))
			{
				((Dictionary<GunController, float>)null).set_Item((GunController)(object)typeof(IEnumerator), value);
				object obj12;
				object obj5;
				if (obj3 != null)
				{
					if (obj2 != null)
					{
						object obj4 = obj2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v329 @ r10_v11+12E]");
						if ((nint)0 < (nint)0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v329 @ r10_v11+B0]");
							obj5 = 0;
							object obj6 = 0;
							while (true)
							{
								object obj7 = obj6 + obj6;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v604 @ r8_v24+v650 @ rax_v53*8]");
								if (0 == (nint)typeof(IEnumerator<GunController>))
								{
									break;
								}
								obj6++;
								object obj8 = obj6;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v329 @ r10_v11+12E]");
								if ((nint)obj8 < 0)
								{
									continue;
								}
								goto IL_01a8;
							}
							object obj9 = obj6 + obj6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v604 @ r8_v24+8+v765 @ rcx_v43*8]");
							object obj10 = (nint)0 << 4;
							object obj11 = obj10 + 312;
							obj12 = obj11 + obj4;
							continue;
						}
						goto IL_01a8;
					}
					throw new NullReferenceException();
				}
				if (obj != null)
				{
					((Dictionary<GunController, float>)null).set_Item((GunController)(object)typeof(IDisposable), value);
				}
				return;
				IL_01a8:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj12 = obj13;
				obj5 = 0;
			}
			throw new NullReferenceException();
		}
		IEnumerable<GunController> enumerable2 = EnumerateValidGuns();
		((Dictionary<GunController, float>)null).set_Item((GunController)(object)typeof(IEnumerable<GunController>), value);
		object obj14 = (object)(&obj2);
		Dictionary<GunController, float> dictionary2 = null;
		object obj15 = default(object);
		object obj25 = default(object);
		GunController key = default(GunController);
		float num3 = default(float);
		while (true)
		{
			object obj24;
			object obj17;
			if (obj2 != null)
			{
				((Dictionary<GunController, float>)null).set_Item((GunController)(object)typeof(IEnumerator), value);
				if (obj15 != null)
				{
					bool flag = obj2 == null;
					dictionary2 = null;
					if (!flag)
					{
						object obj16 = obj2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v378 @ r10_v4+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_036a;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v378 @ r10_v4+B0]");
						obj17 = 0;
						object obj18 = 0;
						while (true)
						{
							object obj19 = obj18 + obj18;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v551 @ r8_v9+v684 @ rax_v24*8]");
							if (0 == (nint)typeof(IEnumerator<GunController>))
							{
								break;
							}
							obj18++;
							object obj20 = obj18;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v378 @ r10_v4+12E]");
							if ((nint)obj20 < 0)
							{
								continue;
							}
							goto IL_036a;
						}
						object obj21 = obj18 + obj18;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v551 @ r8_v9+8+v798 @ rcx_v22*8]");
						object obj22 = (nint)0 << 4;
						object obj23 = obj22 + 312;
						obj24 = obj23 + obj16;
						goto IL_053b;
					}
					throw new NullReferenceException();
				}
				if (obj14 != null)
				{
					((Dictionary<GunController, float>)null).set_Item((GunController)(object)typeof(IDisposable), value);
				}
				return;
			}
			throw new NullReferenceException();
			IL_036a:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj24 = obj25;
			obj17 = 0;
			goto IL_053b;
			IL_053b:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v803 @ rdx_v13] (should have been resolved before IL gen)");
			float num = triggerAboveDegrees;
			if (_lastElevation == null)
			{
				break;
			}
			_lastElevation.set_Item(key, (float)(nint)(&num3));
		}
		throw new NullReferenceException();
	}

	private void OnDisable()
	{
		int pendingToken = _pendingToken + 1;
		_pendingToken = pendingToken;
		if (_pendingCoroutine != null)
		{
			StopCoroutine(_pendingCoroutine);
			_pendingCoroutine = null;
		}
		_pendingGun = null;
	}

	private unsafe void Update()
	{
		//IL_006f: Expected O, but got Ref
		//IL_00d4: Expected I, but got O
		//IL_015b: Expected O, but got I4
		//IL_010c: Expected O, but got I
		//IL_01fc: Expected O, but got I
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Expected O, but got Unknown
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		//IL_026f: Invalid comparison between F4 and I4
		//IL_0298: Expected O, but got I4
		//IL_0383: Expected F4, but got Ref
		if (_hasTriggered && triggerOnlyOnce)
		{
			return;
		}
		IEnumerable<GunController> enumerable = EnumerateValidGuns();
		bool flag = enumerable == null;
		GunElevationWatcher gunElevationWatcher = this;
		Dictionary<GunController, float> dictionary2;
		if (!flag)
		{
			float value = default(float);
			((Dictionary<GunController, float>)null).set_Item((GunController)(object)typeof(IEnumerable<GunController>), value);
			Dictionary<GunController, float> dictionary = default(Dictionary<GunController, float>);
			object obj = (object)(&dictionary);
			dictionary2 = null;
			object obj2 = default(object);
			GunController gunController = default(GunController);
			object arg = default(object);
			object arg2 = default(object);
			float num6 = default(float);
			object obj10 = default(object);
			while (true)
			{
				object obj3;
				object obj8;
				if (dictionary != null)
				{
					((Dictionary<GunController, float>)null).set_Item((GunController)(object)typeof(IEnumerator), value);
					if (obj2 == null)
					{
						break;
					}
					bool flag2 = dictionary == null;
					dictionary2 = null;
					if (!flag2)
					{
						nint num = (nint)dictionary;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v350 @ r10_v9 (Il2CppClass<System.Collections.Generic.Dictionary`2<GunController, System.Single>>)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_0148;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v350 @ r10_v9 (Il2CppClass<System.Collections.Generic.Dictionary`2<GunController, System.Single>>)+B0]");
						obj3 = 0;
						Coroutine coroutine = null;
						while (true)
						{
							object obj4 = (object)coroutine + (object)coroutine;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v440 @ r8_v19+v609 @ rax_v56*8]");
							if (0 == (nint)typeof(IEnumerator<GunController>))
							{
								break;
							}
							coroutine = (Coroutine)(coroutine + 1);
							Coroutine coroutine2 = coroutine;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v350 @ r10_v9 (Il2CppClass<System.Collections.Generic.Dictionary`2<GunController, System.Single>>)+12E]");
							if ((nint)coroutine2 < 0)
							{
								continue;
							}
							goto IL_0148;
						}
						object obj5 = (object)coroutine + (object)coroutine;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v440 @ r8_v19+8+v676 @ rcx_v42*8]");
						object obj6 = (nint)0 << 4;
						object obj7 = obj6 + 312;
						obj8 = obj7 + num;
						goto IL_05bc;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
				IL_05bc:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v681 @ rdx_v23] (should have been resolved before IL gen)");
				if ((object)gunController != null)
				{
					if (_lastElevation != null)
					{
						bool flag3 = _lastElevation.TryGetValue(gunController, out var value2);
						float num2 = (flag3 ? value2 : ((armImmediatelyOnEnable == flag3) ? gunController._003CCurrentElevation_003Ek__BackingField : triggerAboveDegrees));
						float num3 = triggerAboveDegrees;
						if (!(triggerAboveDegrees < num2))
						{
							bool flag4 = gunController._003CCurrentElevation_003Ek__BackingField < triggerAboveDegrees;
							float num4 = gunController._003CCurrentElevation_003Ek__BackingField - triggerAboveDegrees;
							bool flag5 = num4 == 0f;
							bool flag6 = !flag4;
							bool flag7 = !flag5;
							object obj9 = flag7 & flag6;
							if (obj9 != null)
							{
								if (logCrossings)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									num3 = triggerAboveDegrees;
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									string message = $"[GunElevationWatcher] Crossed up: '{gunController.gunName}' {arg:0.###}° > {arg2:0.###}°";
									Debug.Log(message, gunController);
									float num5 = triggerAboveDegrees;
								}
								TryScheduleTrigger(gunController);
							}
						}
						dictionary2 = _lastElevation;
						if (_lastElevation != null)
						{
							_lastElevation.set_Item(gunController, (float)(nint)(&num6));
							continue;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
				IL_0148:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj3 = 0;
				obj8 = obj10;
				goto IL_05bc;
			}
			if (obj != null)
			{
				((Dictionary<GunController, float>)null).set_Item((GunController)(object)typeof(IDisposable), value);
			}
			if (!cancelIfGunDropsBelowDuringDelay || !(_pendingGun != null))
			{
				return;
			}
			GunController pendingGun = _pendingGun;
			bool flag8 = (object)_pendingGun == null;
			gunElevationWatcher = (GunElevationWatcher)(object)_pendingGun;
			if (!flag8)
			{
				if (!(triggerAboveDegrees < pendingGun._003CCurrentElevation_003Ek__BackingField))
				{
					if (logCrossings)
					{
						string message2 = "[GunElevationWatcher] Pending trigger cancelled (gun dropped below threshold): '" + pendingGun.gunName + "'";
						Debug.Log(message2, _pendingGun);
					}
					int pendingToken = _pendingToken + 1;
					_pendingToken = pendingToken;
					if (_pendingCoroutine != null)
					{
						StopCoroutine(_pendingCoroutine);
						_pendingCoroutine = null;
					}
					_pendingGun = null;
				}
				return;
			}
		}
		dictionary2 = (Dictionary<GunController, float>)(object)gunElevationWatcher;
		throw new NullReferenceException();
	}

	public unsafe void AddGun(GunController gun)
	{
		//IL_00b1: Expected F4, but got Ref
		if (!(gun != null))
		{
			return;
		}
		guns.Add(gun);
		if (deduplicateGunReferences)
		{
			DeduplicateGunsInPlace();
		}
		bool flag = _lastElevation.ContainsKey(gun);
		if (!flag)
		{
			if (armImmediatelyOnEnable != flag)
			{
			}
			object obj = default(object);
			_lastElevation.set_Item(gun, (float)(nint)(&obj));
		}
	}

	public void RemoveGun(GunController gun)
	{
		if (!(gun != null))
		{
			return;
		}
		bool flag = guns.Remove(gun);
		bool flag2 = _lastElevation.Remove(gun);
		if (_pendingGun == gun)
		{
			int pendingToken = _pendingToken + 1;
			_pendingToken = pendingToken;
			if (_pendingCoroutine != null)
			{
				StopCoroutine(_pendingCoroutine);
				_pendingCoroutine = null;
			}
			_pendingGun = null;
		}
	}

	public void ResetTriggerLatch()
	{
		_hasTriggered = false;
	}

	public void CancelPendingTrigger()
	{
		int pendingToken = _pendingToken + 1;
		_pendingToken = pendingToken;
		if (_pendingCoroutine != null)
		{
			StopCoroutine(_pendingCoroutine);
			_pendingCoroutine = null;
		}
		_pendingGun = null;
	}

	private void TryScheduleTrigger(GunController gun)
	{
		//IL_0073: Invalid comparison between I4 and F4
		//IL_0085: Expected F4, but got I4
		//IL_01a8: Invalid comparison between I4 and F4
		if ((_hasTriggered && triggerOnlyOnce) || (singlePendingTrigger && _pendingCoroutine != null))
		{
			return;
		}
		bool flag = !(0f < triggerDelaySeconds);
		float num = 0f;
		if (!flag)
		{
			num = triggerDelaySeconds;
		}
		if (0f < num)
		{
			_pendingGun = gun;
			int num2 = _pendingToken + 1;
			bool flag2 = !logCrossings;
			_pendingToken = num2;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"[GunElevationWatcher] Trigger scheduled in {arg:0.###}s by '{gun.gunName}'";
				Debug.Log(message, gun);
			}
			_003CDelayedFire_003Ed__26 obj = new _003CDelayedFire_003Ed__26(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			obj.tokenAtSchedule = num2;
			obj.gun = gun;
			obj.delay = num;
			Coroutine pendingCoroutine = StartCoroutine(obj);
			_pendingCoroutine = pendingCoroutine;
		}
		else
		{
			FireEventsNow(gun);
		}
	}

	private IEnumerator DelayedFire(int tokenAtSchedule, GunController gun, float delay)
	{
		_003CDelayedFire_003Ed__26 obj = new _003CDelayedFire_003Ed__26(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.tokenAtSchedule = tokenAtSchedule;
		obj.gun = gun;
		obj.delay = delay;
		return obj;
	}

	private void FireEventsNow(GunController gun)
	{
		if (_hasTriggered && triggerOnlyOnce)
		{
			return;
		}
		if (logCrossings)
		{
			string text = ((!(gun != null)) ? "NULL" : gun.gunName);
			string message = "[GunElevationWatcher] Trigger fired by '" + text + "'";
			Debug.Log(message, gun);
		}
		if (onAnyGunElevatedAboveThreshold != null)
		{
			onAnyGunElevatedAboveThreshold.Invoke();
		}
		if (gun != null && onThresholdCrossedWithGun != null)
		{
			onThresholdCrossedWithGun.Invoke(gun);
		}
		bool flag = !triggerOnlyOnce;
		_hasTriggered = true;
		if (!flag)
		{
			int pendingToken = _pendingToken + 1;
			_pendingToken = pendingToken;
			if (_pendingCoroutine != null)
			{
				StopCoroutine(_pendingCoroutine);
				_pendingCoroutine = null;
			}
			_pendingGun = null;
		}
	}

	private void RefreshGunListIfConfigured()
	{
		if (autoFindGunsInScene)
		{
			GunController[] array = UnityEngine.Object.FindObjectsByType<GunController>(FindObjectsInactive.Include, FindObjectsSortMode.None);
			if (array != null)
			{
				guns.AddRange(array);
			}
		}
		if (deduplicateGunReferences)
		{
			DeduplicateGunsInPlace();
		}
	}

	private void DeduplicateGunsInPlace()
	{
		//IL_00d1: Expected O, but got I4
		HashSet<GunController> hashSet = new HashSet<GunController>();
		List<GunController> list = guns;
		bool flag = (nint)guns < 0;
		int num = list._size - 1;
		if (flag)
		{
			return;
		}
		UnityEngine.Object obj = default(UnityEngine.Object);
		object obj2 = default(object);
		object obj3;
		do
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			bool flag2;
			if (obj != null)
			{
				hashSet.Add((GunController)obj);
				flag2 = (nint)obj2 < 0;
				if (obj2 != null)
				{
					goto IL_00b8;
				}
			}
			flag2 = (nint)guns < 0;
			guns.RemoveAt(num);
			goto IL_00b8;
			IL_00b8:
			num--;
			obj3 = !flag2;
		}
		while (obj3 != null);
	}

	private IEnumerable<GunController> EnumerateValidGuns()
	{
		//IL_0042: Expected I4, but got I8
		_003CEnumerateValidGuns_003Ed__30 obj = new _003CEnumerateValidGuns_003Ed__30(0);
		obj._003C_003E1__state = -2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
		int num = default(int);
		obj._003C_003El__initialThreadId = num;
		obj._003C_003E4__this = this;
		return obj;
	}

	public GunElevationWatcher()
	{
		List<GunController> list = new List<GunController>();
		guns = list;
		deduplicateGunReferences = true;
		triggerAboveDegrees = 10f;
		singlePendingTrigger = true;
		onAnyGunElevatedAboveThreshold = new UnityEvent();
		onThresholdCrossedWithGun = new GunControllerEvent();
		Dictionary<GunController, float> dictionary = new Dictionary<GunController, float>();
		dictionary._002Ector();
		_lastElevation = dictionary;
		base._002Ector();
	}
}
