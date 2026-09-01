using System;
using UnityEngine;

public class SoundOnCollide : SimBehaviour
{
	protected static ParticleSystem.EmitParams emitter = default(ParticleSystem.EmitParams);

	protected static Color Yellow = new Color(0.5f, 0.5f, 0.05f, 1f);

	protected static Color Red = new Color(0.46f, 0.07f, 0.15f, 1f);

	public Action<Collision> nestedCollision;

	public RandomSoundController randSoundController;

	public float cutoff = 6f;

	public float sfxMaxImpact = 2000f;

	public ParticleSystem particles;

	public float chanceParticleWillPlay = 1f;

	private float baseVolume = 0.02f;

	protected override void Start()
	{
		base.Start();
		randSoundController.AssignMixers();
	}

	public void SetSourceCollider(SoundOnCollide source)
	{
		source.nestedCollision = (Action<Collision>)Delegate.Combine(source.nestedCollision, new Action<Collision>(Collide));
	}

	protected virtual void OnCollisionEnter(Collision other)
	{
		if (base.isSimulating && base.SimPhysics && !StatMaster.isHeadless)
		{
			Collider thisCollider = other.contacts[0].thisCollider;
			if (thisCollider.gameObject.CompareTag("ArmourTag") && !thisCollider.attachedRigidbody.gameObject.CompareTag("ArmourTag"))
			{
				InvokeCollide(other);
			}
			else
			{
				Collide(other);
			}
		}
	}

	public void InvokeCollide(Collision other)
	{
		if (nestedCollision != null)
		{
			nestedCollision(other);
		}
	}

	public void Collide(Collision other)
	{
		float sqrMagnitude = other.relativeVelocity.sqrMagnitude;
		if (!(sqrMagnitude > cutoff))
		{
			return;
		}
		float num = Mathf.Clamp01(baseVolume + Mathf.InverseLerp(cutoff, sfxMaxImpact, sqrMagnitude) * 0.3f);
		if (!other.collider.CompareTag("NoSound"))
		{
			PlaySound(num);
		}
		if (StatMaster.stressCoded)
		{
			Red.a = (Yellow.a = num);
			emitter.startColor = Color.Lerp(Yellow, Red, num / (baseVolume * 4f));
			emitter.position = other.contacts[0].point;
			GlobalParticles.EmitParticle(19, emitter, 1);
		}
		if (StatMaster.isMP && !StatMaster.IsLevelEditorOnly)
		{
			if (base.NetBlock != null)
			{
				base.NetBlock.Event(NetworkEntity.EntityEvent.SoundOnCollide, (byte)(num * 255f));
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
	}

	public void PlaySound(float vol)
	{
		randSoundController.AssignMixers();
		float submergedPerc = 0f;
		if (HasBasicInfo)
		{
			submergedPerc = ((!basicInfo.IgnoredByWater) ? basicInfo.GetSubmergedPctMV : ((float)((WaterController.Exist && base.transform.position.y < WaterController.waterTransformHeight) ? 1 : 0)));
		}
		randSoundController.Play(vol, submergedPerc);
		if (chanceParticleWillPlay >= UnityEngine.Random.value && !StatMaster.stressCoded && particles != null)
		{
			particles.Play();
		}
	}
}
