using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Physics/AI/SquidController")]
public class SquidController : MonoBehaviour
{
	[Serializable]
	public class Tentacle
	{
		public LimbController limb;

		public Rigidbody leader;

		public Collider[] cols;

		public float intensity = 1f;

		public float forwardScale = 1f;
	}

	public SkinnedMeshRenderer renderer;

	public BasicInfo main;

	public float bobRate = 2f;

	public float bobForce = 100f;

	public Tentacle[] tentacles;

	public float moveRate = 3f;

	public float tentacleTiming = 2.5f;

	public float moveForce = 100f;

	public float steeringForce = 100f;

	public MonoBehaviour[] enableOnMove;

	public AnimationCurve outwardForceCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	public AnimationCurve upForceCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	public AnimationCurve forwardForceCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	public PhysicMaterial sticky;

	public PhysicMaterial slippery;

	public SetKinematicIfSim kinematicHandler;

	public bool moveOnLevelComplete;

	public Transform initialTarget;

	private bool useInitialTarget;

	private static CharacterJoint jointPrefab;

	public GameObject prefab;

	private bool jointsCreated;

	private Transform targetTransform;

	private int currentMove;

	private float physTime;

	private float moveTime = -2f;

	private bool startedMoving;

	private int health;

	private int maxHealth;

	protected bool HasSimBlocks
	{
		get
		{
			return (!StatMaster.isMP) ? (Machine.Active().SimulationBlocks.Count > 0) : (ReferenceMaster.SimulationBlocks.Count > 0);
		}
	}

	public Transform target
	{
		get
		{
			return useInitialTarget ? initialTarget : ((!HasSimBlocks) ? main.transform : targetTransform);
		}
	}

	public Vector3 direction
	{
		get
		{
			return (target.position - main.transform.position).normalized;
		}
	}

	public Transform GetTarget()
	{
		if (StatMaster.isMP)
		{
			if (FactionsController.setupComplete)
			{
				int closestMachine = FactionsController.GetClosestMachine(main.transform.position);
				if (closestMachine != -1)
				{
					return ReferenceMaster.GetRandomBlock((uint)closestMachine).transform;
				}
			}
			return null;
		}
		List<BlockBehaviour> simulationBlocks = Machine.Active().SimulationBlocks;
		return simulationBlocks[UnityEngine.Random.Range(0, simulationBlocks.Count)].transform;
	}

	private void OnEnable()
	{
		if (!StatMaster.levelSimulating)
		{
			CreateJoints();
			jointPrefab = tentacles[0].limb.GetComponent<CharacterJoint>();
			return;
		}
		if (StatMaster.isMP)
		{
			moveTime = UnityEngine.Random.Range(1f, 4f);
			currentMove = UnityEngine.Random.Range(0, tentacles.Length);
		}
		if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim)
		{
			ManualDenest(main.transform);
			for (int i = 0; i < tentacles.Length; i++)
			{
				ManualDenest(tentacles[i].limb.transform);
			}
			return;
		}
		for (int j = 0; j < tentacles.Length; j++)
		{
			tentacles[j].limb.index = j;
		}
		health = (maxHealth = tentacles.Length);
		renderer.enabled = true;
		useInitialTarget = initialTarget != null;
		SetupTentacles();
	}

	private void ManualDenest(Transform t)
	{
		if (t.gameObject.CompareTag("Seam"))
		{
			return;
		}
		t.parent = base.transform;
		foreach (Transform item in t)
		{
			ManualDenest(item);
		}
	}

	private void CreateJoints()
	{
		if (jointsCreated)
		{
			return;
		}
		Vector3[] array = new Vector3[2];
		Quaternion[] array2 = new Quaternion[2];
		CharacterJoint[] componentsInChildren = prefab.GetComponentsInChildren<CharacterJoint>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform childWithName = GetChildWithName(base.transform, componentsInChildren[i].name);
			Transform childWithName2 = GetChildWithName(base.transform, componentsInChildren[i].connectedBody.name);
			array[0] = childWithName.localPosition;
			array2[0] = childWithName.localRotation;
			array[1] = childWithName2.localPosition;
			array2[1] = childWithName2.localRotation;
			childWithName.localPosition = componentsInChildren[i].transform.localPosition;
			childWithName.rotation = componentsInChildren[i].transform.localRotation;
			CharacterJoint characterJoint = childWithName.gameObject.AddComponent<CharacterJoint>();
			characterJoint.CopyJoint(componentsInChildren[i]);
			characterJoint.connectedBody = childWithName2.GetComponent<Rigidbody>();
			TransformSyncBetween2Bodies component = childWithName.GetComponent<TransformSyncBetween2Bodies>();
			if ((bool)component)
			{
				component.SetReferencePoints();
			}
			childWithName.localPosition = array[0];
			childWithName.localRotation = array2[0];
			childWithName2.localPosition = array[1];
			childWithName2.localRotation = array2[1];
		}
		jointsCreated = true;
	}

	private Transform GetChildWithName(Transform parent, string name)
	{
		for (int i = 0; i < parent.childCount; i++)
		{
			Transform child = parent.GetChild(i);
			if (child.name == name)
			{
				return child;
			}
			Transform childWithName = GetChildWithName(child, name);
			if (childWithName != null)
			{
				return childWithName;
			}
		}
		return null;
	}

	private void FixedUpdate()
	{
		if ((StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim) || !main.isSimulating)
		{
			return;
		}
		if (moveOnLevelComplete)
		{
			if (!WinCondition.Instance.ObjectiveMet)
			{
				return;
			}
			if (targetTransform == null)
			{
				if (StatMaster.isMP)
				{
					targetTransform = GetTarget();
					return;
				}
				List<BlockBehaviour> simulationBlocks = Machine.Active().SimulationBlocks;
				targetTransform = simulationBlocks[0].transform;
			}
		}
		else if (targetTransform == null || (targetTransform.position - main.transform.position).sqrMagnitude > ((!useInitialTarget) ? 30000f : 5000f))
		{
			targetTransform = GetTarget();
			return;
		}
		if (!startedMoving)
		{
			for (int i = 0; i < enableOnMove.Length; i++)
			{
				enableOnMove[i].enabled = true;
			}
		}
		float fixedDeltaTime = Time.fixedDeltaTime;
		main.Rigidbody.AddForce(Vector3.up * Mathf.Sin(physTime * (float)Math.PI / bobRate) * bobForce);
		Move(fixedDeltaTime);
		Steer();
		if (useInitialTarget)
		{
			Vector3 vector = initialTarget.position - main.transform.position;
			vector.y = 0f;
			if (vector.sqrMagnitude < 40f)
			{
				useInitialTarget = false;
			}
		}
		physTime += fixedDeltaTime;
		startedMoving = true;
	}

	public void Steer()
	{
		Vector3 forward = main.transform.forward;
		Vector3 vector = direction;
		forward.y = (vector.y = 0f);
		Vector3 vector2 = Vector3.Cross(forward, vector);
		Vector3 vector3 = vector2.normalized * Mathf.Sqrt(vector2.magnitude);
		main.Rigidbody.AddTorque(vector3 * steeringForce * 2f);
		Debug.DrawRay(main.transform.position, forward, Color.green);
		Debug.DrawRay(main.transform.position, vector, Color.cyan);
		Debug.DrawRay(main.transform.position, vector3, Color.yellow);
	}

	private void Move(float delta)
	{
		if (moveTime > moveRate + UnityEngine.Random.Range(0f - delta, delta))
		{
			Tentacle tentacle = tentacles[currentMove];
			Vector3 vector = tentacle.leader.transform.right;
			if (tentacle.leader.gameObject.tag == "Reverse")
			{
				vector = -vector;
			}
			if (tentacle.intensity != 0f)
			{
				StartCoroutine(Move(tentacle, vector, moveRate * tentacleTiming));
			}
			currentMove++;
			if (currentMove == tentacles.Length)
			{
				currentMove = 0;
			}
			moveTime = 0f;
		}
		moveTime += delta;
	}

	private IEnumerator Move(Tentacle e, Vector3 outward, float duration)
	{
		Rigidbody b = e.leader;
		for (int i = 0; i < e.cols.Length; i++)
		{
			e.cols[i].material = slippery;
		}
		for (float t = 0f; t < duration; t += Time.fixedDeltaTime)
		{
			if (e.intensity == 0f)
			{
				break;
			}
			float pct = t / duration;
			b.AddForce(outwardForceCurve.Evaluate(pct) * outward * moveForce, ForceMode.Force);
			b.AddForce(upForceCurve.Evaluate(pct) * Vector3.up * moveForce, ForceMode.Force);
			Vector3 dir = direction + (e.limb.transform.position - main.transform.position).normalized * 0.15f * e.forwardScale;
			dir.Normalize();
			Vector3 forward = forwardForceCurve.Evaluate(pct) * dir * moveForce * e.forwardScale;
			b.AddForce(forward * e.intensity, ForceMode.Force);
			if (forward.y > 0f)
			{
				forward.y = Mathf.Pow(forward.y / 7f, 2f) * 6f;
			}
			forward *= (float)health / (1f * (float)maxHealth);
			main.Rigidbody.AddForce(forward / 8f, ForceMode.Force);
			yield return new WaitForFixedUpdate();
		}
		for (int j = 0; j < e.cols.Length; j++)
		{
			e.cols[j].material = sticky;
		}
	}

	private void SetupTentacles()
	{
		for (int i = 0; i < tentacles.Length; i++)
		{
			LimbController limb = tentacles[i].limb;
			limb.Severed = (Action<LimbController>)Delegate.Combine(limb.Severed, new Action<LimbController>(LostLimb));
			Rubberise(tentacles[i]);
		}
	}

	private void Rubberise(Tentacle e)
	{
		for (int i = 0; i < e.cols.Length; i++)
		{
			e.cols[i].material = sticky;
		}
	}

	private void LostLimb(LimbController limb)
	{
		limb.content.parent = main.transform.parent;
		CharacterJoint characterJoint = limb.content.gameObject.AddComponent<CharacterJoint>();
		characterJoint.CopyJoint(jointPrefab);
		float breakForce = (characterJoint.breakTorque = float.PositiveInfinity);
		characterJoint.breakForce = breakForce;
		characterJoint.connectedBody = main.Rigidbody;
		SphereCollider sphereCollider = characterJoint.gameObject.AddComponent<SphereCollider>();
		sphereCollider.center = Vector3.up * 0.1f;
		sphereCollider.radius = 0.2f;
		main.density += 0.4f * main.density;
		health--;
		tentacles[limb.index].intensity = 0f;
	}
}
