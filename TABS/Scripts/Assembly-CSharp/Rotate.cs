using UnityEngine;

public class Rotate : MonoBehaviour
{
	public Vector3 rotation;

	public Space space;

	public bool unsacled;

	public float randomMultiplier;

	public float randomAddition;

	private Holdable holdable;

	private float cappedDeltaTime;

	private void Start()
	{
		holdable = GetComponentInParent<Holdable>();
		SetRandom();
	}

	private void Update()
	{
		cappedDeltaTime = (unsacled ? Time.unscaledDeltaTime : Time.deltaTime);
		cappedDeltaTime = Mathf.Clamp(cappedDeltaTime, 0f, 0.05f);
		if (!holdable || holdable.held)
		{
			base.transform.Rotate(rotation * cappedDeltaTime, space);
		}
	}

	public void SetRandom()
	{
		if (randomMultiplier > 0f)
		{
			rotation = new Vector3(rotation.x + Random.Range(0f - randomMultiplier, randomMultiplier), rotation.y + Random.Range(0f - randomMultiplier, randomMultiplier), rotation.z + Random.Range(0f - randomMultiplier, randomMultiplier));
		}
		if (randomAddition > 0f)
		{
			rotation *= 1f + Random.Range(0f - randomAddition, randomAddition);
		}
	}
}
