using UnityEngine;

namespace Battlehub.RTCommon
{
	public interface ITouchInput
	{
		bool IsTouchSupported { get; }

		int TouchCount { get; }

		Touch GetTouch(int index);
	}
}
