using Landfall.TABS;
using UnityEngine;
using UnityEngine.Events;

public class DarkPHand : MonoBehaviour
{
	private enum HandAction
	{
		Throw = 0,
		ThrowAway = 1,
		Explosion = 2,
		Grab = 3,
		GrabProjectile = 4,
		GrabProjectileBlunt = 5,
		DoubleGrab = 6
	}

	public AnimationCurve upCurve;

	public float upForce;

	public UnityEvent exploEvent;

	public float exploDelay;

	public UnityEvent startExploEvent;

	public AnimationCurve throwCurveBluntProj;

	public float throwForceBluntProj;

	public UnityEvent throwStartEvent;

	public AnimationCurve throwCurve;

	public float throwForce;

	public float throwDmg = 250f;

	public UnityEvent throwCompleteEvent;

	public Transform effectRoot;

	private float totalT;

	private float atTargetT;

	private Rigidbody grabRig;

	private Rigidbody targetRig;

	private HandAction handAction;

	private Vector3 localPos;

	private DataHandler grabUnit;

	private Vector3 throwAwayDir;

	private bool done;

	private bool atTarget;

	private Vector3 vel;

	private Vector3 reachedTargetPos;

	private Vector3 grabGroundPos;

	private Vector3 startForward;

	private Vector3 startUp;

	public void Target(GameObject grabObject, GameObject targetObject, Vector3 posOrDir)
	{
		grabRig = grabObject.GetComponentInChildren<DataHandler>().mainRig;
		grabUnit = grabObject.GetComponentInChildren<DataHandler>();
		if ((bool)grabObject && (bool)targetObject)
		{
			targetRig = targetObject.GetComponentInChildren<DataHandler>().mainRig;
			if (Vector3.Distance(grabRig.position, targetRig.position) > 1.5f)
			{
				handAction = HandAction.Throw;
				throwStartEvent.Invoke();
			}
			else
			{
				handAction = HandAction.Explosion;
			}
		}
		else if (posOrDir != Vector3.zero)
		{
			throwAwayDir = posOrDir;
			if (Random.value > 0.9f)
			{
				handAction = HandAction.ThrowAway;
			}
			else
			{
				handAction = HandAction.Explosion;
			}
		}
		Vector3 position = grabRig.GetComponentInChildren<Collider>().ClosestPoint(base.transform.position);
		localPos = grabRig.transform.InverseTransformPoint(position);
	}

	private void FixedUpdate()
	{
		if ((bool)grabRig)
		{
			if (handAction == HandAction.GrabProjectileBlunt)
			{
				DoGrabProjectileeBluntFixed();
			}
			if (handAction == HandAction.ThrowAway)
			{
				DoThrowAwayFixed();
			}
			if (handAction == HandAction.Throw)
			{
				DoThrowFixed();
			}
			if (handAction == HandAction.Explosion)
			{
				DoExplosionFixed();
			}
		}
	}

	private void LateUpdate()
	{
		if ((bool)grabRig)
		{
			totalT += Time.deltaTime;
			if (atTarget)
			{
				atTargetT += Time.deltaTime;
			}
			GoToTarget();
		}
	}

	private Vector3 GetTargetPos()
	{
		return grabRig.transform.TransformPoint(localPos);
	}

	private void GoToTarget()
	{
		if (atTarget)
		{
			base.transform.SetPositionAndRotation(GetTargetPos(), Quaternion.LookRotation(grabRig.transform.TransformDirection(startForward), grabRig.transform.TransformDirection(startUp)));
			return;
		}
		Vector3 vector = GetTargetPos() - base.transform.position;
		if (vector.magnitude < 2f)
		{
			vector = vector.normalized * 2f;
		}
		vel = Vector3.Lerp(vel, vector * 5f, Time.deltaTime * 8f);
		if (handAction == HandAction.GrabProjectileBlunt)
		{
			vel = Vector3.Lerp(vel, vector * 10f, Time.deltaTime * 50f);
		}
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.LookRotation(grabRig.position - GetTargetPos()), Time.deltaTime * 3f);
		base.transform.position += vel * Time.deltaTime;
		if (!(Vector3.Distance(GetTargetPos(), base.transform.position) < 0.5f))
		{
			return;
		}
		atTarget = true;
		reachedTargetPos = base.transform.position;
		base.transform.rotation = Quaternion.LookRotation(grabRig.position - GetTargetPos());
		startForward = grabRig.transform.InverseTransformDirection(base.transform.forward);
		startUp = grabRig.transform.InverseTransformDirection(base.transform.up);
		if (handAction == HandAction.Explosion)
		{
			RaycastHit groundPos = WilhelmPhysicsFunctions.GetGroundPos(base.transform.position);
			if ((bool)groundPos.transform)
			{
				grabGroundPos = groundPos.point;
			}
			else
			{
				grabGroundPos = reachedTargetPos;
			}
		}
		if (handAction == HandAction.GrabProjectileBlunt && (bool)targetRig)
		{
			throwAwayDir = targetRig.position - grabRig.position;
			throwAwayDir = throwAwayDir.normalized;
		}
	}

	private void DoExplosionFixed()
	{
		if (!done && atTarget && (bool)grabRig)
		{
			Vector3 vector = Vector3.up;
			if (upCurve.Evaluate(atTargetT) < 0f)
			{
				vector = -(grabGroundPos - base.transform.position).normalized;
			}
			grabRig.AddForce(upCurve.Evaluate(atTargetT) * upForce * vector, ForceMode.Acceleration);
			grabUnit.sinceGrounded = 0f;
			if (atTargetT > 0.3f && (atTargetT > exploDelay || Vector3.Distance(grabGroundPos, base.transform.position) < 1f + grabUnit.unit.thickness))
			{
				effectRoot.transform.SetPositionAndRotation(grabGroundPos, Quaternion.LookRotation(Vector3.up));
				done = true;
				exploEvent.Invoke();
			}
		}
	}

	private void DoThrowFixed()
	{
		if (!done && atTarget && (bool)grabRig && (bool)targetRig)
		{
			Vector3 normalized = (targetRig.transform.position - grabRig.transform.position).normalized;
			grabRig.AddForce(throwCurve.Evaluate(atTargetT) * throwForce * normalized, ForceMode.Acceleration);
			float num = Vector3.Distance(grabRig.transform.position, targetRig.transform.position);
			if (atTargetT > 0.4f && num < 1f + grabUnit.unit.thickness)
			{
				done = true;
				effectRoot.transform.rotation = Quaternion.LookRotation(normalized);
				throwCompleteEvent.Invoke();
				grabRig.GetComponentInParent<HealthHandler>().TakeDamage(throwDmg, effectRoot.transform.forward, null);
				targetRig.GetComponentInParent<HealthHandler>().TakeDamage(throwDmg, effectRoot.transform.forward, null);
			}
		}
	}

	private void DoThrowAwayFixed()
	{
		if (!done && atTarget && (bool)grabRig)
		{
			grabRig.AddForce(throwCurve.Evaluate(atTargetT) * throwForce * throwAwayDir.normalized, ForceMode.Acceleration);
			if (atTargetT > 2f)
			{
				grabUnit.fallTime = 1f;
				grabRig.GetComponentInParent<HealthHandler>().TakeDamage(throwDmg, effectRoot.transform.forward, null);
				done = true;
			}
		}
	}

	internal void GrabProjectile(Rigidbody rig, Unit primeTarget)
	{
		grabRig = rig;
		if ((bool)primeTarget)
		{
			targetRig = primeTarget.data.mainRig;
		}
		throwAwayDir = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
		handAction = HandAction.GrabProjectileBlunt;
		Vector3 position = grabRig.GetComponentInChildren<Collider>().ClosestPoint(base.transform.position + Random.onUnitSphere * 100f);
		localPos = grabRig.transform.InverseTransformPoint(position);
	}

	private void DoGrabProjectileeBluntFixed()
	{
		if (!done && (bool)grabRig && atTarget)
		{
			grabRig.useGravity = false;
			grabRig.AddForce(throwAwayDir.normalized * throwCurveBluntProj.Evaluate(atTargetT) * throwForceBluntProj, ForceMode.Acceleration);
			grabRig.velocity *= 0.92f;
			if (atTargetT > 1.5f)
			{
				done = true;
				grabRig.useGravity = true;
			}
		}
	}
}
