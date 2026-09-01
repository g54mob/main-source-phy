using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

public class CampaignThanksHandler : UIComponentMainMenu
{
	[SerializeField]
	private LocalizeText m_title;

	[SerializeField]
	private LocalizeText m_description;

	[SerializeField]
	private LocalizeText m_text;

	[SerializeField]
	private PostLerper m_PostEffects;

	[SerializeField]
	private FadeLerper m_FadeEffects;

	public void SetCampaignInfoText(CampaignInfo campaignInfo)
	{
		m_title.LocaleID = campaignInfo.ThankYouTitle;
		m_description.LocaleID = campaignInfo.Description;
		m_text.LocaleID = campaignInfo.ThankYouText;
	}

	public void GoBack()
	{
		base.Close();
	}

	protected override void Update()
	{
		base.Update();
		if (PlayerActions.Instance.m_back.WasPressed && base.IsActive)
		{
			base.Close();
		}
	}

	protected override void OnOpen()
	{
		base.OnOpen();
		DoBackgroundBlur();
		CursorVisibilityController service = ServiceLocator.GetService<CursorVisibilityController>();
		if (service != null)
		{
			service.SetLockState(CursorLockMode.None);
			service.SetVisibility(visible: true);
		}
	}

	private void DoBackgroundBlur()
	{
		if (m_PostEffects == null)
		{
			m_PostEffects = Object.FindObjectOfType<PostLerper>();
		}
		if (m_PostEffects != null && m_FadeEffects != null)
		{
			m_FadeEffects.fadeValue = 1f;
			m_PostEffects.dofAmount = 1f;
		}
		else
		{
			Debug.LogError("Menu blur or fade reference missing");
		}
	}
}
