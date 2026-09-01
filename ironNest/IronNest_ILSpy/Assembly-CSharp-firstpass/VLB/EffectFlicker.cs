using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public class EffectFlicker : EffectAbstractBase
{
	private sealed class _003CCoChangeIntensity_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public EffectFlicker _003C_003E4__this;

		public float nextIntensity;

		public float expectedDuration;

		private float _003Cvelocity_003E5__2;

		private float _003Ct_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCoChangeIntensity_003Ed__13(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_006d: Expected I4, but got I8
			//IL_0166: Expected I4, but got O
			//IL_00be: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Expected Ref, but got Unknown
			EffectFlicker effectFlicker = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003Cvelocity_003E5__2 = 0f;
			}
			else if (_003C_003E1__state != 1)
			{
				goto IL_0152;
			}
			_003C_003E1__state = -1;
			if (expectedDuration > _003Ct_003E5__3)
			{
				if ((object)_003C_003E4__this != null)
				{
					float deltaTime = Time.deltaTime;
					float maxSpeed = default(float);
					float deltaTime2 = default(float);
					float additiveIntensity = (effectFlicker.m_CurrentAdditiveIntensity = Mathf.SmoothDamp(effectFlicker.m_CurrentAdditiveIntensity, nextIntensity, ref *(float*)(this + 48), effectFlicker.smoothing, maxSpeed, deltaTime2));
					_003C_003E4__this.SetAdditiveIntensity(additiveIntensity);
					float deltaTime3 = Time.deltaTime;
					float num = deltaTime3 + _003Ct_003E5__3;
					_003C_003E2__current = null;
					_003Ct_003E5__3 = num;
					_003C_003E1__state = 1;
					return true;
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			goto IL_0152;
			IL_0152:
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

	private sealed class _003CCoFlicker_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public EffectFlicker _003C_003E4__this;

		private float _003CremainingDuration_003E5__2;

		private float _003CfreqDuration_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCoFlicker_003Ed__12(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_003b: Expected I4, but got I8
			//IL_00d9: Expected I4, but got I8
			//IL_01ce: Expected I4, but got O
			//IL_0074: Expected F4, but got I
			//IL_0074: Expected F4, but got O
			//IL_017f: Expected F4, but got I
			//IL_017f: Expected F4, but got O
			//IL_012f: Invalid comparison between F4 and I4
			EffectFlicker effectFlicker = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					MinMaxRangeFloat flickeringDuration = effectFlicker.flickeringDuration;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rdi_v1 (VLB.EffectFlicker)+5C]");
					float num = UnityEngine.Random.Range((float)flickeringDuration, 0f);
					_003CremainingDuration_003E5__2 = num;
					float deltaTime = Time.deltaTime;
					goto IL_0102;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_01ba;
				}
				float num2 = _003CremainingDuration_003E5__2 - _003CfreqDuration_003E5__3;
				_003C_003E1__state = -1;
				_003CremainingDuration_003E5__2 = num2;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_0102;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0102:
			if (!effectFlicker.performPauses || _003CremainingDuration_003E5__2 > 0f)
			{
				float expectedDuration = (_003CfreqDuration_003E5__3 = 1f / effectFlicker.frequency);
				MinMaxRangeFloat intensityAmplitude = effectFlicker.intensityAmplitude;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rdi_v1 (VLB.EffectFlicker)+70]");
				float nextIntensity = UnityEngine.Random.Range((float)intensityAmplitude, 0f);
				IEnumerator enumerator = _003C_003E4__this.CoChangeIntensity(expectedDuration, nextIntensity);
				_003C_003E2__current = enumerator;
				_003C_003E1__state = 1;
				return true;
			}
			goto IL_01ba;
			IL_01ba:
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

	private sealed class _003CCoUpdate_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public EffectFlicker _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCoUpdate_003Ed__11(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0129: Expected I4, but got I8
			//IL_0176: Expected I4, but got O
			//IL_0039: Expected O, but got I4
			//IL_0079: Expected I4, but got I8
			//IL_00d4: Expected F4, but got I
			//IL_00d4: Expected F4, but got O
			//IL_0103: Expected F4, but got I4
			EffectFlicker effectFlicker = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (flag)
				{
					_003C_003E1__state = -1;
					if ((object)_003C_003E4__this == null)
					{
						goto IL_0168;
					}
					if (effectFlicker.performPauses)
					{
						MinMaxRangeFloat pauseDuration = effectFlicker.pauseDuration;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rdi_v1 (VLB.EffectFlicker)+64]");
						float expectedDuration = UnityEngine.Random.Range((float)pauseDuration, 0f);
						float nextIntensity = ((!effectFlicker.restoreIntensityOnPause) ? effectFlicker.m_CurrentAdditiveIntensity : 0f);
						IEnumerator enumerator = _003C_003E4__this.CoChangeIntensity(expectedDuration, nextIntensity);
						_003C_003E2__current = enumerator;
						_003C_003E1__state = 2;
						return true;
					}
					goto IL_01a8;
				}
				if ((nint)obj != 1)
				{
					return false;
				}
			}
			_003C_003E1__state = -1;
			if ((object)_003C_003E4__this == null)
			{
				goto IL_0168;
			}
			goto IL_01a8;
			IL_0168:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_01a8:
			_003CCoFlicker_003Ed__12 obj2 = new _003CCoFlicker_003Ed__12(0);
			obj2._003C_003E1__state = 0;
			obj2._003C_003E4__this = _003C_003E4__this;
			_003C_003E2__current = obj2;
			_003C_003E1__state = 1;
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

	public new const string ClassName = "EffectFlicker";

	public float frequency;

	public bool performPauses;

	public MinMaxRangeFloat flickeringDuration;

	public MinMaxRangeFloat pauseDuration;

	public bool restoreIntensityOnPause;

	public MinMaxRangeFloat intensityAmplitude;

	public float smoothing;

	private float m_CurrentAdditiveIntensity;

	public override void InitFrom(EffectAbstractBase source)
	{
		//IL_01d8: Expected I, but got O
		//IL_01e0: Expected I, but got O
		//IL_01f0: Expected O, but got I
		//IL_005e: Expected O, but got I
		//IL_0083: Expected O, but got I4
		//IL_00e0: Expected F4, but got I
		//IL_0111: Expected O, but got I
		//IL_0130: Expected O, but got I
		//IL_0161: Expected O, but got I
		//IL_0173: Expected F4, but got I
		UnityEngine.Object obj;
		if ((bool)source)
		{
			componentsToChange = source.componentsToChange;
			restoreIntensityOnDisable = source.restoreIntensityOnDisable;
		}
		else if ((object)source == null)
		{
			obj = null;
			goto IL_00a8;
		}
		nint num = (nint)typeof(EffectFlicker);
		nint num2 = (nint)source;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rdx_v4 (Il2CppClass<VLB.EffectFlicker>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v147 @ r8_v2 (Il2CppClass<VLB.EffectAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rdx_v4 (Il2CppClass<VLB.EffectFlicker>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v147 @ r8_v2 (Il2CppClass<VLB.EffectAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v200 @ rax_v22+FFFFFFF8+v148 @ rax_v18*8]");
			bool flag = 0 == (nint)typeof(EffectFlicker);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_01a7;
			}
		}
		obj4 = null;
		goto IL_01a7;
		IL_00a8:
		if ((bool)obj)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rbx_v1 (UnityEngine.Object)+50]");
			frequency = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rbx_v1 (UnityEngine.Object)+54]");
			performPauses = false;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rbx_v1 (UnityEngine.Object)+5C]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rbx_v1 (UnityEngine.Object)+58]");
			flickeringDuration = (MinMaxRangeFloat)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rbx_v1 (UnityEngine.Object)+64]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rbx_v1 (UnityEngine.Object)+60]");
			pauseDuration = (MinMaxRangeFloat)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rbx_v1 (UnityEngine.Object)+68]");
			restoreIntensityOnPause = false;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rbx_v1 (UnityEngine.Object)+70]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rbx_v1 (UnityEngine.Object)+6C]");
			intensityAmplitude = (MinMaxRangeFloat)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rbx_v1 (UnityEngine.Object)+74]");
			smoothing = 0f;
		}
		return;
		IL_01a7:
		bool flag2 = (object)obj4 == null;
		obj = null;
		if (!flag2)
		{
			obj = source;
		}
		goto IL_00a8;
	}

	protected override void OnEnable()
	{
		StopAllCoroutines();
		_003CCoUpdate_003Ed__11 obj = new _003CCoUpdate_003Ed__11(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private IEnumerator CoUpdate()
	{
		_003CCoUpdate_003Ed__11 obj = new _003CCoUpdate_003Ed__11(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator CoFlicker()
	{
		_003CCoFlicker_003Ed__12 obj = new _003CCoFlicker_003Ed__12(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator CoChangeIntensity(float expectedDuration, float nextIntensity)
	{
		_003CCoChangeIntensity_003Ed__13 obj = new _003CCoChangeIntensity_003Ed__13(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.expectedDuration = expectedDuration;
		obj.nextIntensity = nextIntensity;
		return obj;
	}

	public EffectFlicker()
	{
		//IL_001e: Expected I, but got O
		//IL_0059: Expected I, but got O
		//IL_008f: Expected I, but got O
		frequency = 10f;
		nint num = (nint)typeof(Consts.Effects);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rax_v3 (Il2CppClass<VLB.Consts+Effects>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v4 (Il2CppStaticFields<VLB.Consts+Effects>)+4]");
		_ = 0;
		flickeringDuration = Consts.Effects.FlickeringDurationDefault;
		nint num3 = (nint)typeof(Consts.Effects);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rax_v6 (Il2CppClass<VLB.Consts+Effects>)+B8]");
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rax_v7 (Il2CppStaticFields<VLB.Consts+Effects>)+C]");
		_ = 0;
		pauseDuration = Consts.Effects.PauseDurationDefault;
		nint num5 = (nint)typeof(Consts.Effects);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rax_v9 (Il2CppClass<VLB.Consts+Effects>)+B8]");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rax_v10 (Il2CppStaticFields<VLB.Consts+Effects>)+14]");
		_ = 0;
		intensityAmplitude = Consts.Effects.IntensityAmplitudeDefault;
		smoothing = 0.05f;
		componentsToChange = (ComponentsToChange)2147483647;
		restoreIntensityOnDisable = true;
		((MonoBehaviour)this)._002Ector();
	}
}
