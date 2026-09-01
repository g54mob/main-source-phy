using System.Collections;
using UnityEngine;

public class SetInterpolationOnSimulate : SimBehaviour
{
	public int waitFrames;

	public RigidbodyInterpolation interpolate = RigidbodyInterpolation.Interpolate;

	protected override void Start()
	{
		base.Start();
		if (base.isSimulating && base.SimPhysics)
		{
			StartCoroutine(SetInterpolation());
		}
	}

	protected IEnumerator SetInterpolation()
	{
		for (int i = 0; i < waitFrames; i++)
		{
			yield return null;
		}
		Rigidbody body = GetComponent<Rigidbody>();
		if ((bool)body)
		{
			body.interpolation = ((Time.timeScale < 0.6f || !StatMaster.useSmartInterpolation) ? interpolate : RigidbodyInterpolation.None);
		}
		Object.Destroy(this);
	}
}
