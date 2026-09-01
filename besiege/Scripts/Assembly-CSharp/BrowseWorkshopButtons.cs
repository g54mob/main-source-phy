using UnityEngine;

public class BrowseWorkshopButtons : BrowseWorkshopButton
{
	[Space(10f)]
	[SerializeField]
	protected SimpleUIButton steamButton;

	[SerializeField]
	protected SimpleUIButton weGameButton;

	[SerializeField]
	protected SimpleUIButton modIOButton;

	protected void Awake()
	{
		steamButton.Click += base.HandleClickSteam;
		Object.Destroy(weGameButton.gameObject);
		Object.Destroy(modIOButton.gameObject);
	}

	protected override void UpdateVisual()
	{
	}

	public override void OnClicked()
	{
	}
}
