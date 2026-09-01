using Landfall.TABS;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public enum WeaponMaterialType
	{
		Metal = 0,
		Wood = 1,
		Meat = 2
	}

	public enum WeaponAlignment
	{
		Forward = 0,
		Up = 1
	}

	public enum WeaponType
	{
		Melee = 0,
		Bullet = 1,
		Magic = 2,
		Etc = 3
	}

	public enum WeaponTargetType
	{
		EnemiesOnly = 0,
		Fridemies = 1
	}

	public enum PossessionAimingMode
	{
		UseTarget = 0,
		UseCameraDir = 1,
		Forward = 2
	}

	public WeaponMaterialType weaponMaterialType;

	public WeaponAlignment weaponAlignment;

	public WeaponType m_weaponType;

	public WeaponTargetType m_weaponTargetType;

	public Faction faction;

	[HideInInspector]
	public Weapon[] weaponList;

	[HideInInspector]
	public float defaultDrag;

	[HideInInspector]
	public float defaultAngularDrag;

	public float internalCooldown = 1f;

	public bool randomCooldown = true;

	public float maxRange = 2f;

	public float maxAngle = 360f;

	public bool startOnCooldown;

	public bool countSelf = true;

	public bool onlyCountInRange;

	[HideInInspector]
	public float levelMultiplier = 1f;

	[HideInInspector]
	public float internalCounter;

	[HideInInspector]
	public Rigidbody rigidbody;

	protected Holdable holdable;

	[HideInInspector]
	public AttackEffect[] attackEffects;

	private DataHandler internalConnectedData;

	public bool callAttackEffects = true;

	public bool useTargetingWhenPossessed = true;

	public PossessionAimingMode possessionAimingMode;

	public bool isRange;

	[HideInInspector]
	public float attackSpeedM = 1f;

	public bool auto;

	[HideInInspector]
	public DataHandler connectedData
	{
		get
		{
			if (!internalConnectedData && (bool)holdable && (bool)holdable.holderData)
			{
				internalConnectedData = holdable.holderData;
			}
			else if (!internalConnectedData)
			{
				internalConnectedData = base.transform.root.GetComponentInChildren<DataHandler>();
			}
			return internalConnectedData;
		}
		set
		{
			internalConnectedData = value;
		}
	}

	public byte WeaponIndex { get; private set; }

	protected virtual void Awake()
	{
		internalCounter = 10000f;
		holdable = GetComponent<Holdable>();
		rigidbody = GetComponent<Rigidbody>();
		if ((bool)rigidbody)
		{
			defaultDrag = rigidbody.drag;
			defaultAngularDrag = rigidbody.angularDrag;
		}
		attackEffects = GetComponentsInChildren<AttackEffect>();
		weaponList = GetComponents<Weapon>();
		isRange = GetComponent<RangeWeapon>();
		if (startOnCooldown)
		{
			internalCounter = 0f;
		}
	}

	protected virtual void Start()
	{
	}

	public void SetWeaponIndex(byte weaponIndex)
	{
		WeaponIndex = weaponIndex;
	}

	public float GetLowestAttackTimeOfWeapons()
	{
		float num = weaponList[0].internalCooldown;
		for (int i = 1; i < weaponList.Length; i++)
		{
			if (weaponList[i].internalCooldown < num)
			{
				num = weaponList[i].internalCooldown;
			}
		}
		return num;
	}

	public Weapon GetBestWeaponOnWeapon()
	{
		Weapon weapon = weaponList[0];
		for (int i = 1; i < weaponList.Length; i++)
		{
			if ((weapon.IsOnCooldown() && !weaponList[i].IsOnCooldown()) || (!weaponList[i].IsOnCooldown() && weaponList[i].maxRange > weapon.maxRange))
			{
				weapon = weaponList[i];
			}
		}
		return weapon;
	}

	public bool IsOnCooldown()
	{
		return internalCooldown > internalCounter;
	}

	public void UpdateCounters(float attackSpeedMultiplier = 1f)
	{
		if (!isRange)
		{
			attackSpeedM = (4f + attackSpeedMultiplier) * 0.2f;
			attackSpeedM = Mathf.Clamp(attackSpeedM, 1f, 2f);
		}
		else
		{
			attackSpeedM = attackSpeedMultiplier;
		}
		if ((!holdable || holdable.held) && countSelf && (!onlyCountInRange || ((bool)connectedData && connectedData.inRange)))
		{
			for (int i = 0; i < weaponList.Length; i++)
			{
				weaponList[i].internalCounter += Time.deltaTime * attackSpeedM;
			}
		}
	}

	public abstract void Attack(Vector3 position, Rigidbody targetRig, Vector3 forcedDirection, bool recursive = false);
}
