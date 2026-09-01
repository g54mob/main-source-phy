using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMInput : MonoBehaviour
	{
		public enum ButtonStates
		{
			Off = 0,
			ButtonDown = 1,
			ButtonPressed = 2,
			ButtonUp = 3
		}

		public enum AxisTypes
		{
			Positive = 0,
			Negative = 1
		}

		public class IMButton
		{
			public delegate void ButtonDownMethodDelegate();

			public delegate void ButtonPressedMethodDelegate();

			public delegate void ButtonUpMethodDelegate();

			public string ButtonID;

			public ButtonDownMethodDelegate ButtonDownMethod;

			public ButtonPressedMethodDelegate ButtonPressedMethod;

			public ButtonUpMethodDelegate ButtonUpMethod;

			protected float _lastButtonDownAt;

			protected float _lastButtonUpAt;

			public MMStateMachine<ButtonStates> State { get; protected set; }

			public virtual float TimeSinceLastButtonDown => 0f;

			public virtual float TimeSinceLastButtonUp => 0f;

			public virtual bool IsPressed => false;

			public virtual bool IsDown => false;

			public virtual bool IsUp => false;

			public virtual bool IsOff => false;

			public virtual bool ButtonDownRecently(float time)
			{
				return false;
			}

			public virtual bool ButtonUpRecently(float time)
			{
				return false;
			}

			public IMButton(string playerID, string buttonID, ButtonDownMethodDelegate btnDown = null, ButtonPressedMethodDelegate btnPressed = null, ButtonUpMethodDelegate btnUp = null)
			{
			}

			public virtual void TriggerButtonDown()
			{
			}

			public virtual void TriggerButtonPressed()
			{
			}

			public virtual void TriggerButtonUp()
			{
			}
		}

		public static ButtonStates ProcessAxisAsButton(string axisName, float threshold, ButtonStates currentState, AxisTypes AxisType = AxisTypes.Positive)
		{
			return default(ButtonStates);
		}
	}
}
