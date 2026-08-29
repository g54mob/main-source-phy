using System;
using FMOD;
using UnityEngine;

public class Sound : MonoBehaviour
{
	public enum SoundType
	{
		Music = 0,
		SoundEffect = 1,
		Footstep = 2
	}

	public enum FalloffMode
	{
		NaturalFade = 0,
		LinearFade = 1,
		SmoothFade = 2
	}

	public string path;

	public string _event;

	public SoundType type;

	public bool isLoopable;

	public bool is2D;

	public float minDistance3D;

	public float maxDistance3D;

	public bool activeOnStart;

	public float volume;

	public bool localOnly;

	public FalloffMode falloffMode;

	public long handle;

	public long eventHandle;

	public Channel channel;

	public void update3D()
	{
		if (!is2D && type == SoundType.SoundEffect && channel.handle != IntPtr.Zero)
		{
			PineFmod.get3DAttributes(channel, out var _, out var vel);
			VECTOR pos2 = new VECTOR
			{
				x = base.transform.position.x,
				y = base.transform.position.y,
				z = base.transform.position.z
			};
			channel.set3DAttributes(ref pos2, ref vel);
		}
	}
}
