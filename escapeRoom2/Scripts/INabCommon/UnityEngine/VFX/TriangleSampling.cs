using System;

namespace UnityEngine.VFX
{
	[Serializable]
	[VFXType(VFXTypeAttribute.Usage.GraphicsBuffer, null)]
	public struct TriangleSampling
	{
		public Vector2 coord;

		public uint index;
	}
}
