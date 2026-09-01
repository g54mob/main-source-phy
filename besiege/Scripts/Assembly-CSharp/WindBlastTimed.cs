using UnityEngine;

[AddComponentMenu("Physics/WindBlastTimed")]
public class WindBlastTimed : MonoBehaviour
{
	private WindController windController;

	public float windForce;

	private void Start()
	{
		windController = GetComponent<WindController>();
		windController.windForce = 0f;
		InvokeRepeating("WindBlast", 0f, 5f);
		InvokeRepeating("WindStop", 2.5f, 5.9f);
	}

	private void WindBlast()
	{
		windController.windForce = 65f;
	}

	private void WindStop()
	{
		windController.windForce = 0f;
	}
}
