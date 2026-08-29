using UnityEngine;

namespace Battlehub.RTCommon
{
	public interface IInput
	{
		bool IsAnyKeyDown();

		bool IsAnyKey();

		float GetAxis(InputAxis axis);

		bool GetKeyDown(KeyCode key);

		bool GetKeyUp(KeyCode key);

		bool GetKey(KeyCode key);

		bool GetPointerDown(int button);

		bool GetPointerUp(int button);

		bool GetPointer(int button);

		Vector3 GetPointerXY(int pointer);
	}
}
