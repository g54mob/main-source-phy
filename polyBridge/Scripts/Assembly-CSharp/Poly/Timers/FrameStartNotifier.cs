using System.Collections.Generic;
using UnityEngine;

namespace Poly.Timers
{
	public class FrameStartNotifier : MonoBehaviour
	{
		public List<FrameStartEndListener> listeners = new List<FrameStartEndListener>();

		private void Awake()
		{
			foreach (FrameStartEndListener listener in listeners)
			{
				listener.OnAwakeBegin();
			}
		}

		private void Start()
		{
			foreach (FrameStartEndListener listener in listeners)
			{
				listener.OnStartBegin();
			}
		}

		private void Update()
		{
			foreach (FrameStartEndListener listener in listeners)
			{
				listener.OnUpdateBegin();
			}
		}

		private void LateUpdate()
		{
			foreach (FrameStartEndListener listener in listeners)
			{
				listener.OnLateUpdateBegin();
			}
		}

		private void FixedUpdate()
		{
			foreach (FrameStartEndListener listener in listeners)
			{
				listener.OnFixedUpdateBegin();
			}
		}
	}
}
