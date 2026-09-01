using Landfall.TABS;
using Landfall.TABS.UnitEditor;
using UnityEngine;

public class UnitEditorTeamToggle : MonoBehaviour
{
	public UnitEditorTeamButton RedButton;

	public UnitEditorTeamButton BlueButton;

	public UnitEditorManager manager;

	private Team currentTeam;

	private ModalPanel modalPanel;

	private void Awake()
	{
		modalPanel = ServiceLocator.GetService<ModalPanel>();
	}

	private void Start()
	{
		RedButton.Select();
	}

	internal void OnButtonSelected(Team team)
	{
		if (currentTeam != team)
		{
			SetButtonSelected(team);
		}
	}

	public void SetButtonSelected(Team team)
	{
		if (!(modalPanel != null) || !modalPanel.IsPopupOpen)
		{
			currentTeam = team;
			if (team == Team.Red)
			{
				RedButton.Select();
				BlueButton.Deselect();
			}
			else
			{
				RedButton.Deselect();
				BlueButton.Select();
			}
			manager.ChangeTeamColor(team);
		}
	}
}
