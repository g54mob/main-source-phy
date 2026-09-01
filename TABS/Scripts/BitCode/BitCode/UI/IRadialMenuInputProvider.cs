using UnityEngine;

namespace BitCode.UI
{
	public interface IRadialMenuInputProvider<in TItem>
	{
		RadialMenuInputState InputState { get; }

		Vector2 GetAbsoluteInput();

		Vector2 GetRelativeInput();

		void SelectItem(TItem item);
	}
}
