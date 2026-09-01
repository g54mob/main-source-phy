using System.Linq;
using UnityEngine;

public class CollisionEnterHook : MonoBehaviour, IExplosionEffect
{
	public BlockBehaviour thisBlock;

	public event CollisionEntered CollisionEntered;

	public virtual void OnCollisionEnter(Collision other)
	{
		if (this.CollisionEntered != null)
		{
			this.CollisionEntered(other);
		}
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (object.ReferenceEquals(thisBlock, null))
		{
			return false;
		}
		if (thisBlock.gotChildBlocks)
		{
			thisBlock.CreateSimLists();
			foreach (BlockBehaviour item in thisBlock.parentedColliders.Keys.Reverse())
			{
				explosionPos = new Vector3(explosionPos.x, explosionPos.y - upPower, explosionPos.z);
				Vector3 vector = item.transform.TransformPoint(item.originalCOM) - explosionPos;
				float magnitude = vector.magnitude;
				Vector3 vector2 = (1f - ((!(radius > 0f)) ? 0f : (magnitude / radius))) * power * vector.normalized;
				vector2 = ((!(item.originalMass > 0f)) ? Vector3.zero : (vector2 / item.originalMass * Time.fixedDeltaTime));
				item.Rigidbody.AddForceAtPosition(vector2, item.transform.TransformPoint(item.originalCOM), ForceMode.Acceleration);
				item.Rigidbody.AddRelativeTorque(Random.onUnitSphere * torquePower);
				if (item.jointBreakForce != float.PositiveInfinity)
				{
					item.StartCoroutine(item.VirtualJointBreakExplosion(vector2));
				}
			}
		}
		return false;
	}
}
