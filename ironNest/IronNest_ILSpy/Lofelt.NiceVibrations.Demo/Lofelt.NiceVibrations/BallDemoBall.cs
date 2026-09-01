using System;
using System.Collections;
using Cpp2ILInjected;
using UnityEngine;

namespace Lofelt.NiceVibrations;

public class BallDemoBall : MonoBehaviour
{
	public bool HapticsEnabled = true;

	public ParticleSystem HitParticles;

	public ParticleSystem HitPusherParticles;

	public LayerMask WallMask;

	public LayerMask PusherMask;

	public MMUIShaker LogoShaker;

	public AudioSource EmphasisAudioSource;

	protected Rigidbody2D _rigidBody;

	protected float _lastRaycastTimestamp;

	protected Animator _ballAnimator;

	protected int _hitAnimationParameter;

	protected virtual void Awake()
	{
		GameObject gameObject = base.gameObject;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		Rigidbody2D rigidBody = default(Rigidbody2D);
		_rigidBody = rigidBody;
		GameObject gameObject2 = base.gameObject;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		Animator ballAnimator = default(Animator);
		_ballAnimator = ballAnimator;
		int hitAnimationParameter = Animator.StringToHash("Hit");
		_hitAnimationParameter = hitAnimationParameter;
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected I4, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		GameObject gameObject = collision.gameObject;
		int layer = gameObject.layer;
		int num = layer & 0x1F;
		int num2 = 1 << num;
		object obj = default(object);
		int num3 = obj | num2;
		object obj2 = default(object);
		if ((nint)obj2 == num3)
		{
			HitWall();
		}
	}

	protected unsafe virtual void Update()
	{
		//IL_0028: Expected O, but got Ref
		//IL_0028: Expected O, but got Ref
		//IL_0028: Expected O, but got Ref
		Transform transform = base.transform;
		Vector3 position = transform.position;
		Vector3 vector = default(Vector3);
		Vector2 vector2 = default(Vector2);
		object obj = default(object);
		Debug.DrawLine((Vector3)(&vector), (Vector3)(&vector2), (Color)(&obj));
		float time = Time.time;
		float num = time - _lastRaycastTimestamp;
		if (num > 1f)
		{
			float time2 = Time.time;
			_lastRaycastTimestamp = time2;
			Transform transform2 = base.transform;
			Vector3 position2 = transform2.position;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
			Vector2 vector3 = default(Vector2);
			int layerMask = default(int);
			RaycastHit2D raycastHit2D = Physics2D.Raycast(vector3, vector3, 5f, layerMask);
			RaycastHit2D raycastHit2D2 = default(RaycastHit2D);
			Collider2D collider = raycastHit2D2.collider;
			if (collider != null)
			{
				HitBottom();
			}
		}
	}

	protected virtual void HitBottom()
	{
		Vector2 force = default(Vector2);
		_rigidBody.AddForce(force);
		IEnumerator routine = LogoShaker.Shake(0.2f);
		Coroutine coroutine = StartCoroutine(routine);
	}

	protected virtual void HitWall()
	{
		//IL_0130: Expected I, but got O
		Vector2 linearVelocity = _rigidBody.linearVelocity;
		nint num = (nint)typeof(Math);
		object obj2 = default(object);
		object obj = obj2 * obj2;
		object obj3 = linearVelocity * linearVelocity;
		double d = (double)obj + (double)obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ rcx_v5 (Il2CppClass<System.Math>)+E4]");
		if ((nint)0 <= (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm2\"");
		}
		else
		{
			double num2 = Math.Sqrt(d);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm6,xmm0\"");
		float num3 = 0f / 100f;
		HapticPatterns.PlayEmphasis(num3, 0.7f);
		EmphasisAudioSource.volume = num3;
		IEnumerator routine = LogoShaker.Shake(0.2f);
		Coroutine coroutine = StartCoroutine(routine);
		EmphasisAudioSource.Play();
		_ballAnimator.SetTrigger(_hitAnimationParameter);
	}

	public virtual void HitPusher()
	{
		HitPusherParticles.Play();
		HapticController._fallbackPreset = HapticPatterns.PresetType.Selection;
		HapticPatterns.PlayEmphasis(0.85f, 0.05f);
		EmphasisAudioSource.volume = 0.1f;
		IEnumerator routine = LogoShaker.Shake(0.2f);
		Coroutine coroutine = StartCoroutine(routine);
		EmphasisAudioSource.Play();
		_ballAnimator.SetTrigger(_hitAnimationParameter);
	}
}
