using UnityEngine;

namespace FluffyUnderware.Curvy.Controllers
{
	public class CurvyControllerEventArgs : CurvyEventArgs
	{
		public readonly CurvyController Controller;

		public CurvyControllerEventArgs(MonoBehaviour sender, CurvyController controller)
			: base(sender, null)
		{
			Controller = controller;
		}
	}
}
