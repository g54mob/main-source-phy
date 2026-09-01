using UnityEngine;

namespace Interface.QuickSelect
{
	public class BuildMenu : QuickMenu
	{
		public override void OnSelect(int index)
		{
			Debug.Log("[BuildMenu] Select " + index);
		}
	}
}
