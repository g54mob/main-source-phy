using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SandboxThumbnail : MonoBehaviour
{
	public string m_ID;

	public Image m_ThumbnailImage;

	public TextMeshProUGUI m_ThumbnailName;

	public Button m_ThumbnailButton;

	public RectTransform m_IconRectTransform;

	private string m_LocID;

	private Action<SandboxThumbnail> m_OnPressedAction;

	private readonly float DEFAULT_SCALE = 1.2f;

	private readonly float HOVER_SCALE = 1.4f;

	public void Awake()
	{
		m_ThumbnailButton.onClick.AddListener(OnClick);
	}

	public void OnEnable()
	{
		m_ThumbnailImage.transform.localScale = new Vector3(DEFAULT_SCALE, DEFAULT_SCALE, DEFAULT_SCALE);
	}

	public void SetCallback(string id, Action<SandboxThumbnail> action)
	{
		m_ID = id;
		m_OnPressedAction = action;
	}

	public void SetName(string locId)
	{
		m_LocID = locId;
		RefreshLocalization();
	}

	public void RefreshLocalization()
	{
		m_ThumbnailName.text = Localize.Get(m_LocID);
	}

	public void SetSprite(Sprite sprite)
	{
		m_ThumbnailImage.sprite = sprite;
	}

	public void AddSandboxListener(SandboxItemType sandboxItemType, GameObject prefab, string prefabAddress, string modId)
	{
		if (!(base.gameObject.GetComponent<SandboxCreateObjectListener>() != null))
		{
			SandboxCreateObjectListener sandboxCreateObjectListener = base.gameObject.AddComponent<SandboxCreateObjectListener>();
			if (sandboxCreateObjectListener != null)
			{
				sandboxCreateObjectListener.m_Category = sandboxItemType;
				sandboxCreateObjectListener.m_Prefab = prefab;
				sandboxCreateObjectListener.m_PrefabAddress = prefabAddress;
				sandboxCreateObjectListener.m_Id = m_ID;
				sandboxCreateObjectListener.m_ModId = modId;
				sandboxCreateObjectListener.SetHoverCallback(OnHoverChange);
			}
		}
	}

	public void AddToolTip(string locID)
	{
		ToolTipText toolTipText = m_ThumbnailImage.gameObject.AddComponent<ToolTipText>();
		if (toolTipText != null)
		{
			toolTipText.m_RawLocalizationKey = locID;
			toolTipText.m_LocalizationKey = ToolTipLocalizationKey.TOOLTIP_MISSING;
		}
	}

	public bool PassesFilter(string lowerCaseFilter)
	{
		if (string.IsNullOrEmpty(lowerCaseFilter))
		{
			return true;
		}
		return Localize.Get(m_LocID).ToLower().Contains(lowerCaseFilter);
	}

	private void OnClick()
	{
		m_OnPressedAction?.Invoke(this);
	}

	private void OnHoverChange(bool hover)
	{
		if (hover)
		{
			m_IconRectTransform.DOScale(HOVER_SCALE * Vector3.one, 0.1f).SetEase(Ease.InOutBounce).SetLoops(1, LoopType.Yoyo)
				.SetUpdate(isIndependentUpdate: true);
			InterfaceAudio.Play("ui_menuButton_hover");
		}
		else
		{
			m_IconRectTransform.DOScale(DEFAULT_SCALE * Vector3.one, 0.1f).SetEase(Ease.InOutBounce).SetLoops(1, LoopType.Yoyo)
				.SetUpdate(isIndependentUpdate: true);
		}
	}
}
