using System.Collections;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.Workshop;
using UnityEngine;
using UnityEngine.Playables;

public class CaveAnimation : MonoBehaviour
{
	public PlayableDirector caveAnimation;

	public Animator[] animatorsToEnableOnAnimation;

	public Transform m_portal;

	[SerializeField]
	private MapAsset m_unlockMap;

	[SerializeField]
	private TABSCampaignAsset m_unlockCampaign;

	private bool portalOpen;

	private Transform m_cam;

	private bool hasAnimated;

	private bool restrictInGameMode;

	private float animateValue;

	private IEnumerator Start()
	{
		restrictInGameMode = ServiceLocator.GetService<GameModeService>().IsGameModeRestricted();
		for (int i = 0; i < animatorsToEnableOnAnimation.Length; i++)
		{
			animatorsToEnableOnAnimation[i].enabled = false;
		}
		caveAnimation.Play();
		yield return null;
		caveAnimation.Pause();
	}

	private void Update()
	{
		if (restrictInGameMode)
		{
			return;
		}
		if (m_cam == null)
		{
			if (MainCam.instance == null)
			{
				return;
			}
			m_cam = MainCam.instance.transform;
		}
		float num = Vector3.Distance(m_portal.position, m_cam.position);
		if (portalOpen)
		{
			if (num < 0.8f)
			{
				Transistion();
				portalOpen = false;
			}
		}
		else if (animateValue < 1f)
		{
			if (num < 6f)
			{
				animateValue += Time.deltaTime * 0.15f;
			}
			else if (animateValue > 0f)
			{
				animateValue -= Time.deltaTime * 0.15f;
			}
		}
		else if (!hasAnimated)
		{
			hasAnimated = true;
			Animate();
		}
	}

	public void Animate()
	{
		for (int i = 0; i < animatorsToEnableOnAnimation.Length; i++)
		{
			animatorsToEnableOnAnimation[i].enabled = true;
		}
		caveAnimation.Play();
		StartCoroutine(PauseAnimation());
	}

	public void Transistion()
	{
		if (m_unlockMap != null && !restrictInGameMode)
		{
			ISaveLoaderService service = ServiceLocator.GetService<ISaveLoaderService>();
			if (service.HasUnlockedSecret(m_unlockCampaign.Entity.UnlockKey))
			{
				ServiceLocator.GetService<GameModeService>().SetGameMode<SandboxGameMode>();
				CampaignPlayerDataHolder.StartedPlayingSandbox();
				TABSSceneManager.LoadMap(m_unlockMap);
			}
			else
			{
				service.UnlockSecret(m_unlockMap.Entity.UnlockKey);
				CampaignPlayerDataHolder.StartedPlayingNewCampaign(m_unlockCampaign, 0);
				TABSSceneManager.LoadCampaign();
			}
		}
	}

	public IEnumerator PauseAnimation()
	{
		yield return new WaitForSeconds(17f);
		portalOpen = true;
		yield return new WaitForSeconds(8f);
		caveAnimation.Pause();
		for (int i = 0; i < animatorsToEnableOnAnimation.Length; i++)
		{
			animatorsToEnableOnAnimation[i].enabled = false;
		}
	}
}
