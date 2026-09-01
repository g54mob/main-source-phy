using UnityEngine;

public class OnCollisionDestroyJoints : MonoBehaviour
{
	public Vector3 edgeCenter;

	public Vector3 edgeSize;

	public Vector3 edgeRotation;

	public Vector3 forceScale = Vector3.one;

	public bool SliceAhead;

	public LayerMask mask;

	private void OnDrawGizmosSelected()
	{
		DebugExtension.DebugCube(base.transform.TransformPoint(edgeCenter), edgeSize, base.transform.rotation * Quaternion.Euler(edgeRotation), Color.magenta, 0f);
	}

	private void OnCollisionEnter(Collision col)
	{
		Rigidbody attachedRigidbody = col.collider.attachedRigidbody;
		if (!attachedRigidbody)
		{
			return;
		}
		Joint[] components = attachedRigidbody.GetComponents<Joint>();
		int num = 0;
		Joint[] array = components;
		foreach (Joint joint in array)
		{
			for (int j = 0; j < col.contacts.Length; j++)
			{
				if (IsPointInsideBox(col.contacts[j].point, edgeCenter, edgeSize, base.transform.rotation * Quaternion.Euler(edgeRotation)))
				{
					float breakForce = (joint.breakTorque = 0f);
					joint.breakForce = breakForce;
					num++;
					break;
				}
			}
		}
		if (num <= 0)
		{
			return;
		}
		Vector3 direction = col.collider.transform.position - col.contacts[0].point;
		direction = base.transform.InverseTransformDirection(direction);
		direction = Vector3.Scale(direction, forceScale);
		direction = base.transform.TransformDirection(direction);
		col.collider.attachedRigidbody.AddForceAtPosition(direction, col.contacts[0].point, ForceMode.VelocityChange);
		if (!SliceAhead)
		{
			return;
		}
		Collider[] array2 = Physics.OverlapBox(base.transform.TransformPoint(edgeCenter), edgeSize * 0.5f, base.transform.rotation * Quaternion.Euler(edgeRotation), mask);
		for (int k = 0; k < array2.Length; k++)
		{
			if ((bool)array2[k].attachedRigidbody)
			{
				DestroyJoints(array2[k].attachedRigidbody);
			}
		}
	}

	private void DestroyJoints(Rigidbody b)
	{
		Joint[] components = b.GetComponents<Joint>();
		int num = 0;
		Joint[] array = components;
		int num2 = 0;
		if (num2 < array.Length)
		{
			Joint joint = array[num2];
			float breakForce = (joint.breakTorque = 0f);
			joint.breakForce = breakForce;
			num++;
		}
	}

	private bool IsPointInsideBox(Vector3 point, Vector3 boxCenter, Vector3 boxSize, Quaternion boxRotation)
	{
		Vector3 vector = Quaternion.Inverse(boxRotation) * (point - base.transform.TransformPoint(boxCenter));
		return Mathf.Abs(vector.x) <= boxSize.x * 0.5f && Mathf.Abs(vector.y) <= boxSize.y * 0.5f && Mathf.Abs(vector.z) <= boxSize.z * 0.5f;
	}
}
