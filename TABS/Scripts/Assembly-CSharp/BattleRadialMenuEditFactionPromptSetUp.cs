using Landfall.TABS_Input;
using UnityEngine;

public class BattleRadialMenuEditFactionPromptSetUp : MonoBehaviour
{
	[SerializeField]
	private string editFactionsText = "LABEL_EDIT_FACTIONS";

	[SerializeField]
	private string backText = "BUTTON_BACK";

	[SerializeField]
	private ActionGlyphText actionGlyph;

	[SerializeField]
	private GameObject resetFactionsPrompt;

	private string editFactionActionName = "Edit Radial Menu Faction";

	private string backActionName = "Back";

	private PlayerActions playerActions;

	private void Awake()
	{
		actionGlyph = GetComponentInChildren<ActionGlyphText>();
		SetButtonForEdit();
	}

	public void SetButtonForEdit()
	{
		actionGlyph.UpdateActionNames(editFactionActionName, editFactionsText);
		if (resetFactionsPrompt != null)
		{
			resetFactionsPrompt.SetActive(value: false);
		}
	}

	public void SetButtonForBack()
	{
		actionGlyph.UpdateActionNames(backActionName, backText);
		if (resetFactionsPrompt != null)
		{
			resetFactionsPrompt.SetActive(value: true);
		}
	}
}
