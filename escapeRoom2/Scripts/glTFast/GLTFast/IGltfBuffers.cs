using GLTFast.Schema;
using Unity.Collections;

namespace GLTFast
{
	internal interface IGltfBuffers
	{
		AccessorBase GetAccessor(int index);

		void GetAccessorDataAndByteStride(int index, out NativeSlice<byte> data, out int byteStride);

		unsafe void GetAccessorAndData(int index, out AccessorBase accessor, out void* data, out int byteStride);

		unsafe void GetAccessorSparseIndices(AccessorSparseIndices sparseIndices, out void* data);

		unsafe void GetAccessorSparseValues(AccessorSparseValues sparseValues, out void* data);

		NativeSlice<byte> GetBufferView(int bufferViewIndex, out int byteStride, int offset = 0, int length = 0);
	}
}
