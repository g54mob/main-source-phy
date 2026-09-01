using UnityEngine;

namespace Modding
{
	public class BlockScript : ModBlockBehaviour, ILimitsDisplay
	{
		public virtual Transform GetLimitsDisplay()
		{
			return handler.GetLimitsDisplay();
		}

		public virtual bool OnFlip()
		{
			return base.CanFlip;
		}
	}
}
