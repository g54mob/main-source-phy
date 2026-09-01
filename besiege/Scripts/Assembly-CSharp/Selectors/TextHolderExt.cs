using UnityEngine;

namespace Selectors
{
	public class TextHolderExt : TextHolder
	{
		public MonoBehaviour[] scriptsToDisable = new MonoBehaviour[0];

		private bool _enabled = true;

		protected override void Update()
		{
			base.Update();
			ToggleScripts(!base.IsFocused);
		}

		private void ToggleScripts(bool enable)
		{
			if (_enabled != enable)
			{
				text.transform.localScale = Vector3.one;
				_enabled = enable;
				for (int i = 0; i < scriptsToDisable.Length; i++)
				{
					scriptsToDisable[i].enabled = enable;
				}
			}
		}
	}
}
