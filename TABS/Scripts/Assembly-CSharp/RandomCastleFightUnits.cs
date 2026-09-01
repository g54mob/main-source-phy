using Landfall.TABS;
using UnityEngine;

public class RandomCastleFightUnits : MonoBehaviour
{
	public UnitBlueprint[] lowTeir;

	public UnitBlueprint[] midTeir;

	public UnitBlueprint[] highTeir;

	public UnitBlueprint[] selectedUnits = new UnitBlueprint[10];

	private void Awake()
	{
		CastleFightPlacer component = GetComponent<CastleFightPlacer>();
		for (int i = 0; i < selectedUnits.Length; i++)
		{
			if (i > 0)
			{
				if (i < 3)
				{
					AssignUnit(lowTeir, i);
				}
				else if (i < 7)
				{
					AssignUnit(midTeir, i);
				}
				else
				{
					AssignUnit(highTeir, i);
				}
			}
		}
		component.units = selectedUnits;
	}

	private void AssignUnit(UnitBlueprint[] teir, int index)
	{
		UnitBlueprint unitBlueprint = teir[Random.Range(0, teir.Length)];
		for (int i = 0; i < selectedUnits.Length; i++)
		{
			if (selectedUnits[i] == unitBlueprint)
			{
				AssignUnit(teir, index);
				return;
			}
		}
		selectedUnits[index] = unitBlueprint;
	}
}
