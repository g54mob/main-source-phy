using UnityEngine;

public class FpsRangeWeaponHandler : MonoBehaviour
{
	private PlayerHolding holding;

	public bool isCharging;

	private BowStringAnimation BowStringAnim;

	private float chargeUp;

	private CharacterData data;

	private void Start()
	{
		data = GetComponentInParent<CharacterData>();
		holding = GetComponentInParent<PlayerHolding>();
	}

	public void ChargeUp(FPSWeapon weapon)
	{
		if (!isCharging)
		{
			chargeUp = 0f;
			holding.SetHoldingData("ChargeUp");
			GetComponentInParent<PlayCurveRotation>().Play(weapon.shootData.chargeUpCamCurve, weapon.shootData.chargeUpCamCurveAngle);
		}
		isCharging = true;
		chargeUp += Time.deltaTime / weapon.chargeUpTime;
		chargeUp = Mathf.Clamp(chargeUp, 0f, 1f);
		weapon.ChargeUp(chargeUp);
	}

	public void Shoot(FPSWeapon weapon, bool charge = false)
	{
		if (charge)
		{
			weapon.ChargeUp(chargeUp);
		}
		isCharging = false;
		holding.SetHoldingData();
		weapon.Shoot();
		data.screenShake.AddForce(weapon.shootData.screenShake * weapon.transform.forward * 3f, weapon.transform.position);
	}
}
