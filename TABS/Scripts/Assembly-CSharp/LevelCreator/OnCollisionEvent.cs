using System;
using UnityEngine;
using UnityEngine.Events;

namespace LevelCreator
{
	public class OnCollisionEvent : MonoBehaviour
	{
		[Serializable]
		public class CollisionEvent : UnityEvent<Collision>
		{
		}

		public CollisionEvent enter = new CollisionEvent();

		public CollisionEvent exit = new CollisionEvent();

		private void OnCollisionEnter(Collision other)
		{
			enter.Invoke(other);
		}

		private void OnCollisionExit(Collision other)
		{
			exit.Invoke(other);
		}
	}
}
