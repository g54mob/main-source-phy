using System.Collections.Generic;
using Localisation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameGrind
{
	public class AchievementUIList : SingleInstance<AchievementUIList>, ILocalisationAware
	{
		private string currencyTitle = "Points";

		[SerializeField]
		private GameObject listElementPrefab;

		[SerializeField]
		private Transform listUIPanel;

		[SerializeField]
		private Text currentAchievementScore;

		[SerializeField]
		private Button closeButton;

		[SerializeField]
		private bool altRowShading;

		private bool isPanelActive;

		private List<AchievementUIElement> achievementUIObject = new List<AchievementUIElement>();

		public override string Name
		{
			get
			{
				return "AchievementUIList";
			}
		}

		private void Awake()
		{
			SingleInstance<AchievementUIList>.Initialize(this);
			base.gameObject.SetActive(false);
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		private void OnDestroy()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
			AchievementEvents.OnAchievementChange -= UpdateAchievementUIData;
			AchievementEvents.OnAchievementGrant -= UpdateScore;
		}

		private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
		{
			Close();
		}

		private void Start()
		{
			AchievementController.CurrentAchievementScore = 0;
			base.gameObject.SetActive(true);
			BuildStatListUI();
			AchievementEvents.OnAchievementChange += UpdateAchievementUIData;
			AchievementEvents.OnAchievementGrant += UpdateScore;
			currentAchievementScore.text = AchievementController.CurrentAchievementScore + " " + currencyTitle;
			closeButton.onClick.AddListener(delegate
			{
				base.gameObject.SetActive(false);
			});
		}

		public void BuildStatListUI()
		{
			for (int i = 0; i < Journal.achievementMaster.Count; i++)
			{
				achievementUIObject.Add(Object.Instantiate(listElementPrefab).GetComponent<AchievementUIElement>());
				achievementUIObject[i].transform.SetParent(listUIPanel, false);
				if (altRowShading && i % 2 == 0)
				{
					achievementUIObject[i].GetComponent<Image>().color = achievementUIObject[i].altRowShading;
				}
				if (Journal.achievementMaster[i].completed)
				{
					AchievementController.CurrentAchievementScore += Journal.achievementMaster[i].points;
				}
			}
			UpdateStatListData();
		}

		public void UpdateStatListData()
		{
			for (int i = 0; i < achievementUIObject.Count; i++)
			{
				achievementUIObject[i].SetAchievementValues(Journal.achievementMaster[i]);
			}
		}

		public void OnLocalisationChange()
		{
			for (int i = 0; i < achievementUIObject.Count; i++)
			{
				achievementUIObject[i].TranslateAchievement(Journal.achievementMaster[i]);
			}
		}

		public void UpdateAchievementUIData(Achievement achievement)
		{
			for (int i = 0; i < Journal.achievementMaster.Count; i++)
			{
				if (Journal.achievementMaster[i].id == achievement.id)
				{
					achievementUIObject[i].SetAchievementValues(achievement);
				}
			}
		}

		public void UpdateScore(Achievement achievement)
		{
			currentAchievementScore.text = AchievementController.CurrentAchievementScore + " Points";
		}

		public void TogglePanel()
		{
			isPanelActive = !isPanelActive;
			StatMaster.SetInMenu(isPanelActive);
			base.gameObject.SetActive(isPanelActive);
		}

		private void Update()
		{
			if (isPanelActive && InputManager.CloseKey())
			{
				Close();
			}
		}

		private void OnDisable()
		{
			if (isPanelActive)
			{
				isPanelActive = false;
				StatMaster.SetInMenu(false);
			}
		}

		public void Close()
		{
			if (base.gameObject.activeSelf)
			{
				isPanelActive = false;
				StatMaster.SetInMenu(false);
				base.gameObject.SetActive(false);
			}
		}

		private void ClearAchievementList()
		{
			for (int i = 0; i < achievementUIObject.Count; i++)
			{
				Object.Destroy(achievementUIObject[i].gameObject);
			}
		}
	}
}
