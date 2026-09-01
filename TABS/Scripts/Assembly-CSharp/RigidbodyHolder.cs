using Landfall.MonoBatch;
using UnityEngine;

public class RigidbodyHolder : BatchedMonobehaviour
{
	public bool forceInterpolation;

	public float dragMultiplier = 1f;

	public bool setKinematic;

	public bool randomizeRigidbodySizes = true;

	private Rigidbody[] allRigs;

	private Collider[] allColliders;

	private PhysicMaterial[] allPhysicMats;

	public PhysicMaterial frictionMaterial;

	public float massMultiplier = 1f;

	private Vector2[] allDrags;

	public Vector2[] defaultDrags;

	private WeaponHandler weaponHandler;

	[HideInInspector]
	public DataHandler data;

	private GlobalSettingsHandler m_settingsHandler;

	private RigidbodyInterpolation m_interpolationMode;

	private bool m_isSlowMo;

	public Rigidbody[] AllRigs
	{
		get
		{
			return allRigs;
		}
		private set
		{
			allRigs = value;
		}
	}

	public Vector2[] AllDrags
	{
		get
		{
			return allDrags;
		}
		private set
		{
			allDrags = value;
		}
	}

	private void Awake()
	{
		allRigs = GetComponentsInChildren<Rigidbody>();
		data = GetComponent<DataHandler>();
		m_settingsHandler = ServiceLocator.GetService<GlobalSettingsHandler>();
	}

	private void OnPhysicsChanged(int value)
	{
		m_interpolationMode = GlobalSettingsHandler.GetInterpolationMode(value);
		UpdateInterpolationMode();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		m_settingsHandler.GetSettingsInstance("VIDEO_PHYSICS").OnValueChanged -= OnPhysicsChanged;
	}

	protected override void Start()
	{
		base.Start();
		weaponHandler = GetComponent<WeaponHandler>();
		allDrags = new Vector2[AllRigs.Length];
		defaultDrags = new Vector2[AllRigs.Length];
		for (int i = 0; i < allRigs.Length; i++)
		{
			allRigs[i].drag *= dragMultiplier;
			allRigs[i].angularDrag *= dragMultiplier;
			allDrags[i].x = allRigs[i].drag;
			allDrags[i].y = allRigs[i].angularDrag;
			defaultDrags[i].x = allRigs[i].drag;
			defaultDrags[i].y = allRigs[i].angularDrag;
			if (!data.unit.isUnitCopy)
			{
				allRigs[i].mass *= massMultiplier;
				if (randomizeRigidbodySizes)
				{
					allRigs[i].transform.localScale *= NormalRandom.GetSuperRandom(0.5f, 1.5f);
				}
			}
			if (setKinematic)
			{
				allRigs[i].isKinematic = true;
			}
		}
		ConfigurableJoint[] componentsInChildren = GetComponentsInChildren<ConfigurableJoint>();
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			JointDrive angularXDrive = componentsInChildren[j].angularXDrive;
			if (!data.unit.isUnitCopy)
			{
				angularXDrive.positionDamper *= massMultiplier;
				angularXDrive.positionSpring *= massMultiplier;
			}
			componentsInChildren[j].angularXDrive = angularXDrive;
			componentsInChildren[j].angularYZDrive = angularXDrive;
		}
		allColliders = GetComponentsInChildren<Collider>();
		allPhysicMats = new PhysicMaterial[allColliders.Length];
		SettingsInstance settingsInstance = m_settingsHandler.GetSettingsInstance("VIDEO_PHYSICS");
		settingsInstance.OnValueChanged += OnPhysicsChanged;
		OnPhysicsChanged(settingsInstance.currentValue);
	}

	public override void BatchedUpdate()
	{
		bool flag = Time.timeScale < 0.95f;
		if (flag != m_isSlowMo)
		{
			m_isSlowMo = flag;
			UpdateInterpolationMode();
		}
	}

	public void UpdateInterpolationMode()
	{
		RigidbodyInterpolation interpolation = ((m_isSlowMo || forceInterpolation) ? RigidbodyInterpolation.Interpolate : m_interpolationMode);
		int num = allRigs.Length;
		for (int i = 0; i < num; i++)
		{
			if ((bool)allRigs[i])
			{
				allRigs[i].interpolation = interpolation;
			}
		}
		if ((bool)weaponHandler)
		{
			if ((bool)weaponHandler.rightWeapon && (bool)weaponHandler.rightWeapon.rigidbody)
			{
				weaponHandler.rightWeapon.rigidbody.interpolation = interpolation;
			}
			if ((bool)weaponHandler.leftWeapon && (bool)weaponHandler.leftWeapon.rigidbody)
			{
				weaponHandler.leftWeapon.rigidbody.interpolation = interpolation;
			}
		}
	}

	public void SetColliders(bool slippery)
	{
		for (int i = 0; i < allColliders.Length; i++)
		{
			if (slippery)
			{
				allColliders[i].sharedMaterial = frictionMaterial;
			}
			else
			{
				allColliders[i].sharedMaterial = allPhysicMats[i];
			}
		}
	}
}
