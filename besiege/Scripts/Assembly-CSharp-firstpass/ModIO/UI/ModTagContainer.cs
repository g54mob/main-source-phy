using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[Obsolete("Use TagContainer instead.")]
	public class ModTagContainer : ModTagCollectionDisplayComponent
	{
		[Header("Settings")]
		public GameObject tagDisplayPrefab;

		[Header("UI Components")]
		public RectTransform container;

		public GameObject loadingOverlay;

		[Header("Display Data")]
		[SerializeField]
		private ModTagDisplayData[] m_data = new ModTagDisplayData[0];

		private List<ModTagDisplayComponent> m_tagDisplays = new List<ModTagDisplayComponent>();

		public IEnumerable<ModTagDisplayComponent> tagDisplays
		{
			get
			{
				return m_tagDisplays;
			}
		}

		public override IEnumerable<ModTagDisplayData> data
		{
			get
			{
				return m_data;
			}
			set
			{
				if (value == null)
				{
					m_data = new ModTagDisplayData[0];
				}
				else
				{
					m_data = value.ToArray();
				}
				PresentData(m_data);
			}
		}

		public event Action<ModTagDisplayComponent> tagClicked;

		private void PresentData(IList<ModTagDisplayData> displayData)
		{
			if (loadingOverlay != null)
			{
				loadingOverlay.SetActive(false);
			}
			int count = displayData.Count;
			while (count < m_tagDisplays.Count)
			{
				ModTagDisplayComponent modTagDisplayComponent = m_tagDisplays[count];
				m_tagDisplays.RemoveAt(count);
				UnityEngine.Object.Destroy(modTagDisplayComponent.gameObject);
			}
			while (m_tagDisplays.Count < count)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(tagDisplayPrefab);
				gameObject.transform.SetParent(container, false);
				ModTagDisplayComponent component = gameObject.GetComponent<ModTagDisplayComponent>();
				component.Initialize();
				component.onClick += NotifyTagClicked;
				m_tagDisplays.Add(component);
			}
			for (int i = 0; i < count; i++)
			{
				m_tagDisplays[i].data = displayData[i];
			}
			if (base.isActiveAndEnabled)
			{
				StartCoroutine(LateUpdateLayouting());
			}
		}

		public override void Initialize()
		{
			if (Application.isPlaying)
			{
			}
			CollectChildTags();
			PresentData(m_data);
		}

		private void CollectChildTags()
		{
			m_tagDisplays = new List<ModTagDisplayComponent>();
			foreach (Transform item in container)
			{
				ModTagDisplayComponent component = item.GetComponent<ModTagDisplayComponent>();
				if (component != null)
				{
					m_tagDisplays.Add(component);
				}
			}
		}

		public void OnEnable()
		{
			StartCoroutine(LateUpdateLayouting());
		}

		public IEnumerator LateUpdateLayouting()
		{
			yield return null;
			LayoutRebuilder.MarkLayoutForRebuild(container);
		}

		public override void DisplayTags(ModProfile profile, IEnumerable<ModTagCategory> tagCategories)
		{
			DisplayTags(profile.tagNames, tagCategories);
		}

		public override void DisplayTags(IEnumerable<string> tags, IEnumerable<ModTagCategory> tagCategories)
		{
			if (tags == null)
			{
				tags = new string[0];
			}
			m_data = ModTagDisplayData.GenerateArray(tags, tagCategories);
			PresentData(m_data);
		}

		public override void DisplayLoading()
		{
			if (loadingOverlay != null)
			{
				loadingOverlay.SetActive(true);
			}
			foreach (ModTagDisplayComponent tagDisplay in m_tagDisplays)
			{
				UnityEngine.Object.Destroy(tagDisplay.gameObject);
			}
			m_tagDisplays.Clear();
		}

		public void NotifyTagClicked(ModTagDisplayComponent display)
		{
			if (this.tagClicked != null)
			{
				this.tagClicked(display);
			}
		}
	}
}
