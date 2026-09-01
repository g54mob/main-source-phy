using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class HS_CameraShaker : MonoBehaviour
{
	private sealed class _003CShake_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float wait;

		public HS_CameraShaker _003C_003E4__this;

		public float amp;

		public float freq;

		public float dur;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CShake_003Ed__9(int _003C_003E1__state)
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
			//IL_017c: Expected I4, but got O
			//IL_00b4: Expected O, but got F4
			HS_CameraShaker hS_CameraShaker = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				WaitForSeconds waitForSeconds = new WaitForSeconds(wait);
				_003C_003E2__current = waitForSeconds;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				float num = UnityEngine.Random.Range(0f, 32f);
				hS_CameraShaker.noiseOffset = (Vector3)num;
				float num2 = UnityEngine.Random.Range(0f, 32f);
				float num3 = UnityEngine.Random.Range(0f, 32f);
				hS_CameraShaker.amplitude = amp;
				hS_CameraShaker.frequency = freq;
				hS_CameraShaker.duration = dur;
				if ((hS_CameraShaker.timeRemaining += dur) > dur)
				{
					hS_CameraShaker.timeRemaining = dur;
				}
			}
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

	public Transform cameraObject;

	public float amplitude;

	public float frequency;

	public float duration;

	public float timeRemaining;

	private Vector3 noiseOffset;

	private Vector3 noise;

	private AnimationCurve smoothCurve;

	private void Start()
	{
		//IL_001d: Expected O, but got F4
		float num = UnityEngine.Random.Range(0f, 32f);
		noiseOffset = (Vector3)num;
		float num2 = UnityEngine.Random.Range(0f, 32f);
		float num3 = UnityEngine.Random.Range(0f, 32f);
	}

	public IEnumerator Shake(float amp, float freq, float dur, float wait)
	{
		_003CShake_003Ed__9 obj = new _003CShake_003Ed__9(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.amp = amp;
		obj.freq = freq;
		obj.dur = dur;
		float wait2 = default(float);
		obj.wait = wait2;
		return obj;
	}

	private void Update()
	{
		//IL_0010: Invalid comparison between I4 and F4
		//IL_00be: Expected O, but got F4
		//IL_00e4: Expected O, but got F4
		//IL_00fa: Expected F4, but got I
		//IL_0119: Expected F4, but got I
		//IL_0171: Expected I, but got O
		if (0f < timeRemaining)
		{
			float deltaTime = Time.deltaTime;
			float num = timeRemaining - deltaTime;
			float num2 = deltaTime * frequency;
			timeRemaining = num;
			float num3 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (HS_CameraShaker)+40]");
			float num4 = num3 + 0f;
			float num5 = num2 + (float)noiseOffset;
			float num6 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (HS_CameraShaker)+3C]");
			float num7 = num6 + 0f;
			noiseOffset = (Vector3)num5;
			float num8 = Mathf.PerlinNoise(num5, 0f);
			noise = (Vector3)num8;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (HS_CameraShaker)+3C]");
			float num9 = Mathf.PerlinNoise(0f, 1f);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (HS_CameraShaker)+40]");
			float num10 = Mathf.PerlinNoise(0f, 2f);
			nint num11 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v93 @ rax_v4 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num12 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rcx_v4 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
			float num13 = 0f * 0.5f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (HS_CameraShaker)+4C]");
			float num14 = 0f - num13;
			Vector3 vector = default(Vector3);
			noise = vector;
			float num15 = amplitude * num14;
			float num16 = timeRemaining / duration;
			float time = 1f - num16;
			float num17 = smoothCurve.Evaluate(time);
			float num18 = num15 * num17;
			noise = vector;
		}
	}

	private unsafe void LateUpdate()
	{
		//IL_000b: Invalid comparison between I4 and F4
		//IL_006b: Expected O, but got Ref
		//IL_0091: Expected O, but got Ref
		if (0f < timeRemaining)
		{
			Transform transform = cameraObject.transform;
			Vector3 vector = default(Vector3);
			transform.localPosition = (Vector3)(&vector);
			Transform transform2 = cameraObject.transform;
			transform2.localEulerAngles = (Vector3)(&vector);
		}
	}

	public HS_CameraShaker()
	{
		Keyframe[] keys = new Keyframe[3];
		float outTangent = default(float);
		Keyframe keyframe = new Keyframe(0f, 0f, 0f, outTangent);
		_ = 0;
		_ = 0;
		_ = 0;
		Keyframe keyframe2 = new Keyframe(0.2f, 1f);
		_ = 0;
		_ = 0;
		_ = 0;
		Keyframe keyframe3 = new Keyframe(1f, 0f);
		_ = 0;
		_ = 0;
		_ = 0;
		smoothCurve = new AnimationCurve(keys);
		base._002Ector();
	}
}
