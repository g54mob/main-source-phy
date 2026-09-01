using System.Collections;
using System.Collections.Generic;
using DM;
using Landfall.TABS;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : Paginator
{
	public enum LevelType
	{
		Sandbox = 0,
		Campaign = 1,
		CampaignLevel = 2,
		SandboxBattleRoot = 3,
		LocalMultiplayer = 4,
		OnlineMultiplayer = 5
	}

	public LevelType levelType;

	[SerializeField]
	protected PageCounter m_PageCounter;

	[SerializeField]
	protected GameObject m_templateLevelButton;

	[SerializeField]
	protected MainMenuUIHandler m_mainMenuUIHandler;

	[SerializeField]
	protected Transform m_Grid;

	[SerializeField]
	protected Toggle[] m_PageTabs;

	[SerializeField]
	protected LocalizeText m_selectionTitle;

	[Space]
	[SerializeField]
	protected Button leftPageButton;

	[SerializeField]
	protected Button smallLeftPageButton;

	[Space]
	[SerializeField]
	protected Button rightPageButton;

	[SerializeField]
	protected Button smallRightPageButton;

	[Space]
	[SerializeField]
	protected float buttonSwapAspectRatioThreshold = 1.7f;

	protected List<GameObject> SpawnedButtons;

	protected List<Button> m_LevelButtons;

	protected PlayerActions m_playerActions;

	protected int m_TabIndex;

	protected ContentDatabase unitDatabase;

	protected MapAsset.MapType mapType;

	protected float currentAspectRatio;

	protected override void Awake()
	{
		base.Awake();
		m_LevelButtons = new List<Button>();
		SpawnedButtons = new List<GameObject>();
		m_playerActions = PlayerActions.Instance;
		unitDatabase = ContentDatabase.Instance();
	}

	public virtual void Go()
	{
		OpenPage();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		InputService service = ServiceLocator.GetService<InputService>();
		if (service != null)
		{
			service.InputChanged += OnInputSourceChanged;
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		InputService service = ServiceLocator.GetService<InputService>();
		if (service != null)
		{
			service.InputChanged -= OnInputSourceChanged;
		}
	}

	protected override void Update()
	{
		base.Update();
		if (base.IsOpen)
		{
			if (m_playerActions.m_cycleUnitsUp.WasPressed)
			{
				NextPage();
			}
			if (m_playerActions.m_cycleUnitsDown.WasPressed)
			{
				PreviousPage();
			}
			if (m_playerActions.m_cycleTabs.WasPressed && levelType != LevelType.CampaignLevel && m_PageTabs != null && m_PageTabs.Length != 0)
			{
				int num = m_PageTabs.Length;
				int num2 = Mathf.RoundToInt(PlayerActions.Instance.m_cycleTabs.Value);
				m_TabIndex = (m_TabIndex + num2) % num;
				m_TabIndex = ((m_TabIndex < 0) ? (num - 1) : m_TabIndex);
				m_PageTabs[m_TabIndex].isOn = true;
			}
		}
	}

	public override void OpenPage()
	{
		base.OpenPage();
		canvasToggle.FadeIn();
	}

	public override void ClosePage()
	{
		bool flag = false;
		flag = levelType == LevelType.LocalMultiplayer;
		flag = levelType == LevelType.OnlineMultiplayer;
		if (levelType == LevelType.Sandbox || levelType == LevelType.Campaign || levelType == LevelType.CampaignLevel || flag)
		{
			base.ClosePage();
			base.Close?.Invoke();
			canvasToggle.FadeOut();
		}
	}

	protected override void Populate(int page = 0)
	{
		base.Populate(page);
	}

	protected override void Clear()
	{
		base.Clear();
		ClearListAndDestroyObjects(m_LevelButtons);
		ClearListAndDestroyObjects(SpawnedButtons);
	}

	private void ClearListAndDestroyObjects(IList list)
	{
		if (list != null)
		{
			foreach (object item in list)
			{
				Object.Destroy(item as Object);
			}
		}
		list?.Clear();
	}

	private void OnInputSourceChanged(InputType type)
	{
		if (type != InputType.Controller)
		{
			_ = type - 1;
			_ = 1;
		}
		else
		{
			SelectFirstButtonInList();
		}
	}

	protected virtual void SelectFirstButtonInList()
	{
		bool flag = false;
		if (levelType == LevelType.LocalMultiplayer)
		{
			flag = true;
		}
		if (levelType == LevelType.OnlineMultiplayer)
		{
			flag = true;
		}
		if ((base.IsActive || flag) && m_LevelButtons != null && m_LevelButtons.Count > 0 && m_LevelButtons[0] != null)
		{
			m_LevelButtons[0].Select();
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		InputService service = ServiceLocator.GetService<InputService>();
		if (service != null)
		{
			service.InputChanged -= OnInputSourceChanged;
		}
	}
}
