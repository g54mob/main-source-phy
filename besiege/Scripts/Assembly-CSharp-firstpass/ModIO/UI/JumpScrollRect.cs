using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	public class JumpScrollRect : MonoBehaviour
	{
		public enum ButtonInteractivity
		{
			DoNothing = 0,
			AutoDisable = 1,
			AutoHide = 2
		}

		public const float JUMP_TOLERANCE = 0.0001f;

		[Header("Settings")]
		[SerializeField]
		private ButtonInteractivity m_buttonInteractivity = ButtonInteractivity.AutoHide;

		[SerializeField]
		private Button m_jumpLeftButton;

		[SerializeField]
		private Button m_jumpRightButton;

		[SerializeField]
		[Tooltip("If at first/last anchor, set the jump target this amount beyond the anchor")]
		private float m_overshootAmount;

		[Header("UI Components")]
		public RectTransform viewport;

		public RectTransform content;

		private Coroutine m_updateCoroutine;

		private void OnEnable()
		{
			if (m_jumpLeftButton != null)
			{
				m_jumpLeftButton.onClick.AddListener(JumpLeft);
			}
			if (m_jumpRightButton != null)
			{
				m_jumpRightButton.onClick.AddListener(JumpRight);
			}
			if (m_buttonInteractivity != ButtonInteractivity.DoNothing)
			{
				m_updateCoroutine = StartCoroutine(UpdateButtonsCoroutine());
			}
		}

		private void OnDisable()
		{
			if (m_jumpLeftButton != null)
			{
				m_jumpLeftButton.onClick.RemoveListener(JumpLeft);
			}
			if (m_jumpRightButton != null)
			{
				m_jumpRightButton.onClick.RemoveListener(JumpRight);
			}
			if (m_updateCoroutine != null)
			{
				StopCoroutine(m_updateCoroutine);
				m_updateCoroutine = null;
			}
		}

		public void JumpLeft()
		{
			JumpInternal(true, false);
		}

		public void JumpRight()
		{
			JumpInternal(true, true);
		}

		private void JumpInternal(bool horizontal, bool positiveDir)
		{
			if (content == null || viewport == null)
			{
				return;
			}
			JumpScrollAnchor[] componentsInChildren = content.GetComponentsInChildren<JumpScrollAnchor>();
			List<Vector2> list = CalcRelativePositions(viewport, componentsInChildren);
			int num = ((!horizontal) ? 1 : 0);
			Vector2 vector;
			int index2;
			float num2;
			if (!positiveDir)
			{
				vector = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
				foreach (Vector2 item in list)
				{
					bool flag = item[num] < -0.0001f;
					bool flag2 = vector[num] < item[num];
					if (flag && flag2)
					{
						vector = item;
					}
				}
				if (vector[num] == float.NegativeInfinity)
				{
					vector = Vector2.zero;
					int index = (index2 = num);
					num2 = vector[index2];
					vector[index] = num2 - m_overshootAmount;
				}
			}
			else
			{
				vector = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
				foreach (Vector2 item2 in list)
				{
					bool flag3 = 0.0001f < item2[num];
					bool flag4 = item2[num] < vector[num];
					if (flag3 && flag4)
					{
						vector = item2;
					}
				}
				if (vector[num] == float.PositiveInfinity)
				{
					vector = Vector2.zero;
					int index3 = (index2 = num);
					num2 = vector[index2];
					vector[index3] = num2 + m_overshootAmount;
				}
			}
			Vector2 anchoredPosition = content.anchoredPosition;
			int index4 = (index2 = num);
			num2 = anchoredPosition[index2];
			anchoredPosition[index4] = num2 - vector[num];
			content.anchoredPosition = anchoredPosition;
		}

		private static List<Vector2> CalcRelativePositions(RectTransform rootTransform, IEnumerable<JumpScrollAnchor> jumpAnchors)
		{
			List<Vector2> list = new List<Vector2>();
			foreach (JumpScrollAnchor jumpAnchor in jumpAnchors)
			{
				List<RectTransform> list2 = new List<RectTransform>();
				RectTransform rectTransform = jumpAnchor.transform as RectTransform;
				while (rectTransform != null && rectTransform != rootTransform)
				{
					list2.Add(rectTransform);
					rectTransform = rectTransform.parent as RectTransform;
				}
				if (rectTransform != rootTransform)
				{
					Debug.LogWarning("[mod.io] Attempted to calculate offset of non-child JumpScrollAnchor: " + jumpAnchor.name, rootTransform);
					continue;
				}
				Vector2 zero = Vector2.zero;
				RectTransform rectTransform2 = rootTransform;
				for (int num = list2.Count - 1; num >= 0; num--)
				{
					rectTransform = list2[num];
					Vector2 vector = new Vector2
					{
						x = rectTransform.anchorMin.x * rectTransform2.rect.width + rectTransform.offsetMin.x,
						y = rectTransform.anchorMin.y * rectTransform2.rect.height + rectTransform.offsetMin.y
					};
					zero.x += vector.x;
					zero.y += vector.y;
					rectTransform2 = rectTransform;
				}
				zero.x += rectTransform.pivot.x * rectTransform.rect.width;
				zero.y += rectTransform.pivot.y * rectTransform.rect.height;
				list.Add(zero);
			}
			return list;
		}

		private IEnumerator UpdateButtonsCoroutine()
		{
			while (Application.isPlaying)
			{
				yield return null;
				if (viewport != null && content != null)
				{
					bool horizontalMovement = viewport.rect.width < content.rect.width;
					bool hInteractable = horizontalMovement || m_buttonInteractivity != ButtonInteractivity.AutoDisable;
					bool hActive = horizontalMovement || m_buttonInteractivity != ButtonInteractivity.AutoHide;
					m_jumpLeftButton.interactable = hInteractable;
					m_jumpRightButton.interactable = hInteractable;
					m_jumpLeftButton.gameObject.SetActive(hActive);
					m_jumpRightButton.gameObject.SetActive(hActive);
				}
			}
		}
	}
}
