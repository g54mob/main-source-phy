using UnityEngine;

public class PlayTrailAnimation : MonoBehaviour
{
	public string animatorState;

	public ParticleSystem particleSystem;

	private Animator animator;

	private bool isAnimating;

	private AudioSource source;

	public bool endStuff;

	public float endTime = 1f;

	public GameObject enableOnEnd;

	public GameObject disableOnEnd;

	private void Start()
	{
		particleSystem = GetComponent<ParticleSystem>();
		animator = GetComponent<Animator>();
		source = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (particleSystem.time > 0f && !isAnimating)
		{
			source.Play();
			animator.Play(animatorState);
			isAnimating = true;
		}
		if (endStuff && isAnimating)
		{
			if (endTime <= 0f)
			{
				enableOnEnd.SetActive(true);
				disableOnEnd.SetActive(false);
				endStuff = false;
			}
			endTime -= Time.deltaTime;
		}
	}
}
