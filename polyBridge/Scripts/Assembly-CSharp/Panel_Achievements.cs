using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Achievements : MonoBehaviour
{
	[Header("Header")]
	public TextMeshProUGUI m_NumCompleteText;

	public Button m_CancelButton;

	[Header("Toggles")]
	public Toggle m_AllToggle;

	public Toggle m_CompleteToggle;

	public Toggle m_IncompleteToggle;

	[Header("Body")]
	public RectTransform m_ContentRectTransform;

	public Sprite[] m_LockedSprites;

	public Sprite[] m_UnlockedSprites;

	public GameObject m_OfflineObject;

	[Header("Prefabs")]
	public GameObject m_AchievementSlotPrefab;

	public GameObject m_AchievementSlotLockedPrefab;

	private List<AchievementSlot> m_AchievementSlots = new List<AchievementSlot>();

	private AchivementFilterType m_AchivementFilterType;

	private readonly GameAchievement[] m_SlotOrder = new GameAchievement[28]
	{
		GameAchievement.BeatWorld_CR,
		GameAchievement.BeatWorld_MM,
		GameAchievement.BeatWorld_RB,
		GameAchievement.BeatWorld_BB,
		GameAchievement.BeatWorld_LL,
		GameAchievement.BeatWorld_VT,
		GameAchievement.BeatWorld_MB,
		GameAchievement.BeatWorld_DS,
		GameAchievement.BeatWorld_CW,
		GameAchievement.BeatWorld_RMT,
		GameAchievement.BeatWorld_AT,
		GameAchievement.BeatWorld_RTA,
		GameAchievement.BeatWorld_TT,
		GameAchievement.BeatWorld_FR,
		GameAchievement.Unlock_2Sheep,
		GameAchievement.Unlock_3Sheep,
		GameAchievement.Unlock_4Sheep,
		GameAchievement.Unlock_5Sheep,
		GameAchievement.UI_SharingIsCaring,
		GameAchievement.UI_WorkShopping,
		GameAchievement.UI_ExtraFlavor,
		GameAchievement.Fun_SpeedRunner,
		GameAchievement.Fun_NeverGoingToGiveYouUp,
		GameAchievement.Fun_TisButAScratch,
		GameAchievement.Fun_FirstTry,
		GameAchievement.Fun_MeantToDoThat,
		GameAchievement.Fun_Hydrophobic,
		GameAchievement.Fun_Inflexable
	};

	private void Awake()
	{
		m_NumCompleteText.gameObject.SetActive(value: false);
	}

	private void Start()
	{
		m_AllToggle.onValueChanged.AddListener(delegate
		{
			OnAllToggle();
		});
		m_CompleteToggle.onValueChanged.AddListener(delegate
		{
			OnCompleteToggle();
		});
		m_IncompleteToggle.onValueChanged.AddListener(delegate
		{
			OnIncompleteToggle();
		});
		m_CancelButton.onClick.AddListener(Close);
	}

	private void Update()
	{
		ProcessInput();
	}

	private void OnEnable()
	{
		m_OfflineObject.SetActive(value: false);
		InstantiateAchievementSlots();
		UpdateAchievementSlots();
		SetNumCompletedText();
		FilterAchievements(m_AchivementFilterType);
		ActivePanels.Add(base.gameObject);
		GameUI.m_Instance.m_GamepadLegend.Save();
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
		GameUI.m_Instance.m_Campaign.m_Root.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_GamepadLegend.Restore();
	}

	public void Open()
	{
		base.gameObject.SetActive(value: true);
	}

	private void Close()
	{
		InterfaceAudio.Play("ui_menu_cancel");
		base.gameObject.SetActive(value: false);
	}

	private void InstantiateAchievementSlots()
	{
		foreach (AchievementSlot achievementSlot in m_AchievementSlots)
		{
			Object.Destroy(achievementSlot.gameObject);
		}
		m_AchievementSlots.Clear();
		for (int i = 0; i < m_SlotOrder.Length; i++)
		{
			GameObject gameObject = Object.Instantiate(GameAchievements.HasUnlocked(m_SlotOrder[i]) ? m_AchievementSlotPrefab : m_AchievementSlotLockedPrefab, m_ContentRectTransform);
			if (gameObject != null)
			{
				AchievementSlot component = gameObject.GetComponent<AchievementSlot>();
				m_AchievementSlots.Add(component);
				component.Init((int)m_SlotOrder[i]);
			}
		}
		FilterAchievements(m_AchivementFilterType);
	}

	private void UpdateAchievementSlots()
	{
		foreach (AchievementSlot achievementSlot in m_AchievementSlots)
		{
			if (achievementSlot.GetGameAchivement() == GameAchievement.BeatWorld_FR && !GameManager.IsSecretWorldUnlocked())
			{
				achievementSlot.gameObject.SetActive(value: false);
			}
			else
			{
				achievementSlot.gameObject.SetActive(value: true);
			}
			achievementSlot.UpdateWithAchivementData();
		}
	}

	private void SetNumCompletedText()
	{
		int num = 0;
		int num2 = 0;
		foreach (AchievementSlot achievementSlot in m_AchievementSlots)
		{
			if (achievementSlot.gameObject.activeInHierarchy)
			{
				num++;
				if (achievementSlot.m_CompletedDate.gameObject.activeInHierarchy)
				{
					num2++;
				}
			}
		}
		m_NumCompleteText.text = string.Format(Localize.Get("UI_ACHIEVEMENTS_UNLOCKED"), num2, num);
		m_NumCompleteText.gameObject.SetActive(value: true);
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject) && (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST)))
		{
			Close();
		}
	}

	private void OnAllToggle()
	{
		if (m_AllToggle.isOn)
		{
			InterfaceAudio.Play("ui_menu_select");
			SelectToggle(AchivementFilterType.ALL);
			FilterAchievements(AchivementFilterType.ALL);
		}
	}

	private void OnCompleteToggle()
	{
		if (m_CompleteToggle.isOn)
		{
			InterfaceAudio.Play("ui_menu_select");
			SelectToggle(AchivementFilterType.COMPLETE);
			FilterAchievements(AchivementFilterType.COMPLETE);
		}
	}

	private void OnIncompleteToggle()
	{
		if (m_IncompleteToggle.isOn)
		{
			InterfaceAudio.Play("ui_menu_select");
			SelectToggle(AchivementFilterType.INCOMPLETE);
			FilterAchievements(AchivementFilterType.INCOMPLETE);
		}
	}

	private void SelectToggle(AchivementFilterType filter)
	{
		m_AchivementFilterType = filter;
	}

	private void FilterAchievements(AchivementFilterType filter)
	{
		foreach (AchievementSlot achievementSlot in m_AchievementSlots)
		{
			switch (filter)
			{
			case AchivementFilterType.ALL:
				achievementSlot.gameObject.SetActive(value: true);
				break;
			case AchivementFilterType.COMPLETE:
				achievementSlot.gameObject.SetActive(achievementSlot.IsUnlocked());
				break;
			case AchivementFilterType.INCOMPLETE:
				achievementSlot.gameObject.SetActive(!achievementSlot.IsUnlocked());
				break;
			}
		}
	}
}
