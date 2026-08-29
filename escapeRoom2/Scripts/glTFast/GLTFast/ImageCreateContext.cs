using System.Runtime.InteropServices;
using Unity.Jobs;

namespace GLTFast
{
	internal struct ImageCreateContext
	{
		public int imageIndex;

		public byte[] buffer;

		public GCHandle gcHandle;

		public JobHandle jobHandle;
	}
}
