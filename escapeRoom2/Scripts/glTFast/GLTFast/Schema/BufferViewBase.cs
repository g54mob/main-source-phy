using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class BufferViewBase<TExtensions> : BufferViewBase where TExtensions : BufferViewExtensions
	{
		public TExtensions extensions;

		public override BufferViewExtensions Extensions => extensions;
	}
	[Serializable]
	public abstract class BufferViewBase : NamedObject, IBufferView
	{
		public int buffer;

		public int byteOffset;

		public int byteLength;

		public int byteStride = -1;

		public int target;

		public int Buffer => buffer;

		public int ByteOffset => byteOffset;

		public int ByteLength => byteLength;

		public abstract BufferViewExtensions Extensions { get; }

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			writer.AddProperty("buffer", buffer);
			writer.AddProperty("byteLength", byteLength);
			if (byteOffset > 0)
			{
				writer.AddProperty("byteOffset", byteOffset);
			}
			if (byteStride > 0)
			{
				writer.AddProperty("byteStride", byteStride);
			}
			if (target > 0)
			{
				writer.AddProperty("target", target);
			}
			writer.Close();
		}
	}
}
