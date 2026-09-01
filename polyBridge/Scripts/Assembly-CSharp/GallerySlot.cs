using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GallerySlot : MonoBehaviour
{
	public delegate void OnHoverChangeDelegate(GallerySlot slot, bool hover);

	public OnHoverChangeDelegate m_OnHoverChangeCallback;

	[Header("Content")]
	public GameObject m_ContentNode;

	public RawImage m_RawImage;

	public PointerEvents m_PointerEvents;

	public Image m_Progress;

	public Image m_Background;

	public Image m_Border;

	public GameObject m_VideoLoadingAnim;

	[Header("Footer")]
	public TextMeshProUGUI m_LevelNameText;

	public TextMeshProUGUI m_MaxStressText;

	public TextMeshProUGUI m_DateText;

	public TextMeshProUGUI m_BudgetText;

	public Image m_WinIcon;

	public Image m_CheatIcon;

	public Image m_StressIcon;

	public Image m_BreaksIcon;

	public Image m_Divider;

	public HorizontalLayoutGroup m_FooterHorizontalLayoutGroup;

	private GallerySlotResult m_Result;

	private long m_FrameIndex;

	private RenderTexture m_RenderTexture;

	[NonSerialized]
	public int m_SlotIndex = -1;

	private GalleryItem m_GalleryItem;

	public GalleryItem GetGalleryItem => m_GalleryItem;

	public RenderTexture RenderTexture => m_RenderTexture;

	public Texture2D PreviewTexture => m_GalleryItem?.m_PreviewTexture;

	public long FrameIndex
	{
		get
		{
			return m_FrameIndex;
		}
		set
		{
			m_FrameIndex = value;
		}
	}

	private void OnEnable()
	{
		m_PointerEvents.RegisterOnHoverChangeDelegate(OnHoverChange);
	}

	private void OnDestroy()
	{
		if (m_GalleryItem != null)
		{
			m_GalleryItem = null;
		}
		if ((bool)m_RenderTexture)
		{
			m_RenderTexture.Release();
			m_RenderTexture = null;
		}
	}

	public void UpdateManual()
	{
		if (m_GalleryItem != null && m_GalleryItem.m_PreviewTexture == null)
		{
			Texture2D texture2D = PreviewCache.Get(m_GalleryItem.GetVideoPreviewFilename());
			if (texture2D != null)
			{
				m_GalleryItem.m_PreviewTexture = texture2D;
				PreviewLoaded();
			}
		}
		if (m_GalleryItem != null && m_GalleryItem.m_PreviewTexture != null && !m_RawImage.enabled)
		{
			PreviewLoaded();
		}
	}

	public void SetHovered(bool hovered)
	{
		m_Border.enabled = hovered;
	}

	public void AllocateRenderTexture()
	{
		if (m_RenderTexture == null)
		{
			m_RenderTexture = new RenderTexture(Gallery.VIDEO_PREVIEW_WIDTH, Gallery.VIDEO_PREVIEW_HEIGHT, 16);
		}
	}

	public void SetLoading()
	{
		ClearItem();
		base.gameObject.SetActive(value: true);
		m_ContentNode.SetActive(value: false);
	}

	public void ClearItem()
	{
		if (m_GalleryItem != null)
		{
			m_GalleryItem = null;
		}
		m_Progress.fillAmount = 0f;
		if ((bool)m_Border)
		{
			m_Border.enabled = false;
		}
	}

	public void SetDisplayedItem(GalleryItem item)
	{
		if (item == m_GalleryItem)
		{
			return;
		}
		ClearItem();
		base.gameObject.SetActive(value: true);
		m_ContentNode.SetActive(value: true);
		m_GalleryItem = item;
		UpdateFooter();
		AllocateRenderTexture();
		if (m_GalleryItem.m_PreviewTexture != null)
		{
			if (m_RawImage != null)
			{
				m_RawImage.enabled = true;
				m_RawImage.texture = m_GalleryItem.m_PreviewTexture;
			}
			return;
		}
		if (m_RawImage != null)
		{
			m_RawImage.enabled = false;
		}
		Texture2D texture2D = PreviewCache.Get(m_GalleryItem.GetId());
		if (texture2D != null)
		{
			m_GalleryItem.m_PreviewTexture = texture2D;
			PreviewLoaded();
		}
	}

	public void SetHidden()
	{
		ClearItem();
		base.gameObject.SetActive(value: false);
	}

	public void PreviewLoaded()
	{
		if (m_RawImage != null && m_GalleryItem != null && m_GalleryItem.m_PreviewTexture != null)
		{
			m_RawImage.enabled = true;
			m_RawImage.texture = m_GalleryItem.m_PreviewTexture;
		}
	}

	public void UpdateFooter()
	{
		GameUI.SetAndEnableText(m_LevelNameText, m_GalleryItem.GetLevelNameFormatted());
		m_BudgetText.text = m_GalleryItem.GetBudget();
		m_DateText.text = FormatDate(m_GalleryItem.GetCreatedAt());
		m_WinIcon.gameObject.SetActive(m_GalleryItem.IsWin());
		m_CheatIcon.gameObject.SetActive(m_GalleryItem.IsCheat());
		bool flag = m_GalleryItem.HasBreaks();
		m_BreaksIcon.gameObject.SetActive(flag);
		if (m_FooterHorizontalLayoutGroup != null)
		{
			m_StressIcon.gameObject.SetActive(!flag);
			m_Divider.gameObject.SetActive(!flag);
			m_MaxStressText.gameObject.SetActive(!flag);
			m_MaxStressText.text = m_GalleryItem.GetMaxStress();
			LayoutRebuilder.ForceRebuildLayoutImmediate(m_FooterHorizontalLayoutGroup.GetComponent<RectTransform>());
		}
	}

	public void SetProgress(float progress)
	{
		m_Progress.fillAmount = Mathf.Clamp01(progress);
	}

	private void OnHoverChange(bool hover)
	{
		if (m_OnHoverChangeCallback != null)
		{
			m_OnHoverChangeCallback(this, hover);
		}
	}

	private string FormatDate(string createdAt)
	{
		if (string.IsNullOrEmpty(createdAt))
		{
			return string.Empty;
		}
		if (!DateTime.TryParse(createdAt, out var result))
		{
			return string.Empty;
		}
		return Utils.FormatShortDate(result);
	}
}
