using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/FireBall")]
public class FireBall : BlockBehaviour
{
	public float radius = 5f;

	public float collideCutoff = 500f;

	public ParticleSystem bubbles;

	private Vector3 explosionPos;

	private Collider[] hitColliders;

	private List<Rigidbody> prevRigidbodies = new List<Rigidbody>();

	private Rigidbody colAttachedRigidbody;

	private Color emissColor;

	private Color rimColor;

	private float rate = 80f;

	public bool hasIgnited;

	public float pct = 1f;

	[HideInInspector]
	public bool _onFire;

	[SerializeField]
	[HideInInspector]
	protected float pst;

	protected override void Start()
	{
		base.Start();
		MeshRenderer meshRenderer = VisualController.renderers[0];
		if (meshRenderer.sharedMaterial.HasProperty("_RimColor"))
		{
			rimColor = meshRenderer.sharedMaterial.GetColor("_RimColor");
		}
		if (meshRenderer.sharedMaterial.HasProperty("_EmissionColor"))
		{
			emissColor = meshRenderer.sharedMaterial.GetColor("_EmissionColor");
		}
		ParticleSystem fireParticles = fireTag.fireControllerCode.fireParticles;
		if (!isSimulating)
		{
			ParticleSystem.EmissionModule emission = fireParticles.emission;
			rate = emission.rate.constant;
			emission.rate = rate * 0.25f;
			if (!WaterController.Exist)
			{
				_onFire = true;
				if (!fireParticles.isPlaying)
				{
					fireParticles.Play();
				}
			}
			return;
		}
		ParticleSystem.EmissionModule emission2 = fireParticles.emission;
		emission2.rate = rate;
		base.InWater = !_onFire;
		if (_onFire)
		{
			fireParticles.Simulate(pst);
			fireParticles.Play();
		}
		else
		{
			fireParticles.Stop();
		}
		if (!WaterController.Exist)
		{
			Object.Destroy(bubbles.gameObject);
		}
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates(Prefab.RegisterSimUpdate, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, Prefab.RegisterEmulationUpdate);
	}

	public override void LateUpdateBlock()
	{
		base.LateUpdate();
		if (isSimulating)
		{
			float num = pct * (1f - base.GetSubmergedPctMV * 0.5f);
			VisualController.AssignMaterialProperty("_RimPower", 2.5f + (1f - num) * 5f, false);
			VisualController.AssignMaterialProperty("_RimColor", num * rimColor, false);
			VisualController.AssignMaterialProperty("_EmissionColor", (num * 0.8f + 0.2f) * emissColor);
			if (fireTag.burning)
			{
				pct += Time.deltaTime;
			}
			else
			{
				pct -= Time.deltaTime;
			}
			pct = Mathf.Clamp01(pct);
		}
		else
		{
			if (StatMaster.startingMachines)
			{
				return;
			}
			ParticleSystem fireParticles = fireTag.fireControllerCode.fireParticles;
			if (WaterController.Exist)
			{
				Vector3 pos = GetCenter();
				if (!WaterController.IsUnderwater(pos))
				{
					if (!fireParticles.isPlaying)
					{
						_onFire = true;
						fireParticles.Play();
					}
				}
				else if (fireParticles.isPlaying)
				{
					_onFire = false;
					fireParticles.Stop();
				}
			}
			pst = fireParticles.time;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (SimPhysics && isSimulating && other.relativeVelocity.sqrMagnitude > collideCutoff)
		{
			Explode();
		}
	}

	public void Explode()
	{
		if (!SimPhysics || !_onFire || base.InWater)
		{
			return;
		}
		explosionPos = base.transform.position;
		LayerMask layerMask = AddPiece.CreateLayerMask(new int[13]
		{
			0, 12, 14, 15, 16, 17, 18, 24, 25, 26,
			28, 29, 31
		});
		hitColliders = Physics.OverlapSphere(explosionPos, radius, layerMask);
		int num = 0;
		Collider[] array = hitColliders;
		foreach (Collider collider in array)
		{
			if (!collider.attachedRigidbody || !(collider.attachedRigidbody != Rigidbody) || prevRigidbodies.Contains(collider.attachedRigidbody))
			{
				continue;
			}
			colAttachedRigidbody = collider.attachedRigidbody;
			int mask = ((!fireTag.fireControllerCode.onFire) ? 2 : 18);
			IEnumerable<IExplosionEffect> interfaces = ReferenceMaster.GetInterfaces<IExplosionEffect>(colAttachedRigidbody.gameObject);
			foreach (IExplosionEffect item in interfaces)
			{
				item.OnExplode(1f, 0f, 10f, explosionPos, radius, mask, false);
			}
			num++;
		}
		if (!hasIgnited)
		{
			hasIgnited = num > 0;
		}
		prevRigidbodies.Clear();
	}
}
