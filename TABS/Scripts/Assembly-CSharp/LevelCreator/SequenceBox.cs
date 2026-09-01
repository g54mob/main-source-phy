using System.Collections;
using HighlightingSystem;
using Landfall.TABS.GameState;
using TFBGames;
using TMPro;
using UnityEngine;

namespace LevelCreator
{
	public class SequenceBox : TriggerBox
	{
		[SerializeField]
		private float m_minInterval = 1f;

		[SerializeField]
		private float m_maxInterval = 20f;

		[SerializeField]
		private GameObject m_timerDisplayObject;

		[SerializeField]
		private GameObject m_floorCanvas;

		private TMP_Text m_timerDisplayText;

		private float m_timer;

		private bool m_triggering;

		private IHighlight m_highlighter;

		private GameStateManager m_gameStateManager;

		private float CalculateInterval()
		{
			return Mathf.Lerp(m_minInterval, m_maxInterval, base.transform.rotation.eulerAngles.y / 360f);
		}

		private void Start()
		{
			m_timerDisplayText = m_timerDisplayObject.GetComponentInChildren<TMP_Text>();
			m_highlighter = GetComponent<IHighlight>();
			if (DMEditor.Instance == null)
			{
				m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
			}
		}

		private void Update()
		{
			float num = CalculateInterval();
			if (DMEditor.Instance != null)
			{
				m_timerDisplayObject.transform.position = DMEditor.Instance.playerCamera.WorldToScreenPoint(base.transform.position + Vector3.up * 2f);
				m_timerDisplayText.text = num.ToString("F1");
				if (m_highlighter == null)
				{
					m_timerDisplayObject.SetActive(value: false);
					m_highlighter = GetComponent<Highlighter>();
				}
				else
				{
					m_timerDisplayObject.SetActive(m_highlighter.IsHighlighted);
					m_floorCanvas.SetActive(m_highlighter.IsHighlighted);
				}
			}
			else
			{
				m_timerDisplayObject.SetActive(value: false);
				m_floorCanvas.SetActive(value: false);
				m_timer += Time.deltaTime;
				if (m_timer >= num && !m_triggering)
				{
					m_timer = 0f;
					Trigger(null);
				}
			}
		}

		public override void Trigger(Collider other)
		{
			if (m_gameStateManager != null && m_gameStateManager.GameState == GameState.BattleState)
			{
				StartCoroutine(TriggerSequence());
			}
			IEnumerator TriggerSequence()
			{
				m_triggering = true;
				foreach (GameObject playConnection in m_playConnections)
				{
					playConnection.GetComponent<ITriggerable>()?.Trigger();
					yield return new WaitForSeconds(CalculateInterval());
				}
				m_triggering = false;
			}
		}
	}
}
