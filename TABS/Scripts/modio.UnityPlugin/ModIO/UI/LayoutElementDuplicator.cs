using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(RectTransform))]
	[ExecuteInEditMode]
	public class LayoutElementDuplicator : UIBehaviour, ILayoutElement
	{
		[SerializeField]
		private RectTransform copySource;

		[SerializeField]
		private int m_LayoutPriority = 1;

		[SerializeField]
		private bool m_CopyMinWidth;

		[SerializeField]
		private bool m_CopyMinHeight;

		[SerializeField]
		private bool m_CopyPreferredWidth;

		[SerializeField]
		private bool m_CopyPreferredHeight;

		[SerializeField]
		private bool m_CopyFlexibleWidth;

		[SerializeField]
		private bool m_CopyFlexibleHeight;

		private ILayoutElement[] m_layoutElementSources = new ILayoutElement[0];

		private float m_minWidth = -1f;

		private float m_preferredWidth = -1f;

		private float m_flexibleWidth = -1f;

		private float m_minHeight = -1f;

		private float m_preferredHeight = -1f;

		private float m_flexibleHeight = -1f;

		private bool m_awaitingRebuild;

		public float minWidth => m_minWidth;

		public float preferredWidth => m_preferredWidth;

		public float flexibleWidth => m_flexibleWidth;

		public float minHeight => m_minHeight;

		public float preferredHeight => m_preferredHeight;

		public float flexibleHeight => m_flexibleHeight;

		public int layoutPriority => m_LayoutPriority;

		public void CalculateLayoutInputHorizontal()
		{
			CalcLayoutHorizontal_Internal();
		}

		private bool CalcLayoutHorizontal_Internal()
		{
			UpdateLayoutSources();
			float num = -1f;
			if (m_CopyMinWidth)
			{
				ILayoutElement[] layoutElementSources = m_layoutElementSources;
				for (int i = 0; i < layoutElementSources.Length; i++)
				{
					num = Mathf.Max(layoutElementSources[i].minWidth, num);
				}
			}
			float num2 = -1f;
			if (m_CopyPreferredWidth)
			{
				int num3 = -1;
				ILayoutElement[] layoutElementSources = m_layoutElementSources;
				foreach (ILayoutElement layoutElement in layoutElementSources)
				{
					if (layoutElement.layoutPriority > num3 && layoutElement.preferredWidth >= 0f)
					{
						num2 = layoutElement.preferredWidth;
					}
				}
			}
			float num4 = -1f;
			if (m_CopyFlexibleWidth)
			{
				int num5 = -1;
				ILayoutElement[] layoutElementSources = m_layoutElementSources;
				foreach (ILayoutElement layoutElement2 in layoutElementSources)
				{
					if (layoutElement2.layoutPriority > num5 && layoutElement2.flexibleWidth >= 0f)
					{
						num4 = layoutElement2.flexibleWidth;
					}
				}
			}
			bool result = (num != m_minWidth) | (num2 != m_preferredWidth) | (num4 != m_flexibleWidth);
			m_minWidth = num;
			m_preferredWidth = num2;
			m_flexibleWidth = num4;
			return result;
		}

		public void CalculateLayoutInputVertical()
		{
			CalcLayoutVertical_Internal();
		}

		private bool CalcLayoutVertical_Internal()
		{
			UpdateLayoutSources();
			float num = -1f;
			if (m_CopyMinHeight)
			{
				ILayoutElement[] layoutElementSources = m_layoutElementSources;
				for (int i = 0; i < layoutElementSources.Length; i++)
				{
					num = Mathf.Max(layoutElementSources[i].minHeight, num);
				}
			}
			float num2 = -1f;
			if (m_CopyPreferredHeight)
			{
				int num3 = -1;
				ILayoutElement[] layoutElementSources = m_layoutElementSources;
				foreach (ILayoutElement layoutElement in layoutElementSources)
				{
					if (layoutElement.layoutPriority > num3 && layoutElement.preferredHeight >= 0f)
					{
						num2 = layoutElement.preferredHeight;
					}
				}
			}
			float num4 = -1f;
			if (m_CopyFlexibleHeight)
			{
				int num5 = -1;
				ILayoutElement[] layoutElementSources = m_layoutElementSources;
				foreach (ILayoutElement layoutElement2 in layoutElementSources)
				{
					if (layoutElement2.layoutPriority > num5 && layoutElement2.flexibleHeight >= 0f)
					{
						num4 = layoutElement2.flexibleHeight;
					}
				}
			}
			bool result = (num != m_minHeight) | (num2 != m_preferredHeight) | (num4 != m_flexibleHeight);
			m_minHeight = num;
			m_preferredHeight = num2;
			m_flexibleHeight = num4;
			return result;
		}

		private void OnGUI()
		{
			if ((0u | (CalcLayoutHorizontal_Internal() ? 1u : 0u) | (CalcLayoutVertical_Internal() ? 1u : 0u)) != 0)
			{
				SetDirty();
			}
		}

		private void UpdateLayoutSources()
		{
			if (copySource == null)
			{
				m_layoutElementSources = new ILayoutElement[0];
			}
			else
			{
				m_layoutElementSources = copySource.gameObject.GetComponents<ILayoutElement>();
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			SetDirty();
		}

		protected override void OnTransformParentChanged()
		{
			SetDirty();
		}

		protected override void OnDisable()
		{
			SetDirty();
			base.OnDisable();
		}

		protected override void OnDidApplyAnimationProperties()
		{
			SetDirty();
		}

		protected override void OnBeforeTransformParentChanged()
		{
			SetDirty();
		}

		protected void SetDirty()
		{
			if (IsActive() && !m_awaitingRebuild)
			{
				if (!CanvasUpdateRegistry.IsRebuildingLayout())
				{
					LayoutRebuilder.MarkLayoutForRebuild(base.transform as RectTransform);
					return;
				}
				m_awaitingRebuild = true;
				StartCoroutine(DelayedSetDirty(base.transform as RectTransform));
			}
		}

		private IEnumerator DelayedSetDirty(RectTransform rectTransform)
		{
			yield return null;
			LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
			m_awaitingRebuild = false;
		}
	}
}
