using System;
using UnityEngine;

public class DisabledRenderer : MonoBehaviour
{
	[Flags]
	public enum DisabledFlag
	{
		None = 0,
		Activator = 1,
		Culling = 2
	}

	public bool initialRendererEnable;

	public DisabledFlag flags;

	public bool hasFlag(DisabledFlag flag)
	{
		return (flags & flag) == flag;
	}
}
