using UnityEngine;

public class InheritForce : MonoBehaviour
{
	public ForceMode mode;

	public Vector3 forceToAdd;

	public Vector3 torqueToAdd;

	public float multiplier = 200f;

	private Rigidbody objectToForce;

	private Transform myTransform;

	protected void Awake()
	{
		myTransform = base.transform;
	}

	public void AddForce()
	{
		for (int i = 0; i < myTransform.childCount; i++)
		{
			Rigidbody component = myTransform.GetChild(i).GetComponent<Rigidbody>();
			if ((bool)component)
			{
				objectToForce = component;
				objectToForce.AddForce(forceToAdd * multiplier, mode);
				objectToForce.AddTorque(torqueToAdd * multiplier, mode);
			}
		}
	}
}
