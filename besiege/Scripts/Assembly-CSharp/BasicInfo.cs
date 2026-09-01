using System;
using System.Collections;
using InternalModding.Blocks;
using UnityEngine;
using UnityEngine.Serialization;

public class BasicInfo : MonoBehaviour
{
	public enum BasicInfoType
	{
		None = 0,
		Block = 1,
		Projectile = 2,
		Entity = 3
	}

	public enum BoundsType
	{
		Mesh = 0,
		Collider = 1,
		Colliders = 2,
		Custom = 3
	}

	public enum DirectionToSplit
	{
		x = 0,
		y = 1,
		z = 2
	}

	[HideInInspector]
	public int InstanceID;

	public BasicInfoType infoType;

	public Rigidbody Rigidbody;

	public MeshRenderer MeshRenderer;

	public BoundsType boundsType;

	public Bounds customBounds;

	[HideInInspector]
	public bool localCustom;

	public bool noRigidbody;

	public bool stripped;

	private bool checkedRenderer;

	[NonSerialized]
	public bool hasAiScript;

	[NonSerialized]
	public EntityAI aiEntity;

	protected bool _hasRenderer = true;

	[NonSerialized]
	public bool isBuildBlock;

	[HideInInspector]
	public NetworkBlock NetBlock;

	[HideInInspector]
	public bool _hasParentMachine;

	[HideInInspector]
	public Machine _parentMachine;

	[Tooltip("If TRUE it will be ignored by wind calculations")]
	public bool IgnoredByWind;

	[Tooltip("If TRUE it will be ignored by water calculations")]
	public bool IgnoredByWater;

	[Tooltip("Automatically Calculated (mass/volume) unless assigned a non-zero value")]
	public float density;

	[NonSerialized]
	[HideInInspector]
	public bool SimPhysics;

	[NonSerialized]
	[HideInInspector]
	public bool isSimulating;

	[NonSerialized]
	[HideInInspector]
	public bool IsMagnetic;

	[NonSerialized]
	[HideInInspector]
	public bool isDestroyed;

	[NonSerialized]
	[HideInInspector]
	public bool isDisabled;

	[NonSerialized]
	[HideInInspector]
	public float ShelterAmount;

	[NonSerialized]
	[HideInInspector]
	public bool BeingVacuumed;

	[NonSerialized]
	[HideInInspector]
	public bool inWind;

	[HideInInspector]
	public bool AddNoWaterForce;

	[NonSerialized]
	[HideInInspector]
	public bool isZeroG;

	[HideInInspector]
	public float zeroGZoneDistance = float.MaxValue;

	[HideInInspector]
	public ZeroGravityZone zeroGZone;

	[NonSerialized]
	[HideInInspector]
	public bool InitializedState;

	[NonSerialized]
	[HideInInspector]
	public bool isKinematic;

	[NonSerialized]
	[HideInInspector]
	public Vector3 offsetDir;

	[NonSerialized]
	[HideInInspector]
	public float originalDensity;

	public float originalMassDensity;

	[NonSerialized]
	[HideInInspector]
	public Vector3 vGravity;

	[NonSerialized]
	[HideInInspector]
	public bool uniformlyScaled;

	[NonSerialized]
	[HideInInspector]
	public float extentLength;

	[NonSerialized]
	[HideInInspector]
	public float surfaceAreaToVel = 1f;

	public Action CallBackOnDisable;

	private bool added;

	[NonSerialized]
	internal bool objectIsPrefab;

	protected bool gotBoundsThisFrame;

	protected Bounds _updatedBounds;

	private bool gotUpdatedCenter;

	private Vector3 _updatedCenter;

	private bool gotUpdatedRotation;

	private Quaternion _updatedRotation = Quaternion.identity;

	private bool gotMin;

	private float _lowestPoint;

	[HideInInspector]
	public bool hasMultipleBounds;

	[HideInInspector]
	public bool gotBoundsArray;

	[HideInInspector]
	public Bounds[] _defaultBoundsArray;

	[HideInInspector]
	public Quaternion[] _defaultBoundsRotation;

	private bool gotExtents;

	[HideInInspector]
	public Vector3 defaultExtents;

	private Vector3 _rotatedDefaultExtents;

	private Vector3 one;

	private Vector3 two;

	private Vector3 three;

	private Quaternion rbRot;

	protected float _MaxAreaSize;

	private float _MaxArea;

	[NonSerialized]
	public bool gotBounds;

	protected Bounds _defaultBounds;

	protected float hoverPct;

	[NonSerialized]
	[HideInInspector]
	public float lastHoverPct;

	[NonSerialized]
	[HideInInspector]
	public bool hasBeenHovered;

	protected bool vacuumCoroutineRunning;

	protected IEnumerator vacuumCoroutine;

	[HideInInspector]
	public bool grabbed;

	public float waterDragMulti;

	[FormerlySerializedAs("dragInWater")]
	public bool calcDragInWater = true;

	[FormerlySerializedAs("angularDragInWater")]
	public bool calcAngularDragInWater = true;

	[HideInInspector]
	public float dragScale = 1f;

	[HideInInspector]
	public float submergedPercent;

	public bool splitBody;

	public DirectionToSplit directionToSplit = DirectionToSplit.z;

	[HideInInspector]
	public float waterDepth;

	[HideInInspector]
	public float _waterDrag;

	[HideInInspector]
	public float _waterAngularDrag;

	public bool _inWater;

	[HideInInspector]
	public bool HasParentMachine
	{
		get
		{
			return _hasParentMachine;
		}
	}

	public bool IsMPClientNotLocalSim
	{
		get
		{
			return StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim;
		}
	}

	public bool HasRenderer
	{
		get
		{
			if (checkedRenderer)
			{
				return _hasRenderer;
			}
			checkedRenderer = true;
			return _hasRenderer = MeshRenderer != null;
		}
	}

	public Machine ParentMachine
	{
		get
		{
			return _parentMachine;
		}
	}

	public Bounds UpdatedBounds
	{
		get
		{
			RetrieveBounds();
			return _updatedBounds;
		}
	}

	public Vector3 WorldRBCenter
	{
		get
		{
			if (!gotUpdatedCenter)
			{
				_updatedCenter = Rigidbody.worldCenterOfMass;
				gotUpdatedCenter = true;
			}
			return _updatedCenter;
		}
	}

	public Quaternion RBRot
	{
		get
		{
			if (!gotUpdatedRotation)
			{
				_updatedRotation = Rigidbody.rotation;
				gotUpdatedRotation = true;
			}
			return _updatedRotation;
		}
	}

	public Vector3 CenterOfBounds
	{
		get
		{
			return UpdatedBounds.center;
		}
	}

	public float LowestPoint
	{
		get
		{
			if (!gotMin)
			{
				if (!gotBounds)
				{
					CalculateBounds();
				}
				rbRot = RBRot;
				one = rbRot * new Vector3(defaultExtents.x, 0f, 0f);
				two = rbRot * new Vector3(0f, defaultExtents.y, 0f);
				three = rbRot * new Vector3(0f, 0f, defaultExtents.z);
				float num = ((!(one.y < 0f)) ? one.y : (0f - one.y)) + ((!(two.y < 0f)) ? two.y : (0f - two.y)) + ((!(three.y < 0f)) ? three.y : (0f - three.y));
				_lowestPoint = WorldRBCenter.y - num;
				gotMin = true;
			}
			return _lowestPoint;
		}
	}

	public float ClientLowestPoint
	{
		get
		{
			if (!gotMin)
			{
				if (!gotBounds)
				{
					CalculateBounds();
				}
				rbRot = base.transform.rotation;
				one = rbRot * new Vector3(defaultExtents.x, 0f, 0f);
				two = rbRot * new Vector3(0f, defaultExtents.y, 0f);
				three = rbRot * new Vector3(0f, 0f, defaultExtents.z);
				float num = ((!(one.y < 0f)) ? one.y : (0f - one.y)) + ((!(two.y < 0f)) ? two.y : (0f - two.y)) + ((!(three.y < 0f)) ? three.y : (0f - three.y));
				_lowestPoint = GetCenter().y - num;
				gotMin = true;
			}
			return _lowestPoint;
		}
	}

	public Bounds[] DefaultBoundsArray
	{
		get
		{
			if (gotBoundsArray)
			{
				return _defaultBoundsArray;
			}
			CalculateBounds();
			return _defaultBoundsArray;
		}
		set
		{
			_defaultBoundsArray = value;
		}
	}

	public Vector3 DefaultBoundsRotated
	{
		get
		{
			if (!gotExtents)
			{
				rbRot = RBRot;
				if (!gotBounds)
				{
					CalculateBounds();
				}
				one = rbRot * new Vector3(defaultExtents.x, 0f, 0f);
				two = rbRot * new Vector3(0f, defaultExtents.y, 0f);
				three = rbRot * new Vector3(0f, 0f, defaultExtents.z);
				_rotatedDefaultExtents.x = ((!(one.x < 0f)) ? one.x : (0f - one.x)) + ((!(two.x < 0f)) ? two.x : (0f - two.x)) + ((!(three.x < 0f)) ? three.x : (0f - three.x));
				_rotatedDefaultExtents.y = ((!(one.y < 0f)) ? one.y : (0f - one.y)) + ((!(two.y < 0f)) ? two.y : (0f - two.y)) + ((!(three.y < 0f)) ? three.y : (0f - three.y));
				_rotatedDefaultExtents.z = ((!(one.z < 0f)) ? one.z : (0f - one.z)) + ((!(two.z < 0f)) ? two.z : (0f - two.z)) + ((!(three.z < 0f)) ? three.z : (0f - three.z));
				gotExtents = true;
			}
			return _rotatedDefaultExtents;
		}
	}

	public float MaxAreaSize
	{
		get
		{
			if (_MaxAreaSize != 0f)
			{
				return _MaxAreaSize;
			}
			CalculateBounds();
			return _MaxAreaSize;
		}
	}

	public float MaxArea
	{
		get
		{
			if (_MaxArea != 0f)
			{
				return _MaxArea;
			}
			CalculateBounds();
			return _MaxArea;
		}
		set
		{
			_MaxArea = value;
		}
	}

	public Bounds DefaultBounds
	{
		get
		{
			if (gotBounds)
			{
				return _defaultBounds;
			}
			CalculateBounds();
			return _defaultBounds;
		}
		set
		{
			_defaultBounds = value;
		}
	}

	public float GetSubmergedPctMV
	{
		get
		{
			return ((!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim) && !IgnoredByWater) ? submergedPercent : Mathf.Clamp01(WaterController.waterTransformHeight - GetCenter().y);
		}
	}

	public virtual float WaterDrag
	{
		get
		{
			return _waterDrag;
		}
		set
		{
			if (!noRigidbody && (infoType != BasicInfoType.Block || !(this as BlockBehaviour).isParented) && (calcDragInWater || value == 0f))
			{
				if (waterDragMulti != 0f && waterDragMulti != 1f)
				{
					value *= waterDragMulti;
				}
				Rigidbody.drag += value - _waterDrag;
				_waterDrag = value;
			}
		}
	}

	public float WaterAngularDrag
	{
		get
		{
			return _waterAngularDrag;
		}
		set
		{
			if (!noRigidbody && (infoType != BasicInfoType.Block || !(this as BlockBehaviour).isParented) && (calcAngularDragInWater || value == 0f))
			{
				if (waterDragMulti != 0f && waterDragMulti != 1f)
				{
					value *= waterDragMulti;
				}
				Rigidbody.angularDrag += value - _waterAngularDrag;
				_waterAngularDrag = value;
			}
		}
	}

	public bool CalcDragInWater
	{
		get
		{
			return calcDragInWater;
		}
		set
		{
			if (calcDragInWater != value)
			{
				calcDragInWater = value;
				WaterDrag = 0f;
			}
		}
	}

	public bool CalcAngularDragInWater
	{
		get
		{
			return calcAngularDragInWater;
		}
		set
		{
			if (calcAngularDragInWater != value)
			{
				calcAngularDragInWater = value;
				WaterAngularDrag = 0f;
			}
		}
	}

	public bool InWater
	{
		get
		{
			return _inWater;
		}
		set
		{
			if (value != _inWater)
			{
				_inWater = value;
				if (!value)
				{
					WaterDrag = 0f;
					WaterAngularDrag = 0f;
				}
			}
		}
	}

	public virtual Vector3 GetCenter()
	{
		if (hasAiScript)
		{
			return CenterOfBounds;
		}
		if (HasRenderer)
		{
			return MeshRenderer.bounds.center;
		}
		return GetTarget();
	}

	public virtual Vector3 GetTarget()
	{
		return base.transform.position;
	}

	public void SetParentMachine(Machine m)
	{
		_parentMachine = m;
		_hasParentMachine = true;
	}

	public void ResetParentMachine()
	{
		_hasParentMachine = false;
	}

	protected virtual void RetrieveBounds()
	{
		if (!gotBoundsThisFrame)
		{
			_updatedBounds = MeshRenderer.bounds;
			gotBoundsThisFrame = true;
		}
	}

	protected virtual void RetrieveDefaultBounds()
	{
		Transform transform = ((!(Rigidbody != null)) ? base.transform : Rigidbody.transform);
		if (infoType == BasicInfoType.Block && (this as BlockBehaviour).isParented)
		{
			transform = base.transform;
		}
		bool isInactive = !transform.gameObject.activeSelf;
		switch (boundsType)
		{
		case BoundsType.Mesh:
			if ((bool)MeshRenderer)
			{
				_defaultBounds = MeshRenderer.bounds;
				customBounds = new Bounds(transform.InverseTransformPoint(_defaultBounds.center), _defaultBounds.size);
				localCustom = true;
			}
			else
			{
				boundsType = BoundsType.Colliders;
				RetrieveDefaultBounds();
			}
			break;
		case BoundsType.Collider:
		{
			GameObject gameObject = base.gameObject;
			if (!noRigidbody && (bool)Rigidbody)
			{
				gameObject = Rigidbody.gameObject;
			}
			Collider componentInChildren = gameObject.GetComponentInChildren<Collider>();
			if ((bool)componentInChildren)
			{
				_defaultBounds = GetColliderBounds(componentInChildren, isInactive);
				customBounds = new Bounds(transform.InverseTransformPoint(_defaultBounds.center), _defaultBounds.size);
				localCustom = true;
			}
			else
			{
				boundsType = BoundsType.Colliders;
				RetrieveDefaultBounds();
			}
			break;
		}
		case BoundsType.Colliders:
			if (!noRigidbody && (bool)Rigidbody)
			{
				Collider[] componentsInChildren = Rigidbody.GetComponentsInChildren<Collider>();
				int num = 0;
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					int layer = componentsInChildren[i].gameObject.layer;
					if (layer != 8 && layer != 22)
					{
						if (num == 0)
						{
							_defaultBounds = GetColliderBounds(componentsInChildren[i], isInactive);
						}
						else
						{
							_defaultBounds.Encapsulate(GetColliderBounds(componentsInChildren[i], isInactive));
						}
						num++;
					}
				}
			}
			customBounds = new Bounds(transform.InverseTransformPoint(_defaultBounds.center), _defaultBounds.size);
			localCustom = true;
			break;
		case BoundsType.Custom:
			if (localCustom)
			{
				_defaultBounds = new Bounds(transform.TransformPoint(customBounds.center), customBounds.size);
				break;
			}
			_defaultBounds = customBounds;
			customBounds = new Bounds(transform.InverseTransformPoint(customBounds.center), customBounds.size);
			localCustom = true;
			break;
		default:
			_defaultBounds = new Bounds(new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f));
			customBounds = new Bounds(transform.InverseTransformPoint(_defaultBounds.center), _defaultBounds.size);
			_MaxAreaSize = 1f;
			localCustom = true;
			break;
		}
	}

	private Bounds GetColliderBounds(Collider col, bool isInactive)
	{
		if (isInactive)
		{
			if (col is BoxCollider)
			{
				return ReferenceMaster.GetBoxBounds(col as BoxCollider);
			}
			if (col is SphereCollider)
			{
				return ReferenceMaster.GetSphereBounds(col as SphereCollider);
			}
			if (col is CapsuleCollider)
			{
				return ReferenceMaster.GetCapsuleBounds(col as CapsuleCollider);
			}
			return ReferenceMaster.GetMeshColliderBounds(col as MeshCollider);
		}
		return col.bounds;
	}

	protected void CalculateBounds()
	{
		if (gotBounds)
		{
			return;
		}
		if (isDestroyed || base.transform == null)
		{
			Debug.LogError("CalculateBounds called on " + base.transform.name + " with " + isDestroyed + " or " + (base.transform == null));
			_defaultBounds = new Bounds(new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f));
			_MaxAreaSize = 1f;
			return;
		}
		Transform transform = ((!(Rigidbody != null)) ? base.transform : Rigidbody.transform);
		if (infoType == BasicInfoType.Block && (this as BlockBehaviour).isParented)
		{
			transform = base.transform;
		}
		Vector3 position = transform.position;
		Quaternion rotation = transform.rotation;
		if (!(this is BuildSurface))
		{
			transform.position = Vector3.zero;
			transform.rotation = Quaternion.identity;
		}
		RetrieveDefaultBounds();
		if (Rigidbody != null)
		{
			offsetDir = _defaultBounds.center - Rigidbody.worldCenterOfMass;
		}
		transform.position = position;
		transform.rotation = rotation;
		if (isSimulating || objectIsPrefab)
		{
			gotBounds = true;
		}
		defaultExtents = _defaultBounds.extents;
		Vector3 extents = _defaultBounds.extents;
		_MaxArea = extents.x * extents.y * extents.z;
		if (extents.x > extents.y)
		{
			if (extents.z > extents.y)
			{
				_MaxAreaSize = extents.x * extents.z;
			}
			else
			{
				_MaxAreaSize = extents.x * extents.y;
			}
		}
		else if (extents.z > extents.x)
		{
			_MaxAreaSize = extents.y * extents.z;
		}
		else
		{
			_MaxAreaSize = extents.y * extents.x;
		}
		_MaxArea = Mathf.Max(float.Epsilon, _MaxArea);
		_MaxAreaSize = Mathf.Max(float.Epsilon, _MaxAreaSize);
		float num = extents.x * 0.1f;
		uniformlyScaled = Mathf.Abs(extents.x - extents.y) < num && Mathf.Abs(extents.y - extents.z) < num;
		extentLength = extents.magnitude;
		if (_MaxArea == 0f && SimPhysics)
		{
			_defaultBounds = new Bounds(new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f));
			_MaxAreaSize = 1f;
			IgnoredByWater = true;
		}
		CalculateDensity();
	}

	public virtual void Hover(float pct)
	{
		if (SimPhysics)
		{
			hoverPct += pct;
			if (hoverPct > Rigidbody.mass)
			{
				hoverPct = Rigidbody.mass;
			}
		}
	}

	protected virtual void LateUpdate()
	{
		gotBoundsThisFrame = false;
		gotExtents = false;
		gotMin = false;
		gotUpdatedCenter = false;
		gotUpdatedRotation = false;
		if (SimPhysics && BeingVacuumed)
		{
			if (!vacuumCoroutineRunning)
			{
				vacuumCoroutineRunning = true;
				vacuumCoroutine = IEProcessVacuuming();
				StartCoroutine(vacuumCoroutine);
			}
		}
		else if (vacuumCoroutineRunning)
		{
			vacuumCoroutineRunning = false;
			if (vacuumCoroutine != null)
			{
				StopCoroutine(vacuumCoroutine);
			}
		}
	}

	protected virtual IEnumerator IEProcessVacuuming()
	{
		while (StatMaster.levelSimulating)
		{
			if (hoverPct > 0f)
			{
				if (Rigidbody.useGravity)
				{
					Rigidbody.AddForce(-Physics.gravity * hoverPct);
				}
				hoverPct = 0f;
			}
			yield return new WaitForFixedUpdate();
		}
		vacuumCoroutineRunning = false;
	}

	protected virtual void Awake()
	{
		InstanceID = GetInstanceID();
		if (!noRigidbody && Rigidbody == null)
		{
			if (StatMaster.isMP && !stripped)
			{
				Debug.LogWarning(Machine.GetObjectPath(base.gameObject) + ": BasicInfo is not properly set up!", base.gameObject);
			}
			Rigidbody = GetComponent<Rigidbody>();
			if (Rigidbody == null)
			{
				Rigidbody = GetComponentInChildren<Rigidbody>();
			}
			noRigidbody = Rigidbody != null;
			GetMeshRendererReference();
		}
		if (originalMassDensity == 0f && !noRigidbody && _hasParentMachine && isBuildBlock)
		{
			originalMassDensity = Rigidbody.mass;
		}
		if (_hasParentMachine && !isBuildBlock)
		{
			UpdateSimState();
			if (!noRigidbody && Rigidbody.mass < 0.1f)
			{
				IgnoredByWater = true;
			}
		}
	}

	protected virtual void GetMeshRendererReference()
	{
		if (MeshRenderer == null)
		{
			MeshRenderer = GetComponent<MeshRenderer>();
			if (MeshRenderer == null)
			{
				MeshRenderer = GetComponentInChildren<MeshRenderer>();
			}
		}
	}

	protected virtual void Start()
	{
		if (InWater)
		{
			InWater = WaterController.Exist && !WaterController.isDisabled;
		}
		if (_hasParentMachine && !isBuildBlock && isSimulating)
		{
			CalculateBounds();
		}
	}

	public void UpdateSimState(bool updateKinematic = true)
	{
		if (isBuildBlock)
		{
			SimPhysics = (isSimulating = false);
		}
		else if (_hasParentMachine)
		{
			SimPhysics = _parentMachine.SimPhysics;
			isSimulating = _parentMachine.isSimulating;
		}
		else if (!StatMaster.isMP)
		{
			SimPhysics = (isSimulating = StatMaster.levelSimulating);
		}
		else
		{
			SimPhysics = StatMaster.isHosting || StatMaster.isLocalSim;
			isSimulating = infoType != BasicInfoType.None || StatMaster.levelSimulating;
		}
		if (updateKinematic)
		{
			isKinematic = SimPhysics && !noRigidbody && Rigidbody.isKinematic;
		}
		if (!isBuildBlock && WaterController.Exist)
		{
			InWater = WaterController.GetInitialWaterState(base.transform.position.y);
		}
	}

	protected virtual void OnEnable()
	{
		if (!_hasParentMachine || isBuildBlock)
		{
			UpdateSimState();
		}
		if (StatMaster.isMP && !isBuildBlock && !noRigidbody && Rigidbody == null)
		{
			noRigidbody = true;
		}
		if (!added && isSimulating && SimPhysics && !isBuildBlock)
		{
			bool flag = base.gameObject.CompareTag("StayKinematic");
			bool flag2 = false;
			if (infoType == BasicInfoType.Entity)
			{
				GenericEntity genericEntity = (GenericEntity)this;
				if (genericEntity.prefab.stayKinematic)
				{
					flag = true;
					flag2 = true;
				}
				if (genericEntity.prefab.ignorePhysics)
				{
					flag2 = true;
				}
				if (genericEntity.entity.isStatic && genericEntity.prefab.batchWhenStatic)
				{
					flag2 = true;
				}
			}
			if (!noRigidbody && !flag)
			{
				isKinematic = false;
			}
			if (!noRigidbody && !flag2)
			{
				ReferenceMaster.ExternalForceObjects.Add(this);
				ReferenceMaster.ExternalForceTemp.Add(this);
				added = true;
			}
		}
		else if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim && StatMaster.aeroCoded && infoType == BasicInfoType.Block && !added && !noRigidbody)
		{
			ClientAddToExternalForceObjects();
		}
		isDisabled = false;
	}

	public void ClientAddToExternalForceObjects(bool remove = false)
	{
		if (added && remove)
		{
			ReferenceMaster.ExternalForceObjects.Remove(this);
			added = false;
		}
		else if (!added)
		{
			ReferenceMaster.ExternalForceObjects.Add(this);
			added = true;
		}
	}

	protected virtual void OnDisable()
	{
		if (added && (SimPhysics || StatMaster.aeroCoded))
		{
			ReferenceMaster.ExternalForceObjects.Remove(this);
			ReferenceMaster.ExternalForceTemp.Remove(this);
			added = false;
		}
		isDisabled = true;
		if (CallBackOnDisable != null)
		{
			CallBackOnDisable();
		}
	}

	protected virtual void OnDestroy()
	{
		if (added && (SimPhysics || StatMaster.aeroCoded))
		{
			ReferenceMaster.ExternalForceObjects.Remove(this);
			ReferenceMaster.ExternalForceTemp.Remove(this);
			added = false;
		}
		_hasParentMachine = false;
		noRigidbody = true;
		isDestroyed = true;
	}

	public virtual void UpdateParentMachine()
	{
		if (infoType == BasicInfoType.Block && !_hasParentMachine)
		{
			_parentMachine = GetComponentInParent<Machine>();
			_hasParentMachine = !object.ReferenceEquals(_parentMachine, null);
			UpdateSimState();
		}
	}

	protected void DestroyRigidbody()
	{
		UnityEngine.Object.Destroy(Rigidbody);
		noRigidbody = true;
	}

	public virtual void SetGrabbed(bool grabbed, MonoBehaviour j)
	{
		if (grabbed)
		{
			if (hasAiScript)
			{
				aiEntity.Grabbed(j);
			}
		}
		else if (hasAiScript)
		{
			aiEntity.StopBeingGrabbed();
		}
		this.grabbed = grabbed;
	}

	public float CalculateClientSubmerge()
	{
		float num = submergedPercent;
		Quaternion rotation = base.transform.rotation;
		Vector3 vector = defaultExtents;
		Vector3 vector2 = Quaternion.Inverse(rotation) * Vector3.up;
		Vector3 vector3 = default(Vector3);
		vector3.x = ((!(vector2.x > 0f)) ? (0f - vector.x) : vector.x);
		vector3.y = ((!(vector2.y > 0f)) ? (0f - vector.y) : vector.y);
		vector3.z = ((!(vector2.z > 0f)) ? (0f - vector.z) : vector.z);
		Vector3 vector4 = rotation * vector3;
		float num2 = vector4.y + GetCenter().y;
		num = (num2 - waterDepth) / Math.Abs(vector4.y * 2f);
		if (num > 1f)
		{
			num = 1f;
		}
		else if (num < 0f)
		{
			num = 0f;
		}
		return 1f - num;
	}

	protected virtual void ChangeMass(float newValue)
	{
		if (!noRigidbody && (!gotBounds || originalDensity != 0f))
		{
			if (originalDensity == 0f && density != 0f)
			{
				originalDensity = density;
			}
			Rigidbody.mass = newValue;
			density = Rigidbody.mass / MaxArea;
		}
	}

	public virtual void CalculateDensity(bool forceRecalc = false)
	{
		if ((density == 0f || forceRecalc) && Rigidbody != null)
		{
			originalDensity = (density = Rigidbody.mass / MaxArea);
			BlockLoader instance = SingleInstanceFindOnly<BlockLoader>.Instance;
			if (infoType == BasicInfoType.Block && instance.IsModBlock(((BlockBehaviour)this).BlockID))
			{
				originalDensity *= instance.DefaultDensityMultiplier;
				density = originalDensity;
			}
			if (!base.gameObject.activeInHierarchy)
			{
				gotBounds = false;
			}
		}
	}
}
