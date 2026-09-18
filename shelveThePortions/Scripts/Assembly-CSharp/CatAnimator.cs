using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class CatAnimator : MonoBehaviour
{
	[SerializeField]
	private Animator animator;

	[SerializeField]
	private AudioClipTypes meowAudioClipTypes;

	private AnimatorStateInfo currentAnimationStateInfo;

	[SerializeField]
	[ReadOnly]
	private AnimationClip currentAnimationClip;

	[SerializeField]
	[ReadOnly]
	private int currentFrame;

	[SerializeField]
	[ReadOnly]
	private float currentAnimationTime;

	[SerializeField]
	[ReadOnly]
	private int fullAnimationFrames;

	[Space]
	[SerializeField]
	private AnimationClip meowClip;

	[SerializeField]
	private List<AnimationClip> idleClips;

	[SerializeField]
	private List<AnimationClip> randomClips;

	private int endGameClipsIndex;

	[SerializeField]
	private List<AnimationClip> endGameClips;

	private bool isEndGame;

	private void Update()
	{
		UpdateCurrentAnimationSOLength();
		if (currentFrame >= fullAnimationFrames - 1)
		{
			FinishedAnimating();
		}
	}

	private void FinishedAnimating()
	{
		if (isEndGame)
		{
			animator.Play(endGameClips[endGameClipsIndex].name);
			endGameClipsIndex++;
			if (endGameClipsIndex == endGameClips.Count - 1)
			{
				isEndGame = false;
			}
		}
		else
		{
			AnimationClip animationClip = ((!(Random.value > 0.5f)) ? randomClips[Random.Range(0, randomClips.Count)] : idleClips[Random.Range(0, idleClips.Count)]);
			if (animationClip == meowClip)
			{
				Singleton<AudioManager>.Instance.PlayClip(meowAudioClipTypes, base.transform.position);
			}
			animator.Play(animationClip.name);
		}
	}

	public void PlayEndGameClips()
	{
		isEndGame = true;
		FinishedAnimating();
	}

	private void UpdateCurrentAnimationSOLength()
	{
		currentAnimationStateInfo = animator.GetCurrentAnimatorStateInfo(0);
		currentAnimationClip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
		currentAnimationTime = currentAnimationStateInfo.normalizedTime;
		currentFrame = Mathf.FloorToInt(currentAnimationTime * currentAnimationClip.length * currentAnimationClip.frameRate);
		fullAnimationFrames = Mathf.FloorToInt(currentAnimationClip.length * currentAnimationClip.frameRate);
	}
}
