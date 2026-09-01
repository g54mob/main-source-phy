using System;
using UnityEngine;
using UnityEngine.Events;

namespace LevelCreator
{
	public class OnTriggerEvent : MonoBehaviour
	{
		[Serializable]
		public class TriggerEvent : UnityEvent<Collider>
		{
		}

		public TriggerEvent enter = new TriggerEvent();

		public TriggerEvent exit = new TriggerEvent();

		private void OnTriggerEnter(Collider other)
		{
			enter.Invoke(other);
		}

		private void OnTriggerExit(Collider other)
		{
			exit.Invoke(other);
		}
	}
}
