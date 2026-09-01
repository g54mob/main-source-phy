using Landfall.TABS;
using TMPro;
using UnityEngine;

public class WinConditionTimerUI : MonoBehaviour
{
	private enum PulseState
	{
		Idle = 0,
		PulsingUp = 1,
		PulsingDown = 2
	}

	private const float MinTimerScale = 1f;

	private const float MaxTimerScale = 1.3f;

	private const float PulseSpeed = 0.1f;

	public RectTransform numberTransform;

	public TextMeshProUGUI text;

	private PulseState currentPulseState;

	private float timerScale = 1f;

	private int lastTime = -1;

	private LocalizeText m_headerText;

	private void Start()
	{
		m_headerText = GetComponent<LocalizeText>();
		currentPulseState = PulseState.Idle;
	}

	public void SetTeamText(Team team)
	{
		m_headerText.Args = new string[1] { team.ToString().ToUpper() };
		m_headerText.LocaleID = "WIN_CON_TEAM_WINS_IN";
	}

	public void UpdateTime(float secondsLeft)
	{
		int num = Mathf.CeilToInt(secondsLeft);
		if (num != lastTime)
		{
			currentPulseState = PulseState.PulsingUp;
		}
		lastTime = num;
		text.text = num.ToString();
	}

	private void Update()
	{
		switch (currentPulseState)
		{
		case PulseState.PulsingUp:
			if (timerScale < 1.3f)
			{
				timerScale = Mathf.MoveTowards(timerScale, 1.3f, 0.1f);
				numberTransform.localScale = Vector3.one * timerScale;
			}
			else
			{
				currentPulseState = PulseState.PulsingDown;
			}
			break;
		case PulseState.PulsingDown:
			if (timerScale > 1f)
			{
				timerScale = Mathf.MoveTowards(timerScale, 1f, 0.1f);
				numberTransform.localScale = Vector3.one * timerScale;
			}
			else
			{
				currentPulseState = PulseState.Idle;
			}
			break;
		}
	}
}
