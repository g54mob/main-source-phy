using UnityEngine;

public class AnimationSpeedMultiplier : MonoBehaviour
{
	public float speed = 1f;

	private void Start()
	{
		GetComponent<Animator>().speed = speed;
	}
}
