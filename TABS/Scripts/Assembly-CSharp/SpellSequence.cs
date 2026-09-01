using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpellSequence : MonoBehaviour, GameObjectPooling.IPoolable
{
	public bool playOnAwake = true;

	public UnityEvent spellEvent;

	public SpellSequenceInstance[] instances;

	public float interval = 0.1f;

	public float spacing = 0.5f;

	public float rayCastHeight = 1f;

	public LayerMask rayCastMask;

	private Explosion explosion;

	private AddScreensShake shake;

	public float initialForwardJump;

	private ParticleSystem[] parts;

	private float originalShakeAmount;

	private float originalExplosionRadius;

	private float[] originalParticleSizes;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Start()
	{
		parts = GetComponentsInChildren<ParticleSystem>();
		explosion = GetComponentInChildren<Explosion>();
		shake = GetComponentInChildren<AddScreensShake>();
		originalShakeAmount = shake.amount;
		originalExplosionRadius = explosion.radius;
		originalParticleSizes = new float[parts.Length];
		for (int i = 0; i < parts.Length; i++)
		{
			ParticleSystem.MainModule main = parts[i].main;
			originalParticleSizes[i] = main.startSize.constant;
		}
		if (!IsManagedByPool)
		{
			InitializeOnSpawn();
		}
	}

	public void Go()
	{
		StartCoroutine(PlaySpell());
	}

	public void Initialize()
	{
		InitializeOnSpawn();
	}

	public void Reset()
	{
	}

	public void Release()
	{
		for (int i = 0; i < parts.Length; i++)
		{
			parts[i].Clear();
		}
		ResetScales();
	}

	private void InitializeOnSpawn()
	{
		base.transform.position += initialForwardJump * base.transform.forward;
		if (playOnAwake)
		{
			Go();
		}
	}

	private IEnumerator PlaySpell()
	{
		for (int i = 0; i < instances.Length; i++)
		{
			yield return new WaitForSeconds(interval);
			Vector3 vector = RayCastPos(base.transform.position);
			if (vector != base.transform.position && vector != Vector3.zero)
			{
				base.transform.position = vector;
				FixScales(i);
				spellEvent.Invoke();
			}
		}
	}

	private void FixScales(int id)
	{
		for (int i = 0; i < parts.Length; i++)
		{
			ParticleSystem.MainModule main = parts[i].main;
			float num = main.startSize.constant;
			main.startSize = instances[id].sizeMultiplier * num;
		}
		explosion.radius *= instances[id].sizeMultiplier;
		shake.amount *= instances[id].sizeMultiplier + instances[id].extraShakeMultiplier;
	}

	private void ResetScales()
	{
		explosion.radius = originalExplosionRadius;
		shake.amount = originalShakeAmount;
		for (int i = 0; i < parts.Length; i++)
		{
			ParticleSystem.MainModule main = parts[i].main;
			main.startSize = originalParticleSizes[i];
		}
	}

	private Vector3 RayCastPos(Vector3 pos)
	{
		pos += Vector3.up * rayCastHeight;
		pos += base.transform.forward * spacing;
		Physics.Raycast(new Ray(pos, Vector3.down), out var hitInfo, 10f, rayCastMask);
		if ((bool)hitInfo.transform)
		{
			pos = hitInfo.point;
			return pos;
		}
		return Vector3.zero;
	}
}
