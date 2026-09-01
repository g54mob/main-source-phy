using Landfall.TABS.GameState;
using Photon.Bolt;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class Wings : MonoBehaviour
{
	public LayerMask mask;

	public AnimationCurve flightCurve;

	public float deadPowerDecay = 2f;

	public float heightVariance = 0.5f;

	public float variationSpeed = 0.5f;

	public float flightForce;

	public float legForceMultiplier = 1f;

	public float headM = 0.5f;

	public Animator animator1;

	public Animator animator2;

	public bool useWings = true;

	public bool useWingsInPlacement = true;

	public float rotationTorque = 10f;

	[Tooltip("Enable if units move erratically on the client side of ProjectMars games. Only enable if you are sure Wings.cs is causing erratic movement.")]
	public bool setUnitMainRigKinematic;

	private DataHandler data;

	private float deadPower = 1f;

	private Rigidbody rightFootRig;

	private Rigidbody leftFootRig;

	private Rigidbody headRig;

	private float time;

	private bool dead;

	private GameStateManager m_gameStateManager;

	private Keyframe flightCurveLastKeyFrame;

	private NativeArray<RaycastCommand> raycastCommands;

	private NativeArray<RaycastHit> raycastHits;

	private JobHandle jobHandle;

	private void Awake()
	{
		raycastCommands = new NativeArray<RaycastCommand>(1, Allocator.Persistent);
		raycastHits = new NativeArray<RaycastHit>(1, Allocator.Persistent);
	}

	private void Start()
	{
		data = base.transform.root.GetComponentInChildren<DataHandler>();
		data.takeFallDamage = false;
		data.canFall = false;
		if ((bool)data.footRight)
		{
			rightFootRig = data.footRight.GetComponent<Rigidbody>();
		}
		if ((bool)data.footLeft)
		{
			leftFootRig = data.footLeft.GetComponent<Rigidbody>();
		}
		if ((bool)data.head)
		{
			headRig = data.head.GetComponent<Rigidbody>();
		}
		AnimationHandler component = data.GetComponent<AnimationHandler>();
		if ((bool)component)
		{
			component.multiplier = 0.5f;
		}
		heightVariance *= Random.value;
		time = Random.Range(0f, 1000f);
		Balance component2 = data.GetComponent<Balance>();
		if ((bool)component2)
		{
			component2.enabled = false;
		}
		m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
		if (flightCurve.keys.Length != 0)
		{
			flightCurveLastKeyFrame = flightCurve.keys[flightCurve.keys.Length - 1];
		}
		if (setUnitMainRigKinematic && BoltNetwork.IsClient)
		{
			data.mainRig.isKinematic = true;
		}
	}

	private void FixedUpdate()
	{
		if ((!useWingsInPlacement && m_gameStateManager.GameState != GameState.BattleState) || !useWings)
		{
			return;
		}
		if ((bool)data && data.Dead)
		{
			if (data.allRigs.AllRigs[0] != null)
			{
				if (deadPower > 0f)
				{
					data.mainRig.AddForce(deadPower * flightForce * data.mainRig.transform.up, ForceMode.Acceleration);
					data.mainRig.AddForce(0.3f * deadPower * flightForce * Vector3.up, ForceMode.Acceleration);
					deadPower -= Time.deltaTime * deadPowerDecay;
				}
				else
				{
					if (dead)
					{
						return;
					}
					dead = true;
					if ((bool)animator1)
					{
						animator1.speed = 0f;
						animator1.transform.SetParent(base.transform.root);
						Rigidbody rigidbody = animator1.gameObject.AddComponent<Rigidbody>();
						if ((bool)rigidbody && (bool)data.mainRig)
						{
							rigidbody.gameObject.AddComponent<SetInterpolation>();
							rigidbody.velocity = data.mainRig.velocity;
						}
						animator1.GetComponentInChildren<Collider>().enabled = true;
						animator1.gameObject.AddComponent<RemoveAfterSeconds>().shrink = true;
					}
					if ((bool)animator2)
					{
						animator2.speed = 0f;
						animator2.transform.SetParent(base.transform.root);
						Rigidbody rigidbody2 = animator2.gameObject.AddComponent<Rigidbody>();
						if ((bool)rigidbody2 && (bool)data.mainRig)
						{
							rigidbody2.gameObject.AddComponent<SetInterpolation>();
							rigidbody2.velocity = data.mainRig.velocity;
						}
						animator2.GetComponentInChildren<Collider>().enabled = true;
						animator2.gameObject.AddComponent<RemoveAfterSeconds>().shrink = true;
					}
				}
			}
			else if (!dead)
			{
				dead = true;
				if ((bool)animator1)
				{
					animator1.speed = 0f;
				}
				if ((bool)animator2)
				{
					animator2.speed = 0f;
				}
			}
			return;
		}
		bool value = data.unit.m_PreferedDistance > data.distanceToTarget;
		if ((bool)animator1)
		{
			animator1.SetBool("InRange", value);
		}
		if ((bool)animator2)
		{
			animator2.SetBool("InRange", value);
		}
		jobHandle.Complete();
		RaycastHit raycastHit = raycastHits[0];
		if (raycastHit.distance > 0f)
		{
			float num = raycastHit.distance + Mathf.Cos((Time.time + time) * variationSpeed) * heightVariance;
			data.mainRig.AddTorque(rotationTorque * Vector3.Angle(data.mainRig.transform.up, data.groundedMovementDirectionObject.forward) * Vector3.Cross(data.mainRig.transform.up, data.groundedMovementDirectionObject.forward), ForceMode.Acceleration);
			float num2 = flightForce * flightCurve.Evaluate(num);
			float num3 = num2 * legForceMultiplier * 0.5f;
			if ((bool)headRig)
			{
				headRig.AddForce(num2 * headM * Vector3.up, ForceMode.Acceleration);
			}
			data.mainRig.AddForce(num2 * Vector3.up, ForceMode.Acceleration);
			if ((bool)rightFootRig)
			{
				rightFootRig.AddForce(num3 * Vector3.up, ForceMode.Acceleration);
			}
			if ((bool)leftFootRig)
			{
				leftFootRig.AddForce(num3 * Vector3.up, ForceMode.Acceleration);
			}
			data.TouchGround(raycastHit.point, raycastHit.normal);
		}
		raycastCommands[0] = new RaycastCommand(base.transform.position, Vector3.down, flightCurveLastKeyFrame.time, mask);
		jobHandle = RaycastCommand.ScheduleBatch(raycastCommands, raycastHits, 1);
	}

	private void OnDestroy()
	{
		jobHandle.Complete();
		raycastCommands.Dispose();
		raycastHits.Dispose();
	}

	public void EnableFlight()
	{
		useWings = true;
	}

	public void DiableFlight()
	{
		useWings = false;
	}
}
