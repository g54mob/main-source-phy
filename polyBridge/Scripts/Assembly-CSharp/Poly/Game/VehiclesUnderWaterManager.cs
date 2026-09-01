using Poly.UI;
using UnityEngine;

namespace Poly.Game
{
	public class VehiclesUnderWaterManager : MonoBehaviour
	{
		public VehiclesUnderWater implementation;

		[ShowIf("false", false, false, "")]
		public string status;

		public int realInstanceID;

		private void OnValidate()
		{
			if (implementation != null)
			{
				realInstanceID = implementation.instanceId;
			}
			status = ((implementation == VehiclesUnderWater.instance) ? "This is active instance" : "Not used in world");
		}
	}
}
