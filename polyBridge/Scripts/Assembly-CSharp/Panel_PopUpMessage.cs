using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PopUpMessage : MonoBehaviour
{
	public TextMeshProUGUI m_Title;

	public TextMeshProUGUI m_Message;

	public Toggle m_NeverShowAgainToggle;

	public Button m_BackButton;

	[Header("OK Button")]
	public Action m_OnOkCallback;

	public Action<FileSlot> m_OnOkFIleSlotCallback;

	public Button m_OkButton;

	public TextMeshProUGUI m_OKButtonText;

	[Header("Cancel Button")]
	public Button m_CancelButton;

	public TextMeshProUGUI m_CancelButtonText;

	public Action m_OnCancelCallback;

	[Header("Animation")]
	public GameObject m_WaitAnimation;

	[NonSerialized]
	public PopUpWarningCategory m_Category;

	[NonSerialized]
	public FileSlot m_FileSlot;

	private void Awake()
	{
		m_CancelButton.onClick.AddListener(OnCancel);
		m_OkButton.onClick.AddListener(OnOk);
		m_BackButton.onClick.AddListener(OnBack);
		m_WaitAnimation.SetActive(value: false);
	}

	private void OnEnable()
	{
		BridgeJointPlacement.CancelSelection();
		InterfaceAudio.Play("ui_menubar_gen_on");
		ActivePanels.Add(base.gameObject);
		if ((bool)m_OkButton.GetComponent<TweenScale>())
		{
			m_OkButton.GetComponent<TweenScale>().Stop();
			m_OkButton.GetComponent<TweenScale>().Reset();
		}
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
		GameUI.m_Instance.m_GamepadLegend.HideButtons();
	}

	private void Update()
	{
		ProcessInput();
	}

	private void OnCancel()
	{
		Close();
		InterfaceAudio.Play("ui_menubar_gen_off");
		m_OnCancelCallback?.Invoke();
	}

	private void OnOk()
	{
		Close();
		InterfaceAudio.Play("ui_menu_accept");
		if (m_OnOkFIleSlotCallback != null)
		{
			m_OnOkFIleSlotCallback(m_FileSlot);
		}
		else
		{
			m_OnOkCallback?.Invoke();
		}
	}

	public void OnBack()
	{
		Close();
		InterfaceAudio.Play("ui_menubar_gen_off");
	}

	public void Close()
	{
		if (m_NeverShowAgainToggle.gameObject.activeInHierarchy)
		{
			if (m_NeverShowAgainToggle.isOn)
			{
				if (!Profiles.m_ActiveProfile.m_NeverShowAgain.Contains(m_Category))
				{
					Profiles.m_ActiveProfile.m_NeverShowAgain.Add(m_Category);
					Profiles.SaveActiveProfile();
				}
			}
			else if (Profiles.m_ActiveProfile.m_NeverShowAgain.Contains(m_Category))
			{
				Profiles.m_ActiveProfile.m_NeverShowAgain.Remove(m_Category);
				Profiles.SaveActiveProfile();
			}
		}
		GameUI.m_Instance.m_PopUpMessage.gameObject.SetActive(value: false);
	}

	private void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			return;
		}
		if (!m_CancelButton.gameObject.activeInHierarchy)
		{
			if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH) || GamepadManager.ButtonJustPressed(GamepadButtonType.SOUTH))
			{
				OnOk();
			}
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				Game.ForceIgnoreNextSelection();
			}
			if (m_BackButton.gameObject.activeInHierarchy)
			{
				OnBack();
			}
			else
			{
				OnCancel();
			}
		}
		if (m_OkButton.gameObject.activeInHierarchy && GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
		{
			OnOk();
		}
	}

	public void PlayOkTween()
	{
		if (!m_OkButton.GetComponent<TweenScale>())
		{
			TweenScale tweenScale = m_OkButton.gameObject.AddComponent<TweenScale>();
			tweenScale.m_ScaleTo = new Vector3(1.2f, 1.2f, 1f);
			tweenScale.m_Time = 0.5f;
			tweenScale.m_EaseType = iTween.EaseType.easeInQuart;
			tweenScale.m_LoopType = iTween.LoopType.pingPong;
		}
		m_OkButton.GetComponent<TweenScale>().Play();
	}
}
