using UnityEngine;

public class GameFeel : MonoBehaviour
{
	public static GameFeel instance;

	public RotationShake rotationShake;

	private void Awake()
	{
		instance = this;
	}

	public void AddShake(Vector3 force, Vector3 pos, float range = 0f)
	{
		if (range == 0f)
		{
			range = force.magnitude * 10f;
		}
		rotationShake.AddForce(force, pos, range);
	}

	public void AddShakeOverTime(Vector3 force, Vector3 pos, float time, float range = 0f)
	{
		if (range == 0f)
		{
			range = force.magnitude * 10f;
		}
		rotationShake.ShakeOverTime(force, pos, time, range);
	}

	public void AddLookCurve(AnimationCurve curve, Vector2 lookForce)
	{
	}
}
