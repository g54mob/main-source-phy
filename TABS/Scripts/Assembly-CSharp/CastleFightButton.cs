using Landfall.TABS;
using UnityEngine;

public class CastleFightButton : MonoBehaviour
{
	public enum CastleFightButtonType
	{
		Cancel = 0,
		Sell = 1,
		Level = 2,
		Buy = 3
	}

	public CastleFightButtonType buttonType;

	public UnitBlueprint unit;
}
