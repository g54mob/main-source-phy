using TMPro;
using UnityEngine;

public class Panel_CampaignTutorial : MonoBehaviour
{
	[Header("Screen Elements")]
	public GameObject m_SelectSim;

	public GameObject m_SelectPhaseD;

	public GameObject m_ClickHydraulic;

	public GameObject m_ClickAnchor;

	public GameObject m_IndicateAnchor1;

	public GameObject m_IndicateAnchor2;

	public GameObject m_IndicateStartHydro1;

	public GameObject m_IndicateStartHydro2;

	public GameObject m_SplitJoint;

	[Header("Misc")]
	public RectTransform m_SelectAreaUITransform;

	public RectTransform m_MoveJointUITransform;

	public RectTransform m_BottomPanelRectTransform;

	public TweenPosition m_BottomPanelTween;

	public TextMeshProUGUI m_BottmPanelText;

	public Sprite m_SliderArrowSprite;

	public Sprite m_RotationIndicatorSprite;

	public RectTransform m_SplitJointRectTransform;

	public TweenPosition m_SplitJointTween;

	private CampaignTutorialStage m_CurrentStage;

	private bool m_BottomPanelShowing;

	private readonly string SIZE22 = "<size=20>";

	private readonly string SIZE18 = "<size=18>";

	private void OnEnable()
	{
		m_SelectSim.SetActive(value: false);
		m_SelectPhaseD.SetActive(value: false);
		m_ClickHydraulic.SetActive(value: false);
		m_ClickAnchor.SetActive(value: false);
		m_IndicateAnchor1.SetActive(value: false);
		m_IndicateAnchor2.SetActive(value: false);
		m_IndicateStartHydro1.SetActive(value: false);
		m_IndicateStartHydro2.SetActive(value: false);
		m_SplitJoint.SetActive(value: false);
		m_BottomPanelTween.Reset();
	}

	private void OnDisable()
	{
		CampaignTutorial.OnDisable();
		m_BottomPanelShowing = false;
	}

	public void Update()
	{
		UpdateBottmPanelText();
	}

	public void UpdateBasicActiveStage(CampaignTutorialStage currentStage)
	{
		m_CurrentStage = currentStage;
		if (m_BottomPanelShowing && ShouldHideBottomPanel(currentStage))
		{
			m_BottomPanelTween.PlayReverse();
			m_BottomPanelShowing = false;
		}
		else if (CampaignTutorial.IsFirstStage(currentStage) || ShouldHideBottomPanel(currentStage - 1))
		{
			m_BottomPanelTween.Play();
			m_BottomPanelShowing = true;
		}
		if (m_BottomPanelShowing)
		{
			UpdateBottmPanelText();
		}
	}

	public void UpdateBottmPanelText()
	{
		if (ShouldHideBottomPanel(m_CurrentStage))
		{
			m_BottmPanelText.text = string.Empty;
			return;
		}
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			m_BottmPanelText.text = Localize.Get($"TUTORIAL_{m_CurrentStage}_GAMEPAD");
		}
		else
		{
			m_BottmPanelText.text = Localize.Get($"TUTORIAL_{m_CurrentStage}");
		}
		m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"right-stick\">", SIZE22 + "<sprite name=\"steamdeck_stick_r\">" + SIZE18);
		m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"left-stick\">", SIZE22 + "<sprite name=\"steamdeck_stick_l\">" + SIZE18);
		switch (GamepadManager.GetGamepadType())
		{
		case GamepadType.STEAMDECK:
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-dpadup\">", SIZE22 + "<sprite name=\"steamdeck_dpad_up_outline\">" + SIZE18);
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-north\">", SIZE22 + "<sprite name=\"steamdeck_button_y\">" + SIZE18);
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-south\">", SIZE22 + "<sprite name=\"steamdeck_button_a\">" + SIZE18);
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-east\">", SIZE22 + "<sprite name=\"steamdeck_button_b\">" + SIZE18);
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-start\">", SIZE22 + "<sprite name=\"steamdeck_button_options\">" + SIZE18);
			break;
		case GamepadType.PLAYSTATION:
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-dpadup\">", SIZE22 + "<sprite name=\"playstation_dpad_up_outline\">" + SIZE18);
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-north\">", SIZE22 + "<sprite name=\"playstation_button_triangle\">" + SIZE18);
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-south\">", SIZE22 + "<sprite name=\"playstation_button_cross\">" + SIZE18);
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-east\">", SIZE22 + "<sprite name=\"playstation_button_circle\">" + SIZE18);
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-start\">", SIZE22 + "<sprite name=\"playstation5_button_options\">" + SIZE18);
			break;
		case GamepadType.XBOX:
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-dpadup\">", SIZE22 + "<sprite name=\"xbox_dpad_up_outline\">" + SIZE18);
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-north\">", SIZE22 + "<sprite name=\"xbox_button_color_y\">" + SIZE18);
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-south\">", SIZE22 + "<sprite name=\"xbox_button_color_a\">" + SIZE18);
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-east\">", SIZE22 + "<sprite name=\"xbox_button_color_b\">" + SIZE18);
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-start\">", SIZE22 + "<sprite name=\"xbox_button_menu\">" + SIZE18);
			break;
		case GamepadType.SWITCH:
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-dpadup\">", SIZE22 + "<sprite name=\"switch_dpad_up_outline\">" + SIZE18);
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-north\">", SIZE22 + "<sprite name=\"switch_button_y\">" + SIZE18);
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-south\">", SIZE22 + "<sprite name=\"switch_button_a\">" + SIZE18);
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-east\">", SIZE22 + "<sprite name=\"switch_button_b\">" + SIZE18);
			m_BottmPanelText.text = m_BottmPanelText.text.Replace("<sprite name=\"button-start\">", SIZE22 + "<sprite name=\"switch_button_plus\">" + SIZE18);
			break;
		default:
			Debug.LogWarning($"Unexpected platform in UpdateBottomPanelText: {GamepadManager.GetGamepadType()}");
			break;
		}
	}

	private bool ShouldHideBottomPanel(CampaignTutorialStage stage)
	{
		switch (stage)
		{
		case CampaignTutorialStage.INVALID:
		case CampaignTutorialStage.UI_END:
		case CampaignTutorialStage.HYDRO_END:
		case CampaignTutorialStage.HYDRAULICS_CONTROLLER_WAIT_FOR_PHASEA:
		case CampaignTutorialStage.HYDRAULICS_CONTROLLER_WAIT_FOR_PHASED:
		case CampaignTutorialStage.HYDRAULICS_CONTROLLER_WAIT_FOR_FAILURE:
		case CampaignTutorialStage.HYDRAULICS_CONTROLLER_WAIT_FOR_NOTICE:
		case CampaignTutorialStage.HYDRAULICS_CONTROLLER_END:
			return true;
		default:
			return false;
		}
	}

	public void PauseTweens()
	{
		TweenPosition[] componentsInChildren = GetComponentsInChildren<TweenPosition>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			iTween.Pause(componentsInChildren[i].gameObject);
		}
		CampaignTutorial.PauseTweens();
	}

	public void ResumeTweens()
	{
		TweenPosition[] componentsInChildren = GetComponentsInChildren<TweenPosition>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			iTween.Resume(componentsInChildren[i].gameObject);
		}
		CampaignTutorial.ResumeTweens();
	}

	public void PositionClickHydrulicIndicator(Vector3 worldPos)
	{
		Vector2 v = Cameras.MainCamera().WorldToScreenPoint(worldPos);
		m_ClickHydraulic.transform.position = Utils.V2toV3(v);
	}

	public bool BottomPanelIsShowing()
	{
		return m_BottomPanelShowing;
	}
}
