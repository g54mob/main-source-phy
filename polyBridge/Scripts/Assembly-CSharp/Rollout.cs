using UnityEngine;
using UnityEngine.UI;

public class Rollout : MonoBehaviour
{
	public RolloutState m_InitialState;

	public GameObject m_Child;

	public Button m_Button;

	public Image m_Background;

	public Image m_ButtonCollapsed;

	public Image m_ButtonExpanded;

	private RolloutState m_State;

	public void Awake()
	{
		m_Button.onClick.AddListener(OnButton);
		SetState(m_InitialState);
	}

	public void OnEnable()
	{
		m_Background.color = GameUI.m_Instance.m_RolloutBackgroundColor;
	}

	public void OnButton()
	{
		if (m_State == RolloutState.COLLAPSED)
		{
			SetState(RolloutState.EXPANDED);
			InterfaceAudio.Play("ui_menubar_gen_on");
		}
		else
		{
			SetState(RolloutState.COLLAPSED);
			InterfaceAudio.Play("ui_menubar_gen_off");
		}
	}

	public RolloutState GetState()
	{
		return m_State;
	}

	public void SetState(RolloutState state)
	{
		m_State = state;
		if (state == RolloutState.COLLAPSED)
		{
			m_Child.SetActive(value: false);
			m_ButtonCollapsed.gameObject.SetActive(value: true);
			m_ButtonExpanded.gameObject.SetActive(value: false);
		}
		else
		{
			m_Child.SetActive(value: true);
			m_ButtonCollapsed.gameObject.SetActive(value: false);
			m_ButtonExpanded.gameObject.SetActive(value: true);
		}
	}
}
