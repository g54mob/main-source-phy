using System.Collections.Generic;
using Landfall.TABS;
using Sirenix.OdinInspector;

public class ConquestNode : SerializedMonoBehaviour
{
	public enum Allegience
	{
		Netural = 0,
		Red = 1,
		Blue = 2,
		Green = 3,
		Yellow = 4,
		Purple = 5,
		Orange = 6
	}

	public string placeName = "";

	public ConquestUnitWrapper[] startUnits;

	public Dictionary<UnitBlueprint, int> units = new Dictionary<UnitBlueprint, int>();

	private void Start()
	{
		for (int i = 0; i < startUnits.Length; i++)
		{
			units.Add(startUnits[i].unit, startUnits[i].number);
		}
	}
}
