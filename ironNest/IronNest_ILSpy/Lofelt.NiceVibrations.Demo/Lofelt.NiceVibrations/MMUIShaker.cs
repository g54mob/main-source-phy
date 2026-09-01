using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Lofelt.NiceVibrations;

public class MMUIShaker : MonoBehaviour
{
	private sealed class _003CShake_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MMUIShaker _003C_003E4__this;

		public float duration;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CShake_003Ed__7(int _003C_003E1__state)
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
			//IL_00a2: Expected I4, but got I8
			//IL_00e2: Expected I4, but got O
			MMUIShaker mMUIShaker = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					mMUIShaker.Shaking = true;
					WaitForSeconds waitForSeconds = new WaitForSeconds(duration);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_00d4;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_00d4;
				}
				mMUIShaker.Shaking = false;
			}
			return false;
			IL_00d4:
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

	public float Amplitude;

	public float Frequency;

	public bool Shaking;

	protected Vector3 _initialPosition;

	protected Vector3 _shakePosition;

	protected RectTransform _rectTransform;

	protected virtual void Start()
	{
		//IL_003c: Expected O, but got F4
		GameObject gameObject = base.gameObject;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		RectTransform rectTransform = default(RectTransform);
		_rectTransform = rectTransform;
		Vector3 localPosition = _rectTransform.localPosition;
		_initialPosition = (Vector3)localPosition.x;
		_ = localPosition.z;
	}

	public virtual IEnumerator Shake(float duration)
	{
		_003CShake_003Ed__7 obj = new _003CShake_003Ed__7(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.duration = duration;
		return obj;
	}

	protected unsafe virtual void Update()
	{
		//IL_0230: Expected O, but got Ref
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_00ba: Expected O, but got F4
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected O, but got Unknown
		//IL_0220: Expected O, but got Ref
		Vector3 vector = default(Vector3);
		if (Shaking)
		{
			float time = Time.time;
			float time2 = Time.time;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj = time ^ 0;
			float y = time2 * Frequency;
			float x = (float)obj * Frequency;
			float num = Mathf.PerlinNoise(x, y);
			float num2 = num * Amplitude;
			float num3 = Amplitude * 0.5f;
			float num4 = num2 - num3;
			_shakePosition = (Vector3)num4;
			float time3 = Time.time;
			float time4 = Time.time;
			float num5 = time3 + 0.25f;
			float y2 = time4 * Frequency;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj2 = num5 ^ 0;
			float x2 = (float)obj2 * Frequency;
			float num6 = Mathf.PerlinNoise(x2, y2);
			float num7 = num6 * Amplitude;
			float num8 = Amplitude * 0.5f;
			float num9 = num7 - num8;
			float time5 = Time.time;
			float time6 = Time.time;
			float y3 = time6 * Frequency;
			float num10 = time5 + 0.5f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj3 = num10 ^ 0;
			float x3 = (float)obj3 * Frequency;
			float num11 = Mathf.PerlinNoise(x3, y3);
			float num12 = num11 * Amplitude;
			float num13 = Amplitude * 0.5f;
			float num14 = num12 - num13;
			_rectTransform.localPosition = (Vector3)(&vector);
		}
		else
		{
			_rectTransform.localPosition = (Vector3)(&vector);
		}
	}
}
