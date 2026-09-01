using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ModIO.UI
{
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(RectTransform))]
	public class ScaleFitter : UIBehaviour, ILayoutSelfController, ILayoutController
	{
		public enum ScaleMode
		{
			Disabled = 0,
			WidthControlsHeight = 1,
			HeightControlsWidth = 2,
			FitInParent = 3,
			EnvelopeParent = 4,
			Stretch = 5
		}

		[SerializeField]
		private ScaleMode m_scaleMode;

		[NonSerialized]
		private RectTransform m_rect;

		private DrivenRectTransformTracker m_tracker;

		public virtual ScaleMode scaleMode
		{
			get
			{
				return m_scaleMode;
			}
			set
			{
				if (m_scaleMode != value)
				{
					m_scaleMode = value;
					UpdateRectScale();
				}
			}
		}

		protected RectTransform rectTransform
		{
			get
			{
				if (m_rect == null)
				{
					m_rect = GetComponent<RectTransform>();
				}
				return m_rect;
			}
		}

		protected virtual Vector2 GetParentSize()
		{
			RectTransform rectTransform = this.rectTransform.parent as RectTransform;
			if (!rectTransform)
			{
				return Vector2.zero;
			}
			return rectTransform.rect.size;
		}

		protected virtual Vector2 GetThisSize()
		{
			return rectTransform.rect.size;
		}

		protected ScaleFitter()
		{
		}

		protected override void OnRectTransformDimensionsChange()
		{
			UpdateRectScale();
		}

		protected override void OnTransformParentChanged()
		{
			UpdateRectScale();
		}

		protected virtual void UpdateRectScale()
		{
			m_tracker.Clear();
			if (m_scaleMode == ScaleMode.Disabled)
			{
				rectTransform.localScale = new Vector3(1f, 1f, rectTransform.localScale.z);
			}
			if (IsActive() && m_scaleMode != ScaleMode.Disabled)
			{
				ScaleMode calculationMode = GetCalculationMode(m_scaleMode);
				Vector3 calculcatedLocalScale = CalculateScaleValues(calculationMode);
				ApplyCalculatedValues(calculcatedLocalScale, calculationMode);
			}
		}

		protected virtual ScaleMode GetCalculationMode(ScaleMode selectedScaleMode)
		{
			ScaleMode scaleMode = selectedScaleMode;
			Vector2 size = rectTransform.rect.size;
			if (size.x == 0f)
			{
				switch (scaleMode)
				{
				case ScaleMode.WidthControlsHeight:
					scaleMode = ScaleMode.Disabled;
					break;
				case ScaleMode.FitInParent:
				case ScaleMode.EnvelopeParent:
					scaleMode = ScaleMode.HeightControlsWidth;
					break;
				}
			}
			if (size.y == 0f)
			{
				switch (scaleMode)
				{
				case ScaleMode.HeightControlsWidth:
					scaleMode = ScaleMode.Disabled;
					break;
				case ScaleMode.FitInParent:
				case ScaleMode.EnvelopeParent:
					scaleMode = ScaleMode.WidthControlsHeight;
					break;
				}
			}
			return scaleMode;
		}

		protected virtual Vector3 CalculateScaleValues(ScaleMode calculationScaleMode)
		{
			Vector2 parentSize = GetParentSize();
			Vector2 thisSize = GetThisSize();
			float num = 1f;
			float num2 = 1f;
			float z = rectTransform.localScale.z;
			switch (calculationScaleMode)
			{
			case ScaleMode.WidthControlsHeight:
				num = parentSize.x / thisSize.x;
				num2 = num;
				break;
			case ScaleMode.HeightControlsWidth:
				num2 = parentSize.y / thisSize.y;
				num = num2;
				break;
			case ScaleMode.FitInParent:
				num = (num2 = Mathf.Min(parentSize.x / thisSize.x, parentSize.y / thisSize.y));
				break;
			case ScaleMode.EnvelopeParent:
				num = (num2 = Mathf.Max(parentSize.x / thisSize.x, parentSize.y / thisSize.y));
				break;
			case ScaleMode.Stretch:
				num = parentSize.x / thisSize.x;
				num2 = parentSize.y / thisSize.y;
				break;
			}
			return new Vector3(num, num2, z);
		}

		protected virtual void ApplyCalculatedValues(Vector3 calculcatedLocalScale, ScaleMode calculcationScaleMode)
		{
			m_tracker.Add(this, rectTransform, DrivenTransformProperties.ScaleX | DrivenTransformProperties.ScaleY);
			rectTransform.localScale = calculcatedLocalScale;
			if (m_scaleMode == ScaleMode.FitInParent || m_scaleMode == ScaleMode.EnvelopeParent || m_scaleMode == ScaleMode.Stretch)
			{
				m_tracker.Add(this, rectTransform, DrivenTransformProperties.Anchors | DrivenTransformProperties.AnchoredPosition | DrivenTransformProperties.Pivot);
				RectTransform obj = rectTransform;
				RectTransform obj2 = rectTransform;
				Vector2 vector = (rectTransform.anchorMax = new Vector2(0.5f, 0.5f));
				Vector2 pivot = (obj2.anchorMin = vector);
				obj.pivot = pivot;
				rectTransform.anchoredPosition = Vector2.zero;
			}
		}

		public virtual void SetLayoutHorizontal()
		{
			UpdateRectScale();
		}

		public virtual void SetLayoutVertical()
		{
		}
	}
}
