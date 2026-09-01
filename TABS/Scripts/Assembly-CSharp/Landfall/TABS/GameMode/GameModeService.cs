using System;
using Landfall.TABS.GameState;
using UnityEngine;
using UnityEngine.Audio;

namespace Landfall.TABS.GameMode
{
	public class GameModeService : ServicePrefab
	{
		[Serializable]
		public class AudioMorphSettings
		{
			[SerializeField]
			public AudioMixer AudioMixer;

			[SerializeField]
			public AnimationCurve LowpassCurve;
		}

		[SerializeField]
		private AudioMorphSettings m_audioMorphSettings;

		private BaseGameMode m_currentGameMode;

		public AudioMorphSettings AudioSettings => m_audioMorphSettings;

		public BaseGameMode CurrentGameMode => m_currentGameMode;

		public event Action<BaseGameMode> GameModeSet;

		private void Awake()
		{
			GameStateListenerSurrogate gameStateListenerSurrogate = base.gameObject.AddComponent<GameStateListenerSurrogate>();
			gameStateListenerSurrogate.OnEnterNewSceneDelegate = (GameStateListenerSurrogate.SurrogateCallback)Delegate.Combine(gameStateListenerSurrogate.OnEnterNewSceneDelegate, new GameStateListenerSurrogate.SurrogateCallback(OnEnterNewScene));
			gameStateListenerSurrogate.OnEnterBattleStateCallback = (GameStateListenerSurrogate.SurrogateCallback)Delegate.Combine(gameStateListenerSurrogate.OnEnterBattleStateCallback, new GameStateListenerSurrogate.SurrogateCallback(OnEnterBattleState));
			gameStateListenerSurrogate.OnEnterPlacementStateDelegate = (GameStateListenerSurrogate.SurrogateCallback)Delegate.Combine(gameStateListenerSurrogate.OnEnterPlacementStateDelegate, new GameStateListenerSurrogate.SurrogateCallback(OnEnterPlacementState));
			gameStateListenerSurrogate.OnExitBattleStateCallback = (GameStateListenerSurrogate.SurrogateCallback)Delegate.Combine(gameStateListenerSurrogate.OnExitBattleStateCallback, new GameStateListenerSurrogate.SurrogateCallback(OnExitBattleState));
			gameStateListenerSurrogate.OnExitPlacementStateDelegate = (GameStateListenerSurrogate.SurrogateCallback)Delegate.Combine(gameStateListenerSurrogate.OnExitPlacementStateDelegate, new GameStateListenerSurrogate.SurrogateCallback(OnExitPlacementState));
			SetGameMode<MainMenuGameMode>();
		}

		public void SetGameMode<T>() where T : BaseGameMode
		{
			Debug.Log("Initiating GameMode: " + typeof(T).Name);
			m_currentGameMode?.Destroy();
			m_currentGameMode = null;
			BaseGameMode baseGameMode = (m_currentGameMode = Activator.CreateInstance<T>());
			baseGameMode.GameModeService = this;
			this.GameModeSet?.Invoke(baseGameMode);
			baseGameMode.Start();
		}

		public bool IsCurrentBaseGameModeType<T>() where T : BaseGameMode
		{
			return m_currentGameMode.GetType() == typeof(T);
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			m_currentGameMode?.Update();
		}

		public void OnEnterNewScene()
		{
			m_currentGameMode?.OnEnterNewScene();
		}

		public void OnEnterBattleState()
		{
			m_currentGameMode?.OnEnterBattleState();
		}

		public void OnEnterPlacementState()
		{
			m_currentGameMode?.OnEnterPlacementState();
		}

		public void OnExitBattleState()
		{
			m_currentGameMode?.OnExitBattleState();
		}

		public void OnExitPlacementState()
		{
			m_currentGameMode?.OnExitPlacementState();
		}

		public bool IsGameModeRestricted()
		{
			if (m_currentGameMode == null)
			{
				return false;
			}
			if (!(m_currentGameMode is OnlineMultiplayerGameMode))
			{
				return m_currentGameMode is LocalMultiplayerGameMode;
			}
			return true;
		}
	}
}
