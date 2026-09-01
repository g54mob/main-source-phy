using Localisation;

public abstract class MapperType
{
	public const string XDATA_PREFIX = "bmt-";

	private string _displayName;

	private bool _displayInMapper = true;

	protected int nameLocalisationId;

	public string Key { get; private set; }

	public string DisplayName
	{
		get
		{
			return _displayName;
		}
		set
		{
			if (_displayName != value)
			{
				_displayName = value;
				InvokeNameChanged(value);
			}
		}
	}

	public abstract bool isDefaultValue { get; }

	public XData defaultData { get; protected set; }

	public virtual bool DisplayInMapper
	{
		get
		{
			return _displayInMapper;
		}
		set
		{
			if (_displayInMapper != value)
			{
				_displayInMapper = value;
				InvokeDisplayStateChanged(value);
			}
		}
	}

	public int NameLocalisationId
	{
		get
		{
			return nameLocalisationId;
		}
	}

	public event DisplayStateHandler DisplayStateChanged;

	public event NameChangeHandler NameChanged;

	protected MapperType(int nameLocalisationId, string key)
	{
		this.nameLocalisationId = nameLocalisationId;
		DisplayName = LocalisationManager.GetTranslation(nameLocalisationId);
		Key = key;
	}

	protected MapperType(string displayName, string key)
	{
		if (displayName != null)
		{
			DisplayName = displayName;
		}
		Key = key;
	}

	public void ResetLocalisation(int newID = 0)
	{
		if (newID != 0)
		{
			nameLocalisationId = newID;
		}
		if (nameLocalisationId != 0)
		{
			DisplayName = LocalisationManager.GetTranslation(nameLocalisationId);
		}
	}

	public virtual void ResetValue()
	{
	}

	public virtual void ApplyValue()
	{
	}

	public virtual void ResetDefaults()
	{
	}

	public abstract XData Serialize();

	public abstract XData SerializeDefault();

	public abstract XData SerializeLoadValue();

	public abstract void DeSerialize(XData raw);

	public abstract bool CompareValue(MapperType other);

	protected virtual void InvokeDisplayStateChanged(bool visible)
	{
		DisplayStateHandler displayStateChanged = this.DisplayStateChanged;
		if (displayStateChanged != null)
		{
			displayStateChanged(visible);
			if (BlockMapper.CurrentInstance != null)
			{
				BlockMapper.CurrentInstance.Refresh();
			}
		}
	}

	protected virtual void InvokeNameChanged(string name)
	{
		NameChangeHandler nameChanged = this.NameChanged;
		if (nameChanged != null)
		{
			nameChanged(name);
		}
	}
}
