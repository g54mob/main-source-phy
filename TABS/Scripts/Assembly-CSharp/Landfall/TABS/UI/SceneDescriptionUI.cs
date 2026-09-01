using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using LevelCreator;
using UIStateManager;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UI
{
	public class SceneDescriptionUI : MonoBehaviour
	{
		[SerializeField]
		private Button m_mapSelectionButton;

		[SerializeField]
		private Button m_returnToEditorButton;

		private LocalizeText m_descriptionText;

		private CodeAnimation m_animation;

		private InterfaceStateManager m_interfaceStateManager;

		private bool m_firstHide;

		private void Awake()
		{
			m_descriptionText = GetComponentInChildren<LocalizeText>();
			m_animation = GetComponent<CodeAnimation>();
			m_interfaceStateManager = Object.FindObjectOfType<InterfaceStateManager>();
		}

		private void SetDescriptionText()
		{
			if (ServiceLocator.GetService<GameModeService>().CurrentGameMode.GetType() == typeof(CampaignGameMode))
			{
				TABSCampaignLevelAsset currentLevel = CampaignPlayerDataHolder.GetCurrentLevel();
				m_descriptionText.LocaleID = currentLevel.Entity.Name;
			}
			else if (SpawnLevel.IsCustomLevelScene && !SpawnLevel.IsCustomLevelTestRun && SpawnLevel.CustomMap != null)
			{
				m_descriptionText.LocaleID = SpawnLevel.CustomMap.Entity.Name;
			}
			else if (TABSSceneManager.CurrentLoadedMap != null)
			{
				string text = TABSSceneManager.CurrentLoadedMap.Entity.Name;
				if (text == "LevelCreator")
				{
					text = "BUTTON_MAKELEVEL";
				}
				m_descriptionText.LocaleID = text;
			}
		}

		private void Start()
		{
			SetDescriptionText();
			if (SpawnLevel.IsCustomLevelScene)
			{
				m_mapSelectionButton.onClick.AddListener(delegate
				{
					SpawnLevel.ReturnToEditor();
				});
			}
			if (SpawnLevel.IsCustomLevelTestRun)
			{
				m_mapSelectionButton.gameObject.SetActive(value: false);
				m_returnToEditorButton.gameObject.SetActive(value: true);
				m_returnToEditorButton.onClick.AddListener(delegate
				{
					SpawnLevel.ReturnToEditor();
				});
			}
			else
			{
				m_mapSelectionButton.gameObject.SetActive(value: true);
				m_returnToEditorButton.gameObject.SetActive(value: false);
			}
		}

		private void Update()
		{
			if (SpawnLevel.IsCustomLevelTestRun && SpawnLevel.finishedPathfindingScan && (bool)PlayerActions.Instance.m_mapSelect && m_interfaceStateManager.IsDefaultState)
			{
				SpawnLevel.ReturnToEditor();
			}
		}

		public void Hide()
		{
			if (m_animation.currentState != CodeAnimationInstance.AnimationUse.Out && (ServiceLocator.GetService<GameStateManager>().GameState == Landfall.TABS.GameState.GameState.PlacementState || !m_firstHide))
			{
				m_firstHide = true;
				m_animation.PlayOut();
			}
		}

		public void Show()
		{
			GameStateManager service = ServiceLocator.GetService<GameStateManager>();
			if (!ServiceLocator.GetService<GameModeService>().CurrentGameMode.IsInFreeLook && service.GameState != Landfall.TABS.GameState.GameState.BattleState)
			{
				m_animation.PlayIn();
				m_firstHide = false;
			}
		}
	}
}
