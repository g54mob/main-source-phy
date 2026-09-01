using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public class EffectPulse : EffectAbstractBase
{
	private sealed class _003CCoUpdate_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public EffectPulse _003C_003E4__this;

		private float _003Ct_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCoUpdate_003Ed__5(int _003C_003E1__state)
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
			//IL_0047: Expected F4, but got I4
			//IL_0082: Expected I4, but got I8
			//IL_015d: Expected I4, but got O
			//IL_00f9: Invalid comparison between I4 and F4
			//IL_0144: Expected F4, but got I4
			//IL_0191: Unknown result type (might be due to invalid IL or missing references)
			//IL_0196: Expected O, but got Unknown
			EffectPulse effectPulse = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003Ct_003E5__2 = _003C_003E1__state;
				_003CCoUpdate_003Ed__5 obj = this;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
				float deltaTime = Time.deltaTime;
				float num = deltaTime + _003Ct_003E5__2;
				_003Ct_003E5__2 = num;
				_003CCoUpdate_003Ed__5 obj = null;
			}
			if ((object)_003C_003E4__this != null)
			{
				float num2 = effectPulse.frequency * _003Ct_003E5__2;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
				float num3 = num2 * 0.5f;
				float num4 = num3 + 0.5f;
				if (!(0f > num4))
				{
					if (num4 > 1f)
					{
						num4 = 1f;
					}
				}
				else
				{
					num4 = 0f;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rdi_v1 (VLB.EffectPulse)+58]");
				object obj2 = 0 - effectPulse.intensityAmplitude;
				float num5 = (float)obj2 * num4;
				float additiveIntensity = num5 + (float)effectPulse.intensityAmplitude;
				_003C_003E4__this.SetAdditiveIntensity(additiveIntensity);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
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
	}

	public new const string ClassName = "EffectPulse";

	public float frequency;

	public MinMaxRangeFloat intensityAmplitude;

	public override void InitFrom(EffectAbstractBase source)
	{
		//IL_0164: Expected I, but got O
		//IL_016c: Expected I, but got O
		//IL_017c: Expected O, but got I
		//IL_005e: Expected O, but got I
		//IL_0083: Expected O, but got I4
		//IL_00e0: Expected F4, but got I
		//IL_00ff: Expected O, but got I
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
		nint num = (nint)typeof(EffectPulse);
		nint num2 = (nint)source;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rdx_v4 (Il2CppClass<VLB.EffectPulse>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v147 @ r8_v2 (Il2CppClass<VLB.EffectAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rdx_v4 (Il2CppClass<VLB.EffectPulse>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v147 @ r8_v2 (Il2CppClass<VLB.EffectAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v200 @ rax_v17+FFFFFFF8+v148 @ rax_v13*8]");
			bool flag = 0 == (nint)typeof(EffectPulse);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_0133;
			}
		}
		obj4 = null;
		goto IL_0133;
		IL_00a8:
		if ((bool)obj)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rbx_v1 (UnityEngine.Object)+50]");
			frequency = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rbx_v1 (UnityEngine.Object)+58]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rbx_v1 (UnityEngine.Object)+54]");
			intensityAmplitude = (MinMaxRangeFloat)0;
		}
		return;
		IL_0133:
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
		_003CCoUpdate_003Ed__5 obj = new _003CCoUpdate_003Ed__5(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private IEnumerator CoUpdate()
	{
		_003CCoUpdate_003Ed__5 obj = new _003CCoUpdate_003Ed__5(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public EffectPulse()
	{
		//IL_001e: Expected I, but got O
		frequency = 10f;
		nint num = (nint)typeof(Consts.Effects);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rax_v3 (Il2CppClass<VLB.Consts+Effects>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v4 (Il2CppStaticFields<VLB.Consts+Effects>)+14]");
		_ = 0;
		intensityAmplitude = Consts.Effects.IntensityAmplitudeDefault;
		componentsToChange = (ComponentsToChange)2147483647;
		restoreIntensityOnDisable = true;
		((MonoBehaviour)this)._002Ector();
	}
}
