using UnityEngine;

public class SnapOnSharpChop : MonoBehaviour
{
	public SpringCode springCode;

	public HarpoonController harpoonCode;

	private Machine machine;

	private bool hasMachine;

	private void Start()
	{
		Collider component = GetComponent<Collider>();
		if (component != null)
		{
			component.enabled = ((!(springCode != null)) ? harpoonCode.isSimulating : springCode.isSimulating);
			machine = GetComponentInParent<Machine>();
			hasMachine = machine != null;
			if (hasMachine && machine.isSimulating)
			{
				base.gameObject.layer = 25;
			}
		}
		else
		{
			Object.Destroy(this);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!hasMachine || !machine.SimPhysics || !machine.isSimulating || machine.UnbreakableMode || other.isTrigger || !other.attachedRigidbody)
		{
			return;
		}
		BlockBehaviour component = other.attachedRigidbody.GetComponent<BlockBehaviour>();
		if (!object.ReferenceEquals(component, null) && component.Prefab.hasDamageType && component.Prefab.myDamageType == DamageType.Sharp)
		{
			if (springCode != null)
			{
				springCode.Snap();
			}
			else if (harpoonCode != null)
			{
				harpoonCode.Snap();
			}
			Object.Destroy(this);
		}
	}
}
