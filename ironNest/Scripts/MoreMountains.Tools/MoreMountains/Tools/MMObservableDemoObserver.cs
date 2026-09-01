using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu(null)]
	public class MMObservableDemoObserver : MonoBehaviour
	{
		public MMObservableDemoSubject TargetSubject;

		protected virtual void OnPositionChange()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}
	}
}
