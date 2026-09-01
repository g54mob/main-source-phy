using UnityEngine;

namespace TFBGames
{
	[RequireComponent(typeof(ActionGlyphCombo))]
	public class ZoomActionGlyphComboRadialCheck : MonoBehaviour
	{
		private void Start()
		{
			if (ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("UI_INPUT_MODE").currentValue == 0)
			{
				ActionGlyphCombo component = base.gameObject.GetComponent<ActionGlyphCombo>();
				if (component != null)
				{
					component.AdditionalActionName = string.Empty;
					component.RefreshGlyph();
				}
				else
				{
					Debug.LogError("Component of type ActionGlyphCombo required but none found");
				}
			}
		}
	}
}
