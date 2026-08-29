using Unity.Collections;

namespace GLTFast.Loading
{
	public interface INativeDownload
	{
		NativeArray<byte>.ReadOnly NativeData { get; }
	}
}
