using System.Collections;
using TFBGames;
using UnityEngine;

public class WeaponForceAnimation : AttackEffect
{
	public SpellAnimation[] animations;

	public float immunityTime;

	public float upwardsModifier;

	public float chance;

	private DataHandler data;

	private Holdable holdable;

	private Rigidbody rig;

	private void Start()
	{
		rig = GetComponentInParent<Rigidbody>();
	}

	public override void DoEffect(Rigidbody target, Vector3 targetDir)
	{
		if (!data)
		{
			data = GetComponentInParent<Weapon>().connectedData;
		}
		if (!holdable)
		{
			holdable = GetComponentInParent<Holdable>();
		}
		if (chance == 0f || !(chance < Random.value))
		{
			for (int i = 0; i < animations.Length; i++)
			{
				StartCoroutine(PlayAnimationAfterDelay(animations[i], target ? target.position : (base.transform.position + targetDir), target));
			}
		}
	}

	private IEnumerator PlayAnimationAfterDelay(SpellAnimation animation, Vector3 position, Rigidbody targetRig = null)
	{
		if (upwardsModifier != 0f)
		{
			position += Vector3.up * upwardsModifier;
		}
		if ((bool)holdable && (bool)holdable.hl && animation.invertForceIfLeft)
		{
			animation.rigAnimationForce *= -1f;
		}
		Rigidbody[] usedRig = new Rigidbody[1] { rig };
		if (animation.animationRig == SpellAnimation.AnimationRig.All)
		{
			usedRig = data.allRigs.AllRigs;
		}
		if (animation.animationRig == SpellAnimation.AnimationRig.Torso)
		{
			usedRig[0] = data.mainRig;
		}
		if (animation.animationRig == SpellAnimation.AnimationRig.Hip)
		{
			usedRig[0] = data.hip;
		}
		if (animation.animationRig == SpellAnimation.AnimationRig.ThisRig)
		{
			usedRig[0] = GetComponent<Rigidbody>();
		}
		Vector3 animationDirection = SetDirection(position, animation);
		yield return new WaitForSeconds(animation.animationDelay);
		float t = animation.rigAnimationCurve[animation.rigAnimationCurve.length - 1].time;
		float c = 0f;
		float ASM = Mathf.Clamp(data.unit.attackSpeedMultiplier, 0f, 6f);
		while (c < t && data.ragdollControl > 0.7f)
		{
			if (animation.setDirectionContinious && (bool)targetRig)
			{
				animationDirection = SetDirection(targetRig.position + Vector3.up * upwardsModifier, animation);
			}
			if (data.sinceGrounded < 0.3f)
			{
				for (int i = 0; i < usedRig.Length; i++)
				{
					usedRig[i].AddForce(FixedTimeStepService.SmallForceCoefficient * 100f * animation.rigAnimationCurve.Evaluate(c) * animation.rigAnimationForce * ASM * Time.deltaTime * animationDirection, ForceMode.Acceleration);
				}
			}
			c += Time.deltaTime * ASM;
			yield return null;
		}
	}

	private Vector3 SetDirection(Vector3 position, SpellAnimation animation)
	{
		Vector3 result = (position - base.transform.position).normalized;
		if (animation.animationDirection == RangeWeapon.SpawnRotation.TowardsTargetWithoutY)
		{
			result = new Vector3(result.x, 0f, result.z).normalized;
		}
		else if (animation.animationDirection == RangeWeapon.SpawnRotation.Up)
		{
			result = Vector3.up;
		}
		else if (animation.animationDirection == RangeWeapon.SpawnRotation.identity)
		{
			result = Vector3.forward;
		}
		else if (animation.animationDirection == RangeWeapon.SpawnRotation.CharacterForward)
		{
			result = data.characterForwardObject.forward;
		}
		if (animation.rangeMultiplierCurve.length > 0)
		{
			result *= animation.rangeMultiplierCurve.Evaluate(Vector3.Distance(base.transform.position, position));
		}
		return result;
	}
}
