using UnityEngine;

public class BleedOnJointBreak : SimBehaviour, IExplosionEffect
{
	public ParticleSystem particles;

	public ParticleSystem burstBloodParticles;

	public ParticleSystem dustBurst;

	public ParticleSystem smokeParticles;

	public InjuryController injuryControllerCode;

	public POVCam povCode;

	public float deathThreshold = 10f;

	public float sharpThreshold = 250f;

	public EnemyAISimple aiCode;

	public Renderer myRenderer;

	public MeshFilter meshFiltery;

	public Mesh[] deathPoses;

	public Transform gibCorpse;

	public FireController fireCode;

	public RandomSoundController soundController;

	public Transform[] destroyOnDie;

	public GameObject goreGib;

	private bool isDead;

	private bool gibbed;

	protected override void Awake()
	{
		base.Awake();
		if (goreGib == null && (bool)ReferenceMaster.Instance && ReferenceMaster.Instance.goreGib != null)
		{
			goreGib = ReferenceMaster.Instance.goreGib;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!isDead)
		{
			float num = deathThreshold;
			num *= 1.4f;
			if ((bool)other.collider.attachedRigidbody && (bool)other.collider.attachedRigidbody.GetComponent<EnemyAISimple>())
			{
				num *= 2f;
			}
			if (other.relativeVelocity.sqrMagnitude > num)
			{
				if (other.collider.gameObject.layer == 26 && other.relativeVelocity.sqrMagnitude > sharpThreshold)
				{
					Killed(true, InjuryType.Sharp);
				}
				else
				{
					Killed(true);
				}
				BleedOnObject(other);
			}
			else if (other.collider.gameObject.layer == 26)
			{
				BloodParticle();
			}
		}
		else if (other.relativeVelocity.sqrMagnitude > deathThreshold * 0.6f)
		{
			BleedOnObject(other);
			if (other.relativeVelocity.sqrMagnitude > sharpThreshold && other.collider.gameObject.layer == 26 && goreGib != null)
			{
				GibNew();
			}
			else
			{
				BloodParticle();
			}
		}
	}

	private void BleedOnObject(Collision other)
	{
		if (!OptionsMaster.BesiegeConfig.BloodEnabled)
		{
			return;
		}
		Rigidbody attachedRigidbody = other.collider.attachedRigidbody;
		if (attachedRigidbody != null)
		{
			BlockBehaviour component = attachedRigidbody.GetComponent<BlockBehaviour>();
			if (component != null)
			{
				component.BloodSplatter();
			}
		}
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		aiCode.Rigidbody.AddExplosionForce(0f - power, explosionPos, radius, upPower);
		aiCode.Rigidbody.AddExplosionForce(power * 0.4f, explosionPos, radius * 1.5f, upPower * 80f);
		if ((mask & 4) != 0)
		{
			Killed(false, InjuryType.Fire);
			return true;
		}
		return false;
	}

	public void Killed(bool jump, InjuryType type = InjuryType.Blunt)
	{
		if (isDead)
		{
			return;
		}
		soundController.Play();
		if (StatMaster.GodTools.GravityDisabled)
		{
			jump = false;
		}
		switch (type)
		{
		case InjuryType.Sharp:
			BloodParticle();
			if (base.gameObject.CompareTag("Enemy"))
			{
				Gib();
			}
			else if (goreGib != null)
			{
				GibNew();
			}
			else
			{
				KillMe(jump);
			}
			break;
		case InjuryType.Blunt:
			BloodParticle();
			KillMe(jump);
			break;
		default:
			KillMe(jump);
			break;
		}
		base.gameObject.tag = "Untagged";
		isDead = true;
	}

	private void BloodParticle()
	{
		if (NetworkBlock.applyingState)
		{
			return;
		}
		if (!OptionsMaster.BesiegeConfig.BloodEnabled)
		{
			if (dustBurst != null)
			{
				dustBurst.Play();
			}
		}
		else if (aiCode.InWater)
		{
			GlobalParticles.EmitParticleBursts(6, base.transform.position);
		}
		else
		{
			GlobalParticles.EmitParticleBursts(5, base.transform.position);
		}
	}

	public void KillMe(bool jump)
	{
		injuryControllerCode.Kill();
		bloodTextureSwap();
		meshFiltery.mesh = deathPoses[Random.Range(0, deathPoses.Length)];
		AddToPercentageBar();
		DestroyOnDie();
		if (aiCode != null)
		{
			if (jump)
			{
				aiCode.Die();
			}
			else
			{
				aiCode.DieNoJump();
			}
		}
	}

	private void bloodTextureSwap()
	{
		if (OptionsMaster.BesiegeConfig.BloodEnabled && (bool)myRenderer && myRenderer.material.HasProperty("_BloodAmount"))
		{
			myRenderer.material.SetColor("_BloodColor", StatMaster.BloodColor);
			myRenderer.material.SetFloat("_BloodAmount", 1f);
		}
	}

	private void Gib()
	{
		if (SingleInstance<StatMaster>.Instance.LowViolence)
		{
			return;
		}
		injuryControllerCode.Kill();
		AddToPercentageBar();
		gibbed = true;
		if (aiCode != null)
		{
			aiCode.Die();
		}
		if (OptionsMaster.BesiegeConfig.BloodEnabled)
		{
			if (!NetworkBlock.applyingState)
			{
				Object.Instantiate(gibCorpse.gameObject, base.transform.position, base.transform.rotation, ReferenceMaster.physicsGoalInstance);
			}
			if (!StatMaster.isMP)
			{
				Object.Destroy(base.gameObject);
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	private void GibNew()
	{
		if (!gibbed && !SingleInstance<StatMaster>.Instance.LowViolence)
		{
			gibbed = true;
			injuryControllerCode.Kill();
			if (!isDead)
			{
				AddToPercentageBar();
			}
			if (aiCode != null)
			{
				aiCode.Die();
			}
			if (OptionsMaster.BesiegeConfig.BloodEnabled && goreGib != null)
			{
				Object.Instantiate(goreGib, base.transform.position, base.transform.rotation, ReferenceMaster.physicsGoalInstance);
				base.gameObject.SetActive(false);
			}
		}
	}

	private void AddToPercentageBar()
	{
		if (!StatMaster.isMP && base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.currentObjsCompleted++;
		}
	}

	private void DestroyOnDie()
	{
		for (int i = 0; i < destroyOnDie.Length; i++)
		{
			Object.Destroy(destroyOnDie[i].gameObject);
		}
	}
}
