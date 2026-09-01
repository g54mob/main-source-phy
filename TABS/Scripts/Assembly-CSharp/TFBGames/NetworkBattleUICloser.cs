using System;
using System.Collections.Generic;
using GamepadUI.StateManager.Core;
using Landfall.TABS.GameMode;
using UnityEngine;

namespace TFBGames
{
	public class NetworkBattleUICloser : MonoBehaviour
	{
		private OnlineMultiplayerGameMode m_multiplayerGameMode;

		private readonly Dictionary<UIComponent, Action> m_componentsToClose = new Dictionary<UIComponent, Action>();

		private bool m_isClosingUI;

		public static NetworkBattleUICloser Instance { get; private set; }

		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			BaseGameMode currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			m_multiplayerGameMode = currentGameMode as OnlineMultiplayerGameMode;
			if (m_multiplayerGameMode != null)
			{
				m_multiplayerGameMode.MatchEnded += OnMultiplayerMatchEnded;
			}
		}

		private void OnDestroy()
		{
			Instance = null;
			if (m_multiplayerGameMode != null)
			{
				m_multiplayerGameMode.MatchEnded -= OnMultiplayerMatchEnded;
			}
		}

		private void Update()
		{
			UpdateCloseUI();
		}

		public void RegisterComponent(UIComponent component, Action closeCallback)
		{
			m_componentsToClose[component] = closeCallback;
		}

		public void UnregisterComponent(UIComponent component)
		{
			m_componentsToClose.Remove(component);
		}

		private void UpdateCloseUI()
		{
			if (!m_isClosingUI)
			{
				return;
			}
			foreach (KeyValuePair<UIComponent, Action> item in m_componentsToClose)
			{
				UIComponent key = item.Key;
				Action value = item.Value;
				if (key != null && key.State == UIState.Open && value != null)
				{
					value();
					return;
				}
			}
			m_isClosingUI = false;
		}

		private void OnMultiplayerMatchEnded()
		{
			m_isClosingUI = true;
		}
	}
}
