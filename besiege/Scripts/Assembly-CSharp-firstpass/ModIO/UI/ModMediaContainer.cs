using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[Obsolete("Use ModLogoDisplay, GalleryImageContainer, and YouTubeThumbnailContainer instead.")]
	public class ModMediaContainer : ModMediaCollectionDisplayComponent
	{
		[Header("Settings")]
		public GameObject logoPrefab;

		public GameObject galleryImagePrefab;

		public GameObject youTubeThumbnailPrefab;

		[Header("UI Components")]
		public RectTransform container;

		[Header("Display Data")]
		private ImageDisplay m_logoDisplay;

		private List<ImageDisplay> m_galleryDisplays = new List<ImageDisplay>();

		private List<ImageDisplay> m_youTubeDisplays = new List<ImageDisplay>();

		private ImageDisplayData m_logoData = default(ImageDisplayData);

		private ImageDisplayData[] m_youTubeData = new ImageDisplayData[0];

		private ImageDisplayData[] m_galleryData = new ImageDisplayData[0];

		public ImageDisplay logoDisplay
		{
			get
			{
				return m_logoDisplay;
			}
		}

		public IEnumerable<ImageDisplay> youTubeDisplays
		{
			get
			{
				return m_youTubeDisplays;
			}
		}

		public IEnumerable<ImageDisplay> galleryDisplays
		{
			get
			{
				return m_galleryDisplays;
			}
		}

		public IEnumerable<ImageDisplay> allDisplays
		{
			get
			{
				if (m_logoDisplay != null)
				{
					yield return m_logoDisplay;
				}
				foreach (ImageDisplay youTubeDisplay in m_youTubeDisplays)
				{
					yield return youTubeDisplay;
				}
				foreach (ImageDisplay galleryDisplay in m_galleryDisplays)
				{
					yield return galleryDisplay;
				}
			}
		}

		public override ImageDisplayData logoData
		{
			get
			{
				if (m_logoDisplay == null)
				{
					return m_logoData;
				}
				return m_logoDisplay.data;
			}
			set
			{
				if (!logoData.Equals(value))
				{
					m_logoData = value;
					PresentLogoData();
				}
			}
		}

		public override IEnumerable<ImageDisplayData> youTubeData
		{
			get
			{
				if (youTubeThumbnailPrefab == null)
				{
					ImageDisplayData[] array = m_youTubeData;
					for (int i = 0; i < array.Length; i++)
					{
						yield return array[i];
					}
					yield break;
				}
				foreach (ImageDisplay display in m_youTubeDisplays)
				{
					yield return display.data;
				}
			}
			set
			{
				if (!youTubeData.Equals(value))
				{
					m_youTubeData = value.ToArray();
					PresentYouTubeData();
				}
			}
		}

		public override IEnumerable<ImageDisplayData> galleryData
		{
			get
			{
				if (galleryImagePrefab == null)
				{
					ImageDisplayData[] array = m_galleryData;
					for (int i = 0; i < array.Length; i++)
					{
						yield return array[i];
					}
					yield break;
				}
				foreach (ImageDisplay display in m_galleryDisplays)
				{
					yield return display.data;
				}
			}
			set
			{
				if (!galleryData.Equals(value))
				{
					m_galleryData = value.ToArray();
					PresentGalleryData();
				}
			}
		}

		public event Action<ImageDisplay> logoClicked;

		public event Action<ImageDisplay> galleryImageClicked;

		public event Action<ImageDisplay> youTubeThumbnailClicked;

		public override void Initialize()
		{
		}

		public void OnEnable()
		{
			StartCoroutine(EndOfFrameUpdateCoroutine());
		}

		public override void DisplayMedia(ModProfile profile)
		{
			DisplayMedia(profile.id, profile.logoLocator, profile.media.galleryImageLocators, profile.media.youTubeURLs);
		}

		public override void DisplayMedia(int modId, LogoImageLocator logoLocator, IEnumerable<GalleryImageLocator> galleryImageLocators, IEnumerable<string> youTubeURLs)
		{
			ClearDisplays();
			if (logoLocator != null && logoPrefab != null)
			{
				ImageDisplay imageDisplay = InstantiatePrefab(logoPrefab);
				imageDisplay.DisplayLogo(modId, logoLocator);
				imageDisplay.onClick += NotifyLogoClicked;
				m_logoDisplay = imageDisplay;
			}
			if (youTubeURLs != null && youTubeThumbnailPrefab != null)
			{
				foreach (string youTubeURL in youTubeURLs)
				{
					ImageDisplay imageDisplay2 = InstantiatePrefab(youTubeThumbnailPrefab);
					imageDisplay2.DisplayYouTubeThumbnail(modId, Utility.ExtractYouTubeIdFromURL(youTubeURL));
					imageDisplay2.onClick += NotifyYouTubeThumbnailClicked;
					m_youTubeDisplays.Add(imageDisplay2);
				}
			}
			if (galleryImageLocators != null && galleryImagePrefab != null)
			{
				foreach (GalleryImageLocator galleryImageLocator in galleryImageLocators)
				{
					ImageDisplay imageDisplay3 = InstantiatePrefab(galleryImagePrefab);
					imageDisplay3.DisplayGalleryImage(modId, galleryImageLocator);
					imageDisplay3.onClick += NotifyGalleryImageClicked;
					m_galleryDisplays.Add(imageDisplay3);
				}
			}
			if (Application.isPlaying)
			{
				LateLayoutUpdate();
			}
		}

		public override void DisplayLoading()
		{
			ClearDisplays();
			if (Application.isPlaying)
			{
				LateLayoutUpdate();
			}
		}

		private void PresentLogoData()
		{
			if (m_logoData.descriptor == ImageDescriptor.None)
			{
				if (m_logoDisplay != null)
				{
					UnityEngine.Object.Destroy(m_logoDisplay.gameObject);
				}
			}
			else if (logoPrefab != null)
			{
				if (logoDisplay == null)
				{
					m_logoDisplay = InstantiatePrefab(logoPrefab);
					m_logoDisplay.transform.SetSiblingIndex(0);
					m_logoDisplay.onClick += NotifyLogoClicked;
				}
				m_logoDisplay.data = m_logoData;
			}
			if (Application.isPlaying)
			{
				LateLayoutUpdate();
			}
		}

		private void PresentYouTubeData()
		{
			foreach (ImageDisplay youTubeDisplay in m_youTubeDisplays)
			{
				UnityEngine.Object.Destroy(youTubeDisplay.gameObject);
			}
			m_youTubeDisplays.Clear();
			int num = 0;
			if (logoDisplay != null)
			{
				num++;
			}
			if (youTubeThumbnailPrefab != null)
			{
				ImageDisplayData[] array = m_youTubeData;
				foreach (ImageDisplayData data in array)
				{
					ImageDisplay imageDisplay = InstantiatePrefab(youTubeThumbnailPrefab);
					imageDisplay.data = data;
					imageDisplay.transform.SetSiblingIndex(num);
					imageDisplay.onClick += NotifyYouTubeThumbnailClicked;
					m_youTubeDisplays.Add(imageDisplay);
					num++;
				}
			}
			if (Application.isPlaying)
			{
				LateLayoutUpdate();
			}
		}

		private void PresentGalleryData()
		{
			foreach (ImageDisplay galleryDisplay in m_galleryDisplays)
			{
				UnityEngine.Object.Destroy(galleryDisplay.gameObject);
			}
			m_galleryDisplays.Clear();
			if (galleryImagePrefab != null)
			{
				ImageDisplayData[] array = m_galleryData;
				foreach (ImageDisplayData data in array)
				{
					ImageDisplay imageDisplay = InstantiatePrefab(galleryImagePrefab);
					imageDisplay.data = data;
					imageDisplay.onClick += NotifyGalleryImageClicked;
					m_galleryDisplays.Add(imageDisplay);
				}
			}
			if (Application.isPlaying)
			{
				LateLayoutUpdate();
			}
		}

		private void ClearDisplays()
		{
			foreach (ImageDisplay allDisplay in allDisplays)
			{
				UnityEngine.Object.Destroy(allDisplay.gameObject);
			}
			m_logoDisplay = null;
			m_youTubeDisplays.Clear();
			m_galleryDisplays.Clear();
		}

		private ImageDisplay InstantiatePrefab(GameObject imagePrefab)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(imagePrefab, container);
			ImageDisplay component = gameObject.GetComponent<ImageDisplay>();
			component.Initialize();
			return component;
		}

		private void LateLayoutUpdate()
		{
			if (base.isActiveAndEnabled)
			{
				StartCoroutine(EndOfFrameUpdateCoroutine());
			}
			else
			{
				LayoutRebuilder.MarkLayoutForRebuild(container);
			}
		}

		private IEnumerator EndOfFrameUpdateCoroutine()
		{
			yield return null;
			LayoutRebuilder.MarkLayoutForRebuild(container);
		}

		public void NotifyLogoClicked(ImageDisplay display)
		{
			if (this.logoClicked != null)
			{
				this.logoClicked(display);
			}
		}

		public void NotifyYouTubeThumbnailClicked(ImageDisplay display)
		{
			if (this.youTubeThumbnailClicked != null)
			{
				this.youTubeThumbnailClicked(display);
			}
		}

		public void NotifyGalleryImageClicked(ImageDisplay display)
		{
			if (this.galleryImageClicked != null)
			{
				this.galleryImageClicked(display);
			}
		}
	}
}
