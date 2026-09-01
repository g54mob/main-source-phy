using System;
using UnityEngine;

namespace GamepadUI.StateManager.Core
{
	public abstract class UIComponentBase : UINavigationGroup
	{
		public UIState State { get; private set; }

		public bool IsActive { get; set; }

		public bool IsBackgrounded { get; set; }

		public Action Open => delegate
		{
			SetState(UIState.Open, isActive: true);
			OnOpen();
		};

		public Action Close => delegate
		{
			SetState(UIState.Closed, isActive: false);
			OnClose();
		};

		public Action Background => delegate
		{
			SetState(UIState.Closed, isActive: false, background: true);
			OnBackground();
		};

		protected abstract void OnOpen();

		protected abstract void OnClose();

		protected virtual void OnBackground()
		{
			Debug.Log(base.name + " was put in the Background");
		}

		private void SetState(UIState state, bool isActive, bool background = false)
		{
			State = state;
			IsActive = isActive;
			IsBackgrounded = background;
		}
	}
}
