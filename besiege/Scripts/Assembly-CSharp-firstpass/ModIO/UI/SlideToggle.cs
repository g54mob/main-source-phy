using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ModIO.UI
{
	[Obsolete("Use ModIO.UI.SlidingToggle instead.")]
	public class SlideToggle : StateToggleDisplay, IPointerExitHandler, IEventSystemHandler
	{
		public enum SlideAxis
		{
			Horizontal = 0,
			Vertical = 1
		}

		[Tooltip("Should the slide button untoggle when the user moves the mouse away?")]
		[Header("Settings")]
		[SerializeField]
		private bool m_untoggleOnMouseExit;

		[SerializeField]
		private SlideAxis m_slideAxis;

		[SerializeField]
		private float m_slideDuration = 0.15f;

		[SerializeField]
		[Tooltip("Set duration to block clicks for after the slide animation")]
		private float m_reactivateDelay = 0.05f;

		[Header("UI Components")]
		[SerializeField]
		private RectTransform content;

		[SerializeField]
		[Header("Display Data")]
		private bool m_isOn;

		private GameObject m_clickBlocker;

		private Coroutine m_animation;

		public override bool isOn
		{
			get
			{
				return m_isOn;
			}
			set
			{
				if (m_isOn != value)
				{
					m_isOn = value;
					UpdateScroll(true);
				}
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
					UpdateScroll(true);
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

		private void OnEnable()
		{
			if (m_clickBlocker == null)
			{
				m_clickBlocker = new GameObject("Click Blocker", typeof(RectTransform));
				RectTransform component = m_clickBlocker.GetComponent<RectTransform>();
				component.SetParent(content);
				component.localScale = Vector3.one;
				component.anchorMin = Vector2.zero;
				component.anchorMax = Vector2.one;
				component.offsetMin = Vector2.zero;
				component.offsetMax = Vector2.zero;
				m_clickBlocker.AddComponent<CanvasRenderer>();
				m_clickBlocker.AddComponent<Touchable>();
				m_clickBlocker.SetActive(false);
			}
			StartCoroutine(LateEnable());
		}

		private IEnumerator LateEnable()
		{
			yield return null;
			UpdateScroll(false);
		}

		private void OnDisable()
		{
			if (m_untoggleOnMouseExit)
			{
				isOn = false;
			}
		}

		private void UpdateScroll(bool animate)
		{
			if (content == null)
			{
				return;
			}
			Vector2 startPos;
			Vector2 vector;
			if (m_slideAxis == SlideAxis.Horizontal)
			{
				if (m_isOn)
				{
					startPos = GetLeftPos(content);
					vector = GetRightPos(content);
				}
				else
				{
					startPos = GetRightPos(content);
					vector = GetLeftPos(content);
				}
			}
			else if (m_isOn)
			{
				startPos = GetBottomPos(content);
				vector = GetTopPos(content);
			}
			else
			{
				startPos = GetTopPos(content);
				vector = GetBottomPos(content);
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
				content.anchoredPosition = vector;
			}
		}

		private IEnumerator AnimateScroll(Vector2 startPos, Vector2 targetPos)
		{
			Vector2 currentPos = content.anchoredPosition;
			float elapsed = 0f;
			float distance = Vector2.Distance(startPos, targetPos);
			float factoredDuration = Vector2.Distance(currentPos, targetPos) / distance * m_slideDuration;
			m_clickBlocker.SetActive(true);
			while (elapsed < factoredDuration)
			{
				currentPos = Vector2.LerpUnclamped(startPos, targetPos, elapsed / factoredDuration);
				content.anchoredPosition = currentPos;
				elapsed += Time.unscaledDeltaTime;
				yield return null;
			}
			content.anchoredPosition = targetPos;
			yield return new WaitForSecondsRealtime(m_reactivateDelay);
			m_clickBlocker.SetActive(false);
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

		public void OnPointerExit(PointerEventData pointerEventData)
		{
			if (m_untoggleOnMouseExit)
			{
				isOn = false;
			}
		}
	}
}
