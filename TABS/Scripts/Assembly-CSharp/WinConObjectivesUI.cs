using System;
using System.Collections.Generic;
using System.Linq;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.WinConditions;
using TMPro;
using UnityEngine;

public class WinConObjectivesUI : GameStateListener
{
	public enum Anchor
	{
		Left = 0,
		Right = 1
	}

	[Serializable]
	public struct Line
	{
		public enum LineThickness
		{
			Header = 0,
			Condition = 1,
			Space = 2
		}

		public LineThickness Thickness;

		public string Text;
	}

	public Anchor AnchorSide;

	public Team OwningTeam;

	public Color BlueTeamColor;

	public Color RedTeamColor;

	public Color TimerColor;

	public GameObject LinePrefab;

	public GameObject SpacePrefab;

	private List<GameObject> m_shownConditions = new List<GameObject>();

	private WinConditionPropagator m_winConditionPropagator;

	private bool m_updateGUI;

	private bool m_allowUpdateGUI;

	protected new virtual void Awake()
	{
		base.Awake();
		m_winConditionPropagator = ServiceLocator.GetService<GameModeService>().CurrentGameMode.WinConditionPropagator;
	}

	private void Start()
	{
		BaseGameMode currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
		currentGameMode.OnDonePlacingUnitsCallback = (BaseGameMode.OnDonePlacingAllUnitsDelegate)Delegate.Combine(currentGameMode.OnDonePlacingUnitsCallback, new BaseGameMode.OnDonePlacingAllUnitsDelegate(OnDonePlacingUnits));
	}

	private void OnDonePlacingUnits()
	{
		m_allowUpdateGUI = true;
	}

	public void UpdateGUI()
	{
		m_updateGUI = true;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		GameModeService service = ServiceLocator.GetService<GameModeService>();
		if (!(service == null))
		{
			BaseGameMode currentGameMode = service.CurrentGameMode;
			currentGameMode.OnDonePlacingUnitsCallback = (BaseGameMode.OnDonePlacingAllUnitsDelegate)Delegate.Remove(currentGameMode.OnDonePlacingUnitsCallback, new BaseGameMode.OnDonePlacingAllUnitsDelegate(OnDonePlacingUnits));
		}
	}

	private void ExtractWinConditions()
	{
		List<Line> list = new List<Line>();
		List<WinCondition> list2 = m_winConditionPropagator.GetWinConditionsForTeam(OwningTeam).ToList();
		if (ServiceLocator.GetService<GameModeService>().CurrentGameMode.GetType() == typeof(CampaignGameMode))
		{
			Team team = ((OwningTeam == Team.Red) ? Team.Blue : Team.Red);
			list2.AddRange(m_winConditionPropagator.GetWinConditionsForTeam(team));
		}
		foreach (WinCondition item in list2)
		{
			string text = ((item.OwningTeam != OwningTeam) ? item.GetBattleDescription(invertDescription: true) : item.GetBattleDescription(invertDescription: false));
			if (!(text == string.Empty))
			{
				list.Add(new Line
				{
					Thickness = Line.LineThickness.Condition,
					Text = text
				});
			}
		}
		Setup(list.ToArray());
	}

	private void Update()
	{
		if (m_updateGUI && m_allowUpdateGUI)
		{
			m_updateGUI = false;
			ClearShownConditions();
			ExtractWinConditions();
		}
	}

	public void Setup(Line[] newLines)
	{
		for (int i = 0; i < newLines.Length; i++)
		{
			string text = newLines[i].Text;
			if (newLines[i].Thickness == Line.LineThickness.Space)
			{
				UnityEngine.Object.Instantiate(SpacePrefab, base.transform);
				continue;
			}
			RectTransform rectTransform = UnityEngine.Object.Instantiate(LinePrefab, base.transform).transform as RectTransform;
			rectTransform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = text;
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, GetWidth(newLines[i].Thickness));
			m_shownConditions.Add(rectTransform.gameObject);
		}
	}

	private void ClearShownConditions()
	{
		for (int i = 0; i < m_shownConditions.Count; i++)
		{
			UnityEngine.Object.Destroy(m_shownConditions[i]);
		}
		m_shownConditions.Clear();
	}

	public float GetWidth(Line.LineThickness thickness)
	{
		switch (thickness)
		{
		case Line.LineThickness.Condition:
			return 29f;
		case Line.LineThickness.Header:
			return 59f;
		default:
			return 29f;
		}
	}

	public override void OnEnterPlacementState()
	{
		m_allowUpdateGUI = false;
	}

	public override void OnEnterBattleState()
	{
	}
}
