using System.Collections.Generic;
using UnityEngine;

namespace FMODUnity
{
	[AddComponentMenu("FMOD Studio/FMOD Studio Listener")]
	public class StudioListener : MonoBehaviour
	{
		[SerializeField]
		private bool nonRigidbodyVelocity;

		[SerializeField]
		private GameObject attenuationObject;

		private Vector3 lastFramePosition;

		private Rigidbody rigidBody;

		private Rigidbody2D rigidBody2D;

		private static List<StudioListener> listeners;

		public GameObject AttenuationObject
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public static int ListenerCount => 0;

		public int ListenerNumber => 0;

		public static float DistanceToNearestListener(Vector3 position)
		{
			return 0f;
		}

		public static float DistanceSquaredToNearestListener(Vector3 position)
		{
			return 0f;
		}

		private static void AddListener(StudioListener listener)
		{
		}

		private static void RemoveListener(StudioListener listener)
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void Update()
		{
		}
	}
}
