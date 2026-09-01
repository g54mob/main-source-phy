using UnityEngine;

namespace TFBGames
{
	public class DelaySelectable
	{
		public bool IsDelayed;

		public int FrameDelay;

		public GameObject GameObjectToSelect;

		private readonly int DefaultFrameDelay = 1;

		public void SetDelay(GameObject targetSelectable)
		{
			IsDelayed = true;
			GameObjectToSelect = targetSelectable;
			FrameDelay = Time.frameCount + DefaultFrameDelay;
		}
	}
}
