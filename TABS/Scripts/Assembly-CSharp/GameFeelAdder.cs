using UnityEngine;

public class GameFeelAdder : MonoBehaviour
{
	public float upShake;

	public float sideShake;

	public float forwardShake;

	public AnimationCurve cameraLookCurve;

	public Vector2 lookForce;

	public void DoGameFeel()
	{
		if (upShake != 0f || sideShake != 0f)
		{
			GameFeel.instance.AddShake(base.transform.up * upShake + base.transform.forward * forwardShake + Random.Range(-1f, 1f) * sideShake * base.transform.right, base.transform.position);
		}
		if (lookForce != Vector2.zero)
		{
			GameFeel.instance.AddLookCurve(cameraLookCurve, lookForce);
		}
	}
}
