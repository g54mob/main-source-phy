using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("Layout/Content Size Fitter Animated", 141)]
[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class ContentSizeFitterAnimated : UIBehaviour, ILayoutSelfController, ILayoutController
{
	public enum FitMode
	{
		Unconstrained = 0,
		MinSize = 1,
		PreferredSize = 2
	}

	[SerializeField]
	protected FitMode m_HorizontalFit;

	[SerializeField]
	protected FitMode m_VerticalFit;

	[NonSerialized]
	private RectTransform m_Rect;

	private DrivenRectTransformTracker m_Tracker;

	private bool initialResize = true;

	public FitMode horizontalFit
	{
		get
		{
			return m_HorizontalFit;
		}
		set
		{
			if (SetStruct(ref m_HorizontalFit, value))
			{
				SetDirty();
			}
		}
	}

	public FitMode verticalFit
	{
		get
		{
			return m_VerticalFit;
		}
		set
		{
			if (SetStruct(ref m_VerticalFit, value))
			{
				SetDirty();
			}
		}
	}

	private RectTransform rectTransform
	{
		get
		{
			if (m_Rect == null)
			{
				m_Rect = GetComponent<RectTransform>();
			}
			return m_Rect;
		}
	}

	private static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
	{
		if (currentValue.Equals(newValue))
		{
			return false;
		}
		currentValue = newValue;
		return true;
	}

	protected ContentSizeFitterAnimated()
	{
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		SetDirty();
		initialResize = true;
	}

	protected override void OnDisable()
	{
		m_Tracker.Clear();
		LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
		base.OnDisable();
	}

	protected override void OnRectTransformDimensionsChange()
	{
		SetDirty();
	}

	public void SetDirtyExternal()
	{
		SetDirty();
	}

	private void HandleSelfFittingAlongAxis(int axis)
	{
		FitMode fitMode = ((axis == 0) ? horizontalFit : verticalFit);
		if (fitMode == FitMode.Unconstrained)
		{
			m_Tracker.Add(this, rectTransform, DrivenTransformProperties.None);
			return;
		}
		m_Tracker.Add(this, rectTransform, (axis == 0) ? DrivenTransformProperties.SizeDeltaX : DrivenTransformProperties.SizeDeltaY);
		float targetSize = 0f;
		if (fitMode == FitMode.MinSize)
		{
			targetSize = LayoutUtility.GetMinSize(m_Rect, axis);
		}
		else
		{
			targetSize = LayoutUtility.GetPreferredSize(m_Rect, axis);
		}
		if (false || initialResize)
		{
			rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, targetSize);
			initialResize = false;
			return;
		}
		TemporaryUpdate.temporaryUpdate(base.gameObject, delegate
		{
			float num = ((axis == 0) ? rectTransform.sizeDelta.x : rectTransform.sizeDelta.y);
			float num2 = Mathf.MoveTowards(num, targetSize, Time.deltaTime * 1000f);
			rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, num2);
			return num == num2;
		});
	}

	public virtual void SetLayoutHorizontal()
	{
		m_Tracker.Clear();
		HandleSelfFittingAlongAxis(0);
	}

	public virtual void SetLayoutVertical()
	{
		HandleSelfFittingAlongAxis(1);
	}

	protected void SetDirty()
	{
		if (IsActive())
		{
			LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
		}
	}
}
