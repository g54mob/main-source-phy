using System.Collections.Generic;
using System.Linq;
using InControl;
using Landfall.TABS;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LevelCreator
{
	public class StartMenu : DMUIPanel
	{
		public enum StartMenuBackState
		{
			ToMainMenu = 0,
			ToCustomContent = 1,
			ToEditor = 2
		}

		[SerializeField]
		private Transform m_recentLevelsParent;

		[SerializeField]
		private GameObject m_recentLevelsTemplate;

		[SerializeField]
		private GameObject m_recentLevelsEmptyTemplate;

		private const int m_maxRecentLevels = 10;

		public static StartMenuBackState backState { get; private set; }

		public static void SetBackButtonState(StartMenuBackState state)
		{
			backState = state;
		}

		public override void OnOpen()
		{
			base.OnOpen();
			BuildRecentLevels();
		}

		private void Start()
		{
			AssertionCheck();
			AssignInput();
		}

		private void OnEnable()
		{
			PlayerActions.Instance.OnLastInputTypeChanged += OnLastInputTypeChanged;
		}

		private void OnDisable()
		{
			PlayerActions.Instance.OnLastInputTypeChanged -= OnLastInputTypeChanged;
		}

		private void OnLastInputTypeChanged(BindingSourceType obj)
		{
			if (PlayerActions.Instance.InputType == InputType.Controller && m_defaultObject != null)
			{
				EventSystem.current.SetSelectedGameObject(m_defaultObject.gameObject);
			}
		}

		private void AssertionCheck()
		{
		}

		private void AssignInput()
		{
			PlayerActions instance = PlayerActions.Instance;
			m_inputState.AddOnKeyDownListener(instance.m_back, delegate
			{
				Back();
			});
			m_inputState.AddOnKeyDownListener(instance.m_enterExitBattle, delegate
			{
				Back();
			});
		}

		public void OpenLoadLevel()
		{
			UIUtil.AskForConfirmationFirstIfEditorHasDirtyUserLevel(delegate
			{
				CustomContetnManager.returnToMapCreator = true;
				UnitCreatorFactionBrowser.selectedTab = 4;
				TABSSceneManager.LoadCustomContentPage();
			});
		}

		public void OpenNewLevel()
		{
			UIUtil.AskForConfirmationFirstIfEditorHasDirtyUserLevel(delegate
			{
				if (DMUIManager.Instance != null)
				{
					DMUIManager.Instance.OpenPanel(DMUIManager.UIPanels.NewLevelMenu, clearModalSelection: true);
				}
			});
		}

		public void Back()
		{
			switch (backState)
			{
			case StartMenuBackState.ToMainMenu:
				TABSSceneManager.LoadMainMenu();
				break;
			case StartMenuBackState.ToCustomContent:
				TABSSceneManager.LoadCustomContentPage();
				break;
			case StartMenuBackState.ToEditor:
				DMUIManager.Instance.PopPanel();
				break;
			}
		}

		private void BuildRecentLevels()
		{
			foreach (Transform item in m_recentLevelsParent.transform)
			{
				Object.Destroy(item.gameObject);
			}
			LevelUtility.WithRecentLevelPaths(delegate(IEnumerable<string> levelPathsEnum)
			{
				HashSet<DatabaseID> hashSet = new HashSet<DatabaseID>();
				string[] array = levelPathsEnum.ToArray();
				for (int i = 0; i < 10; i++)
				{
					string levelPath = null;
					string text = null;
					for (int j = i; j <= array.Length; j++)
					{
						levelPath = ((j < array.Length) ? array[j] : null);
						if (levelPath != null)
						{
							CustomMap customMapFromLevelPath = LevelUtility.GetCustomMapFromLevelPath(levelPath);
							if (customMapFromLevelPath != null && customMapFromLevelPath.Entity != null && !hashSet.Contains(customMapFromLevelPath.Entity.GUID))
							{
								text = customMapFromLevelPath.Entity.Name;
								hashSet.Add(customMapFromLevelPath.Entity.GUID);
								break;
							}
						}
					}
					float num = ((((i >= 5) ? (i + 1) : i) % 2 == 0) ? 1f : 0.6f);
					if (text != null)
					{
						GameObject obj = Object.Instantiate(m_recentLevelsTemplate, m_recentLevelsParent);
						obj.GetComponentInChildren<Image>().color *= num;
						obj.SetActive(value: true);
						obj.GetComponentInChildren<Button>().onClick.AddListener(delegate
						{
							UIUtil.AskForConfirmationFirstIfEditorHasDirtyUserLevel(delegate
							{
								DMEditor.Instance.LoadLevel(DMEditor.StartState.Edit, levelPath);
								DMUIManager.Instance.PopAll();
							});
						});
						obj.GetComponentInChildren<LocalizeText>().Localized = false;
						obj.GetComponentInChildren<LocalizeText>().LocaleID = text;
					}
					else
					{
						GameObject obj2 = Object.Instantiate(m_recentLevelsEmptyTemplate, m_recentLevelsParent);
						obj2.GetComponentInChildren<Image>().color *= num;
						obj2.SetActive(value: true);
					}
				}
			});
		}
	}
}
