using System.Collections;
using TFBGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class SimpleButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
	{
		[SerializeField]
		protected Image m_highlight;

		public float m_selectedScale = 1f;

		[SerializeField]
		protected Color lockedColor;

		[SerializeField]
		protected bool m_OverrideNormalColor;

		[SerializeField]
		protected Color m_OverrideColor;

		[SerializeField]
		protected bool m_OverrideHighlight;

		[SerializeField]
		protected Color m_OverrideHighlightColor;

		[SerializeField]
		protected Sprite m_normalSprite;

		[SerializeField]
		protected Sprite m_highlightSprite;

		protected Color m_highlightedColor;

		protected Color m_targetColor;

		protected Color m_normalColor;

		protected MainCam m_mainCam;

		protected Transform m_mainCamTransform;

		public bool overrideSelection;

		protected bool disabled;

		public bool IsHighlighted { get; private set; }

		public virtual bool Unlocked { get; set; }

		protected virtual void Start()
		{
			if (!m_OverrideNormalColor)
			{
				if (SceneSettings.UseSceneColorOverwrite)
				{
					m_normalColor = SceneSettings.GetBackgroundColor();
				}
				else
				{
					m_normalColor = UIStyleManager.GetStyle().m_BackgroundColor;
				}
			}
			else
			{
				m_normalColor = m_OverrideColor;
			}
			if (!m_OverrideHighlight)
			{
				m_highlightedColor = UIStyleManager.GetStyle().m_HighlightedColor;
			}
			else
			{
				m_highlightedColor = m_OverrideHighlightColor;
			}
			if (m_highlight != null)
			{
				m_highlight.color = m_normalColor;
			}
			m_mainCam = ServiceLocator.GetService<PlayerCamerasManager>()?.GetMainCam(TFBGames.Player.One);
			m_mainCamTransform = ((m_mainCam != null) ? m_mainCam.transform : null);
		}

		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			OnEnter();
		}

		public virtual void OnPointerExit(PointerEventData eventData)
		{
			OnExit();
		}

		public void HighlightAndScale(Color highglight, bool scaleUp)
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			IsHighlighted = scaleUp;
			if (!overrideSelection)
			{
				if (m_highlightSprite != null && m_normalSprite != null)
				{
					m_highlight.sprite = (scaleUp ? m_highlightSprite : m_normalSprite);
				}
				m_targetColor = highglight;
				StopAllCoroutines();
				StartCoroutine(LerpColors(scaleUp));
			}
		}

		public void DisableButton()
		{
			if (!disabled)
			{
				OnPointerExit(null);
				disabled = true;
			}
		}

		public void EnableButton()
		{
			if (disabled)
			{
				disabled = false;
				OnPointerExit(null);
			}
		}

		public virtual void OnEnter()
		{
			HighlightAndScale(m_highlightedColor, scaleUp: true);
		}

		public virtual void OnExit()
		{
			HighlightAndScale(m_normalColor, scaleUp: false);
		}

		protected IEnumerator LerpColors(bool enter)
		{
			float timer = 0f;
			float fromScale = 1f;
			float toScale = 1f;
			if (enter)
			{
				toScale = m_selectedScale;
			}
			else
			{
				fromScale = m_selectedScale;
			}
			while (timer < 0.15f)
			{
				if ((bool)m_highlight)
				{
					m_highlight.color = Color.Lerp(m_highlight.color, m_targetColor, 20f * Time.unscaledDeltaTime);
				}
				base.transform.localScale = Vector3.Slerp(Vector3.one * fromScale, Vector3.one * toScale, timer / 0.15f);
				timer += Time.unscaledDeltaTime;
				yield return null;
			}
			if ((bool)m_highlight)
			{
				m_highlight.color = m_targetColor;
			}
			base.transform.localScale = Vector3.one * toScale;
		}

		public void OnSelect(BaseEventData eventData)
		{
			OnEnter();
		}

		public void OnDeselect(BaseEventData eventData)
		{
			OnExit();
		}
	}
}
