using BlockMapperInternal;

public class CollapsedSkinWidget : ParameterWidget
{
	public UIButton openButton;

	public void Awake()
	{
		openButton.Click += Open;
	}

	public void Open()
	{
		StatMaster.collapseSkinMapper = false;
		BlockMapper.CurrentInstance.IsDirty = true;
	}
}
