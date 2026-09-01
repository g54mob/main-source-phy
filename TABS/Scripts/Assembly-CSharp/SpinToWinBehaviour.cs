using UnityEngine;
using UnityEngine.Events;

public class SpinToWinBehaviour : MonoBehaviour
{
	public enum SpinDirection
	{
		WorldUp = 0,
		MainRigUp = 1
	}

	public SpinDirection spinDir;

	public bool isOn;

	public float spinForce;

	public float timeToReachFullSpin = 2f;

	private float spinMultiplier;

	private RotationHandler rotation;

	private DataHandler data;

	private float attackSpeedM = 1f;

	public UnityEvent turnadoStartEvent;

	public UnityEvent turnadoEndEvent;

	private bool isTurnado;

	private float turnadoTime;

	private void Start()
	{
		rotation = base.transform.root.GetComponentInChildren<RotationHandler>();
		data = base.transform.root.GetComponentInChildren<DataHandler>();
	}

	private void FixedUpdate()
	{
		if ((bool)data && (bool)data.unit && !isTurnado)
		{
			attackSpeedM = (5f + data.unit.attackSpeedMultiplier) / 10f;
		}
		CheckTurnado();
		if (isOn)
		{
			spinMultiplier = Mathf.Clamp(spinMultiplier + Time.fixedDeltaTime / timeToReachFullSpin, 0f, 1f);
		}
		else
		{
			spinMultiplier = Mathf.Clamp(spinMultiplier - Time.fixedDeltaTime / timeToReachFullSpin, 0f, 1f);
			if ((bool)rotation)
			{
				rotation.rotationMultiplier = Mathf.Clamp(rotation.rotationMultiplier + Time.fixedDeltaTime / timeToReachFullSpin, 0f, 1f);
			}
		}
		if ((bool)data && (bool)data.mainRig && !data.Dead && (data.isGrounded || attackSpeedM > 2f))
		{
			if (spinDir == SpinDirection.WorldUp)
			{
				data.mainRig.AddTorque(attackSpeedM * spinForce * spinMultiplier * Vector3.up, ForceMode.Acceleration);
			}
			else
			{
				data.mainRig.AddTorque(attackSpeedM * spinForce * spinMultiplier * data.mainRig.transform.up, ForceMode.Acceleration);
			}
		}
	}

	public void TurnOn()
	{
		isOn = true;
		if ((bool)rotation)
		{
			rotation.rotationMultiplier = 0f;
		}
	}

	public void TurnOff()
	{
		if (!isTurnado)
		{
			isOn = false;
		}
	}

	private void CheckTurnado()
	{
		if ((bool)data && data.Dead)
		{
			if (isTurnado)
			{
				isTurnado = false;
				turnadoEndEvent.Invoke();
			}
			return;
		}
		if (attackSpeedM > 4f && !isTurnado)
		{
			turnadoStartEvent.Invoke();
			ConditionalEvent component = GetComponent<ConditionalEvent>();
			if ((bool)component)
			{
				component.AddRangeToAllConditions(200f);
				if ((bool)component.data)
				{
					component.data.canFall = false;
					component.data.unit.turnSpeedMultiplier *= 100f;
					Explosion componentInChildren = GetComponentInChildren<Explosion>();
					if ((bool)componentInChildren)
					{
						componentInChildren.AddProtectedRig(data.mainRig);
					}
					if ((bool)component.data.weaponHandler)
					{
						if ((bool)component.data.weaponHandler.leftWeapon)
						{
							CollisionWeapon component2 = component.data.weaponHandler.leftWeapon.GetComponent<CollisionWeapon>();
							if ((bool)component2)
							{
								component2.damage *= 6f;
								component2.onImpactForce *= 10f;
								component2.massCap *= 7f;
							}
						}
						if ((bool)component.data.weaponHandler.rightWeapon)
						{
							CollisionWeapon component3 = component.data.weaponHandler.rightWeapon.GetComponent<CollisionWeapon>();
							if ((bool)component3)
							{
								component3.damage *= 6f;
								component3.onImpactForce *= 10f;
								component3.massCap *= 7f;
							}
						}
					}
				}
			}
			ServiceLocator.GetService<AchievementService>().UnlockAchievement("TWISTED_TWISTER");
			isTurnado = true;
		}
		if (isTurnado)
		{
			turnadoTime += Time.deltaTime;
			float num = Mathf.Clamp(turnadoTime * 0.2f, 0f, 1f);
			data.mainRig.AddForce(20000f * num * Time.deltaTime * data.characterForwardObject.forward, ForceMode.Acceleration);
			data.mainRig.AddForce(5000f * num * Time.deltaTime * Vector3.up, ForceMode.Acceleration);
		}
	}
}
