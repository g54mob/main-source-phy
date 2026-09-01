using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HS_CameraShaker : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CShake_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float wait;

		public HS_CameraShaker _003C_003E4__this;

		public float amp;

		public float freq;

		public float dur;

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
		public _003CShake_003Ed__9(int _003C_003E1__state)
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
	}

	[IteratorStateMachine(typeof(_003CShake_003Ed__9))]
	public IEnumerator Shake(float amp, float freq, float dur, float wait)
	{
		return null;
	}

	private void Update()
	{
	}

	private void LateUpdate()
	{
	}
}
