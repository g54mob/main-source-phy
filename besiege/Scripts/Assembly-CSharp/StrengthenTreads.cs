using System.Collections;
using UnityEngine;

public class StrengthenTreads : MonoBehaviour
{
	public float breakForce = 22000f;

	public float breakTorque = 20000f;

	[SerializeField]
	protected BlockBehaviour block;

	private void Start()
	{
		if (block.isSimulating)
		{
			if (!block.SimPhysics)
			{
				Object.Destroy(this);
			}
			else if (!block.ParentMachine.UnbreakableMode)
			{
				StartCoroutine(ChangeStrength());
			}
		}
	}

	private IEnumerator ChangeStrength()
	{
		yield return null;
		yield return null;
		yield return null;
		Joint joint = block.blockJoint;
		if ((bool)joint && (bool)joint.connectedBody)
		{
			BlockBehaviour otherBlock = joint.connectedBody.GetComponent<BlockBehaviour>();
			if (otherBlock != null && otherBlock.Prefab.Type == BlockType.Hinge)
			{
				joint.breakForce = breakForce;
				joint.breakTorque = breakTorque;
			}
		}
		Object.Destroy(this);
	}
}
