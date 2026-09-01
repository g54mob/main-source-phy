using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
	private PlayerHolding holding;

	private SwingHandler swing;

	private PlayerInput input;

	[HideInInspector]
	public FPSWeapon fpsWeapon;

	private Transform cam;

	private FpsRangeWeaponHandler rangeWeapon;

	private void Start()
	{
		rangeWeapon = GetComponentInChildren<FpsRangeWeaponHandler>();
		cam = GetComponentInChildren<Camera>().transform;
		swing = GetComponentInChildren<SwingHandler>();
		holding = GetComponent<PlayerHolding>();
		input = GetComponent<PlayerInput>();
	}

	private void Update()
	{
		if ((bool)holding.weapon)
		{
			fpsWeapon = holding.weapon.GetComponent<FPSWeapon>();
		}
		if ((bool)fpsWeapon.meleeWeapon)
		{
			Swinging();
		}
		if ((bool)fpsWeapon.rangeWeapon)
		{
			Shooting();
		}
	}

	private void Shooting()
	{
		if (fpsWeapon.rangeWeaponType == FPSWeapon.RangeWeaponType.ChargeUp)
		{
			if (fpsWeapon.counter < fpsWeapon.cd)
			{
				return;
			}
			if (input.mouse0IsPressed)
			{
				rangeWeapon.ChargeUp(fpsWeapon);
			}
			else if (input.mouse0WasReleased)
			{
				rangeWeapon.Shoot(fpsWeapon, charge: true);
			}
		}
		if (fpsWeapon.rangeWeaponType == FPSWeapon.RangeWeaponType.JustShoot && !(fpsWeapon.counter < fpsWeapon.cd) && input.mouse0IsPressed)
		{
			rangeWeapon.Shoot(fpsWeapon);
		}
	}

	private void Swinging()
	{
		if (input.mouse0IsPressed && !swing.IsSwinging && swing.holdDirection != SwingHandler.HoldDirection.None)
		{
			swing.HoldSwing(fpsWeapon);
			holding.followWeaponTarget = true;
		}
		else if (input.mouse0WasReleased && !swing.IsSwinging && swing.holdDirection != SwingHandler.HoldDirection.None)
		{
			swing.StartSwing(fpsWeapon);
			fpsWeapon.Swing();
		}
		else if (swing.IsSwinging)
		{
			holding.followWeaponTarget = true;
			holding.SetTargetsToHoldable();
		}
		else
		{
			holding.followWeaponTarget = false;
		}
	}

	public void StartSwing()
	{
		fpsWeapon.StartSwing();
	}

	public void SetSwingData(Vector3 swingDirection)
	{
		fpsWeapon.SetSwingData((swingDirection + cam.transform.forward).normalized);
	}

	public void EndSwing()
	{
		fpsWeapon.EndSwing();
	}
}
