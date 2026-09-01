using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.Workshop;
using UnityEngine;

public class CavePortal : MonoBehaviour
{
	[SerializeField]
	private MapAsset m_unlockMap;

	[SerializeField]
	private TABSCampaignAsset m_unlockCampaign;

	private Transform m_cam;

	private bool m_portalOpen = true;

	private bool m_restrictInGameMode;

	private void Start()
	{
		m_restrictInGameMode = ServiceLocator.GetService<GameModeService>().IsGameModeRestricted();
	}

	public void Transistion()
	{
		if (!(m_unlockMap == null) && !m_restrictInGameMode)
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

	private void Update()
	{
		if (m_restrictInGameMode)
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
		float num = Vector3.Distance(base.transform.position, m_cam.position);
		if (m_portalOpen && num < 0.8f)
		{
			Transistion();
			m_portalOpen = false;
		}
	}
}
