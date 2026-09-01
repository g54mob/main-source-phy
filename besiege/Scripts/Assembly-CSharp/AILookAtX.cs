using UnityEngine;

public class AILookAtX : MonoBehaviour
{
	public Rigidbody body;

	public AnimationCurve torqueScale;

	public bool useTorqueScale;

	public Transform baseObject;

	public float rotationSpeed = 1f;

	public float maxTorque = 20f;

	public float startDelay = 2f;

	private Quaternion cannonRotation;

	private BlockBehaviour target;

	public bool clearVelocityOnNew = true;

	public float prediction;

	private Vector3 correctedDir = Vector3.up;

	private Vector3 direction;

	public float activationDistance;

	private float activationDistanceSqr;

	public bool randomRotateOnIdle;

	private float minInterval = 6f;

	private float maxInterval = 14f;

	private float intervalTimer;

	private Vector3 randomDirection = Vector3.forward;

	private bool hasTarget;

	private void Awake()
	{
		activationDistanceSqr = activationDistance * activationDistance;
	}

	private void FixedUpdate()
	{
		if ((!hasTarget || (target.Prefab.hasHealthBar && target.BlockHealth.health == 0f)) && !GetTarget())
		{
			RandomisedRotation();
			return;
		}
		if (prediction > 0f)
		{
			direction = target.transform.position + target.Rigidbody.velocity * prediction - base.transform.position;
		}
		else
		{
			direction = target.transform.position - base.transform.position;
		}
		if (direction.sqrMagnitude > activationDistanceSqr)
		{
			RandomisedRotation();
			hasTarget = false;
		}
		else
		{
			Rotate(direction, maxTorque);
		}
	}

	private void Rotate(Vector3 direction, float maxTorque, float speed = 1f)
	{
		if (hasTarget)
		{
			speed = rotationSpeed * speed;
			Vector3 normalized = direction.normalized;
			cannonRotation.SetLookRotation(normalized, base.transform.up);
			Vector3 vector = baseObject.InverseTransformDirection(normalized);
			Vector3 normalized2 = new Vector3(vector.x, 0f, vector.z).normalized;
			correctedDir = baseObject.TransformDirection(normalized2);
			float num = Vector3.Angle(body.transform.forward, correctedDir);
			Vector3 vector2 = Vector3.Cross(body.transform.forward, correctedDir);
			float num2 = num * speed * 5f;
			if (useTorqueScale)
			{
				num2 = torqueScale.Evaluate(num2 / maxTorque) * maxTorque;
			}
			Vector3 vector3 = vector2 * num2;
			vector3 = Vector3.ClampMagnitude(vector3, maxTorque);
			if (clearVelocityOnNew)
			{
				vector3 -= body.angularVelocity;
			}
			body.AddTorque(vector3, ForceMode.Acceleration);
		}
	}

	private void RandomisedRotation()
	{
		if (!randomRotateOnIdle)
		{
			return;
		}
		if (Vector3.Dot(body.transform.forward, correctedDir) > 0.95f)
		{
			intervalTimer -= Time.deltaTime;
			if (intervalTimer < 0f)
			{
				randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.35f, 0f), Random.Range(-1f, 1f)).normalized;
				intervalTimer = Random.Range(minInterval, maxInterval);
			}
		}
		Rotate(randomDirection, maxTorque / 2f, 0.2f);
	}

	private bool GetTarget()
	{
		int closestMachine = FactionsController.GetClosestMachine(base.transform.position);
		if (closestMachine != -1)
		{
			BlockBehaviour randomIntactBlock = ReferenceMaster.GetRandomIntactBlock((uint)closestMachine);
			if (object.ReferenceEquals(randomIntactBlock, null) || randomIntactBlock.IsDestroyed)
			{
				return false;
			}
			target = randomIntactBlock;
			hasTarget = true;
			return true;
		}
		return false;
	}
}
