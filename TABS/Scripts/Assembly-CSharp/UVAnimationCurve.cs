using UnityEngine;

public class UVAnimationCurve : MonoBehaviour
{
	public AnimationCurve curve;

	public bool X;

	public bool Y;

	public float speed = 1f;

	public float randomSpeed;

	public float amount = 1f;

	public float randomAmount;

	private UVAnimation anim;

	private void Start()
	{
		anim = GetComponent<UVAnimation>();
		SetRandoms();
	}

	private void Update()
	{
		if (X)
		{
			anim.offsetPerFrame.x = curve.Evaluate(Time.time * speed % 1f) * amount;
		}
		if (Y)
		{
			anim.offsetPerFrame.y = curve.Evaluate(Time.time * speed % 1f) * amount;
		}
	}

	private void SetRandoms()
	{
		if (randomSpeed > 0f)
		{
			speed += Random.Range(0f - randomSpeed, randomSpeed);
		}
		if (randomAmount > 0f)
		{
			amount += Random.Range(0f - randomAmount, randomAmount);
		}
	}
}
