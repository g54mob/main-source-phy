using UnityEngine;

public class GibOnImpact : SimBehaviour, IExplosionEffect
{
	public float powerRequired = 20f;

	public Transform corpse;

	public Transform corpseDust;

	public InjuryController injuryControllerCode;

	public EnemyAISimple aiCode;

	public ExplodeOnCollide bombCode;

	public string nameToIgnore = "none";

	public bool gibOnStaticImpact;

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!base.isSimulating || !base.SimPhysics)
		{
			return false;
		}
		if ((mask & 4) != 0 && power > powerRequired)
		{
			Gib();
			return true;
		}
		return false;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!base.SimPhysics || !base.isSimulating)
		{
			return;
		}
		if ((bool)other.collider.attachedRigidbody)
		{
			if (!(other.collider.attachedRigidbody.gameObject.name != nameToIgnore))
			{
				return;
			}
			if (other.relativeVelocity.sqrMagnitude > powerRequired)
			{
				Gib();
			}
			else if ((bool)other.collider.attachedRigidbody)
			{
				BlockBehaviour component = other.collider.attachedRigidbody.GetComponent<BlockBehaviour>();
				if (component != null && component.Prefab.Type == BlockType.Drill)
				{
					Gib();
				}
			}
		}
		else if (gibOnStaticImpact && other.relativeVelocity.sqrMagnitude > powerRequired)
		{
			Gib();
		}
	}

	public void Gib()
	{
		if (injuryControllerCode != null)
		{
			injuryControllerCode.Kill();
		}
		if (aiCode != null)
		{
			aiCode.Die();
		}
		AddToPercentageBar();
		if (bombCode != null)
		{
			bombCode.Explodey();
		}
		if (!NetworkBlock.applyingState)
		{
			if (OptionsMaster.BesiegeConfig.BloodEnabled)
			{
				Object.Instantiate(corpse.gameObject, base.transform.position, base.transform.rotation);
			}
			else if (corpseDust != null)
			{
				Object.Instantiate(corpseDust.gameObject, base.transform.position, base.transform.rotation);
			}
		}
		base.gameObject.SetActive(false);
	}

	private void AddToPercentageBar()
	{
		if (!StatMaster.isMP && base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.currentObjsCompleted++;
		}
	}
}
