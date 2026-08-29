using UnityEngine;

public class LocalRotator : MonoBehaviour
{
	public float MaxAngle = 15f;

	public AnimationCurve Curve;

	public float LoopLength;

	public float Offset;

	public Vector3 Axis = Vector3.up;

	private Quaternion startRotation;

	private float elapsedTime;

	private void Start()
	{
		startRotation = base.transform.localRotation;
		elapsedTime = Offset;
	}

	private void OnEnable()
	{
		elapsedTime = Offset;
	}

	private void Update()
	{
		if (float.IsNaN(elapsedTime))
		{
			elapsedTime = 0f;
		}
		elapsedTime += Time.deltaTime;
		elapsedTime = Mathf.Repeat(elapsedTime, LoopLength);
		float time = elapsedTime / LoopLength;
		float num = Curve.Evaluate(time);
		base.transform.localRotation = Quaternion.AngleAxis(num * MaxAngle, Axis) * startRotation;
	}
}
