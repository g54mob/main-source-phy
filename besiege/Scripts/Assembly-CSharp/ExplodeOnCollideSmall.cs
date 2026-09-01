using UnityEngine;

public class ExplodeOnCollideSmall : SimBehaviour
{
	public float radius = 5f;

	public float power = 10f;

	public float torquePower = 1000f;

	public float upPower = 6f;

	public Renderer bombVis;

	public Transform explosionEffectPrefab;

	public bool hasExploded;

	public bool explodeOnCollision;

	public float collideCutoff = 10f;

	public Transform parentObj;

	private Rigidbody prevRigidbody;

	private Vector3 explosionPos;

	private Collider[] colliders;

	private Rigidbody myAttachedRigidbody;

	private bool simPhys;

	private bool isSim;

	protected override void Start()
	{
		base.Start();
		simPhys = base.SimPhysics;
		isSim = base.isSimulating;
	}

	private void Explodey()
	{
		GameObject gameObject = Object.Instantiate(explosionEffectPrefab.gameObject, base.transform.position, Quaternion.identity, ReferenceMaster.physicsGoalInstance) as GameObject;
		if (!gameObject.activeSelf)
		{
			gameObject.SetActive(true);
		}
		hasExploded = true;
		if (!simPhys || StatMaster.Rules.DisableExplosions)
		{
			return;
		}
		Rigidbody component = GetComponent<Rigidbody>();
		if (component != null)
		{
			component.isKinematic = true;
		}
		explosionPos = base.transform.position;
		colliders = Physics.OverlapSphere(explosionPos, radius);
		Collider[] array = colliders;
		foreach (Collider collider in array)
		{
			if (collider == null)
			{
				continue;
			}
			if (collider.attachedRigidbody != null)
			{
				myAttachedRigidbody = collider.attachedRigidbody;
			}
			if (myAttachedRigidbody != null && myAttachedRigidbody != prevRigidbody && myAttachedRigidbody != GetComponent<Rigidbody>() && myAttachedRigidbody.gameObject.layer != 22 && myAttachedRigidbody.tag != "KeepConstraintsAlways")
			{
				myAttachedRigidbody.WakeUp();
				myAttachedRigidbody.constraints = RigidbodyConstraints.None;
				myAttachedRigidbody.AddExplosionForce(power, explosionPos, radius, upPower);
				myAttachedRigidbody.AddRelativeTorque(Random.insideUnitSphere.normalized * torquePower);
				ExplodeMultiplier component2 = myAttachedRigidbody.gameObject.GetComponent<ExplodeMultiplier>();
				if ((bool)component2)
				{
					component2.Explodey(power, explosionPos, radius, upPower);
				}
				FireTag component3 = myAttachedRigidbody.gameObject.GetComponent<FireTag>();
				if (component3 != null)
				{
					component3.Ignite(1f);
				}
				SimpleBirdAI component4 = myAttachedRigidbody.gameObject.GetComponent<SimpleBirdAI>();
				if (component4 != null)
				{
					component4.Explode();
				}
				EnemyAISimple component5 = myAttachedRigidbody.gameObject.GetComponent<EnemyAISimple>();
				if (component5 != null)
				{
					component5.Die();
				}
				CastleWallBreak component6 = myAttachedRigidbody.gameObject.GetComponent<CastleWallBreak>();
				if (component6 != null)
				{
					component6.BreakExplosion(power, explosionPos, radius, upPower);
				}
				BreakOnForce component7 = myAttachedRigidbody.gameObject.GetComponent<BreakOnForce>();
				if (component7 != null)
				{
					component7.BreakExplosion(power, explosionPos, radius, upPower);
				}
				BreakOnForceNoSpawn component8 = myAttachedRigidbody.gameObject.GetComponent<BreakOnForceNoSpawn>();
				if (component8 != null)
				{
					component8.BreakExplosion(power, explosionPos, radius, upPower);
				}
				InjuryController component9 = myAttachedRigidbody.gameObject.GetComponent<InjuryController>();
				if (component9 != null)
				{
					component9.activeType = InjuryType.Fire;
					component9.Kill();
				}
				prevRigidbody = myAttachedRigidbody;
			}
			else if (collider.transform.parent != null)
			{
				Rigidbody component10 = collider.transform.parent.GetComponent<Rigidbody>();
				if (component10 != null)
				{
					component10.WakeUp();
					component10.AddExplosionForce(power, explosionPos, radius, upPower);
					component10.AddRelativeTorque(Random.insideUnitSphere.normalized * torquePower);
				}
			}
		}
		parentObj.gameObject.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!hasExploded && simPhys && isSim && !explodeOnCollision && !hasExploded && other.gameObject.layer != 2)
		{
			Explodey();
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!hasExploded && simPhys && isSim && explodeOnCollision && !hasExploded && StatMaster.levelSimulating && other.collider.gameObject.layer != 2 && other.gameObject.layer != 27 && other.relativeVelocity.sqrMagnitude > collideCutoff)
		{
			Explodey();
		}
	}

	private void DisableOnExplode(bool toggle)
	{
		GetComponent<Collider>().enabled = toggle;
		bombVis.enabled = toggle;
	}
}
