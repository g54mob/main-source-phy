using UnityEngine;

public static class BlinkEffect
{
	public static void Blink(DataHandler data, Rigidbody target, MeleeWeapon weapon = null)
	{
		Transform[] array = new Transform[data.transform.childCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = data.transform.GetChild(i);
		}
		for (int j = 0; j < array.Length; j++)
		{
			array[j].parent = null;
		}
		Transform transform = data.mainRig.transform;
		data.transform.SetPositionAndRotation(transform.position, transform.rotation);
		for (int k = 0; k < array.Length; k++)
		{
			array[k].parent = data.transform;
		}
		HoldingHandler component = data.GetComponent<HoldingHandler>();
		if ((bool)component)
		{
			if ((bool)component.rightObject)
			{
				component.rightObject.transform.parent = data.transform;
			}
			if ((bool)component.leftObject)
			{
				component.leftObject.transform.parent = data.transform;
			}
		}
		Vector3 vector = target.position + target.transform.forward * -0.75f - data.mainRig.position;
		vector.y = 0f;
		data.transform.root.position += vector;
		Vector3 forward = target.position - data.mainRig.position;
		forward.y = 0f;
		data.transform.rotation = Quaternion.LookRotation(forward);
		if ((bool)component)
		{
			if ((bool)component.rightObject)
			{
				component.rightObject.transform.parent = null;
			}
			if ((bool)component.leftObject)
			{
				component.leftObject.transform.parent = null;
			}
		}
		if ((bool)weapon)
		{
			weapon.swingDirection = (target.position + Vector3.down * 0.3f - weapon.transform.position).normalized;
		}
	}
}
