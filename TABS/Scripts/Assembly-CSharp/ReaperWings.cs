using System.Collections;
using System.Collections.Generic;
using Landfall.TABS;
using UnityEngine;

public class ReaperWings : MonoBehaviour
{
	public string swingRef;

	public string hitRef;

	private Transform targetTrans;

	private List<AttackArm> attackArms = new List<AttackArm>();

	public AnimationCurve reachCurve;

	public AnimationCurve goBackToHoldCurve;

	public AnimationCurve goBackToHoldCurveFollowMainRigAmount;

	public AnimationCurve throwCurve;

	[SerializeField]
	private Renderer[] armRenderers;

	private float followMainRigAmount;

	private Unit unit;

	private float counter;

	private float snapSpeed;

	private void Start()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			AttackArm attackArm = new AttackArm();
			attackArm.targetObj = base.transform.GetChild(i).Find("Target").gameObject;
			attackArm.restPosObj = base.transform.GetChild(i).Find("RestPos").gameObject;
			attackArm.lerpSpeed = Random.Range(0.5f, 1.5f);
			attackArm.smoothTargetPos = base.transform.position;
			attackArm.targetPos = base.transform.position;
			attackArms.Add(attackArm);
		}
		unit = base.transform.root.GetComponent<Unit>();
		if (armRenderers != null && unit != null)
		{
			unit.AddRenderersToShowHide(armRenderers);
		}
		targetTrans = unit.data.mainRig.transform;
		base.transform.SetParent(null);
	}

	private void Update()
	{
		if (targetTrans == null || unit.data.Dead)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		base.transform.SetPositionAndRotation(targetTrans.position, Quaternion.Lerp(base.transform.rotation, targetTrans.rotation, Time.deltaTime * 2.5f));
		counter += Time.deltaTime;
		for (int i = 0; i < attackArms.Count; i++)
		{
			attackArms[i].counter += Time.deltaTime;
			Vector3 vector = Vector3.Lerp(attackArms[i].smoothTargetPos, attackArms[i].targetPos, Time.deltaTime * 10f);
			if (attackArms[i].armState == AttackArm.ArmState.Free)
			{
				attackArms[i].targetPos = Vector3.Lerp(attackArms[i].targetPos, attackArms[i].restPosObj.transform.position + GetPerlinPos(attackArms[i].lerpSpeed) * 2f, Time.deltaTime * 3f * attackArms[i].lerpSpeed);
				attackArms[i].smoothTargetPos = vector;
				snapSpeed = 0f;
			}
			else
			{
				snapSpeed += Time.deltaTime;
				attackArms[i].smoothTargetPos = Vector3.Lerp(vector, attackArms[i].targetPos, snapSpeed);
			}
			if ((bool)attackArms[i].heldUnit)
			{
				attackArms[i].targetObj.transform.position = Vector3.Lerp(attackArms[i].smoothTargetPos, attackArms[i].heldUnit.data.mainRig.position, followMainRigAmount);
			}
			else
			{
				attackArms[i].targetObj.transform.position = attackArms[i].smoothTargetPos;
			}
		}
		CheckAttack(attackArms[Random.Range(0, attackArms.Count)]);
	}

	private void FixedUpdate()
	{
		counter += Time.deltaTime;
		for (int i = 0; i < attackArms.Count; i++)
		{
			Hold(attackArms[i]);
		}
	}

	private void Hold(AttackArm attack)
	{
		if (!(attack.heldUnit != null) || !(attack.heldUnit.data.mainRig != null))
		{
			return;
		}
		if (!attack.heldUnit.dead)
		{
			attack.heldUnit.data.healthHandler.TakeDamage(1000f, Vector3.up, null);
		}
		if (!(attack.counter > 0.1f))
		{
			return;
		}
		attack.heldUnit.data.mainRig.AddForce((attack.smoothTargetPos - attack.heldUnit.data.mainRig.position) * 500f, ForceMode.Acceleration);
		attack.heldUnit.data.mainRig.velocity *= 0.7f;
		for (int i = 0; i < attack.heldUnit.data.allRigs.AllRigs.Length; i++)
		{
			if (attack.heldUnit.data.allRigs.AllRigs[i] != null)
			{
				attack.heldUnit.data.allRigs.AllRigs[i].velocity *= 0.8f;
			}
		}
		attack.heldUnit.data.sinceGrounded = 0f;
	}

	private void CheckAttack(AttackArm attack)
	{
		if (!(counter < 0.7f) && !(attack.counter < 10f) && attack.armState != AttackArm.ArmState.Holding && (bool)unit.data.targetData && !(unit.data.distanceToTarget > 5f) && !(unit.data.targetData.maxHealth > 250f))
		{
			counter = 0f;
			attack.counter = 0f;
			StartCoroutine(DoAttack(attack, unit.data.targetData.unit));
		}
	}

	private IEnumerator DoAttack(AttackArm attack, Unit targetUnit)
	{
		attack.armState = AttackArm.ArmState.Holding;
		float c = 0f;
		float t = AnimationCurveFunctions.GetAnimLength(reachCurve);
		if (swingRef != "")
		{
			ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect(swingRef, 1f, base.transform.position);
		}
		while (c < t && (bool)targetUnit && (bool)targetUnit.data.mainRig)
		{
			Vector3 vector = targetUnit.data.mainRig.position - attack.restPosObj.transform.position;
			attack.targetPos = attack.restPosObj.transform.position + vector * reachCurve.Evaluate(c);
			c += Time.deltaTime;
			yield return null;
		}
		c = 0f;
		t = AnimationCurveFunctions.GetAnimLength(goBackToHoldCurve);
		attack.heldUnit = targetUnit;
		attack.counter = 0f;
		if ((bool)targetUnit && hitRef != "")
		{
			ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect(hitRef, 1f, base.transform.position);
		}
		while (c < t && (bool)targetUnit && (bool)targetUnit.data.mainRig)
		{
			Vector3 vector2 = targetUnit.data.mainRig.position - attack.restPosObj.transform.position;
			attack.targetPos = attack.restPosObj.transform.position + vector2 * goBackToHoldCurve.Evaluate(c);
			c += Time.deltaTime;
			followMainRigAmount = goBackToHoldCurveFollowMainRigAmount.Evaluate(c);
			yield return null;
		}
		attack.armState = AttackArm.ArmState.Free;
	}

	private Vector3 GetPerlinPos(float input)
	{
		input *= 0.2f;
		Vector3 result = new Vector3(0f, 0f, 0f);
		result.x += Mathf.PerlinNoise(Time.time * input, 0f);
		result.y += Mathf.PerlinNoise(Time.time * input, Time.time * input);
		result.z += Mathf.PerlinNoise(0f, Time.time * input);
		result.x -= 0.5f;
		result.y -= 0.5f;
		result.z -= 0.5f;
		return result;
	}
}
