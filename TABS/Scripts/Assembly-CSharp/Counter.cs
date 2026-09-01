using TFBGames;
using UnityEngine;

public class Counter : MonoBehaviour
{
	public float counter;

	public float cooldown = 5f;

	public float extraRandomCooldown;

	private void Update()
	{
		counter += Time.deltaTime / FixedTimeStepService.SmallForceCoefficient;
	}

	public bool IsOnCooldown()
	{
		return counter < cooldown;
	}

	public void ResetCounter()
	{
		counter = Random.Range(0f, 0f - extraRandomCooldown);
	}
}
