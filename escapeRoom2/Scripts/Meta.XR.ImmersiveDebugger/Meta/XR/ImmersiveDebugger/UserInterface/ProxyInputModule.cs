using UnityEngine;
using UnityEngine.EventSystems;

namespace Meta.XR.ImmersiveDebugger.UserInterface
{
	internal class ProxyInputModule
	{
		private readonly GameObject _owner;

		private readonly OVRCursor _cursor;

		private EventSystem _eventSystem;

		public OVRInputModule InputModule { get; private set; }

		public ProxyInputModule(GameObject owner, OVRCursor cursor)
		{
			_cursor = cursor;
			_owner = owner;
		}

		public bool Refresh()
		{
			if (InputModule != null && InputModule.isActiveAndEnabled)
			{
				return true;
			}
			SearchForEventSystem();
			return InputModule;
		}

		private void SearchForEventSystem()
		{
			EventSystem eventSystem = Object.FindAnyObjectByType<EventSystem>();
			if (!eventSystem && RuntimeSettings.Instance.CreateEventSystem)
			{
				eventSystem = _owner.AddComponent<EventSystem>();
			}
			SetupEventSystem(eventSystem);
		}

		private void SetupEventSystem(EventSystem eventSystem)
		{
			_eventSystem = eventSystem;
			if ((bool)_eventSystem)
			{
				if (!_eventSystem.TryGetComponent<OVRInputModule>(out var component))
				{
					component = _eventSystem.gameObject.AddComponent<PanelInputModule>();
					_eventSystem.UpdateModules();
				}
				SetupInputModule(component);
			}
		}

		private void SetupInputModule(OVRInputModule inputModule)
		{
			InputModule = inputModule;
			if ((bool)InputModule)
			{
				OVRInputModule inputModule2 = InputModule;
				if ((object)inputModule2.m_Cursor == null)
				{
					inputModule2.m_Cursor = _cursor;
				}
				InputModule.allowActivationOnMobileDevice = true;
				InputModule.joyPadClickButton = OVRInput.Button.Any;
			}
		}
	}
}
