using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/DrillBrickDamage")]
public class DrillBrickDamage : CogMotorControllerHinge
{
	private void OnCollisionEnter(Collision other)
	{
		if (!isSimulating || !SimPhysics || Mathf.Abs(base.Input) < 0.1f)
		{
			return;
		}
		Rigidbody attachedRigidbody = other.collider.attachedRigidbody;
		if (!hasJoint || !(attachedRigidbody != null))
		{
			return;
		}
		BreakBase component = attachedRigidbody.GetComponent<BreakBase>();
		if (component == null)
		{
			return;
		}
		StructuralPhysTile structuralPhysTile = component as StructuralPhysTile;
		if (!object.ReferenceEquals(structuralPhysTile, null))
		{
			structuralPhysTile.DestroyTile(Vector3.zero);
			return;
		}
		PhysNodeTile physNodeTile = component as PhysNodeTile;
		if (!object.ReferenceEquals(physNodeTile, null))
		{
			physNodeTile.BreakNode(other);
		}
		else if ((bool)attachedRigidbody.GetComponent<Drillable>())
		{
			attachedRigidbody.GetComponent<BreakOnForce>().Drill(other.impulse.sqrMagnitude);
		}
	}
}
