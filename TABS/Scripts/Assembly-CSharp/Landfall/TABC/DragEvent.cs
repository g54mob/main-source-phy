using System;
using UnityEngine;
using UnityEngine.Events;

namespace Landfall.TABC
{
	public class DragEvent : MonoBehaviour
	{
		public UnityEvent startDragEvent;

		public UnityEvent endDragEvent;

		private void Start()
		{
			DragHandler instance = DragHandler.instance;
			instance.EndDragAction = (Action)Delegate.Combine(instance.EndDragAction, new Action(EndDrag));
			DragHandler instance2 = DragHandler.instance;
			instance2.StartDragAction = (Action)Delegate.Combine(instance2.StartDragAction, new Action(StartDrag));
		}

		public void StartDrag()
		{
			startDragEvent.Invoke();
		}

		public void EndDrag()
		{
			endDragEvent.Invoke();
		}
	}
}
