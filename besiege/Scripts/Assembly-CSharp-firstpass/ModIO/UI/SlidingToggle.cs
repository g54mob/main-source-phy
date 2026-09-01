using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class SlidingToggle : Toggle
	{
		public enum SlideAxis
		{
			LeftOffRightOn = 0,
			TopOffBottomOn = 1,
			RightOffLeftOn = 2,
			BottomOffTopOn = 3
		}

		public UnityEvent onClickedWhileOn = new UnityEvent();

		public UnityEvent onClickedWhileOff = new UnityEvent();

		[SerializeField]
		private RectTransform m_slideContent;

		[Tooltip("When enabled, the isOn value is not toggled via a click/submit action.")]
		[SerializeField]
		private bool m_disableAutoToggle;

		[SerializeField]
		private SlideAxis m_slideAxis;

		[SerializeField]
		private float m_slideDuration = 0.15f;

		[Tooltip("Duration for which clicks are ignored after animating is completed. A negative value will allow clicking during the slide animation.")]
		[SerializeField]
		private float m_reactivateDelay;

		private Coroutine m_animation;

		private bool m_wasOn;

		private bool IsClickable
		{
			get
			{
				return m_reactivateDelay < 0f || !isAnimating;
			}
		}

		public SlideAxis slideAxis
		{
			get
			{
				return m_slideAxis;
			}
			set
			{
				if (m_slideAxis != value)
				{
					m_slideAxis = value;
					UpdateContentPosition(true);
				}
			}
		}

		public bool isAnimating
		{
			get
			{
				return m_animation != null;
			}
		}

		protected override void Start()
		{
			m_wasOn = base.isOn;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			StartCoroutine(LateEnable());
		}

		private IEnumerator LateEnable()
		{
			yield return null;
			UpdateContentPosition(false);
		}

		private void Update()
		{
			if (m_wasOn != base.isOn)
			{
				UpdateContentPosition(true);
				m_wasOn = base.isOn;
			}
		}

		private void UpdateContentPosition(bool animate)
		{
			if (m_slideContent == null)
			{
				return;
			}
			Vector2 startPos;
			Vector2 vector;
			if ((m_slideAxis == SlideAxis.LeftOffRightOn && base.isOn) || (slideAxis == SlideAxis.RightOffLeftOn && !base.isOn))
			{
				startPos = GetLeftPos(m_slideContent);
				vector = GetRightPos(m_slideContent);
			}
			else if ((m_slideAxis == SlideAxis.RightOffLeftOn && base.isOn) || (slideAxis == SlideAxis.LeftOffRightOn && !base.isOn))
			{
				startPos = GetRightPos(m_slideContent);
				vector = GetLeftPos(m_slideContent);
			}
			else if ((m_slideAxis == SlideAxis.TopOffBottomOn && base.isOn) || (slideAxis == SlideAxis.BottomOffTopOn && !base.isOn))
			{
				startPos = GetTopPos(m_slideContent);
				vector = GetBottomPos(m_slideContent);
			}
			else
			{
				startPos = GetBottomPos(m_slideContent);
				vector = GetTopPos(m_slideContent);
			}
			animate &= base.isActiveAndEnabled && m_slideDuration > 0f;
			if (animate)
			{
				if (m_animation != null)
				{
					StopCoroutine(m_animation);
				}
				m_animation = StartCoroutine(AnimateScroll(startPos, vector));
			}
			else
			{
				m_slideContent.anchoredPosition = vector;
			}
		}

		private IEnumerator AnimateScroll(Vector2 startPos, Vector2 targetPos)
		{
			Vector2 currentPos = m_slideContent.anchoredPosition;
			float elapsed = 0f;
			float distance = Vector2.Distance(startPos, targetPos);
			float factoredDuration = Vector2.Distance(currentPos, targetPos) / distance * m_slideDuration;
			while (elapsed < factoredDuration)
			{
				currentPos = Vector2.LerpUnclamped(startPos, targetPos, elapsed / factoredDuration);
				m_slideContent.anchoredPosition = currentPos;
				elapsed += Time.unscaledDeltaTime;
				yield return null;
			}
			m_slideContent.anchoredPosition = targetPos;
			yield return new WaitForSecondsRealtime(m_reactivateDelay);
			m_animation = null;
		}

		private static Vector2 GetLeftPos(RectTransform content)
		{
			Rect rect = content.parent.GetComponent<RectTransform>().rect;
			float num = (0f - content.anchorMin.x) * rect.width;
			float num2 = content.anchoredPosition.x - content.offsetMin.x;
			Vector2 result = new Vector2(num + num2, content.anchoredPosition.y);
			return result;
		}

		private static Vector2 GetRightPos(RectTransform content)
		{
			Rect rect = content.parent.GetComponent<RectTransform>().rect;
			float num = (1f - content.anchorMax.x) * rect.width;
			float num2 = content.anchoredPosition.x - content.offsetMax.x;
			Vector2 result = new Vector2(num + num2, content.anchoredPosition.y);
			return result;
		}

		private static Vector2 GetBottomPos(RectTransform content)
		{
			Rect rect = content.parent.GetComponent<RectTransform>().rect;
			float num = (0f - content.anchorMin.y) * rect.height;
			float num2 = content.anchoredPosition.y - content.offsetMin.y;
			Vector2 result = new Vector2(content.anchoredPosition.x, num + num2);
			return result;
		}

		private static Vector2 GetTopPos(RectTransform content)
		{
			Rect rect = content.parent.GetComponent<RectTransform>().rect;
			float num = (1f - content.anchorMax.y) * rect.height;
			float num2 = content.anchoredPosition.y - content.offsetMax.y;
			Vector2 result = new Vector2(content.anchoredPosition.x, num + num2);
			return result;
		}

		public override void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left && IsClickable)
			{
				if (base.isOn)
				{
					onClickedWhileOn.Invoke();
				}
				else
				{
					onClickedWhileOff.Invoke();
				}
				if (!m_disableAutoToggle)
				{
					base.OnPointerClick(eventData);
				}
			}
		}

		public override void OnSubmit(BaseEventData eventData)
		{
			if (IsClickable)
			{
				if (base.isOn)
				{
					onClickedWhileOn.Invoke();
				}
				else
				{
					onClickedWhileOff.Invoke();
				}
				if (!m_disableAutoToggle)
				{
					base.OnSubmit(eventData);
				}
			}
		}
	}
}
