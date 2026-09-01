using System.Collections.Generic;
using UnityEngine;

namespace Poly.Timers
{
	public class FrameEndNotifier : MonoBehaviour
	{
		public List<FrameStartEndListener> listeners = new List<FrameStartEndListener>();

		private void Awake()
		{
			foreach (FrameStartEndListener listener in listeners)
			{
				listener.OnAwakeEnd();
			}
		}

		private void Start()
		{
			foreach (FrameStartEndListener listener in listeners)
			{
				listener.OnStartEnd();
			}
		}

		private void Update()
		{
			foreach (FrameStartEndListener listener in listeners)
			{
				listener.OnUpdateEnd();
			}
		}

		private void LateUpdate()
		{
			foreach (FrameStartEndListener listener in listeners)
			{
				listener.OnLateUpdateEnd();
			}
		}

		private void FixedUpdate()
		{
			foreach (FrameStartEndListener listener in listeners)
			{
				listener.OnFixedUpdateEnd();
			}
		}
	}
}
