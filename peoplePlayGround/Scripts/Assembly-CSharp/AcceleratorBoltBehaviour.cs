using UnityEngine;

public class AcceleratorBoltBehaviour : BaseBoltBehaviour
{
	public float EnergyLevel;

	public float CameraShakeAmount;

	public GameObject ImpactEffect;

	public GameObject LargeImpactEffect;

	public float ImpactStrength;

	public float AoERadius = 1f;

	public float ActivationDelay = 0.5f;

	[SkipSerialisation]
	public LayerMask ImmobiltyFieldLayer;

	[SkipSerialisation]
	public float ImmobilityFieldSlowdown = 0.8f;

	private float speedMultiplier = 1f;

	private static readonly Collider2D[] buffer = new Collider2D[32];

	private PhysicalBehaviour hitPhys;

	private float timeAlive;

	public override float GetSpeedMultiplier()
	{
		return speedMultiplier;
	}

	protected override void Update()
	{
		base.Update();
		timeAlive += Time.deltaTime;
	}

	private void FixedUpdate()
	{
		CameraShakeBehaviour.main.Shake(CameraShakeAmount, base.transform.position);
		if ((bool)Physics2D.OverlapPoint(base.transform.position, ImmobiltyFieldLayer))
		{
			speedMultiplier *= ImmobilityFieldSlowdown;
		}
		else
		{
			speedMultiplier = 1f;
		}
		if (!(timeAlive > ActivationDelay))
		{
			return;
		}
		int num = Physics2D.OverlapCircleNonAlloc(base.transform.position, AoERadius, buffer, Layers);
		for (int i = 0; i < num; i++)
		{
			Collider2D collider2D = buffer[i];
			if (Global.main.PhysicalObjectsInWorldByTransform.TryGetValue(collider2D.transform, out hitPhys))
			{
				Vector3 vector = collider2D.transform.position - base.transform.position;
				float magnitude = vector.magnitude;
				vector /= magnitude;
				vector /= magnitude * magnitude;
				hitPhys.rigidbody.AddForce(0.15f * EnergyLevel * vector);
				hitPhys.Charge = Mathf.Max(EnergyLevel, hitPhys.Charge);
				hitPhys.Temperature += EnergyLevel;
			}
		}
		if (Random.value > 0.8f)
		{
			SimpleLightningEmitterBehaviour.Instance.Emit(base.transform.position, Random.insideUnitCircle, AoERadius / 2f, 0.04f, OnLightningHit, Color.yellow, Layers, 3f, (int)EnergyLevel + 25, 0.07f, 0.03f);
		}
	}

	private static void OnLightningHit(PhysicalBehaviour phys, Vector2 point)
	{
		if (!float.IsNaN(point.x) && !float.IsNaN(point.y))
		{
			phys.Charge += 20f;
			phys.Temperature += 100f;
			ExplosionCreator.Explode(point, 2f);
		}
	}

	public override bool ShouldReflect(RaycastHit2D hit)
	{
		return Global.main.EnergyLayers.HasLayer(hit.collider.gameObject.layer);
	}

	protected override void OnHit(RaycastHit2D hit)
	{
		Vector2 normal = hit.normal;
		Object.Instantiate((EnergyLevel > 100f) ? LargeImpactEffect : ImpactEffect, hit.point, Quaternion.identity).transform.up = normal;
		ExplosionCreator.CreatePulseExplosion(hit.point, ImpactStrength, AoERadius / 2f, soundAndEffects: false);
		int num = Physics2D.OverlapCircleNonAlloc(hit.point, AoERadius, buffer, Layers);
		for (int i = 0; i < num; i++)
		{
			Collider2D collider2D = buffer[i];
			if (Global.main.PhysicalObjectsInWorldByTransform.TryGetValue(collider2D.transform, out hitPhys))
			{
				if (Random.value < EnergyLevel / 20f)
				{
					hitPhys.SendMessage("Break", -normal * ImpactStrength, SendMessageOptions.DontRequireReceiver);
				}
				hitPhys.Charge = Mathf.Max(EnergyLevel, hitPhys.Charge);
				hitPhys.Temperature += EnergyLevel;
				hitPhys.rigidbody.AddForce(1.2f * EnergyLevel * base.transform.right, ForceMode2D.Impulse);
			}
		}
	}
}
