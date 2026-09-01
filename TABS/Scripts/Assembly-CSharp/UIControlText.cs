using System.Collections.ObjectModel;
using System.Text;
using InControl;
using Landfall.TABS.GameMode;
using Landfall.TABS_Input;
using TFBG;
using TMPro;
using UnityEngine;

public class UIControlText : MonoBehaviour, IDisruptionServiceSubscriber
{
	[SerializeField]
	private string m_playerActionKey;

	private TextMeshProUGUI m_text;

	private LocalizeText m_localizeText;

	private string m_originalText;

	private PlayerAction m_playerAction;

	private GlyphService m_GlyphService;

	private InputType m_currentInputType;

	private StringBuilder bindingName;

	private GameModeService m_gameModeService;

	private GameDisruptionService m_GameDisruptionService;

	private CountdownTimerService m_countdownTimerService;

	private bool liveText;

	private const string ProjectMarsCountdownLocalizationKey = "MP_LABEL_NEW_MATCH_IN";

	private void Start()
	{
		m_gameModeService = ServiceLocator.GetService<GameModeService>();
		m_GameDisruptionService = ServiceLocator.GetService<GameDisruptionService>();
		m_countdownTimerService = ServiceLocator.GetService<CountdownTimerService>();
		m_GlyphService = ServiceLocator.GetService<GlyphService>();
		bindingName = new StringBuilder();
		m_text = GetComponent<TextMeshProUGUI>();
		if (m_text == null)
		{
			Debug.LogError("No texmex");
			Object.Destroy(this);
			return;
		}
		m_localizeText = GetComponent<LocalizeText>();
		if (m_localizeText != null)
		{
			if (m_gameModeService != null && m_gameModeService.CurrentGameMode is OnlineMultiplayerGameMode)
			{
				m_localizeText.LocaleID = "MP_LABEL_NEW_MATCH_IN";
			}
			m_originalText = Localizer.GetSinglePhrase(m_localizeText.LocaleID);
		}
		else
		{
			m_originalText = m_text.text;
		}
		m_playerAction = PlayerActions.Instance.GetPlayerActionByName(m_playerActionKey);
		m_playerAction.OnBindingsChanged += UpdateBindingText;
		PlayerActions.Instance.OnLastInputTypeChanged += OnInputChange;
		m_currentInputType = PlayerActions.Instance.InputType;
		UpdateBindingText();
	}

	private void Update()
	{
		if (m_countdownTimerService.IsCountingDown)
		{
			if ((bool)m_localizeText)
			{
				m_localizeText.Args = new string[1] { $"{m_countdownTimerService.TimeLeft:0}" };
				m_localizeText.LocaleID = m_localizeText.LocaleID;
			}
			else
			{
				m_text.text = $"NEW MATCH IN {m_countdownTimerService.TimeLeft:0}";
			}
		}
	}

	public void Subscribe()
	{
		liveText = true;
		m_countdownTimerService.OnCounterEnded += Unsubscribe;
		m_GameDisruptionService.AddWatcher(m_countdownTimerService, this);
	}

	public void Unsubscribe()
	{
		liveText = false;
		m_countdownTimerService.OnCounterEnded -= Unsubscribe;
	}

	private void OnDestroy()
	{
		Unsubscribe();
		m_playerAction.OnBindingsChanged -= UpdateBindingText;
		PlayerActions.Instance.OnLastInputTypeChanged -= OnInputChange;
	}

	private void OnDisable()
	{
		Unsubscribe();
	}

	private void OnInputChange(BindingSourceType bindingSourceType)
	{
		m_currentInputType = PlayerActions.Instance.GetInputType(bindingSourceType);
		UpdateBindingText();
	}

	private void UpdateBindingText()
	{
		if (m_countdownTimerService != null && m_gameModeService != null && m_gameModeService.IsCurrentBaseGameModeType<OnlineMultiplayerGameMode>())
		{
			Subscribe();
			return;
		}
		bindingName.Clear();
		ReadOnlyCollection<BindingSource> bindings = m_playerAction.Bindings;
		int num = 0;
		for (int i = 0; i < bindings.Count; i++)
		{
			BindingSource bindingSource = bindings[i];
			InputType inputType = PlayerActions.Instance.GetInputType(bindingSource.BindingSourceType);
			if (m_currentInputType == InputType.Keyboard && inputType == InputType.Keyboard)
			{
				if (num > 0)
				{
					bindingName.Append(" or ");
				}
				bindingName.Append(m_GlyphService.GetBindingsGlyph(bindingSource, inputType, PlayerActions.Instance.LastDeviceStyle).ToUpper());
				num++;
			}
			else if (m_currentInputType == InputType.Controller && inputType == InputType.Controller)
			{
				if (num > 0)
				{
					bindingName.Append(" or ");
				}
				bindingName.Append(m_GlyphService.GetBindingsGlyph(bindingSource, inputType, PlayerActions.Instance.LastDeviceStyle));
				num++;
			}
		}
		if (!m_localizeText)
		{
			m_text.text = string.Format(m_originalText, bindingName);
			return;
		}
		m_localizeText.Args = new string[1] { bindingName.ToString() };
		m_localizeText.LocaleID = m_localizeText.LocaleID;
	}
}
