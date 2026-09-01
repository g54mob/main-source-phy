using System;
using Landfall.TABS;
using UnityEngine;

public class SetTeamColorOnStart : MonoBehaviour, GameObjectPooling.IPoolable
{
	private TeamHolder m_teamHolder;

	private DataHandler m_data;

	private TeamColor[] m_teamColor;

	private Team m_team;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Start()
	{
		SetTeam();
	}

	public void SetTeam()
	{
		m_teamHolder = GetComponent<TeamHolder>();
		if ((bool)m_teamHolder)
		{
			m_team = m_teamHolder.team;
		}
		else
		{
			m_data = base.transform.root.GetComponentInChildren<DataHandler>();
			if ((bool)m_data)
			{
				m_team = m_data.team;
			}
		}
		m_teamColor = GetComponentsInChildren<TeamColor>();
		Initialize();
	}

	public void Initialize()
	{
		if (m_teamColor != null || !(m_data == null))
		{
			for (int i = 0; i < m_teamColor.Length; i++)
			{
				m_teamColor[i].SetTeamColor(m_team);
			}
		}
	}

	public void Reset()
	{
	}

	public void Release()
	{
	}
}
