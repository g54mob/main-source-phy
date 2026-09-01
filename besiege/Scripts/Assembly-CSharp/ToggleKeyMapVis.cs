using UnityEngine;

public class ToggleKeyMapVis : ClickBehaviour
{
	public Renderer rendy;

	public static ToggleKeyMapVis Instance;

	protected void OnEnable()
	{
		Instance = this;
		Set();
	}

	public override void OnClicked()
	{
		Machine machine = Machine.Active();
		if (machine != null)
		{
			if (OverviewBlockMapper.CurrentInstance == null)
			{
				OverviewBlockMapper.Open(machine);
			}
			else
			{
				OverviewBlockMapper.Close();
			}
		}
	}

	public void Set()
	{
		rendy.enabled = OverviewBlockMapper.CurrentInstance != null;
	}
}
