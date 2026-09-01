using System;
using System.Collections;
using System.Collections.Generic;
using Landfall.TABS.GameMode;
using UnityEngine;

namespace Landfall.TABS.GameState
{
	public class GameStateManager : IService
	{
		private GameState m_previousGameState = GameState.None;

		private GameState m_currentGameState = GameState.None;

		private List<GameStateListener> m_listeners = new List<GameStateListener>();

		private MonoBehaviour m_coroutineHook;

		public GameState PreviousGameState => m_previousGameState;

		public GameState GameState
		{
			get
			{
				return m_currentGameState;
			}
			protected set
			{
				Debug.Log("Setting GameState to " + value);
				if (m_currentGameState != value)
				{
					m_previousGameState = m_currentGameState;
					m_currentGameState = value;
					Debug.Log("Changed from " + m_previousGameState.ToString() + " to " + m_currentGameState);
					SendOutCallbacks();
				}
				else if (value != GameState.None)
				{
					Debug.LogWarning("Something tried to change the GameState to the same as it already is!");
				}
			}
		}

		public event Action<GameState> GameStateChanged;

		public void RegisterListener(GameStateListener listener)
		{
			m_listeners.Add(listener);
		}

		public void UnregisterListener(GameStateListener listener)
		{
			m_listeners.Remove(listener);
		}

		public void EnterNewScene()
		{
			int count = m_listeners.Count;
			for (int i = 0; i < count; i++)
			{
				m_listeners[i].OnEnterNewScene();
			}
		}

		public void EnterBattleState()
		{
			m_coroutineHook.StartCoroutine(ChangeState(GameState.BattleState));
		}

		public void EnterPlacementState()
		{
			m_coroutineHook.StartCoroutine(ChangeState(GameState.PlacementState));
		}

		public void EnterNoneState()
		{
			m_coroutineHook.StartCoroutine(ChangeState(GameState.None));
		}

		private IEnumerator ChangeState(GameState newState)
		{
			GameState = newState;
			yield return null;
		}

		private void SendOutCallbacks()
		{
			this.GameStateChanged?.Invoke(GameState);
			if (m_listeners == null)
			{
				return;
			}
			int count = m_listeners.Count;
			for (int i = 0; i < count; i++)
			{
				GameStateListener gameStateListener = m_listeners[i];
				switch (m_previousGameState)
				{
				case GameState.PlacementState:
					gameStateListener.OnExitPlacementState();
					break;
				case GameState.BattleState:
					gameStateListener.OnExitBattleState();
					break;
				}
				switch (m_currentGameState)
				{
				case GameState.PlacementState:
					gameStateListener.OnEnterPlacementStateEarly();
					gameStateListener.OnEnterPlacementState();
					break;
				case GameState.BattleState:
					gameStateListener.OnEnterBattleState();
					break;
				}
			}
		}

		public void OnMatchOver()
		{
			foreach (GameStateListener listener in m_listeners)
			{
				listener.OnMatchOver();
			}
		}

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
			m_coroutineHook = ServiceLocator.GetService<GameModeService>();
		}

		public void OnStart()
		{
		}

		public void OnUpdate()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void UnRegister()
		{
		}
	}
}
