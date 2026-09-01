using System;
using UnityEngine;

public class ToggleWorkshopButton : MonoBehaviour
{
	public Action<WorkshopType> OpenWorkshopButtonClicked;

	[SerializeField]
	protected OpenWorkshopButton steamButton;

	[SerializeField]
	protected OpenWorkshopButton weGameButton;

	[SerializeField]
	protected OpenWorkshopButton modIOButton;

	private void Start()
	{
		ToggleSteamButton();
		ToggleWeGameButton();
		ToggleModIOButton();
	}

	private void ToggleSteamButton()
	{
		if (steamButton != null)
		{
			steamButton.Click += delegate
			{
				InvokeWorkshopClicked(WorkshopType.Steam);
			};
		}
	}

	private void ToggleWeGameButton()
	{
		if (weGameButton != null)
		{
			weGameButton.Click += delegate
			{
				InvokeWorkshopClicked(WorkshopType.WeGame);
			};
		}
	}

	private void ToggleModIOButton()
	{
		if (modIOButton != null)
		{
			modIOButton.Click += delegate
			{
				InvokeWorkshopClicked(WorkshopType.ModIO);
			};
		}
	}

	private void InvokeWorkshopClicked(WorkshopType workshopType)
	{
		if (OpenWorkshopButtonClicked != null)
		{
			OpenWorkshopButtonClicked(workshopType);
		}
	}
}
