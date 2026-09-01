using UnityEngine;

namespace Interface.QuickSelect
{
	public class GenericMenu : QuickMenu
	{
		public override void OnSelect(int index)
		{
			Debug.Log("[GenericMenu] Select " + index);
		}
	}
}
