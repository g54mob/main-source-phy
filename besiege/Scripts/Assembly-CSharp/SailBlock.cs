using System;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/SailBlock")]
public class SailBlock : BlockBehaviour
{
	private const float windSpeed = 1f;

	private const float sailArea = 50f;

	private const float airDensity = 1.225f;

	private const float dragCoefficient = 2f;

	private const float liftCoefficient = 2.5f;

	private const float magicCoefficient = 3f;

	public static bool SidePlacing = false;

	public SkinnedMeshRenderer skinnedMesh;

	public int version = 1;

	private MKey raiseSailKey;

	private MKey lowerSailKey;

	private MSlider sizeSlider;

	private MSlider speedSlider;

	private MToggle holdMode;

	private MToggle inverted;

	public float counterTorquePower = 10f;

	public float power = 100f;

	public float maxSpeed = 30f;

	public AnimationCurve maxSpeedRamp = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	public float sailSpeed = 1.25f;

	public GameObject baseTrigger;

	public GameObject[] sideTriggers;

	public Mesh defMesh;

	public Mesh sideMesh;

	public Mesh defMeshNoSail;

	public Mesh sideMeshNoSail;

	public Mesh sailOnly;

	public Material sailMat;

	public GameObject defBreak;

	public GameObject sideBreak;

	public GameObject skinBreak;

	public GameObject topBeam;

	public GameObject bottomBeam;

	public AudioSource sailCue;

	public GameObject brokenSail;

	public Cloth sailCloth;

	public CapsuleCollider dummyCapsule;

	public GameObject brokenFrame;

	private float currentSpeed;

	private bool maintainingSpeed;

	private bool isFrozen;

	private float input = 1f;

	private float prevInput = float.MaxValue;

	private float prevInputFixed = float.MaxValue;

	private bool raiseHeld;

	private bool lowerHeld;

	private bool emuRaiseHeld;

	private bool emuLowerHeld;

	public bool EmulateWind;

	public bool sidePlaced;

	private float lastOffset;

	private float targetOffset;

	private Vector3 sailPos;

	private Vector3 sailSize;

	public LayerMask mask;

	private bool sailBroken;

	private bool broken;

	private bool internallyFlipped;

	[HideInInspector]
	[SerializeField]
	private int uv;

	private float proximityClear = 1f;

	[SerializeField]
	[HideInInspector]
	private float offsetArea;

	[HideInInspector]
	[SerializeField]
	private float area = 1f;

	public float limitingBias = 1f;

	private static Collider[] results = new Collider[5];

	private float startLerp;

	private Vector3 worldUp = Vector3.up;

	private static Vector3 windDir = Vector3.forward;

	public MKey RaiseSail
	{
		get
		{
			return raiseSailKey;
		}
	}

	public MKey LowerSail
	{
		get
		{
			return lowerSailKey;
		}
	}

	public MSlider SizeSlider
	{
		get
		{
			return sizeSlider;
		}
	}

	public MSlider SpeedSlider
	{
		get
		{
			return speedSlider;
		}
	}

	public override bool OnFlip(bool sound, bool isUndo)
	{
		if (Flipped != internallyFlipped)
		{
			if (sound || isUndo)
			{
				if (sound)
				{
					ReferenceMaster.PlayFlip();
				}
				base.transform.Rotate(Vector3.forward, 180f, Space.Self);
				Rotation = base.transform.localRotation;
			}
			internallyFlipped = Flipped;
		}
		return true;
	}

	public override Vector3 GetTarget()
	{
		return GetCenter();
	}

	public override Vector3 GetCenter()
	{
		return base.transform.TransformPoint(new Vector3(0f, 0f, 1.5f));
	}

	protected override void Awake()
	{
		base.Awake();
		BlockSkinnedVisualController obj = VisualController as BlockSkinnedVisualController;
		obj.onMeshChanged = (Action<BlockSkinLoader.SkinPack.Skin>)Delegate.Combine(obj.onMeshChanged, new Action<BlockSkinLoader.SkinPack.Skin>(SetMesh));
		BlockSkinnedVisualController obj2 = VisualController as BlockSkinnedVisualController;
		obj2.onMatChanged = (Action<BlockSkinLoader.SkinPack.Skin>)Delegate.Combine(obj2.onMatChanged, new Action<BlockSkinLoader.SkinPack.Skin>(SetMaterial));
		VisualController.AssignMaterialProperty("_SailTex", skinnedMesh.sharedMaterial.mainTexture);
		raiseSailKey = AddKey(4465, "UnFurl", ControlScheme.BlockControls.Sail, 1, KeyCode.K);
		lowerSailKey = AddKey(4464, "Furl", ControlScheme.BlockControls.Sail, 0, KeyCode.I);
		sizeSlider = AddSlider(4647, "sail-size", 1f, 0f, 1f, string.Empty);
		sizeSlider.ValueChanged += delegate
		{
			skinnedMesh.SetBlendShapeWeight(0, 100f - 100f * Mathf.Clamp01(sizeSlider.Value));
			UpdateArea();
		};
		speedSlider = AddSlider(4624, "fold-speed", 1f, 0f, 1.5f, string.Empty);
		holdMode = AddToggle(4611, "hold-mode", false);
		inverted = AddToggle(4644, "reverse", false);
		inverted.DisplayInMapper = StatMaster.advancedBuilding;
		ReferenceMaster.onAdvancedBuildingToggled = (Action)Delegate.Combine(ReferenceMaster.onAdvancedBuildingToggled, new Action(ToggleInvertedDisplay));
		holdMode.Toggled += SetKeysShown;
		inverted.Toggled += ToggleMinMaxSize;
		VisualController.AssignMaterialProperty("_Intensity", 0.7f);
		if (isSimulating)
		{
			if (base.InWind)
			{
				windDir = base.CurrentWindController.transform.TransformDirection(base.CurrentWindController.windPower).normalized;
			}
			brokenSail.transform.parent = ReferenceMaster.physicsGoalInstance;
			brokenFrame.transform.parent = ReferenceMaster.physicsGoalInstance;
			SetClothSize();
			VisualController.AssignMaterialProperty("_uv", uv, false);
			if (sidePlaced)
			{
				UnityEngine.Object.DestroyImmediate(blockJoint);
			}
			else
			{
				for (int num = 0; num < sideTriggers.Length; num++)
				{
					UnityEngine.Object.Destroy(sideTriggers[num]);
				}
			}
			if (SimPhysics)
			{
			}
		}
		else
		{
			SetSidePlaced(sidePlaced || (SidePlacing && !_parentMachine.isLoadingInfo));
			uv = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
			VisualController.AssignMaterialProperty("_uv", uv, false);
		}
	}

	private void ToggleInvertedDisplay()
	{
		inverted.DisplayInMapper = StatMaster.advancedBuilding;
	}

	private void UpdateArea()
	{
		offsetArea = 0f;
		area = Mathf.Clamp01(sizeSlider.Value);
		if (inverted.IsActive)
		{
			offsetArea = area;
			area = 1f - area;
			if (!holdMode.IsActive)
			{
				input = 0f;
			}
		}
	}

	public override void StartPhysics(bool isKinematic)
	{
		base.StartPhysics(isKinematic);
		if (!noRigidbody && sidePlaced)
		{
			Rigidbody.centerOfMass = new Vector3(0f, 0f, 1.5f);
		}
	}

	private void ToggleMinMaxSize(bool b)
	{
		sizeSlider.ResetLocalisation((!b) ? 4647 : 4648);
		SetKeysShown(b);
	}

	private void SetKeysShown(bool b)
	{
		raiseSailKey.DisplayInMapper = !holdMode.IsActive || !inverted.IsActive;
		lowerSailKey.DisplayInMapper = !holdMode.IsActive || inverted.IsActive;
		UpdateArea();
	}

	private void SetClothSize()
	{
		sailPos = new Vector3(0f, -0.25f, 2.6f - 1.1f * input);
		sailSize = new Vector3(2.5f, 0.6f, 0.7f + 1.8f * input) / 2f;
	}

	public void SetSidePlaced(bool b)
	{
		sidePlaced = b;
		if (!StatMaster.isMP || StatMaster.isHosting || (base.HasParentMachine && base.ParentMachine.isLocalSim))
		{
			baseTrigger.SetActive(!sidePlaced);
			for (int i = 0; i < sideTriggers.Length; i++)
			{
				sideTriggers[i].SetActive(sidePlaced);
			}
			CapsuleCollider capsuleCollider = myBounds.childColliders[1] as CapsuleCollider;
			Vector3 vector = capsuleCollider.center;
			if (sidePlaced || version == 0)
			{
				capsuleCollider.height = 0.5f;
				vector.z = 0.25f;
				capsuleCollider.center = vector;
			}
			else
			{
				capsuleCollider.height = 1f;
				vector.z = 0f;
				capsuleCollider.center = vector;
			}
		}
		SetMesh(VisualController.selectedSkin);
	}

	protected void SetMesh(BlockSkinLoader.SkinPack.Skin skin)
	{
		if (!OptionsMaster.skinsEnabled || skin == null || skin.isDefault || skin.mesh == skin.prefab.DefaultSkin.mesh)
		{
			if (sailBroken)
			{
				skinnedMesh.sharedMesh = ((!sidePlaced) ? defMeshNoSail : sideMeshNoSail);
			}
			else
			{
				skinnedMesh.sharedMesh = ((!sidePlaced) ? defMesh : sideMesh);
				topBeam.SetActive(true);
			}
			if (!broken)
			{
				skinBreak.SetActive(false);
				sideBreak.SetActive(sidePlaced);
				defBreak.SetActive(!sidePlaced);
			}
		}
		else
		{
			skinnedMesh.sharedMesh = sailOnly;
			if (!sailBroken)
			{
				topBeam.SetActive(false);
			}
			if (!broken)
			{
				skinBreak.SetActive(true);
				sideBreak.SetActive(false);
				defBreak.SetActive(false);
			}
		}
	}

	protected void SetMaterial(BlockSkinLoader.SkinPack.Skin skin)
	{
		if (skin == null)
		{
			skin = VisualController.selectedSkin;
			if (skin == null)
			{
				return;
			}
		}
		if (!skin.isDefault && !StatMaster.clusterCoded && !StatMaster.aeroCoded && !StatMaster.stressCoded)
		{
			if (skin.material == null)
			{
				return;
			}
			if (skin.materials.Length < 2 || skin.mesh == skin.prefab.DefaultSkin.mesh)
			{
				Material[] sharedMaterials = new Material[2] { skin.material, sailMat };
				skinnedMesh.sharedMaterials = sharedMaterials;
			}
			else
			{
				skinnedMesh.sharedMaterials = skin.materials;
			}
		}
		VisualController.AssignMaterialProperty("_SailTex", skin.texture);
	}

	protected void OnCollisionEnter(Collision c)
	{
		if (SimPhysics && isSimulating && !StatMaster.GodTools.UnbreakableMode && c.relativeVelocity.sqrMagnitude > 2000f)
		{
			BreakAll(c.collider);
		}
	}

	public virtual void OnJointBreak(float breakForce)
	{
		if (blockJoint == null || blockJoint.connectedBody == null)
		{
			BreakAll();
			FragmentVisualController.EmitJointBreakMarker(base.transform.position);
		}
	}

	protected bool CheckSail()
	{
		if (sailBroken)
		{
			return true;
		}
		Vector3 vector = base.transform.TransformPoint(sailPos);
		int num = Physics.OverlapBoxNonAlloc(vector, sailSize, results, base.transform.rotation, mask, QueryTriggerInteraction.Ignore);
		bool flag = false;
		for (int i = 0; i < num; i++)
		{
			Rigidbody attachedRigidbody = results[i].attachedRigidbody;
			if (!(attachedRigidbody != Rigidbody) || !(attachedRigidbody != null))
			{
				continue;
			}
			if (attachedRigidbody.transform.parent.name.Contains("Simulation Machine"))
			{
				BlockBehaviour component = attachedRigidbody.GetComponent<BlockBehaviour>();
				if ((bool)component && component.ClusterIndex == base.ClusterIndex)
				{
					continue;
				}
			}
			flag = true;
			if ((attachedRigidbody.velocity - Rigidbody.velocity).sqrMagnitude > 100f)
			{
				BreakSail(results[i]);
				return true;
			}
		}
		if (flag)
		{
			if (proximityClear > -2.5f)
			{
				proximityClear -= Time.fixedDeltaTime * 8f;
			}
		}
		else if (proximityClear < 1f)
		{
			proximityClear += Time.fixedDeltaTime * 5f;
		}
		return false;
	}

	public void BreakSail(Collider other)
	{
		if (!sailBroken)
		{
			bottomBeam.transform.localPosition = new Vector3(0f, 0f, 2.48f * (1f - input));
			brokenSail.transform.rotation = base.transform.rotation;
			brokenSail.transform.position = GetCenter();
			CollideCloth(other);
			brokenSail.SetActive(true);
			sailBroken = true;
			BlockSkinLoader.SkinPack.Skin selectedSkin = VisualController.selectedSkin;
			if (selectedSkin == null || selectedSkin.isDefault)
			{
				skinnedMesh.sharedMesh = ((!sidePlaced) ? defMeshNoSail : sideMeshNoSail);
			}
			else
			{
				skinnedMesh.enabled = false;
			}
		}
	}

	public void CollideCloth(Collider other)
	{
		if (VisualController.BurnPct < 0.95f && (base.transform.position - SingleInstanceFindOnly<MouseOrbit>.Instance.transform.position).sqrMagnitude < 2500f)
		{
			float num = 1f - input;
			sailCloth.transform.localPosition = new Vector3(0f, 0f, 0.875f * num);
			sailCloth.transform.localScale = new Vector3(1.5f, 0.5f + input, 1.5f);
			sailCloth.transform.parent = ((!StatMaster.isMP) ? ReferenceMaster.physicsGoalInstance : base.transform.parent);
			if (!other)
			{
				return;
			}
			if (other is SphereCollider)
			{
				sailCloth.sphereColliders = new ClothSphereColliderPair[1]
				{
					new ClothSphereColliderPair(other as SphereCollider)
				};
			}
			else if (other is CapsuleCollider)
			{
				sailCloth.capsuleColliders = new CapsuleCollider[1] { other as CapsuleCollider };
			}
			else if (other is BoxCollider)
			{
				BoxCollider boxCollider = other as BoxCollider;
				dummyCapsule.center = boxCollider.center;
				Vector3 vector = Vector3.Scale(other.transform.lossyScale, boxCollider.size);
				float num2 = Mathf.Max(vector.x, vector.y, vector.z);
				if (vector.x == num2)
				{
					dummyCapsule.direction = 0;
					dummyCapsule.radius = Mathf.Min(vector.y, vector.z) * 0.5f;
					dummyCapsule.height = vector.x;
				}
				else if (vector.y == num2)
				{
					dummyCapsule.direction = 1;
					dummyCapsule.radius = Mathf.Min(vector.x, vector.z) * 0.5f;
					dummyCapsule.height = vector.y;
				}
				else if (vector.z == num2)
				{
					dummyCapsule.direction = 2;
					dummyCapsule.radius = Mathf.Min(vector.x, vector.y) * 0.5f;
					dummyCapsule.height = vector.z;
				}
				dummyCapsule.transform.parent = ((!StatMaster.isMP) ? ReferenceMaster.physicsGoalInstance : base.transform.parent);
				dummyCapsule.transform.position = other.transform.position;
				dummyCapsule.transform.rotation = other.transform.rotation;
				sailCloth.capsuleColliders = new CapsuleCollider[1] { dummyCapsule };
				dummyCapsule.gameObject.SetActive(true);
				if ((bool)other.attachedRigidbody)
				{
					Rigidbody component = dummyCapsule.GetComponent<Rigidbody>();
					component.isKinematic = false;
					component.velocity = other.attachedRigidbody.velocity;
					component.angularVelocity = other.attachedRigidbody.angularVelocity;
					component.WakeUp();
				}
			}
		}
		else
		{
			sailCloth.enabled = false;
			sailCloth.GetComponent<SkinnedMeshRenderer>().enabled = false;
		}
	}

	public void BreakAll(Collider other = null)
	{
		if (!broken)
		{
			broken = true;
			if (!sailBroken)
			{
				BreakSail(other);
			}
			brokenFrame.transform.rotation = base.transform.rotation;
			brokenFrame.transform.position = GetCenter();
			brokenFrame.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates(Prefab.RegisterSimUpdate, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (sailBroken)
		{
			raiseHeld = (lowerHeld = false);
			_parentMachine.UnregisterUpdate(this, false);
			return;
		}
		raiseHeld = raiseSailKey.IsHeld;
		lowerHeld = lowerSailKey.IsHeld;
		CheckKeys(raiseHeld || emuRaiseHeld, lowerHeld || emuLowerHeld, Time.deltaTime);
		if (input != prevInput)
		{
			prevInput = input;
			if (!SimPhysics)
			{
				originalDrag = offsetArea + area * input;
			}
			skinnedMesh.SetBlendShapeWeight(0, 100f - 100f * (input * area + offsetArea));
			SetClothSize();
		}
		if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim)
		{
			ClientSpeed();
		}
		float num = currentSpeed * input;
		if (num > 0.65f)
		{
			if (!maintainingSpeed)
			{
				if (!sailCue.isPlaying)
				{
					sailCue.pitch = UnityEngine.Random.Range(0.96f, 1.04f);
					sailCue.Play();
				}
				maintainingSpeed = true;
			}
		}
		else if (num < 0.3f)
		{
			maintainingSpeed = false;
		}
		if (Time.timeScale > 0f)
		{
			float t = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
			float num2 = Mathf.Lerp(lastOffset, targetOffset, t);
			float getSubmergedPctMV = base.GetSubmergedPctMV;
			VisualController.AssignMaterialProperty("_Intensity", Mathf.Clamp01((input * area + offsetArea) * 2f) * 0.7f, false);
			VisualController.AssignMaterialProperty("_Offset", num2 * proximityClear * (1f - getSubmergedPctMV), false);
			VisualController.AssignMaterialProperty("_Speed", 1f + 3f * (1f - getSubmergedPctMV), false);
			VisualController.AssignMaterialProperty("_Bleed", 0.15f * (1f - getSubmergedPctMV), false);
			float num3 = 1f - getSubmergedPctMV * 0.3f;
			float num4 = 1f - fireTag.fireControllerCode.fireProgress;
			num3 *= num4;
			VisualController.AssignMaterialProperty("_SailColor", new Color(num3, num3, num3, 1f));
		}
	}

	public override void FixedUpdateBlock()
	{
		if (!_parentMachine.isReady)
		{
			return;
		}
		if (noRigidbody || !SimPhysics || sailBroken)
		{
			_parentMachine.UnregisterFixedUpdate(this, false);
		}
		else
		{
			if (isFrozen || (!StatMaster.GodTools.UnbreakableMode && CheckSail()))
			{
				return;
			}
			if (input != prevInputFixed)
			{
				originalDrag = offsetArea + area * input;
				Rigidbody.drag = originalDrag * 2f + WaterDrag;
				prevInputFixed = input;
			}
			if (power != 0f)
			{
				float num = 1f - fireTag.fireControllerCode.fireProgress;
				if (num <= 0f)
				{
					BreakAll();
				}
				else if (EmulateWind || base.InWind)
				{
					EmulateRealWind();
				}
				else
				{
					AlwaysForward();
				}
			}
		}
	}

	protected void ClientSpeed()
	{
		Vector3 pushDir = -base.transform.up;
		Vector3 velocity = NetBlock.Velocity;
		velocity.y = 0f;
		float mag;
		ClampSpeed(pushDir, velocity, out mag);
	}

	public void AlwaysForward()
	{
		Vector3 vector = -base.transform.up;
		Vector3 velocity = Rigidbody.velocity;
		Vector3 vector2 = Parachute(velocity.y);
		velocity.y = 0f;
		float num = 1f - Mathf.Abs(Vector3.Dot(vector, worldUp));
		vector.y *= ((!StatMaster.GodTools.GravityDisabled) ? (num * num) : 0f);
		float num2 = num * GetIntensity();
		float num3 = power * num2;
		float mag;
		Vector3 vector3 = vector * num3 * ClampSpeed(vector, velocity, out mag);
		Rigidbody.AddForceAtPosition(vector3 + vector2, GetForcePoint());
		CalculateVisualParameters(vector3.sqrMagnitude);
	}

	public void EmulateRealWind()
	{
		Vector3 vector = -base.transform.up;
		Vector3 forward = base.transform.forward;
		Vector3 velocity = Rigidbody.velocity;
		Vector3 vector2 = Parachute(velocity.y);
		velocity.y = 0f;
		float num = 1f - Mathf.Abs(Vector3.Dot(vector, worldUp));
		vector.y *= ((!StatMaster.GodTools.GravityDisabled) ? (num * num) : 0f);
		float intensity = GetIntensity();
		Vector3 vector3 = CalculateSailForce(windDir, vector, forward);
		vector3 *= intensity;
		float mag;
		vector3 *= ClampSpeed(vector3.normalized, velocity, out mag);
		Rigidbody.AddForceAtPosition(vector3 + vector2, GetForcePoint());
		CalculateVisualParameters(vector3.sqrMagnitude);
	}

	private Vector3 GetForcePoint()
	{
		Vector3 direction = base.transform.InverseTransformDirection(Vector3.down);
		direction.y = 0f;
		direction = base.transform.TransformDirection(direction);
		return GetCenter() + direction;
	}

	private void OldTorque(Vector3 sailForward, float mag, float intensity)
	{
		float num = counterTorquePower * intensity;
		if (StatMaster.GodTools.GravityDisabled)
		{
			num *= 0.5f;
		}
		mag = Mathf.Clamp01(mag * 0.05f) * 2f;
		Vector3 vector = Vector3.Cross(sailForward, worldUp);
		Rigidbody.AddTorque(vector * num * mag);
	}

	private Vector3 Parachute(float y)
	{
		Vector3 vector = new Vector3(0f, y, 0f) * 0.1f * input;
		if (StatMaster.GodTools.GravityDisabled || Vector3.Dot(vector, Vector3.down) < 0f)
		{
			return Vector3.zero;
		}
		return vector;
	}

	private float ClampSpeed(Vector3 pushDir, Vector3 vel, out float mag)
	{
		vel = ((!(Vector3.Dot(vel, pushDir) > 0f)) ? Vector3.zero : Vector3.Project(vel, pushDir));
		mag = vel.magnitude;
		float num = Mathf.InverseLerp(maxSpeed, 0f, mag);
		float num2 = maxSpeedRamp.Evaluate(num);
		currentSpeed = 1f - num;
		return num2 * limitingBias + (1f - limitingBias);
	}

	private float GetIntensity()
	{
		startLerp += Time.fixedDeltaTime * 0.5f;
		if (startLerp > 1f)
		{
			startLerp = 1f;
		}
		float num = 1f - fireTag.fireControllerCode.fireProgress;
		float num2 = Mathf.Pow(1f - submergedPercent, 2f);
		return (offsetArea + area * input) * num2 * num * startLerp;
	}

	private void CalculateVisualParameters(float forceMag)
	{
		float num = Mathf.Clamp01(forceMag / 10000f);
		lastOffset = targetOffset;
		targetOffset = (currentSpeed + num) * 0.5f;
	}

	private Vector3 CalculateSailForce(Vector3 windDirection, Vector3 sailNormal, Vector3 sailUp)
	{
		float num = Vector3.Dot(windDirection, sailNormal);
		float num2 = 61.25f;
		float num3 = Mathf.Pow(1f, 2f);
		Vector3 lhs = Vector3.Cross(windDirection, sailNormal);
		Vector3 vector = Vector3.Cross(lhs, sailNormal);
		Vector3 vector2 = 3f * vector * num * num;
		if (num > 0.9999f)
		{
			return (vector2 + 2f * windDirection) * num2 * num3;
		}
		float num4 = num;
		float num5 = num4 * num4;
		float num6 = Mathf.Sqrt(Mathf.Max(float.Epsilon, 1f - num5));
		float num7 = Mathf.Pow((num4 + 1f) * 0.5f, 2f);
		float num8 = 2f * num7 * num6;
		Vector3 vector3 = Vector3.zero;
		if (num > float.Epsilon)
		{
			float num9 = 2f * num5;
			vector3 = num9 * windDirection;
		}
		Vector3 vector4 = Vector3.Cross(lhs, windDir);
		float num10 = 2.5f * num8;
		Vector3 vector5 = num10 * vector4;
		return (vector2 + vector3 + vector5) * num2 * num3;
	}

	public override void FreezeMe()
	{
		base.FreezeMe();
		isFrozen = true;
		power = 0f;
	}

	public override void EmulationUpdateBlock()
	{
		if (_parentMachine.isReady)
		{
			if (sailBroken)
			{
				emuRaiseHeld = (emuLowerHeld = false);
				return;
			}
			emuRaiseHeld = raiseSailKey.EmulationHeld(true);
			emuLowerHeld = lowerSailKey.EmulationHeld(true);
			CheckKeys(raiseHeld || emuRaiseHeld, lowerHeld || emuLowerHeld, Time.fixedDeltaTime * 2f);
		}
	}

	private void CheckKeys(bool raise, bool lower, float delta)
	{
		float num = delta * sailSpeed * speedSlider.Value;
		if (holdMode.IsActive)
		{
			if (inverted.IsActive)
			{
				raise = !lower;
			}
			if (raise)
			{
				input += num;
			}
			else
			{
				input -= num;
			}
		}
		else if (raise)
		{
			input += num;
		}
		else if (lower)
		{
			input -= num;
		}
		if (input < 0f || input > 1f)
		{
			input = Mathf.Clamp01(input);
		}
	}

	public override void OnSave(XDataHolder data)
	{
		data.Write("bmt-version", version);
		base.OnSave(data);
		data.Write("sideplaced", sidePlaced);
		data.Write("flipped", Flipped);
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (isSimulating && !SimPhysics)
		{
			return;
		}
		if (!data.HasKey("bmt-version"))
		{
			if (data.WasLoadedFromFile)
			{
				version = 0;
				data.Write("bmt-version", version);
			}
		}
		else
		{
			version = data.ReadInt("bmt-version");
		}
		if (data.HasKey("sideplaced"))
		{
			sidePlaced = data.ReadBool("sideplaced");
			SetSidePlaced(sidePlaced);
		}
		if (!isSimulating && data.HasKey("flipped") && !data.WasCreated && !data.WasLoadedFromFile)
		{
			Flipped = data.ReadBool("flipped");
			PostFlip(false, false);
		}
	}
}
