using UnityEngine;

namespace Landfall.TABS.UI.Widgets.Fields
{
	public class UIFloatField : UIPropertyField
	{
		protected override void OnPropertyChanged(string value)
		{
			float result = 0f;
			if (float.TryParse(value, out result))
			{
				if (result < 0f)
				{
					result = 0f;
				}
				SetValueOnOwner(result);
			}
			else if (value == string.Empty)
			{
				result = 0f;
				SetValueOnOwner(result);
			}
			else
			{
				Debug.LogError("Failed to parse float!");
			}
		}
	}
}
