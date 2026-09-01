using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamepadUI.StateManager.Core
{
	[Serializable]
	public class UIComponentContainer
	{
		[Tooltip("For editor use.")]
		public string UIComponentName;

		public GameObject ContainerObject;

		public UIComponent Component;

		public bool IsStackable;

		public bool HasStackParent = true;

		[FormerlySerializedAs("OpenByDefault")]
		[Tooltip("Do not mark more than one UIComponent as default.")]
		public bool DefaultState;

		public bool EnableOnAwake;

		public UIPauseState PauseState;

		[FormerlySerializedAs("MenuButtons")]
		[Space]
		public List<ChangeStateEvent> ChangeStateEvents;
	}
}
