using UnityEngine;
using UnityEngine.UI;

public class SelectableOption
{
	public GameObject mainObject;

	public OptionsPrefab_Base baseObject;

	public SelectableOptionType type;

	public int direction = 1;

	public Selectable selectable
	{
		get
		{
			if (baseObject != null)
			{
				return baseObject.selectable;
			}
			return null;
		}
	}

	public SelectableOption(GameObject mainObject, OptionsPrefab_Base baseObject, SelectableOptionType type, int direction)
	{
		this.mainObject = mainObject;
		this.baseObject = baseObject;
		this.type = type;
		this.direction = direction;
	}
}
