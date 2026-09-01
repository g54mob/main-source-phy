using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
	public class DamageAmount
	{
		[Tooltip("Miniman Velocity for this unit to take Damage through Collisions")]
		public float minimalVelocity = 100f;

		[Tooltip("Max damage from a singe block through Collisions")]
		public float maxDamage = 100f;

		[Tooltip("Amount of Damage per Second from Fire")]
		public float fireDamage = 250f;

		[Tooltip("Cannon/Rocket Damage Multiplier")]
		public float ProjectileScale = 0.25f;

		[Tooltip("Fire Damage Multiplier")]
		public float FireScale = 1f;

		[Tooltip("Sharp Damage Multiplier")]
		public float SharpScale = 1f;

		[Tooltip("Blunt Damage Multiplier")]
		public float BluntScale = 1f;
	}

	public class GateEnforcement
	{
		public GameObject gameObject;

		public float health = 3333f;

		public bool isDestroyed;

		public void Destroy()
		{
			gameObject.SetActive(false);
			isDestroyed = true;
		}
	}

	[HideInInspector]
	public GateEnforcement[] enforcements;

	[HideInInspector]
	public GateEnforcement currentEnforcement;

	[HideInInspector]
	public List<Rigidbody> drills = new List<Rigidbody>();

	[HideInInspector]
	public List<Rigidbody> sawDisc = new List<Rigidbody>();

	public FireController fireController;

	public GameObject[] enforcementObjects;

	public HingeJoint[] hinges;

	public Material material;

	public DamageAmount dmc = new DamageAmount();

	private Rigidbody rb;

	private bool invaulnable;

	private void OnEnable()
	{
		ResetMaterial();
	}

	private void Start()
	{
		if (StatMaster.levelSimulating)
		{
			if (rb == null)
			{
				rb = GetComponent<Rigidbody>();
			}
			enforcements = new GateEnforcement[enforcementObjects.Length];
			for (int i = 0; i < enforcementObjects.Length; i++)
			{
				enforcements[i] = new GateEnforcement();
				enforcements[i].gameObject = enforcementObjects[i];
			}
			currentEnforcement = enforcements[0];
		}
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		UpdatePresistentDamage();
		UpdateFire();
		UpdateEnforcement();
		if (currentEnforcement.isDestroyed)
		{
			WinCondition.currentObjsCompleted++;
			HingeJoint[] array = hinges;
			foreach (HingeJoint hingeJoint in array)
			{
				hingeJoint.useLimits = false;
				hingeJoint.useSpring = false;
			}
			base.enabled = false;
		}
	}

	public void ApplyDamage(float damage, GateEnforcement target)
	{
		target.health -= Mathf.Clamp(damage, 0f, dmc.maxDamage) * ((!invaulnable) ? 1f : 0.2f);
		if (target.health <= 0f)
		{
			target.Destroy();
			StartCoroutine(SetInvaulnable());
		}
		UpdateMaterial();
	}

	private void UpdatePresistentDamage()
	{
		for (int i = 0; i < drills.Count; i++)
		{
			ApplyDamage(150f * dmc.SharpScale * Time.deltaTime, currentEnforcement);
		}
		for (int j = 0; j < sawDisc.Count; j++)
		{
			ApplyDamage(200f * dmc.SharpScale * Time.deltaTime, currentEnforcement);
		}
	}

	private void UpdateEnforcement()
	{
		if (!currentEnforcement.isDestroyed)
		{
			return;
		}
		for (int i = 0; i < enforcements.Length; i++)
		{
			if (!enforcements[i].isDestroyed)
			{
				currentEnforcement = enforcements[i];
			}
		}
	}

	private void UpdateFire()
	{
		if (!fireController.onFire)
		{
			return;
		}
		for (int i = 0; i < enforcements.Length; i++)
		{
			if (!enforcements[i].isDestroyed)
			{
				ApplyDamage(dmc.fireDamage * dmc.FireScale * Time.deltaTime, enforcements[i]);
			}
		}
	}

	private IEnumerator SetInvaulnable()
	{
		invaulnable = true;
		yield return new WaitForSeconds(1.5f);
		invaulnable = false;
	}

	private void UpdateMaterial()
	{
		if (material.HasProperty("_DamageAmount"))
		{
			float num = 0f;
			for (int i = 0; i < enforcements.Length; i++)
			{
				num += ((!enforcements[i].isDestroyed) ? enforcements[i].health : 0f);
			}
			material.SetFloat("_DamageAmount", 1f - num / 10000f);
		}
	}

	private void ResetMaterial()
	{
		if (material.HasProperty("_DamageAmount"))
		{
			material.SetFloat("_DamageAmount", 0f);
		}
	}
}
