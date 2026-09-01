using UnityEngine;

public class RandomUIRotation : MonoBehaviour
{
	public enum Axis
	{
		X = 0,
		Y = 1,
		Z = 2
	}

	[Header("Rotation Settings")]
	public float minAngle;

	public float maxAngle;

	public Axis rotationAxis;

	private void Awake()
	{
	}
}
