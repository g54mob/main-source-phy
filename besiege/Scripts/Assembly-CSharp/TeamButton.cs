using UnityEngine;

public class TeamButton : MonoBehaviour
{
	public Renderer teamNone;

	public Renderer teamColor;

	private MPTeam _team;

	public MPTeam Team
	{
		get
		{
			return _team;
		}
	}

	protected void Awake()
	{
		SetTeam(MPTeam.None);
	}

	public void NextTeam()
	{
		int team = (int)_team;
		int num = ReferenceMaster.Instance.teamColors.Length;
		MPTeam team2 = ((team + 1 < num) ? ((MPTeam)(team + 1)) : MPTeam.None);
		_team = team2;
	}

	public void SetTeam(MPTeam team)
	{
		_team = team;
		teamNone.enabled = team == MPTeam.None;
		if (team != MPTeam.None)
		{
			teamColor.enabled = true;
			teamColor.material.SetColor("_TintColor", ReferenceMaster.Instance.teamColors[(int)team]);
		}
		else
		{
			teamColor.enabled = false;
		}
	}
}
