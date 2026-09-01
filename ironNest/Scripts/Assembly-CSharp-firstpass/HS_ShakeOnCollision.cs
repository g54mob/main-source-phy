using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HS_ShakeOnCollision : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CExplosionShockWave_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public HS_ShakeOnCollision _003C_003E4__this;

		private float _003Ctimer_003E5__2;

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
		public _003CExplosionShockWave_003Ed__16(int _003C_003E1__state)
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

	[Space]
	[Header("Camera Shaker script")]
	private HS_CameraShaker cameraShaker;

	public float amplitude;

	public float frequency;

	public float duration;

	public float timeRemaining;

	[Space]
	[Header("Explosion sphere")]
	public float explosionFinalRadious;

	public float explosionCurrentRadious;

	public AnimationCurve sizeCurve;

	public float shockWaveLifetime;

	public float repeatingTime;

	public LayerMask layers;

	private List<Collider> addedColliders;

	[Space]
	[Header("Sound effects")]
	private AudioSource soundComponent;

	private AudioClip explosionClip;

	private void Start()
	{
	}

	public void Update()
	{
	}

	[IteratorStateMachine(typeof(_003CExplosionShockWave_003Ed__16))]
	public IEnumerator ExplosionShockWave()
	{
		return null;
	}

	private void OnDrawGizmosSelected()
	{
	}
}
