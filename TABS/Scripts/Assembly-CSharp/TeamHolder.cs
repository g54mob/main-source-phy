using System;
using Landfall.TABS;
using UnityEngine;

public class TeamHolder : MonoBehaviour, GameObjectPooling.IPoolable
{
	public bool getRootTeamOnAwake;

	public Team team;

	public Rigidbody target;

	public GameObject spawner;

	public GameObject spawnerWeapon;

	public bool setTeamcolors;

	private TeamHolder rootTeam;

	private SetTeamColorOnStart teamcolorSetter;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Start()
	{
		if (!IsManagedByPool)
		{
			InitializeOnSpawn();
		}
	}

	public void SwitchTeam()
	{
		if (team == Team.Blue)
		{
			team = Team.Red;
		}
		else
		{
			team = Team.Blue;
		}
	}

	public void Initialize()
	{
		InitializeOnSpawn();
	}

	public void Reset()
	{
	}

	public void Release()
	{
		target = null;
		spawner = null;
		spawnerWeapon = null;
	}

	private void InitializeOnSpawn()
	{
		if (!getRootTeamOnAwake)
		{
			return;
		}
		rootTeam = base.transform.root.GetComponent<TeamHolder>();
		if ((bool)rootTeam)
		{
			team = rootTeam.team;
			spawner = rootTeam.spawner;
			teamcolorSetter = base.transform.GetComponent<SetTeamColorOnStart>();
			if ((bool)teamcolorSetter)
			{
				teamcolorSetter.SetTeam();
			}
		}
	}

	public static void AddTeamHolder(GameObject target, GameObject spawner)
	{
		TeamHolder teamHolder = target.FetchComponent<TeamHolder>();
		if (!(teamHolder != null))
		{
			return;
		}
		Unit componentInChildren = spawner.transform.root.GetComponentInChildren<Unit>();
		if (componentInChildren != null)
		{
			teamHolder.team = componentInChildren.Team;
			teamHolder.spawner = componentInChildren.gameObject;
			return;
		}
		TeamHolder componentInChildren2 = spawner.transform.root.GetComponentInChildren<TeamHolder>();
		if (componentInChildren2 != null)
		{
			teamHolder.team = componentInChildren2.team;
			teamHolder.spawner = componentInChildren2.spawner;
		}
	}

	public static TeamHolder AddTeamHolder(GameObject target, Unit unit, TeamHolder spawnerRootTeamHolder)
	{
		TeamHolder teamHolder = target.FetchComponent<TeamHolder>();
		if (unit != null)
		{
			teamHolder.team = unit.Team;
			teamHolder.spawner = unit.gameObject;
		}
		else if (spawnerRootTeamHolder != null)
		{
			teamHolder.team = spawnerRootTeamHolder.team;
			teamHolder.spawner = spawnerRootTeamHolder.spawner;
		}
		return teamHolder;
	}

	public static void GetTeamRelevantComponents(Transform spawner, ref Unit spawnerRootUnit, ref TeamHolder rootTeamHolder)
	{
		spawnerRootUnit = spawner.root.GetComponentInChildren<Unit>();
		if (spawnerRootUnit == null)
		{
			rootTeamHolder = spawner.root.GetComponentInChildren<TeamHolder>();
		}
	}
}
