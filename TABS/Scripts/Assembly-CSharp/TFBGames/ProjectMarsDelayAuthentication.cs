using System;
using BitCode.Networking;
using GamepadUI.StateManager.Core;
using UnityEngine;

namespace TFBGames
{
	public class ProjectMarsDelayAuthentication : MonoBehaviour
	{
		private const int DelayAuthenticationMaxFrames = 2;

		private Action<string, DelayUserAuthenticationCallback> m_doAuthentication;

		private int? m_delayFrames;

		private UISubMenu m_subMenu;

		private DelayUserAuthenticationCallback m_callback;

		private string m_regionCode;

		private void Update()
		{
			if (UpdateSchedule())
			{
				Clear();
				m_doAuthentication(m_regionCode, m_callback);
			}
		}

		public void Initialize(Action<string, DelayUserAuthenticationCallback> doAuthenticationAction)
		{
			m_doAuthentication = doAuthenticationAction;
			base.enabled = false;
		}

		public void ScheduleUserAuthentication(UISubMenu subMenu, IGameInvitation invitation, DelayUserAuthenticationCallback callback)
		{
			base.enabled = true;
			m_delayFrames = 2;
			m_subMenu = subMenu;
			m_regionCode = null;
			m_callback = callback;
		}

		public void Clear()
		{
			base.enabled = false;
			m_delayFrames = null;
		}

		private bool UpdateSchedule()
		{
			if (!m_delayFrames.HasValue)
			{
				return false;
			}
			if (m_subMenu != null && m_subMenu.IsAnimationOpenAndPlaying)
			{
				return false;
			}
			m_delayFrames--;
			return m_delayFrames.Value <= 0;
		}
	}
}
