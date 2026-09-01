using UnityEngine;

public class ShakeCompositor : MonoBehaviour
{
	public Transform shakeParent;

	private Transform[] shakes;

	private void Start()
	{
		shakes = new Transform[shakeParent.childCount];
		for (int i = 0; i < shakes.Length; i++)
		{
			shakes[i] = shakeParent.GetChild(i);
		}
	}

	private void LateUpdate()
	{
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		for (int i = 0; i < shakes.Length; i++)
		{
			zero += shakes[i].transform.localPosition;
			zero2 += shakes[i].transform.localEulerAngles;
		}
		base.transform.localPosition = zero;
		base.transform.localEulerAngles = zero2;
	}
}
