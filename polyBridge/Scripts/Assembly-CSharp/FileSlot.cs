using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FileSlot : MonoBehaviour
{
	public delegate void OnClickedDelegate(FileSlot slot);

	public delegate void OnHoverChangeDelegate(FileSlot slot, bool hover);

	public TextMeshProUGUI m_Prefix;

	public TextMeshProUGUI m_DisplayName;

	public TextMeshProUGUI m_Budget;

	[Header("Status")]
	public Image m_AsteriskIcon;

	public Image m_LockedIcon;

	public Image m_PassIcon;

	public Image m_UnderBudgetIcon;

	public Image m_UnderBudgetNoBreaksIcon;

	[Header("Toggle")]
	public Toggle m_Toggle;

	public PointerEvents m_TogglePointerEvents;

	[Header("Buttons")]
	public Button m_FileSlotButton;

	public Button m_InfoButton;

	public Button m_RenameButton;

	public Button m_DeleteButton;

	public Button m_UploadButton;

	public Button m_PlayButton;

	[Header("Highlight")]
	public Image m_Background;

	public Image m_SelectedHighlight;

	public Image m_HoverHighlight;

	public PointerEvents m_PointerEvents;

	[Header("BridgePreview")]
	public PointerEvents m_InfoPointerEvents;

	[NonSerialized]
	public string m_FileName;

	[NonSerialized]
	public long m_LastWriteTimeTicks;

	[NonSerialized]
	public bool m_IsDirectory;

	private OnHoverChangeDelegate m_OnHoverChangeCallback;

	private OnClickedDelegate m_OnClickedCallback;

	private OnClickedDelegate m_OnRenameCallback;

	private OnClickedDelegate m_OnDeleteCallback;

	private Action<FileSlot, bool> m_ToggleCallback;

	private Action<FileSlot> m_UploadCallback;

	private Action<FileSlot> m_OnPlayCallback;

	private void Awake()
	{
		if (m_AsteriskIcon != null)
		{
			m_AsteriskIcon.gameObject.SetActive(value: false);
		}
		m_LockedIcon.gameObject.SetActive(value: false);
		m_PassIcon.gameObject.SetActive(value: false);
		m_UnderBudgetIcon.gameObject.SetActive(value: false);
		m_UnderBudgetNoBreaksIcon.gameObject.SetActive(value: false);
		m_SelectedHighlight.gameObject.SetActive(value: false);
		if ((bool)m_HoverHighlight)
		{
			m_HoverHighlight.gameObject.SetActive(value: false);
		}
		m_InfoButton.gameObject.SetActive(value: false);
		m_RenameButton.gameObject.SetActive(value: false);
		m_DeleteButton.gameObject.SetActive(value: false);
		if ((bool)m_UploadButton)
		{
			m_UploadButton.gameObject.SetActive(value: false);
		}
		if ((bool)m_Budget)
		{
			m_Budget.text = string.Empty;
		}
		if ((bool)m_Toggle && (bool)m_TogglePointerEvents)
		{
			m_TogglePointerEvents.RegisterOnClickedDelegate(OnToggle);
		}
	}

	private void OnEnable()
	{
		m_FileSlotButton.onClick.AddListener(OnClicked);
		m_RenameButton.onClick.AddListener(OnRename);
		m_DeleteButton.onClick.AddListener(OnDelete);
		if ((bool)m_PlayButton)
		{
			m_PlayButton.onClick.AddListener(OnPlay);
		}
		if ((bool)m_UploadButton)
		{
			m_UploadButton.onClick.AddListener(OnUpload);
		}
	}

	private void OnDisable()
	{
		m_FileSlotButton.onClick.RemoveAllListeners();
		m_RenameButton.onClick.RemoveAllListeners();
		m_DeleteButton.onClick.RemoveAllListeners();
		if ((bool)m_UploadButton)
		{
			m_UploadButton.onClick.RemoveAllListeners();
		}
	}

	public void EnableHighlightOnHover()
	{
		m_PointerEvents.RegisterOnHoverChangeDelegate(OnHoverChange);
	}

	public void SetOnToggleCallback(Action<FileSlot, bool> callback)
	{
		m_ToggleCallback = callback;
		m_Toggle.gameObject.SetActive(value: true);
		m_PlayButton.gameObject.SetActive(value: false);
	}

	public void SetOnPlayCallback(Action<FileSlot> callback)
	{
		m_OnPlayCallback = callback;
		m_Toggle.gameObject.SetActive(value: false);
		m_PlayButton.gameObject.SetActive(value: true);
	}

	public void SetOnUploadCallback(Action<FileSlot> callback)
	{
		m_UploadCallback = callback;
	}

	public void SetOnHoverChangeCallback(OnHoverChangeDelegate callback)
	{
		if (callback != null)
		{
			m_OnHoverChangeCallback = callback;
		}
	}

	public void SetOnClickedCallback(OnClickedDelegate callback)
	{
		if (callback != null)
		{
			m_OnClickedCallback = callback;
		}
	}

	public void SetOnRenameCallback(OnClickedDelegate callback)
	{
		if (callback != null)
		{
			m_OnRenameCallback = callback;
			m_RenameButton.gameObject.SetActive(value: true);
		}
	}

	public void SetOnDeleteCallback(OnClickedDelegate callback)
	{
		if (callback != null)
		{
			m_OnDeleteCallback = callback;
			m_DeleteButton.gameObject.SetActive(value: true);
		}
	}

	public void SetStatusIcon(CampaignLevelStatus status)
	{
		switch (status)
		{
		case CampaignLevelStatus.PASS:
			m_LockedIcon.gameObject.SetActive(value: false);
			m_PassIcon.gameObject.SetActive(value: true);
			m_UnderBudgetIcon.gameObject.SetActive(value: false);
			m_UnderBudgetNoBreaksIcon.gameObject.SetActive(value: false);
			break;
		case CampaignLevelStatus.UNDER_BUDGET:
			m_LockedIcon.gameObject.SetActive(value: false);
			m_PassIcon.gameObject.SetActive(value: true);
			m_UnderBudgetIcon.gameObject.SetActive(value: true);
			m_UnderBudgetNoBreaksIcon.gameObject.SetActive(value: false);
			break;
		case CampaignLevelStatus.UNDER_BUDGET_NO_BREAKS:
			m_LockedIcon.gameObject.SetActive(value: false);
			m_PassIcon.gameObject.SetActive(value: true);
			m_UnderBudgetIcon.gameObject.SetActive(value: true);
			m_UnderBudgetNoBreaksIcon.gameObject.SetActive(value: true);
			break;
		default:
			m_LockedIcon.gameObject.SetActive(value: false);
			m_PassIcon.gameObject.SetActive(value: false);
			m_UnderBudgetIcon.gameObject.SetActive(value: false);
			m_UnderBudgetNoBreaksIcon.gameObject.SetActive(value: false);
			break;
		}
	}

	public bool IsToggleOn()
	{
		if ((bool)m_Toggle)
		{
			return m_Toggle.isOn;
		}
		return false;
	}

	private void OnClicked()
	{
		if (m_OnClickedCallback == null)
		{
			return;
		}
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			FileSlot fileSlotUnderPointer = GameUI.GetFileSlotUnderPointer();
			if (fileSlotUnderPointer != null)
			{
				m_OnClickedCallback(fileSlotUnderPointer);
				return;
			}
		}
		m_OnClickedCallback(this);
	}

	private void OnRename()
	{
		if (m_OnRenameCallback != null)
		{
			m_OnRenameCallback(this);
		}
	}

	private void OnDelete()
	{
		if (m_OnDeleteCallback != null)
		{
			m_OnDeleteCallback(this);
		}
	}

	private void OnUpload()
	{
		m_UploadCallback?.Invoke(this);
	}

	private void OnHoverChange(bool hover)
	{
		if ((bool)m_HoverHighlight)
		{
			m_HoverHighlight.gameObject.SetActive(hover);
		}
		if (m_OnHoverChangeCallback != null)
		{
			m_OnHoverChangeCallback(this, hover);
		}
	}

	private void OnToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		m_ToggleCallback?.Invoke(this, m_Toggle.isOn);
	}

	private void OnPlay()
	{
		m_OnPlayCallback?.Invoke(this);
	}
}
