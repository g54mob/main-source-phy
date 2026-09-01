using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("Blocks/BlockBehaviour")]
public class BlockBehaviour : SaveableDataHolder, ISelectable
{
	public int BuildIndex = -1;

	public BlockVisualController VisualController;

	public ReduceBreakForceOnImpact BreakOnImpact;

	public BlockHealthBar BlockHealth;

	public MyBounds myBounds;

	public FireTag fireTag;

	public IceTag iceTag;

	public Joint blockJoint;

	public GameObject[] DestroyOnClient;

	public GameObject[] DestroyOnSimulate;

	[NonSerialized]
	public bool Flipped;

	[NonSerialized]
	public bool wasNotAllowed;

	[NonSerialized]
	public bool hasSimBlock;

	[NonSerialized]
	public MVisual Visual;

	[NonSerialized]
	public BlockPrefab Prefab;

	[NonSerialized]
	public bool isPrefab;

	[NonSerialized]
	public int respawnCallbackCount;

	[NonSerialized]
	public Action<BlockBehaviour> onRespawn;

	[NonSerialized]
	public bool hasOffset;

	[NonSerialized]
	public Vector3 posOffset = Vector3.zero;

	[NonSerialized]
	public Quaternion rotOffset = Quaternion.identity;

	[NonSerialized]
	public Quaternion rotInvOffset = Quaternion.identity;

	[NonSerialized]
	public List<Joint> jointsToMe = new List<Joint>(0);

	[NonSerialized]
	public List<Joint> iJointTo = new List<Joint>(0);

	[NonSerialized]
	public List<JoinOnTriggerBlock> grabbers;

	[NonSerialized]
	public Dictionary<BlockBehaviour, Collider[]> parentedColliders;

	[NonSerialized]
	public List<Renderer> visAddedToMe;

	[NonSerialized]
	protected Vector3 center;

	[NonSerialized]
	public BlockBehaviour SimBlock;

	[NonSerialized]
	public BlockBehaviour BuildingBlock;

	[NonSerialized]
	public Vector3 Position;

	[NonSerialized]
	public Quaternion Rotation;

	[NonSerialized]
	public Vector3 Scale;

	[NonSerialized]
	public bool isIntact;

	[NonSerialized]
	public Guid Guid;

	[NonSerialized]
	public int NodeIndex = -1;

	[NonSerialized]
	public int _clusterIndex = -1;

	[NonSerialized]
	public bool isParented;

	[NonSerialized]
	public Vector3 originalCOM;

	[NonSerialized]
	public float originalMass;

	[NonSerialized]
	public float originalDrag;

	[NonSerialized]
	public float originalADrag;

	[NonSerialized]
	public bool gotChildBlocks;

	[NonSerialized]
	public float jointBreakForce;

	[NonSerialized]
	public BlockBehaviour parentBlock;

	[NonSerialized]
	public CollisionEnterHook parentCollision;

	protected bool updatingRigidBody;

	protected bool needsRigidUpdate;

	protected KeyInputController inputController;

	private MKey simToggleKey;

	private bool createdVisList;

	protected bool createdSimLists;

	private BlockJoint[] joints = new BlockJoint[0];

	private bool checkJointForce;

	private Vector3 combinedPower;

	private Transform nonScaledChild;

	public bool DebugSpeed;

	[Obsolete("BlockBehaviour::hasMyBounds is obsolete. Use Prefab.hasMyBounds instead.", false)]
	[HideInInspector]
	public bool hasMyBounds;

	private static MKey[] blank = new MKey[0];

	protected int stressFrame;

	protected float currentStress;

	public virtual bool CanBurn
	{
		get
		{
			return Prefab.canBurn;
		}
	}

	public MPTeam Team { get; set; }

	public bool PlacementComplete { get; set; }

	public int BlockID
	{
		get
		{
			return Prefab.ID;
		}
	}

	public virtual bool SetColliderIterations
	{
		get
		{
			return Prefab.SetColliderIterations;
		}
	}

	public virtual bool SetVelocityIterations
	{
		get
		{
			return Prefab.SetVelocityIterations;
		}
	}

	public bool SurfaceType
	{
		get
		{
			return Prefab.Type == BlockType.BuildNode || Prefab.Type == BlockType.BuildEdge || Prefab.Type == BlockType.BuildSurface;
		}
	}

	public virtual bool ShouldInitHealth
	{
		get
		{
			return true;
		}
	}

	public WindController CurrentWindController { get; set; }

	public bool InWind
	{
		get
		{
			return !object.ReferenceEquals(CurrentWindController, null);
		}
	}

	public bool IsArmor
	{
		get
		{
			return Prefab.isArmor;
		}
	}

	public bool IsDestroyed
	{
		get
		{
			return isDestroyed;
		}
		set
		{
			isDestroyed = value;
		}
	}

	public long identifier
	{
		get
		{
			return BuildIndex;
		}
		set
		{
		}
	}

	public bool IsSelected { get; set; }

	public bool IsSelectedExtra { get; set; }

	public int SymmetryIndex { get; set; }

	public float TransformMultiplier { get; set; }

	public Transform ParentingTransform
	{
		get
		{
			if (base.transform.localScale == Vector3.one)
			{
				return base.transform;
			}
			if (nonScaledChild == null)
			{
				nonScaledChild = new GameObject("Attached Visuals").transform;
				nonScaledChild.SetParent(base.transform, false);
				nonScaledChild.localPosition = Vector3.zero;
				nonScaledChild.localRotation = Quaternion.identity;
				float num = Mathf.Max(base.transform.localScale.x, 0.0001f);
				float num2 = Mathf.Max(base.transform.localScale.y, 0.0001f);
				float num3 = Mathf.Max(base.transform.localScale.z, 0.0001f);
				nonScaledChild.localScale = new Vector3(1f / num, 1f / num2, 1f / num3);
			}
			return nonScaledChild;
		}
	}

	public bool RegisteredBuildUpdate { get; set; }

	public bool RegisteredBuildFixedUpdate { get; set; }

	public bool RegisteredBuildLateUpdate { get; set; }

	public bool RegisteredSimUpdate { get; set; }

	public bool RegisteredSimFixedUpdate { get; set; }

	public bool RegisteredSimLateUpdate { get; set; }

	public bool RegisteredSimEmulationUpdate { get; set; }

	public bool RegisteredSimSendEmulationUpdate { get; set; }

	public virtual bool CanGlow
	{
		get
		{
			return Prefab.canBeHeated;
		}
	}

	public int ClusterIndex
	{
		get
		{
			return _clusterIndex;
		}
		set
		{
			ClusterChanged(value);
			_clusterIndex = value;
			if (StatMaster.clusterCoded && Prefab.hasBVC)
			{
				VisualController.SetNormal();
			}
		}
	}

	public Transform GetTransform()
	{
		return base.transform;
	}

	public void SetUpPrefab()
	{
		for (int i = 0; i < DestroyOnSimulate.Length; i++)
		{
			GameObject gameObject = DestroyOnSimulate[i];
			if (gameObject.tag == "Untagged")
			{
				gameObject.tag = "PendingDestructionSim";
			}
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (!isSimulating || !_hasParentMachine)
		{
			return;
		}
		if (!noRigidbody)
		{
			originalDrag = Rigidbody.drag;
			originalADrag = Rigidbody.angularDrag;
		}
		if (BuildToSimBlock())
		{
			if (!createdVisList)
			{
				visAddedToMe = new List<Renderer>();
				createdVisList = true;
			}
			VisualController.RemoveOutline();
			if (BuildIndex == 0)
			{
				simToggleKey = AddEmulatorKey(string.Empty, string.Empty, InputManager.GetSimToggleKey());
			}
			if (StatMaster.aeroCoded || StatMaster.stressCoded)
			{
				VisualController.SetNormal();
			}
			AddToAreodynamicsList();
		}
	}

	protected virtual void AddToAreodynamicsList()
	{
		if (WaterController.Exist)
		{
			WaterController.aerodynamicCount++;
		}
	}

	protected virtual bool BuildToSimBlock()
	{
		if (_parentMachine.GetBlockFromIndex(BuildIndex, out BuildingBlock))
		{
			Prefab = BuildingBlock.Prefab;
			if (_parentMachine.AddSimBlock(this))
			{
				BuildingBlock.SimBlock = this;
				BuildingBlock.hasSimBlock = true;
				Prefab.InitSimComponents(this);
				return true;
			}
		}
		return false;
	}

	public virtual void StartPhysics(bool isKinematic)
	{
		if (!isKinematic)
		{
			SetNonJoining();
		}
		if (!object.ReferenceEquals(BlockHealth, null))
		{
			BlockHealth.StartPhysics();
		}
	}

	public virtual void SetNonJoining()
	{
		if (Prefab.hasMyBounds)
		{
			myBounds.SetNonJoining();
		}
	}

	public virtual void OnPostEdit()
	{
	}

	public override Vector3 GetCenter()
	{
		if (!hasOffset)
		{
			Bounds bounds = VisualController.renderers[0].bounds;
			center = ((!(bounds.size.sqrMagnitude > 0f)) ? Vector3.zero : base.transform.InverseTransformPoint(bounds.center));
			hasOffset = true;
		}
		return base.transform.TransformPoint(center);
	}

	public void InvalidatePrefab()
	{
		Prefab = null;
	}

	public virtual void Select(bool selected)
	{
		if (!isDestroyed && !(base.gameObject == null))
		{
			IsSelected = selected;
			if (!selected)
			{
				IsSelectedExtra = false;
				SymmetryIndex = 0;
			}
		}
	}

	protected IEnumerator UpdateRigidbodies(Transform source)
	{
		updatingRigidBody = true;
		yield return new WaitForEndOfFrame();
		Rigidbody[] rs = source.GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody current in rs)
		{
			Transform currentTransform = current.transform;
			Vector3 pos = currentTransform.position;
			Quaternion rot = currentTransform.rotation;
			RigidbodyInterpolation ri = current.interpolation;
			current.interpolation = RigidbodyInterpolation.None;
			currentTransform.position = pos;
			currentTransform.rotation = rot;
			current.interpolation = ri;
			current.WakeUp();
		}
		updatingRigidBody = false;
	}

	public void OnUpdateSkin()
	{
		if (OptionsMaster.skinsEnabled && Visual != null)
		{
			Visual.SetSkin(VisualController.selectedSkin);
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (needsRigidUpdate)
		{
			StartCoroutine(UpdateRigidbodies(base.transform));
			needsRigidUpdate = false;
		}
	}

	public void SetInputController(KeyInputController input)
	{
		inputController = input;
	}

	protected void EmulateKeys(MKey[] activationKeys, MKey emulateKey, bool emulate)
	{
		if (SimPhysics)
		{
			if (!inputController)
			{
				Debug.LogWarning("inputController on block " + base.name + ":" + BuildIndex + " is null");
			}
			else
			{
				inputController.Emulate(this, activationKeys, emulateKey, emulate);
			}
		}
	}

	internal void EmulateSimToggleKey(bool emulate)
	{
		if (isSimulating && BuildIndex == 0)
		{
			EmulateKeys(blank, simToggleKey, emulate);
		}
	}

	protected bool CheckLoop(MKey[] inputs, MKey emulateKey)
	{
		return inputController.CheckLoop(inputs, emulateKey);
	}

	public virtual void SetPosition(Vector3 pos)
	{
		base.transform.position = pos - base.transform.TransformVector(posOffset);
		if (base.ParentMachine.isLocalMachine)
		{
			Position = base.transform.localPosition;
		}
		UpdateBody();
	}

	public virtual void SetRotation(Quaternion rot)
	{
		base.transform.rotation = rot * rotOffset;
		if (base.ParentMachine.isLocalMachine)
		{
			Rotation = base.transform.localRotation;
		}
		UpdateBody();
	}

	public void SetScale(Vector3 scale)
	{
		base.transform.localScale = (Scale = scale);
		UpdateBody();
	}

	public void UpdateBody()
	{
		if (!updatingRigidBody)
		{
			if (base.gameObject.activeInHierarchy)
			{
				StartCoroutine(UpdateRigidbodies(base.transform));
			}
			else
			{
				needsRigidUpdate = true;
			}
		}
	}

	public virtual LevelBoundingBox.GroundResult Ground(LayerMask layerMask)
	{
		if (noRigidbody)
		{
			return new LevelBoundingBox.GroundResult(false, null, 0f);
		}
		return LevelBoundingBox.Ground(this, layerMask);
	}

	public virtual void OnRemoteEmulate(MKey key, bool emulate)
	{
	}

	public void SetNetworkBlock(NetworkBlock netBlock)
	{
		NetBlock = netBlock;
	}

	public void CreateSimLists()
	{
		if (!createdSimLists)
		{
			if (!createdVisList)
			{
				visAddedToMe = new List<Renderer>();
				createdVisList = true;
			}
			parentedColliders = new Dictionary<BlockBehaviour, Collider[]>();
			grabbers = new List<JoinOnTriggerBlock>();
			iJointTo = new List<Joint>();
			jointsToMe = new List<Joint>();
			createdSimLists = true;
		}
	}

	public virtual void OnAddRemote()
	{
	}

	public virtual void RegisterSimUpdates()
	{
		if (_hasParentMachine)
		{
			RegisterSimUpdates(Prefab.RegisterSimUpdate, SimPhysics && Prefab.RegisterSimFixedUpdate, Prefab.RegisterSimLateUpdate, Prefab.RegisterEmulationUpdate);
		}
	}

	protected void RegisterSimUpdates(bool update, bool fixedUpdate, bool lateUpdate, bool emulationUpdate)
	{
		if (CanGlow)
		{
			_parentMachine.RegisterBVCUpdate(VisualController, false);
		}
		if (update)
		{
			_parentMachine.RegisterUpdate(this, false);
		}
		else if (ShowInVisualiser() && Prefab.hasBVC && !_parentMachine.ContainedInSimUpdate(this))
		{
			_parentMachine.AddToSimUpdate(this);
		}
		if (fixedUpdate)
		{
			_parentMachine.RegisterFixedUpdate(this, false);
		}
		if (lateUpdate)
		{
			_parentMachine.RegisterLateUpdate(this, false);
		}
		if (emulationUpdate)
		{
			_parentMachine.RegisterEmulationUpdate(this);
		}
	}

	public void FinishDestroySimulate()
	{
		if (!SimPhysics)
		{
			for (int i = 0; i < DestroyOnClient.Length; i++)
			{
				UnityEngine.Object.Destroy(DestroyOnClient[i]);
			}
		}
		for (int i = 0; i < DestroyOnSimulate.Length; i++)
		{
			UnityEngine.Object.Destroy(DestroyOnSimulate[i]);
		}
	}

	public void SetColIterations()
	{
		Rigidbody.solverIterations = Prefab.IterationCount;
		if (SetVelocityIterations && OptionsMaster.BesiegeConfig.MorePrecisePhysics)
		{
			Rigidbody.solverVelocityIterations = Prefab.VelocityIterationCount;
		}
	}

	public void SetMaxAngularVelocity()
	{
		Rigidbody.maxAngularVelocity = Prefab.MaxAngularVelocity;
	}

	public virtual void FinishPhysics()
	{
		if (IsDestroyed)
		{
			return;
		}
		if (SimPhysics)
		{
			GetJoints();
			if (_parentMachine.UnbreakableMode)
			{
				SetInvincible();
			}
		}
		else
		{
			if (stripped)
			{
				return;
			}
			Collider[] componentsInChildren = base.gameObject.GetComponentsInChildren<Collider>(true);
			Collider[] array = componentsInChildren;
			foreach (Collider collider in array)
			{
				if (!collider.isTrigger)
				{
					collider.enabled = false;
				}
			}
			NetworkBlock.StripTransform(base.transform);
			noRigidbody = true;
		}
	}

	public virtual void OnReloadAmmo(ref int units, ReloadAmmoType type, bool setAmmo, bool eachBlock, bool playAnim = true)
	{
	}

	public virtual void SetFlip(bool flip)
	{
		Flipped = flip;
	}

	public bool PostFlip(bool sound, bool isUndo)
	{
		bool flag = OnFlip(sound, isUndo);
		if (flag)
		{
			XDataHolder data = new XDataHolder();
			OnSave(data);
			FlipArrow(Flipped);
		}
		return flag;
	}

	public bool PostSpin(bool sound, bool isUndo)
	{
		bool flag = OnSpin(sound, isUndo);
		if (flag)
		{
			XDataHolder data = new XDataHolder();
			OnSave(data);
		}
		return flag;
	}

	protected virtual void FlipArrow(bool flipped)
	{
		VisualController.FlipArrow(flipped);
	}

	public virtual bool OnFlip(bool playSound, bool isUndo)
	{
		return false;
	}

	public virtual bool OnSpin(bool playSound, bool forward)
	{
		return false;
	}

	public virtual void ClusterChanged(float value)
	{
	}

	public void SetClusterIndexNoUpdate(int index)
	{
		_clusterIndex = index;
	}

	protected override XDataHolder GetInitialHolder()
	{
		XDataHolder xDataHolder = InitialState.Clone();
		xDataHolder.EraseCustomBlockData();
		return xDataHolder;
	}

	public void BloodSplatter()
	{
		if (!isSimulating || !Prefab.bleedOnTexture || !Prefab.hasBVC)
		{
			return;
		}
		VisualController.SetBloodyLevel(1f, StatMaster.BloodColor);
		if (StatMaster.isHosting && SimPhysics)
		{
			if (NetBlock != null)
			{
				NetBlock.Event(NetworkEntity.EntityEvent.SetBloodyLevel);
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
	}

	public bool IsChildBlockCollider(Collider col)
	{
		if (gotChildBlocks)
		{
			CreateSimLists();
			foreach (Collider[] value in parentedColliders.Values)
			{
				if (value.Contains(col))
				{
					return true;
				}
			}
		}
		return false;
	}

	public BlockBehaviour GetChildBlockFromCollider(Collider col)
	{
		if (gotChildBlocks)
		{
			CreateSimLists();
			foreach (BlockBehaviour key in parentedColliders.Keys)
			{
				Collider[] source = parentedColliders[key];
				if (source.Contains(col))
				{
					return key;
				}
			}
		}
		return null;
	}

	public void UnParentChildBlock(BlockBehaviour childBlock)
	{
		if (isParented)
		{
			parentBlock.UnParentChildBlock(childBlock);
			return;
		}
		if (childBlock == null)
		{
			Debug.LogWarning("UnParentChildBlock: Block is null");
			return;
		}
		GameObject gameObject = childBlock.gameObject;
		if (gameObject == null)
		{
			Debug.LogWarning("UnParentChildBlock: ChildGO is null");
			return;
		}
		CreateSimLists();
		parentedColliders.Remove(childBlock);
		Rigidbody rigidbody = (childBlock.Rigidbody = gameObject.AddComponent<Rigidbody>());
		childBlock.noRigidbody = false;
		if (!noRigidbody && Rigidbody.interpolation == RigidbodyInterpolation.Interpolate)
		{
			childBlock.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		}
		childBlock.WaterDrag = 0f;
		childBlock.WaterAngularDrag = 0f;
		rigidbody.mass = childBlock.originalMass;
		rigidbody.drag = childBlock.Prefab.blockBehaviour.Rigidbody.drag;
		rigidbody.angularDrag = childBlock.Prefab.blockBehaviour.Rigidbody.angularDrag;
		if (StatMaster.GodTools.GravityDisabled)
		{
			rigidbody.useGravity = false;
		}
		else
		{
			rigidbody.useGravity = childBlock.Prefab.blockBehaviour.Rigidbody.useGravity;
		}
		childBlock.CreateSimLists();
		if (parentedColliders.Count == 0)
		{
			gotChildBlocks = false;
		}
		Transform transform = childBlock.transform;
		Transform parent = transform.parent;
		transform.SetParent(_parentMachine.SimulationMachine, true);
		UnityEngine.Object.Destroy(parent.gameObject);
		childBlock.isParented = false;
		rigidbody.ResetCenterOfMass();
		if (gotChildBlocks && !noRigidbody)
		{
			float num = rigidbody.mass / (Rigidbody.mass + rigidbody.mass);
			Rigidbody.centerOfMass -= (Rigidbody.transform.InverseTransformPoint(rigidbody.worldCenterOfMass) - Rigidbody.centerOfMass) * num;
			Rigidbody.mass -= rigidbody.mass;
		}
		else
		{
			if (!noRigidbody)
			{
				Rigidbody.mass -= rigidbody.mass;
				Rigidbody.ResetCenterOfMass();
			}
			UnityEngine.Object.Destroy(childBlock.parentCollision);
		}
		WaterDrag = 0f;
		base.WaterAngularDrag = 0f;
		if (!noRigidbody)
		{
			Rigidbody.drag = Prefab.blockBehaviour.Rigidbody.drag;
			Rigidbody.angularDrag = Prefab.blockBehaviour.Rigidbody.angularDrag;
		}
	}

	public void UnParentChildBlockWForce(BlockBehaviour childBlock, Vector3 force)
	{
		if (isParented)
		{
			parentBlock.UnParentChildBlockWForce(childBlock, force);
			return;
		}
		UnParentChildBlock(childBlock);
		childBlock.Rigidbody.AddForce(force);
	}

	public virtual void UpdateBlock()
	{
		if (isSimulating)
		{
			if (StatMaster.aeroCoded)
			{
				VisualController.UpdateAeroDragDisplay();
			}
			else if (StatMaster.stressCoded)
			{
				VisualController.UpdateStressDisplay();
			}
		}
	}

	public virtual void FixedUpdateBlock()
	{
	}

	public virtual void LateUpdateBlock()
	{
	}

	public virtual void EmulationUpdateBlock()
	{
	}

	public virtual void SendEmulationUpdateBlock()
	{
	}

	public void RemoveSimBlock(bool destroy)
	{
		if (isSimulating && base.HasParentMachine)
		{
			Machine parentMachine = base.ParentMachine;
			if (RegisteredSimFixedUpdate)
			{
				parentMachine.UnregisterFixedUpdate(this, false);
			}
			if (RegisteredSimLateUpdate)
			{
				parentMachine.UnregisterLateUpdate(this, false);
			}
			if (RegisteredSimUpdate)
			{
				parentMachine.UnregisterUpdate(this, false);
			}
			else if (ShowInVisualiser() && Prefab.hasBVC && parentMachine.ContainedInSimUpdate(this))
			{
				parentMachine.RemoveFromSimUpdate(this);
			}
			if (RegisteredSimEmulationUpdate)
			{
				parentMachine.UnregisterEmulationUpdate(this);
			}
			if (CanGlow)
			{
				parentMachine.UnregisterBVCUpdate(VisualController, false);
			}
			parentMachine.RemoveSimBlock(this, destroy);
		}
	}

	public void VirtualJointBreakCollision()
	{
		if (isParented && jointBreakForce <= 0f)
		{
			parentBlock.UnParentChildBlock(this);
		}
	}

	public IEnumerator VirtualJointBreakExplosion(Vector3 power)
	{
		combinedPower += power;
		if (!checkJointForce)
		{
			checkJointForce = true;
			yield return new WaitForFixedUpdate();
			if (!isDestroyed && !noRigidbody && combinedPower.sqrMagnitude > jointBreakForce)
			{
				Vector3 currentVelocity = Rigidbody.GetPointVelocity(base.transform.TransformPoint(originalCOM));
				UnParentChildBlockWForce(this, combinedPower.magnitude * currentVelocity);
				Rigidbody.AddRelativeTorque(UnityEngine.Random.onUnitSphere * 10f);
			}
			checkJointForce = false;
		}
	}

	public virtual int CheckJoints()
	{
		Joint[] array = ((!(this is SpringCode)) ? base.gameObject.GetComponents<Joint>() : base.gameObject.GetComponentsInChildren<Joint>());
		int num = array.Length;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].connectedBody == null)
			{
				UnityEngine.Object.Destroy(array[i]);
				num--;
			}
		}
		return num;
	}

	public override void OnSave(XDataHolder data)
	{
		base.OnSave(data);
		SaveMapperValues(data);
	}

	public override void OnSave(XDataHolder data, CopyMode mode)
	{
		base.OnSave(data, mode);
		OnSave(data);
	}

	public override void OnLoad(XDataHolder data, CopyMode mode)
	{
		base.OnLoad(data, mode);
		OnLoad(data);
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		LoadMapperValues(data);
	}

	private void GetJoints()
	{
		Joint[] components = base.gameObject.GetComponents<Joint>();
		joints = new BlockJoint[components.Length];
		bool flag = true;
		Rigidbody rigidbody = Rigidbody;
		flag = !(rigidbody == null) && !rigidbody.isKinematic && IgnoredSwapBlock(Prefab.Type);
		int num = 0;
		for (int i = 0; i < joints.Length; i++)
		{
			if ((bool)components[i].connectedBody)
			{
				joints[i] = new BlockJoint(this, components[i]);
				joints[i].Swap(flag, rigidbody.mass);
				num++;
			}
		}
		if (num != joints.Length)
		{
			BlockJoint[] destinationArray = new BlockJoint[num];
			Array.Copy(joints, destinationArray, num);
			joints = destinationArray;
		}
	}

	public int GetJointLikyBroken(out BlockJoint j)
	{
		j = null;
		if (joints.Length == 1)
		{
			return 0;
		}
		float num = float.MaxValue;
		int result = -1;
		for (int i = 0; i < joints.Length; i++)
		{
			BlockJoint blockJoint = joints[i];
			if (blockJoint != null && blockJoint.breakForce < num)
			{
				num = blockJoint.breakForce;
				j = blockJoint;
				result = i;
			}
		}
		return result;
	}

	public void SetJointsInvicible(bool invincible)
	{
		if (invincible)
		{
			SetInvincible();
		}
		else
		{
			SetNormal();
		}
	}

	public static bool IgnoredSwapBlock(BlockType type)
	{
		switch (type)
		{
		case BlockType.SteeringBlock:
		case BlockType.Piston:
		case BlockType.Grabber:
		case BlockType.SteeringHinge:
		case BlockType.Balloon:
		case BlockType.Pin:
		case BlockType.MetalJaw:
		case BlockType.Rudder:
			return false;
		default:
			return true;
		}
	}

	private void SetInvincible()
	{
		for (int i = 0; i < joints.Length; i++)
		{
			joints[i].SetInvincible();
		}
	}

	private void SetNormal()
	{
		for (int i = 0; i < joints.Length; i++)
		{
			joints[i].ResetBreak();
		}
	}

	public virtual void FreezeMe()
	{
		if (Prefab.hasMyBounds && !stripped)
		{
			myBounds.SetNonJoining();
		}
	}

	public override void SetGrabbed(bool grabbed, MonoBehaviour j)
	{
		CreateSimLists();
		if (!(j is JoinOnTriggerBlock))
		{
			return;
		}
		JoinOnTriggerBlock item = j as JoinOnTriggerBlock;
		if (grabbed)
		{
			if (!grabbers.Contains(item))
			{
				grabbers.Add(item);
			}
			return;
		}
		CreateSimLists();
		if (grabbers.Contains(item))
		{
			grabbers.Remove(item);
		}
	}

	public bool ShowInVisualiser()
	{
		if (StatMaster.stressCoded)
		{
			if (!IgnoredForStress(Prefab.Type))
			{
				return true;
			}
		}
		else if (StatMaster.aeroCoded && !IgnoredForDrag(Prefab.Type))
		{
			return true;
		}
		return false;
	}

	public static bool IgnoredForStress(BlockType type)
	{
		switch (type)
		{
		case BlockType.Spring:
		case BlockType.Bomb:
		case BlockType.FlameBall:
		case BlockType.Boulder:
		case BlockType.Balloon:
		case BlockType.RopeWinch:
		case BlockType.Grenade:
		case BlockType.Pin:
		case BlockType.CameraBlock:
		case BlockType.Rocket:
		case BlockType.BuildNode:
		case BlockType.BuildEdge:
		case BlockType.RopeMeasure:
			return true;
		default:
			return false;
		}
	}

	public static bool IgnoredForDrag(BlockType type)
	{
		switch (type)
		{
		case BlockType.Spring:
		case BlockType.RopeWinch:
		case BlockType.Pin:
		case BlockType.CameraBlock:
		case BlockType.BuildNode:
		case BlockType.BuildEdge:
		case BlockType.RopeMeasure:
			return true;
		default:
			return false;
		}
	}

	public virtual void SetStress(float s, Vector3 pos)
	{
		if (isSimulating)
		{
			currentStress = Mathf.Max(currentStress, s);
			_parentMachine.maxStress = Mathf.Max(_parentMachine.maxStress, s);
			stressFrame = Time.frameCount;
		}
	}

	public float GetStress()
	{
		return currentStress;
	}

	public void UpdateStress()
	{
		if (IgnoredForStress(Prefab.Type))
		{
			return;
		}
		for (int i = 0; i < joints.Length; i++)
		{
			if (joints[i].joint != null)
			{
				float s = EstimateJointStress(joints[i], base.transform, joints[i].connectedBlock.transform);
				joints[i].connectedBlock.SetStress(s, joints[i].connectedAnchor);
				SetStress(s, joints[i].anchor);
			}
		}
		FadeStress();
	}

	protected virtual void FadeStress()
	{
		currentStress = Mathf.Lerp(currentStress, 0f, Time.deltaTime * 2f);
		if (Time.frameCount > stressFrame + 5 && stressFrame > 0 && currentStress < 0.01f)
		{
			stressFrame = int.MaxValue;
			currentStress = 0f;
		}
	}

	public void CalculateJointStress(ref float max)
	{
		for (int i = 0; i < joints.Length; i++)
		{
			if (joints[i].joint != null)
			{
				max = Mathf.Max(max, EstimateJointStress(joints[i], base.transform, joints[i].connectedBlock.transform));
			}
		}
	}

	public static float EstimateJointStress(BlockJoint blockJoint, Transform bodyA, Transform bodyB)
	{
		Transform bodyA2 = bodyA.transform;
		Transform bodyB2 = bodyB.transform;
		float num = ((!blockJoint.canMove) ? (GetPositionStress(blockJoint, bodyA2, bodyB2) * 20000f / blockJoint.breakForce) : 0f);
		float f = ((!blockJoint.ballJoint) ? (GetAngularStress(blockJoint, bodyA2, bodyB2) * 400000f / blockJoint.breakTorque) : 0f);
		return num + Mathf.Sqrt(f);
	}

	private static float GetPositionStress(BlockJoint joint, Transform bodyA, Transform bodyB)
	{
		return (bodyA.TransformPoint(joint.anchor) - bodyB.TransformPoint(joint.connectedAnchor)).sqrMagnitude;
	}

	private static float GetAngularStress(BlockJoint joint, Transform bodyA, Transform bodyB)
	{
		float num = 0f;
		Vector3 lhs = bodyA.TransformDirection(joint.axis);
		Vector3 rhs = bodyB.TransformDirection(joint.connectedAxis);
		float value = Vector3.Dot(lhs, rhs);
		float num2 = 1f - Mathf.Clamp01(value);
		if (joint.type == BlockJoint.Type.Config && joint.freeAxis != BlockJoint.Axis.X)
		{
			if (joint.freeAxis == BlockJoint.Axis.Y)
			{
				num2 = 0f;
			}
			else if (joint.freeAxis != BlockJoint.Axis.Z)
			{
			}
			Vector3 lhs2 = bodyA.TransformDirection(joint.secondaryAxis);
			Vector3 rhs2 = bodyB.TransformDirection(joint.connectedSecondaryAxis);
			float value2 = Vector3.Dot(lhs2, rhs2);
			num = 1f - Mathf.Clamp01(value2);
		}
		return num2 + num;
	}
}
