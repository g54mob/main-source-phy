using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu(null)]
	public class MMObservableDemoObserverAutoSleep : MonoBehaviour
	{
		public MMObservableDemoSubject TargetSubject;

		protected virtual void OnSpeedChange()
		{
		}

		protected virtual void Awake()
		{
		}

		protected virtual void OnDestroy()
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
