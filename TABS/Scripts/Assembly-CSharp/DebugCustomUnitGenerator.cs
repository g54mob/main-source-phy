using Landfall.TABS;
using Landfall.TABS.Workshop;
using TMPro;
using UnityEngine;

public class DebugCustomUnitGenerator : MonoBehaviour
{
	public TMP_InputField amountInputField;

	public Sprite CustomUnitIcon;

	public void Generate()
	{
		int num = int.Parse(amountInputField.text);
		LandfallUnitDatabase database = LandfallUnitDatabase.GetDatabase();
		for (int i = 0; i < num; i++)
		{
			UnitBlueprint unitEditorBlueprint = database.m_unitEditorBlueprint;
			unitEditorBlueprint.Entity.Name = $"Randomly Generated Unit [{i}]";
			unitEditorBlueprint.Entity.SpriteIcon = CustomUnitIcon;
			CustomUnitHandler.SaveUnit(unitEditorBlueprint);
		}
	}
}
