using UnityEngine;

public class RuneParticles : MonoBehaviour
{
	public FireController fireController;

	public GameObject particleObject;

	public GameObject particleObject2;

	public Rigidbody pillarSegment;

	private void Update()
	{
		if (fireController.onFire)
		{
			particleObject.SetActive(true);
			particleObject2.SetActive(true);
			pillarSegment.constraints = (RigidbodyConstraints)94;
			Object.Destroy(this);
		}
	}
}
