using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
	public string[] stateNames;

	public int currentState;

	public float multiplier = 1f;

	public bool randomRun = true;

	private void Start()
	{
		if (randomRun)
		{
			multiplier *= NormalRandom.GetRandom(0.5f, 1.5f);
		}
	}
}
