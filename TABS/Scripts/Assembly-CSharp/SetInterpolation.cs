using UnityEngine;

public class SetInterpolation : MonoBehaviour
{
	private Rigidbody rig;

	private bool isTimeSlow;

	private void Start()
	{
		rig = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if ((bool)rig && (double)Time.timeScale < 0.9 != isTimeSlow)
		{
			isTimeSlow = Time.timeScale < 0.9f;
			if (isTimeSlow)
			{
				rig.interpolation = RigidbodyInterpolation.Interpolate;
			}
			else
			{
				rig.interpolation = RigidbodyInterpolation.None;
			}
		}
	}
}
