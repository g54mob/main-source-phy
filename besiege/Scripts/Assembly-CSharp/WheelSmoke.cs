using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Blocks/WheelSmoke")]
public class WheelSmoke : SoundOnCollide
{
	[NonSerialized]
	public bool smokeActive;

	[SerializeField]
	protected float wheelRadius = 1f;

	[SerializeField]
	protected float speedCutoff = 0.5f;

	[SerializeField]
	protected BlockBehaviour block;

	[SerializeField]
	protected ParticleSystem particleSystemy;

	private bool systemActive;

	private List<GameObject> rbList = new List<GameObject>();

	private Vector3 particlePos = Vector3.zero;

	private Transform blockTransform;

	private Transform particleObj;

	private int groundedCounter;

	private float radiusSqr = 1f;

	private Vector3 lastContact;

	private float contactTime;

	protected override void Awake()
	{
		base.Awake();
		if (block.isSimulating)
		{
			wheelRadius *= base.transform.localScale.x;
			radiusSqr = wheelRadius * wheelRadius * 0.75f;
			blockTransform = block.transform;
			particleObj = particleSystemy.transform;
		}
		else if (StatMaster.GetCurrentIsland() == Island.Krolmar)
		{
			ParticleSystemRenderer component = particleSystemy.GetComponent<ParticleSystemRenderer>();
			component.material.SetColor("_TintColor", new Color(0.31f, 0.26f, 0.211f, 0.33f));
		}
	}

	private void Update()
	{
		if (StatMaster.isHeadless)
		{
			return;
		}
		bool flag;
		if (block.SimPhysics)
		{
			for (int num = rbList.Count - 1; num >= 0; num--)
			{
				if (rbList[num] == null || !rbList[num].activeInHierarchy)
				{
					rbList.RemoveAt(num);
					groundedCounter--;
				}
			}
			if (groundedCounter < 0)
			{
				groundedCounter = 0;
			}
			Vector3 velocity = block.Rigidbody.velocity;
			flag = (groundedCounter > 0 || Time.time < contactTime + 0.25f) && velocity.sqrMagnitude > speedCutoff;
		}
		else
		{
			flag = smokeActive;
		}
		if (flag)
		{
			particlePos = blockTransform.position + lastContact;
			particleObj.position = particlePos;
			if (!systemActive)
			{
				particleSystemy.Play();
				smokeActive = true;
				systemActive = true;
			}
		}
		else if (systemActive)
		{
			particleSystemy.Stop();
			smokeActive = false;
			systemActive = false;
			base.enabled = false;
		}
	}

	public void ToggleSmoke(bool toggle)
	{
		smokeActive = toggle;
		if (toggle)
		{
			base.enabled = true;
		}
	}

	protected override void OnCollisionEnter(Collision other)
	{
		if (!block.isSimulating)
		{
			return;
		}
		if (!block.noRigidbody)
		{
			Vector3 normal = other.contacts[0].normal;
			Vector3 normalized = other.relativeVelocity.normalized;
			if (Mathf.Abs(Vector3.Dot(normal, normalized)) > 0.707f)
			{
				base.OnCollisionEnter(other);
			}
			else if (block.Rigidbody.velocity.sqrMagnitude < 200f && Mathf.Abs(Vector3.Dot(normalized, base.transform.forward)) > 0.707f)
			{
				base.OnCollisionEnter(other);
			}
		}
		Rigidbody rigidbody = other.rigidbody;
		GameObject item = other.collider.gameObject;
		if (rigidbody != null)
		{
			item = rigidbody.gameObject;
		}
		bool flag = false;
		for (int i = 0; i < other.contacts.Length; i++)
		{
			Vector3 vector = other.contacts[i].point - blockTransform.position;
			if (vector.sqrMagnitude > radiusSqr)
			{
				if (other.collider.gameObject.layer == 29 || other.collider.gameObject.layer == 24 || Time.time > contactTime + 0.1f)
				{
					lastContact = vector;
				}
				flag = true;
				break;
			}
		}
		if (flag)
		{
			rbList.Add(item);
			groundedCounter++;
			contactTime = Time.time;
			if (!systemActive)
			{
				base.enabled = true;
			}
		}
	}

	private void OnCollisionExit(Collision other)
	{
		if (block.isSimulating)
		{
			Rigidbody rigidbody = other.rigidbody;
			GameObject item = other.collider.gameObject;
			if (rigidbody != null)
			{
				item = rigidbody.gameObject;
			}
			if (rbList.Contains(item))
			{
				rbList.Remove(item);
				groundedCounter--;
			}
			if (groundedCounter < 0)
			{
				groundedCounter = 0;
			}
		}
	}
}
