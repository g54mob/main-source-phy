using System.Collections;
using UnityEngine;

public class SetInterpolationBasedOnTimescale : MonoBehaviour
{
	public int waitFrames;

	private IEnumerator Start()
	{
		if (StatMaster.useSmartInterpolation)
		{
			for (int i = 0; i < waitFrames; i++)
			{
				yield return null;
			}
			Rigidbody r = base.gameObject.GetComponent<Rigidbody>();
			if ((bool)r && !r.isKinematic)
			{
				r.interpolation = ((Time.timeScale < 0.6f) ? RigidbodyInterpolation.Interpolate : RigidbodyInterpolation.None);
			}
		}
	}
}
