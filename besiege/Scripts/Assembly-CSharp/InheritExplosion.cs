using UnityEngine;

public class InheritExplosion : MonoBehaviour
{
	public float forceScaler = 2f;

	public float upScaler = 2f;

	private Rigidbody objectToForce;

	private Transform myTransform;

	private bool awake;

	protected void Awake()
	{
		if (!awake)
		{
			myTransform = base.transform;
			awake = true;
		}
	}

	public void InheritForce(float powery, Vector3 position, float radiusy, float upAmount)
	{
		Awake();
		for (int i = 0; i < myTransform.childCount; i++)
		{
			Rigidbody component = myTransform.GetChild(i).GetComponent<Rigidbody>();
			if (component != null)
			{
				component.AddExplosionForce(powery * forceScaler, position, radiusy, upAmount * upScaler);
			}
		}
	}
}
