using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class HS_ShakeOnCollision : MonoBehaviour
{
	private sealed class _003CExplosionShockWave_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public HS_ShakeOnCollision _003C_003E4__this;

		private float _003Ctimer_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CExplosionShockWave_003Ed__16(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_00c3: Expected I4, but got I8
			//IL_0015: Expected O, but got I4
			//IL_007e: Expected I4, but got I8
			//IL_0052: Expected I4, but got I8
			//IL_0216: Invalid comparison between I4 and F4
			//IL_0273: Expected F4, but got I4
			//IL_02b4: Expected O, but got Ref
			//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cb: Expected O, but got Unknown
			//IL_02d5: Expected F4, but got I4
			//IL_02df: Expected F4, but got I4
			//IL_0574: Invalid comparison between F4 and I4
			//IL_02ee: Invalid comparison between F4 and I4
			//IL_056a: Expected I4, but got O
			//IL_0463: Unknown result type (might be due to invalid IL or missing references)
			//IL_0468: Expected O, but got Unknown
			HS_ShakeOnCollision hS_ShakeOnCollision = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			float num;
			float deltaTime;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj == 1)
					{
						_003C_003E1__state = -1;
						num = _003Ctimer_003E5__2;
						deltaTime = Time.deltaTime;
						goto IL_01c5;
					}
				}
				else
				{
					_003C_003E1__state = -1;
					IEnumerator routine = hS_ShakeOnCollision.ExplosionShockWave();
					Coroutine coroutine = hS_ShakeOnCollision.StartCoroutine(routine);
				}
				return false;
			}
			_003C_003E1__state = -1;
			_003Ctimer_003E5__2 = 0f;
			List<Collider> addedColliders = hS_ShakeOnCollision.addedColliders;
			int version = addedColliders._version + 1;
			addedColliders._version = version;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj2 = default(object);
			if (obj2 == null)
			{
				addedColliders._size = 0;
			}
			else
			{
				addedColliders._size = 0;
				if (addedColliders._size > 0)
				{
					Array.Clear(addedColliders._items, 0, addedColliders._size);
				}
			}
			hS_ShakeOnCollision.soundComponent.PlayOneShot(hS_ShakeOnCollision.explosionClip);
			num = _003Ctimer_003E5__2;
			deltaTime = Time.deltaTime;
			goto IL_01c5;
			IL_01c5:
			float num2 = deltaTime / hS_ShakeOnCollision.shockWaveLifetime;
			float time = (_003Ctimer_003E5__2 = num2 + num);
			float num3 = hS_ShakeOnCollision.sizeCurve.Evaluate(time);
			if (!(0f > num3))
			{
				bool flag2 = !(num3 > 1f);
				float num4 = 1f;
				if (!flag2)
				{
					num4 = 1f;
					num3 = 1f;
				}
			}
			else
			{
				num3 = 0f;
			}
			float explosionCurrentRadious = hS_ShakeOnCollision.explosionFinalRadious * num3;
			hS_ShakeOnCollision.explosionCurrentRadious = explosionCurrentRadious;
			Transform transform = hS_ShakeOnCollision.transform;
			Vector3 position = transform.position;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
			HS_CameraShaker cameraShaker = default(HS_CameraShaker);
			int layerMask = default(int);
			Collider[] array = Physics.OverlapSphere((Vector3)(&cameraShaker), hS_ShakeOnCollision.explosionCurrentRadious, layerMask, QueryTriggerInteraction.UseGlobal);
			object obj3 = array + 32;
			float num5 = 0f;
			float num6 = 0f;
			UnityEngine.Object obj4 = default(UnityEngine.Object);
			UnityEngine.Object obj5 = default(UnityEngine.Object);
			AudioSource audioSource = default(AudioSource);
			float wait = default(float);
			float seconds = default(float);
			while (true)
			{
				if (num6 < (float)array.Length)
				{
					if (!(num5 < (float)array.Length))
					{
						break;
					}
					if (!hS_ShakeOnCollision.addedColliders.Contains((Collider)obj3))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
						if (obj4 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
							if ((bool)obj5)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
								AudioClip clip = audioSource.clip;
								audioSource.PlayOneShot(clip);
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
								hS_ShakeOnCollision.cameraShaker = cameraShaker;
								num3 = hS_ShakeOnCollision.timeRemaining;
								IEnumerator routine2 = hS_ShakeOnCollision.cameraShaker.Shake(hS_ShakeOnCollision.amplitude, hS_ShakeOnCollision.frequency, hS_ShakeOnCollision.duration, wait);
								Coroutine coroutine2 = hS_ShakeOnCollision.StartCoroutine(routine2);
							}
						}
						hS_ShakeOnCollision.addedColliders.Add((Collider)obj3);
					}
					num5++;
					obj3 += 8;
					num6 = num5;
					continue;
				}
				if (hS_ShakeOnCollision.explosionCurrentRadious < hS_ShakeOnCollision.explosionFinalRadious)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
				}
				else
				{
					WaitForSeconds waitForSeconds = new WaitForSeconds(seconds);
					seconds = hS_ShakeOnCollision.repeatingTime - hS_ShakeOnCollision.shockWaveLifetime;
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
				}
				return true;
			}
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

	private HS_CameraShaker cameraShaker;

	public float amplitude;

	public float frequency;

	public float duration;

	public float timeRemaining;

	public float explosionFinalRadious = 850f;

	public float explosionCurrentRadious;

	public AnimationCurve sizeCurve;

	public float shockWaveLifetime = 6f;

	public float repeatingTime = 15f;

	public LayerMask layers;

	private List<Collider> addedColliders;

	private AudioSource soundComponent;

	private AudioClip explosionClip;

	private void Start()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		AudioSource audioSource = default(AudioSource);
		soundComponent = audioSource;
		AudioClip clip = soundComponent.clip;
		explosionClip = clip;
		_003CExplosionShockWave_003Ed__16 obj = new _003CExplosionShockWave_003Ed__16(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	public void Update()
	{
	}

	public IEnumerator ExplosionShockWave()
	{
		_003CExplosionShockWave_003Ed__16 obj = new _003CExplosionShockWave_003Ed__16(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private unsafe void OnDrawGizmosSelected()
	{
		//IL_0009: Expected O, but got Ref
		//IL_0034: Expected O, but got Ref
		object obj = default(object);
		Gizmos.color = (Color)(&obj);
		Transform transform = base.transform;
		Vector3 position = transform.position;
		object obj2 = default(object);
		Gizmos.DrawWireSphere((Vector3)(&obj2), explosionCurrentRadious);
	}

	public HS_ShakeOnCollision()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		LayerMask layerMask = default(LayerMask);
		layers = layerMask;
		List<Collider> list = new List<Collider>();
		addedColliders = list;
		base._002Ector();
	}
}
