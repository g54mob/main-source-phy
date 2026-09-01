using System;
using BitCode.UI;
using InControl;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TFBGames
{
	public class InControlRadialMenuInputProvider : MonoBehaviour, IRadialMenuInputProvider<BattleRadialButton>
	{
		public RadialMenuInputState InputState => RadialMenuInputState.Absolute;

		public Vector2 GetAbsoluteInput()
		{
			InputDevice activeDevice = InputManager.ActiveDevice;
			return MeanVector(activeDevice.LeftStick.Value, activeDevice.RightStick.Value);
		}

		public Vector2 GetRelativeInput()
		{
			throw new NotImplementedException();
		}

		public void SelectItem(BattleRadialButton button)
		{
			EventSystem.current.SetSelectedGameObject(null);
			button.Select();
		}

		public Vector2 MeanVector(Vector2 first, Vector2 second)
		{
			return new Vector2((first.x + second.x) / 2f, (first.y + second.y) / 2f);
		}
	}
}
