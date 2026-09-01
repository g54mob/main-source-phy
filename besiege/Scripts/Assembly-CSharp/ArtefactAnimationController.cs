using UnityEngine;

public class ArtefactAnimationController : MonoBehaviour
{
	public Animator animator;

	public AudioSource activate;

	private bool once = true;

	private void Update()
	{
		if (WinCondition.hasWon && once)
		{
			animator.SetBool("hasWon", true);
			activate.Play();
			once = false;
		}
	}
}
