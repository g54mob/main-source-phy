using UnityEngine;
using UnityEngine.Events;

namespace MoreMountains.Tools
{
	public class MMGameEventListener : MonoBehaviour, MMEventListener<MMGameEvent>, MMEventListenerBase
	{
		[Header("MMGameEvent")]
		[Tooltip("the name of the event you want to listen for")]
		public string EventName;

		[Tooltip("a UnityEvent hook you can use to call methods when the specified event gets triggered")]
		public UnityEvent OnMMGameEvent;

		public void OnMMEvent(MMGameEvent gameEvent)
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
