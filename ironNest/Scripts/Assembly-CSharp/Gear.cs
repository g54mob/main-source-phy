using UnityEngine;

public class Gear : MonoBehaviour
{
	public Transform parentGear;

	public float ratio;

	private float lastRatio;

	private Quaternion initialParentRotation;

	private Quaternion myInitialRotation;

	private Vector3 myInitialUp;

	private bool _initted;

	private int parentRotations;

	private float lastAngle;

	public bool debug { get; set; }

	private void Awake()
	{
	}

	private void InitGear()
	{
	}

	private void Update()
	{
	}

	public float GetPistonProgress()
	{
		return 0f;
	}

	public float GetAngle()
	{
		return 0f;
	}
}
