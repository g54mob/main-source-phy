using UnityEngine;

public class RandomCharacterStats : MonoBehaviour
{
	public float minStandingOffset;

	public float maxStandingOffset;

	public float minMovement = 1f;

	public float maxMovemenmt = 1f;

	public AnimationCurve randomCurve;

	private StandingHandler standing;

	private MovementHandler movement;

	private void Start()
	{
		standing = GetComponent<StandingHandler>();
		movement = GetComponent<MovementHandler>();
		float value = Random.value;
		value = randomCurve.Evaluate(value);
		standing.selfOffset = Mathf.Lerp(minStandingOffset, maxStandingOffset, value);
		movement.multiplier = Mathf.Lerp(minMovement, maxMovemenmt, value);
	}
}
