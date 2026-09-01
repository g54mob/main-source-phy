using System;
using UnityEngine;

public class SquashOnTriggerEnter : SimBehaviour
{
	public Transform visToScale;

	public float startScale = 0.3f;

	public float squashScale = 0.3f;

	public int triggerCount;

	public float targetScale = 1f;

	public float lerpSpeed = 6f;

	public RandomSoundController randomSFX;

	public RandomSoundController chopSFX;

	public float gibVelocity = 50f;

	public float gibAngularVelocity = 50f;

	public MeshFilter meshFilter;

	public Mesh choppedMesh;

	public ParticleSystem gibParticles;

	public bool chopped;

	public bool triggerDestroy;

	protected override void Start()
	{
		base.Start();
		visToScale.localScale = new Vector3(visToScale.localScale.x, visToScale.localScale.y * UnityEngine.Random.Range(0.9f, 1.2f), visToScale.localScale.z);
		startScale = visToScale.localScale.y;
		lerpSpeed *= UnityEngine.Random.Range(0.8f, 1.4f);
		squashScale *= UnityEngine.Random.Range(0.8f, 1.4f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (base.SimPhysics && base.isSimulating && (bool)other.attachedRigidbody && (bool)other.attachedRigidbody.GetComponent<MyBounds>())
		{
			triggerCount++;
			randomSFX.Play();
			Rigidbody attachedRigidbody = other.attachedRigidbody;
			if (!chopped && (bool)attachedRigidbody && (attachedRigidbody.velocity.sqrMagnitude > gibVelocity || attachedRigidbody.angularVelocity.sqrMagnitude > gibAngularVelocity))
			{
				chopped = true;
				Gib();
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if ((bool)other.attachedRigidbody && (bool)other.attachedRigidbody.GetComponent<MyBounds>())
		{
			triggerCount--;
		}
	}

	public void Gib()
	{
		if (StatMaster.isMP && base.SimPhysics)
		{
			NetworkBlock component = GetComponent<NetworkBlock>();
			if (component != null)
			{
				component.Event(NetworkEntity.EntityEvent.Kill);
				if (triggerDestroy)
				{
					LevelEntity levelEntity = component as LevelEntity;
					levelEntity.TriggerEvent(TriggerType.Destroy);
				}
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
		GetComponent<AudioSource>().volume = 0.35f;
		meshFilter.sharedMesh = choppedMesh;
		GetComponent<Collider>().enabled = false;
		chopSFX.Play();
		targetScale = startScale;
		visToScale.localScale = new Vector3(visToScale.localScale.x, startScale, visToScale.localScale.z);
		gibParticles.Play();
		AddToPercentageBar();
	}

	private void AddToPercentageBar()
	{
		if (!StatMaster.isMP && base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.currentObjsCompleted++;
		}
	}

	private void Update()
	{
		targetScale = ((!((float)triggerCount > 0f)) ? startScale : squashScale);
		if (!chopped)
		{
			visToScale.localScale = new Vector3(visToScale.localScale.x, Mathf.Lerp(visToScale.localScale.y, targetScale, Time.deltaTime * lerpSpeed), visToScale.localScale.z);
		}
	}
}
