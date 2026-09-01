using UnityEngine;

public class CurvedBlock : BlockBehaviour
{
	public float pushForce = 1f;

	public override void FixedUpdateBlock()
	{
		if (WaterController.Exist && !noRigidbody && base.InWater && !StatMaster.GodTools.GravityDisabled)
		{
			Vector3 inNormal = new Vector3(0f, 0.707f, -0.707f);
			Vector3 velocity = Rigidbody.velocity;
			velocity = base.transform.InverseTransformDirection(velocity);
			velocity = Vector3.Reflect(velocity, inNormal);
			velocity.x = 0f;
			velocity = base.transform.TransformDirection(velocity);
			velocity = Vector3.ClampMagnitude(velocity, 1000f);
			Rigidbody.AddForce(velocity * submergedPercent * pushForce, ForceMode.Acceleration);
		}
	}
}
