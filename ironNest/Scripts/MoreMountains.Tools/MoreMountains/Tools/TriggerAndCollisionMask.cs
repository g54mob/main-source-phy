using System;

namespace MoreMountains.Tools
{
	[Flags]
	public enum TriggerAndCollisionMask
	{
		IgnoreAll = 0,
		OnTriggerEnter = 1,
		OnTriggerStay = 2,
		OnTriggerExit = 4,
		OnCollisionEnter = 8,
		OnCollisionStay = 0x10,
		OnCollisionExit = 0x20,
		OnTriggerEnter2D = 0x40,
		OnTriggerStay2D = 0x80,
		OnTriggerExit2D = 0x100,
		OnCollisionEnter2D = 0x200,
		OnCollisionStay2D = 0x400,
		OnCollisionExit2D = 0x800,
		OnAnyTrigger3D = 7,
		OnAnyCollision3D = 0x38,
		OnAnyTrigger2D = 0x1C0,
		OnAnyCollision2D = 0xE00,
		OnAnyTrigger = 0x1C7,
		OnAnyCollision = 0xE38,
		All_3D = 0x3F,
		All_2D = 0xFC0,
		All = 0xFFF
	}
}
