using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class PreviewRenderer : MonoBehaviour
	{
		private static PreviewRenderer instance;

		private PlacementUI m_placementUI;

		public float m_Spring = 100f;

		public float m_Damper = 10f;

		public float m_ScaleSpring = 10f;

		public float m_ScaleDamper = 3f;

		private bool m_isAlive;

		private Vector3 m_velocity;

		private Vector3 m_targetPosistion;

		private Vector3 m_scaleVelocity;

		private Vector3 m_scaleTarget;

		private RawImage m_renderTexture;

		private void Awake()
		{
			instance = this;
			m_renderTexture = GetComponent<RawImage>();
		}

		private void Start()
		{
			m_placementUI = Object.FindObjectOfType<PlacementUI>();
		}

		public static void SetPosistion(Vector2 mousePosistion)
		{
			instance.SetPreviewPosistion(mousePosistion);
		}

		private void Update()
		{
			float num = Mathf.Clamp(Time.deltaTime, 0f, 0.02f);
			Vector3 vector = (m_scaleTarget - base.transform.localScale) * num * m_ScaleSpring;
			m_scaleVelocity += vector;
			m_scaleVelocity -= m_scaleVelocity * num * m_ScaleDamper;
			base.transform.localScale += m_scaleVelocity * num;
			if (m_isAlive)
			{
				Vector3 vector2 = (m_targetPosistion - base.transform.position) * num * m_Spring;
				m_velocity += vector2;
				m_velocity -= m_velocity * num * m_Damper;
				base.transform.position += m_velocity * num;
			}
		}

		public void SetPreviewPosistion(Vector3 posistion)
		{
			posistion += Vector3.up * Screen.height * 0.02f;
			RectTransform rectTransform = (RectTransform)base.transform;
			if (posistion.x + 15f >= m_placementUI.FactionXMin && posistion.x - 15f <= m_placementUI.FactionXMax)
			{
				posistion += Vector3.up * Screen.height * 0.06f;
			}
			rectTransform.position = posistion;
			if (rectTransform.position.x >= m_placementUI.FactionXMin)
			{
				_ = rectTransform.position.x;
				_ = m_placementUI.FactionXMax;
			}
			if (!m_isAlive)
			{
				base.transform.localScale = Vector3.one * 0f;
				m_isAlive = true;
			}
			else
			{
				m_scaleVelocity += Vector3.one * 5f;
			}
			m_targetPosistion = posistion;
		}

		public static void Hide()
		{
			instance.StartCoroutine(instance.TurnOfTexture(0.15f));
			instance.m_scaleTarget = Vector3.zero;
			instance.m_isAlive = false;
		}

		private IEnumerator TurnOfTexture(float delay)
		{
			yield return new WaitForSeconds(delay);
			instance.m_renderTexture.enabled = false;
		}

		public static void Show()
		{
			instance.m_scaleTarget = Vector3.one;
			instance.StopAllCoroutines();
			instance.m_renderTexture.enabled = true;
			instance.m_isAlive = true;
		}
	}
}
