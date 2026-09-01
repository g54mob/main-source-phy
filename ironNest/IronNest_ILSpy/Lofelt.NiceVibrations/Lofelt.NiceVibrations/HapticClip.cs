using UnityEngine;

namespace Lofelt.NiceVibrations;

public class HapticClip : ScriptableObject
{
	public byte[] json;

	public GamepadRumble gamepadRumble;
}
