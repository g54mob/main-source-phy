using UnityEngine;

public class ChessPuzzle : PuzzleController
{
	[SerializeField]
	private Animator animator;

	public override void InteractWithPuzzle()
	{
		base.InteractWithPuzzle();
		animator.enabled = true;
		animator.Play("Show_Solutin");
	}

	public void PlayChessSound()
	{
		Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Puzzle_Chess, base.transform.position);
	}
}
