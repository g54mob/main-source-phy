using System;
using UnityEngine;
using UnityEngine.Events;

namespace Landfall.TABC
{
	public class LevelUpEvent : MonoBehaviour
	{
		public UnityEvent levelUpEvent;

		public UnityEvent visualLevelUpEvent;

		public void LevelUp(int xpNeededThisLevel)
		{
			levelUpEvent.Invoke();
		}

		public void VisualLevelUp(int xpNeededThisLevel)
		{
			visualLevelUpEvent.Invoke();
		}

		private void Start()
		{
			XPHandlerClient instance = XPHandlerClient.instance;
			instance.LevelUpAction = (Action<int>)Delegate.Combine(instance.LevelUpAction, new Action<int>(LevelUp));
			XPHandlerClient instance2 = XPHandlerClient.instance;
			instance2.VisualLevelUpAction = (Action<int>)Delegate.Combine(instance2.VisualLevelUpAction, new Action<int>(VisualLevelUp));
		}
	}
}
