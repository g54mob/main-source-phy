using UnityEngine;

namespace TFBGames
{
	public class WinConditionContentInspector : MonoBehaviour
	{
		private const int CLOSE_INPUT_BUFFER = 5;

		private CodeAnimation m_CodeAnimator;

		private WinConditionsBrowser[] m_ConditionBrowsers;

		private bool m_BufferClose;

		private bool m_SetToClose;

		private int m_ClosedTriggeredOnFrame;

		private bool m_IsOpen;

		public bool IsOpen => GetComponent<CanvasGroup>().interactable;

		private void Awake()
		{
			m_CodeAnimator = GetComponent<CodeAnimation>();
			m_CodeAnimator.InPlayed += SetAsOpen;
			m_CodeAnimator.OutPlayed += SetAsClosed;
			m_ConditionBrowsers = GetComponentsInChildren<WinConditionsBrowser>();
		}

		private void Update()
		{
			if (m_BufferClose && Time.frameCount >= m_ClosedTriggeredOnFrame + 5)
			{
				m_IsOpen = false;
				m_BufferClose = false;
			}
		}

		public void Open()
		{
			if (m_CodeAnimator != null)
			{
				m_CodeAnimator.PlayIn();
			}
			m_IsOpen = true;
			WinConditionsBrowser[] conditionBrowsers = m_ConditionBrowsers;
			for (int i = 0; i < conditionBrowsers.Length; i++)
			{
				conditionBrowsers[i].Open();
			}
		}

		public void Close()
		{
			if (m_CodeAnimator != null)
			{
				m_CodeAnimator.PlayOut();
			}
			SetAsClosed();
			WinConditionsBrowser[] conditionBrowsers = m_ConditionBrowsers;
			foreach (WinConditionsBrowser obj in conditionBrowsers)
			{
				obj.IsOpen = false;
				obj.WasReopened = false;
			}
		}

		private void SetAsClosed()
		{
			m_BufferClose = true;
			m_ClosedTriggeredOnFrame = Time.frameCount;
		}

		private void SetAsOpen()
		{
			m_IsOpen = true;
		}
	}
}
