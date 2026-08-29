using System;
using Unity.Collections;

namespace GLTFast
{
	public static class GltfGlobals
	{
		public const string GlbExt = ".glb";

		public const string GltfExt = ".gltf";

		public const string GltfPackageName = "com.unity.cloud.gltfast";

		public const uint GltfBinaryMagic = 1179937895u;

		public static bool IsGltfBinary(byte[] data)
		{
			return BitConverter.ToUInt32(data, 0) == 1179937895;
		}

		public static bool IsGltfBinary(NativeArray<byte>.ReadOnly data)
		{
			return (data[0] | (data[1] << 8) | (data[2] << 16) | (data[3] << 24)) == 1179937895;
		}
	}
}
