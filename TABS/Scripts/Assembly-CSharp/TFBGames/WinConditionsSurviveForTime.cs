using Landfall.TABS.UI.Widgets.Fields;
using Landfall.TABS_Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	public class WinConditionsSurviveForTime : MonoBehaviour
	{
		[SerializeField]
		private Button m_IncreaseTimeButton;

		[SerializeField]
		private Button m_DecreaseTimeButton;

		[SerializeField]
		private TextMeshProUGUI m_DisplayedTime;

		[SerializeField]
		private int m_TimeIncrimentValue = 5;

		[SerializeField]
		private UIPropertyField m_PropertyField;

		private const int INCREASE = 1;

		private const int DECREASE = -1;

		private const int MINIMUM_TIME = 5;

		private const int MAXIMUM_TIME = 3600;

		private const float HOLD_TIME = 1f;

		private const float HOLD_INTERVAL = 0.1f;

		private const string ZERO = "0";

		private int m_SetTimeSeconds;

		private bool m_IsActive;

		private float m_TimeSinceLastIncriment;

		private float m_TimeToStartAutoIncriment;

		private PlayerActions m_PlayerActions;

		private WinConditionsBrowser m_WinConditionBrowser;

		private CodeAnimation m_CodeAnimation;

		private void Awake()
		{
			_ = m_IncreaseTimeButton != null;
			_ = m_DecreaseTimeButton != null;
			m_PlayerActions = PlayerActions.Instance;
			UpdateTextField();
		}

		public void InitTimer(string timeValue)
		{
			m_SetTimeSeconds = int.Parse(timeValue);
			m_DisplayedTime.text = ConvertTimeToString(m_SetTimeSeconds);
		}

		public void SetBrowser(WinConditionsBrowser broswer)
		{
			m_WinConditionBrowser = broswer;
			if (m_WinConditionBrowser != null)
			{
				m_CodeAnimation = m_WinConditionBrowser.ConditionPanelCodeAnimation;
			}
		}

		private void Update()
		{
			if (m_PlayerActions != null)
			{
				if (m_PlayerActions.m_uiRight.WasPressed)
				{
					AdjustTime(1);
				}
				if (m_PlayerActions.m_uiLeft.WasPressed)
				{
					AdjustTime(-1);
				}
				if (m_PlayerActions.m_uiRight.WasReleased || m_PlayerActions.m_uiLeft.WasReleased)
				{
					m_TimeToStartAutoIncriment = 0f;
					m_TimeSinceLastIncriment = 0f;
				}
				HandleAutoIncrimentTime();
			}
		}

		private void HandleAutoIncrimentTime()
		{
			CheckAutoIncriment(m_PlayerActions.m_uiRight.IsPressed, 1);
			CheckAutoIncriment(m_PlayerActions.m_uiLeft.IsPressed, -1);
		}

		private void CheckAutoIncriment(bool input, int direction)
		{
			if (input)
			{
				m_TimeToStartAutoIncriment += Time.unscaledDeltaTime;
				if (m_TimeToStartAutoIncriment > 1f)
				{
					m_TimeSinceLastIncriment += Time.unscaledDeltaTime;
					AutoIncrimentTime(direction);
				}
			}
		}

		private void AutoIncrimentTime(int direction)
		{
			if (!(m_TimeSinceLastIncriment < 0.1f))
			{
				AdjustTime(direction);
				m_TimeSinceLastIncriment = 0f;
			}
		}

		public void AdjustTime(int i)
		{
			if (!(m_WinConditionBrowser == null) && !(m_CodeAnimation == null) && m_WinConditionBrowser.IsOpen && m_CodeAnimation.IsInAndNotPlaying)
			{
				m_SetTimeSeconds += i * m_TimeIncrimentValue;
				if (m_SetTimeSeconds < 5)
				{
					m_SetTimeSeconds = 5;
				}
				m_DisplayedTime.text = ConvertTimeToString(m_SetTimeSeconds);
				UpdateTextField();
			}
		}

		private void UpdateTextField()
		{
			m_PropertyField.InputField.text = m_SetTimeSeconds.ToString();
		}

		private void OnDestroy()
		{
			if (m_DecreaseTimeButton != null)
			{
				m_DecreaseTimeButton.onClick.RemoveAllListeners();
			}
			if (m_IncreaseTimeButton != null)
			{
				m_IncreaseTimeButton.onClick.RemoveAllListeners();
			}
		}

		private string ConvertTimeToString(int timeValue)
		{
			int time = timeValue % 3600 / 60;
			int time2 = timeValue % 60;
			return AddZero(time) + ":" + AddZero(time2);
		}

		private string AddZero(int time)
		{
			if (time < 10)
			{
				return "0" + time;
			}
			return string.Concat(time);
		}
	}
}
