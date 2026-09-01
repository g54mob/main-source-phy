using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PolyTwitchMain_Settings : MonoBehaviour
{
	[Header("Toggles")]
	public Toggle m_AllowSuggestionsToggle;

	public Toggle m_SubscribersOnlyToggle;

	public Toggle m_ModeratedToggle;

	public Toggle m_AutoPlayToggle;

	public Toggle m_AutoAdvanceToggle;

	public TextMeshProUGUI m_AutoAdvanceToggleText;

	public Image m_AutoAdvanceToggleCheckmark;

	public Toggle m_BitsEnabledToggle;

	public Toggle m_BitsMandatoryToggle;

	[Header("Sliders")]
	public Slider m_CooldownSlider;

	public TextMeshProUGUI m_CooldownPreview;

	private PointerEvents m_AllowSuggestionsTogglePointerEvents;

	private PointerEvents m_SubscribersOnlyTogglePointerEvents;

	private PointerEvents m_ModeratedTogglePointerEvents;

	private PointerEvents m_AutoPlayTogglePointerEvents;

	private PointerEvents m_AutoAdvanceTogglePointerEvents;

	private PointerEvents m_BitsEnabledTogglePointerEvents;

	private PointerEvents m_BitsMandatoryTogglePointerEvents;

	private bool m_SliderScrolling;

	private bool m_AutoAdvanceToggleRestore;

	private void Awake()
	{
		m_AllowSuggestionsTogglePointerEvents = m_AllowSuggestionsToggle.GetComponent<PointerEvents>();
		m_AllowSuggestionsTogglePointerEvents.RegisterOnClickedDelegate(OnAllowSuggestionsToggle);
		m_SubscribersOnlyTogglePointerEvents = m_SubscribersOnlyToggle.GetComponent<PointerEvents>();
		m_SubscribersOnlyTogglePointerEvents.RegisterOnClickedDelegate(OnSubscribersOnlyToggle);
		m_ModeratedTogglePointerEvents = m_ModeratedToggle.GetComponent<PointerEvents>();
		m_ModeratedTogglePointerEvents.RegisterOnClickedDelegate(OnModeratedToggle);
		m_AutoPlayTogglePointerEvents = m_AutoPlayToggle.GetComponent<PointerEvents>();
		m_AutoPlayTogglePointerEvents.RegisterOnClickedDelegate(OnAutoPlayToggle);
		m_AutoAdvanceTogglePointerEvents = m_AutoAdvanceToggle.GetComponent<PointerEvents>();
		m_AutoAdvanceTogglePointerEvents.RegisterOnClickedDelegate(OnAutoAdvanceToggle);
		m_BitsEnabledTogglePointerEvents = m_BitsEnabledToggle.GetComponent<PointerEvents>();
		m_BitsEnabledTogglePointerEvents.RegisterOnClickedDelegate(OnBitsEnabledToggle);
		m_BitsMandatoryTogglePointerEvents = m_BitsMandatoryToggle.GetComponent<PointerEvents>();
		m_BitsMandatoryTogglePointerEvents.RegisterOnClickedDelegate(OnBitsMandatoryToggle);
	}

	public void OnEnable()
	{
		m_AllowSuggestionsToggle.isOn = Profiles.m_ActiveProfile.m_TwitchAllowSuggestions;
		m_SubscribersOnlyToggle.isOn = Profiles.m_ActiveProfile.m_TwitchSuscribersOnly;
		m_ModeratedToggle.isOn = Profiles.m_ActiveProfile.m_TwitchModerated;
		m_AutoPlayToggle.isOn = Profiles.m_ActiveProfile.m_TwitchAutoPlay;
		m_AutoAdvanceToggle.enabled = true;
		m_AutoAdvanceToggle.isOn = Profiles.m_ActiveProfile.m_TwitchAutoAdvance;
		m_AutoAdvanceToggle.enabled = !m_AutoPlayToggle.isOn;
		m_BitsEnabledToggle.isOn = Profiles.m_ActiveProfile.m_TwitchBitsEnabled;
		m_BitsMandatoryToggle.enabled = true;
		m_BitsMandatoryToggle.isOn = Profiles.m_ActiveProfile.m_TwitchBitsMandatory;
		m_BitsMandatoryToggle.enabled = !m_BitsEnabledToggle.isOn;
		m_CooldownSlider.minValue = PolyTwitch.VIEWER_COOLDOWN_SECONDS_MIN;
		m_CooldownSlider.maxValue = PolyTwitch.VIEWER_COOLDOWN_SECONDS_MAX;
		m_CooldownSlider.value = Profiles.m_ActiveProfile.m_TwitchViewerCooldownSeconds;
		m_CooldownPreview.text = string.Format(Localize.Get("UI_POLYTWITCH_COOLDOWN"), Profiles.m_ActiveProfile.m_TwitchViewerCooldownSeconds);
		m_CooldownSlider.onValueChanged.AddListener(OnCooldownChanged);
		m_SliderScrolling = false;
		UpdateToggleDependencies();
	}

	public void OnDisable()
	{
		Profiles.SaveActiveProfile();
		m_SliderScrolling = false;
	}

	public void Update()
	{
		TrackSliderScrolling();
		UpdateToggleDependencies();
	}

	public bool IsSliderScrolling()
	{
		return m_SliderScrolling;
	}

	private void OnAllowSuggestionsToggle()
	{
		Profiles.m_ActiveProfile.m_TwitchAllowSuggestions = m_AllowSuggestionsToggle.isOn;
		InterfaceAudio.Play("ui_settings_toggle");
	}

	private void OnSubscribersOnlyToggle()
	{
		Profiles.m_ActiveProfile.m_TwitchSuscribersOnly = m_SubscribersOnlyToggle.isOn;
		InterfaceAudio.Play("ui_settings_toggle");
	}

	private void OnModeratedToggle()
	{
		Profiles.m_ActiveProfile.m_TwitchModerated = m_ModeratedToggle.isOn;
		InterfaceAudio.Play("ui_settings_toggle");
	}

	private void OnAutoPlayToggle()
	{
		Profiles.m_ActiveProfile.m_TwitchAutoPlay = m_AutoPlayToggle.isOn;
		Profiles.SaveActiveProfile();
		InterfaceAudio.Play("ui_settings_toggle");
	}

	private void OnAutoAdvanceToggle()
	{
		Profiles.m_ActiveProfile.m_TwitchAutoAdvance = m_AutoAdvanceToggle.isOn;
		InterfaceAudio.Play("ui_settings_toggle");
	}

	private void OnBitsEnabledToggle()
	{
		Profiles.m_ActiveProfile.m_TwitchBitsEnabled = m_BitsEnabledToggle.isOn;
		InterfaceAudio.Play("ui_settings_toggle");
	}

	private void OnBitsMandatoryToggle()
	{
		Profiles.m_ActiveProfile.m_TwitchBitsMandatory = m_BitsMandatoryToggle.isOn;
		InterfaceAudio.Play("ui_settings_toggle");
	}

	private void OnCooldownChanged(float value)
	{
		Profiles.m_ActiveProfile.m_TwitchViewerCooldownSeconds = Mathf.Clamp(Mathf.RoundToInt(m_CooldownSlider.value), PolyTwitch.VIEWER_COOLDOWN_SECONDS_MIN, PolyTwitch.VIEWER_COOLDOWN_SECONDS_MAX);
		m_CooldownPreview.text = string.Format(Localize.Get("UI_POLYTWITCH_COOLDOWN"), Profiles.m_ActiveProfile.m_TwitchViewerCooldownSeconds);
	}

	private void TrackSliderScrolling()
	{
		if (GameInput.GetMouseButtonJustPressed(0) && GameUI.PointerOver(typeof(Slider)))
		{
			m_SliderScrolling = true;
		}
		if (GameInput.GetMouseButtonJustReleased(0))
		{
			m_SliderScrolling = false;
		}
	}

	private void UpdateToggleDependencies()
	{
		if (m_AutoPlayToggle.isOn != m_AutoAdvanceToggle.enabled)
		{
			m_AutoAdvanceToggle.enabled = m_AutoPlayToggle.isOn;
			Image[] componentsInChildren = m_AutoAdvanceToggle.transform.parent.GetComponentsInChildren<Image>();
			foreach (Image obj in componentsInChildren)
			{
				Color color = obj.color;
				color.a = (m_AutoPlayToggle.isOn ? 1f : 0.5f);
				obj.color = color;
			}
			TextMeshProUGUI[] componentsInChildren2 = m_AutoAdvanceToggle.transform.parent.GetComponentsInChildren<TextMeshProUGUI>();
			foreach (TextMeshProUGUI obj2 in componentsInChildren2)
			{
				Color color2 = obj2.color;
				color2.a = (m_AutoPlayToggle.isOn ? 1f : 0.5f);
				obj2.color = color2;
			}
		}
		if (m_BitsEnabledToggle.isOn != m_BitsMandatoryToggle.enabled)
		{
			m_BitsMandatoryToggle.enabled = m_BitsEnabledToggle.isOn;
			Image[] componentsInChildren = m_BitsMandatoryToggle.transform.parent.GetComponentsInChildren<Image>();
			foreach (Image obj3 in componentsInChildren)
			{
				Color color3 = obj3.color;
				color3.a = (m_BitsEnabledToggle.isOn ? 1f : 0.5f);
				obj3.color = color3;
			}
			TextMeshProUGUI[] componentsInChildren2 = m_BitsMandatoryToggle.transform.parent.GetComponentsInChildren<TextMeshProUGUI>();
			foreach (TextMeshProUGUI obj4 in componentsInChildren2)
			{
				Color color4 = obj4.color;
				color4.a = (m_BitsEnabledToggle.isOn ? 1f : 0.5f);
				obj4.color = color4;
			}
		}
		if (m_BitsMandatoryToggle.transform.parent.gameObject.activeInHierarchy != PolyTwitch.CanUseBits())
		{
			m_BitsMandatoryToggle.transform.parent.gameObject.SetActive(PolyTwitch.CanUseBits());
		}
	}
}
