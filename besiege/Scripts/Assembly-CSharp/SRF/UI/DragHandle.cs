using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRF.UI
{
	public class DragHandle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEventSystemHandler, IEndDragHandler
	{
		private CanvasScaler _canvasScaler;

		private float _delta;

		private float _startValue;

		public RectTransform.Axis Axis;

		public bool Invert;

		public float MaxSize = -1f;

		public LayoutElement TargetLayoutElement;

		public RectTransform TargetRectTransform;

		private float Mult
		{
			get
			{
				return (!Invert) ? 1 : (-1);
			}
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			if (Verify())
			{
				_startValue = GetCurrentValue();
				_delta = 0f;
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (Verify())
			{
				float num = 0f;
				num = ((Axis != RectTransform.Axis.Horizontal) ? (num + eventData.delta.y) : (num + eventData.delta.x));
				if (_canvasScaler != null)
				{
					num /= _canvasScaler.scaleFactor;
				}
				num *= Mult;
				_delta += num;
				SetCurrentValue(Mathf.Clamp(_startValue + _delta, GetMinSize(), GetMaxSize()));
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (Verify())
			{
				SetCurrentValue(Mathf.Max(_startValue + _delta, GetMinSize()));
				_delta = 0f;
				CommitCurrentValue();
			}
		}

		private void Start()
		{
			Verify();
			_canvasScaler = GetComponentInParent<CanvasScaler>();
		}

		private bool Verify()
		{
			if (TargetLayoutElement == null && TargetRectTransform == null)
			{
				Debug.LogWarning("DragHandle: TargetLayoutElement and TargetRectTransform are both null. Disabling behaviour.");
				base.enabled = false;
				return false;
			}
			return true;
		}

		private float GetCurrentValue()
		{
			if (TargetLayoutElement != null)
			{
				return (Axis != RectTransform.Axis.Horizontal) ? TargetLayoutElement.preferredHeight : TargetLayoutElement.preferredWidth;
			}
			if (TargetRectTransform != null)
			{
				return (Axis != RectTransform.Axis.Horizontal) ? TargetRectTransform.sizeDelta.y : TargetRectTransform.sizeDelta.x;
			}
			throw new InvalidOperationException();
		}

		private void SetCurrentValue(float value)
		{
			if (TargetLayoutElement != null)
			{
				if (Axis == RectTransform.Axis.Horizontal)
				{
					TargetLayoutElement.preferredWidth = value;
				}
				else
				{
					TargetLayoutElement.preferredHeight = value;
				}
				return;
			}
			if (TargetRectTransform != null)
			{
				Vector2 sizeDelta = TargetRectTransform.sizeDelta;
				if (Axis == RectTransform.Axis.Horizontal)
				{
					sizeDelta.x = value;
				}
				else
				{
					sizeDelta.y = value;
				}
				TargetRectTransform.sizeDelta = sizeDelta;
				return;
			}
			throw new InvalidOperationException();
		}

		private void CommitCurrentValue()
		{
			if (TargetLayoutElement != null)
			{
				if (Axis == RectTransform.Axis.Horizontal)
				{
					TargetLayoutElement.preferredWidth = ((RectTransform)TargetLayoutElement.transform).sizeDelta.x;
				}
				else
				{
					TargetLayoutElement.preferredHeight = ((RectTransform)TargetLayoutElement.transform).sizeDelta.y;
				}
			}
		}

		private float GetMinSize()
		{
			if (TargetLayoutElement == null)
			{
				return 0f;
			}
			return (Axis != RectTransform.Axis.Horizontal) ? TargetLayoutElement.minHeight : TargetLayoutElement.minWidth;
		}

		private float GetMaxSize()
		{
			if (MaxSize > 0f)
			{
				return MaxSize;
			}
			return float.MaxValue;
		}
	}
}
