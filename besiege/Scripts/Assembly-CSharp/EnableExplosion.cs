using UnityEngine;

public class EnableExplosion : MonoBehaviour
{
	private BarrierExplosion toBeEnabled;

	private void Start()
	{
		toBeEnabled = GetComponent<BarrierExplosion>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			toBeEnabled.enabled = true;
		}
	}
}
