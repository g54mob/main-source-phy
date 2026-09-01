using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("Physics/AI/LimbBodyJointHandler")]
public class LimbBodyJointHandler : SimBehaviour, IFireEffect
{
	private const float GRAB_RATE = 0.5f;

	private const int deathCompletionWeight = 10;

	private const float ATTACK_RANGE = 1000f;

	public Action<LimbBodyJointHandler> OnDeath;

	[Header("Tentacle")]
	public new string name = string.Empty;

	public Rigidbody[] rigidbodies = new Rigidbody[0];

	public Renderer[] renderers = new Renderer[0];

	public Vector3[] defaultRots = new Vector3[0];

	public Joint[] joints = new Joint[0];

	public CollisionHook[] collisions = new CollisionHook[0];

	public JointBreakHook[] slices = new JointBreakHook[0];

	public FireTag[] fires = new FireTag[0];

	public bool denest;

	public float wiggleStrength;

	public float appearDelay = 2f;

	public float jointStiffness = 1f;

	public BasicInfo clam;

	[Header("Attack & Damage")]
	public AudioSource sfx;

	public AudioClip[] atkSfx = new AudioClip[0];

	public AudioClip[] dmgSfx = new AudioClip[0];

	public LayerMask grabMask;

	public float attackImpactThreshold = 1000f;

	public float damageImpactThreshold = 1000f;

	public float crunchDamageThreshold = 1000f;

	public Vector3 crunchVector = new Vector3(0f, -1f, 0f);

	[FormerlySerializedAs("delay")]
	[Header("Behaviour")]
	public float startDelay = 1f;

	public float slapDuration = 10f;

	public float crunchDuration = 25f;

	public float grabDuration = 35f;

	public float loopToIdleChance = 0.2f;

	[HideInInspector]
	[SerializeField]
	private List<Vector3> sourcePos = new List<Vector3>();

	[SerializeField]
	[HideInInspector]
	private List<Vector3> sourceDir = new List<Vector3>();

	[SerializeField]
	[HideInInspector]
	private List<Transform> parents = new List<Transform>();

	[HideInInspector]
	[SerializeField]
	private float offset;

	[SerializeField]
	[HideInInspector]
	private float ptime = -3f;

	private FixedJoint grabJoint;

	private float stateTimer;

	private float attackPct;

	private float grabTimer;

	private float startTime;

	private BasicInfo target;

	private bool waiting;

	private bool setup;

	private bool grabbed;

	private bool alive = true;

	private float HP = 20f;

	private float MAX_HP = 20f;

	private float HP_TO_SLICE = 10f;

	private float HP_TO_FLEE = 4f;

	private Material mat;

	private int[] children = new int[8];

	private HashSet<BasicInfo> scaryTargets = new HashSet<BasicInfo>();

	private int damageLevels = 10;

	[SerializeField]
	[HideInInspector]
	private List<Vector3> jointsToAdjustInBuildMode = new List<Vector3>();

	private Vector3 lastCenter = Vector3.zero;

	private float nextUpdate;

	[HideInInspector]
	[SerializeField]
	private bool[] lastInWater = new bool[8] { true, true, true, true, true, true, true, true };

	[HideInInspector]
	public Vector3 Pos = Vector3.zero;

	private float burnTime;

	protected ParticleSystem.EmitParams emitter = default(ParticleSystem.EmitParams);

	private bool slicing;

	private float dmgTime = -1f;

	private float dmgInterval = 0.5f;

	private float distToTarget = 1001f;

	private bool crunch
	{
		get
		{
			return stateTimer < slapDuration && stateTimer < crunchDuration;
		}
	}

	public bool Alive
	{
		get
		{
			return alive;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (!base.isSimulating && base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.PreSetup = (Action<int>)Delegate.Combine(WinCondition.PreSetup, new Action<int>(AddCompletionWeights));
		}
	}

	private void OnDestroy()
	{
		WinCondition.PreSetup = (Action<int>)Delegate.Remove(WinCondition.PreSetup, new Action<int>(AddCompletionWeights));
	}

	private void AddCompletionWeights(int c)
	{
		if (WinCondition.Instance != null)
		{
			WinCondition.Instance.objectiveObjectCount += 10 + damageLevels - 1;
		}
	}

	protected override void Start()
	{
		base.Start();
		if (!base.isSimulating)
		{
			setup = true;
			offset = UnityEngine.Random.Range(0f, (float)Math.PI * 2f);
			lastInWater = new bool[rigidbodies.Length];
			for (int i = 0; i < rigidbodies.Length; i++)
			{
				lastInWater[i] = true;
				Rigidbody rigidbody = rigidbodies[i];
				sourcePos.Add(rigidbody.transform.position);
				sourceDir.Add(rigidbody.transform.forward);
				parents.Add(rigidbody.transform.parent);
			}
			StartCoroutine(SetUnderwater(appearDelay));
			ResetJoints();
			ScaleJoints(jointStiffness);
			SetKinematic(false, denest);
			jointsToAdjustInBuildMode.Clear();
			for (int j = 0; j < joints.Length; j++)
			{
				if (joints[j].breakForce != float.PositiveInfinity)
				{
					jointsToAdjustInBuildMode.Add(new Vector3(j, joints[j].breakForce, joints[j].breakTorque));
					joints[j].breakForce = float.PositiveInfinity;
					joints[j].breakTorque = float.PositiveInfinity;
				}
			}
		}
		else
		{
			SetKinematic(true, denest);
			ResetJoints();
			SetKinematic(false, denest);
			for (int k = 0; k < collisions.Length; k++)
			{
				collisions[k].CollisionHappend += OnCollide;
				collisions[k].ExplosionHappend += OnExplode;
			}
			for (int l = 0; l < slices.Length; l++)
			{
				JointBreakHook obj = slices[l];
				obj.JointBroke = (Action<Rigidbody>)Delegate.Combine(obj.JointBroke, new Action<Rigidbody>(Slice));
			}
			mat = renderers[0].material;
			for (int m = 0; m < renderers.Length; m++)
			{
				renderers[m].sharedMaterial = mat;
			}
			IFireEffect[] affected = new IFireEffect[1] { this };
			for (int n = 0; n < fires.Length; n++)
			{
				fires[n].SetAffected(affected);
			}
			children = new int[rigidbodies.Length];
			for (int num = 0; num < children.Length; num++)
			{
				children[num] = rigidbodies[num].transform.childCount;
			}
			for (int num2 = 0; num2 < jointsToAdjustInBuildMode.Count; num2++)
			{
				int num3 = (int)jointsToAdjustInBuildMode[num2].x;
				joints[num3].breakForce = jointsToAdjustInBuildMode[num2].y;
				joints[num3].breakTorque = jointsToAdjustInBuildMode[num2].z;
			}
		}
		startTime = Time.time + startDelay;
	}

	public bool OnIgnite(FireTag t, Collider c, bool pyroMode)
	{
		t.burning = (t.hasBeenBurned = false);
		burnTime = Time.time + 3f;
		return false;
	}

	private bool OnExplode(GameObject hit, float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		float value = (explosionPos - hit.transform.position).magnitude / 20f;
		float num = 1f - Mathf.Clamp01(value);
		if ((mask & 4) != 0)
		{
			num *= 2f;
		}
		if ((mask & 1) != 0)
		{
			num *= 2f;
		}
		bool flag = alive;
		TakeDamage(num);
		if (flag && !alive)
		{
			AttemptSlice(explosionPos, true);
		}
		startDelay = 0f;
		return num > 0f;
	}

	private void OnCollide(Collision col)
	{
		float num = col.relativeVelocity.sqrMagnitude;
		Collider collider = col.collider;
		switch (collider.gameObject.layer)
		{
		case 0:
		case 15:
		case 25:
			if (collider.CompareTag("Cannonball"))
			{
				if (num > damageImpactThreshold)
				{
					ReleaseGrab();
					TakeDamage(Mathf.Clamp(2f * num / damageImpactThreshold, 0f, 4f));
					EmitBlood(col.contacts[0].point);
					if (col.contacts.Length > 1)
					{
						EmitBlood(col.contacts[1].point);
					}
				}
			}
			else
			{
				if (grabbed)
				{
					break;
				}
				float num3 = ((!crunch) ? attackImpactThreshold : crunchDamageThreshold);
				if (!(num > num3) || collider.CompareTag("ArmourTag"))
				{
					break;
				}
				Rigidbody attachedRigidbody3 = col.contacts[0].thisCollider.attachedRigidbody;
				if (!crunch && attachedRigidbody3.velocity.sqrMagnitude < num3)
				{
					break;
				}
				Rigidbody attachedRigidbody4 = collider.attachedRigidbody;
				if (!attachedRigidbody4)
				{
					break;
				}
				BlockBehaviour component2 = attachedRigidbody4.GetComponent<BlockBehaviour>();
				if ((bool)component2)
				{
					AttemptDamageBlock(component2, num / num3);
					if (HP / MAX_HP < 0.9f)
					{
						ApplyBlood(component2);
					}
				}
			}
			break;
		case 26:
		{
			ReleaseGrab();
			Rigidbody attachedRigidbody2 = collider.attachedRigidbody;
			float sqrMagnitude = attachedRigidbody2.angularVelocity.sqrMagnitude;
			if (num + sqrMagnitude > damageImpactThreshold)
			{
				float sqrMagnitude2 = attachedRigidbody2.velocity.sqrMagnitude;
				if ((bool)attachedRigidbody2 && sqrMagnitude2 + sqrMagnitude < damageImpactThreshold * 0.2f)
				{
					break;
				}
				num = (sqrMagnitude2 + num) * 0.5f;
				if (sqrMagnitude > num)
				{
					num = sqrMagnitude;
				}
				float num2 = num / damageImpactThreshold;
				if (num2 > 4f)
				{
					num2 = 4f;
				}
				TakeDamage(num2);
				if (HP < HP_TO_SLICE)
				{
					AttemptSlice(col.contacts[0].point);
				}
				else if (num2 > damageImpactThreshold * 5f && sqrMagnitude2 > damageImpactThreshold)
				{
					AttemptSlice(col.contacts[0].point);
				}
				EmitBlood(col.contacts[0].point);
			}
			if (num > damageImpactThreshold * 0.3f)
			{
				if (!scaryTargets.Contains(target))
				{
					scaryTargets.Add(target);
				}
				if (UnityEngine.Random.value < loopToIdleChance)
				{
					ReturnToIdle(3f);
				}
				else
				{
					GetNewTarget();
				}
			}
			break;
		}
		case 28:
			if (num > attackImpactThreshold)
			{
				Rigidbody attachedRigidbody = collider.attachedRigidbody;
				if (attachedRigidbody == clam.Rigidbody)
				{
					FixedJoint component = attachedRigidbody.GetComponent<FixedJoint>();
					component.breakForce = (component.breakTorque *= 0.5f);
				}
			}
			break;
		}
	}

	private void ApplyBlood(BlockBehaviour block)
	{
		if (!block.Prefab.hasBVC)
		{
			return;
		}
		block.VisualController.SetBloodyLevel(1f, KrakkenController.BloodColor);
		if (StatMaster.isHosting && base.SimPhysics && !StatMaster.IsLevelEditorOnly)
		{
			if (base.NetBlock != null)
			{
				base.NetBlock.Event(NetworkEntity.EntityEvent.SetBloodyLevel);
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
	}

	private void EmitBlood(Vector3 point)
	{
		emitter.position = point;
		emitter.startColor = new Color(0f, 0.75f, 0.55f);
		emitter.rotation = UnityEngine.Random.Range(0f, 360f);
		GlobalParticles.EmitParticleBursts(5, emitter);
	}

	private bool AttemptDamageBlock(BlockBehaviour block, float dmg)
	{
		if (!block.Prefab.hasHealthBar || block.BlockHealth.health <= 0f)
		{
			return false;
		}
		block.BlockHealth.DamageBlock(dmg);
		return dmg > 0f;
	}

	private void AttemptSlice(Vector3 point, bool onlyWeak = false)
	{
		int num = joints.Length - 1;
		float num2 = float.MaxValue;
		for (int i = 0; i < joints.Length - 1; i++)
		{
			Joint joint = joints[i];
			if (!(joint == null) && (!onlyWeak || joint.breakForce != float.PositiveInfinity))
			{
				float sqrMagnitude = (point - joint.transform.position).sqrMagnitude;
				if (sqrMagnitude < num2)
				{
					num = i;
					num2 = sqrMagnitude;
				}
			}
		}
		if (joints[num].breakForce < float.MaxValue)
		{
			if (HP > HP_TO_SLICE)
			{
				TakeDamage(HP * 0.5f);
			}
			joints[num].breakForce = (joints[num].breakTorque *= 0.5f);
			slicing = true;
		}
	}

	private void Slice(Rigidbody b)
	{
		if (!slicing && HP > HP_TO_SLICE)
		{
			TakeDamage(HP * 0.5f);
		}
		EmitBlood(b.transform.position);
		int num = Array.IndexOf(rigidbodies, b);
		Joint joint = ((num <= 0) ? b.GetComponent<Joint>() : joints[num]);
		Transform transform = joint.connectedBody.transform.FindChild("break");
		if (joint.breakForce < float.MaxValue)
		{
			float breakForce = (joint.breakTorque = 0f);
			joint.breakForce = breakForce;
		}
		if ((bool)transform)
		{
			transform.parent = b.transform;
			transform.localPosition = new Vector3(1.5f, 0f, 0f);
			transform.localRotation = Quaternion.identity;
		}
		if (num < 0)
		{
			if (alive)
			{
				Debug.LogError("Tentacle alive but bodies set as dead");
			}
			return;
		}
		for (int i = num; i < rigidbodies.Length; i++)
		{
			rigidbodies[i].useGravity = true;
			rigidbodies[i].drag = 1f;
		}
		num = Mathf.Max(1, num);
		Rigidbody[] destinationArray = new Rigidbody[num];
		Array.Copy(rigidbodies, destinationArray, num);
		rigidbodies = destinationArray;
		int[] destinationArray2 = new int[num];
		Array.Copy(children, destinationArray2, num);
		children = destinationArray2;
		Die();
	}

	public void TakeDamage(float hit)
	{
		if (!alive || Time.fixedTime < dmgTime + dmgInterval)
		{
			return;
		}
		HP -= hit;
		float num = hit / MAX_HP;
		if (base.gameObject.CompareTag("ObjectiveObj"))
		{
			int num2 = Mathf.RoundToInt(num * 10f);
			damageLevels -= num2;
			if (damageLevels < 0)
			{
				damageLevels = 0;
			}
			WinCondition.currentObjsCompleted += num2;
		}
		if (HP < 0f)
		{
			HP = 0f;
			Die();
		}
		else if (hit > 0.5f)
		{
			sfx.pitch = UnityEngine.Random.Range(0.85f, 0.9f);
			sfx.PlayOneShot(dmgSfx[UnityEngine.Random.Range(0, dmgSfx.Length)], 2f);
		}
		num = HP / MAX_HP;
		mat.SetFloat("_BloodAmount", 1f - num);
	}

	private void Die()
	{
		if (alive)
		{
			alive = false;
			if (base.gameObject.CompareTag("ObjectiveObj"))
			{
				WinCondition.currentObjsCompleted += 10 + damageLevels;
			}
			damageLevels = 0;
			if (!string.IsNullOrEmpty(name))
			{
				RealtimeUpdater.Instance.AddBox(name, name, InjuryType.Crushed, "Tentacular Torsion");
			}
			for (int i = 0; i < rigidbodies.Length; i++)
			{
				rigidbodies[i].useGravity = true;
				rigidbodies[i].drag = 1f;
			}
			if (WaterController.IsUnderwater(sfx.transform.position))
			{
				sfx.outputAudioMixerGroup = ReferenceMaster.GetWaterMixerFrom(sfx.outputAudioMixerGroup);
			}
			sfx.pitch = UnityEngine.Random.Range(0.6f, 0.8f);
			sfx.Play();
			if (OnDeath != null)
			{
				OnDeath(this);
			}
		}
	}

	private void OnEnable()
	{
		if (!base.isSimulating && setup)
		{
			SetKinematic(true, denest);
			ResetJoints();
			SetKinematic(false, denest);
		}
	}

	private void OnDisable()
	{
		waiting = false;
		if (ptime < 0f)
		{
			ptime = 0f;
		}
	}

	public void SetKinematic(bool kinematic, bool denest)
	{
		for (int i = 0; i < rigidbodies.Length; i++)
		{
			Rigidbody rigidbody = rigidbodies[i];
			if (!rigidbody)
			{
				continue;
			}
			if (denest)
			{
				if (kinematic)
				{
					rigidbody.transform.parent = parents[i];
					rigidbody.transform.localScale = Vector3.one;
				}
				else if (base.transform.parent.position == Vector3.zero)
				{
					rigidbody.transform.parent = base.transform.parent;
				}
				else
				{
					rigidbody.transform.parent = ReferenceMaster.physicsGoalInstance;
				}
			}
			rigidbody.isKinematic = kinematic;
		}
	}

	public void ScaleJoints(float scale)
	{
		for (int i = 0; i < joints.Length; i++)
		{
			CharacterJoint characterJoint = joints[i] as CharacterJoint;
			if ((bool)characterJoint)
			{
				SoftJointLimitSpring swingLimitSpring = characterJoint.swingLimitSpring;
				swingLimitSpring.spring *= scale;
				swingLimitSpring.damper *= scale;
				characterJoint.swingLimitSpring = swingLimitSpring;
				swingLimitSpring = characterJoint.twistLimitSpring;
				swingLimitSpring.spring *= scale;
				swingLimitSpring.damper *= scale;
				characterJoint.twistLimitSpring = swingLimitSpring;
			}
		}
	}

	public void ResetJoints()
	{
		Quaternion[] array = new Quaternion[rigidbodies.Length];
		for (int i = 0; i < rigidbodies.Length; i++)
		{
			Rigidbody rigidbody = rigidbodies[i];
			if ((bool)rigidbody)
			{
				array[i] = rigidbody.transform.localRotation;
				rigidbody.transform.localRotation = Quaternion.Euler(defaultRots[i]);
				Rigidbody rigidbody2 = rigidbody;
				Vector3 velocity = (rigidbody.angularVelocity = Vector3.zero);
				rigidbody2.velocity = velocity;
			}
		}
		for (int j = 0; j < joints.Length; j++)
		{
			Joint joint = joints[j];
			if ((bool)joint)
			{
				Rigidbody rigidbody = joint.connectedBody;
				joint.autoConfigureConnectedAnchor = false;
				joint.connectedBody = null;
				joint.autoConfigureConnectedAnchor = true;
				joint.connectedBody = rigidbody;
			}
		}
		for (int k = 0; k < rigidbodies.Length; k++)
		{
			Rigidbody rigidbody = rigidbodies[k];
			if ((bool)rigidbody)
			{
				rigidbody.transform.localRotation = array[k];
				Rigidbody rigidbody3 = rigidbody;
				Vector3 velocity = (rigidbody.angularVelocity = Vector3.zero);
				rigidbody3.velocity = velocity;
			}
		}
	}

	private void FixedUpdate()
	{
		if (!waiting && alive)
		{
			if (base.isSimulating)
			{
				Pos = rigidbodies[0].transform.position;
				Pos.y = rigidbodies[rigidbodies.Length / 2].transform.position.y;
				distToTarget = ((!target) ? 1001f : (target.transform.position - Pos).sqrMagnitude);
				if (Time.time < startTime || scaryTargets.Contains(target))
				{
					if (grabbed)
					{
						ReleaseGrab();
					}
					Idle();
					ResetState();
					GetNewTarget();
				}
				else if (distToTarget > 1000f)
				{
					if ((bool)target)
					{
						DebugExtension.DebugWireSphere(target.transform.position, Color.magenta, 0.2f, 0f);
						Debug.DrawLine(target.transform.position, Pos, Color.magenta);
					}
					ReturnToIdle(1f);
				}
				else
				{
					if (!grabbed)
					{
						if (target != clam && (HP < HP_TO_FLEE || burnTime > Time.time))
						{
							Flee();
						}
						else
						{
							if (attackPct == 0f)
							{
								sfx.PlayOneShot(atkSfx[UnityEngine.Random.Range(0, atkSfx.Length)], 2f);
							}
							Attack();
							if (stateTimer > crunchDuration)
							{
								if (grabTimer > 0.5f)
								{
									AttemptSuction();
								}
								grabTimer += Time.fixedDeltaTime;
								if (stateTimer > grabDuration)
								{
									ReturnToIdle(3f);
								}
							}
						}
						if (target.isDestroyed || !target.gameObject.activeInHierarchy)
						{
							ReturnToIdle(1f);
						}
					}
					else
					{
						if (grabTimer == 0f)
						{
							sfx.PlayOneShot(atkSfx[UnityEngine.Random.Range(0, atkSfx.Length)], 2f);
						}
						Throw();
					}
					stateTimer += Time.fixedDeltaTime;
				}
			}
			else
			{
				Idle();
				ResetState();
			}
			ptime += Time.fixedDeltaTime;
		}
		WaterVFX();
	}

	private void WaterVFX()
	{
		int num = 10;
		float num2 = float.MaxValue;
		float num3 = WaterController.waterTransformHeight + 1f;
		for (int i = 0; i < rigidbodies.Length - 1; i++)
		{
			Rigidbody rigidbody = rigidbodies[i];
			if (!(rigidbody == null))
			{
				float num4 = Mathf.Abs(rigidbody.worldCenterOfMass.y - num3);
				if (num4 < num2)
				{
					num = i;
					num2 = num4;
				}
			}
		}
		if (num >= rigidbodies.Length)
		{
			return;
		}
		WaterController waterController = ((!base.isSimulating) ? WaterController.buildInstance : WaterController.simInstance);
		Rigidbody rigidbody2 = rigidbodies[num];
		Vector3 worldCenterOfMass = rigidbody2.worldCenterOfMass;
		float sqrMagnitude = rigidbody2.velocity.sqrMagnitude;
		float num5 = WaterController.CheckHeightMap(worldCenterOfMass.x, worldCenterOfMass.z);
		float num6 = num5 - worldCenterOfMass.y;
		bool flag = num6 > 0f;
		if (flag != lastInWater[num])
		{
			Vector3 emitPosition = worldCenterOfMass;
			emitPosition.y = num5;
			waterController.EmitWaterParticles(emitPosition, (!(sqrMagnitude > 500f)) ? 1 : 2);
			waterController.EmitFoam(emitPosition, UnityEngine.Random.Range(0.8f, 1.4f), sqrMagnitude * 200f);
			lastInWater[num] = flag;
		}
		if (worldCenterOfMass.y < rigidbody2.transform.position.y)
		{
			num6 = 0f - num6;
		}
		Vector3 vector = ((!(num6 > 0f)) ? ((num <= 0) ? rigidbody2.transform.position : rigidbodies[num - 1].worldCenterOfMass) : rigidbodies[num + 1].worldCenterOfMass);
		Vector3 direction = vector - worldCenterOfMass;
		Plane plane = new Plane(Vector3.up, new Vector3(0f, num5, 0f));
		Ray ray = new Ray(worldCenterOfMass, direction);
		float enter = 0f;
		if (plane.Raycast(ray, out enter))
		{
			worldCenterOfMass += direction.normalized * enter;
			if (!(Mathf.Abs(worldCenterOfMass.y - num5) > 5f) && (!((lastCenter - worldCenterOfMass).sqrMagnitude < 0.1f) || !(nextUpdate > Time.fixedTime)))
			{
				waterController.EmitRipple(worldCenterOfMass, UnityEngine.Random.Range(0.85f, 1.25f), 200f - Mathf.Clamp(sqrMagnitude * 0.025f, 0f, 200f));
				lastCenter = worldCenterOfMass;
				nextUpdate = Time.fixedTime + 0.1f;
			}
		}
	}

	private void CheckForArrows(int i)
	{
		if (!base.isSimulating)
		{
			return;
		}
		Transform transform = rigidbodies[i].transform;
		int childCount = transform.childCount;
		if (childCount != children[i])
		{
			if (childCount > children[i])
			{
				TakeDamage(1f);
				EmitBlood(transform.GetChild(childCount - 1).position);
			}
			children[i] = childCount;
		}
	}

	private void ReturnToIdle(float delay)
	{
		if (Time.time + delay > startTime)
		{
			startTime = Time.time + delay;
		}
	}

	private void GetNewTarget()
	{
		if (!StatMaster.isMP && UnityEngine.Random.value > 0.5f && (clam.transform.position - Pos).sqrMagnitude < 1000f)
		{
			target = clam;
		}
		else
		{
			target = Machine.Active().GetRandomBlock();
		}
	}

	private void ResetState()
	{
		stateTimer = (attackPct = (grabTimer = 0f));
	}

	public void Idle()
	{
		for (int i = 0; i < rigidbodies.Length; i++)
		{
			Rigidbody b = rigidbodies[i];
			SteerBodyTowardsTarget(b, sourcePos[i], sourceDir[i], 40f, 100f);
			CheckForArrows(i);
		}
	}

	private void Flee()
	{
		Vector3 vector = target.transform.position - rigidbodies[rigidbodies.Length / 2].transform.position;
		vector.y *= 0.5f;
		vector = vector.normalized * (1f - Mathf.Clamp01(new Vector3(vector.x, vector.y * 0.1f, vector.z).sqrMagnitude * 0.002f));
		for (int num = rigidbodies.Length - 1; num >= 0; num--)
		{
			float num2 = (float)num / (1f * (float)(rigidbodies.Length - 1));
			num2 = 0.5f + 0.5f * (1f - num2);
			Vector3 b = sourcePos[num] - vector * num2 * 15f;
			b = Vector3.Lerp(sourcePos[num], b, num2);
			Rigidbody b2 = rigidbodies[num];
			SteerBodyTowardsTarget(b2, b, sourceDir[num], 40f, 100f);
			CheckForArrows(num);
		}
	}

	public void Attack()
	{
		float y = target.transform.position.y;
		y = Mathf.Clamp(10f + y * 0.5f, 5f, 10f);
		Vector3 vector = Vector3.zero;
		if (crunch)
		{
			vector = crunchVector * (1f + (stateTimer - slapDuration) / (crunchDuration - slapDuration));
		}
		for (int num = rigidbodies.Length - 1; num >= 0; num--)
		{
			float num2 = (float)num / (1f * (float)(rigidbodies.Length - 1));
			Rigidbody rigidbody = rigidbodies[num];
			Vector3 vector2 = target.transform.position;
			vector2.y *= num2;
			vector2 += Vector3.up * y * (1f - num2);
			vector2 += vector * num2;
			Vector3 vector3 = vector2 - rigidbody.transform.position;
			float magnitude = vector3.magnitude;
			float num3 = Mathf.Lerp(50f, 100f, num2);
			if (magnitude > num3)
			{
				vector3 = num3 * vector3 / magnitude;
				vector2 = rigidbody.transform.position + vector3;
			}
			vector2 = Vector3.Lerp(sourcePos[num], vector2, attackPct * num2);
			vector2 += UnityEngine.Random.insideUnitSphere * (1f - num2);
			vector3 = vector2 - rigidbody.transform.position;
			if (vector3.sqrMagnitude > 0f)
			{
				rigidbody.AddForce(vector3 * 15f, ForceMode.Acceleration);
			}
			CheckForArrows(num);
		}
		attackPct += Time.fixedDeltaTime * 10f * (1.1f - Mathf.Clamp01(distToTarget / 1000f));
	}

	protected void AttemptSuction()
	{
		int num = UnityEngine.Random.Range(0, rigidbodies.Length - 1);
		Rigidbody rigidbody = null;
		Rigidbody rigidbody2 = rigidbodies[num];
		Rigidbody rigidbody3 = rigidbodies[num + 1];
		Vector3 vector = rigidbody2.worldCenterOfMass + rigidbody2.transform.up * 0.1f;
		Vector3 vector2 = rigidbody3.worldCenterOfMass + rigidbody3.transform.up * 0.1f;
		Collider[] array = Physics.OverlapCapsule(vector, vector2, 0.75f, grabMask);
		Plane plane = new Plane((rigidbody2.transform.up + rigidbody3.transform.up) * 0.5f, (vector + vector2) * 0.5f);
		Transform root = Machine.Active().transform.root;
		foreach (Collider collider in array)
		{
			Rigidbody attachedRigidbody = collider.attachedRigidbody;
			if ((bool)attachedRigidbody && !attachedRigidbody.isKinematic && !(attachedRigidbody.transform.root != root) && !(attachedRigidbody.transform.parent == clam.transform.parent))
			{
				if (collider.gameObject.CompareTag("ArmourTag") || attachedRigidbody.gameObject.CompareTag("ArmourTag"))
				{
					rigidbody = null;
					break;
				}
				if (plane.GetSide(attachedRigidbody.worldCenterOfMass))
				{
					rigidbody = attachedRigidbody;
				}
			}
		}
		if (rigidbody != null)
		{
			grabbed = true;
			grabJoint = rigidbody2.gameObject.AddComponent<FixedJoint>();
			grabJoint.autoConfigureConnectedAnchor = true;
			grabJoint.connectedBody = rigidbody;
			FixedJoint fixedJoint = grabJoint;
			float num2 = 6000f;
			grabJoint.breakTorque = num2;
			fixedJoint.breakForce = num2;
		}
		grabTimer = 0f;
	}

	protected void Throw()
	{
		float num = Mathf.Clamp01(grabTimer) * 300f;
		for (int num2 = rigidbodies.Length - 1; num2 >= 0; num2--)
		{
			Rigidbody rigidbody = rigidbodies[num2];
			SteerBodyTowardsTarget(rigidbody, sourcePos[num2] + Vector3.up, sourceDir[num2], 80f, 400f - num * Mathf.Clamp01(rigidbody.velocity.sqrMagnitude / 50f));
			CheckForArrows(num2);
		}
		grabTimer += Time.fixedDeltaTime;
		if (grabJoint == null || grabTimer > 1.5f)
		{
			if (target is BlockBehaviour)
			{
				AttemptDamageBlock(target as BlockBehaviour, 2f);
			}
			ReleaseGrab();
		}
		attackPct = 0f;
		stateTimer = 0f;
	}

	protected void ReleaseGrab()
	{
		grabbed = false;
		grabTimer = 0f;
		if (grabJoint != null)
		{
			UnityEngine.Object.Destroy(grabJoint);
		}
	}

	public void SteerBodyTowardsTarget(Rigidbody b, Vector3 pos, Vector3 dir, float force, float clamp)
	{
		float num = Mathf.Clamp01(ptime * 0.25f);
		float num2 = ptime * 0.7f + offset;
		float num3 = Mathf.Clamp01((b.transform.position.y + 2f) / 10f);
		float num4 = Mathf.Cos(num2 + (float)Math.PI) * 0.6f + 0.4f;
		Vector3 vector = (b.transform.up + Vector3.down * 0.25f) * num4 * 3f * Mathf.Pow(num3, 2f);
		num4 = Mathf.Cos(num2) * 0.2f + 0.8f;
		num4 = Mathf.Lerp(1f, num4, num);
		Vector3 vector2 = UnityEngine.Random.insideUnitSphere * 0.1f + Mathf.Sin(Time.time * 2f + offset + num3 * 2f) * (b.transform.up * 0.5f + b.transform.forward) * num3;
		vector2 *= wiggleStrength;
		Vector3 vector3 = pos + (vector + vector2) * num - b.transform.position;
		b.AddForce(Vector3.ClampMagnitude(vector3 * force, clamp) * num4, ForceMode.Acceleration);
		vector = Mathf.Sin(num2) * b.transform.up * (1f - num3) * num;
		vector3 = Vector3.Cross(b.transform.forward, dir + vector);
		b.AddTorque(Vector3.ClampMagnitude(vector3 * force, clamp) * 0.5f * num4, ForceMode.Acceleration);
	}

	public IEnumerator SetUnderwater(float duration)
	{
		waiting = true;
		Quaternion[] rots = new Quaternion[rigidbodies.Length];
		for (int i = 0; i < rigidbodies.Length; i++)
		{
			Rigidbody b = rigidbodies[i];
			if ((bool)b)
			{
				b.transform.localRotation = Quaternion.Euler(defaultRots[i] + new Vector3(0f, 0f, -25f - 15f * Mathf.InverseLerp(rigidbodies.Length, 0f, i)));
				rots[i] = b.transform.rotation;
			}
		}
		yield return new WaitForEndOfFrame();
		for (float t = 0f; t < duration; t += Time.fixedDeltaTime)
		{
			for (int j = 0; j < rigidbodies.Length; j++)
			{
				Rigidbody b = rigidbodies[j];
				if ((bool)b)
				{
					b.transform.rotation = rots[j];
					Rigidbody rigidbody = b;
					Vector3 velocity = (b.angularVelocity = Vector3.zero);
					rigidbody.velocity = velocity;
				}
			}
			yield return new WaitForFixedUpdate();
		}
		waiting = false;
	}
}
