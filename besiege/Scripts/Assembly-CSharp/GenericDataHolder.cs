public class GenericDataHolder : SaveableDataHolder
{
	public string Name { get; set; }

	public override XDataHolder InitialState
	{
		get
		{
			XDataHolder xDataHolder = new XDataHolder();
			foreach (MapperType mapperType in base.MapperTypes)
			{
				xDataHolder.Write(mapperType.SerializeDefault());
			}
			return xDataHolder;
		}
	}

	protected override void Awake()
	{
		noRigidbody = true;
		base.Awake();
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		LoadMapperValues(data);
	}

	public override void OnLoad(XDataHolder data, CopyMode mode)
	{
		base.OnLoad(data, mode);
		OnLoad(data);
	}

	public override void OnSave(XDataHolder data)
	{
		base.OnSave(data);
		SaveMapperValues(data);
	}

	public override void OnSave(XDataHolder data, CopyMode mode)
	{
		base.OnSave(data, mode);
		OnSave(data);
	}
}
