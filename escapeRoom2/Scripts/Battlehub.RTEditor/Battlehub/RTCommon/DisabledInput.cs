using System;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public class DisabledInput : IInput, ITouchInput
	{
		public bool IsTouchSupported => Input.touchSupported;

		public int TouchCount => 0;

		public float GetAxis(InputAxis axis)
		{
			return 0f;
		}

		public bool GetKey(KeyCode key)
		{
			return false;
		}

		public bool GetKeyDown(KeyCode key)
		{
			return false;
		}

		public bool GetKeyUp(KeyCode key)
		{
			return false;
		}

		public bool GetPointer(int button)
		{
			return false;
		}

		public bool GetPointerDown(int button)
		{
			return false;
		}

		public bool GetPointerUp(int button)
		{
			return false;
		}

		public Vector3 GetPointerXY(int pointer)
		{
			if (pointer == 0)
			{
				return Input.mousePosition;
			}
			return Input.GetTouch(pointer).position;
		}

		public bool IsAnyKeyDown()
		{
			return false;
		}

		public bool IsAnyKey()
		{
			return false;
		}

		public Touch GetTouch(int index)
		{
			throw new ArgumentOutOfRangeException();
		}
	}
}
