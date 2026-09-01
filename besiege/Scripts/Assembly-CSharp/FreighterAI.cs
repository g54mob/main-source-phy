using UnityEngine;

public class FreighterAI : MonoBehaviour
{
	public int VictoryValue = 20;

	public Rigidbody[] balloons;

	public Vector3 offsetForce = Vector3.zero;

	private float targetAltitude = 15f;

	public float balloonPower = 200f;

	private float startAltitude;

	public float bobSpeed = 2f;

	public float amplitude = 2f;

	public float timingOffset;

	public bool machineBroken;

	public float forceAfterBroken = 240f;

	public float torqueAfterBroken;

	public RandomSoundController randomSoundController;

	public bool targetAltitudeNeverBelowPlayer;

	protected Vector3 ShipPos
	{
		get
		{
			Vector3 zero = Vector3.zero;
			float num = 0f;
			for (int i = 0; i < balloons.Length; i++)
			{
				if ((bool)balloons[i])
				{
					zero += balloons[i].position;
					num += 1f;
				}
			}
			if (num == 0f)
			{
				return Vector3.zero;
			}
			return zero / num;
		}
	}

	protected float ShipHeight
	{
		get
		{
			float num = ShipPos.y - targetAltitude;
			if (num < 0f)
			{
				num = 0f;
			}
			return num;
		}
	}

	private void Start()
	{
		if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim)
		{
			return;
		}
		startAltitude = balloons[0].position.y + 2.5f;
		targetAltitude = startAltitude;
		timingOffset = Random.Range(0, 10);
		if (!StatMaster.levelSimulating)
		{
			int num = VictoryValue - 1;
			if (num > 0)
			{
				WinCondition.Instance.fullObjectiveObjectCount += num;
			}
		}
	}

	private void Update()
	{
		if ((!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim) && StatMaster.levelSimulating)
		{
			SetSinePos();
		}
	}

	private void FixedUpdate()
	{
		if ((!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim) && StatMaster.levelSimulating)
		{
			SetBalloons();
		}
	}

	private void SetBalloons()
	{
		float num = 0f;
		bool flag = false;
		Vector3 shipPos = ShipPos;
		if (targetAltitudeNeverBelowPlayer)
		{
			Vector3 machineCenterPos = Machine.Active().MachineCenterPos;
			if ((shipPos - machineCenterPos).sqrMagnitude < 15000f)
			{
				num = machineCenterPos.y + 10f;
				num -= startAltitude;
				if (num < 0f)
				{
					num = 0f;
				}
			}
		}
		for (int i = 0; i < balloons.Length; i++)
		{
			if (!(balloons[i] != null))
			{
				continue;
			}
			if (!machineBroken)
			{
				Vector3 position = balloons[i].position;
				bool flag2 = position.y > shipPos.y + 0.2f;
				float num2 = position.y - targetAltitude - num;
				if ((targetAltitudeNeverBelowPlayer && num > 0f) || ShipHeight > 0f)
				{
					num2 = Mathf.Clamp(num2, (!flag2) ? (-4f) : (-2f), -1f);
				}
				if (offsetForce.sqrMagnitude > 0f)
				{
					balloons[i].AddForceAtPosition(new Vector3(0f, num2, 0f) * balloonPower, balloons[i].transform.TransformPoint(offsetForce));
				}
				else
				{
					balloons[i].AddForce(new Vector3(0f, num2, 0f) * balloonPower);
				}
			}
			else if (StatMaster.isMP)
			{
				balloons[i].AddRelativeForce(Vector3.up * Random.Range(0f, forceAfterBroken));
				balloons[i].AddRelativeTorque((Vector3.up + Random.onUnitSphere) * forceAfterBroken * 0.2f);
			}
			else
			{
				balloons[i].AddRelativeForce(Vector3.up * forceAfterBroken);
				if (torqueAfterBroken > 0f)
				{
					balloons[i].AddRelativeTorque((Vector3.up + Random.onUnitSphere) * torqueAfterBroken);
				}
			}
		}
	}

	private void SetSinePos()
	{
		targetAltitude = startAltitude + Mathf.Sin(Time.time * bobSpeed + timingOffset) * amplitude;
	}

	public void Break()
	{
		if (!machineBroken && (bool)randomSoundController)
		{
			randomSoundController.Play();
		}
		if (!machineBroken)
		{
			AddToPercentageBar();
		}
		machineBroken = true;
	}

	private void AddToPercentageBar()
	{
		if (!StatMaster.isMP && base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.currentObjsCompleted += VictoryValue;
		}
	}
}
