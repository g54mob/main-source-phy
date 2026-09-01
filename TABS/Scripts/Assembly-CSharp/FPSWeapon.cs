using UnityEngine;

public class FPSWeapon : MonoBehaviour
{
	public enum RangeWeaponType
	{
		JustShoot = 0,
		ChargeUp = 1
	}

	public bool isHeld;

	public float cd;

	public float chargeUpTime = 1f;

	public float sizeMultiplier = 1f;

	[HideInInspector]
	public float counter;

	public float heldDrag = 18f;

	public bool customHoldingata;

	public Vector3 leftElbowGoal = Vector3.zero;

	public Vector3 rightElbowGoal = Vector3.zero;

	[Tooltip("Leave empty for default. Presets can be found on playerHolding.cs on player prefab")]
	public string holdingPresetName;

	public FpsHoldingData holdingData;

	public ShootDataInstance shootData;

	public RangeWeaponType rangeWeaponType;

	public SwingDataInstance down;

	public SwingDataInstance left;

	public SwingDataInstance right;

	public SwingDataInstance up;

	[HideInInspector]
	public MeleeWeapon meleeWeapon;

	[HideInInspector]
	public RangeWeapon rangeWeapon;

	private BowStringAnimation bowStringAnim;

	[HideInInspector]
	public float currentChargeUp;

	[HideInInspector]
	public Rigidbody rig;

	private Vector3 defaulSize;

	private bool inited;

	public void Init()
	{
		if (!inited)
		{
			inited = true;
			defaulSize = base.transform.localScale;
			rig = GetComponent<Rigidbody>();
			meleeWeapon = GetComponent<MeleeWeapon>();
			rangeWeapon = GetComponent<RangeWeapon>();
			bowStringAnim = GetComponent<BowStringAnimation>();
			SetStuff();
		}
	}

	public void Shoot()
	{
		if (rangeWeaponType == RangeWeaponType.ChargeUp)
		{
			rangeWeapon.charge = currentChargeUp * 0.4f + 0.6f;
		}
		rangeWeapon.Attack(base.transform.position + base.transform.forward * 25f, null, base.transform.forward);
		currentChargeUp = 0f;
		counter = 0f;
		if ((bool)bowStringAnim)
		{
			bowStringAnim.ChargeUp(0f);
		}
	}

	public void ChargeUp(float chargeUp)
	{
		if (chargeUp > currentChargeUp)
		{
			currentChargeUp = chargeUp;
		}
		if ((bool)bowStringAnim)
		{
			bowStringAnim.ChargeUp(chargeUp);
		}
	}

	private void Update()
	{
		counter += Time.deltaTime;
	}

	public void Swing()
	{
	}

	public void StartSwing()
	{
		if ((bool)meleeWeapon)
		{
			meleeWeapon.canDealDamage = true;
			CollisionWeapon component = meleeWeapon.GetComponent<CollisionWeapon>();
			if ((bool)component)
			{
				component.lastHitHealth = null;
			}
		}
	}

	public void SetSwingData(Vector3 direction)
	{
		if ((bool)meleeWeapon)
		{
			meleeWeapon.SetSpecificSwingDirection(direction);
		}
	}

	public void EndSwing()
	{
		if ((bool)meleeWeapon)
		{
			meleeWeapon.canDealDamage = false;
		}
	}

	private void SetStuff()
	{
		if (cd != 0f)
		{
			DelayEvent component = GetComponent<DelayEvent>();
			if ((bool)component)
			{
				component.delay = cd;
			}
		}
		if ((bool)rangeWeapon)
		{
			rangeWeapon.shootRecoil = shootData.recoil;
		}
		if (sizeMultiplier != 1f)
		{
			base.transform.localScale = defaulSize * sizeMultiplier;
		}
	}
}
