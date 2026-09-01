using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu(null)]
	public class MMObservableDemoSubject : MonoBehaviour
	{
		public MMObservable<float> PositionX;

		protected virtual void Update()
		{
		}
	}
}
