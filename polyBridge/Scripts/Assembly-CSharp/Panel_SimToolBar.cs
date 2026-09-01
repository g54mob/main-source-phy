using UnityEngine;
using UnityEngine.UI;

public class Panel_SimToolBar : MonoBehaviour
{
	[Header("Buttons")]
	public Button m_StressButton;

	public Button m_StressSelectedButton;

	public Button m_PauseOnBreakButton;

	public Button m_PauseOnBreakSelectedButton;

	public Button m_RightView;

	public Button m_LeftView;

	public Button m_CenterPitchedDownView;

	public Button m_CenterView;

	public Button m_FollowCarView;

	[Header("View Icons")]
	public Image m_RightViewIcon;

	public Image m_LeftViewIcon;

	public Image m_CenterPitchedDownViewIcon;

	public Image m_CenterViewIcon;

	public Image m_FollowCarViewIcon;

	[Header("View Icons")]
	public ToolTipText m_FollowCarTooltipText;

	private PointOfViewType m_Last3DPointOfViews = PointOfViewType.SIM_RIGHT;

	public void Start()
	{
		m_StressButton.onClick.AddListener(OnStress);
		m_StressSelectedButton.onClick.AddListener(OnStressSelected);
		m_PauseOnBreakButton.onClick.AddListener(OnPauseOnBreak);
		m_PauseOnBreakSelectedButton.onClick.AddListener(OnPauseOnBreakSelected);
		m_RightView.onClick.AddListener(OnRightView);
		m_LeftView.onClick.AddListener(OnLeftView);
		m_CenterPitchedDownView.onClick.AddListener(OnCenterPitchedDownView);
		m_CenterView.onClick.AddListener(OnCenterView);
		m_FollowCarView.onClick.AddListener(OnFollowCarView);
	}

	public void OnEnable()
	{
		m_RightView.interactable = IsInValidGameState();
		m_LeftView.interactable = IsInValidGameState();
		m_CenterPitchedDownView.interactable = IsInValidGameState();
		UpdateFollowCarIcon();
	}

	public void UpdateFollowCarIcon()
	{
		m_FollowCarViewIcon.color = ((Profiles.m_ActiveProfile != null && Profiles.m_ActiveProfile.m_FollowCar) ? GameUI.m_Instance.m_GoldColor : Color.white);
	}

	public void HighlightPointOfView(PointOfViewType pointOfViewType)
	{
		m_RightViewIcon.color = ((pointOfViewType == PointOfViewType.SIM_RIGHT) ? GameUI.m_Instance.m_GoldColor : Color.white);
		m_LeftViewIcon.color = ((pointOfViewType == PointOfViewType.SIM_LEFT) ? GameUI.m_Instance.m_GoldColor : Color.white);
		m_CenterPitchedDownViewIcon.color = ((pointOfViewType == PointOfViewType.SIM_CENTER_PITCHED_DOWN) ? GameUI.m_Instance.m_GoldColor : Color.white);
		m_CenterViewIcon.color = ((pointOfViewType == PointOfViewType.SIM_CENTER) ? GameUI.m_Instance.m_GoldColor : Color.white);
	}

	public void OnStress()
	{
		OnStressSilent();
		InterfaceAudio.Play("ui_menubar_gen_on");
	}

	public void OnStressSilent()
	{
		m_StressButton.gameObject.SetActive(value: false);
		m_StressSelectedButton.gameObject.SetActive(value: true);
		Profiles.m_ActiveProfile.m_StressViewEnabled = true;
		Profiles.SaveActiveProfile();
	}

	public void OnStressSelected()
	{
		OnStressSelectedSilent();
		InterfaceAudio.Play("ui_menubar_gen_off");
	}

	public void OnStressSelectedSilent()
	{
		m_StressButton.gameObject.SetActive(value: true);
		m_StressSelectedButton.gameObject.SetActive(value: false);
		Profiles.m_ActiveProfile.m_StressViewEnabled = false;
		Profiles.SaveActiveProfile();
		BridgeEdges.SetDefaultColors();
	}

	public void OnPauseOnBreak()
	{
		OnPauseOnBreakSilent();
		InterfaceAudio.Play("ui_menubar_gen_on");
	}

	public void OnPauseOnBreakSilent()
	{
		m_PauseOnBreakButton.gameObject.SetActive(value: false);
		m_PauseOnBreakSelectedButton.gameObject.SetActive(value: true);
		Profiles.m_ActiveProfile.m_PauseOnBreak = true;
		Profiles.SaveActiveProfile();
	}

	public void OnPauseOnBreakSelected()
	{
		OnPauseOnBreakSelectedSilent();
		InterfaceAudio.Play("ui_menubar_gen_off");
	}

	public void OnPauseOnBreakSelectedSilent()
	{
		m_PauseOnBreakButton.gameObject.SetActive(value: true);
		m_PauseOnBreakSelectedButton.gameObject.SetActive(value: false);
		Profiles.m_ActiveProfile.m_PauseOnBreak = false;
		Profiles.SaveActiveProfile();
	}

	public void OnRightView()
	{
		if (Profiles.m_ActiveProfile.m_LockBuildCamera)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else if (IsInValidGameState())
		{
			StopFollowingVehicle();
			PointsOfView.m_PointsOfView[PointOfViewType.SIM_RIGHT].FrameObjects(Game.GetLevelId());
			PointsOfView.RotateTo(PointOfViewType.SIM_RIGHT, GameSettings.TransitionTimeSeconds());
			Profiles.m_ActiveProfile.m_PointOfViewType = PointOfViewType.SIM_RIGHT;
			Profiles.SaveActiveProfile();
			HighlightPointOfView(PointOfViewType.SIM_RIGHT);
			m_Last3DPointOfViews = PointOfViewType.SIM_RIGHT;
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	public void OnLeftView()
	{
		if (Profiles.m_ActiveProfile.m_LockBuildCamera)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else if (IsInValidGameState())
		{
			StopFollowingVehicle();
			PointsOfView.m_PointsOfView[PointOfViewType.SIM_LEFT].FrameObjects(Game.GetLevelId());
			PointsOfView.RotateTo(PointOfViewType.SIM_LEFT, GameSettings.TransitionTimeSeconds());
			Profiles.m_ActiveProfile.m_PointOfViewType = PointOfViewType.SIM_LEFT;
			Profiles.SaveActiveProfile();
			HighlightPointOfView(PointOfViewType.SIM_LEFT);
			m_Last3DPointOfViews = PointOfViewType.SIM_LEFT;
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	public void OnCenterPitchedDownView()
	{
		if (Profiles.m_ActiveProfile.m_LockBuildCamera)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else if (IsInValidGameState())
		{
			StopFollowingVehicle();
			PointsOfView.m_PointsOfView[PointOfViewType.SIM_CENTER_PITCHED_DOWN].FrameObjects(Game.GetLevelId());
			PointsOfView.RotateTo(PointOfViewType.SIM_CENTER_PITCHED_DOWN, GameSettings.TransitionTimeSeconds());
			Profiles.m_ActiveProfile.m_PointOfViewType = PointOfViewType.SIM_CENTER_PITCHED_DOWN;
			Profiles.SaveActiveProfile();
			HighlightPointOfView(PointOfViewType.SIM_CENTER_PITCHED_DOWN);
			m_Last3DPointOfViews = PointOfViewType.SIM_CENTER_PITCHED_DOWN;
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	public void OnCenterView()
	{
		if (Profiles.m_ActiveProfile.m_LockBuildCamera)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else if (IsInValidGameState())
		{
			StopFollowingVehicle();
			if (PointsOfView.m_Locked2D && Profiles.m_ActiveProfile.m_PointOfViewType == PointOfViewType.SIM_CENTER)
			{
				SwitchToLast3DPointOfView();
			}
			else
			{
				PointsOfView.m_PointsOfView[PointOfViewType.SIM_CENTER].FrameObjects(Game.GetLevelId());
				PointsOfView.RotateTo(PointOfViewType.SIM_CENTER, GameSettings.TransitionTimeSeconds());
				Profiles.m_ActiveProfile.m_PointOfViewType = PointOfViewType.SIM_CENTER;
				Profiles.SaveActiveProfile();
				HighlightPointOfView(PointOfViewType.SIM_CENTER);
			}
			InterfaceAudio.Play("ui_menu_select");
		}
		else
		{
			PointsOfView.m_PointsOfView[PointOfViewType.BUILD].FrameObjects(Game.GetLevelId());
			if (Game.IsCurrentLevelTutorial())
			{
				PointsOfView.m_PointsOfView[PointOfViewType.BUILD].m_OrthographicsSize = GameSettings.TutorialOrthographicSize();
			}
			PointsOfView.RotateTo(PointOfViewType.BUILD, 0f);
		}
	}

	public void EnableForSim()
	{
		base.gameObject.SetActive(!GameUI.m_DisableHud);
		m_StressButton.gameObject.SetActive(!Profiles.m_ActiveProfile.m_StressViewEnabled);
		m_StressSelectedButton.gameObject.SetActive(Profiles.m_ActiveProfile.m_StressViewEnabled);
		m_PauseOnBreakButton.gameObject.SetActive(!Profiles.m_ActiveProfile.m_PauseOnBreak);
		m_PauseOnBreakSelectedButton.gameObject.SetActive(Profiles.m_ActiveProfile.m_PauseOnBreak);
	}

	private void SwitchToLast3DPointOfView()
	{
		switch (m_Last3DPointOfViews)
		{
		case PointOfViewType.SIM_RIGHT:
			OnRightView();
			break;
		case PointOfViewType.SIM_LEFT:
			OnLeftView();
			break;
		case PointOfViewType.SIM_CENTER_PITCHED_DOWN:
			OnCenterPitchedDownView();
			break;
		}
	}

	public void OnFollowCarView()
	{
		if (Game.IsCurrentLevelTutorial())
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		VehicleFollow.Toggle();
		Profiles.SaveActiveProfile();
		UpdateFollowCarIcon();
		if (Profiles.m_ActiveProfile.m_FollowCar && !Profiles.m_ActiveProfile.m_LockBuildCamera)
		{
			GameUI.m_Instance.m_SimToolBar.HighlightPointOfView(PointOfViewType.SIM_CUSTOM);
		}
		InterfaceAudio.Play("ui_menu_select");
	}

	public void OnCycleView()
	{
		if (Profiles.m_ActiveProfile.m_PointOfViewType == PointOfViewType.SIM_RIGHT)
		{
			OnLeftView();
		}
		else if (Profiles.m_ActiveProfile.m_PointOfViewType == PointOfViewType.SIM_LEFT)
		{
			OnCenterPitchedDownView();
		}
		else if (Profiles.m_ActiveProfile.m_PointOfViewType == PointOfViewType.SIM_CENTER_PITCHED_DOWN)
		{
			OnCenterView();
		}
		else
		{
			OnRightView();
		}
	}

	private bool IsInValidGameState()
	{
		if (GameStateManager.GetState() != GameState.SIM)
		{
			return GameStateManager.GetState() == GameState.PHOTO;
		}
		return true;
	}

	private void StopFollowingVehicle()
	{
		if (Profiles.m_ActiveProfile.m_FollowCar)
		{
			Profiles.m_ActiveProfile.m_FollowCar = false;
			UpdateFollowCarIcon();
		}
	}
}
