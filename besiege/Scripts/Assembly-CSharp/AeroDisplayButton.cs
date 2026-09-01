using UnityEngine;

public class AeroDisplayButton : ClickBehaviour
{
	public UIButton reset;

	public GameObject activeBG;

	public void Awake()
	{
		reset.Click += Reset;
		activeBG.SetActive(StatMaster.Mode.displayDrag);
	}

	public void Reset()
	{
		AeroDynamicDisplay.Reset();
	}

	public override void OnClicked()
	{
		Set();
	}

	private void Set()
	{
		StatMaster.Mode.displayDrag = !StatMaster.Mode.displayDrag;
		activeBG.SetActive(StatMaster.Mode.displayDrag);
	}
}
