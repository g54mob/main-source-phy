using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ModIO.UI
{
	public class ViewControlBindings : MonoBehaviour
	{
		[Serializable]
		public class ButtonEvent : UnityEvent
		{
		}

		[Flags]
		public enum ButtonTriggerCondition
		{
			OnDown = 1,
			OnUp = 2,
			OnHeld = 4
		}

		[Flags]
		public enum AxisTriggerCondition
		{
			BecameGreaterThan = 1,
			BecameLessThan = 2,
			BecameEqualTo = 4,
			IsGreaterThan = 8,
			IsLessThan = 0x10,
			IsEqualTo = 0x20
		}

		[Serializable]
		public struct ButtonBinding
		{
			public string inputName;

			public ButtonTriggerCondition condition;

			public ButtonEvent actions;
		}

		[Serializable]
		public struct KeyCodeBinding
		{
			public KeyCode keyCode;

			public ButtonTriggerCondition condition;

			public ButtonEvent actions;
		}

		[Serializable]
		public class AxisEvent : UnityEvent<float>
		{
		}

		[Serializable]
		public struct AxisBinding
		{
			public string inputName;

			public float thresholdValue;

			public AxisTriggerCondition condition;

			public AxisEvent actions;
		}

		public List<ButtonBinding> buttonBindings = new List<ButtonBinding>();

		public List<KeyCodeBinding> keyCodeBindings = new List<KeyCodeBinding>();

		public List<AxisBinding> axisBindings = new List<AxisBinding>();
	}
}
