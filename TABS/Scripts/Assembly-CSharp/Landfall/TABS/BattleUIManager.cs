using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class BattleUIManager : GameStateListener
	{
		[SerializeField]
		private PlacementUI m_PlacementUI;

		[SerializeField]
		[Tooltip("Value to check against to use width or height as the reference dimension.")]
		private float m_WidthOrHeightThreshold = 0.5f;

		private LocalMultiplayerGameMode m_LocalMultiplayerGameMode;

		private BaseGameMode m_currentGameMode;

		private CanvasScaler m_CanvasScaler;

		private int m_CurrentScreenWidth;

		private int m_CurrentScreenHeight;

		private bool m_ResolutionChangedThisFrame;

		protected override void Awake()
		{
			base.Awake();
			m_currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.LocalMultiplayer)
			{
				m_LocalMultiplayerGameMode = (LocalMultiplayerGameMode)m_currentGameMode;
			}
			m_CanvasScaler = GetComponent<CanvasScaler>();
		}

		public override void OnEnterPlacementState()
		{
			m_PlacementUI.EnterPlacementState();
		}

		public override void OnEnterBattleState()
		{
			m_PlacementUI.EnterBattleState();
		}

		public override void OnEnterNewScene()
		{
			base.OnEnterNewScene();
			m_ResolutionChangedThisFrame = true;
		}

		public override void OnExitBattleState()
		{
			base.OnExitBattleState();
		}

		public override void OnExitPlacementState()
		{
			base.OnExitPlacementState();
		}

		public override void OnEnterPlacementStateEarly()
		{
			base.OnEnterPlacementStateEarly();
		}

		private void Update()
		{
			int width = Screen.width;
			int height = Screen.height;
			if (m_CurrentScreenWidth != width || m_CurrentScreenHeight != height)
			{
				m_ResolutionChangedThisFrame = true;
			}
			if (m_ResolutionChangedThisFrame)
			{
				UpdateRadialScaleFactor(width, height);
			}
		}

		private void UpdateRadialScaleFactor(int width, int height)
		{
			float radialMenuScale = CalculateScaleFactor();
			if (m_PlacementUI != null)
			{
				m_PlacementUI.SetRadialMenuScale(radialMenuScale);
			}
			m_CurrentScreenWidth = width;
			m_CurrentScreenHeight = height;
			m_ResolutionChangedThisFrame = false;
		}

		private float CalculateScaleFactor()
		{
			if (m_CanvasScaler == null)
			{
				return 1f;
			}
			Vector2 referenceResolution = m_CanvasScaler.referenceResolution;
			bool flag = m_CanvasScaler.matchWidthOrHeight < m_WidthOrHeightThreshold;
			float num = Screen.width;
			float num2 = referenceResolution.x;
			if (flag && referenceResolution.x > (float)Screen.width)
			{
				num = Screen.width;
				num2 = referenceResolution.x;
			}
			else if (!flag && referenceResolution.y > (float)Screen.height)
			{
				num = Screen.height;
				num2 = referenceResolution.y;
			}
			return num / num2;
		}
	}
}
