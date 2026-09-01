using UnityEngine;

public class ArtefactController : MonoBehaviour
{
	public Animator animator;

	public AudioSource animationStartSfx;

	public Animation animation;

	public AnimationClip clip;

	private void Start()
	{
		animator = GetComponent<Animator>();
		animation = GetComponent<Animation>();
		if (!animationStartSfx)
		{
			animationStartSfx = GetComponent<AudioSource>();
		}
	}

	private void Update()
	{
	}

	public void playAnimation()
	{
		animation.Play(clip.name);
		if ((bool)animationStartSfx)
		{
			animationStartSfx.Play();
		}
	}
}
