using System.Collections;
using UnityEngine;

public class TelescopePuzzle : PuzzleController
{
	[Space(10f)]
	[Header("Telescope Puzzle")]
	[Space(5f)]
	[SerializeField]
	private Animator moonsAnimator;

	[SerializeField]
	private float timeTillAnimationActivatin;

	public override void InteractWithPuzzle()
	{
		base.InteractWithPuzzle();
		StopAllCoroutines();
		StartCoroutine(IInteract());
	}

	public override void ReturnCamera()
	{
		if (isCameraZoomed)
		{
			base.ReturnCamera();
			StopAllCoroutines();
			moonsAnimator.Play("Idle");
		}
	}

	private IEnumerator IInteract()
	{
		yield return new WaitForSeconds(timeTillAnimationActivatin);
		moonsAnimator.Play("Show_Solution");
	}
}
