using UnityEngine;

namespace Selectors
{
	public class BaseHolder : MonoBehaviour
	{
		protected bool stoppedHotkeys;

		protected void StopHotkeys(bool toggle)
		{
			if (stoppedHotkeys != toggle)
			{
				stoppedHotkeys = toggle;
				StatMaster.StopHotKeys(toggle);
			}
		}

		protected void TextFieldSelected(bool toggle)
		{
			StatMaster.textFieldSelected = toggle;
		}

		protected virtual void OnDisable()
		{
			StopHotkeys(false);
		}

		protected virtual void OnDestroy()
		{
			StopHotkeys(false);
		}
	}
}
