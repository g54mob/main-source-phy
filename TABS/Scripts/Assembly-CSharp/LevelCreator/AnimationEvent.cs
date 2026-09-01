using UnityEngine;
using UnityEngine.Events;

namespace LevelCreator
{
	public class AnimationEvent : MonoBehaviour
	{
		public UnityEvent Event;

		public void InvokeEvent()
		{
			Event.Invoke();
		}
	}
}
