using UnityEngine;

public class AddRigidbodyOnDeath : MonoBehaviour
{
	public float mass = 200f;

	private HealthHandler healthHandler;

	private void Start()
	{
		healthHandler = base.transform.root.GetComponentInChildren<HealthHandler>();
		if ((bool)healthHandler)
		{
			healthHandler.AddDieAction(Die);
		}
	}

	public void Die()
	{
		Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
		rigidbody.mass = mass;
		rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		base.transform.SetParent(healthHandler.transform, worldPositionStays: true);
		Object.Destroy(this);
	}
}
