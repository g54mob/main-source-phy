using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battlehub.RTCommon
{
	[DefaultExecutionOrder(-100)]
	public class RTEStandaloneInputModule : StandaloneInputModule, IRTEInputModule
	{
		public bool UseMouse = true;

		public event Action Update;

		protected override void Awake()
		{
			base.Awake();
			IOC.RegisterFallback((IRTEInputModule)this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			IOC.UnregisterFallback((IRTEInputModule)this);
		}

		public override void UpdateModule()
		{
			base.UpdateModule();
			this.Update?.Invoke();
		}

		public override void Process()
		{
			bool flag = SendUpdateEventToSelectedObject();
			if (base.eventSystem.sendNavigationEvents)
			{
				if (!flag)
				{
					flag |= SendMoveEventToSelectedObject();
				}
				if (!flag)
				{
					SendSubmitEventToSelectedObject();
				}
			}
			if (ProcessTouchEvents())
			{
				return;
			}
			if (UseMouse)
			{
				if (Input.mousePresent)
				{
					ProcessMouseEvent();
				}
			}
			else if (Input.GetMouseButtonDown(0))
			{
				Debug.LogWarning("Processing of touch events only. To enable processing of mouse events set RTEStandaloneInputModule.UseMouse = true");
			}
		}

		private bool ProcessTouchEvents()
		{
			for (int i = 0; i < Input.touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				if (touch.type != TouchType.Indirect)
				{
					bool pressed;
					bool released;
					PointerEventData touchPointerEventData = GetTouchPointerEventData(touch, out pressed, out released);
					ProcessTouchPress(touchPointerEventData, pressed, released);
					if (!released)
					{
						ProcessMove(touchPointerEventData);
						ProcessDrag(touchPointerEventData);
					}
					else
					{
						RemovePointerData(touchPointerEventData);
					}
				}
			}
			return Input.touchCount > 0;
		}
	}
}
