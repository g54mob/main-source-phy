public sealed class MTeam : MapperType
{
	private MPTeam _team;

	private MPTeam _loadTeam;

	private MPTeam _defaultTeam;

	public MPTeam Team
	{
		get
		{
			return _team;
		}
		set
		{
			if (_team != value)
			{
				_team = value;
				InvokeTeamChanged(value);
			}
		}
	}

	public override bool isDefaultValue
	{
		get
		{
			return _team == _defaultTeam;
		}
	}

	public event TeamHandler TeamChanged;

	public MTeam(int nameLocalisationId, string key, MPTeam defaultValue)
		: base(nameLocalisationId, key)
	{
		_team = (_loadTeam = (_defaultTeam = defaultValue));
		InvokeTeamChanged(_team);
		base.defaultData = Serialize();
	}

	public MTeam(string displayName, string key, MPTeam defaultValue)
		: base(displayName, key)
	{
		_team = (_loadTeam = (_defaultTeam = defaultValue));
		InvokeTeamChanged(_team);
		base.defaultData = Serialize();
	}

	public override XData Serialize()
	{
		return new XInteger("bmt-" + base.Key, (int)Team);
	}

	public override XData SerializeLoadValue()
	{
		return new XInteger("bmt-" + base.Key, (int)_loadTeam);
	}

	public override XData SerializeDefault()
	{
		return new XInteger("bmt-" + base.Key, (int)_defaultTeam);
	}

	public override void ApplyValue()
	{
		_loadTeam = _team;
		InvokeTeamChanged(_team);
	}

	public void SetValue(MPTeam team)
	{
		_team = team;
	}

	public override void ResetValue()
	{
		Team = _loadTeam;
	}

	public override void ResetDefaults()
	{
		Team = _defaultTeam;
	}

	public override void DeSerialize(XData raw)
	{
		int loadTeam = (int)(XInteger)raw;
		_team = (_loadTeam = (MPTeam)loadTeam);
		InvokeTeamChanged(_team);
	}

	public override bool CompareValue(MapperType other)
	{
		MTeam mTeam = other as MTeam;
		return mTeam != null && mTeam._team == _team;
	}

	private void InvokeTeamChanged(MPTeam team)
	{
		TeamHandler teamChanged = this.TeamChanged;
		if (teamChanged != null)
		{
			teamChanged(team);
		}
	}
}
