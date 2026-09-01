using System;
using UnityEngine;

public class mySmoothFollow : MonoBehaviour
{
	public Transform target;

	public float smoothAmount;

	private void Start()
	{
		if (target == null)
		{
			MouseOrbit.CameraMoved = (Action<Vector3>)Delegate.Combine(MouseOrbit.CameraMoved, new Action<Vector3>(Follow));
		}
	}

	private void LateUpdate()
	{
		if (!(target == null))
		{
			base.transform.position = Vector3.Lerp(base.transform.position, target.position, TimeSlider.Instance.deltaTime * smoothAmount);
		}
	}

	private void Follow(Vector3 pos)
	{
		base.transform.position = pos;
	}

	private void OnDestroy()
	{
		MouseOrbit.CameraMoved = (Action<Vector3>)Delegate.Remove(MouseOrbit.CameraMoved, new Action<Vector3>(Follow));
	}
}
