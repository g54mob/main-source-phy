using System.Collections;
using Landfall.TABS.GameMode;
using UnityEngine;
using UnityEngine.Playables;

public class CaveDoorAnimation : MonoBehaviour
{
	public PlayableDirector animation;

	public Animator[] animatorsToEnableOnAnimation;

	public float animationTime = 10f;

	private bool restrictInGameMode;

	public void Animate()
	{
		animation.Play();
		StartCoroutine(PauseAnimation());
	}

	private IEnumerator Start()
	{
		restrictInGameMode = ServiceLocator.GetService<GameModeService>().IsGameModeRestricted();
		if (!restrictInGameMode)
		{
			for (int i = 0; i < animatorsToEnableOnAnimation.Length; i++)
			{
				animatorsToEnableOnAnimation[i].enabled = false;
			}
			animation.Play();
			yield return null;
			animation.Pause();
		}
	}

	private void Update()
	{
		_ = restrictInGameMode;
	}

	public IEnumerator PauseAnimation()
	{
		yield return new WaitForSeconds(animationTime);
		animation.Pause();
		for (int i = 0; i < animatorsToEnableOnAnimation.Length; i++)
		{
			animatorsToEnableOnAnimation[i].enabled = false;
		}
	}
}
