using TFBGames;
using UnityEngine;

public class PushStick : MonoBehaviour
{
	public float force;

	public float lowMass = 100f;

	private ProjectileStick stick;

	private void Start()
	{
		stick = GetComponent<ProjectileStick>();
	}

	private void Update()
	{
		if ((bool)stick.targetRig)
		{
			if (stick.targetRig.mass > 50f)
			{
				WilhelmPhysicsFunctions.AddForceWithMinWeight(stick.targetRig, (Vector3.up * 0.3f + base.transform.forward) * (Time.deltaTime * force * FixedTimeStepService.SmallForceCoefficient), base.transform.position, ForceMode.Force, lowMass);
			}
			else
			{
				WilhelmPhysicsFunctions.AddForceWithMinWeight(stick.targetRig, (Vector3.up * 0.3f + base.transform.forward) * (Time.deltaTime * force * FixedTimeStepService.SmallForceCoefficient), ForceMode.Force, lowMass);
			}
			stick.targetRig.velocity += stick.targetRig.velocity * (Time.deltaTime * -15f);
			DataHandler componentInChildren = stick.targetRig.transform.root.GetComponentInChildren<DataHandler>();
			if ((bool)componentInChildren)
			{
				componentInChildren.sinceGrounded = 0f;
			}
		}
	}
}
