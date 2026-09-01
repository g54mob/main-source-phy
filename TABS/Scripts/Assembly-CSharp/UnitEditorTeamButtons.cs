using System;
using Landfall.TABS;
using UnityEngine;
using UnityEngine.UI;

public class UnitEditorTeamButtons : MonoBehaviour
{
	[SerializeField]
	private Toggle m_toggleTeamRed;

	[SerializeField]
	private Toggle m_toggleTeamBlue;

	public static Action<Team> _OnTeamChanged;

	public static Team _CurrentTeam { get; private set; }

	private void Awake()
	{
		m_toggleTeamRed.onValueChanged.AddListener(delegate(bool value)
		{
			OnValueChanged(value, Team.Red);
		});
		m_toggleTeamBlue.onValueChanged.AddListener(delegate(bool value)
		{
			OnValueChanged(value, Team.Blue);
		});
	}

	private void OnValueChanged(bool value, Team team)
	{
		if (value)
		{
			_CurrentTeam = team;
			UnitEditorHandler.Instance.SetTeamColors(team);
			_OnTeamChanged?.Invoke(team);
		}
	}
}
