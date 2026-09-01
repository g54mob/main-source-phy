using System.Collections;
using System.Collections.Generic;
using ModIO;
using ModIO.UI;
using UnityEngine;
using UnityEngine.UI;

public class DMPreviewSelector : MonoBehaviour, IModViewElement
{
	private ModView m_view;

	private ModMediaDisplaySwitch m_preview;

	private List<Button> m_buttons = new List<Button>();

	[SerializeField]
	private Button m_imageTemplate;

	[SerializeField]
	private RectTransform m_imageContainer;

	[SerializeField]
	private GameObject m_description;

	[SerializeField]
	private UIAutoScrollRect m_imageButtonAutoScroll;

	GameObject IModViewElement.gameObject => base.gameObject;

	public virtual void SetModView(ModView view)
	{
		if (!(m_view == view))
		{
			if (m_view != null)
			{
				m_view.onProfileChanged.RemoveListener(DisplayProfile);
			}
			m_view = view;
			if (m_view != null)
			{
				m_view.onProfileChanged.AddListener(DisplayProfile);
				DisplayProfile(m_view.profile);
			}
			else
			{
				DisplayProfile(null);
			}
		}
	}

	private void Awake()
	{
		m_preview = GetComponent<ModMediaDisplaySwitch>();
	}

	private void DisplayProfile(ModProfile modProfile)
	{
		if (modProfile == null)
		{
			return;
		}
		EnableDescription(enable: false);
		foreach (Transform item in m_imageContainer.transform)
		{
			Object.Destroy(item.gameObject);
		}
		m_buttons.Clear();
		Button button = Object.Instantiate(m_imageTemplate, m_imageContainer);
		button.onClick.AddListener(delegate
		{
			m_preview.DisplayLogo(modProfile.id, modProfile.logoLocator);
			EnableDescription(enable: false);
		});
		m_buttons.Add(button);
		if (modProfile.media.galleryImageLocators.Length != 0)
		{
			GalleryImageLocator[] galleryImageLocators = modProfile.media.galleryImageLocators;
			foreach (GalleryImageLocator galleryImageLocator in galleryImageLocators)
			{
				Button imageButton = Object.Instantiate(m_imageTemplate, m_imageContainer);
				GalleryImageLocator l = galleryImageLocator;
				imageButton.onClick.AddListener(delegate
				{
					DisplayPreview(l, modProfile.id);
					m_imageButtonAutoScroll.SetSelectionOverride(imageButton.gameObject);
				});
				m_buttons.Add(imageButton);
			}
		}
		Button btn = Object.Instantiate(m_imageTemplate, m_imageContainer);
		btn.onClick.AddListener(delegate
		{
			EnableDescription(enable: true);
			m_imageButtonAutoScroll.SetSelectionOverride(btn.gameObject);
		});
		btn.transform.GetChild(0).gameObject.SetActive(value: true);
		m_buttons.Add(btn);
		m_imageButtonAutoScroll.SetSelectionOverride(m_buttons[0].gameObject);
		DMInvokeCyclic component = m_imageContainer.GetComponent<DMInvokeCyclic>();
		Selectable[] selectables = m_buttons.ToArray();
		component.m_selectables = selectables;
		component.FetchSelectables();
		VerticalLayoutGroup verticalLayout = component.transform.parent.parent.parent.GetComponent<VerticalLayoutGroup>();
		StartCoroutine(Delay());
		IEnumerator Delay()
		{
			verticalLayout.enabled = false;
			yield return null;
			verticalLayout.enabled = true;
		}
	}

	private void DisplayPreview(GalleryImageLocator locator, int modId)
	{
		m_preview.DisplayGalleryImage(modId, locator);
		EnableDescription(enable: false);
	}

	private void EnableDescription(bool enable)
	{
		m_description.SetActive(enable);
	}
}
