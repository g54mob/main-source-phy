using BlockMapperInternal;

public class HealthRangeWidget : ParameterWidget
{
	private MHealthType healthType;

	public void Awake()
	{
	}

	public override void Init(int i, object parameter)
	{
		base.Init(i, parameter);
		UpdateVisual();
	}

	public void TotalHealth()
	{
		UpdateVisual();
	}

	public void MinHealth()
	{
		UpdateVisual();
	}

	public void MaxHealth()
	{
		UpdateVisual();
	}

	public void ResetAll()
	{
		UpdateVisual();
	}

	protected void UpdateVisual()
	{
	}
}
