using UnityEngine.EventSystems;

namespace Meta.XR.ImmersiveDebugger.UserInterface
{
	public class PanelInputModule : OVRInputModule
	{
		private void Update()
		{
			if (base.eventSystem.currentInputModule != this)
			{
				Process();
			}
		}
	}
}
