using UnityEngine;

public class DestroyJointIfNull : MonoBehaviour
{
	private ServerMachine machine;

	private bool checkedForMachine;

	private bool hasServerMachine;

	private float totalDamageAdded;

	private bool checkedForHealthBar;

	private bool hasHealthBar;

	private static float jointDamageValue = 4f;

	public void CheckJoints()
	{
		if (!checkedForHealthBar)
		{
			checkedForHealthBar = true;
			hasHealthBar = base.gameObject.GetComponent<BlockHealthBar>() != null;
		}
		if (!base.gameObject.CompareTag("StayKinematic") && !checkedForMachine && !hasHealthBar)
		{
			checkedForMachine = true;
			Machine componentInParent = base.transform.GetComponentInParent<Machine>();
			if (componentInParent is ServerMachine)
			{
				hasServerMachine = true;
				machine = componentInParent as ServerMachine;
			}
		}
		ConfigurableJoint[] components = base.gameObject.GetComponents<ConfigurableJoint>();
		HingeJoint[] components2 = base.gameObject.GetComponents<HingeJoint>();
		int num = 0;
		for (num = 0; num < components.Length; num++)
		{
			if (components[num].connectedBody == null)
			{
				Object.Destroy(components[num]);
			}
			else if (hasServerMachine)
			{
				machine.DamageController.AddTotalDamage(jointDamageValue);
				totalDamageAdded += jointDamageValue;
			}
		}
		for (num = 0; num < components2.Length; num++)
		{
			if (components2[num].connectedBody == null)
			{
				Object.Destroy(components2[num]);
			}
			else if (hasServerMachine)
			{
				machine.DamageController.AddTotalDamage(jointDamageValue);
				totalDamageAdded += jointDamageValue;
			}
		}
	}

	protected virtual void OnJointBreak(float breakForce)
	{
		if (hasServerMachine)
		{
			machine.DamageController.ApplyJointDamage(jointDamageValue);
		}
	}

	protected virtual void OnDestroy()
	{
		if (hasServerMachine)
		{
			machine.DamageController.RemoveTotalDamage(totalDamageAdded);
		}
	}
}
